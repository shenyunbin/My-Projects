<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNCK
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNCK))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridView5 = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.CheckBoxQysc = New System.Windows.Forms.CheckBox()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LabelJS = New System.Windows.Forms.Label()
        Me.LabelBJ = New System.Windows.Forms.Label()
        Me.LabelCK = New System.Windows.Forms.Label()
        Me.LabelSH = New System.Windows.Forms.Label()
        Me.LabelSC = New System.Windows.Forms.Label()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCTh = New System.Windows.Forms.TextBox()
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.ButtonB = New System.Windows.Forms.Button()
        Me.ButtonC = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialogPicture = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(824, 596)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 15)
        Me.Label4.TabIndex = 249
        Me.Label4.Text = "关联的零件信息："
        '
        'DataGridView5
        '
        Me.DataGridView5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView5.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView5.Location = New System.Drawing.Point(827, 615)
        Me.DataGridView5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView5.Name = "DataGridView5"
        Me.DataGridView5.RowHeadersVisible = False
        Me.DataGridView5.RowTemplate.Height = 23
        Me.DataGridView5.Size = New System.Drawing.Size(840, 260)
        Me.DataGridView5.TabIndex = 248
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(827, 358)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 15)
        Me.Label2.TabIndex = 243
        Me.Label2.Text = "部件送货记录："
        '
        'DataGridView3
        '
        Me.DataGridView3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(827, 376)
        Me.DataGridView3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.RowHeadersVisible = False
        Me.DataGridView3.RowTemplate.Height = 23
        Me.DataGridView3.Size = New System.Drawing.Size(840, 216)
        Me.DataGridView3.TabIndex = 242
        '
        'CheckBoxQysc
        '
        Me.CheckBoxQysc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxQysc.AutoSize = True
        Me.CheckBoxQysc.BackColor = System.Drawing.Color.LightSlateGray
        Me.CheckBoxQysc.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.CheckBoxQysc.ForeColor = System.Drawing.Color.White
        Me.CheckBoxQysc.Location = New System.Drawing.Point(1556, 18)
        Me.CheckBoxQysc.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBoxQysc.Name = "CheckBoxQysc"
        Me.CheckBoxQysc.Size = New System.Drawing.Size(116, 19)
        Me.CheckBoxQysc.TabIndex = 239
        Me.CheckBoxQysc.Text = "启用高级功能"
        Me.CheckBoxQysc.UseVisualStyleBackColor = False
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.Location = New System.Drawing.Point(19, 69)
        Me.LabelMain.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(126, 20)
        Me.LabelMain.TabIndex = 238
        Me.LabelMain.Text = "仓库信息 >>"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(827, 119)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 15)
        Me.Label1.TabIndex = 237
        Me.Label1.Text = "毛坯入库记录："
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(827, 138)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(839, 216)
        Me.DataGridView2.TabIndex = 236
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(19, 138)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(800, 455)
        Me.DataGridView1.TabIndex = 235
        '
        'LabelJS
        '
        Me.LabelJS.AutoSize = True
        Me.LabelJS.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelJS.Font = New System.Drawing.Font("微软雅黑", 14.25!)
        Me.LabelJS.ForeColor = System.Drawing.Color.White
        Me.LabelJS.Location = New System.Drawing.Point(602, 11)
        Me.LabelJS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelJS.Name = "LabelJS"
        Me.LabelJS.Size = New System.Drawing.Size(130, 31)
        Me.LabelJS.TabIndex = 234
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
        Me.LabelBJ.TabIndex = 233
        Me.LabelBJ.Text = "1.报价系统"
        '
        'LabelCK
        '
        Me.LabelCK.AutoSize = True
        Me.LabelCK.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelCK.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold)
        Me.LabelCK.ForeColor = System.Drawing.Color.White
        Me.LabelCK.Location = New System.Drawing.Point(456, 11)
        Me.LabelCK.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCK.Name = "LabelCK"
        Me.LabelCK.Size = New System.Drawing.Size(132, 31)
        Me.LabelCK.TabIndex = 232
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
        Me.LabelSH.TabIndex = 231
        Me.LabelSH.Text = "3.送货系统"
        '
        'LabelSC
        '
        Me.LabelSC.AutoSize = True
        Me.LabelSC.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSC.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSC.ForeColor = System.Drawing.Color.White
        Me.LabelSC.Location = New System.Drawing.Point(162, 11)
        Me.LabelSC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSC.Name = "LabelSC"
        Me.LabelSC.Size = New System.Drawing.Size(130, 31)
        Me.LabelSC.TabIndex = 230
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
        Me.RectangleShape1.Size = New System.Drawing.Size(1694, 47)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1685, 889)
        Me.ShapeContainer1.TabIndex = 250
        Me.ShapeContainer1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 108)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 271
        Me.Label3.Text = "零件图号"
        '
        'TextBoxCTh
        '
        Me.TextBoxCTh.Location = New System.Drawing.Point(101, 104)
        Me.TextBoxCTh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxCTh.Name = "TextBoxCTh"
        Me.TextBoxCTh.Size = New System.Drawing.Size(188, 25)
        Me.TextBoxCTh.TabIndex = 269
        '
        'ButtonA
        '
        Me.ButtonA.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonA.Location = New System.Drawing.Point(299, 101)
        Me.ButtonA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(100, 29)
        Me.ButtonA.TabIndex = 270
        Me.ButtonA.Text = "图号搜索"
        Me.ButtonA.UseVisualStyleBackColor = True
        '
        'ButtonB
        '
        Me.ButtonB.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ButtonB.Location = New System.Drawing.Point(407, 101)
        Me.ButtonB.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonB.Name = "ButtonB"
        Me.ButtonB.Size = New System.Drawing.Size(100, 29)
        Me.ButtonB.TabIndex = 272
        Me.ButtonB.Text = "打开图纸"
        Me.ButtonB.UseVisualStyleBackColor = True
        '
        'ButtonC
        '
        Me.ButtonC.Enabled = False
        Me.ButtonC.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ButtonC.Location = New System.Drawing.Point(515, 101)
        Me.ButtonC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonC.Name = "ButtonC"
        Me.ButtonC.Size = New System.Drawing.Size(100, 29)
        Me.ButtonC.TabIndex = 273
        Me.ButtonC.Text = "更改库存"
        Me.ButtonC.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 596)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(127, 15)
        Me.Label5.TabIndex = 275
        Me.Label5.Text = "关联的部件信息："
        '
        'DataGridView4
        '
        Me.DataGridView4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Location = New System.Drawing.Point(19, 615)
        Me.DataGridView4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.RowHeadersVisible = False
        Me.DataGridView4.RowTemplate.Height = 23
        Me.DataGridView4.Size = New System.Drawing.Size(800, 260)
        Me.DataGridView4.TabIndex = 274
        '
        'OpenFileDialogPicture
        '
        Me.OpenFileDialogPicture.FileName = "OpenFileDialog1"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'FormNCK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1685, 889)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DataGridView4)
        Me.Controls.Add(Me.ButtonC)
        Me.Controls.Add(Me.ButtonB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCTh)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DataGridView5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.CheckBoxQysc)
        Me.Controls.Add(Me.LabelMain)
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
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MinimumSize = New System.Drawing.Size(1701, 926)
        Me.Name = "FormNCK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "仓库系统"
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridView5 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBoxQysc As System.Windows.Forms.CheckBox
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelJS As System.Windows.Forms.Label
    Friend WithEvents LabelBJ As System.Windows.Forms.Label
    Friend WithEvents LabelCK As System.Windows.Forms.Label
    Friend WithEvents LabelSH As System.Windows.Forms.Label
    Friend WithEvents LabelSC As System.Windows.Forms.Label
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCTh As System.Windows.Forms.TextBox
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents ButtonB As System.Windows.Forms.Button
    Friend WithEvents ButtonC As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialogPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
End Class
