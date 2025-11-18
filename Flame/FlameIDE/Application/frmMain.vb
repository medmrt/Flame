Imports System.Security.Cryptography
Imports FlameLib
Imports ScintillaNET

Public Class frmMain

    Sub newFile()
        txtCode.Tag = Nothing
        txtCode.Text = ""
        txtPath.Text = ""
    End Sub

    Sub OpenFile(Optional FilePath As String = "")
        If FilePath <> "" Then
            txtCode.Text = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)
            txtCode.Tag = FilePath
            txtPath.Text = FilePath
            UpdateRecentFiles(FilePath)
        Else
            Dim opnFile As New OpenFileDialog
            opnFile.Filter = $"Flame Files|*.{FlameLib.FlameLang.Extension};"
            If opnFile.ShowDialog = DialogResult.OK Then
                txtCode.Text = My.Computer.FileSystem.ReadAllText(opnFile.FileName, System.Text.Encoding.UTF8)
                txtCode.Tag = opnFile.FileName
                txtPath.Text = opnFile.FileName
                UpdateRecentFiles(opnFile.FileName)
            End If
        End If

    End Sub
    Sub SaveFile()
        If txtCode.Tag Is Nothing Then
            Dim Sv As New SaveFileDialog
            Sv.Filter = $"Flame Files|*.{FlameLib.FlameLang.Extension};"
            If Sv.ShowDialog = DialogResult.OK Then
                Dim FileName As String = If(Sv.FileName.EndsWith($".{FlameLib.FlameLang.Extension}"), Sv.FileName, $"{Sv.FileName}.{FlameLib.FlameLang.Extension}")
                My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
                txtCode.Tag = FileName
                txtPath.Text = FileName
            End If
        Else
            My.Computer.FileSystem.WriteAllText(txtCode.Tag, txtCode.Text, False, System.Text.Encoding.UTF8)
        End If
    End Sub
    Sub SaveAs()
        Dim Sv As New SaveFileDialog
        Sv.Filter = $"Flame Files|*.{FlameLib.FlameLang.Extension};"
        If Sv.ShowDialog = DialogResult.OK Then
            Dim FileName As String = If(Sv.FileName.EndsWith($".{FlameLib.FlameLang.Extension}"), Sv.FileName, $"{Sv.FileName}.{FlameLib.FlameLang.Extension}")

            My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
            txtCode.Tag = FileName
            txtPath.Text = FileName
        End If
    End Sub


    Function QuoteIt(xText As String) As String
        xText = Replace(xText, """", "\""")
        Return $"""{Join(Split(xText, vbNewLine), $"""{vbNewLine}""")}"""
    End Function

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        newFile()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFile()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveAs()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub PasteAndQuoteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteAndQuoteToolStripMenuItem.Click
        txtCode.Selection.Text = QuoteIt(My.Computer.Clipboard.GetText())
    End Sub
    Sub CommentSelection()
        Dim OrginalSelection = txtCode.Selection.Text
        Dim NewSelection As New List(Of String)


        For Each ln In OrginalSelection.Split(vbCrLf)
            ln = ln.Replace(vbCr, "").Replace(vbLf, "")
            Dim Diff = ln.Length - ln.TrimStart.Length

            NewSelection.Add($"{If(Diff > 0, ln.Substring(0, Diff), "")}//{ln.Trim}")
        Next
        txtCode.Selection.Text = Join(NewSelection.ToArray, vbNewLine)
    End Sub
    Private Sub CommentSelectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommentSelectionToolStripMenuItem.Click
        CommentSelection()
    End Sub


    Sub UnCommentSelection()
        Dim OrginalSelection = txtCode.Selection.Text
        Dim NewSelection As New List(Of String)
        For Each ln In OrginalSelection.Split(vbCrLf)
            ln = ln.Replace(vbCr, "").Replace(vbLf, "")
            If ln.TrimStart.StartsWith("//") Then
                NewSelection.Add(Replace(ln, "//", "",, 1))
            Else
                NewSelection.Add(ln)
            End If
        Next
        txtCode.Selection.Text = Join(NewSelection.ToArray, vbNewLine)
    End Sub


    Private Sub UncommentSelectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UncommentSelectionToolStripMenuItem.Click
        UnCommentSelection()
    End Sub

    Function Check(SourceCode As String, ByRef SyntaxErrors As List(Of ErrorInformation), ByRef SemanticErrors As List(Of ErrorInformation)) As Boolean
        Dim FlamGrammer As New FlameLang

        Dim P = FlamGrammer.Parse(SourceCode)
        If TypeOf P Is Language Then
            Return True

        End If

        SyntaxErrors = FlamGrammer.SyntaxErrors
        SemanticErrors = FlamGrammer.SemanticErrors
        Return False

    End Function


    Function Saved() As Boolean

        If txtCode.Tag Is Nothing Then
            SaveFile()

            If txtCode.Tag Is Nothing Then
                Return False
            Else
                Return True
            End If
        End If


        If FlameLib.Utilities.IsFileChanged(txtCode.Tag, txtCode.Text) Then
            SaveFile()
            If FlameLib.Utilities.IsFileChanged(txtCode.Tag, txtCode.Text) Then
                Return False
            Else
                Return True
            End If
        End If

        Return True
    End Function


    Sub Parse()
        txtCode.Markers.DeleteAll()
        If Not Saved() Then
            MsgBox("Save File First")
            Exit Sub
        End If



        Dim SynError As List(Of ErrorInformation)
        Dim SemanticError As List(Of ErrorInformation)
        If Not FlameLib.Utilities.ParseFile(txtCode.Tag, SynError, SemanticError) Then
            txtCode.Selection.Start = 0
            txtCode.Selection.Length = 0
            If SynError.Count > 0 Then
                MsgBox(SynError.First.Description)
                txtCode.Lines(SynError.First.Location.Line).AddMarker(ErroMarkSymbolIndex)
                txtCode.Lines(SynError.First.Location.Line).AddMarker(ErroLineStyleMarkIndex)

                txtCode.Lines(SynError.First.Location.Line).Goto()
            Else
                MsgBox(SemanticError.First.Description)
                'txtCode.Selection.Start = SemanticError.First.Location.Position
                txtCode.Lines(SemanticError.First.Location.Line).AddMarker(ErroMarkSymbolIndex)
                txtCode.Lines(SemanticError.First.Location.Line).AddMarker(ErroLineStyleMarkIndex)
                txtCode.Lines(SemanticError.First.Location.Line).Goto()
            End If
            Exit Sub
        End If


        Dim G As New FlameLang
        Dim SourceCode = txtCode.Text
        Dim flameLanguage = G.Parse(SourceCode)
        Dim lng As New LanguageCompiler(flameLanguage)
        Dim templateMSG = lng.ParseTemplate(flameLanguage)
        If templateMSG IsNot Nothing Then
            MsgBox(templateMSG.Message)


            txtCode.Lines(templateMSG.Line).AddMarker(ErroMarkSymbolIndex)
            txtCode.Lines(templateMSG.Line).AddMarker(ErroLineStyleMarkIndex)
            txtCode.Lines(templateMSG.Line).Goto()
            Exit Sub
        End If


        MsgBox("No Error!")
    End Sub

    Function GetNextToken() As List(Of String)
        Dim SynError As List(Of ErrorInformation)
        Dim SemanticError As List(Of ErrorInformation)
        If Not FlameLib.Utilities.Parse(txtCode.Text, SynError, SemanticError) Then
            If SynError.First.Description.Contains(":") Then
                Dim X = Split(SynError.First.Description, ":")(1)

                Dim Parts = Split(X, ",")
                Dim L As New List(Of String)
                For Each it In Parts
                    If it.Trim <> "" Then
                        L.Add($"{it.Trim}")
                    End If
                Next
                Return L

            End If
        End If
        Return Nothing

    End Function


    Private Sub CompileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileToolStripMenuItem.Click
        Parse()
    End Sub


    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Process.Start($"{Application.StartupPath}\Help\FlameLang.chm")
    End Sub

    Private Sub AboutSparkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutSparkToolStripMenuItem.Click
        Dim F As New frmAbout
        F.ShowDialog()
    End Sub

    Private Sub RunToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunToolStripMenuItem.Click
        Run()
    End Sub

    Sub Run()

        txtCode.Markers.DeleteAll()
        If Not Saved() Then
            MsgBox("Save File First")
            Exit Sub
        End If



        Dim Parts = Split(txtCode.Tag, "\").ToList
        Parts.RemoveAt(Parts.Count - 1)
        Dim Folder As String = Join(Parts.ToArray, "\")
        Dim G As New FlameLang

        Dim SourceCode = txtCode.Text
        Dim Code As String = ""


        Dim flameLanguage = G.Parse(SourceCode)
        If TypeOf flameLanguage Is Language Then

            Dim lng As New LanguageCompiler(flameLanguage)

            Dim templateMSG = lng.ParseTemplate(flameLanguage)

            If templateMSG IsNot Nothing Then
                MsgBox(templateMSG.Message)


                txtCode.Lines(templateMSG.Line).AddMarker(ErroMarkSymbolIndex)
                txtCode.Lines(templateMSG.Line).AddMarker(ErroLineStyleMarkIndex)
                txtCode.Lines(templateMSG.Line).Goto()
                Exit Sub
            End If



            Dim Keywords = FlameLib.Utilities.GetKeywords(flameLanguage)


            If Keywords.Count = 0 Then
                Keywords.Add("xyz")
            End If

            Dim updatedTemplate As String = My.Computer.FileSystem.ReadAllText($"{Application.StartupPath}\testTemplate.xml")

            updatedTemplate = Replace(updatedTemplate, "{KEYWORDS}", Join(Keywords.ToArray, " "))
            My.Computer.FileSystem.WriteAllText($"{Application.StartupPath}\test.xml", updatedTemplate, False)
            Dim OutputPath As String = txtCode.Tag.ToString
            OutputPath = OutputPath.Substring(0, OutputPath.LastIndexOf("\") + 1)
            Dim F As New frmLanguageRun
            F.ShowDialog(flameLanguage, Code, OutputPath, Join(Keywords.ToArray, " "))


        Else





            txtCode.Selection.Start = 0
            txtCode.Selection.Length = 0

            If G.SyntaxErrors.Count > 0 Then
                MsgBox(G.SyntaxErrors.First.Description)
                'txtCode.Selection.Start = G.SyntaxErrors.First.Location.Position

                txtCode.Lines(G.SyntaxErrors.First.Location.Line).AddMarker(ErroMarkSymbolIndex)
                txtCode.Lines(G.SyntaxErrors.First.Location.Line).AddMarker(ErroLineStyleMarkIndex)
                txtCode.Lines(G.SyntaxErrors.First.Location.Line).Goto()
            End If

        End If

    End Sub
    ' Constants
    Private Const MAX_RECENT_FILES As Integer = 5

    ''' <summary>
    ''' Adds a new file path to the recent files list, ensuring it's at the top.
    ''' </summary>
    Public Sub UpdateRecentFiles(ByVal filePath As String)
        ' 1. Get the current list (ensure it's initialized)
        Dim recentFiles As System.Collections.Specialized.StringCollection = My.Settings.RecentFiles
        If recentFiles Is Nothing Then
            recentFiles = New System.Collections.Specialized.StringCollection()
        End If

        ' 2. Remove the path if it already exists (to move it to the top)
        If recentFiles.Contains(filePath) Then
            recentFiles.Remove(filePath)
        End If

        ' 3. Insert the new path at the beginning
        recentFiles.Insert(0, filePath)

        ' 4. Trim the list to the maximum size
        While recentFiles.Count > MAX_RECENT_FILES
            recentFiles.RemoveAt(recentFiles.Count - 1)
        End While

        ' 5. Save the updated list to user settings
        My.Settings.RecentFiles = recentFiles
        My.Settings.Save()
    End Sub

    Private ErroLineStyleMarkIndex As Integer = 10
    Private ErroMarkSymbolIndex As Integer = 11

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try
            txtCode.Markers(ErroMarkSymbolIndex).Symbol = MarkerSymbol.Arrow
            txtCode.Markers(ErroLineStyleMarkIndex).Symbol = MarkerSymbol.Background ';
            txtCode.Markers(ErroLineStyleMarkIndex).BackColor = Color.Red
            txtCode.Markers(ErroLineStyleMarkIndex).ForeColor = Color.White
            txtCode.Markers(ErroLineStyleMarkIndex).Alpha = 100
        Catch ex As Exception

        End Try




        Dim arguments As String() = Environment.GetCommandLineArgs()
        If arguments.Count = 2 Then
            Dim PassedFileName As String = arguments.Last
            If IO.File.Exists(PassedFileName) Then
                OpenFile(PassedFileName)
                ShowReCentItems()
                Exit Sub
            End If
        End If


        ShowReCentItems()



    End Sub


    Sub ShowReCentItems()
        Dim recentFiles As System.Collections.Specialized.StringCollection = My.Settings.RecentFiles
        If recentFiles IsNot Nothing Then
            'Remove last items
            Dim LastMenu As New List(Of ToolStripMenuItem)
            For Each it In mnuFile.DropDownItems
                If TypeOf it.Tag Is RecentFile Then
                    LastMenu.Add(it)
                End If

            Next

            For Each it In LastMenu
                mnuFile.DropDownItems.Remove(it)
            Next


            For Each it In recentFiles
                Dim ToolStringMenuItemX As New ToolStripMenuItem(it)
                AddHandler ToolStringMenuItemX.Click, Sub()
                                                          OpenFile(it)
                                                      End Sub
                ToolStringMenuItemX.Tag = New RecentFile
                mnuFile.DropDownItems.Insert(4, ToolStringMenuItemX)
            Next

            If recentFiles.Count > 0 Then
                mnuFile.DropDownItems.Insert(4, New ToolStripSeparator)
            End If
        End If

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

    Private Sub txtCode_SelectionChanged(sender As Object, e As EventArgs) Handles txtCode.SelectionChanged
        Dim currentLine = txtCode.Lines.FromPosition(txtCode.CurrentPos).Number + 1
        Dim currentCol As Integer = txtCode.GetColumn(txtCode.CurrentPos)
        lblPos.Text = $"Line {currentLine}, Column {currentCol}"
    End Sub

    Public Class RecentFile

    End Class

    Private Sub RunIDEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunIDEToolStripMenuItem.Click
        Dim CurrentPath As String = txtPath.Text.Trim
        If CurrentPath <> "" Then
            Dim currentPathFolder As String = CurrentPath.Substring(0, CurrentPath.LastIndexOf("\"))

            Dim FoundedFile As String = ""
            For Each it In IO.Directory.GetFiles(currentPathFolder, $"*.{FlameLib.FlameLang.CodeFileExtension}")
                FoundedFile = it
            Next
            If FoundedFile <> "" Then
                Process.Start(FoundedFile)
            End If
        Else
            MsgBox("No File founded")
        End If

    End Sub
End Class
