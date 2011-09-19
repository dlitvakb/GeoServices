Namespace Logger
    ''' <summary>
    ''' Provee la interfaz para el event viewer
    ''' </summary>
    ''' <remarks>Se debe especificar el nombre de la aplicación en Config.xml</remarks>
    Class ServiceEventWriter
        Private Shared _source As String = XML.XMLLoggerGetter.getName() & "Events"
        Private Shared _log As String = XML.XMLLoggerGetter.getName() & "Log"

        Private Shared Function checkCreate() As EventLog
            If Not EventLog.SourceExists(_source) Then EventLog.CreateEventSource(_source, _log)
            Return New EventLog(_log, System.Environment.MachineName, _source)
        End Function

        Public Shared Sub Warn(ByVal message As Object)
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Warning)
        End Sub

        Public Shared Sub [Error](ByVal message As Object)
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Error)
        End Sub

        Public Shared Sub Info(ByVal message As Object)
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Information)
        End Sub
    End Class
End Namespace