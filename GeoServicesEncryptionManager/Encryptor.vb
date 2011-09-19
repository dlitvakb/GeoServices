Public Class Encryptor

    Private Sub btEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEncrypt.Click
        tbResult.Text = New GeoServices.Security.Encrypt().Encrypt(tbPass.Text)
    End Sub

    Private Sub tbConfirmar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbConfirmar.TextChanged
        Me.checkConfirm()
    End Sub

    Private Sub tbPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPass.TextChanged
        tbConfirmar.Enabled = tbPass.Text.Length > 0
        Me.checkConfirm()
    End Sub
    Private Sub checkConfirm()
        tbConfirmar.BackColor = IIf(tbConfirmar.Text.Length > 0, IIf(tbPass.Text = tbConfirmar.Text, Color.PaleGreen, Color.PaleVioletRed), Color.White)
        btEncrypt.Enabled = tbPass.Text = tbConfirmar.Text
    End Sub
End Class
