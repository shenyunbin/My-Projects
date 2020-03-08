<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.L_chazhi = New System.Windows.Forms.Button()
        Me.N_chazhi = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VIN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Line_nihe = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GRA = New System.Windows.Forms.PictureBox()
        Me.LabelYmax = New System.Windows.Forms.Label()
        Me.LabelYmin = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LabelXmin = New System.Windows.Forms.Label()
        Me.LabelXmax = New System.Windows.Forms.Label()
        Me.TextY = New System.Windows.Forms.TextBox()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Xine = New System.Windows.Forms.TextBox()
        Me.Yine = New System.Windows.Forms.TextBox()
        Me.ERRO = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Labelup = New System.Windows.Forms.Label()
        Me.Labelleft = New System.Windows.Forms.Label()
        Me.Labelright = New System.Windows.Forms.Label()
        Me.Labeldown = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.GRA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'L_chazhi
        '
        Me.L_chazhi.BackColor = System.Drawing.Color.White
        Me.L_chazhi.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.L_chazhi.Location = New System.Drawing.Point(535, 123)
        Me.L_chazhi.Name = "L_chazhi"
        Me.L_chazhi.Size = New System.Drawing.Size(95, 23)
        Me.L_chazhi.TabIndex = 0
        Me.L_chazhi.Text = "拉格朗日插值"
        Me.L_chazhi.UseVisualStyleBackColor = False
        '
        'N_chazhi
        '
        Me.N_chazhi.BackColor = System.Drawing.Color.White
        Me.N_chazhi.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.N_chazhi.Location = New System.Drawing.Point(434, 123)
        Me.N_chazhi.Name = "N_chazhi"
        Me.N_chazhi.Size = New System.Drawing.Size(95, 23)
        Me.N_chazhi.TabIndex = 1
        Me.N_chazhi.Text = "牛顿插值"
        Me.N_chazhi.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "xi:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "yi:"
        '
        'VIN
        '
        Me.VIN.BackColor = System.Drawing.Color.White
        Me.VIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VIN.Location = New System.Drawing.Point(437, 63)
        Me.VIN.Name = "VIN"
        Me.VIN.Size = New System.Drawing.Size(80, 21)
        Me.VIN.TabIndex = 18
        Me.VIN.Text = "0,"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(410, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "x:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(523, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "y:"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(333, 134)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(10, 14)
        Me.TextBox2.TabIndex = 22
        Me.TextBox2.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(304, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 12)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "共:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.Location = New System.Drawing.Point(349, 134)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "组数据"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("宋体", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 15)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "数据输入："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("宋体", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label9.Location = New System.Drawing.Point(403, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 15)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "数据插值："
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("宋体", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 147)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 15)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "曲线拟合："
        '
        'Line_nihe
        '
        Me.Line_nihe.BackColor = System.Drawing.Color.White
        Me.Line_nihe.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Line_nihe.Location = New System.Drawing.Point(535, 200)
        Me.Line_nihe.Name = "Line_nihe"
        Me.Line_nihe.Size = New System.Drawing.Size(95, 23)
        Me.Line_nihe.TabIndex = 28
        Me.Line_nihe.Text = "曲线拟合"
        Me.Line_nihe.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(19, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "y ="
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Location = New System.Drawing.Point(91, 206)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(21, 14)
        Me.TextBox3.TabIndex = 32
        Me.TextBox3.Text = "2"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 206)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "拟合次数："
        '
        'GRA
        '
        Me.GRA.BackColor = System.Drawing.Color.White
        Me.GRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GRA.Location = New System.Drawing.Point(22, 272)
        Me.GRA.Name = "GRA"
        Me.GRA.Size = New System.Drawing.Size(507, 283)
        Me.GRA.TabIndex = 34
        Me.GRA.TabStop = False
        '
        'LabelYmax
        '
        Me.LabelYmax.AutoSize = True
        Me.LabelYmax.BackColor = System.Drawing.Color.White
        Me.LabelYmax.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelYmax.Location = New System.Drawing.Point(535, 272)
        Me.LabelYmax.Name = "LabelYmax"
        Me.LabelYmax.Size = New System.Drawing.Size(35, 12)
        Me.LabelYmax.TabIndex = 37
        Me.LabelYmax.Text = "Ymax:"
        '
        'LabelYmin
        '
        Me.LabelYmin.AutoSize = True
        Me.LabelYmin.BackColor = System.Drawing.Color.White
        Me.LabelYmin.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelYmin.Location = New System.Drawing.Point(535, 543)
        Me.LabelYmin.Name = "LabelYmin"
        Me.LabelYmin.Size = New System.Drawing.Size(35, 12)
        Me.LabelYmin.TabIndex = 38
        Me.LabelYmin.Text = "Ymin:"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(550, 63)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(80, 21)
        Me.TextBox1.TabIndex = 39
        '
        'LabelXmin
        '
        Me.LabelXmin.AutoSize = True
        Me.LabelXmin.BackColor = System.Drawing.Color.White
        Me.LabelXmin.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelXmin.Location = New System.Drawing.Point(20, 558)
        Me.LabelXmin.Name = "LabelXmin"
        Me.LabelXmin.Size = New System.Drawing.Size(41, 12)
        Me.LabelXmin.TabIndex = 40
        Me.LabelXmin.Text = "Xmin："
        '
        'LabelXmax
        '
        Me.LabelXmax.AutoSize = True
        Me.LabelXmax.BackColor = System.Drawing.Color.White
        Me.LabelXmax.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelXmax.Location = New System.Drawing.Point(477, 558)
        Me.LabelXmax.Name = "LabelXmax"
        Me.LabelXmax.Size = New System.Drawing.Size(41, 12)
        Me.LabelXmax.TabIndex = 41
        Me.LabelXmax.Text = "Xmax："
        '
        'TextY
        '
        Me.TextY.BackColor = System.Drawing.Color.White
        Me.TextY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextY.Location = New System.Drawing.Point(56, 173)
        Me.TextY.Name = "TextY"
        Me.TextY.Size = New System.Drawing.Size(574, 21)
        Me.TextY.TabIndex = 42
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1, Me.RectangleShape2, Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(646, 605)
        Me.ShapeContainer1.TabIndex = 43
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.SelectionColor = System.Drawing.SystemColors.ControlLightLight
        Me.LineShape2.X1 = 52
        Me.LineShape2.X2 = 393
        Me.LineShape2.Y1 = 126
        Me.LineShape2.Y2 = 126
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.SelectionColor = System.Drawing.SystemColors.ControlLightLight
        Me.LineShape1.X1 = 52
        Me.LineShape1.X2 = 393
        Me.LineShape1.Y1 = 99
        Me.LineShape1.Y2 = 99
        '
        'RectangleShape2
        '
        Me.RectangleShape2.BorderColor = System.Drawing.Color.Red
        Me.RectangleShape2.FillColor = System.Drawing.Color.Red
        Me.RectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape2.Location = New System.Drawing.Point(567, -8)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(62, 27)
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BorderColor = System.Drawing.Color.White
        Me.RectangleShape1.FillColor = System.Drawing.Color.White
        Me.RectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape1.Location = New System.Drawing.Point(-7, -4)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.SelectionColor = System.Drawing.SystemColors.ControlLightLight
        Me.RectangleShape1.Size = New System.Drawing.Size(647, 617)
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Font = New System.Drawing.Font("宋体", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label12.Location = New System.Drawing.Point(11, 242)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 15)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "图像显示："
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Red
        Me.Label13.Font = New System.Drawing.Font("宋体", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(576, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "CLOSE"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.Gray
        Me.Label14.Location = New System.Drawing.Point(12, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(308, 16)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "计算方法作业——数据插值与拟合 V2.0"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Gray
        Me.Label15.Location = New System.Drawing.Point(371, 584)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(263, 12)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "The software is made by Shenyunbin 02116026"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Gray
        Me.Label16.Location = New System.Drawing.Point(12, 584)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(173, 12)
        Me.Label16.TabIndex = 48
        Me.Label16.Text = "Welcome to use the software!"
        '
        'Xine
        '
        Me.Xine.BackColor = System.Drawing.Color.White
        Me.Xine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Xine.Location = New System.Drawing.Point(53, 83)
        Me.Xine.Name = "Xine"
        Me.Xine.Size = New System.Drawing.Size(342, 14)
        Me.Xine.TabIndex = 49
        Me.Xine.Text = "1,2,3,"
        '
        'Yine
        '
        Me.Yine.BackColor = System.Drawing.Color.White
        Me.Yine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Yine.Location = New System.Drawing.Point(53, 110)
        Me.Yine.Name = "Yine"
        Me.Yine.Size = New System.Drawing.Size(342, 14)
        Me.Yine.TabIndex = 50
        '
        'ERRO
        '
        Me.ERRO.AutoSize = True
        Me.ERRO.BackColor = System.Drawing.Color.White
        Me.ERRO.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ERRO.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ERRO.Location = New System.Drawing.Point(20, 63)
        Me.ERRO.Name = "ERRO"
        Me.ERRO.Size = New System.Drawing.Size(161, 12)
        Me.ERRO.TabIndex = 51
        Me.ERRO.Text = "注:请以a,b,c,...的格式输入"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(434, 200)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 23)
        Me.Button1.TabIndex = 53
        Me.Button1.Text = "清除图像"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label17.Location = New System.Drawing.Point(333, 257)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(185, 12)
        Me.Label17.TabIndex = 54
        Me.Label17.Text = "图像上滑动鼠标滚轮进行放大缩小"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label18.Location = New System.Drawing.Point(494, 95)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(107, 12)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "拉格朗日插值次数:"
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.White
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox5.Location = New System.Drawing.Point(607, 95)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(21, 14)
        Me.TextBox5.TabIndex = 57
        Me.TextBox5.Text = "2"
        '
        'Labelup
        '
        Me.Labelup.AutoSize = True
        Me.Labelup.Location = New System.Drawing.Point(261, 279)
        Me.Labelup.Name = "Labelup"
        Me.Labelup.Size = New System.Drawing.Size(29, 12)
        Me.Labelup.TabIndex = 58
        Me.Labelup.Text = "向上"
        '
        'Labelleft
        '
        Me.Labelleft.AutoSize = True
        Me.Labelleft.Location = New System.Drawing.Point(29, 407)
        Me.Labelleft.Name = "Labelleft"
        Me.Labelleft.Size = New System.Drawing.Size(29, 12)
        Me.Labelleft.TabIndex = 59
        Me.Labelleft.Text = "向左"
        '
        'Labelright
        '
        Me.Labelright.AutoSize = True
        Me.Labelright.Location = New System.Drawing.Point(494, 407)
        Me.Labelright.Name = "Labelright"
        Me.Labelright.Size = New System.Drawing.Size(29, 12)
        Me.Labelright.TabIndex = 60
        Me.Labelright.Text = "向右"
        '
        'Labeldown
        '
        Me.Labeldown.AutoSize = True
        Me.Labeldown.Location = New System.Drawing.Point(261, 536)
        Me.Labeldown.Name = "Labeldown"
        Me.Labeldown.Size = New System.Drawing.Size(29, 12)
        Me.Labeldown.TabIndex = 61
        Me.Labeldown.Text = "向下"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(332, 204)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox1.TabIndex = 62
        Me.CheckBox1.Text = "自动清除图像"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(646, 605)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Labeldown)
        Me.Controls.Add(Me.Labelright)
        Me.Controls.Add(Me.Labelleft)
        Me.Controls.Add(Me.Labelup)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ERRO)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextY)
        Me.Controls.Add(Me.LabelXmax)
        Me.Controls.Add(Me.LabelXmin)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelYmin)
        Me.Controls.Add(Me.LabelYmax)
        Me.Controls.Add(Me.GRA)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Line_nihe)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.VIN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.N_chazhi)
        Me.Controls.Add(Me.L_chazhi)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Yine)
        Me.Controls.Add(Me.Xine)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "计算方法作业——数据插值拟合"
        CType(Me.GRA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents L_chazhi As System.Windows.Forms.Button
    Friend WithEvents N_chazhi As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VIN As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Line_nihe As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GRA As System.Windows.Forms.PictureBox
    Friend WithEvents LabelYmax As System.Windows.Forms.Label
    Friend WithEvents LabelYmin As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents LabelXmin As System.Windows.Forms.Label
    Friend WithEvents LabelXmax As System.Windows.Forms.Label
    Friend WithEvents TextY As System.Windows.Forms.TextBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Xine As System.Windows.Forms.TextBox
    Friend WithEvents Yine As System.Windows.Forms.TextBox
    Friend WithEvents ERRO As System.Windows.Forms.Label
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Labelup As System.Windows.Forms.Label
    Friend WithEvents Labelleft As System.Windows.Forms.Label
    Friend WithEvents Labelright As System.Windows.Forms.Label
    Friend WithEvents Labeldown As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox

End Class
