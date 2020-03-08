<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSjksz
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSjksz))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxUsr = New System.Windows.Forms.TextBox()
        Me.TextBoxKey = New System.Windows.Forms.TextBox()
        Me.ButtonBcsz = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBoxIP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "数据库用户名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "数据库密码"
        '
        'TextBoxUsr
        '
        Me.TextBoxUsr.Location = New System.Drawing.Point(95, 39)
        Me.TextBoxUsr.Name = "TextBoxUsr"
        Me.TextBoxUsr.Size = New System.Drawing.Size(127, 21)
        Me.TextBoxUsr.TabIndex = 2
        '
        'TextBoxKey
        '
        Me.TextBoxKey.Location = New System.Drawing.Point(95, 68)
        Me.TextBoxKey.Name = "TextBoxKey"
        Me.TextBoxKey.Size = New System.Drawing.Size(127, 21)
        Me.TextBoxKey.TabIndex = 3
        Me.TextBoxKey.UseSystemPasswordChar = True
        '
        'ButtonBcsz
        '
        Me.ButtonBcsz.Location = New System.Drawing.Point(14, 124)
        Me.ButtonBcsz.Name = "ButtonBcsz"
        Me.ButtonBcsz.Size = New System.Drawing.Size(208, 23)
        Me.ButtonBcsz.TabIndex = 4
        Me.ButtonBcsz.Text = "保存设置"
        Me.ButtonBcsz.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(14, 95)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(208, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "测试连接"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBoxIP
        '
        Me.TextBoxIP.Location = New System.Drawing.Point(95, 12)
        Me.TextBoxIP.Name = "TextBoxIP"
        Me.TextBoxIP.Size = New System.Drawing.Size(127, 21)
        Me.TextBoxIP.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "数据库IP地址"
        '
        'FormSjksz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 160)
        Me.Controls.Add(Me.TextBoxIP)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ButtonBcsz)
        Me.Controls.Add(Me.TextBoxKey)
        Me.Controls.Add(Me.TextBoxUsr)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSjksz"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "数据库设置"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxUsr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxKey As System.Windows.Forms.TextBox
    Friend WithEvents ButtonBcsz As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBoxIP As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
