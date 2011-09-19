Namespace Logger
    ''' <summary>
    ''' Wrapper para logging de servicios con acceso al event viewer y archivos log de texto
    ''' </summary>
    ''' <remarks>Se debe especificar el nombre de la aplicación en el Config.xml</remarks>
    Public Class Logger
        Public Shared Sub initialize()
            Logger.Info("Program Started")
        End Sub

        Shared Sub Warn(ByVal message As Object)
            FileEventLogger.Warn(message)
            ServiceEventWriter.Warn(message)
        End Sub

        Shared Sub [Error](ByVal message As Object)
            FileEventLogger.Error(message)
            ServiceEventWriter.Error(message)
        End Sub

        Public Shared Sub Info(ByVal message As Object)
            FileEventLogger.Info(message)
        End Sub

        Public Shared Sub EventInfo(ByVal message As Object)
            ServiceEventWriter.Info(message)
        End Sub
    End Class
End Namespace
