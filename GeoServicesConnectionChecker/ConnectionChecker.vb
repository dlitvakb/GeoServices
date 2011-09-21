Public Class ConnectionChecker

    Private Sub btCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCheck.Click
        Me.ValidateConnection()
    End Sub

    Private Sub ValidateConnection()
        Me.btCheck.Enabled = False
        Try
            GeoServices.License.LicenseInitializer.InitializeLicense()
            Me.setValidationField(Not New GeoServices.SDE.WorkspaceConnection(tbUser.Text, New GeoServices.Security.Encrypt().Encrypt(tbPassword.Text), tbServer.Text, tbInstance.Text, tbDatabase.Text, tbVersion.Text).GetWorkspace() Is Nothing)
        Catch ex As Exception
            Me.setValidationField(False)
        Finally
            GeoServices.License.LicenseInitializer.ReleaseLicense()
        End Try
        Me.btCheck.Enabled = True
    End Sub

    Private Sub setValidationField(ByVal valid As Boolean)
        If valid Then
            Me.tbResult.BackColor = Color.PaleGreen
            Me.tbResult.Text = "OK!"
        Else
            Me.tbResult.BackColor = Color.PaleVioletRed
            Me.tbResult.Text = "Fail =("
        End If
    End Sub

    Private Sub EncryptionManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptionManagerToolStripMenuItem.Click
        Dim encryptor As New GeoServicesEncryptionManager.Encryptor
        encryptor.ShowDialog()
    End Sub

    Private Sub tbPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPassword.TextChanged, tbUser.TextChanged, _
                                                                                                           tbInstance.TextChanged, tbServer.TextChanged
        btCheck.Enabled = tbPassword.Text.Length > 0 AndAlso tbUser.Text.Length > 0 AndAlso tbInstance.Text.Length > 0 AndAlso tbServer.Text.Length > 0
    End Sub
End Class
