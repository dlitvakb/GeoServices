Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

Namespace License
    ''' <summary>
    ''' Inicializador de licencias
    ''' </summary>
    ''' <remarks>Es utilizado para comprobar que las licencias activas tienen permisos de edición sobre el SDE</remarks>
    Public Class LicenseInitializer
        Private Const ARCSERVER_ERROR As String = "FAILED TO BIND TO AN ARCGIS SERVER RUNTIME"

        ''' <summary>
        ''' Inicializa la licencia, para que sea exitoso, se debe tener una de las siguientes 4 licencias (las cuales tienen permisos sobre SDE) 
        ''' y seran buscadas en el siguiente orden:
        ''' 1) ArcServer
        ''' 2) ArcEngine Geodatabase Extension
        ''' 3) ArcEditor
        ''' 4) ArcInfo
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub InitializeLicense()
            Try
                RuntimeManager.BindLicense(ProductCode.Server, LicenseLevel.GeodatabaseUpdate)
            Catch ex As RuntimeManagerException
                If ex.Message.ToUpper().Contains(ARCSERVER_ERROR) Then
                    RuntimeManager.BindLicense(ProductCode.EngineOrDesktop, LicenseLevel.GeodatabaseUpdate)
                Else
                    Throw ex
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Libera la licencia
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ReleaseLicense()
            Dim license As New AoInitialize()
            license.Shutdown()
        End Sub
    End Class
End Namespace