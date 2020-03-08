<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F0Login
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
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.TextBoxUsrName = New System.Windows.Forms.TextBox()
        Me.LabelPassword = New System.Windows.Forms.Label()
        Me.LabelUsrName = New System.Windows.Forms.Label()
        Me.LabelSqlAdress = New System.Windows.Forms.Label()
        Me.TextBoxSqlAdress = New System.Windows.Forms.TextBox()
        Me.PictureBoxSZ = New System.Windows.Forms.PictureBox()
        Me.PictureBoxOben = New System.Windows.Forms.PictureBox()
        Me.PictureBoxLogin = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxSZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxOben, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxPassword.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxPassword.Location = New System.Drawing.Point(142, 171)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(198, 27)
        Me.TextBoxPassword.TabIndex = 26
        Me.TextBoxPassword.UseSystemPasswordChar = True
        '
        'TextBoxUsrName
        '
        Me.TextBoxUsrName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxUsrName.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxUsrName.Location = New System.Drawing.Point(142, 138)
        Me.TextBoxUsrName.Name = "TextBoxUsrName"
        Me.TextBoxUsrName.Size = New System.Drawing.Size(198, 27)
        Me.TextBoxUsrName.TabIndex = 24
        '
        'LabelPassword
        '
        Me.LabelPassword.AutoSize = True
        Me.LabelPassword.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPassword.Location = New System.Drawing.Point(69, 173)
        Me.LabelPassword.Name = "LabelPassword"
        Me.LabelPassword.Size = New System.Drawing.Size(62, 20)
        Me.LabelPassword.TabIndex = 25
        Me.LabelPassword.Text = "密  码："
        '
        'LabelUsrName
        '
        Me.LabelUsrName.AutoSize = True
        Me.LabelUsrName.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelUsrName.Location = New System.Drawing.Point(69, 140)
        Me.LabelUsrName.Name = "LabelUsrName"
        Me.LabelUsrName.Size = New System.Drawing.Size(69, 20)
        Me.LabelUsrName.TabIndex = 22
        Me.LabelUsrName.Text = "用户名："
        '
        'LabelSqlAdress
        '
        Me.LabelSqlAdress.AutoSize = True
        Me.LabelSqlAdress.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSqlAdress.Location = New System.Drawing.Point(69, 107)
        Me.LabelSqlAdress.Name = "LabelSqlAdress"
        Me.LabelSqlAdress.Size = New System.Drawing.Size(67, 20)
        Me.LabelSqlAdress.TabIndex = 21
        Me.LabelSqlAdress.Text = "IP地址："
        '
        'TextBoxSqlAdress
        '
        Me.TextBoxSqlAdress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxSqlAdress.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxSqlAdress.Location = New System.Drawing.Point(142, 105)
        Me.TextBoxSqlAdress.Name = "TextBoxSqlAdress"
        Me.TextBoxSqlAdress.Size = New System.Drawing.Size(198, 27)
        Me.TextBoxSqlAdress.TabIndex = 23
        '
        'PictureBoxSZ
        '
        Me.PictureBoxSZ.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxSZ.Image = Global.WJJX_ERP10.My.Resources.Resources.连接设置
        Me.PictureBoxSZ.Location = New System.Drawing.Point(42, 234)
        Me.PictureBoxSZ.Name = "PictureBoxSZ"
        Me.PictureBoxSZ.Size = New System.Drawing.Size(163, 52)
        Me.PictureBoxSZ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxSZ.TabIndex = 31
        Me.PictureBoxSZ.TabStop = False
        '
        'PictureBoxOben
        '
        Me.PictureBoxOben.Image = Global.WJJX_ERP10.My.Resources.Resources.标题栏lan
        Me.PictureBoxOben.Location = New System.Drawing.Point(-10, -1)
        Me.PictureBoxOben.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxOben.Name = "PictureBoxOben"
        Me.PictureBoxOben.Size = New System.Drawing.Size(1125, 71)
        Me.PictureBoxOben.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxOben.TabIndex = 2
        Me.PictureBoxOben.TabStop = False
        '
        'PictureBoxLogin
        '
        Me.PictureBoxLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxLogin.Image = Global.WJJX_ERP10.My.Resources.Resources.进入系统
        Me.PictureBoxLogin.Location = New System.Drawing.Point(220, 234)
        Me.PictureBoxLogin.Name = "PictureBoxLogin"
        Me.PictureBoxLogin.Size = New System.Drawing.Size(162, 51)
        Me.PictureBoxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxLogin.TabIndex = 32
        Me.PictureBoxLogin.TabStop = False
        '
        'F0Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(427, 297)
        Me.Controls.Add(Me.PictureBoxLogin)
        Me.Controls.Add(Me.PictureBoxSZ)
        Me.Controls.Add(Me.TextBoxPassword)
        Me.Controls.Add(Me.TextBoxUsrName)
        Me.Controls.Add(Me.LabelPassword)
        Me.Controls.Add(Me.LabelUsrName)
        Me.Controls.Add(Me.LabelSqlAdress)
        Me.Controls.Add(Me.TextBoxSqlAdress)
        Me.Controls.Add(Me.PictureBoxOben)
        Me.Name = "F0Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F0Login"
        CType(Me.PictureBoxSZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxOben, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxOben As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUsrName As System.Windows.Forms.TextBox
    Friend WithEvents LabelPassword As System.Windows.Forms.Label
    Friend WithEvents LabelUsrName As System.Windows.Forms.Label
    Friend WithEvents LabelSqlAdress As System.Windows.Forms.Label
    Friend WithEvents TextBoxSqlAdress As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxSZ As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxLogin As System.Windows.Forms.PictureBox
End Class
