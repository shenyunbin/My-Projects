<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MOVMENT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MOVMENT))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Buttonopen = New System.Windows.Forms.Button()
        Me.portnamebox = New System.Windows.Forms.ComboBox()
        Me.baudratebox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Buttonclose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.moving = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Labelx = New System.Windows.Forms.Label()
        Me.Labely = New System.Windows.Forms.Label()
        Me.Labelz = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.WriteStart = New System.Windows.Forms.Button()
        Me.FileName = New System.Windows.Forms.TextBox()
        Me.WriteStop = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(198, 40)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(618, 514)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Buttonopen
        '
        Me.Buttonopen.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Buttonopen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Buttonopen.Location = New System.Drawing.Point(12, 66)
        Me.Buttonopen.Name = "Buttonopen"
        Me.Buttonopen.Size = New System.Drawing.Size(87, 23)
        Me.Buttonopen.TabIndex = 1
        Me.Buttonopen.Text = "打开"
        Me.Buttonopen.UseVisualStyleBackColor = True
        '
        'portnamebox
        '
        Me.portnamebox.BackColor = System.Drawing.Color.White
        Me.portnamebox.FormattingEnabled = True
        Me.portnamebox.Location = New System.Drawing.Point(12, 40)
        Me.portnamebox.Name = "portnamebox"
        Me.portnamebox.Size = New System.Drawing.Size(99, 20)
        Me.portnamebox.TabIndex = 6
        '
        'baudratebox
        '
        Me.baudratebox.Location = New System.Drawing.Point(117, 40)
        Me.baudratebox.Name = "baudratebox"
        Me.baudratebox.Size = New System.Drawing.Size(75, 21)
        Me.baudratebox.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(83, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Closed"
        '
        'SerialPort1
        '
        '
        'Buttonclose
        '
        Me.Buttonclose.BackColor = System.Drawing.Color.White
        Me.Buttonclose.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Buttonclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Buttonclose.Location = New System.Drawing.Point(105, 67)
        Me.Buttonclose.Name = "Buttonclose"
        Me.Buttonclose.Size = New System.Drawing.Size(87, 23)
        Me.Buttonclose.TabIndex = 12
        Me.Buttonclose.Text = "关闭"
        Me.Buttonclose.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "串口状态："
        '
        'Button5
        '
        Me.Button5.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Location = New System.Drawing.Point(14, 137)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(178, 23)
        Me.Button5.TabIndex = 14
        Me.Button5.Text = "清除数据"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label7.Location = New System.Drawing.Point(215, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "传感器状态："
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(12, -12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(180, 46)
        Me.Button1.TabIndex = 21
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(14, 108)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(178, 23)
        Me.Button3.TabIndex = 22
        Me.Button3.Text = "位置锁定"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.moving})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1036, 570)
        Me.ShapeContainer1.TabIndex = 23
        Me.ShapeContainer1.TabStop = False
        '
        'moving
        '
        Me.moving.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.moving.FillGradientColor = System.Drawing.Color.White
        Me.moving.Location = New System.Drawing.Point(194, -3)
        Me.moving.Name = "RectangleShape1"
        Me.moving.SelectionColor = System.Drawing.SystemColors.Control
        Me.moving.Size = New System.Drawing.Size(844, 581)
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.LightGray
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.HideSelection = False
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 234)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(180, 320)
        Me.RichTextBox1.TabIndex = 7
        Me.RichTextBox1.Text = ""
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.White
        Me.PictureBox4.Location = New System.Drawing.Point(822, 55)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(200, 100)
        Me.PictureBox4.TabIndex = 26
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.White
        Me.PictureBox5.Location = New System.Drawing.Point(822, 173)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(200, 100)
        Me.PictureBox5.TabIndex = 27
        Me.PictureBox5.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.White
        Me.PictureBox6.Location = New System.Drawing.Point(822, 291)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(200, 100)
        Me.PictureBox6.TabIndex = 28
        Me.PictureBox6.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(823, 276)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Z轴速度变化："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(823, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 12)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Y轴速度变化："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(822, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 12)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "X轴速度变化："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(823, 394)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 12)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "XY平面位移轨迹："
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Location = New System.Drawing.Point(822, 409)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 145)
        Me.PictureBox2.TabIndex = 33
        Me.PictureBox2.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(216, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 12)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "空间坐标/cm："
        '
        'Labelx
        '
        Me.Labelx.AutoSize = True
        Me.Labelx.Location = New System.Drawing.Point(216, 98)
        Me.Labelx.Name = "Labelx"
        Me.Labelx.Size = New System.Drawing.Size(17, 12)
        Me.Labelx.TabIndex = 35
        Me.Labelx.Text = "x:"
        '
        'Labely
        '
        Me.Labely.AutoSize = True
        Me.Labely.Location = New System.Drawing.Point(216, 119)
        Me.Labely.Name = "Labely"
        Me.Labely.Size = New System.Drawing.Size(17, 12)
        Me.Labely.TabIndex = 36
        Me.Labely.Text = "y:"
        '
        'Labelz
        '
        Me.Labelz.AutoSize = True
        Me.Labelz.Location = New System.Drawing.Point(216, 140)
        Me.Labelz.Name = "Labelz"
        Me.Labelz.Size = New System.Drawing.Size(17, 12)
        Me.Labelz.TabIndex = 37
        Me.Labelz.Text = "z:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label9.Location = New System.Drawing.Point(713, 549)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(311, 12)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "手指运动参数检测系统3.0    作者：沈云彬（02116026）"
        '
        'WriteStart
        '
        Me.WriteStart.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.WriteStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WriteStart.Location = New System.Drawing.Point(14, 205)
        Me.WriteStart.Name = "WriteStart"
        Me.WriteStart.Size = New System.Drawing.Size(85, 23)
        Me.WriteStart.TabIndex = 40
        Me.WriteStart.Text = "开始写入"
        Me.WriteStart.UseVisualStyleBackColor = True
        '
        'FileName
        '
        Me.FileName.Location = New System.Drawing.Point(14, 178)
        Me.FileName.Name = "FileName"
        Me.FileName.Size = New System.Drawing.Size(178, 21)
        Me.FileName.TabIndex = 41
        '
        'WriteStop
        '
        Me.WriteStop.BackColor = System.Drawing.Color.White
        Me.WriteStop.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.WriteStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WriteStop.Location = New System.Drawing.Point(105, 205)
        Me.WriteStop.Name = "WriteStop"
        Me.WriteStop.Size = New System.Drawing.Size(87, 23)
        Me.WriteStop.TabIndex = 42
        Me.WriteStop.Text = "停止"
        Me.WriteStop.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 163)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 12)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "数据保存位置："
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.Gray
        Me.Button2.Location = New System.Drawing.Point(176, 178)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(16, 21)
        Me.Button2.TabIndex = 44
        Me.Button2.Text = "…"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'MOVMENT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1036, 570)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.WriteStop)
        Me.Controls.Add(Me.FileName)
        Me.Controls.Add(Me.WriteStart)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Labelz)
        Me.Controls.Add(Me.Labely)
        Me.Controls.Add(Me.Labelx)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Buttonclose)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.baudratebox)
        Me.Controls.Add(Me.portnamebox)
        Me.Controls.Add(Me.Buttonopen)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MOVMENT"
        Me.Text = "手指运动参数检测系统4.0"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Buttonopen As System.Windows.Forms.Button
    Friend WithEvents portnamebox As System.Windows.Forms.ComboBox
    Friend WithEvents baudratebox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Buttonclose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents moving As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Labelx As System.Windows.Forms.Label
    Friend WithEvents Labely As System.Windows.Forms.Label
    Friend WithEvents Labelz As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents WriteStart As System.Windows.Forms.Button
    Friend WithEvents FileName As System.Windows.Forms.TextBox
    Friend WithEvents WriteStop As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
