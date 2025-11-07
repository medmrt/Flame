Public Class StringUtilities
    Public Shared Function CountTextLines(ByVal text As String) As Integer
        ' If the input text is Nothing or empty, return 0 lines immediately.
        If String.IsNullOrEmpty(text) Then
            Return 0
        End If



        ' Define an array of characters that represent common line breaks:
        ' ChrW(10) is Line Feed (LF), common in Unix/Linux/macOS systems.
        ' ChrW(13) is Carriage Return (CR), common in old Mac systems.
        ' The .NET environment handles the combined CR+LF automatically when splitting.
        Dim lineSeparators() As Char = {ChrW(10), ChrW(13)}

        ' Split the string using the line separators.
        ' We use StringSplitOptions.None to ensure that lines which are empty
        ' (e.g., two consecutive line breaks) are counted.
        ' The Length of the resulting array is the total number of lines.
        Dim lines() As String = text.Split(lineSeparators, StringSplitOptions.None)


        Dim L As New List(Of String)


        For Each it In lines
            If it.Trim <> "" Then
                L.Add(it)
            End If
        Next



        Return L.ToArray.Length

    End Function

    Public Shared Function CleanEmptyLines(ByVal text As String) As String
        ' If the input text is Nothing or empty, return 0 lines immediately.
        If String.IsNullOrEmpty(text) Then
            Return 0
        End If

        ' Define an array of characters that represent common line breaks:
        ' ChrW(10) is Line Feed (LF), common in Unix/Linux/macOS systems.
        ' ChrW(13) is Carriage Return (CR), common in old Mac systems.
        ' The .NET environment handles the combined CR+LF automatically when splitting.
        Dim lineSeparators() As Char = {ChrW(10), ChrW(13)}

        ' Split the string using the line separators.
        ' We use StringSplitOptions.None to ensure that lines which are empty
        ' (e.g., two consecutive line breaks) are counted.
        ' The Length of the resulting array is the total number of lines.
        Dim lines() As String = text.Split(lineSeparators, StringSplitOptions.None)


        Dim L As New List(Of String)


        For Each it In lines
            If it.Trim <> "" Then
                L.Add(it)
            End If
        Next



        Return Join(L.ToArray, vbNewLine)
    End Function



    Public Shared Function CalcCodeSaving(ByVal InputSourceCode As String, OutputSourceCodelng As Integer) As Integer
        ' If the input text is Nothing or empty, return 0 lines immediately.
        Dim InputSourceCodeLng = CountTextLines(InputSourceCode)



        If OutputSourceCodelng >= InputSourceCodeLng And InputSourceCodeLng > 0 Then
            If (InputSourceCodeLng / OutputSourceCodelng) * 100 <= 100 Then
                Return Math.Abs(((InputSourceCodeLng / OutputSourceCodelng) * 100) - 100)
            End If

        End If

        Return 0
    End Function




End Class
