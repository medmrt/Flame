Imports System.Collections.ObjectModel
Imports System.Text
Imports FlameLib
Imports Irony.Parsing
Imports ScintillaNET

Public Class frmMain
    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        RestartCurrentFile()
    End Sub

    Sub RestartCurrentFile()
        If txtFilePath.Text.Trim <> "" Then
            Process.Start(txtFilePath.Text)
            End
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFile()
    End Sub



    Sub OpenFile(Optional FilePath As String = "")
        If FilePath <> "" Then
            txtCode.Text = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)
            txtCode.Tag = FilePath
            txtFilePath.Text = FilePath

            AppendTempLanguage()

            ResetCurrentKeywordBasedOnFile()
        Else
            Dim opnFile As New OpenFileDialog
            opnFile.Filter = $"Flames Code Files|*.{FlameLib.FlameLang.CodeFileExtension};"
            If opnFile.ShowDialog = DialogResult.OK Then
                txtCode.Text = My.Computer.FileSystem.ReadAllText(opnFile.FileName, System.Text.Encoding.UTF8)
                txtCode.Tag = opnFile.FileName
                txtFilePath.Text = opnFile.FileName

                AppendTempLanguage()
                ResetCurrentKeywordBasedOnFile()

            End If
        End If

    End Sub
    Dim Compilers As New Hashtable
    Dim Languages As New Hashtable
    Dim LanguageMenu As New List(Of ToolStripMenuItem)
    Sub AppendTempLanguage()

        Dim parts = txtFilePath.Text.Split("\").ToList
        parts.RemoveAt(parts.Count - 1)
        Dim Src = Join(parts.ToArray, "\")

        Dim files As ReadOnlyCollection(Of String)
        Try

            files = My.Computer.FileSystem.GetFiles($"{Src}", FileIO.SearchOption.SearchTopLevelOnly, $"*.{FlameLib.FlameLang.Extension}")
        Catch ex As Exception

        End Try


        For Each f In files
            Dim LanguageSourceCode As String = My.Computer.FileSystem.ReadAllText(f, System.Text.Encoding.UTF8)
            Dim G As New FlameLang
            Dim P = G.Parse(LanguageSourceCode)
            If TypeOf P Is Language Then
                If Not Compilers.ContainsKey(P.Name.ToLower) Then
                    Languages(P.Name.ToLower) = P
                    Compilers(P.Name.ToLower) = New LanguageCompiler(P)
                    Dim mnu As New ToolStripMenuItem(P.Name)
                    mnu.Tag = P
                    mnu.CheckOnClick = True

                    AddHandler mnu.Click, Sub()
                                              For Each it As ToolStripMenuItem In LanguagesToolStripMenuItem.DropDownItems
                                                  it.Checked = False
                                              Next
                                              mnu.Checked = Not mnu.Checked
                                              ResetKeywords()
                                          End Sub
                    LanguagesToolStripMenuItem.DropDownItems.Add(mnu)
                    LanguageMenu.Add(mnu)
                End If
            End If
        Next

        For Each mnu As ToolStripMenuItem In LanguagesToolStripMenuItem.DropDownItems
            If mnu.Tag IsNot Nothing Then
                Dim P As Language = mnu.Tag
                If P.Name.ToLower = GetCurrentLanguageExt.ToLower Then
                    mnu.Checked = True
                Else
                    mnu.Checked = False
                End If
            Else
                mnu.Checked = False
            End If
        Next


    End Sub

    Function GetCurrentLanguageExt() As String
        Dim parts = txtFilePath.Text.Split(".").ToList
        parts.RemoveAt(parts.Count - 1)
        If parts.Count > 0 Then
            Return parts.Last.ToLower
        End If
        Return ""
    End Function

    Sub ResetKeywords()

        For Each mnu As ToolStripMenuItem In LanguagesToolStripMenuItem.DropDownItems
            If mnu.Tag IsNot Nothing And mnu.Checked Then
                Dim P As Language = mnu.Tag
                Dim Keywords As New List(Of String)

                For Each con In P.Rules
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


                If Keywords.Count = 0 Then
                    Keywords.Add(" ")
                End If
                txtCode.Lexing.SetKeywords(0, Join(Keywords.ToArray, " "))
                txtCode.Lexing.SetKeywords(1, Join(Keywords.ToArray, " "))

                Exit Sub
            End If
        Next

        txtCode.Lexing.SetKeywords(0, " ")
        txtCode.Lexing.SetKeywords(1, " ")
    End Sub

    Sub ResetCurrentKeywordBasedOnFile()

        Dim parts = txtFilePath.Text.Split(".").ToList
        parts.RemoveAt(parts.Count - 1)

        If parts.Count > 0 Then
            If Compilers.ContainsKey(parts.Last.ToLower) Then
                Dim Keywords As New List(Of String)
                Dim P As Language = Languages(parts.Last.ToLower)
                For Each con In P.Rules
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


                If Keywords.Count = 0 Then
                    Keywords.Add(" ")
                End If
                txtCode.Lexing.SetKeywords(0, Join(Keywords.ToArray, " "))
                txtCode.Lexing.SetKeywords(1, Join(Keywords.ToArray, " "))
            Else
                txtCode.Lexing.SetKeywords(0, " ")
                txtCode.Lexing.SetKeywords(1, " ")
            End If

        Else
            txtCode.Lexing.SetKeywords(0, " ")
            txtCode.Lexing.SetKeywords(1, " ")
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Sub SaveFile()
        If txtCode.Tag Is Nothing Then
            Dim LanguageExt As String = "*"
            For Each it In LanguageMenu
                If it.Checked Then
                    LanguageExt = $".{it.Text.ToLower}.{FlameLib.FlameLang.CodeFileExtension}"
                    Exit For
                End If
            Next

            If LanguageExt = "*" Then
                LanguageExt = $".{FlameLib.FlameLang.CodeFileExtension}"
            End If

            Dim Sv As New SaveFileDialog
            Sv.Filter = $"Flames Code Files|*.{FlameLib.FlameLang.CodeFileExtension};"

            If Sv.ShowDialog = DialogResult.OK Then
                Dim FileName As String = If(Sv.FileName.EndsWith($"{LanguageExt}"), Sv.FileName, $"{Sv.FileName}{LanguageExt}")
                My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
                txtCode.Tag = FileName
                txtFilePath.Text = FileName
                ResetCurrentKeywordBasedOnFile()
            End If
        Else
            My.Computer.FileSystem.WriteAllText(txtCode.Tag, txtCode.Text, False, System.Text.Encoding.UTF8)
        End If
    End Sub


    Sub SaveAs()
        Dim Sv As New SaveFileDialog



        Sv.Filter = $"Flames Code Files|*.{FlameLib.FlameLang.CodeFileExtension};"
        If Sv.ShowDialog = DialogResult.OK Then
            Dim FileName As String = If(Sv.FileName.EndsWith($".{FlameLib.FlameLang.CodeFileExtension}"), Sv.FileName, $"{Sv.FileName}.{FlameLib.FlameLang.CodeFileExtension}")

            My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
            txtCode.Tag = FileName
            txtFilePath.Text = FileName
            ResetCurrentKeywordBasedOnFile()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveAs()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub ShowHelpFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHelpFileToolStripMenuItem.Click

        Dim lng As Language

        For Each it In LanguageMenu
            If it.Checked Then
                lng = it.Tag
                Exit For
            End If
        Next



        If lng IsNot Nothing Then
            Dim Comp As New LanguageCompiler(lng)
            Dim HtmlHelpFilePath As String = $"{Application.StartupPath}\index.html"
            My.Computer.FileSystem.WriteAllText(HtmlHelpFilePath, Comp.CreateHTMLHelpFile, False)
            Process.Start(HtmlHelpFilePath)

        End If

    End Sub



    Sub Parse()


        Dim lng As Language

        For Each it In LanguageMenu
            If it.Checked Then
                lng = it.Tag
                Exit For
            End If
        Next

        If lng IsNot Nothing Then
            Exit Sub
        End If

        txtOuput.Text = ""
        txtCode.Markers.DeleteAll()
        Dim L As New LanguageCompiler(lng)
        Dim Result = L.Parce(txtCode.Text)

        If TypeOf Result Is List(Of ErrorInformation) Then
            Dim Err As List(Of ErrorInformation) = Result



            Dim Tokens As New List(Of String)



            For Each it In Err

                Tokens.Add(it.Description)

            Next


            txtOuput.Text = $"Expected Tokens  ({Join(Tokens.ToArray, " | ")}) {vbNewLine } Line :{Err.First.Location.Line}"

            txtCode.Lines(Err.First.Location.Line).AddMarker(ErroMarkSymbolIndex)
            txtCode.Lines(Err.First.Location.Line).AddMarker(ErroLineStyleMarkIndex)
            txtCode.Lines(Err.First.Location.Line).Goto()
        Else
            Dim tree As ParseTree = Result
            Dim Builder As New StringBuilder

            txtOuput.ForeColor = Color.Green
            txtOuput.Text = Builder.ToString
            MsgBox("No Error")
        End If

    End Sub

    Private ErroLineStyleMarkIndex As Integer = 10
    Private ErroMarkSymbolIndex As Integer = 11
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtCode.Markers(ErroMarkSymbolIndex).Symbol = MarkerSymbol.Arrow
            txtCode.Markers(ErroLineStyleMarkIndex).Symbol = MarkerSymbol.Background
            txtCode.Markers(ErroLineStyleMarkIndex).BackColor = Color.Red
            txtCode.Markers(ErroLineStyleMarkIndex).ForeColor = Color.White
            txtCode.Markers(ErroLineStyleMarkIndex).Alpha = 100
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PToolStripMenuItem.Click
        Parse()
    End Sub

    Private Sub CompileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileToolStripMenuItem.Click
        Compile()
    End Sub

    Sub Compile()


        Dim lng As Language

        For Each it In LanguageMenu
            If it.Checked Then
                lng = it.Tag
                Exit For
            End If
        Next

        If lng Is Nothing Then
            Exit Sub
        End If

        txtOuput.Text = ""
        progressCodeSaving.Value = 0
        txtSavingProgress.Text = ""
        txtCode.Markers.DeleteAll()
        lvwOutputFiles.Items.Clear()
        Dim lngCompiller As New LanguageCompiler(lng)
        Dim Result = lngCompiller.Parce(txtCode.Text)

        If TypeOf Result Is List(Of ErrorInformation) Then

            Dim Err As List(Of ErrorInformation) = Result
            Dim Tokens As New List(Of String)
            For Each it In Err
                Tokens.Add(it.Description)
            Next
            txtOuput.Text = $"Expected Tokens  ({Join(Tokens.ToArray, " | ")}) {vbNewLine } Line :{Err.First.Location.Line}"
            TabControl1.SelectedTab = TabPage1
            txtCode.Lines(Err.First.Location.Line).AddMarker(ErroMarkSymbolIndex)
            txtCode.Lines(Err.First.Location.Line).AddMarker(ErroLineStyleMarkIndex)
            txtCode.Lines(Err.First.Location.Line).Goto()
        Else

            Dim outputPath = txtFilePath.Text.Split("\").ToList
            outputPath.RemoveAt(outputPath.Count - 1)
            Dim OutputFolder As String = Join(outputPath.ToArray, "\")

            Dim OutpusFiles = lngCompiller.Compile(lng, Result)


            Dim i As Integer = 1
            Dim OutputEncoding As Encoding = New UTF8Encoding(False)
            Dim OutputLines As Integer = 0
            Dim InputLines As Integer = StringUtilities.CountTextLines(txtCode.Text)
            For Each it In OutpusFiles
                Dim Itm As New ListViewItem($"{i.ToString} - {it.FileName}", 0)
                OutputLines += StringUtilities.CountTextLines(it.Content)
                Itm.SubItems.Add(StringUtilities.CountTextLines(it.Content))
                Itm.SubItems.Add($"{Now.ToString("yyyy-MM-dd HH:mm:ss")}")
                Itm.Tag = $"{OutputFolder}\{it.FileName}"
                lvwOutputFiles.Items.Add(Itm)
                i += 1
                My.Computer.FileSystem.WriteAllText($"{OutputFolder}\{it.FileName}", it.Content, False, OutputEncoding)
            Next


            Dim SavingPrc As Integer = StringUtilities.CalcCodeSaving(txtCode.Text, OutputLines)
            progressCodeSaving.Value = SavingPrc
            txtSavingProgress.Text = $"{SavingPrc}%"

            'CalcCodeSaving

            TabControl1.SelectedTab = TabPage2


        End If
    End Sub


    Private Sub lvwOutputFiles_DoubleClick(sender As Object, e As EventArgs) Handles lvwOutputFiles.DoubleClick
        If lvwOutputFiles.SelectedItems.Count > 0 Then
            Dim SelectedFilePath As String = lvwOutputFiles.SelectedItems(0).Tag
            If IO.File.Exists(SelectedFilePath) Then
                Process.Start(SelectedFilePath)
            End If
        End If
    End Sub

    Private Sub AboutSparkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutSparkToolStripMenuItem.Click
        Dim F As New frmAbout
        F.ShowDialog()

    End Sub

    Private Sub txtCode_CharAdded(sender As Object, e As ScintillaNET.CharAddedEventArgs) Handles txtCode.CharAdded


        Select Case e.Ch
            Case "(" : txtCode.InsertText(txtCode.CurrentPos, ")")
            Case "[" : txtCode.InsertText(txtCode.CurrentPos, "]")
            Case """" : txtCode.InsertText(txtCode.CurrentPos, """")

            Case "{"

                If txtCode.GetCurrentLine.Replace("{", "").Trim = "" Then
                    Dim LineLng = txtCode.GetCurrentLine.Length

                    If LineLng > 3 Then
                        txtCode.InsertText(txtCode.CurrentPos, $"{vbNewLine}{New String(vbTab, LineLng - 2)}{vbNewLine}{New String(vbTab, LineLng - 3)}}}")
                        txtCode.CurrentPos += $"{New String(vbTab, LineLng - 2)}{vbNewLine}".Length

                    ElseIf LineLng = 2 Then

                        Dim PaddingA As String = New String(vbTab, LineLng - 2)
                        Dim PaddingB As String = If(LineLng - 3 > 0, New String(vbTab, LineLng - 3), $"{vbTab}")

                        txtCode.InsertText(txtCode.CurrentPos, $"{vbNewLine}{PaddingA}{vbNewLine}{PaddingB}}}")
                        txtCode.CurrentPos += $"{PaddingA}{vbNewLine}".Length
                    ElseIf LineLng = 1 Then
                        Dim PaddingA As String = New String(vbTab, LineLng)
                        Dim PaddingB As String = New String(vbTab, 2)

                        txtCode.InsertText(txtCode.CurrentPos, $"{vbNewLine}{PaddingA}{vbNewLine}{PaddingB}}}")
                        txtCode.CurrentPos += $"{PaddingA}{vbNewLine}".Length
                    Else
                        txtCode.InsertText(txtCode.CurrentPos, $"{vbNewLine}{vbTab}{vbNewLine}}}")
                        txtCode.CurrentPos += 3
                    End If
                End If

        End Select



    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        NewFile()
    End Sub

    Sub NewFile()
        txtCode.Tag = Nothing
        txtCode.Text = ""
        txtFilePath.Text = ""
        txtCode.Lexing.SetKeywords(0, " ")
        txtCode.Lexing.SetKeywords(1, " ")

        Dim Languages As New List(Of Language)

        For Each it In LanguageMenu
            Languages.Add(it.Tag)
        Next

        Dim F As New frmFileTypeSelector
        If F.ShowDialog(Languages) = DialogResult.OK Then

            For Each it In LanguageMenu
                If it.Text = F.SelectedLagnuage.Name Then
                    it.PerformClick()
                    Dim Lng As Language = it.Tag
                    'txtCode.Tag = Lng
                    If Lng.InitialTemplateDef IsNot Nothing Then
                        txtCode.Text = Join(Lng.InitialTemplateDef.Lines.ToArray, vbNewLine)
                    End If
                End If
            Next
        Else
            Close()
            'End?
        End If

    End Sub

    Private Sub txtCode_SelectionChanged(sender As Object, e As EventArgs) Handles txtCode.SelectionChanged
        Dim currentLine = txtCode.Lines.FromPosition(txtCode.CurrentPos).Number + 1
        Dim currentCol As Integer = txtCode.GetColumn(txtCode.CurrentPos)
        lblPos.Text = $"Line {currentLine}, Column {currentCol}"
    End Sub

End Class
