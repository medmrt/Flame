Public Class frmAbout
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start($"https://{LinkLabel1.Text}")
    End Sub
End Class