<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAboutLanguage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAboutLanguage))
        Me.lblCreator = New System.Windows.Forms.Label()
        Me.lblVers = New System.Windows.Forms.Label()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblCreator
        '
        Me.lblCreator.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblCreator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCreator.Location = New System.Drawing.Point(10, 113)
        Me.lblCreator.Name = "lblCreator"
        Me.lblCreator.Size = New System.Drawing.Size(445, 24)
        Me.lblCreator.TabIndex = 17
        Me.lblCreator.Text = "medmrt@gmail.com"
        Me.lblCreator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVers
        '
        Me.lblVers.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblVers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblVers.Location = New System.Drawing.Point(10, 81)
        Me.lblVers.Name = "lblVers"
        Me.lblVers.Size = New System.Drawing.Size(445, 24)
        Me.lblVers.TabIndex = 15
        Me.lblVers.Text = "1.0"
        Me.lblVers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLanguage
        '
        Me.lblLanguage.Font = New System.Drawing.Font("Tahoma", 24.0!)
        Me.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblLanguage.Location = New System.Drawing.Point(10, 19)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(445, 54)
        Me.lblLanguage.TabIndex = 14
        Me.lblLanguage.Text = "Language Name"
        Me.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblDate.Location = New System.Drawing.Point(10, 145)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(445, 24)
        Me.lblDate.TabIndex = 18
        Me.lblDate.Text = "medmrt@gmail.com"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmAboutLanguage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 198)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblCreator)
        Me.Controls.Add(Me.lblVers)
        Me.Controls.Add(Me.lblLanguage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAboutLanguage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAboutLanguage"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblCreator As Label
    Friend WithEvents lblVers As Label
    Friend WithEvents lblLanguage As Label
    Friend WithEvents lblDate As Label
End Class
