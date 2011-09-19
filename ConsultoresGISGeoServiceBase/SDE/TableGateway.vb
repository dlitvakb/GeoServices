Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    Public Class TableGateway
        Inherits SDEDataGateway(Of ITable)

        ''' <summary>
        ''' Obtiene todas las tablas del SDE para la conexión especificada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>El esquema de obtención está realizado para tablas sin anidamiento que se encuentran en la raíz del SDE</remarks>
        Protected Overrides Function doGetAll(ByVal workspace As ESRI.ArcGIS.Geodatabase.IWorkspace) As System.Collections.Generic.List(Of ESRI.ArcGIS.Geodatabase.ITable)
            Dim tables As New List(Of ITable)

            Dim datasets As IEnumDataset = workspace.Datasets(esriDatasetType.esriDTTable)
            Dim table As IDataset = datasets.Next
            While Not table Is Nothing
                tables.Add(table)
                table = datasets.Next
            End While

            System.Runtime.InteropServices.Marshal.ReleaseComObject(datasets)

            Return tables
        End Function

        Protected Overrides Function GetElementName() As String
            Return "Tabla"
        End Function

        Protected Overrides Function GetPluralName() As String
            Return "Tablas"
        End Function
    End Class
End Namespace
