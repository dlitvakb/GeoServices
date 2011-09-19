Public Class LoggerQueDaSiempreOK
    Inherits GeoServices.Logger.Logger

    Private Shared estado As Boolean = False

    Public Shared Property MensajeOK As Boolean
        Get
            estado = Not estado
            Return Not estado
        End Get
        Set(ByVal value As Boolean)
            estado = value
        End Set
    End Property

    Public Overloads Shared Sub Info(ByVal message As Object)
        MensajeOK = True
    End Sub

    Public Overloads Shared Sub Warn(ByVal message As Object)
        MensajeOK = True
    End Sub

    Public Overloads Shared Sub [Error](ByVal message As Object)
        MensajeOK = True
    End Sub

    Public Overloads Shared Sub EventInfo(ByVal message As Object)
        MensajeOK = True
    End Sub
End Class
