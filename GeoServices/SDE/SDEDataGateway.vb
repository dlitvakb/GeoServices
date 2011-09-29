Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    ''' <summary>
    ''' Clase abstracta que permite la obtención genérica de datos del SDE
    ''' </summary>
    ''' <typeparam name="T">Clase que representa al elemento a obtener del SDE. Por Ejemplo: IFeatureClass, ITable</typeparam>
    Public MustInherit Class SDEDataGateway(Of T As {Class})
        ''' <summary>
        ''' Permite obtener todos los elementos del tipo especificado en la subclase presentes en el SDE para la conexión especificada.
        ''' Por defecto retorna unicamente los elementos para los cuales se tienen permisos de edición, para obtener todos los elementos
        ''' en el parámetro RequiresEditorPriviledges poner el valor False
        ''' </summary>
        Public Function GetAll(Optional ByVal connectionNumber As Integer = 0, Optional ByVal Privileges As SDE.SDEPrivileges = SDE.SDEPrivileges.SDEEdit) As List(Of T)
            Dim wksp As IWorkspace = New XML.XMLWorkspaceGetter().GetSingleWorkspace(connectionNumber)
            If wksp Is Nothing Then Throw New DataException("No se ha provisto ningún workspace")

            Dim elements As List(Of T) = Me.doGetAll(wksp, Privileges)

            If elements.Count = 0 Then Throw New DataException("No se ha encontrado ninguna/a " & Me.GetElementName())

            Return elements
        End Function

        Protected MustOverride Function doGetAll(ByVal workspace As IWorkspace, ByVal Privileges As SDE.SDEPrivileges) As List(Of T)

        ''' <summary>
        ''' Realiza la verificación de nombres para el objeto de SDE
        ''' </summary>
        Protected MustOverride Function IsNameEquals(ByVal element As T, ByVal name As String) As Boolean

        ''' <summary>
        ''' Obtiene el nombre del tipo de elemento a utilizar por la clase para ser mostrado en la descripción del error
        ''' </summary>
        Protected MustOverride Function GetElementName() As String

        ''' <summary>
        ''' Obtiene el nombre del tipo de elemento a utilizar por la clase para ser mostrado en la descripción del error
        ''' </summary>
        Protected MustOverride Function GetPluralName() As String

        ''' <summary>
        ''' Permite obtener elementos del SDE en base a una lista de nombres
        ''' </summary>
        ''' <remarks>Por defecto, si no se encuentra algún elemento, se lanza una DataException, sin embargo, cambiando GetResultsAnyway, permite obtener las que se haya encontrado</remarks>
        Public Function GetByNameList(ByVal names As String(), Optional ByVal connectionNumber As Integer = 0, Optional ByVal GetResultsAnyway As Boolean = False, Optional ByVal Privileges As SDE.SDEPrivileges = SDE.SDEPrivileges.SDEEdit) As List(Of T)
            Dim elements As List(Of T) = Me.GetAll(connectionNumber, Privileges)
            Dim result As New List(Of T)

            For Each name As String In names
                For Each element As T In elements
                    If Me.IsNameEquals(element, name) AndAlso Me.PermissionsValidation(element, Privileges) Then
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
        Public Overridable Function GetByName(ByVal name As String, Optional ByVal connectionNumber As Integer = 0, Optional ByVal Privileges As SDE.SDEPrivileges = SDE.SDEPrivileges.SDEEdit) As T
            Try
                Return Me.GetByNameList({name}, connectionNumber, Privileges:=Privileges)(0)
            Catch ex As Exception
                Throw New DataException("El/La " & Me.GetElementName() & " " & name & " no se ha encontrado", ex)
            End Try
        End Function

        ''' <summary>
        ''' Realiza chequeos de permisos sobre los objetos SDE
        ''' </summary>
        Protected Overridable Function PermissionsValidation(ByVal element As T, Optional ByVal Privileges As SDE.SDEPrivileges = SDE.SDEPrivileges.SDEEdit) As Boolean
            Return New SDE.PrivilegesValidator(element).HasPrivileges(Privileges)
        End Function

        Protected Function SanitizeString(ByVal text As String) As String
            Return text.ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        End Function
    End Class
End Namespace