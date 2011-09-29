Imports System.Threading
Imports System.Runtime.InteropServices

''' <summary>
''' Template para Servicios de ConsultoresGIS
''' Debe crearse una clase que herede de la misma.
''' Caso contrario... arroja excepción de acceso no autorizado
''' </summary>
''' <remarks>
''' Esta clase no debe ser instanciada directamente
''' Se debe Overridear doWork
''' </remarks>
Public Class ServiceHandler
    Private oTimer As System.Threading.Timer

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Wrapper de todas las acciones del servicio
    ''' Inicia el log, mantiene actualizada la lista de horarios de Config.xml y da inicio a las acciones especificadas en las subclases
    ''' </summary>
    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim oCallback As New TimerCallback(AddressOf Me.start)
        oTimer = New System.Threading.Timer(oCallback, Nothing, 60000, 60000)
    End Sub

    Protected Overrides Sub OnStop()
        Try
            License.LicenseInitializer.ReleaseLicense()
        Catch ex As Exception
            ''Si salta... es porque ya se habia cerrado... quiero EXPLICITAMENTE ignorar esto...
        End Try
    End Sub

    ''' <summary>
    ''' En este método se debe invocar a la instancia adecuada que herede 
    ''' de WorkingComponent para utilizar un GeoService o hacer una llamada
    ''' a la clase que se considere apropiada en otro caso
    ''' </summary>
    ''' <remarks>Este método debe ser Overrideado para poder utilizar el template del Servicio</remarks>
    Protected Overridable Sub doWork()
        Throw New UnauthorizedAccessException("No se puede instanciar directamente esta clase... se debe heredar de ella")
    End Sub

    ''' <summary>
    ''' En este método se debe invocar a la instancia adecuada que herede 
    ''' de Logger para utilizar el logging
    ''' Por defecto utiliza la herramienta de logging provista por GeoServices
    ''' </summary>
    ''' <remarks>Por defecto utiliza la herramienta de logging provista por GeoServices</remarks>
    Protected Overridable Sub InitializeLogger()
        Logger.Logger.initialize()
    End Sub

    Private Sub start()
        Try
            If XML.XMLScheduleGetter.getSchedule().Contains(System.DateTime.Now.ToShortTimeString()) Then
                Me.oTimer.Change(60000, System.Threading.Timeout.Infinite)
                Me.InitializeLogger()
                Me.doWork()
                Logger.Logger.Info("Finished")
                Me.oTimer.Change(60000, 60000)
            End If
        Catch ex As UnauthorizedAccessException
            Logger.Logger.Warn(ex, False)
            Me.Stop()
        Catch ex As COMException
            Logger.Logger.Warn(ex, False)
            Me.Stop()
        Catch ex As Exception
            Logger.Logger.Error(ex)
            Me.Stop()
        End Try
    End Sub
End Class