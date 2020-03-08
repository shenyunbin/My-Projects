<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNSH
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNSH))
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.LabelBack = New System.Windows.Forms.Label()
        Me.CheckBoxQysc = New System.Windows.Forms.CheckBox()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.LabelJS = New System.Windows.Forms.Label()
        Me.LabelBJ = New System.Windows.Forms.Label()
        Me.LabelCK = New System.Windows.Forms.Label()
        Me.LabelSH = New System.Windows.Forms.Label()
        Me.LabelSC = New System.Windows.Forms.Label()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LabelScjl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewShjl = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewShdxx = New System.Windows.Forms.DataGridView()
        Me.DateTimePickerShrq = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxBz = New System.Windows.Forms.TextBox()
        Me.LabelF = New System.Windows.Forms.Label()
        Me.TextBoxWwg = New System.Windows.Forms.TextBox()
        Me.LabelD = New System.Windows.Forms.Label()
        Me.LabelE = New System.Windows.Forms.Label()
        Me.TextBoxYwg = New System.Windows.Forms.TextBox()
        Me.TextBoxShsl = New System.Windows.Forms.TextBox()
        Me.LabelB = New System.Windows.Forms.Label()
        Me.LabelA = New System.Windows.Forms.Label()
        Me.LabelC = New System.Windows.Forms.Label()
        Me.TextBoxXh = New System.Windows.Forms.TextBox()
        Me.ButtonC = New System.Windows.Forms.Button()
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.ButtonB = New System.Windows.Forms.Button()
        Me.LabelShrq = New System.Windows.Forms.Label()
        Me.LabelShxqsc = New System.Windows.Forms.Label()
        Me.LabelDdxx = New System.Windows.Forms.Label()
        Me.LabelShdToExcel = New System.Windows.Forms.Label()
        Me.OpenFileDialogPicture = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        Me.ButtonD = New System.Windows.Forms.Button()
        Me.ButtonE = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.LabelSX = New System.Windows.Forms.Label()
        Me.TextBoxSX = New System.Windows.Forms.TextBox()
        Me.ButtonSX = New System.Windows.Forms.Button()
        CType(Me.DataGridViewShjl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewShdxx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonBack
        '
        Me.ButtonBack.ForeColor = System.Drawing.Color.IndianRed
        Me.ButtonBack.Location = New System.Drawing.Point(12, 79)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(81, 23)
        Me.ButtonBack.TabIndex = 284
        Me.ButtonBack.Text = "返回上一级"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'LabelBack
        '
        Me.LabelBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelBack.AutoSize = True
        Me.LabelBack.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelBack.ForeColor = System.Drawing.Color.IndianRed
        Me.LabelBack.Location = New System.Drawing.Point(1170, 55)
        Me.LabelBack.Name = "LabelBack"
        Me.LabelBack.Size = New System.Drawing.Size(82, 14)
        Me.LabelBack.TabIndex = 283
        Me.LabelBack.Text = "返回上一级"
        '
        'CheckBoxQysc
        '
        Me.CheckBoxQysc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxQysc.AutoSize = True
        Me.CheckBoxQysc.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBoxQysc.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBoxQysc.ForeColor = System.Drawing.Color.White
        Me.CheckBoxQysc.Location = New System.Drawing.Point(1156, 12)
        Me.CheckBoxQysc.Name = "CheckBoxQysc"
        Me.CheckBoxQysc.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxQysc.TabIndex = 282
        Me.CheckBoxQysc.Text = "启用删除功能"
        Me.CheckBoxQysc.UseVisualStyleBackColor = False
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.Location = New System.Drawing.Point(12, 53)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(375, 16)
        Me.LabelMain.TabIndex = 281
        Me.LabelMain.Text = "请在左上方表格中选择订单以开始送货单编辑 >>"
        '
        'LabelJS
        '
        Me.LabelJS.AutoSize = True
        Me.LabelJS.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelJS.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelJS.ForeColor = System.Drawing.Color.White
        Me.LabelJS.Location = New System.Drawing.Point(452, 9)
        Me.LabelJS.Name = "LabelJS"
        Me.LabelJS.Size = New System.Drawing.Size(104, 25)
        Me.LabelJS.TabIndex = 280
        Me.LabelJS.Text = "5.查询系统"
        '
        'LabelBJ
        '
        Me.LabelBJ.AutoSize = True
        Me.LabelBJ.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelBJ.Font = New System.Drawing.Font("微软雅黑", 14.25!)
        Me.LabelBJ.ForeColor = System.Drawing.Color.White
        Me.LabelBJ.Location = New System.Drawing.Point(12, 9)
        Me.LabelBJ.Name = "LabelBJ"
        Me.LabelBJ.Size = New System.Drawing.Size(104, 25)
        Me.LabelBJ.TabIndex = 279
        Me.LabelBJ.Text = "1.报价系统"
        '
        'LabelCK
        '
        Me.LabelCK.AutoSize = True
        Me.LabelCK.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelCK.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelCK.ForeColor = System.Drawing.Color.White
        Me.LabelCK.Location = New System.Drawing.Point(342, 9)
        Me.LabelCK.Name = "LabelCK"
        Me.LabelCK.Size = New System.Drawing.Size(104, 25)
        Me.LabelCK.TabIndex = 278
        Me.LabelCK.Text = "4.仓库系统"
        '
        'LabelSH
        '
        Me.LabelSH.AutoSize = True
        Me.LabelSH.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSH.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold)
        Me.LabelSH.ForeColor = System.Drawing.Color.White
        Me.LabelSH.Location = New System.Drawing.Point(232, 9)
        Me.LabelSH.Name = "LabelSH"
        Me.LabelSH.Size = New System.Drawing.Size(105, 26)
        Me.LabelSH.TabIndex = 277
        Me.LabelSH.Text = "3.送货系统"
        '
        'LabelSC
        '
        Me.LabelSC.AutoSize = True
        Me.LabelSC.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSC.Font = New System.Drawing.Font("微软雅黑", 14.25!)
        Me.LabelSC.ForeColor = System.Drawing.Color.White
        Me.LabelSC.Location = New System.Drawing.Point(122, 9)
        Me.LabelSC.Name = "LabelSC"
        Me.LabelSC.Size = New System.Drawing.Size(104, 25)
        Me.LabelSC.TabIndex = 276
        Me.LabelSC.Text = "2.入库系统"
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
        Me.RectangleShape1.Size = New System.Drawing.Size(1273, 47)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1264, 711)
        Me.ShapeContainer1.TabIndex = 290
        Me.ShapeContainer1.TabStop = False
        '
        'LabelScjl
        '
        Me.LabelScjl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelScjl.AutoSize = True
        Me.LabelScjl.BackColor = System.Drawing.Color.Transparent
        Me.LabelScjl.ForeColor = System.Drawing.Color.IndianRed
        Me.LabelScjl.Location = New System.Drawing.Point(604, 511)
        Me.LabelScjl.Name = "LabelScjl"
        Me.LabelScjl.Size = New System.Drawing.Size(53, 12)
        Me.LabelScjl.TabIndex = 294
        Me.LabelScjl.Text = "删除记录"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 511)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 293
        Me.Label1.Text = "部件送货记录："
        '
        'DataGridViewShjl
        '
        Me.DataGridViewShjl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewShjl.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridViewShjl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewShjl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewShjl.Location = New System.Drawing.Point(12, 526)
        Me.DataGridViewShjl.Name = "DataGridViewShjl"
        Me.DataGridViewShjl.RowHeadersVisible = False
        Me.DataGridViewShjl.RowTemplate.Height = 23
        Me.DataGridViewShjl.Size = New System.Drawing.Size(645, 173)
        Me.DataGridViewShjl.TabIndex = 292
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 132)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(645, 376)
        Me.DataGridView1.TabIndex = 291
        '
        'DataGridViewShdxx
        '
        Me.DataGridViewShdxx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewShdxx.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridViewShdxx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewShdxx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewShdxx.Location = New System.Drawing.Point(665, 132)
        Me.DataGridViewShdxx.Name = "DataGridViewShdxx"
        Me.DataGridViewShdxx.RowHeadersVisible = False
        Me.DataGridViewShdxx.RowTemplate.Height = 23
        Me.DataGridViewShdxx.Size = New System.Drawing.Size(587, 567)
        Me.DataGridViewShdxx.TabIndex = 295
        '
        'DateTimePickerShrq
        '
        Me.DateTimePickerShrq.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerShrq.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerShrq.Location = New System.Drawing.Point(158, 81)
        Me.DateTimePickerShrq.Name = "DateTimePickerShrq"
        Me.DateTimePickerShrq.Size = New System.Drawing.Size(82, 21)
        Me.DateTimePickerShrq.TabIndex = 328
        '
        'TextBoxBz
        '
        Me.TextBoxBz.Location = New System.Drawing.Point(811, 81)
        Me.TextBoxBz.Name = "TextBoxBz"
        Me.TextBoxBz.Size = New System.Drawing.Size(131, 21)
        Me.TextBoxBz.TabIndex = 332
        '
        'LabelF
        '
        Me.LabelF.AutoSize = True
        Me.LabelF.Location = New System.Drawing.Point(776, 84)
        Me.LabelF.Name = "LabelF"
        Me.LabelF.Size = New System.Drawing.Size(29, 12)
        Me.LabelF.TabIndex = 338
        Me.LabelF.Text = "备注"
        '
        'TextBoxWwg
        '
        Me.TextBoxWwg.Location = New System.Drawing.Point(710, 81)
        Me.TextBoxWwg.Name = "TextBoxWwg"
        Me.TextBoxWwg.Size = New System.Drawing.Size(60, 21)
        Me.TextBoxWwg.TabIndex = 331
        '
        'LabelD
        '
        Me.LabelD.AutoSize = True
        Me.LabelD.Location = New System.Drawing.Point(550, 84)
        Me.LabelD.Name = "LabelD"
        Me.LabelD.Size = New System.Drawing.Size(41, 12)
        Me.LabelD.TabIndex = 336
        Me.LabelD.Text = "已完工"
        '
        'LabelE
        '
        Me.LabelE.AutoSize = True
        Me.LabelE.Location = New System.Drawing.Point(663, 84)
        Me.LabelE.Name = "LabelE"
        Me.LabelE.Size = New System.Drawing.Size(41, 12)
        Me.LabelE.TabIndex = 337
        Me.LabelE.Text = "未完工"
        '
        'TextBoxYwg
        '
        Me.TextBoxYwg.Location = New System.Drawing.Point(597, 81)
        Me.TextBoxYwg.Name = "TextBoxYwg"
        Me.TextBoxYwg.Size = New System.Drawing.Size(60, 21)
        Me.TextBoxYwg.TabIndex = 330
        '
        'TextBoxShsl
        '
        Me.TextBoxShsl.Location = New System.Drawing.Point(484, 81)
        Me.TextBoxShsl.Name = "TextBoxShsl"
        Me.TextBoxShsl.Size = New System.Drawing.Size(60, 21)
        Me.TextBoxShsl.TabIndex = 329
        '
        'LabelB
        '
        Me.LabelB.AutoSize = True
        Me.LabelB.Location = New System.Drawing.Point(99, 84)
        Me.LabelB.Name = "LabelB"
        Me.LabelB.Size = New System.Drawing.Size(53, 12)
        Me.LabelB.TabIndex = 333
        Me.LabelB.Text = "送货日期"
        '
        'LabelA
        '
        Me.LabelA.AutoSize = True
        Me.LabelA.Location = New System.Drawing.Point(246, 84)
        Me.LabelA.Name = "LabelA"
        Me.LabelA.Size = New System.Drawing.Size(59, 12)
        Me.LabelA.TabIndex = 335
        Me.LabelA.Text = "型号/图号"
        '
        'LabelC
        '
        Me.LabelC.AutoSize = True
        Me.LabelC.Location = New System.Drawing.Point(425, 84)
        Me.LabelC.Name = "LabelC"
        Me.LabelC.Size = New System.Drawing.Size(53, 12)
        Me.LabelC.TabIndex = 334
        Me.LabelC.Text = "送货数量"
        '
        'TextBoxXh
        '
        Me.TextBoxXh.Location = New System.Drawing.Point(311, 81)
        Me.TextBoxXh.Name = "TextBoxXh"
        Me.TextBoxXh.Size = New System.Drawing.Size(108, 21)
        Me.TextBoxXh.TabIndex = 327
        '
        'ButtonC
        '
        Me.ButtonC.Location = New System.Drawing.Point(1122, 79)
        Me.ButtonC.Name = "ButtonC"
        Me.ButtonC.Size = New System.Drawing.Size(81, 23)
        Me.ButtonC.TabIndex = 340
        Me.ButtonC.Text = "打开图纸"
        Me.ButtonC.UseVisualStyleBackColor = True
        '
        'ButtonA
        '
        Me.ButtonA.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonA.Location = New System.Drawing.Point(948, 79)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(81, 23)
        Me.ButtonA.TabIndex = 341
        Me.ButtonA.Text = "图/型号搜索"
        Me.ButtonA.UseVisualStyleBackColor = True
        '
        'ButtonB
        '
        Me.ButtonB.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonB.Location = New System.Drawing.Point(1035, 79)
        Me.ButtonB.Name = "ButtonB"
        Me.ButtonB.Size = New System.Drawing.Size(81, 23)
        Me.ButtonB.TabIndex = 342
        Me.ButtonB.Text = "送货出库"
        Me.ButtonB.UseVisualStyleBackColor = True
        '
        'LabelShrq
        '
        Me.LabelShrq.AutoSize = True
        Me.LabelShrq.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabelShrq.Location = New System.Drawing.Point(663, 117)
        Me.LabelShrq.Name = "LabelShrq"
        Me.LabelShrq.Size = New System.Drawing.Size(155, 12)
        Me.LabelShrq.TabIndex = 343
        Me.LabelShrq.Text = "客户-2018-03-16送货单详情"
        '
        'LabelShxqsc
        '
        Me.LabelShxqsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelShxqsc.AutoSize = True
        Me.LabelShxqsc.BackColor = System.Drawing.Color.Transparent
        Me.LabelShxqsc.ForeColor = System.Drawing.Color.IndianRed
        Me.LabelShxqsc.Location = New System.Drawing.Point(1199, 117)
        Me.LabelShxqsc.Name = "LabelShxqsc"
        Me.LabelShxqsc.Size = New System.Drawing.Size(53, 12)
        Me.LabelShxqsc.TabIndex = 344
        Me.LabelShxqsc.Text = "删除记录"
        '
        'LabelDdxx
        '
        Me.LabelDdxx.AutoSize = True
        Me.LabelDdxx.Location = New System.Drawing.Point(15, 117)
        Me.LabelDdxx.Name = "LabelDdxx"
        Me.LabelDdxx.Size = New System.Drawing.Size(281, 12)
        Me.LabelDdxx.TabIndex = 345
        Me.LabelDdxx.Text = "所有在生产订单 - 请选择订单以开始送货单编辑 >>"
        '
        'LabelShdToExcel
        '
        Me.LabelShdToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelShdToExcel.AutoSize = True
        Me.LabelShdToExcel.ForeColor = System.Drawing.Color.Teal
        Me.LabelShdToExcel.Location = New System.Drawing.Point(1128, 117)
        Me.LabelShdToExcel.Name = "LabelShdToExcel"
        Me.LabelShdToExcel.Size = New System.Drawing.Size(65, 12)
        Me.LabelShdToExcel.TabIndex = 346
        Me.LabelShdToExcel.Text = "导出到表格"
        '
        'OpenFileDialogPicture
        '
        Me.OpenFileDialogPicture.FileName = "OpenFileDialog1"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'ButtonD
        '
        Me.ButtonD.Location = New System.Drawing.Point(99, 79)
        Me.ButtonD.Name = "ButtonD"
        Me.ButtonD.Size = New System.Drawing.Size(81, 23)
        Me.ButtonD.TabIndex = 347
        Me.ButtonD.Text = "查询送货单"
        Me.ButtonD.UseVisualStyleBackColor = True
        '
        'ButtonE
        '
        Me.ButtonE.Location = New System.Drawing.Point(186, 79)
        Me.ButtonE.Name = "ButtonE"
        Me.ButtonE.Size = New System.Drawing.Size(81, 23)
        Me.ButtonE.TabIndex = 348
        Me.ButtonE.Text = "订单已完成"
        Me.ButtonE.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBox1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(1054, 12)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox1.TabIndex = 349
        Me.CheckBox1.Text = "显示所有订单"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'LabelSX
        '
        Me.LabelSX.AutoSize = True
        Me.LabelSX.Location = New System.Drawing.Point(351, 84)
        Me.LabelSX.Name = "LabelSX"
        Me.LabelSX.Size = New System.Drawing.Size(113, 12)
        Me.LabelSX.TabIndex = 352
        Me.LabelSX.Text = "输入关键字搜索订单"
        '
        'TextBoxSX
        '
        Me.TextBoxSX.Location = New System.Drawing.Point(470, 81)
        Me.TextBoxSX.Name = "TextBoxSX"
        Me.TextBoxSX.Size = New System.Drawing.Size(100, 21)
        Me.TextBoxSX.TabIndex = 351
        '
        'ButtonSX
        '
        Me.ButtonSX.Location = New System.Drawing.Point(576, 79)
        Me.ButtonSX.Name = "ButtonSX"
        Me.ButtonSX.Size = New System.Drawing.Size(81, 23)
        Me.ButtonSX.TabIndex = 350
        Me.ButtonSX.Text = "筛选订单"
        Me.ButtonSX.UseVisualStyleBackColor = True
        '
        'FormNSH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 711)
        Me.Controls.Add(Me.LabelSX)
        Me.Controls.Add(Me.TextBoxSX)
        Me.Controls.Add(Me.ButtonSX)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ButtonE)
        Me.Controls.Add(Me.ButtonD)
        Me.Controls.Add(Me.LabelShdToExcel)
        Me.Controls.Add(Me.LabelDdxx)
        Me.Controls.Add(Me.LabelShxqsc)
        Me.Controls.Add(Me.LabelShrq)
        Me.Controls.Add(Me.ButtonC)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.ButtonB)
        Me.Controls.Add(Me.DateTimePickerShrq)
        Me.Controls.Add(Me.TextBoxBz)
        Me.Controls.Add(Me.LabelF)
        Me.Controls.Add(Me.TextBoxWwg)
        Me.Controls.Add(Me.LabelD)
        Me.Controls.Add(Me.LabelE)
        Me.Controls.Add(Me.TextBoxYwg)
        Me.Controls.Add(Me.TextBoxShsl)
        Me.Controls.Add(Me.LabelB)
        Me.Controls.Add(Me.LabelA)
        Me.Controls.Add(Me.LabelC)
        Me.Controls.Add(Me.TextBoxXh)
        Me.Controls.Add(Me.DataGridViewShdxx)
        Me.Controls.Add(Me.LabelScjl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridViewShjl)
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
        Me.MinimumSize = New System.Drawing.Size(1280, 750)
        Me.Name = "FormNSH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "送货系统"
        CType(Me.DataGridViewShjl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewShdxx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
    Friend WithEvents LabelBack As System.Windows.Forms.Label
    Friend WithEvents CheckBoxQysc As System.Windows.Forms.CheckBox
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents LabelJS As System.Windows.Forms.Label
    Friend WithEvents LabelBJ As System.Windows.Forms.Label
    Friend WithEvents LabelCK As System.Windows.Forms.Label
    Friend WithEvents LabelSH As System.Windows.Forms.Label
    Friend WithEvents LabelSC As System.Windows.Forms.Label
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LabelScjl As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewShjl As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewShdxx As System.Windows.Forms.DataGridView
    Friend WithEvents DateTimePickerShrq As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxBz As System.Windows.Forms.TextBox
    Friend WithEvents LabelF As System.Windows.Forms.Label
    Friend WithEvents TextBoxWwg As System.Windows.Forms.TextBox
    Friend WithEvents LabelD As System.Windows.Forms.Label
    Friend WithEvents LabelE As System.Windows.Forms.Label
    Friend WithEvents TextBoxYwg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxShsl As System.Windows.Forms.TextBox
    Friend WithEvents LabelB As System.Windows.Forms.Label
    Friend WithEvents LabelA As System.Windows.Forms.Label
    Friend WithEvents LabelC As System.Windows.Forms.Label
    Friend WithEvents TextBoxXh As System.Windows.Forms.TextBox
    Friend WithEvents ButtonC As System.Windows.Forms.Button
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents ButtonB As System.Windows.Forms.Button
    Friend WithEvents LabelShrq As System.Windows.Forms.Label
    Friend WithEvents LabelShxqsc As System.Windows.Forms.Label
    Friend WithEvents LabelDdxx As System.Windows.Forms.Label
    Friend WithEvents LabelShdToExcel As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialogPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonD As System.Windows.Forms.Button
    Friend WithEvents ButtonE As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents LabelSX As System.Windows.Forms.Label
    Friend WithEvents TextBoxSX As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSX As System.Windows.Forms.Button
End Class
