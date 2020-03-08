<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNBJ_DDBJ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNBJ_DDBJ))
        Me.LabelQcsj1 = New System.Windows.Forms.Label()
        Me.ComboBoxZt = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxDdbh = New System.Windows.Forms.TextBox()
        Me.ComboBoxKh = New System.Windows.Forms.ComboBox()
        Me.TextBoxBz = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxPh = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxDdh = New System.Windows.Forms.TextBox()
        Me.LabelInfo = New System.Windows.Forms.Label()
        Me.ButtonXjdd = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelQcsj1
        '
        Me.LabelQcsj1.AutoSize = True
        Me.LabelQcsj1.Location = New System.Drawing.Point(274, 11)
        Me.LabelQcsj1.Name = "LabelQcsj1"
        Me.LabelQcsj1.Size = New System.Drawing.Size(77, 12)
        Me.LabelQcsj1.TabIndex = 199
        Me.LabelQcsj1.Text = "清空输入数据"
        '
        'ComboBoxZt
        '
        Me.ComboBoxZt.FormattingEnabled = True
        Me.ComboBoxZt.Location = New System.Drawing.Point(288, 90)
        Me.ComboBoxZt.Name = "ComboBoxZt"
        Me.ComboBoxZt.Size = New System.Drawing.Size(61, 20)
        Me.ComboBoxZt.TabIndex = 191
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(253, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 198
        Me.Label3.Text = "状态"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 197
        Me.Label1.Text = "编号"
        '
        'TextBoxDdbh
        '
        Me.TextBoxDdbh.Location = New System.Drawing.Point(48, 34)
        Me.TextBoxDdbh.Name = "TextBoxDdbh"
        Me.TextBoxDdbh.Size = New System.Drawing.Size(80, 21)
        Me.TextBoxDdbh.TabIndex = 186
        '
        'ComboBoxKh
        '
        Me.ComboBoxKh.FormattingEnabled = True
        Me.ComboBoxKh.Location = New System.Drawing.Point(48, 61)
        Me.ComboBoxKh.Name = "ComboBoxKh"
        Me.ComboBoxKh.Size = New System.Drawing.Size(199, 20)
        Me.ComboBoxKh.TabIndex = 188
        '
        'TextBoxBz
        '
        Me.TextBoxBz.Location = New System.Drawing.Point(48, 87)
        Me.TextBoxBz.Name = "TextBoxBz"
        Me.TextBoxBz.Size = New System.Drawing.Size(199, 21)
        Me.TextBoxBz.TabIndex = 190
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(13, 90)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(29, 12)
        Me.Label29.TabIndex = 196
        Me.Label29.Text = "备注"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(253, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 195
        Me.Label9.Text = "批号"
        '
        'TextBoxPh
        '
        Me.TextBoxPh.Location = New System.Drawing.Point(288, 64)
        Me.TextBoxPh.Name = "TextBoxPh"
        Me.TextBoxPh.Size = New System.Drawing.Size(61, 21)
        Me.TextBoxPh.TabIndex = 189
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 194
        Me.Label8.Text = "客户"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(134, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 193
        Me.Label6.Text = "订单"
        '
        'TextBoxDdh
        '
        Me.TextBoxDdh.Location = New System.Drawing.Point(169, 34)
        Me.TextBoxDdh.Name = "TextBoxDdh"
        Me.TextBoxDdh.Size = New System.Drawing.Size(180, 21)
        Me.TextBoxDdh.TabIndex = 187
        '
        'LabelInfo
        '
        Me.LabelInfo.AutoSize = True
        Me.LabelInfo.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelInfo.Location = New System.Drawing.Point(12, 9)
        Me.LabelInfo.Name = "LabelInfo"
        Me.LabelInfo.Size = New System.Drawing.Size(112, 14)
        Me.LabelInfo.TabIndex = 192
        Me.LabelInfo.Text = "编辑订单信息："
        '
        'ButtonXjdd
        '
        Me.ButtonXjdd.Location = New System.Drawing.Point(249, 119)
        Me.ButtonXjdd.Name = "ButtonXjdd"
        Me.ButtonXjdd.Size = New System.Drawing.Size(100, 23)
        Me.ButtonXjdd.TabIndex = 200
        Me.ButtonXjdd.Text = "新增订单"
        Me.ButtonXjdd.UseVisualStyleBackColor = True
        '
        'FormNBJ_DDBJ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 154)
        Me.Controls.Add(Me.ButtonXjdd)
        Me.Controls.Add(Me.LabelQcsj1)
        Me.Controls.Add(Me.ComboBoxZt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxDdbh)
        Me.Controls.Add(Me.ComboBoxKh)
        Me.Controls.Add(Me.TextBoxBz)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxPh)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxDdh)
        Me.Controls.Add(Me.LabelInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormNBJ_DDBJ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "报价系统-订单编辑"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelQcsj1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxZt As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDdbh As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxKh As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxBz As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPh As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDdh As System.Windows.Forms.TextBox
    Friend WithEvents LabelInfo As System.Windows.Forms.Label
    Friend WithEvents ButtonXjdd As System.Windows.Forms.Button
End Class
