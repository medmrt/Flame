Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports FlameLib

Public Class frmAboutLanguage
    Overloads Function ShowDialog(P As Language) As DialogResult


        lblLanguage.Text = P.Name
        lblCreator.Text = P.Creator
        lblDate.Text = P.CreationDate
        lblVers.Text = P.Version.ToString


        Return Me.ShowDialog()
    End Function
End Class