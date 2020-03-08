<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNRK
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNRK))
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.LabelBack = New System.Windows.Forms.Label()
        Me.CheckBoxQysc = New System.Windows.Forms.CheckBox()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.LabelJS = New System.Windows.Forms.Label()
        Me.LabelBJ = New System.Windows.Forms.Label()
        Me.LabelCK = New System.Windows.Forms.Label()
        Me.LabelSH = New System.Windows.Forms.Label()
        Me.LabelSC = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.DateTimePickerRkrq = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxMpbz = New System.Windows.Forms.TextBox()
        Me.LabelD = New System.Windows.Forms.Label()
        Me.LabelA = New System.Windows.Forms.Label()
        Me.TextBoxTh = New System.Windows.Forms.TextBox()
        Me.LabelC = New System.Windows.Forms.Label()
        Me.LabelB = New System.Windows.Forms.Label()
        Me.TextBoxMprks = New System.Windows.Forms.TextBox()
        Me.ButtonC = New System.Windows.Forms.Button()
        Me.ButtonD = New System.Windows.Forms.Button()
        Me.ButtonE = New System.Windows.Forms.Button()
        Me.LabelScjl = New System.Windows.Forms.Label()
        Me.ButtonB = New System.Windows.Forms.Button()
        Me.OpenFileDialogPicture = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.LabelSX = New System.Windows.Forms.Label()
        Me.TextBoxSX = New System.Windows.Forms.TextBox()
        Me.ButtonSX = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RectangleShape1
        '
        Me.RectangleShape1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RectangleShape1.BackColor = System.Drawing.Color.LightSlateGray
        Me.RectangleShape1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.RectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.RectangleShape1.Enabled = False
        Me.RectangleShape1.Location = New System.Drawing.Point(-4, -4)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(1694, 47)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1685, 889)
        Me.ShapeContainer1.TabIndex = 0
        Me.ShapeContainer1.TabStop = False
        '
        'ButtonBack
        '
        Me.ButtonBack.ForeColor = System.Drawing.Color.IndianRed
        Me.ButtonBack.Location = New System.Drawing.Point(16, 99)
        Me.ButtonBack.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(108, 29)
        Me.ButtonBack.TabIndex = 216
        Me.ButtonBack.Text = "返回上一级"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'LabelBack
        '
        Me.LabelBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelBack.AutoSize = True
        Me.LabelBack.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelBack.ForeColor = System.Drawing.Color.IndianRed
        Me.LabelBack.Location = New System.Drawing.Point(1560, 69)
        Me.LabelBack.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBack.Name = "LabelBack"
        Me.LabelBack.Size = New System.Drawing.Size(103, 18)
        Me.LabelBack.TabIndex = 215
        Me.LabelBack.Text = "返回上一级"
        '
        'CheckBoxQysc
        '
        Me.CheckBoxQysc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxQysc.AutoSize = True
        Me.CheckBoxQysc.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBoxQysc.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBoxQysc.ForeColor = System.Drawing.Color.White
        Me.CheckBoxQysc.Location = New System.Drawing.Point(1549, 15)
        Me.CheckBoxQysc.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBoxQysc.Name = "CheckBoxQysc"
        Me.CheckBoxQysc.Size = New System.Drawing.Size(119, 19)
        Me.CheckBoxQysc.TabIndex = 214
        Me.CheckBoxQysc.Text = "启用删除功能"
        Me.CheckBoxQysc.UseVisualStyleBackColor = False
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.Location = New System.Drawing.Point(16, 66)
        Me.LabelMain.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(294, 20)
        Me.LabelMain.TabIndex = 213
        Me.LabelMain.Text = "请选择订单以开始毛坯入库 >>"
        '
        'LabelJS
        '
        Me.LabelJS.AutoSize = True
        Me.LabelJS.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelJS.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelJS.ForeColor = System.Drawing.Color.White
        Me.LabelJS.Location = New System.Drawing.Point(603, 11)
        Me.LabelJS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelJS.Name = "LabelJS"
        Me.LabelJS.Size = New System.Drawing.Size(130, 31)
        Me.LabelJS.TabIndex = 211
        Me.LabelJS.Text = "5.查询系统"
        '
        'LabelBJ
        '
        Me.LabelBJ.AutoSize = True
        Me.LabelBJ.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelBJ.Font = New System.Drawing.Font("微软雅黑", 14.25!)
        Me.LabelBJ.ForeColor = System.Drawing.Color.White
        Me.LabelBJ.Location = New System.Drawing.Point(16, 11)
        Me.LabelBJ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBJ.Name = "LabelBJ"
        Me.LabelBJ.Size = New System.Drawing.Size(130, 31)
        Me.LabelBJ.TabIndex = 210
        Me.LabelBJ.Text = "1.报价系统"
        '
        'LabelCK
        '
        Me.LabelCK.AutoSize = True
        Me.LabelCK.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelCK.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelCK.ForeColor = System.Drawing.Color.White
        Me.LabelCK.Location = New System.Drawing.Point(456, 11)
        Me.LabelCK.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCK.Name = "LabelCK"
        Me.LabelCK.Size = New System.Drawing.Size(130, 31)
        Me.LabelCK.TabIndex = 209
        Me.LabelCK.Text = "4.仓库系统"
        '
        'LabelSH
        '
        Me.LabelSH.AutoSize = True
        Me.LabelSH.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSH.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSH.ForeColor = System.Drawing.Color.White
        Me.LabelSH.Location = New System.Drawing.Point(309, 11)
        Me.LabelSH.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSH.Name = "LabelSH"
        Me.LabelSH.Size = New System.Drawing.Size(130, 31)
        Me.LabelSH.TabIndex = 208
        Me.LabelSH.Text = "3.送货系统"
        '
        'LabelSC
        '
        Me.LabelSC.AutoSize = True
        Me.LabelSC.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSC.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold)
        Me.LabelSC.ForeColor = System.Drawing.Color.White
        Me.LabelSC.Location = New System.Drawing.Point(163, 11)
        Me.LabelSC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSC.Name = "LabelSC"
        Me.LabelSC.Size = New System.Drawing.Size(132, 31)
        Me.LabelSC.TabIndex = 207
        Me.LabelSC.Text = "2.入库系统"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 639)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 15)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "毛坯入库记录："
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(16, 658)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(1653, 216)
        Me.DataGridView2.TabIndex = 218
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(16, 135)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(1653, 500)
        Me.DataGridView1.TabIndex = 217
        '
        'ButtonA
        '
        Me.ButtonA.Location = New System.Drawing.Point(132, 99)
        Me.ButtonA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(108, 29)
        Me.ButtonA.TabIndex = 220
        Me.ButtonA.Text = "导出生产单"
        Me.ButtonA.UseVisualStyleBackColor = True
        '
        'DateTimePickerRkrq
        '
        Me.DateTimePickerRkrq.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerRkrq.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerRkrq.Location = New System.Drawing.Point(549, 101)
        Me.DateTimePickerRkrq.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DateTimePickerRkrq.Name = "DateTimePickerRkrq"
        Me.DateTimePickerRkrq.Size = New System.Drawing.Size(119, 25)
        Me.DateTimePickerRkrq.TabIndex = 262
        '
        'TextBoxMpbz
        '
        Me.TextBoxMpbz.Location = New System.Drawing.Point(963, 101)
        Me.TextBoxMpbz.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxMpbz.Name = "TextBoxMpbz"
        Me.TextBoxMpbz.Size = New System.Drawing.Size(159, 25)
        Me.TextBoxMpbz.TabIndex = 264
        '
        'LabelD
        '
        Me.LabelD.AutoSize = True
        Me.LabelD.Location = New System.Drawing.Point(884, 105)
        Me.LabelD.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelD.Name = "LabelD"
        Me.LabelD.Size = New System.Drawing.Size(67, 15)
        Me.LabelD.TabIndex = 268
        Me.LabelD.Text = "毛坯备注"
        '
        'LabelA
        '
        Me.LabelA.AutoSize = True
        Me.LabelA.Location = New System.Drawing.Point(248, 105)
        Me.LabelA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelA.Name = "LabelA"
        Me.LabelA.Size = New System.Drawing.Size(37, 15)
        Me.LabelA.TabIndex = 267
        Me.LabelA.Text = "图号"
        '
        'TextBoxTh
        '
        Me.TextBoxTh.Location = New System.Drawing.Point(303, 101)
        Me.TextBoxTh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxTh.Name = "TextBoxTh"
        Me.TextBoxTh.Size = New System.Drawing.Size(159, 25)
        Me.TextBoxTh.TabIndex = 261
        '
        'LabelC
        '
        Me.LabelC.AutoSize = True
        Me.LabelC.Location = New System.Drawing.Point(677, 105)
        Me.LabelC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelC.Name = "LabelC"
        Me.LabelC.Size = New System.Drawing.Size(67, 15)
        Me.LabelC.TabIndex = 266
        Me.LabelC.Text = "入库数量"
        '
        'LabelB
        '
        Me.LabelB.AutoSize = True
        Me.LabelB.Location = New System.Drawing.Point(471, 105)
        Me.LabelB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelB.Name = "LabelB"
        Me.LabelB.Size = New System.Drawing.Size(67, 15)
        Me.LabelB.TabIndex = 265
        Me.LabelB.Text = "入库日期"
        '
        'TextBoxMprks
        '
        Me.TextBoxMprks.Location = New System.Drawing.Point(756, 101)
        Me.TextBoxMprks.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxMprks.Name = "TextBoxMprks"
        Me.TextBoxMprks.Size = New System.Drawing.Size(119, 25)
        Me.TextBoxMprks.TabIndex = 263
        '
        'ButtonC
        '
        Me.ButtonC.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonC.Location = New System.Drawing.Point(1239, 99)
        Me.ButtonC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonC.Name = "ButtonC"
        Me.ButtonC.Size = New System.Drawing.Size(100, 29)
        Me.ButtonC.TabIndex = 273
        Me.ButtonC.Text = "毛坯入库"
        Me.ButtonC.UseVisualStyleBackColor = True
        '
        'ButtonD
        '
        Me.ButtonD.Location = New System.Drawing.Point(1347, 99)
        Me.ButtonD.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonD.Name = "ButtonD"
        Me.ButtonD.Size = New System.Drawing.Size(100, 29)
        Me.ButtonD.TabIndex = 270
        Me.ButtonD.Text = "导入图纸"
        Me.ButtonD.UseVisualStyleBackColor = True
        '
        'ButtonE
        '
        Me.ButtonE.Location = New System.Drawing.Point(1455, 99)
        Me.ButtonE.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonE.Name = "ButtonE"
        Me.ButtonE.Size = New System.Drawing.Size(100, 29)
        Me.ButtonE.TabIndex = 271
        Me.ButtonE.Text = "打开图纸"
        Me.ButtonE.UseVisualStyleBackColor = True
        '
        'LabelScjl
        '
        Me.LabelScjl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelScjl.AutoSize = True
        Me.LabelScjl.BackColor = System.Drawing.Color.Transparent
        Me.LabelScjl.ForeColor = System.Drawing.Color.IndianRed
        Me.LabelScjl.Location = New System.Drawing.Point(1599, 639)
        Me.LabelScjl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelScjl.Name = "LabelScjl"
        Me.LabelScjl.Size = New System.Drawing.Size(67, 15)
        Me.LabelScjl.TabIndex = 274
        Me.LabelScjl.Text = "删除记录"
        '
        'ButtonB
        '
        Me.ButtonB.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ButtonB.Location = New System.Drawing.Point(1131, 99)
        Me.ButtonB.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonB.Name = "ButtonB"
        Me.ButtonB.Size = New System.Drawing.Size(100, 29)
        Me.ButtonB.TabIndex = 275
        Me.ButtonB.Text = "图号搜索"
        Me.ButtonB.UseVisualStyleBackColor = True
        '
        'OpenFileDialogPicture
        '
        Me.OpenFileDialogPicture.FileName = "OpenFileDialog1"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBox1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(1413, 15)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(119, 19)
        Me.CheckBox1.TabIndex = 276
        Me.CheckBox1.Text = "显示所有订单"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'LabelSX
        '
        Me.LabelSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSX.AutoSize = True
        Me.LabelSX.Location = New System.Drawing.Point(1261, 105)
        Me.LabelSX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSX.Name = "LabelSX"
        Me.LabelSX.Size = New System.Drawing.Size(142, 15)
        Me.LabelSX.TabIndex = 279
        Me.LabelSX.Text = "输入关键字搜索订单"
        '
        'TextBoxSX
        '
        Me.TextBoxSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSX.Location = New System.Drawing.Point(1420, 101)
        Me.TextBoxSX.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxSX.Name = "TextBoxSX"
        Me.TextBoxSX.Size = New System.Drawing.Size(132, 25)
        Me.TextBoxSX.TabIndex = 278
        '
        'ButtonSX
        '
        Me.ButtonSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSX.Location = New System.Drawing.Point(1561, 99)
        Me.ButtonSX.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonSX.Name = "ButtonSX"
        Me.ButtonSX.Size = New System.Drawing.Size(108, 29)
        Me.ButtonSX.TabIndex = 277
        Me.ButtonSX.Text = "筛选订单"
        Me.ButtonSX.UseVisualStyleBackColor = True
        '
        'FormNRK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1685, 889)
        Me.Controls.Add(Me.LabelSX)
        Me.Controls.Add(Me.TextBoxSX)
        Me.Controls.Add(Me.ButtonSX)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ButtonB)
        Me.Controls.Add(Me.LabelScjl)
        Me.Controls.Add(Me.ButtonC)
        Me.Controls.Add(Me.ButtonD)
        Me.Controls.Add(Me.ButtonE)
        Me.Controls.Add(Me.DateTimePickerRkrq)
        Me.Controls.Add(Me.TextBoxMpbz)
        Me.Controls.Add(Me.LabelD)
        Me.Controls.Add(Me.LabelA)
        Me.Controls.Add(Me.TextBoxTh)
        Me.Controls.Add(Me.LabelC)
        Me.Controls.Add(Me.LabelB)
        Me.Controls.Add(Me.TextBoxMprks)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonBack)
        Me.Controls.Add(Me.LabelBack)
        Me.Controls.Add(Me.CheckBoxQysc)
        Me.Controls.Add(Me.LabelMain)
        Me.Controls.Add(Me.LabelJS)
        Me.Controls.Add(Me.LabelBJ)
        Me.Controls.Add(Me.LabelCK)
        Me.Controls.Add(Me.LabelSH)
        Me.Controls.Add(Me.LabelSC)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MinimumSize = New System.Drawing.Size(1701, 925)
        Me.Name = "FormNRK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "入库系统"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
    Friend WithEvents LabelBack As System.Windows.Forms.Label
    Friend WithEvents CheckBoxQysc As System.Windows.Forms.CheckBox
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents LabelJS As System.Windows.Forms.Label
    Friend WithEvents LabelBJ As System.Windows.Forms.Label
    Friend WithEvents LabelCK As System.Windows.Forms.Label
    Friend WithEvents LabelSH As System.Windows.Forms.Label
    Friend WithEvents LabelSC As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerRkrq As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxMpbz As System.Windows.Forms.TextBox
    Friend WithEvents LabelD As System.Windows.Forms.Label
    Friend WithEvents LabelA As System.Windows.Forms.Label
    Friend WithEvents TextBoxTh As System.Windows.Forms.TextBox
    Friend WithEvents LabelC As System.Windows.Forms.Label
    Friend WithEvents LabelB As System.Windows.Forms.Label
    Friend WithEvents TextBoxMprks As System.Windows.Forms.TextBox
    Friend WithEvents ButtonC As System.Windows.Forms.Button
    Friend WithEvents ButtonD As System.Windows.Forms.Button
    Friend WithEvents ButtonE As System.Windows.Forms.Button
    Friend WithEvents LabelScjl As System.Windows.Forms.Label
    Friend WithEvents ButtonB As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialogPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents LabelSX As System.Windows.Forms.Label
    Friend WithEvents TextBoxSX As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSX As System.Windows.Forms.Button
End Class
