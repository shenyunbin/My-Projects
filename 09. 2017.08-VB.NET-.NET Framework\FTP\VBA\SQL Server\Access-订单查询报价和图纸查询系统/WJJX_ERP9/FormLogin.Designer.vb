<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogin
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogin))
        Me.LabelSqlAdress = New System.Windows.Forms.Label()
        Me.LabelUsrName = New System.Windows.Forms.Label()
        Me.LabelPassword = New System.Windows.Forms.Label()
        Me.TextBoxSqlAdress = New System.Windows.Forms.TextBox()
        Me.TextBoxUsrName = New System.Windows.Forms.TextBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.LabelLogin = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelTo = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.LabelX = New System.Windows.Forms.Label()
        Me.LabelExit = New System.Windows.Forms.Label()
        Me.ButtonBj = New System.Windows.Forms.Button()
        Me.ButtonSc = New System.Windows.Forms.Button()
        Me.ButtonCk = New System.Windows.Forms.Button()
        Me.ButtonJs = New System.Windows.Forms.Button()
        Me.LabelAdmin = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ButtonSh = New System.Windows.Forms.Button()
        Me.LabelSjksz = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelSqlAdress
        '
        Me.LabelSqlAdress.AutoSize = True
        Me.LabelSqlAdress.Location = New System.Drawing.Point(50, 72)
        Me.LabelSqlAdress.Name = "LabelSqlAdress"
        Me.LabelSqlAdress.Size = New System.Drawing.Size(53, 12)
        Me.LabelSqlAdress.TabIndex = 0
        Me.LabelSqlAdress.Text = "IP地址："
        '
        'LabelUsrName
        '
        Me.LabelUsrName.AutoSize = True
        Me.LabelUsrName.Location = New System.Drawing.Point(50, 99)
        Me.LabelUsrName.Name = "LabelUsrName"
        Me.LabelUsrName.Size = New System.Drawing.Size(53, 12)
        Me.LabelUsrName.TabIndex = 1
        Me.LabelUsrName.Text = "用户名："
        '
        'LabelPassword
        '
        Me.LabelPassword.AutoSize = True
        Me.LabelPassword.Location = New System.Drawing.Point(50, 126)
        Me.LabelPassword.Name = "LabelPassword"
        Me.LabelPassword.Size = New System.Drawing.Size(53, 12)
        Me.LabelPassword.TabIndex = 2
        Me.LabelPassword.Text = "密  码："
        '
        'TextBoxSqlAdress
        '
        Me.TextBoxSqlAdress.Location = New System.Drawing.Point(109, 69)
        Me.TextBoxSqlAdress.Name = "TextBoxSqlAdress"
        Me.TextBoxSqlAdress.Size = New System.Drawing.Size(198, 21)
        Me.TextBoxSqlAdress.TabIndex = 1
        '
        'TextBoxUsrName
        '
        Me.TextBoxUsrName.Location = New System.Drawing.Point(109, 96)
        Me.TextBoxUsrName.Name = "TextBoxUsrName"
        Me.TextBoxUsrName.Size = New System.Drawing.Size(198, 21)
        Me.TextBoxUsrName.TabIndex = 2
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(109, 123)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(198, 21)
        Me.TextBoxPassword.TabIndex = 3
        Me.TextBoxPassword.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 12.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(24, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(199, 21)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "德清县下舍五金机械厂ERP"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape2, Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(383, 265)
        Me.ShapeContainer1.TabIndex = 8
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RectangleShape2.BorderColor = System.Drawing.SystemColors.Control
        Me.RectangleShape2.Location = New System.Drawing.Point(-17, -52)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(550, 380)
        '
        'RectangleShape1
        '
        Me.RectangleShape1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RectangleShape1.BackColor = System.Drawing.Color.LightSlateGray
        Me.RectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.RectangleShape1.Location = New System.Drawing.Point(-11, 203)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(413, 82)
        '
        'LabelLogin
        '
        Me.LabelLogin.AutoSize = True
        Me.LabelLogin.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelLogin.Font = New System.Drawing.Font("微软雅黑", 20.0!)
        Me.LabelLogin.ForeColor = System.Drawing.SystemColors.Control
        Me.LabelLogin.Location = New System.Drawing.Point(188, 215)
        Me.LabelLogin.Name = "LabelLogin"
        Me.LabelLogin.Size = New System.Drawing.Size(171, 35)
        Me.LabelLogin.TabIndex = 9
        Me.LabelLogin.Text = "登陆系统 >>"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 12.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(334, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 21)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "X"
        '
        'LabelTo
        '
        Me.LabelTo.AutoSize = True
        Me.LabelTo.Location = New System.Drawing.Point(50, 153)
        Me.LabelTo.Name = "LabelTo"
        Me.LabelTo.Size = New System.Drawing.Size(53, 12)
        Me.LabelTo.TabIndex = 11
        Me.LabelTo.Text = "位  置："
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(109, 150)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(198, 20)
        Me.ComboBox1.TabIndex = 4
        '
        'LabelX
        '
        Me.LabelX.AutoSize = True
        Me.LabelX.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelX.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LabelX.Location = New System.Drawing.Point(311, 16)
        Me.LabelX.Name = "LabelX"
        Me.LabelX.Size = New System.Drawing.Size(17, 22)
        Me.LabelX.TabIndex = 13
        Me.LabelX.Text = "-"
        '
        'LabelExit
        '
        Me.LabelExit.AutoSize = True
        Me.LabelExit.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelExit.Font = New System.Drawing.Font("微软雅黑", 20.0!)
        Me.LabelExit.ForeColor = System.Drawing.SystemColors.Control
        Me.LabelExit.Location = New System.Drawing.Point(188, 215)
        Me.LabelExit.Name = "LabelExit"
        Me.LabelExit.Size = New System.Drawing.Size(171, 35)
        Me.LabelExit.TabIndex = 14
        Me.LabelExit.Text = "欢迎使用 >>"
        '
        'ButtonBj
        '
        Me.ButtonBj.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonBj.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonBj.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonBj.Location = New System.Drawing.Point(64, 51)
        Me.ButtonBj.Name = "ButtonBj"
        Me.ButtonBj.Size = New System.Drawing.Size(255, 23)
        Me.ButtonBj.TabIndex = 5
        Me.ButtonBj.Text = "1.报价系统"
        Me.ButtonBj.UseVisualStyleBackColor = False
        '
        'ButtonSc
        '
        Me.ButtonSc.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonSc.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonSc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSc.Location = New System.Drawing.Point(64, 80)
        Me.ButtonSc.Name = "ButtonSc"
        Me.ButtonSc.Size = New System.Drawing.Size(255, 23)
        Me.ButtonSc.TabIndex = 6
        Me.ButtonSc.Text = "2.入库系统"
        Me.ButtonSc.UseVisualStyleBackColor = False
        '
        'ButtonCk
        '
        Me.ButtonCk.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonCk.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonCk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCk.Location = New System.Drawing.Point(64, 138)
        Me.ButtonCk.Name = "ButtonCk"
        Me.ButtonCk.Size = New System.Drawing.Size(255, 23)
        Me.ButtonCk.TabIndex = 8
        Me.ButtonCk.Text = "4.仓库系统"
        Me.ButtonCk.UseVisualStyleBackColor = False
        '
        'ButtonJs
        '
        Me.ButtonJs.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonJs.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonJs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonJs.Location = New System.Drawing.Point(64, 167)
        Me.ButtonJs.Name = "ButtonJs"
        Me.ButtonJs.Size = New System.Drawing.Size(255, 23)
        Me.ButtonJs.TabIndex = 9
        Me.ButtonJs.Text = "5.查询系统"
        Me.ButtonJs.UseVisualStyleBackColor = False
        '
        'LabelAdmin
        '
        Me.LabelAdmin.AutoSize = True
        Me.LabelAdmin.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelAdmin.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelAdmin.ForeColor = System.Drawing.SystemColors.Control
        Me.LabelAdmin.Location = New System.Drawing.Point(12, 228)
        Me.LabelAdmin.Name = "LabelAdmin"
        Me.LabelAdmin.Size = New System.Drawing.Size(91, 19)
        Me.LabelAdmin.TabIndex = 19
        Me.LabelAdmin.Text = "<< 数据管理"
        '
        'ButtonSh
        '
        Me.ButtonSh.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonSh.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonSh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSh.Location = New System.Drawing.Point(64, 109)
        Me.ButtonSh.Name = "ButtonSh"
        Me.ButtonSh.Size = New System.Drawing.Size(255, 23)
        Me.ButtonSh.TabIndex = 7
        Me.ButtonSh.Text = "3.送货系统"
        Me.ButtonSh.UseVisualStyleBackColor = False
        '
        'LabelSjksz
        '
        Me.LabelSjksz.AutoSize = True
        Me.LabelSjksz.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSjksz.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelSjksz.ForeColor = System.Drawing.SystemColors.Control
        Me.LabelSjksz.Location = New System.Drawing.Point(12, 228)
        Me.LabelSjksz.Name = "LabelSjksz"
        Me.LabelSjksz.Size = New System.Drawing.Size(105, 19)
        Me.LabelSjksz.TabIndex = 20
        Me.LabelSjksz.Text = "<< 数据库设置"
        '
        'FormLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(383, 265)
        Me.Controls.Add(Me.LabelX)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonSh)
        Me.Controls.Add(Me.ButtonBj)
        Me.Controls.Add(Me.ButtonJs)
        Me.Controls.Add(Me.ButtonCk)
        Me.Controls.Add(Me.ButtonSc)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.LabelTo)
        Me.Controls.Add(Me.TextBoxPassword)
        Me.Controls.Add(Me.TextBoxUsrName)
        Me.Controls.Add(Me.LabelPassword)
        Me.Controls.Add(Me.LabelUsrName)
        Me.Controls.Add(Me.LabelSqlAdress)
        Me.Controls.Add(Me.TextBoxSqlAdress)
        Me.Controls.Add(Me.LabelLogin)
        Me.Controls.Add(Me.LabelSjksz)
        Me.Controls.Add(Me.LabelAdmin)
        Me.Controls.Add(Me.LabelExit)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "主界面 - 德清县下舍五金机械厂ERP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelSqlAdress As System.Windows.Forms.Label
    Friend WithEvents LabelUsrName As System.Windows.Forms.Label
    Friend WithEvents LabelPassword As System.Windows.Forms.Label
    Friend WithEvents TextBoxSqlAdress As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUsrName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents LabelLogin As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelTo As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents LabelX As System.Windows.Forms.Label
    Friend WithEvents LabelExit As System.Windows.Forms.Label
    Friend WithEvents ButtonBj As System.Windows.Forms.Button
    Friend WithEvents ButtonSc As System.Windows.Forms.Button
    Friend WithEvents ButtonCk As System.Windows.Forms.Button
    Friend WithEvents ButtonJs As System.Windows.Forms.Button
    Friend WithEvents LabelAdmin As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ButtonSh As System.Windows.Forms.Button
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents LabelSjksz As System.Windows.Forms.Label

End Class
