
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.Text
Imports FlameLib
Imports Irony.Parsing

Module Module1

    Sub Main()
        Dim arguments As String() = Environment.GetCommandLineArgs()

        If arguments.Count = 2 Then
            Dim PassedFileName As String = arguments.Last
            If IO.File.Exists(PassedFileName) Then

                Dim Result = Compile(PassedFileName)

                Console.WriteLine(Result)


                Exit Sub
            End If
        Else
            Console.WriteLine("Pass File Name")
        End If



    End Sub


    Function OpenFile(FilePath As String) As String




        Return My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)



    End Function

    Function Compile(SourceCodeFilePath As String) As String

        Dim outputPath = SourceCodeFilePath.Split("\").ToList
        Dim FileName As String = outputPath.Last
        outputPath.RemoveAt(outputPath.Count - 1)
        Dim OutputFolder As String = Join(outputPath.ToArray, "\")

        Dim languageName = Function()
                               Dim parts = FileName.Split(".").ToList
                               parts.RemoveAt(parts.Count - 1)

                               Return parts.Last
                           End Function

        Dim FlameSrcPath As String = ""
        Dim files As ReadOnlyCollection(Of String)
        Try

            files = My.Computer.FileSystem.GetFiles($"{OutputFolder}", FileIO.SearchOption.SearchTopLevelOnly, $"*.{FlameLib.FlameLang.Extension}")

            For Each it In files

                Dim LngFilePath = it.Split(".").ToList
                LngFilePath.RemoveAt(LngFilePath.Count - 1)
                Dim LanguageFileName = LngFilePath.Last


                ' Return $"Error: No Language File Founded! {it.ToLower} = " & $"{languageName().ToLower}.{FlameLib.FlameLang.Extension}"
                If it.ToLower().EndsWith($"{languageName().ToLower}.{FlameLib.FlameLang.Extension}") Then
                    FlameSrcPath = it

                    Exit Try
                End If
            Next

            Return "Error: No Language File Founded!"

        Catch ex As Exception
            Return $"Error:{ex.Message}"
        End Try




        Dim LanguageSourceCode As String = My.Computer.FileSystem.ReadAllText(FlameSrcPath, System.Text.Encoding.UTF8)
        Dim SourceCode As String = My.Computer.FileSystem.ReadAllText(SourceCodeFilePath, System.Text.Encoding.UTF8)
        Dim G As New FlameLang
        Dim P = G.Parse(LanguageSourceCode)
        If TypeOf P Is Language Then
            Dim lng As Language = P

            Dim lngCompiller As New LanguageCompiler(lng)
            Dim Result = lngCompiller.Parce(SourceCode)




            If TypeOf Result Is List(Of ErrorInformation) Then

                Dim Err As List(Of ErrorInformation) = Result
                Dim Tokens As New List(Of String)
                For Each it In Err
                    Tokens.Add(it.Description)
                Next
                Return $"Expected Tokens  ({Join(Tokens.ToArray, " | ")}) {vbNewLine } Line :{Err.First.Location.Line}"

            Else



                Dim OutpusFiles = lngCompiller.Compile(lng, Result)
                Dim OutputEncoding As Encoding = New UTF8Encoding(False)
                For Each it In OutpusFiles
                    My.Computer.FileSystem.WriteAllText($"{OutputFolder}\{it.FileName}", it.Content, False, OutputEncoding)
                Next
                ' Console.WriteLine($"Succuss! (No Error)")
                Return $"Succuss! (No Error)"

                'Dim i As Integer = 1
                '
                ' Dim OutputLines As Integer = 0
                'Dim InputLines As Integer = StringUtilities.CountTextLines(txtCode.Text)
                'For Each it In OutpusFiles
                '    Dim Itm As New ListViewItem($"{i.ToString} - {it.FileName}", 0)
                '    OutputLines += StringUtilities.CountTextLines(it.Content)
                '    Itm.SubItems.Add(StringUtilities.CountTextLines(it.Content))
                '    Itm.SubItems.Add($"{Now.ToString("yyyy-MM-dd HH:mm:ss")}")
                '    Itm.Tag = $"{OutputFolder}\{it.FileName}"

                '    i += 1
                '    My.Computer.FileSystem.WriteAllText($"{OutputFolder}\{it.FileName}", it.Content, False, OutputEncoding)
                'Next




            End If

        End If





        Return ""



    End Function


End Module
