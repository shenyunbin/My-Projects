<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F2BJ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F2BJ))
        Me.LabelJS = New System.Windows.Forms.Label()
        Me.LabelBJ = New System.Windows.Forms.Label()
        Me.LabelCK = New System.Windows.Forms.Label()
        Me.LabelSH = New System.Windows.Forms.Label()
        Me.LabelSC = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonC = New System.Windows.Forms.Button()
        Me.ButtonB = New System.Windows.Forms.Button()
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.ButtonE = New System.Windows.Forms.Button()
        Me.ButtonD = New System.Windows.Forms.Button()
        Me.ButtonG = New System.Windows.Forms.Button()
        Me.ButtonF = New System.Windows.Forms.Button()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.CheckBoxQysc = New System.Windows.Forms.CheckBox()
        Me.LabelBack = New System.Windows.Forms.Label()
        Me.OpenFileDialogPicture = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ButtonSX = New System.Windows.Forms.Button()
        Me.TextBoxSX = New System.Windows.Forms.TextBox()
        Me.LabelSX = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.LabelJS.TabIndex = 191
        Me.LabelJS.Text = "5.查询系统"
        '
        'LabelBJ
        '
        Me.LabelBJ.AutoSize = True
        Me.LabelBJ.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelBJ.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelBJ.ForeColor = System.Drawing.Color.White
        Me.LabelBJ.Location = New System.Drawing.Point(16, 11)
        Me.LabelBJ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBJ.Name = "LabelBJ"
        Me.LabelBJ.Size = New System.Drawing.Size(132, 31)
        Me.LabelBJ.TabIndex = 190
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
        Me.LabelCK.TabIndex = 189
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
        Me.LabelSH.TabIndex = 188
        Me.LabelSH.Text = "3.送货系统"
        '
        'LabelSC
        '
        Me.LabelSC.AutoSize = True
        Me.LabelSC.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSC.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSC.ForeColor = System.Drawing.Color.White
        Me.LabelSC.Location = New System.Drawing.Point(163, 11)
        Me.LabelSC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSC.Name = "LabelSC"
        Me.LabelSC.Size = New System.Drawing.Size(130, 31)
        Me.LabelSC.TabIndex = 187
        Me.LabelSC.Text = "2.入库系统"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1685, 889)
        Me.ShapeContainer1.TabIndex = 192
        Me.ShapeContainer1.TabStop = False
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
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(16, 135)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(1653, 500)
        Me.DataGridView1.TabIndex = 193
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(16, 658)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(1653, 216)
        Me.DataGridView2.TabIndex = 194
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 639)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 15)
        Me.Label1.TabIndex = 195
        Me.Label1.Text = "订单/部件/零件详情："
        '
        'ButtonC
        '
        Me.ButtonC.Location = New System.Drawing.Point(360, 99)
        Me.ButtonC.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonC.Name = "ButtonC"
        Me.ButtonC.Size = New System.Drawing.Size(108, 29)
        Me.ButtonC.TabIndex = 198
        Me.ButtonC.Text = "删除零件"
        Me.ButtonC.UseVisualStyleBackColor = True
        '
        'ButtonB
        '
        Me.ButtonB.Location = New System.Drawing.Point(244, 99)
        Me.ButtonB.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonB.Name = "ButtonB"
        Me.ButtonB.Size = New System.Drawing.Size(108, 29)
        Me.ButtonB.TabIndex = 197
        Me.ButtonB.Text = "修改零件"
        Me.ButtonB.UseVisualStyleBackColor = True
        '
        'ButtonA
        '
        Me.ButtonA.Location = New System.Drawing.Point(128, 99)
        Me.ButtonA.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(108, 29)
        Me.ButtonA.TabIndex = 196
        Me.ButtonA.Text = "新增零件"
        Me.ButtonA.UseVisualStyleBackColor = True
        '
        'ButtonE
        '
        Me.ButtonE.Location = New System.Drawing.Point(592, 99)
        Me.ButtonE.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonE.Name = "ButtonE"
        Me.ButtonE.Size = New System.Drawing.Size(108, 29)
        Me.ButtonE.TabIndex = 200
        Me.ButtonE.Text = "打开图片"
        Me.ButtonE.UseVisualStyleBackColor = True
        '
        'ButtonD
        '
        Me.ButtonD.Location = New System.Drawing.Point(476, 99)
        Me.ButtonD.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonD.Name = "ButtonD"
        Me.ButtonD.Size = New System.Drawing.Size(108, 29)
        Me.ButtonD.TabIndex = 199
        Me.ButtonD.Text = "导入图片"
        Me.ButtonD.UseVisualStyleBackColor = True
        '
        'ButtonG
        '
        Me.ButtonG.Location = New System.Drawing.Point(824, 99)
        Me.ButtonG.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonG.Name = "ButtonG"
        Me.ButtonG.Size = New System.Drawing.Size(108, 29)
        Me.ButtonG.TabIndex = 202
        Me.ButtonG.Text = "导出报价单"
        Me.ButtonG.UseVisualStyleBackColor = True
        '
        'ButtonF
        '
        Me.ButtonF.Location = New System.Drawing.Point(708, 99)
        Me.ButtonF.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonF.Name = "ButtonF"
        Me.ButtonF.Size = New System.Drawing.Size(108, 29)
        Me.ButtonF.TabIndex = 201
        Me.ButtonF.Text = "进入生产"
        Me.ButtonF.UseVisualStyleBackColor = True
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.Location = New System.Drawing.Point(16, 66)
        Me.LabelMain.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(126, 20)
        Me.LabelMain.TabIndex = 203
        Me.LabelMain.Text = "订单信息 >>"
        '
        'CheckBoxQysc
        '
        Me.CheckBoxQysc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxQysc.AutoSize = True
        Me.CheckBoxQysc.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBoxQysc.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBoxQysc.ForeColor = System.Drawing.Color.White
        Me.CheckBoxQysc.Location = New System.Drawing.Point(1550, 15)
        Me.CheckBoxQysc.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxQysc.Name = "CheckBoxQysc"
        Me.CheckBoxQysc.Size = New System.Drawing.Size(119, 19)
        Me.CheckBoxQysc.TabIndex = 204
        Me.CheckBoxQysc.Text = "启用删除功能"
        Me.CheckBoxQysc.UseVisualStyleBackColor = False
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
        Me.LabelBack.TabIndex = 205
        Me.LabelBack.Text = "返回上一级"
        '
        'OpenFileDialogPicture
        '
        Me.OpenFileDialogPicture.FileName = "OpenFileDialog1"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'ButtonBack
        '
        Me.ButtonBack.ForeColor = System.Drawing.Color.IndianRed
        Me.ButtonBack.Location = New System.Drawing.Point(12, 99)
        Me.ButtonBack.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(108, 29)
        Me.ButtonBack.TabIndex = 206
        Me.ButtonBack.Text = "返回上一级"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBox1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(1414, 15)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(119, 19)
        Me.CheckBox1.TabIndex = 207
        Me.CheckBox1.Text = "显示所有订单"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'ButtonSX
        '
        Me.ButtonSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSX.Location = New System.Drawing.Point(1561, 99)
        Me.ButtonSX.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSX.Name = "ButtonSX"
        Me.ButtonSX.Size = New System.Drawing.Size(108, 29)
        Me.ButtonSX.TabIndex = 208
        Me.ButtonSX.Text = "筛选订单"
        Me.ButtonSX.UseVisualStyleBackColor = True
        '
        'TextBoxSX
        '
        Me.TextBoxSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSX.Location = New System.Drawing.Point(1420, 101)
        Me.TextBoxSX.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxSX.Name = "TextBoxSX"
        Me.TextBoxSX.Size = New System.Drawing.Size(132, 25)
        Me.TextBoxSX.TabIndex = 209
        '
        'LabelSX
        '
        Me.LabelSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSX.AutoSize = True
        Me.LabelSX.Location = New System.Drawing.Point(1261, 105)
        Me.LabelSX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSX.Name = "LabelSX"
        Me.LabelSX.Size = New System.Drawing.Size(142, 15)
        Me.LabelSX.TabIndex = 210
        Me.LabelSX.Text = "输入关键字搜索订单"
        '
        'FormNBJ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1685, 889)
        Me.Controls.Add(Me.LabelSX)
        Me.Controls.Add(Me.TextBoxSX)
        Me.Controls.Add(Me.ButtonSX)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ButtonBack)
        Me.Controls.Add(Me.LabelBack)
        Me.Controls.Add(Me.CheckBoxQysc)
        Me.Controls.Add(Me.LabelMain)
        Me.Controls.Add(Me.ButtonE)
        Me.Controls.Add(Me.ButtonD)
        Me.Controls.Add(Me.ButtonG)
        Me.Controls.Add(Me.ButtonF)
        Me.Controls.Add(Me.ButtonC)
        Me.Controls.Add(Me.ButtonB)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelJS)
        Me.Controls.Add(Me.LabelBJ)
        Me.Controls.Add(Me.LabelCK)
        Me.Controls.Add(Me.LabelSH)
        Me.Controls.Add(Me.LabelSC)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(1701, 926)
        Me.Name = "FormNBJ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "报价系统"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelJS As System.Windows.Forms.Label
    Friend WithEvents LabelBJ As System.Windows.Forms.Label
    Friend WithEvents LabelCK As System.Windows.Forms.Label
    Friend WithEvents LabelSH As System.Windows.Forms.Label
    Friend WithEvents LabelSC As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonC As System.Windows.Forms.Button
    Friend WithEvents ButtonB As System.Windows.Forms.Button
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents ButtonE As System.Windows.Forms.Button
    Friend WithEvents ButtonD As System.Windows.Forms.Button
    Friend WithEvents ButtonG As System.Windows.Forms.Button
    Friend WithEvents ButtonF As System.Windows.Forms.Button
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents CheckBoxQysc As System.Windows.Forms.CheckBox
    Friend WithEvents LabelBack As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialogPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonSX As System.Windows.Forms.Button
    Friend WithEvents TextBoxSX As System.Windows.Forms.TextBox
    Friend WithEvents LabelSX As System.Windows.Forms.Label
End Class
