Public Class OutputFile
    Public Sub New(xFileName As String, xContent As String)
        Me.FileName = xFileName
        Me.Content = xContent
    End Sub

    Sub New()

    End Sub

    Property FileName As String = ""
    Property Content As String = ""

End Class
