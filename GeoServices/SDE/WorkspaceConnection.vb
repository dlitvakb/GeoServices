Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesGDB

Namespace SDE
    Public Class WorkspaceConnection
        Private Property Username As String
        Private Property Password As String
        Private Property Server As String
        Private Property Instance As String
        Private Property Database As String
        Private Property Version As String

        Public Sub New(ByVal username As String, ByVal passwordEncriptado As String, ByVal server As String, ByVal instance As String, ByVal database As String, ByVal version As String)
            Me.Username = username
            Me.Password = passwordEncriptado
            Me.Server = server
            Me.Instance = instance
            Me.Database = database
            Me.Version = version
        End Sub

        Public Function GetWorkspace() As IWorkspace
            Try
                Dim properties As IPropertySet2 = New PropertySetClass()
                properties.SetProperty("USER", Me.Username)
                properties.SetProperty("PASSWORD", New Security.PrivateEncrypt().Decrypt(Me.Password))
                properties.SetProperty("SERVER", Me.Server)
                properties.SetProperty("INSTANCE", Me.Instance)
                properties.SetProperty("DATABASE", Me.Database)
                properties.SetProperty("VERSION", Me.Version)

                Return New SdeWorkspaceFactory().Open(properties, 0)
            Catch ex As Exception
                Throw New DataException("No se pudo obtener el Workspace para la configuración especificada", ex)
            End Try
        End Function
    End Class
End Namespace