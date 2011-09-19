Imports ESRI.ArcGIS.Geodatabase

''' <summary>
''' Clase abstracta que permite la obtención genérica de datos del SDE
''' </summary>
''' <typeparam name="T">Clase que representa al elemento a obtener del SDE. Por Ejemplo: IFeatureClass, ITable</typeparam>
''' <remarks></remarks>
Public MustInherit Class SDEDataGateway(Of T As {Class})
    ''' <summary>
    ''' Permite obtener todos los elementos del tipo especificado en la subclase presentes en el SDE para la conexión especificada
    ''' </summary>
    ''' <param name="connectionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll(Optional ByVal connectionNumber As Integer = 0) As List(Of T)
        Dim wksp As IWorkspace = New XML.XMLWorkspaceGetter().GetSingleWorkspace(connectionNumber)
        If wksp Is Nothing Then Throw New DataException("No se ha provisto ningún workspace")

        Dim elements As List(Of T) = Me.doGetAll(wksp)

        If elements.Count = 0 Then Throw New DataException("No se ha encontrado ninguna/a " & Me.GetElementName())

        Return elements
    End Function

    Protected MustOverride Function doGetAll(ByVal workspace As IWorkspace) As List(Of T)

    ''' <summary>
    ''' Obtiene el nombre del tipo de elemento a utilizar por la clase para ser mostrado en la descripción del error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function GetElementName() As String

    ''' <summary>
    ''' Obtiene el nombre del tipo de elemento a utilizar por la clase para ser mostrado en la descripción del error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function GetPluralName() As String

    ''' <summary>
    ''' Permite obtener elementos del SDE en base a una lista de nombres
    ''' </summary>
    ''' <param name="names"></param>
    ''' <param name="connectionNumber"></param>
    ''' <param name="GetResultsAnyway"></param>
    ''' <returns></returns>
    ''' <remarks>Por defecto, si no se encuentra algún elemento, se lanza una DataException, sin embargo, cambiando GetResultsAnyway, permite obtener las que se haya encontrado</remarks>
    Public Function GetByNameList(ByVal names As String(), Optional ByVal connectionNumber As Integer = 0, Optional ByVal GetResultsAnyway As Boolean = False) As List(Of T)
        Dim elements As List(Of T) = Me.GetAll(connectionNumber)
        Dim result As New List(Of T)

        For Each name As String In names
            For Each element As T In elements
                If CType(element, IDataset).Name.ToUpper().Contains(name.ToUpper()) OrElse Me.ExtraValidation(element, name) Then
                    result.Add(element)
                    Exit For
                End If
            Next
        Next

        If result.Count <> names.Length AndAlso Not GetResultsAnyway Then Throw New DataException("No se encontraron " & Me.GetPluralName() & " para los nombres especificados")

        Return result
    End Function

    ''' <summary>
    ''' Permite obtener elementos singulares dentro del SDE
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="connectionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetByName(ByVal name As String, Optional ByVal connectionNumber As Integer = 0) As T
        Try
            Return Me.GetByNameList({name}, connectionNumber)(0)
        Catch ex As Exception
            Throw New DataException("El/La " & Me.GetElementName() & " " & name & " no se ha encontrado", ex)
        End Try
    End Function

    ''' <summary>
    ''' Cualquier validación extra que se quiera hacer para obtener los elementos del SDE en las busquedas por nombre y lista de nombres
    ''' </summary>
    ''' <param name="element"></param>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function ExtraValidation(ByVal element As T, ByVal name As String) As Boolean
        Return False
    End Function
End Class
