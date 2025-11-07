Public Class Utilities
    Shared Function Parse(SourceCode As String, ByRef SyntaxErrors As List(Of ErrorInformation), ByRef SemanticErrors As List(Of ErrorInformation)) As Boolean
        Dim FlamGrammer As New FlameLang

        Dim P = FlamGrammer.Parse(SourceCode)
        If TypeOf P Is Language Then
            Return True

        End If

        SyntaxErrors = FlamGrammer.SyntaxErrors
        SemanticErrors = FlamGrammer.SemanticErrors
        Return False

    End Function



    Shared Function ParseFile(FilePath As String, ByRef SyntaxErrors As List(Of ErrorInformation), ByRef SemanticErrors As List(Of ErrorInformation)) As Boolean


        If Not IO.File.Exists(FilePath) Then

            SyntaxErrors.Add(New ErrorInformation("File Not Found", New Irony.Parsing.SourceLocation(0, 0, 0)))
        End If


        Dim SourceCode As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)

        Dim FlamGrammer As New FlameLang
        Dim P = FlamGrammer.Parse(SourceCode)
        If TypeOf P Is Language Then
            Return True

        End If

        SyntaxErrors = FlamGrammer.SyntaxErrors
        SemanticErrors = FlamGrammer.SemanticErrors
        Return False

    End Function


    Shared Function IsFileChanged(FilePath As String, SourceCode As String)


        If Not IO.File.Exists(FilePath) Then
            Return True
        End If
        Dim FileSourceCode As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)

        Return FileSourceCode <> SourceCode


    End Function


    Shared Function GetKeywords(Lang As Language) As List(Of String)
        Dim Keywords As New List(Of String)

        For Each con In Lang.Rules
            For Each it In con.Items
                If TypeOf it Is KeywordItem Then
                    Keywords.Add(DirectCast(it, KeywordItem).KeywordText)
                End If
                If TypeOf it Is FlagItem Then
                    Keywords.Add(DirectCast(it, FlagItem).FlagText)
                End If

                If TypeOf it Is EnumItem Then
                    Keywords.AddRange(DirectCast(it, EnumItem).EnumerationItems)
                End If
            Next
        Next

        Return Keywords
    End Function

End Class
