<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.txtFilePath = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPos = New System.Windows.Forms.ToolStripStatusLabel()
        Me.progressCodeSaving = New System.Windows.Forms.ToolStripProgressBar()
        Me.txtSavingProgress = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtCode = New ScintillaNET.Scintilla()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtOuput = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lvwOutputFiles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertFileIn64BaseStringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LanguagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewSparkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageSparksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHelpFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutSparkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtFilePath, Me.lblPos, Me.progressCodeSaving, Me.txtSavingProgress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 543)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(921, 25)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'txtFilePath
        '
        Me.txtFilePath.Margin = New System.Windows.Forms.Padding(5)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(569, 15)
        Me.txtFilePath.Spring = True
        Me.txtFilePath.Text = "(NEW)"
        Me.txtFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPos
        '
        Me.lblPos.AutoSize = False
        Me.lblPos.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblPos.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblPos.Name = "lblPos"
        Me.lblPos.Padding = New System.Windows.Forms.Padding(5)
        Me.lblPos.Size = New System.Drawing.Size(200, 20)
        '
        'progressCodeSaving
        '
        Me.progressCodeSaving.Name = "progressCodeSaving"
        Me.progressCodeSaving.Size = New System.Drawing.Size(100, 19)
        Me.progressCodeSaving.Step = 1
        '
        'txtSavingProgress
        '
        Me.txtSavingProgress.Name = "txtSavingProgress"
        Me.txtSavingProgress.Size = New System.Drawing.Size(23, 20)
        Me.txtSavingProgress.Text = "0%"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 519)
        Me.SplitContainer1.SplitterDistance = 346
        Me.SplitContainer1.SplitterWidth = 8
        Me.SplitContainer1.TabIndex = 14
        '
        'txtCode
        '
        Me.txtCode.Caret.CurrentLineBackgroundColor = System.Drawing.Color.Cornsilk
        Me.txtCode.Caret.HighlightCurrentLine = True
        Me.txtCode.ConfigurationManager.CustomLocation = "test.xml"
        Me.txtCode.ConfigurationManager.IsBuiltInEnabled = False
        Me.txtCode.ConfigurationManager.Language = "test"
        Me.txtCode.ConfigurationManager.LoadOrder = ScintillaNET.Configuration.ConfigurationLoadOrder.UserCustomBuiltIn
        Me.txtCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCode.Folding.IsEnabled = False
        Me.txtCode.Folding.MarkerScheme = ScintillaNET.FoldMarkerScheme.Custom
        Me.txtCode.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtCode.Indentation.ShowGuides = True
        Me.txtCode.Indentation.SmartIndentType = ScintillaNET.SmartIndent.Simple
        Me.txtCode.LineWrapping.Mode = ScintillaNET.LineWrappingMode.Word
        Me.txtCode.Location = New System.Drawing.Point(0, 0)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCode.Margins.Margin0.Width = 36
        Me.txtCode.Margins.Margin2.IsClickable = False
        Me.txtCode.Margins.Margin2.IsFoldMargin = False
        Me.txtCode.Markers.Folder.Alpha = 100
        Me.txtCode.Markers.Folder.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Markers.Folder.ForeColor = System.Drawing.Color.Red
        Me.txtCode.Markers.Folder.Number = 30
        Me.txtCode.Markers.Folder.Symbol = ScintillaNET.MarkerSymbol.Minus
        Me.txtCode.Markers.FolderEnd.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderEnd.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderEnd.Number = 25
        Me.txtCode.Markers.FolderEnd.Symbol = ScintillaNET.MarkerSymbol.BoxPlusConnected
        Me.txtCode.Markers.FolderOpen.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderOpen.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderOpen.Number = 31
        Me.txtCode.Markers.FolderOpen.Symbol = ScintillaNET.MarkerSymbol.BoxMinus
        Me.txtCode.Markers.FolderOpenMid.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderOpenMid.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderOpenMid.Number = 26
        Me.txtCode.Markers.FolderOpenMid.Symbol = ScintillaNET.MarkerSymbol.BoxMinusConnected
        Me.txtCode.Markers.FolderOpenMidTail.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderOpenMidTail.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderOpenMidTail.Number = 27
        Me.txtCode.Markers.FolderOpenMidTail.Symbol = ScintillaNET.MarkerSymbol.TCorner
        Me.txtCode.Markers.FolderSub.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderSub.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderSub.Number = 29
        Me.txtCode.Markers.FolderSub.Symbol = ScintillaNET.MarkerSymbol.VLine
        Me.txtCode.Markers.FolderTail.BackColor = System.Drawing.Color.Gray
        Me.txtCode.Markers.FolderTail.ForeColor = System.Drawing.Color.White
        Me.txtCode.Markers.FolderTail.Number = 28
        Me.txtCode.Markers.FolderTail.Symbol = ScintillaNET.MarkerSymbol.LCorner
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(921, 346)
        Me.txtCode.Styles.Bits = 5
        Me.txtCode.Styles.BraceBad.Size = 10.0!
        Me.txtCode.Styles.BraceLight.Size = 10.0!
        Me.txtCode.Styles.ControlChar.Size = 10.0!
        Me.txtCode.Styles.Default.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.Styles.Default.Size = 10.0!
        Me.txtCode.Styles.IndentGuide.IsVisible = False
        Me.txtCode.Styles.IndentGuide.Size = 10.0!
        Me.txtCode.Styles.LastPredefined.Size = 10.0!
        Me.txtCode.Styles.LineNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCode.Styles.LineNumber.ForeColor = System.Drawing.Color.Black
        Me.txtCode.Styles.LineNumber.Size = 10.0!
        Me.txtCode.Styles.Max.Size = 10.0!
        Me.txtCode.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(918, 111)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtOuput)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(910, 85)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Console"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtOuput
        '
        Me.txtOuput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOuput.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtOuput.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtOuput.Location = New System.Drawing.Point(0, 0)
        Me.txtOuput.Multiline = True
        Me.txtOuput.Name = "txtOuput"
        Me.txtOuput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOuput.Size = New System.Drawing.Size(907, 185)
        Me.txtOuput.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lvwOutputFiles)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(910, 85)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Output"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lvwOutputFiles
        '
        Me.lvwOutputFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvwOutputFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwOutputFiles.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lvwOutputFiles.FullRowSelect = True
        Me.lvwOutputFiles.HideSelection = False
        Me.lvwOutputFiles.Location = New System.Drawing.Point(3, 3)
        Me.lvwOutputFiles.Name = "lvwOutputFiles"
        Me.lvwOutputFiles.Size = New System.Drawing.Size(904, 79)
        Me.lvwOutputFiles.TabIndex = 0
        Me.lvwOutputFiles.UseCompatibleStateImageBehavior = False
        Me.lvwOutputFiles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "File Name"
        Me.ColumnHeader1.Width = 474
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Lines Of Code"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 182
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Generation Date/Time"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 300
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.EditToolStripMenuItem, Me.LanguagesToolStripMenuItem, Me.RunToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(921, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem, Me.RestartToolStripMenuItem})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(183, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.RestartToolStripMenuItem.Text = "Restart"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertFileIn64BaseStringToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'InsertFileIn64BaseStringToolStripMenuItem
        '
        Me.InsertFileIn64BaseStringToolStripMenuItem.Name = "InsertFileIn64BaseStringToolStripMenuItem"
        Me.InsertFileIn64BaseStringToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.InsertFileIn64BaseStringToolStripMenuItem.Text = "Insert File In 64 Base String"
        '
        'LanguagesToolStripMenuItem
        '
        Me.LanguagesToolStripMenuItem.Name = "LanguagesToolStripMenuItem"
        Me.LanguagesToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.LanguagesToolStripMenuItem.Text = "Languages"
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompileToolStripMenuItem, Me.PToolStripMenuItem})
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.RunToolStripMenuItem.Text = "Compiler"
        '
        'CompileToolStripMenuItem
        '
        Me.CompileToolStripMenuItem.Name = "CompileToolStripMenuItem"
        Me.CompileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.CompileToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.CompileToolStripMenuItem.Text = "Compile"
        '
        'PToolStripMenuItem
        '
        Me.PToolStripMenuItem.Name = "PToolStripMenuItem"
        Me.PToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.PToolStripMenuItem.Text = "Parse"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewSparkToolStripMenuItem, Me.ManageSparksToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'AddNewSparkToolStripMenuItem
        '
        Me.AddNewSparkToolStripMenuItem.Name = "AddNewSparkToolStripMenuItem"
        Me.AddNewSparkToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.AddNewSparkToolStripMenuItem.Text = "Install A New Language"
        '
        'ManageSparksToolStripMenuItem
        '
        Me.ManageSparksToolStripMenuItem.Name = "ManageSparksToolStripMenuItem"
        Me.ManageSparksToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ManageSparksToolStripMenuItem.Text = "Languages Manager"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHelpFileToolStripMenuItem, Me.AboutSparkToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ShowHelpFileToolStripMenuItem
        '
        Me.ShowHelpFileToolStripMenuItem.Name = "ShowHelpFileToolStripMenuItem"
        Me.ShowHelpFileToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ShowHelpFileToolStripMenuItem.Text = "Language Reference"
        '
        'AboutSparkToolStripMenuItem
        '
        Me.AboutSparkToolStripMenuItem.Name = "AboutSparkToolStripMenuItem"
        Me.AboutSparkToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.AboutSparkToolStripMenuItem.Text = "About Flame.."
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 568)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Flame"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents txtFilePath As ToolStripStatusLabel
    Friend WithEvents lblPos As ToolStripStatusLabel
    Friend WithEvents progressCodeSaving As ToolStripProgressBar
    Friend WithEvents txtSavingProgress As ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtCode As ScintillaNET.Scintilla
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents txtOuput As TextBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents lvwOutputFiles As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents mnuFile As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InsertFileIn64BaseStringToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LanguagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CompileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddNewSparkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ManageSparksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowHelpFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutSparkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PToolStripMenuItem As ToolStripMenuItem
End Class
