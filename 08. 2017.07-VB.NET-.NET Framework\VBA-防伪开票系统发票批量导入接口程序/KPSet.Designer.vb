<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KPSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KPSet))
        Me.LabelBz = New System.Windows.Forms.Label()
        Me.TextBoxBz = New System.Windows.Forms.TextBox()
        Me.LabelFhr = New System.Windows.Forms.Label()
        Me.TextBoxFhr = New System.Windows.Forms.TextBox()
        Me.TextBoxSkr = New System.Windows.Forms.TextBox()
        Me.LabelSkr = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewKPSet = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxGfmc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxGfsh = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxYhzh = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxDzdh = New System.Windows.Forms.TextBox()
        Me.ButtonChange = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxQyjc = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.LabelQyid = New System.Windows.Forms.Label()
        Me.TextBoxQyid = New System.Windows.Forms.TextBox()
        Me.ButtonSsflbmgg = New System.Windows.Forms.Button()
        Me.TextBoxSsflbm = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ButtonSpbmgg = New System.Windows.Forms.Button()
        Me.TextBoxSpbm = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBoxSsbmws = New System.Windows.Forms.TextBox()
        Me.TextBoxSpbmws = New System.Windows.Forms.TextBox()
        Me.TextBoxSsflbmmc = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBoxSl = New System.Windows.Forms.TextBox()
        CType(Me.DataGridViewKPSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelBz
        '
        Me.LabelBz.AutoSize = True
        Me.LabelBz.Location = New System.Drawing.Point(322, 32)
        Me.LabelBz.Name = "LabelBz"
        Me.LabelBz.Size = New System.Drawing.Size(29, 12)
        Me.LabelBz.TabIndex = 12
        Me.LabelBz.Text = "备注"
        '
        'TextBoxBz
        '
        Me.TextBoxBz.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBz.Location = New System.Drawing.Point(357, 29)
        Me.TextBoxBz.Name = "TextBoxBz"
        Me.TextBoxBz.Size = New System.Drawing.Size(103, 21)
        Me.TextBoxBz.TabIndex = 11
        '
        'LabelFhr
        '
        Me.LabelFhr.AutoSize = True
        Me.LabelFhr.Location = New System.Drawing.Point(36, 32)
        Me.LabelFhr.Name = "LabelFhr"
        Me.LabelFhr.Size = New System.Drawing.Size(41, 12)
        Me.LabelFhr.TabIndex = 9
        Me.LabelFhr.Text = "复核人"
        '
        'TextBoxFhr
        '
        Me.TextBoxFhr.Location = New System.Drawing.Point(83, 29)
        Me.TextBoxFhr.Name = "TextBoxFhr"
        Me.TextBoxFhr.Size = New System.Drawing.Size(90, 21)
        Me.TextBoxFhr.TabIndex = 7
        '
        'TextBoxSkr
        '
        Me.TextBoxSkr.Location = New System.Drawing.Point(226, 29)
        Me.TextBoxSkr.Name = "TextBoxSkr"
        Me.TextBoxSkr.Size = New System.Drawing.Size(90, 21)
        Me.TextBoxSkr.TabIndex = 8
        '
        'LabelSkr
        '
        Me.LabelSkr.AutoSize = True
        Me.LabelSkr.Location = New System.Drawing.Point(179, 32)
        Me.LabelSkr.Name = "LabelSkr"
        Me.LabelSkr.Size = New System.Drawing.Size(41, 12)
        Me.LabelSkr.TabIndex = 10
        Me.LabelSkr.Text = "收款人"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "其他开票信息更改："
        '
        'DataGridViewKPSet
        '
        Me.DataGridViewKPSet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewKPSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewKPSet.Location = New System.Drawing.Point(14, 345)
        Me.DataGridViewKPSet.Name = "DataGridViewKPSet"
        Me.DataGridViewKPSet.RowTemplate.Height = 23
        Me.DataGridViewKPSet.Size = New System.Drawing.Size(671, 195)
        Me.DataGridViewKPSet.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 215)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "购房名称"
        '
        'TextBoxGfmc
        '
        Me.TextBoxGfmc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxGfmc.Location = New System.Drawing.Point(95, 212)
        Me.TextBoxGfmc.Name = "TextBoxGfmc"
        Me.TextBoxGfmc.Size = New System.Drawing.Size(466, 21)
        Me.TextBoxGfmc.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 242)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "购方税号"
        '
        'TextBoxGfsh
        '
        Me.TextBoxGfsh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxGfsh.Location = New System.Drawing.Point(95, 239)
        Me.TextBoxGfsh.Name = "TextBoxGfsh"
        Me.TextBoxGfsh.Size = New System.Drawing.Size(466, 21)
        Me.TextBoxGfsh.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 269)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "银行账号"
        '
        'TextBoxYhzh
        '
        Me.TextBoxYhzh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxYhzh.Location = New System.Drawing.Point(95, 266)
        Me.TextBoxYhzh.Name = "TextBoxYhzh"
        Me.TextBoxYhzh.Size = New System.Drawing.Size(466, 21)
        Me.TextBoxYhzh.TabIndex = 22
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 296)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "地址电话"
        '
        'TextBoxDzdh
        '
        Me.TextBoxDzdh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDzdh.Location = New System.Drawing.Point(95, 293)
        Me.TextBoxDzdh.Name = "TextBoxDzdh"
        Me.TextBoxDzdh.Size = New System.Drawing.Size(466, 21)
        Me.TextBoxDzdh.TabIndex = 24
        '
        'ButtonChange
        '
        Me.ButtonChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonChange.Location = New System.Drawing.Point(583, 27)
        Me.ButtonChange.Name = "ButtonChange"
        Me.ButtonChange.Size = New System.Drawing.Size(102, 23)
        Me.ButtonChange.TabIndex = 26
        Me.ButtonChange.Text = "修改并保存"
        Me.ButtonChange.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 12)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "各单位开票信息更改："
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.Location = New System.Drawing.Point(583, 183)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(102, 23)
        Me.ButtonAdd.TabIndex = 28
        Me.ButtonAdd.Text = "增加新企业"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDelete.Location = New System.Drawing.Point(583, 242)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(102, 23)
        Me.ButtonDelete.TabIndex = 29
        Me.ButtonDelete.Text = "删除本条"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 188)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "企业简称"
        '
        'TextBoxQyjc
        '
        Me.TextBoxQyjc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxQyjc.Location = New System.Drawing.Point(280, 185)
        Me.TextBoxQyjc.Name = "TextBoxQyjc"
        Me.TextBoxQyjc.Size = New System.Drawing.Size(281, 21)
        Me.TextBoxQyjc.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 325)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(125, 12)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "各单位开票信息预览："
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonUpdate.Location = New System.Drawing.Point(583, 212)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(102, 23)
        Me.ButtonUpdate.TabIndex = 33
        Me.ButtonUpdate.Text = "更新此企业ID"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'LabelQyid
        '
        Me.LabelQyid.AutoSize = True
        Me.LabelQyid.Location = New System.Drawing.Point(36, 188)
        Me.LabelQyid.Name = "LabelQyid"
        Me.LabelQyid.Size = New System.Drawing.Size(41, 12)
        Me.LabelQyid.TabIndex = 35
        Me.LabelQyid.Text = "企业ID"
        '
        'TextBoxQyid
        '
        Me.TextBoxQyid.Location = New System.Drawing.Point(95, 185)
        Me.TextBoxQyid.Name = "TextBoxQyid"
        Me.TextBoxQyid.Size = New System.Drawing.Size(120, 21)
        Me.TextBoxQyid.TabIndex = 34
        '
        'ButtonSsflbmgg
        '
        Me.ButtonSsflbmgg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSsflbmgg.Location = New System.Drawing.Point(583, 78)
        Me.ButtonSsflbmgg.Name = "ButtonSsflbmgg"
        Me.ButtonSsflbmgg.Size = New System.Drawing.Size(102, 23)
        Me.ButtonSsflbmgg.TabIndex = 41
        Me.ButtonSsflbmgg.Text = "保存更改"
        Me.ButtonSsflbmgg.UseVisualStyleBackColor = True
        '
        'TextBoxSsflbm
        '
        Me.TextBoxSsflbm.Location = New System.Drawing.Point(95, 80)
        Me.TextBoxSsflbm.Name = "TextBoxSsflbm"
        Me.TextBoxSsflbm.Size = New System.Drawing.Size(108, 21)
        Me.TextBoxSsflbm.TabIndex = 40
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 59)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(173, 12)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Txt格式 - 税收分类编码更改："
        '
        'ButtonSpbmgg
        '
        Me.ButtonSpbmgg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSpbmgg.Location = New System.Drawing.Point(583, 129)
        Me.ButtonSpbmgg.Name = "ButtonSpbmgg"
        Me.ButtonSpbmgg.Size = New System.Drawing.Size(102, 23)
        Me.ButtonSpbmgg.TabIndex = 38
        Me.ButtonSpbmgg.Text = "保存更改"
        Me.ButtonSpbmgg.UseVisualStyleBackColor = True
        '
        'TextBoxSpbm
        '
        Me.TextBoxSpbm.Location = New System.Drawing.Point(95, 131)
        Me.TextBoxSpbm.Name = "TextBoxSpbm"
        Me.TextBoxSpbm.Size = New System.Drawing.Size(281, 21)
        Me.TextBoxSpbm.TabIndex = 37
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 110)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(197, 12)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "Xml格式 - 商品（合并）编码更改："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(36, 83)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "税收编码"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(36, 134)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "商品编码"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(382, 83)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "编码位数"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(382, 134)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "编码位数"
        '
        'TextBoxSsbmws
        '
        Me.TextBoxSsbmws.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSsbmws.Location = New System.Drawing.Point(441, 80)
        Me.TextBoxSsbmws.Name = "TextBoxSsbmws"
        Me.TextBoxSsbmws.ReadOnly = True
        Me.TextBoxSsbmws.Size = New System.Drawing.Size(120, 21)
        Me.TextBoxSsbmws.TabIndex = 46
        '
        'TextBoxSpbmws
        '
        Me.TextBoxSpbmws.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSpbmws.Location = New System.Drawing.Point(441, 131)
        Me.TextBoxSpbmws.Name = "TextBoxSpbmws"
        Me.TextBoxSpbmws.ReadOnly = True
        Me.TextBoxSpbmws.Size = New System.Drawing.Size(120, 21)
        Me.TextBoxSpbmws.TabIndex = 47
        '
        'TextBoxSsflbmmc
        '
        Me.TextBoxSsflbmmc.Location = New System.Drawing.Point(244, 80)
        Me.TextBoxSsflbmmc.Name = "TextBoxSsflbmmc"
        Me.TextBoxSsflbmmc.Size = New System.Drawing.Size(132, 21)
        Me.TextBoxSsflbmmc.TabIndex = 48
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(209, 83)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 12)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "名称"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(466, 32)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 12)
        Me.Label16.TabIndex = 51
        Me.Label16.Text = "税率"
        '
        'TextBoxSl
        '
        Me.TextBoxSl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSl.Location = New System.Drawing.Point(501, 29)
        Me.TextBoxSl.Name = "TextBoxSl"
        Me.TextBoxSl.Size = New System.Drawing.Size(60, 21)
        Me.TextBoxSl.TabIndex = 50
        '
        'KPSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 552)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TextBoxSl)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TextBoxSsflbmmc)
        Me.Controls.Add(Me.TextBoxSpbmws)
        Me.Controls.Add(Me.TextBoxSsbmws)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.ButtonSsflbmgg)
        Me.Controls.Add(Me.TextBoxSsflbm)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ButtonSpbmgg)
        Me.Controls.Add(Me.TextBoxSpbm)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.LabelQyid)
        Me.Controls.Add(Me.TextBoxQyid)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxQyjc)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ButtonChange)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxDzdh)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxYhzh)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxGfsh)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxGfmc)
        Me.Controls.Add(Me.DataGridViewKPSet)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelBz)
        Me.Controls.Add(Me.TextBoxBz)
        Me.Controls.Add(Me.LabelFhr)
        Me.Controls.Add(Me.TextBoxFhr)
        Me.Controls.Add(Me.TextBoxSkr)
        Me.Controls.Add(Me.LabelSkr)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "KPSet"
        Me.Text = "开票辅助程序-高级设置"
        CType(Me.DataGridViewKPSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelBz As System.Windows.Forms.Label
    Friend WithEvents TextBoxBz As System.Windows.Forms.TextBox
    Friend WithEvents LabelFhr As System.Windows.Forms.Label
    Friend WithEvents TextBoxFhr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSkr As System.Windows.Forms.TextBox
    Friend WithEvents LabelSkr As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewKPSet As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxGfmc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxGfsh As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYhzh As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDzdh As System.Windows.Forms.TextBox
    Friend WithEvents ButtonChange As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxQyjc As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents LabelQyid As System.Windows.Forms.Label
    Friend WithEvents TextBoxQyid As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSsflbmgg As System.Windows.Forms.Button
    Friend WithEvents TextBoxSsflbm As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ButtonSpbmgg As System.Windows.Forms.Button
    Friend WithEvents TextBoxSpbm As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSsbmws As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSpbmws As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSsflbmmc As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSl As System.Windows.Forms.TextBox
End Class
