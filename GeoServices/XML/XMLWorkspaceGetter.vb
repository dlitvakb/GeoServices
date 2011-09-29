Imports System.Xml
Imports ESRI.ArcGIS.Geodatabase

Namespace XML
    ''' <summary>
    ''' Obtiene Conexiones a una base de datos SDE a partir de la configuración especificada en Config.xml
    ''' </summary>
    Public Class XMLWorkspaceGetter
        Inherits XMLGetter

        ''' <summary>
        ''' Obtiene una lista de propiedades de conexiones
        ''' </summary>
        Public Function GetAllConnections() As List(Of SDE.WorkspaceConnection)
            Dim connections As New List(Of SDE.WorkspaceConnection)

            Try
                For Each node As XmlElement In getSingleXMLElement("sdeconnections").ChildNodes
                    connections.Add(New SDE.WorkspaceConnection(node.GetAttribute("username"), node.GetAttribute("password"), node.GetAttribute("server"), node.GetAttribute("instance"), node.GetAttribute("database"), node.GetAttribute("sdeversion")))
                Next
            Catch ex As Exception
                Logger.Logger.Error(ex)
            End Try

            Return connections
        End Function

        ''' <summary>
        ''' Obtiene una lista de elementos que conforman las propiedades de conexión necesarias para una Base de Datos SDE
        ''' </summary>
        Public Function GetSingleConnection(Optional ByVal index As Integer = 0) As SDE.WorkspaceConnection
            Return Me.GetAllConnections(index)
        End Function

        ''' <summary>
        ''' Obtiene la conexion a una base de datos SDE
        ''' </summary>
        ''' <returns>Retorna un SdeWorkspace</returns>
        Public Function GetSingleWorkspace(Optional ByVal index As Integer = 0) As IWorkspace
            Return Me.GetSingleConnection(index).GetWorkspace()
        End Function

        ''' <summary>
        ''' Obtiene una lista de workspaces a partir de las conexiones especificadas
        ''' </summary>
        Public Function GetAllWorkspaces() As List(Of IWorkspace)
            Dim workspaces As New List(Of IWorkspace)

            For Each conn As SDE.WorkspaceConnection In Me.GetAllConnections()
                workspaces.Add(conn.GetWorkspace())
            Next

            Return workspaces
        End Function
    End Class
End Namespace