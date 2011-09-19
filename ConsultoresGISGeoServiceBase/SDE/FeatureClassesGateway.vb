Imports ESRI.ArcGIS.Geodatabase
Imports System.Xml

Namespace SDE
    ''' <summary>
    ''' Permite obtener FeatureClasses y Tablas de una base de datos SDE
    ''' </summary>
    ''' <remarks>
    ''' Los esquemas de obtención de datos están adaptados siguiente formato:
    '''     SDE -> FeatureClasses (muchos),
    '''     SDE -> FeatureDatasets (muchos) -> FeatureClasses (muchos)
    ''' 
    ''' Si se desea alterar, debe crease una nueva clase que herede de XMLGetter.
    ''' En caso de querer el mismo esquema, pero requerir una validación diferente para los FeatureClass o Tablas
    ''' se debe crear una clase que herede de esta y sobreescribir el método isValid realizando la validación correspondiente 
    ''' </remarks>
    Public Class FeatureClassesGateway
        Inherits SDEDataGateway(Of IFeatureClass)

        ''' <summary>
        ''' Retorna el siguiente Feature Class
        ''' </summary>
        ''' <param name="subset"></param>
        ''' <param name="dataset"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function getNextFClass(ByRef subset As IEnumDataset, ByVal dataset As IDataset) As IFeatureClass
            While Not isFClass(dataset)
                dataset = subset.Next
                If dataset Is Nothing Then Return Nothing
            End While
            Return dataset
        End Function

        ''' <summary>
        ''' Retorna si el Dataset ingresado es Feature Class
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function isFClass(ByVal dataset As IDataset) As Boolean
            Return TypeOf dataset Is IFeatureClass OrElse dataset Is Nothing
        End Function

        ''' <summary>
        ''' Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' La validación realizada acá es que sea una capa en producción y no sea de red.
        ''' Para realizar otra validación se debe generar una subclase que sobreescriba este método
        ''' </remarks>
        Protected Overridable Function validDataset(ByVal dataset As IDataset) As Boolean
            Return Not Me.sanitizeName(dataset.Name).ToUpper Like "*AUDITORIA*" AndAlso Not Me.sanitizeName(dataset.Name).ToUpper Like "*HISTORICO*" AndAlso Not Me.sanitizeName(dataset.Name).ToUpper Like "*RED*" AndAlso TypeOf dataset Is IFeatureClass
        End Function

        ''' <summary>
        ''' Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        ''' </summary>
        ''' <param name="fclass"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' La validación realizada acá es que sea una capa en producción y no sea de red.
        ''' Para realizar otra validación se debe generar una subclase que sobreescriba este método
        ''' </remarks>
        Protected Overridable Function validFeatureClass(ByVal fclass As IFeatureClass) As Boolean
            Return Not Me.sanitizeName(fclass.AliasName).ToUpper Like "*AUDITORIA*" AndAlso Not Me.sanitizeName(fclass.AliasName).ToUpper Like "*HISTORICO*" AndAlso Not Me.sanitizeName(fclass.AliasName).ToUpper Like "*RED*"
        End Function

        ''' <summary>
        ''' Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' La validación realizada acá es que sea una capa en producción y no sea de red.
        ''' Para realizar otra validación se debe generar una subclase que sobreescriba este método
        ''' </remarks>
        Protected Overridable Function isValid(ByVal dataset As IDataset) As Boolean
            Return TypeOf dataset Is IFeatureClass AndAlso Me.validDataset(dataset) AndAlso Me.validFeatureClass(dataset)
        End Function

        Protected Function sanitizeName(ByVal name As String)
            Return name.ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        End Function

        ''' <summary>
        ''' Obtiene una lista de todos los FeatureClasses para la conexión especificada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function doGetAll(ByVal workspace As ESRI.ArcGIS.Geodatabase.IWorkspace) As System.Collections.Generic.List(Of ESRI.ArcGIS.Geodatabase.IFeatureClass)
            Dim fclasses As New List(Of IFeatureClass)

            Dim datasets As IEnumDataset = workspace.Datasets(esriDatasetType.esriDTFeatureDataset)
            Dim featureClasses As IEnumDataset = workspace.Datasets(esriDatasetType.esriDTFeatureClass)

            Dim fclass As IFeatureClass = featureClasses.Next

            While Not fclass Is Nothing
                If Me.isValid(fclass) Then fclasses.Add(fclass)
                fclass = featureClasses.Next
            End While

            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClasses)

            Dim dataset As IFeatureDataset = datasets.Next
            While Not dataset Is Nothing
                If Me.isValid(dataset) Then fclasses.Add(dataset)
                If Not dataset.Subsets Is Nothing Then
                    Dim subset As IEnumDataset = dataset.Subsets
                    fclass = Me.getNextFClass(subset, subset.Next)
                    While Not fclass Is Nothing
                        If Me.isValid(fclass) Then fclasses.Add(fclass)
                        fclass = Me.getNextFClass(subset, subset.Next)
                    End While
                End If
                dataset = datasets.Next
            End While

            System.Runtime.InteropServices.Marshal.ReleaseComObject(datasets)

            Return fclasses
        End Function

        Protected Overrides Function GetElementName() As String
            Return "Feature Class"
        End Function

        Protected Overrides Function GetPluralName() As String
            Return "Feature Classes"
        End Function

        Protected Overrides Function ExtraValidation(ByVal element As ESRI.ArcGIS.Geodatabase.IFeatureClass, ByVal name As String) As Boolean
            Return element.AliasName.ToUpper.Contains(name.ToUpper)
        End Function
    End Class
End Namespace