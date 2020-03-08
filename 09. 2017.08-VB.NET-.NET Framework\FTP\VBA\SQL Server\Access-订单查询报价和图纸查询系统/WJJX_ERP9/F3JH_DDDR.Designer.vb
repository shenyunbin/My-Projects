<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F3JH_DDDR
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
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBoxKh = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxDdh = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxJhrq = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxBz = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.BackColor = System.Drawing.SystemColors.Control
        Me.LabelMain.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.ForeColor = System.Drawing.Color.LightSlateGray
        Me.LabelMain.Location = New System.Drawing.Point(12, 9)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(351, 26)
        Me.LabelMain.TabIndex = 215
        Me.LabelMain.Text = "计划 - 导入报价系统订单到计划系统 >>"
        '
        'ButtonBack
        '
        Me.ButtonBack.BackColor = System.Drawing.Color.White
        Me.ButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonBack.FlatAppearance.BorderSize = 0
        Me.ButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonBack.Font = New System.Drawing.Font("微软雅黑", 11.0!)
        Me.ButtonBack.ForeColor = System.Drawing.Color.IndianRed
        Me.ButtonBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonBack.Location = New System.Drawing.Point(12, 47)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(93, 29)
        Me.ButtonBack.TabIndex = 232
        Me.ButtonBack.Text = "返回上一级"
        Me.ButtonBack.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonBack.UseVisualStyleBackColor = False
        '
        'ButtonA
        '
        Me.ButtonA.BackColor = System.Drawing.Color.White
        Me.ButtonA.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonA.FlatAppearance.BorderSize = 0
        Me.ButtonA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonA.Font = New System.Drawing.Font("微软雅黑", 11.0!)
        Me.ButtonA.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonA.Location = New System.Drawing.Point(111, 47)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(81, 29)
        Me.ButtonA.TabIndex = 231
        Me.ButtonA.Text = "导入订单"
        Me.ButtonA.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonA.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 118)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(877, 474)
        Me.DataGridView1.TabIndex = 230
        '
        'TextBoxKh
        '
        Me.TextBoxKh.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxKh.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxKh.Location = New System.Drawing.Point(54, 91)
        Me.TextBoxKh.Name = "TextBoxKh"
        Me.TextBoxKh.Size = New System.Drawing.Size(100, 18)
        Me.TextBoxKh.TabIndex = 233
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 19)
        Me.Label1.TabIndex = 234
        Me.Label1.Text = "客户"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(160, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 19)
        Me.Label2.TabIndex = 236
        Me.Label2.Text = "订单号"
        '
        'TextBoxDdh
        '
        Me.TextBoxDdh.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxDdh.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxDdh.Location = New System.Drawing.Point(214, 90)
        Me.TextBoxDdh.Name = "TextBoxDdh"
        Me.TextBoxDdh.Size = New System.Drawing.Size(135, 18)
        Me.TextBoxDdh.TabIndex = 235
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(355, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 19)
        Me.Label3.TabIndex = 238
        Me.Label3.Text = "计划日期"
        '
        'TextBoxJhrq
        '
        Me.TextBoxJhrq.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxJhrq.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxJhrq.Location = New System.Drawing.Point(421, 91)
        Me.TextBoxJhrq.Name = "TextBoxJhrq"
        Me.TextBoxJhrq.Size = New System.Drawing.Size(100, 18)
        Me.TextBoxJhrq.TabIndex = 237
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(527, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 19)
        Me.Label4.TabIndex = 240
        Me.Label4.Text = "备注"
        '
        'TextBoxBz
        '
        Me.TextBoxBz.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxBz.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxBz.Location = New System.Drawing.Point(568, 90)
        Me.TextBoxBz.Name = "TextBoxBz"
        Me.TextBoxBz.Size = New System.Drawing.Size(160, 18)
        Me.TextBoxBz.TabIndex = 239
        '
        'FCJH_DDDR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 604)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxBz)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxJhrq)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxDdh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxKh)
        Me.Controls.Add(Me.ButtonBack)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelMain)
        Me.Name = "FCJH_DDDR"
        Me.Text = "FCJH_DDDR"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxKh As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDdh As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxJhrq As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBz As System.Windows.Forms.TextBox
End Class
