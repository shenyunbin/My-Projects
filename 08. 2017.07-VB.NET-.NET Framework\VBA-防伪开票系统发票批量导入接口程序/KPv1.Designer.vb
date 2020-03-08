<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KPv1
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KPv1))
        Me.TextBoxFileAdress = New System.Windows.Forms.TextBox()
        Me.ButtonTxtOut = New System.Windows.Forms.Button()
        Me.LabelFileName = New System.Windows.Forms.Label()
        Me.RichTextBoxInformation = New System.Windows.Forms.RichTextBox()
        Me.OpenFileDialogExcel = New System.Windows.Forms.OpenFileDialog()
        Me.ButtenFileSearch = New System.Windows.Forms.Button()
        Me.SaveFileDialogTxt = New System.Windows.Forms.SaveFileDialog()
        Me.LabelTxtName = New System.Windows.Forms.Label()
        Me.TextBoxTxtAdress = New System.Windows.Forms.TextBox()
        Me.ButtonTxtSave = New System.Windows.Forms.Button()
        Me.LabelCompany = New System.Windows.Forms.Label()
        Me.TextBoxCompanyNum = New System.Windows.Forms.TextBox()
        Me.TextBoxCompany = New System.Windows.Forms.TextBox()
        Me.LabelStatement = New System.Windows.Forms.Label()
        Me.DataGridViewKP = New System.Windows.Forms.DataGridView()
        Me.ButtonXmlOut = New System.Windows.Forms.Button()
        Me.ButtonXmlSave = New System.Windows.Forms.Button()
        Me.TextBoxXmlAdress = New System.Windows.Forms.TextBox()
        Me.LabelXmlName = New System.Windows.Forms.Label()
        Me.SaveFileDialogXml = New System.Windows.Forms.SaveFileDialog()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.ButtonOpenExcel = New System.Windows.Forms.Button()
        Me.LabelInformation = New System.Windows.Forms.Label()
        Me.LabelProSet = New System.Windows.Forms.Label()
        Me.LabelRefresh = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxSpbm = New System.Windows.Forms.TextBox()
        Me.TextBoxSsflbm = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonXjmb = New System.Windows.Forms.Button()
        Me.ButtonOpenTxt = New System.Windows.Forms.Button()
        Me.ButtonOpenXml = New System.Windows.Forms.Button()
        Me.TextBoxSsflbmmc = New System.Windows.Forms.TextBox()
        CType(Me.DataGridViewKP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxFileAdress
        '
        Me.TextBoxFileAdress.Location = New System.Drawing.Point(10, 30)
        Me.TextBoxFileAdress.Name = "TextBoxFileAdress"
        Me.TextBoxFileAdress.Size = New System.Drawing.Size(187, 21)
        Me.TextBoxFileAdress.TabIndex = 1
        '
        'ButtonTxtOut
        '
        Me.ButtonTxtOut.Location = New System.Drawing.Point(10, 281)
        Me.ButtonTxtOut.Name = "ButtonTxtOut"
        Me.ButtonTxtOut.Size = New System.Drawing.Size(187, 23)
        Me.ButtonTxtOut.TabIndex = 2
        Me.ButtonTxtOut.Text = "生成清单导入文件（txt格式）"
        Me.ButtonTxtOut.UseVisualStyleBackColor = True
        '
        'LabelFileName
        '
        Me.LabelFileName.AutoSize = True
        Me.LabelFileName.Location = New System.Drawing.Point(12, 14)
        Me.LabelFileName.Name = "LabelFileName"
        Me.LabelFileName.Size = New System.Drawing.Size(143, 12)
        Me.LabelFileName.TabIndex = 3
        Me.LabelFileName.Text = "需处理的Excel表格路径："
        '
        'RichTextBoxInformation
        '
        Me.RichTextBoxInformation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxInformation.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.RichTextBoxInformation.Location = New System.Drawing.Point(10, 351)
        Me.RichTextBoxInformation.Name = "RichTextBoxInformation"
        Me.RichTextBoxInformation.ReadOnly = True
        Me.RichTextBoxInformation.Size = New System.Drawing.Size(266, 99)
        Me.RichTextBoxInformation.TabIndex = 4
        Me.RichTextBoxInformation.Text = ""
        '
        'OpenFileDialogExcel
        '
        Me.OpenFileDialogExcel.FileName = "OpenFileDialogExcel"
        Me.OpenFileDialogExcel.Filter = "|*xls"
        '
        'ButtenFileSearch
        '
        Me.ButtenFileSearch.Location = New System.Drawing.Point(203, 30)
        Me.ButtenFileSearch.Name = "ButtenFileSearch"
        Me.ButtenFileSearch.Size = New System.Drawing.Size(73, 21)
        Me.ButtenFileSearch.TabIndex = 5
        Me.ButtenFileSearch.Text = "浏览"
        Me.ButtenFileSearch.UseVisualStyleBackColor = True
        '
        'SaveFileDialogTxt
        '
        Me.SaveFileDialogTxt.FileName = "清单导入文件.txt"
        Me.SaveFileDialogTxt.Filter = "|*.txt"
        '
        'LabelTxtName
        '
        Me.LabelTxtName.AutoSize = True
        Me.LabelTxtName.Location = New System.Drawing.Point(12, 54)
        Me.LabelTxtName.Name = "LabelTxtName"
        Me.LabelTxtName.Size = New System.Drawing.Size(107, 12)
        Me.LabelTxtName.TabIndex = 6
        Me.LabelTxtName.Text = "导出Txt文件路径："
        '
        'TextBoxTxtAdress
        '
        Me.TextBoxTxtAdress.Location = New System.Drawing.Point(10, 69)
        Me.TextBoxTxtAdress.Name = "TextBoxTxtAdress"
        Me.TextBoxTxtAdress.Size = New System.Drawing.Size(187, 21)
        Me.TextBoxTxtAdress.TabIndex = 7
        '
        'ButtonTxtSave
        '
        Me.ButtonTxtSave.Location = New System.Drawing.Point(203, 69)
        Me.ButtonTxtSave.Name = "ButtonTxtSave"
        Me.ButtonTxtSave.Size = New System.Drawing.Size(73, 21)
        Me.ButtonTxtSave.TabIndex = 8
        Me.ButtonTxtSave.Text = "浏览"
        Me.ButtonTxtSave.UseVisualStyleBackColor = True
        '
        'LabelCompany
        '
        Me.LabelCompany.AutoSize = True
        Me.LabelCompany.Location = New System.Drawing.Point(12, 132)
        Me.LabelCompany.Name = "LabelCompany"
        Me.LabelCompany.Size = New System.Drawing.Size(77, 12)
        Me.LabelCompany.TabIndex = 9
        Me.LabelCompany.Text = "公司及编码："
        '
        'TextBoxCompanyNum
        '
        Me.TextBoxCompanyNum.Location = New System.Drawing.Point(10, 147)
        Me.TextBoxCompanyNum.Name = "TextBoxCompanyNum"
        Me.TextBoxCompanyNum.Size = New System.Drawing.Size(40, 21)
        Me.TextBoxCompanyNum.TabIndex = 10
        Me.TextBoxCompanyNum.Text = "2"
        '
        'TextBoxCompany
        '
        Me.TextBoxCompany.Location = New System.Drawing.Point(56, 147)
        Me.TextBoxCompany.Name = "TextBoxCompany"
        Me.TextBoxCompany.Size = New System.Drawing.Size(220, 21)
        Me.TextBoxCompany.TabIndex = 11
        Me.TextBoxCompany.Text = "杭机股份"
        '
        'LabelStatement
        '
        Me.LabelStatement.AutoSize = True
        Me.LabelStatement.Location = New System.Drawing.Point(12, 336)
        Me.LabelStatement.Name = "LabelStatement"
        Me.LabelStatement.Size = New System.Drawing.Size(41, 12)
        Me.LabelStatement.TabIndex = 12
        Me.LabelStatement.Text = "状态："
        '
        'DataGridViewKP
        '
        Me.DataGridViewKP.AllowUserToAddRows = False
        Me.DataGridViewKP.AllowUserToDeleteRows = False
        Me.DataGridViewKP.AllowUserToOrderColumns = True
        Me.DataGridViewKP.AllowUserToResizeColumns = False
        Me.DataGridViewKP.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DataGridViewKP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewKP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewKP.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridViewKP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewKP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal
        Me.DataGridViewKP.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewKP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewKP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewKP.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridViewKP.Location = New System.Drawing.Point(282, 29)
        Me.DataGridViewKP.Name = "DataGridViewKP"
        Me.DataGridViewKP.ReadOnly = True
        Me.DataGridViewKP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridViewKP.RowHeadersVisible = False
        Me.DataGridViewKP.RowTemplate.Height = 23
        Me.DataGridViewKP.Size = New System.Drawing.Size(474, 421)
        Me.DataGridViewKP.TabIndex = 13
        '
        'ButtonXmlOut
        '
        Me.ButtonXmlOut.Location = New System.Drawing.Point(10, 310)
        Me.ButtonXmlOut.Name = "ButtonXmlOut"
        Me.ButtonXmlOut.Size = New System.Drawing.Size(187, 23)
        Me.ButtonXmlOut.TabIndex = 15
        Me.ButtonXmlOut.Text = "生成发票导入文件（xml格式）"
        Me.ButtonXmlOut.UseVisualStyleBackColor = True
        '
        'ButtonXmlSave
        '
        Me.ButtonXmlSave.Location = New System.Drawing.Point(203, 108)
        Me.ButtonXmlSave.Name = "ButtonXmlSave"
        Me.ButtonXmlSave.Size = New System.Drawing.Size(73, 21)
        Me.ButtonXmlSave.TabIndex = 18
        Me.ButtonXmlSave.Text = "浏览"
        Me.ButtonXmlSave.UseVisualStyleBackColor = True
        '
        'TextBoxXmlAdress
        '
        Me.TextBoxXmlAdress.Location = New System.Drawing.Point(10, 108)
        Me.TextBoxXmlAdress.Name = "TextBoxXmlAdress"
        Me.TextBoxXmlAdress.Size = New System.Drawing.Size(187, 21)
        Me.TextBoxXmlAdress.TabIndex = 17
        '
        'LabelXmlName
        '
        Me.LabelXmlName.AutoSize = True
        Me.LabelXmlName.Location = New System.Drawing.Point(12, 93)
        Me.LabelXmlName.Name = "LabelXmlName"
        Me.LabelXmlName.Size = New System.Drawing.Size(107, 12)
        Me.LabelXmlName.TabIndex = 16
        Me.LabelXmlName.Text = "导出Xml文件路径："
        '
        'SaveFileDialogXml
        '
        Me.SaveFileDialogXml.FileName = "发票导入文件.xml"
        Me.SaveFileDialogXml.Filter = "|*.xml"
        '
        'LabelVersion
        '
        Me.LabelVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(666, 456)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(89, 12)
        Me.LabelVersion.TabIndex = 19
        Me.LabelVersion.Text = "VERSIONV V12.5"
        '
        'ButtonOpenExcel
        '
        Me.ButtonOpenExcel.Location = New System.Drawing.Point(10, 252)
        Me.ButtonOpenExcel.Name = "ButtonOpenExcel"
        Me.ButtonOpenExcel.Size = New System.Drawing.Size(187, 23)
        Me.ButtonOpenExcel.TabIndex = 20
        Me.ButtonOpenExcel.Text = "打开选定的Excel表格"
        Me.ButtonOpenExcel.UseVisualStyleBackColor = True
        '
        'LabelInformation
        '
        Me.LabelInformation.AutoSize = True
        Me.LabelInformation.Location = New System.Drawing.Point(282, 14)
        Me.LabelInformation.Name = "LabelInformation"
        Me.LabelInformation.Size = New System.Drawing.Size(101, 12)
        Me.LabelInformation.TabIndex = 21
        Me.LabelInformation.Text = "各单位开票信息："
        '
        'LabelProSet
        '
        Me.LabelProSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelProSet.AutoSize = True
        Me.LabelProSet.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LabelProSet.Location = New System.Drawing.Point(12, 456)
        Me.LabelProSet.Name = "LabelProSet"
        Me.LabelProSet.Size = New System.Drawing.Size(53, 12)
        Me.LabelProSet.TabIndex = 22
        Me.LabelProSet.Text = "高级设置"
        '
        'LabelRefresh
        '
        Me.LabelRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelRefresh.AutoSize = True
        Me.LabelRefresh.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LabelRefresh.Location = New System.Drawing.Point(702, 14)
        Me.LabelRefresh.Name = "LabelRefresh"
        Me.LabelRefresh.Size = New System.Drawing.Size(53, 12)
        Me.LabelRefresh.TabIndex = 23
        Me.LabelRefresh.Text = "刷新数据"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 210)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 12)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Xml格式 - 商品（合并）编码："
        '
        'TextBoxSpbm
        '
        Me.TextBoxSpbm.Location = New System.Drawing.Point(10, 225)
        Me.TextBoxSpbm.Name = "TextBoxSpbm"
        Me.TextBoxSpbm.ReadOnly = True
        Me.TextBoxSpbm.Size = New System.Drawing.Size(266, 21)
        Me.TextBoxSpbm.TabIndex = 25
        '
        'TextBoxSsflbm
        '
        Me.TextBoxSsflbm.Location = New System.Drawing.Point(10, 186)
        Me.TextBoxSsflbm.Name = "TextBoxSsflbm"
        Me.TextBoxSsflbm.ReadOnly = True
        Me.TextBoxSsflbm.Size = New System.Drawing.Size(96, 21)
        Me.TextBoxSsflbm.TabIndex = 28
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 171)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(185, 12)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Txt格式 - 税收分类编码及名称："
        '
        'ButtonXjmb
        '
        Me.ButtonXjmb.Location = New System.Drawing.Point(203, 252)
        Me.ButtonXjmb.Name = "ButtonXjmb"
        Me.ButtonXjmb.Size = New System.Drawing.Size(73, 23)
        Me.ButtonXjmb.TabIndex = 29
        Me.ButtonXjmb.Text = "新建模板"
        Me.ButtonXjmb.UseVisualStyleBackColor = True
        '
        'ButtonOpenTxt
        '
        Me.ButtonOpenTxt.Location = New System.Drawing.Point(203, 281)
        Me.ButtonOpenTxt.Name = "ButtonOpenTxt"
        Me.ButtonOpenTxt.Size = New System.Drawing.Size(73, 23)
        Me.ButtonOpenTxt.TabIndex = 30
        Me.ButtonOpenTxt.Text = "打开文件夹"
        Me.ButtonOpenTxt.UseVisualStyleBackColor = True
        '
        'ButtonOpenXml
        '
        Me.ButtonOpenXml.Location = New System.Drawing.Point(203, 310)
        Me.ButtonOpenXml.Name = "ButtonOpenXml"
        Me.ButtonOpenXml.Size = New System.Drawing.Size(73, 23)
        Me.ButtonOpenXml.TabIndex = 31
        Me.ButtonOpenXml.Text = "打开文件夹"
        Me.ButtonOpenXml.UseVisualStyleBackColor = True
        '
        'TextBoxSsflbmmc
        '
        Me.TextBoxSsflbmmc.Location = New System.Drawing.Point(112, 186)
        Me.TextBoxSsflbmmc.Name = "TextBoxSsflbmmc"
        Me.TextBoxSsflbmmc.ReadOnly = True
        Me.TextBoxSsflbmmc.Size = New System.Drawing.Size(164, 21)
        Me.TextBoxSsflbmmc.TabIndex = 32
        '
        'KPv1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 477)
        Me.Controls.Add(Me.TextBoxSsflbmmc)
        Me.Controls.Add(Me.ButtonOpenXml)
        Me.Controls.Add(Me.ButtonOpenTxt)
        Me.Controls.Add(Me.ButtonXjmb)
        Me.Controls.Add(Me.TextBoxSsflbm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxSpbm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelRefresh)
        Me.Controls.Add(Me.LabelProSet)
        Me.Controls.Add(Me.LabelInformation)
        Me.Controls.Add(Me.ButtonOpenExcel)
        Me.Controls.Add(Me.LabelVersion)
        Me.Controls.Add(Me.ButtonXmlSave)
        Me.Controls.Add(Me.TextBoxXmlAdress)
        Me.Controls.Add(Me.LabelXmlName)
        Me.Controls.Add(Me.ButtonXmlOut)
        Me.Controls.Add(Me.DataGridViewKP)
        Me.Controls.Add(Me.LabelStatement)
        Me.Controls.Add(Me.TextBoxCompany)
        Me.Controls.Add(Me.TextBoxCompanyNum)
        Me.Controls.Add(Me.LabelCompany)
        Me.Controls.Add(Me.ButtonTxtSave)
        Me.Controls.Add(Me.TextBoxTxtAdress)
        Me.Controls.Add(Me.LabelTxtName)
        Me.Controls.Add(Me.ButtenFileSearch)
        Me.Controls.Add(Me.RichTextBoxInformation)
        Me.Controls.Add(Me.LabelFileName)
        Me.Controls.Add(Me.ButtonTxtOut)
        Me.Controls.Add(Me.TextBoxFileAdress)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "KPv1"
        Me.Text = "开票辅助程序"
        CType(Me.DataGridViewKP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxFileAdress As System.Windows.Forms.TextBox
    Friend WithEvents ButtonTxtOut As System.Windows.Forms.Button
    Friend WithEvents LabelFileName As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialogExcel As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ButtenFileSearch As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialogTxt As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LabelTxtName As System.Windows.Forms.Label
    Friend WithEvents TextBoxTxtAdress As System.Windows.Forms.TextBox
    Friend WithEvents ButtonTxtSave As System.Windows.Forms.Button
    Friend WithEvents LabelCompany As System.Windows.Forms.Label
    Friend WithEvents TextBoxCompanyNum As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCompany As System.Windows.Forms.TextBox
    Friend WithEvents LabelStatement As System.Windows.Forms.Label
    Friend WithEvents DataGridViewKP As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonXmlOut As System.Windows.Forms.Button
    Friend WithEvents ButtonXmlSave As System.Windows.Forms.Button
    Friend WithEvents TextBoxXmlAdress As System.Windows.Forms.TextBox
    Friend WithEvents LabelXmlName As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialogXml As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents ButtonOpenExcel As System.Windows.Forms.Button
    Friend WithEvents LabelInformation As System.Windows.Forms.Label
    Private WithEvents RichTextBoxInformation As System.Windows.Forms.RichTextBox
    Friend WithEvents LabelProSet As System.Windows.Forms.Label
    Friend WithEvents LabelRefresh As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSpbm As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSsflbm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonXjmb As System.Windows.Forms.Button
    Friend WithEvents ButtonOpenTxt As System.Windows.Forms.Button
    Friend WithEvents ButtonOpenXml As System.Windows.Forms.Button
    Friend WithEvents TextBoxSsflbmmc As System.Windows.Forms.TextBox

End Class
