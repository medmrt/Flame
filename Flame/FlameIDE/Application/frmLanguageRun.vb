Imports System.CodeDom
Imports System.IO
Imports System.Text
Imports FlameLib
Imports Irony.Parsing
Imports ScintillaNET
Imports Scriban
Imports Scriban.Runtime

Public Class frmLanguageRun

    Private Prg As Language
    Private OutputFolder As String
    Private AutoCompleteList As String = ""
    Private ErroLineStyleMarkIndex As Integer = 10
    Private ErroMarkSymbolIndex As Integer = 11
    Private Ext As String = $"{FlameLib.FlameLang.CodeFileExtension}"
    Overloads Function ShowDialog(P As Language, Code As String, _OutputFolder As String, Keywords As String) As DialogResult


        Prg = P
        txtCode.Text = Code
        OutputFolder = _OutputFolder
        AutoCompleteList = Keywords
        AboutToolStripMenuItem.Text = $"About {P.Name}.."
        XToolStripMenuItem.Text = $"{P.Name} Language Reference"


        Return Me.ShowDialog()
    End Function



    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Parse()
    End Sub

    Sub Parse()
        txtOuput.Text = ""
        txtCode.Markers.DeleteAll()
        Dim L As New LanguageCompiler(Prg)
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
            'txtCode.Lines(Err.First.Location.Line).Goto()





        Else
            Dim tree As ParseTree = Result
            Dim Builder As New StringBuilder

            txtOuput.ForeColor = Color.Green
            txtOuput.Text = Builder.ToString
            For Each Comp In Prg.Compilers.OfType(Of Compile)
                MsgBox(Comp.Name)
            Next
        End If

    End Sub

    Private Sub frmLanguageRun_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try
            Dim sourceFiles() As String = Directory.GetFiles(OutputFolder, $"*.{Prg.Name}.{FlameLib.FlameLang.CodeFileExtension}")
            If sourceFiles.Length > 0 Then

                OpenFile(sourceFiles.First)


            End If






        Catch ex As Exception

        End Try



        Try

            txtCode.Folding.IsEnabled = True

            '// Key Step Ensure the marker draws the full line background, even in virtual space.
            '// The MarkerDefine method automatically sets MarkerFlags.FullLine on Background markers, 
            '// but it's good practice to ensure this is set on the Scintilla control itself.
            ' Scintilla.MarkerSetFlags(Scintilla.MarkerGetFlags() | (1 << ERROR_MARKER_INDEX));
            txtCode.Markers(ErroMarkSymbolIndex).Symbol = MarkerSymbol.Arrow ' MarkerSetFlags(Scintilla.MarkerGetFlags() Or (1 << ERROR_MARKER_INDEX))


            '// 1. Set the marker symbol to Background
            txtCode.Markers(ErroLineStyleMarkIndex).Symbol = MarkerSymbol.Background ';
            txtCode.Markers(ErroLineStyleMarkIndex).BackColor = Color.Red
            txtCode.Markers(ErroLineStyleMarkIndex).ForeColor = Color.White
            txtCode.Markers(ErroLineStyleMarkIndex).Alpha = 100
        Catch ex As Exception

        End Try




    End Sub


    Sub OpenFile(Optional FilePath As String = "")
        If FilePath <> "" Then
            txtCode.Text = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8)
            txtCode.Tag = FilePath
            txtPath.Text = FilePath
        Else
            Dim opnFile As New OpenFileDialog
            Dim FileExt As String = $"{Prg.Name}.{Ext}"
            opnFile.Filter = $"Flame Files|*.{FileExt};"
            If opnFile.ShowDialog = DialogResult.OK Then

                txtCode.Text = My.Computer.FileSystem.ReadAllText(opnFile.FileName, System.Text.Encoding.UTF8)
                txtCode.Tag = opnFile.FileName
                txtPath.Text = opnFile.FileName
            End If
        End If

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click



        OpenFile()
    End Sub


    Sub Compile()

        txtOuput.Text = ""
        progressCodeSaving.Value = 0
        txtSavingProgress.Text = ""
        txtCode.Markers.DeleteAll()
        lvwOutputFiles.Items.Clear()
        Dim lngCompiller As New LanguageCompiler(Prg)
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


            Dim OutpusFiles = lngCompiller.Compile(Prg, Result)


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

    Private Sub CompileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileToolStripMenuItem.Click
        Compile()
    End Sub

    Private Sub lvwOutputFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwOutputFiles.SelectedIndexChanged

    End Sub

    Private Sub lvwOutputFiles_DoubleClick(sender As Object, e As EventArgs) Handles lvwOutputFiles.DoubleClick
        If lvwOutputFiles.SelectedItems.Count > 0 Then
            Dim SelectedFilePath As String = lvwOutputFiles.SelectedItems(0).Tag
            If IO.File.Exists(SelectedFilePath) Then
                Process.Start(SelectedFilePath)
            End If
        End If
    End Sub

    Private Sub XToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XToolStripMenuItem.Click
        'LanguageCompiler
        Dim lng As Language = Prg

        If lng IsNot Nothing Then
            Dim Comp As New LanguageCompiler(lng)
            Dim HtmlHelpFilePath As String = $"{Application.StartupPath}\index.html"
            My.Computer.FileSystem.WriteAllText(HtmlHelpFilePath, Comp.CreateHTMLHelpFile, False)
            Process.Start(HtmlHelpFilePath)

        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim F As New frmAboutLanguage
        F.ShowDialog(Prg)
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

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        newFile()
    End Sub

    Sub newFile()
        txtCode.Tag = Nothing
        txtCode.Text = ""
        txtPath.Text = ""
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Sub SaveFile()
        If txtCode.Tag Is Nothing Then
            Dim Sv As New SaveFileDialog
            Dim FileExt As String = $"{Prg.Name}.{Ext}"
            Sv.Filter = $"Flame Files|*.{FileExt};"
            If Sv.ShowDialog = DialogResult.OK Then
                Dim FileName As String = If(Sv.FileName.EndsWith($".{FileExt}"), Sv.FileName, $"{Sv.FileName}.{FileExt}")
                My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
                txtCode.Tag = FileName
                txtPath.Text = FileName
            End If
        Else
            My.Computer.FileSystem.WriteAllText(txtCode.Tag, txtCode.Text, False, System.Text.Encoding.UTF8)
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveAs()
    End Sub

    Sub SaveAs()
        Dim Sv As New SaveFileDialog
        Dim FileExt As String = $"{Prg.Name}.{Ext}"
        Sv.Filter = $"Flame Files|*.{FileExt};"
        If Sv.ShowDialog = DialogResult.OK Then
            Dim FileName As String = If(Sv.FileName.EndsWith($".{FileExt}"), Sv.FileName, $"{Sv.FileName}.{FileExt}")

            My.Computer.FileSystem.WriteAllText(FileName, txtCode.Text, False, System.Text.Encoding.UTF8)
            txtCode.Tag = FileName
            txtPath.Text = FileName
        End If
    End Sub
End Class