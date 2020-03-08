<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNCX
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNCX))
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.LabelJS = New System.Windows.Forms.Label()
        Me.LabelBJ = New System.Windows.Forms.Label()
        Me.LabelCK = New System.Windows.Forms.Label()
        Me.LabelSH = New System.Windows.Forms.Label()
        Me.LabelSC = New System.Windows.Forms.Label()
        Me.ButtonDcssjg = New System.Windows.Forms.Button()
        Me.ComboBoxSslx = New System.Windows.Forms.ComboBox()
        Me.ButtonSs = New System.Windows.Forms.Button()
        Me.TextBoxSsnr = New System.Windows.Forms.TextBox()
        Me.DataGridViewSsjg = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialogPicture = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        CType(Me.DataGridViewSsjg, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RectangleShape1.Size = New System.Drawing.Size(1273, 47)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1264, 711)
        Me.ShapeContainer1.TabIndex = 219
        Me.ShapeContainer1.TabStop = False
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.Location = New System.Drawing.Point(12, 53)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(222, 16)
        Me.LabelMain.TabIndex = 244
        Me.LabelMain.Text = "选择查询类别以开始查询 >>"
        '
        'LabelJS
        '
        Me.LabelJS.AutoSize = True
        Me.LabelJS.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelJS.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold)
        Me.LabelJS.ForeColor = System.Drawing.Color.White
        Me.LabelJS.Location = New System.Drawing.Point(452, 9)
        Me.LabelJS.Name = "LabelJS"
        Me.LabelJS.Size = New System.Drawing.Size(105, 26)
        Me.LabelJS.TabIndex = 243
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
        Me.LabelBJ.TabIndex = 242
        Me.LabelBJ.Text = "1.报价系统"
        '
        'LabelCK
        '
        Me.LabelCK.AutoSize = True
        Me.LabelCK.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelCK.Font = New System.Drawing.Font("微软雅黑", 14.25!)
        Me.LabelCK.ForeColor = System.Drawing.Color.White
        Me.LabelCK.Location = New System.Drawing.Point(342, 9)
        Me.LabelCK.Name = "LabelCK"
        Me.LabelCK.Size = New System.Drawing.Size(104, 25)
        Me.LabelCK.TabIndex = 241
        Me.LabelCK.Text = "4.仓库系统"
        '
        'LabelSH
        '
        Me.LabelSH.AutoSize = True
        Me.LabelSH.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSH.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSH.ForeColor = System.Drawing.Color.White
        Me.LabelSH.Location = New System.Drawing.Point(232, 9)
        Me.LabelSH.Name = "LabelSH"
        Me.LabelSH.Size = New System.Drawing.Size(104, 25)
        Me.LabelSH.TabIndex = 240
        Me.LabelSH.Text = "3.送货系统"
        '
        'LabelSC
        '
        Me.LabelSC.AutoSize = True
        Me.LabelSC.BackColor = System.Drawing.Color.LightSlateGray
        Me.LabelSC.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSC.ForeColor = System.Drawing.Color.White
        Me.LabelSC.Location = New System.Drawing.Point(122, 9)
        Me.LabelSC.Name = "LabelSC"
        Me.LabelSC.Size = New System.Drawing.Size(104, 25)
        Me.LabelSC.TabIndex = 239
        Me.LabelSC.Text = "2.入库系统"
        '
        'ButtonDcssjg
        '
        Me.ButtonDcssjg.Location = New System.Drawing.Point(537, 77)
        Me.ButtonDcssjg.Name = "ButtonDcssjg"
        Me.ButtonDcssjg.Size = New System.Drawing.Size(115, 23)
        Me.ButtonDcssjg.TabIndex = 249
        Me.ButtonDcssjg.Text = "导出搜索结果"
        Me.ButtonDcssjg.UseVisualStyleBackColor = True
        '
        'ComboBoxSslx
        '
        Me.ComboBoxSslx.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxSslx.DropDownHeight = 200
        Me.ComboBoxSslx.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxSslx.FormattingEnabled = True
        Me.ComboBoxSslx.IntegralHeight = False
        Me.ComboBoxSslx.ItemHeight = 12
        Me.ComboBoxSslx.Location = New System.Drawing.Point(12, 79)
        Me.ComboBoxSslx.MaxDropDownItems = 20
        Me.ComboBoxSslx.Name = "ComboBoxSslx"
        Me.ComboBoxSslx.Size = New System.Drawing.Size(152, 20)
        Me.ComboBoxSslx.TabIndex = 246
        '
        'ButtonSs
        '
        Me.ButtonSs.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonSs.Location = New System.Drawing.Point(416, 77)
        Me.ButtonSs.Name = "ButtonSs"
        Me.ButtonSs.Size = New System.Drawing.Size(115, 23)
        Me.ButtonSs.TabIndex = 248
        Me.ButtonSs.Text = "历史记录搜索"
        Me.ButtonSs.UseVisualStyleBackColor = True
        '
        'TextBoxSsnr
        '
        Me.TextBoxSsnr.Location = New System.Drawing.Point(170, 79)
        Me.TextBoxSsnr.Name = "TextBoxSsnr"
        Me.TextBoxSsnr.Size = New System.Drawing.Size(240, 21)
        Me.TextBoxSsnr.TabIndex = 247
        '
        'DataGridViewSsjg
        '
        Me.DataGridViewSsjg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewSsjg.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridViewSsjg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewSsjg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSsjg.Location = New System.Drawing.Point(12, 106)
        Me.DataGridViewSsjg.Name = "DataGridViewSsjg"
        Me.DataGridViewSsjg.RowTemplate.Height = 23
        Me.DataGridViewSsjg.Size = New System.Drawing.Size(1240, 593)
        Me.DataGridViewSsjg.TabIndex = 250
        '
        'OpenFileDialogPicture
        '
        Me.OpenFileDialogPicture.FileName = "OpenFileDialog1"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'FormNCX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 711)
        Me.Controls.Add(Me.DataGridViewSsjg)
        Me.Controls.Add(Me.ButtonDcssjg)
        Me.Controls.Add(Me.ComboBoxSslx)
        Me.Controls.Add(Me.ButtonSs)
        Me.Controls.Add(Me.TextBoxSsnr)
        Me.Controls.Add(Me.LabelMain)
        Me.Controls.Add(Me.LabelJS)
        Me.Controls.Add(Me.LabelBJ)
        Me.Controls.Add(Me.LabelCK)
        Me.Controls.Add(Me.LabelSH)
        Me.Controls.Add(Me.LabelSC)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1280, 750)
        Me.Name = "FormNCX"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "查询系统"
        CType(Me.DataGridViewSsjg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents LabelJS As System.Windows.Forms.Label
    Friend WithEvents LabelBJ As System.Windows.Forms.Label
    Friend WithEvents LabelCK As System.Windows.Forms.Label
    Friend WithEvents LabelSH As System.Windows.Forms.Label
    Friend WithEvents LabelSC As System.Windows.Forms.Label
    Friend WithEvents ButtonDcssjg As System.Windows.Forms.Button
    Friend WithEvents ComboBoxSslx As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonSs As System.Windows.Forms.Button
    Friend WithEvents TextBoxSsnr As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewSsjg As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialogPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
End Class
