Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem

Namespace Worker
    ''' <summary>
    ''' Wrapper de GeoService
    ''' Inicializa licencia y captura errores
    ''' Esta clase debe ser heredada y sus subclases llamadas desde el método doWork() de las subclases de ServiceHandler
    ''' </summary>
    ''' <remarks>
    ''' Los errores generados por el usuario deben ser del tipo DataException
    ''' Los errores generados por ESRI son del tipo COMException
    ''' Dichas excepciones no interrumpirán el flujo de ejecución, 
    ''' y son consideradas como intermedias (usuario) o graves (framework), 
    ''' pero no detendrán la ejecución del servicio, ya que deben ser consideradas 
    ''' como un error de configuración en el Config.xml
    ''' 
    ''' En caso de requerir otro tipo de excepción, ServiceHandler provee el manejo de excepciones
    ''' consideradas como intermedias y graves que ameritan la detención del servicio.
    ''' UnauthorizedAccessException es considerada intermedia y es utilizada para
    ''' el aviso de una instanciación ilegal de una clase o la falta de licencia del producto.
    ''' Exception es cualquier otro tipo de excepción (arrojada por el usuario) que sea considerada grave,
    ''' la misma detendrá la ejecución del servicio.
    ''' </remarks>
    Public MustInherit Class WorkingComponent
        Private Const LICENSE_EXCEPTION_MESSAGE As String = "ARCGIS VERSION NOT SPECIFIED"

        Public Sub Execute()
            Try
                License.LicenseInitializer.InitializeLicense()
                Me.doExecute()
                License.LicenseInitializer.ReleaseLicense()
            Catch ex As DataException
                Logger.Logger.Warn(ex)
            Catch ex As COMException
                If ex.Message.ToUpper().Contains(LICENSE_EXCEPTION_MESSAGE) Then Throw ex
                Logger.Logger.Error(ex)
            End Try
        End Sub

        ''' <summary>
        ''' En este método se debe especificar el flujo de ejecución del servicio
        ''' </summary>
        ''' <remarks>
        ''' Como buena práctica se considera correcto el realizar unicamente delegaciones
        ''' o llamadas a otros métodos desde este. Evitar los condicionales o bucles en este método
        ''' permite una mayor limpieza en el código y mejora notablemente la mantenibilidad
        ''' </remarks>
        Protected MustOverride Sub doExecute()
    End Class
End Namespace