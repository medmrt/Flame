<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.txtPath = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPos = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PasteAndQuoteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CommentSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UncommentSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CompileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunIDEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownButton3 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutSparkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtCode = New ScintillaNET.Scintilla()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtPath, Me.lblPos})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 420)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(800, 30)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'txtPath
        '
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Padding = New System.Windows.Forms.Padding(5)
        Me.txtPath.Size = New System.Drawing.Size(583, 25)
        Me.txtPath.Spring = True
        Me.txtPath.Text = "(NEW)"
        Me.txtPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lblPos.Size = New System.Drawing.Size(200, 25)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.ToolStripSeparator1, Me.ToolStripDropDownButton2, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.ToolStripSeparator3, Me.ToolStripDropDownButton3, Me.ToolStripSeparator4})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(800, 25)
        Me.ToolStrip1.TabIndex = 10
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.mnuFile.Image = CType(resources.GetObject("mnuFile.Image"), System.Drawing.Image)
        Me.mnuFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(38, 22)
        Me.mnuFile.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.OpenToolStripMenuItem.Text = "Open.."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As.."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(189, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripDropDownButton2
        '
        Me.ToolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PasteAndQuoteToolStripMenuItem, Me.ToolStripMenuItem2, Me.CommentSelectionToolStripMenuItem, Me.UncommentSelectionToolStripMenuItem})
        Me.ToolStripDropDownButton2.Image = CType(resources.GetObject("ToolStripDropDownButton2.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(40, 22)
        Me.ToolStripDropDownButton2.Text = "Edit"
        '
        'PasteAndQuoteToolStripMenuItem
        '
        Me.PasteAndQuoteToolStripMenuItem.Name = "PasteAndQuoteToolStripMenuItem"
        Me.PasteAndQuoteToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.PasteAndQuoteToolStripMenuItem.Text = "Paste And Quote"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(231, 6)
        '
        'CommentSelectionToolStripMenuItem
        '
        Me.CommentSelectionToolStripMenuItem.Name = "CommentSelectionToolStripMenuItem"
        Me.CommentSelectionToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.CommentSelectionToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.CommentSelectionToolStripMenuItem.Text = "Comment Selection"
        '
        'UncommentSelectionToolStripMenuItem
        '
        Me.UncommentSelectionToolStripMenuItem.Name = "UncommentSelectionToolStripMenuItem"
        Me.UncommentSelectionToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.UncommentSelectionToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.UncommentSelectionToolStripMenuItem.Text = "Uncomment Selection"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompileToolStripMenuItem, Me.RunToolStripMenuItem, Me.RunIDEToolStripMenuItem})
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(72, 22)
        Me.ToolStripLabel1.Text = "Language"
        '
        'CompileToolStripMenuItem
        '
        Me.CompileToolStripMenuItem.Name = "CompileToolStripMenuItem"
        Me.CompileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.CompileToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.CompileToolStripMenuItem.Text = "Parse .."
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.RunToolStripMenuItem.Text = "Run .."
        '
        'RunIDEToolStripMenuItem
        '
        Me.RunIDEToolStripMenuItem.Name = "RunIDEToolStripMenuItem"
        Me.RunIDEToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.RunIDEToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.RunIDEToolStripMenuItem.Text = "Run Using Flame"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripDropDownButton3
        '
        Me.ToolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.AboutSparkToolStripMenuItem})
        Me.ToolStripDropDownButton3.Image = CType(resources.GetObject("ToolStripDropDownButton3.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton3.Name = "ToolStripDropDownButton3"
        Me.ToolStripDropDownButton3.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripDropDownButton3.Text = "Help"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.HelpToolStripMenuItem.Text = "Help File"
        '
        'AboutSparkToolStripMenuItem
        '
        Me.AboutSparkToolStripMenuItem.Name = "AboutSparkToolStripMenuItem"
        Me.AboutSparkToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.AboutSparkToolStripMenuItem.Text = "About Flame .."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'txtCode
        '
        Me.txtCode.Caret.CurrentLineBackgroundColor = System.Drawing.Color.Cornsilk
        Me.txtCode.Caret.HighlightCurrentLine = True
        Me.txtCode.ConfigurationManager.CustomLocation = "FlameLang.xml"
        Me.txtCode.ConfigurationManager.IsBuiltInEnabled = False
        Me.txtCode.ConfigurationManager.Language = "FlameLang"
        Me.txtCode.ConfigurationManager.LoadOrder = ScintillaNET.Configuration.ConfigurationLoadOrder.UserCustomBuiltIn
        Me.txtCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCode.Folding.IsEnabled = False
        Me.txtCode.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtCode.Indentation.ShowGuides = True
        Me.txtCode.Indentation.SmartIndentType = ScintillaNET.SmartIndent.Simple
        Me.txtCode.Location = New System.Drawing.Point(0, 25)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCode.Margins.Margin0.Width = 36
        Me.txtCode.Margins.Margin2.IsClickable = False
        Me.txtCode.Margins.Margin2.IsFoldMargin = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(800, 395)
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
        Me.txtCode.TabIndex = 11
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents txtPath As ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents mnuFile As ToolStripDropDownButton
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripDropDownButton2 As ToolStripDropDownButton
    Friend WithEvents PasteAndQuoteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents CommentSelectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UncommentSelectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As ToolStripDropDownButton
    Friend WithEvents CompileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunIDEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripDropDownButton3 As ToolStripDropDownButton
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutSparkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents txtCode As ScintillaNET.Scintilla
    Friend WithEvents lblPos As ToolStripStatusLabel
End Class
