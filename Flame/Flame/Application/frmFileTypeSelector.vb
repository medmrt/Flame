Imports FlameLib

Public Class frmFileTypeSelector
    Overloads Function ShowDialog(Languages As List(Of Language)) As DialogResult
        For Each it In Languages
            LvwLanguages.Items.Add(New ListViewItem(it.Name) With {.Tag = it, .ImageIndex = 0, .StateImageIndex = 0})
        Next

        Return Me.ShowDialog

    End Function

    Private Sub frmFileTypeSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SelectALanguage()
    End Sub
    Public SelectedLagnuage As New Language
    Sub SelectALanguage()
        If LvwLanguages.SelectedItems.Count > 0 Then

            SelectedLagnuage = LvwLanguages.SelectedItems(0).Tag

            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub LvwLanguages_DoubleClick(sender As Object, e As EventArgs) Handles LvwLanguages.DoubleClick
        SelectALanguage()
    End Sub
End Class