Imports System.IO

Class FileEventLogger
    Public Shared Sub Warn(ByVal message As Object)
        WriteFile(message, ErrorLevel.Warn)
    End Sub

    Public Shared Sub [Error](ByVal message As Object)
        WriteFile(message, ErrorLevel.Error)
    End Sub

    Public Shared Sub Info(ByVal message As Object)
        WriteFile(message, ErrorLevel.Info)
    End Sub

    Protected Shared Sub WriteFile(ByVal message As Object, ByVal err As ErrorLevel)
        Dim sw As StreamWriter
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory & "\" & XML.XMLLoggerGetter.GetName() & ".log"

        If File.Exists(path) Then
            sw = New StreamWriter(path, True)
        Else
            sw = New StreamWriter(path)
        End If

        sw.WriteLine(GetLoggingMessage(message, err))
        sw.Close()
    End Sub

    Protected Enum ErrorLevel
        Info
        Warn
        [Error]
    End Enum

    Protected Shared Function GetErrorLevel(ByVal err As ErrorLevel) As String
        Select Case err
            Case ErrorLevel.Info
                Return "INFO"
            Case ErrorLevel.Warn
                Return "WARNING"
            Case ErrorLevel.Error
                Return "ERROR"
        End Select
        Throw New DataException("No se envío un valor válido como nivel de error")
    End Function

    Protected Shared Function GetTimeStamp() As String
        Return "[" & Now.ToShortDateString() & " " & Now.ToShortTimeString & "] "
    End Function

    Protected Shared Function GetLoggingMessage(ByVal message As Object, ByVal err As ErrorLevel) As String
        Return GetTimeStamp() & GetErrorLevel(err) & ": " & Convert.ToString(message)
    End Function
End Class
