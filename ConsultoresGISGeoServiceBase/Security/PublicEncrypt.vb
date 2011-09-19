Namespace Security
    Public Class Encrypt
        Public Function Encrypt(ByVal input As String) As String
            Return New PrivateEncrypt().Encrypt(input)
        End Function
    End Class
End Namespace
