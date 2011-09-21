#Region " Imports "

Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

#End Region

Namespace Security
    Class PrivateEncrypt
        Private ReadOnly Property PrivateKey As String
            Get
                Return "VVRJNWRXTXpWbk5rUnpsNVdsaE9TRk5XVGtaaWJVNTVaVmhDTUdGWE9YVlZNbFo1Wkcxc2FscFJQVDA9"
            End Get
        End Property

        Public Function Encrypt(ByVal input As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(PrivateKey))))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            Return Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        End Function

        Public Function Decrypt(ByVal input As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(PrivateKey))))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(input)
                Return System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace