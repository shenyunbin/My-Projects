<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F3JH_FP
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
        Me.ButtonA = New System.Windows.Forms.Button()
        Me.LabelMain = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxRq = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxTh = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxDdh = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxKh = New System.Windows.Forms.TextBox()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.LabelZgs = New System.Windows.Forms.Label()
        Me.LabelShr = New System.Windows.Forms.Label()
        Me.TextBoxShr = New System.Windows.Forms.TextBox()
        Me.LabelFzr = New System.Windows.Forms.Label()
        Me.TextBoxFzr = New System.Windows.Forms.TextBox()
        Me.LabelSl = New System.Windows.Forms.Label()
        Me.TextBoxSl = New System.Windows.Forms.TextBox()
        Me.TextBoxZgs = New System.Windows.Forms.TextBox()
        Me.LabelBz = New System.Windows.Forms.Label()
        Me.TextBoxBz = New System.Windows.Forms.TextBox()
        Me.ComboBoxFP = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LabelSM = New System.Windows.Forms.Label()
        Me.ButtonB = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxJhs = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonA
        '
        Me.ButtonA.BackColor = System.Drawing.Color.White
        Me.ButtonA.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonA.FlatAppearance.BorderSize = 0
        Me.ButtonA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonA.Font = New System.Drawing.Font("微软雅黑", 11.0!)
        Me.ButtonA.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonA.Location = New System.Drawing.Point(143, 47)
        Me.ButtonA.Name = "ButtonA"
        Me.ButtonA.Size = New System.Drawing.Size(100, 28)
        Me.ButtonA.TabIndex = 234
        Me.ButtonA.Text = "仅保存记录"
        Me.ButtonA.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonA.UseVisualStyleBackColor = False
        '
        'LabelMain
        '
        Me.LabelMain.AutoSize = True
        Me.LabelMain.BackColor = System.Drawing.SystemColors.Control
        Me.LabelMain.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelMain.ForeColor = System.Drawing.Color.LightSlateGray
        Me.LabelMain.Location = New System.Drawing.Point(12, 9)
        Me.LabelMain.Name = "LabelMain"
        Me.LabelMain.Size = New System.Drawing.Size(180, 26)
        Me.LabelMain.TabIndex = 233
        Me.LabelMain.Text = "计划 - 任务分配 >>"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 19)
        Me.Label4.TabIndex = 248
        Me.Label4.Text = "日期"
        '
        'TextBoxRq
        '
        Me.TextBoxRq.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxRq.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxRq.Location = New System.Drawing.Point(67, 114)
        Me.TextBoxRq.Name = "TextBoxRq"
        Me.TextBoxRq.Size = New System.Drawing.Size(104, 18)
        Me.TextBoxRq.TabIndex = 247
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(372, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 19)
        Me.Label3.TabIndex = 246
        Me.Label3.Text = "图号"
        '
        'TextBoxTh
        '
        Me.TextBoxTh.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxTh.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxTh.Location = New System.Drawing.Point(426, 90)
        Me.TextBoxTh.Name = "TextBoxTh"
        Me.TextBoxTh.ReadOnly = True
        Me.TextBoxTh.Size = New System.Drawing.Size(100, 18)
        Me.TextBoxTh.TabIndex = 245
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(177, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 19)
        Me.Label2.TabIndex = 244
        Me.Label2.Text = "订单号"
        '
        'TextBoxDdh
        '
        Me.TextBoxDdh.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxDdh.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxDdh.Location = New System.Drawing.Point(231, 90)
        Me.TextBoxDdh.Name = "TextBoxDdh"
        Me.TextBoxDdh.ReadOnly = True
        Me.TextBoxDdh.Size = New System.Drawing.Size(135, 18)
        Me.TextBoxDdh.TabIndex = 243
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 19)
        Me.Label1.TabIndex = 242
        Me.Label1.Text = "客户"
        '
        'TextBoxKh
        '
        Me.TextBoxKh.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxKh.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxKh.Location = New System.Drawing.Point(67, 90)
        Me.TextBoxKh.Name = "TextBoxKh"
        Me.TextBoxKh.ReadOnly = True
        Me.TextBoxKh.Size = New System.Drawing.Size(104, 18)
        Me.TextBoxKh.TabIndex = 241
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(67, 114)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 264
        Me.MonthCalendar1.Visible = False
        '
        'LabelZgs
        '
        Me.LabelZgs.AutoSize = True
        Me.LabelZgs.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelZgs.Location = New System.Drawing.Point(372, 114)
        Me.LabelZgs.Name = "LabelZgs"
        Me.LabelZgs.Size = New System.Drawing.Size(48, 19)
        Me.LabelZgs.TabIndex = 272
        Me.LabelZgs.Text = "总工时"
        '
        'LabelShr
        '
        Me.LabelShr.AutoSize = True
        Me.LabelShr.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelShr.Location = New System.Drawing.Point(13, 139)
        Me.LabelShr.Name = "LabelShr"
        Me.LabelShr.Size = New System.Drawing.Size(48, 19)
        Me.LabelShr.TabIndex = 270
        Me.LabelShr.Text = "审核人"
        '
        'TextBoxShr
        '
        Me.TextBoxShr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxShr.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxShr.Location = New System.Drawing.Point(67, 139)
        Me.TextBoxShr.Name = "TextBoxShr"
        Me.TextBoxShr.Size = New System.Drawing.Size(104, 18)
        Me.TextBoxShr.TabIndex = 269
        '
        'LabelFzr
        '
        Me.LabelFzr.AutoSize = True
        Me.LabelFzr.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelFzr.Location = New System.Drawing.Point(532, 114)
        Me.LabelFzr.Name = "LabelFzr"
        Me.LabelFzr.Size = New System.Drawing.Size(48, 19)
        Me.LabelFzr.TabIndex = 268
        Me.LabelFzr.Text = "负责人"
        '
        'TextBoxFzr
        '
        Me.TextBoxFzr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxFzr.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxFzr.Location = New System.Drawing.Point(586, 114)
        Me.TextBoxFzr.Name = "TextBoxFzr"
        Me.TextBoxFzr.Size = New System.Drawing.Size(106, 18)
        Me.TextBoxFzr.TabIndex = 267
        '
        'LabelSl
        '
        Me.LabelSl.AutoSize = True
        Me.LabelSl.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSl.Location = New System.Drawing.Point(177, 114)
        Me.LabelSl.Name = "LabelSl"
        Me.LabelSl.Size = New System.Drawing.Size(35, 19)
        Me.LabelSl.TabIndex = 266
        Me.LabelSl.Text = "数量"
        '
        'TextBoxSl
        '
        Me.TextBoxSl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxSl.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxSl.Location = New System.Drawing.Point(231, 114)
        Me.TextBoxSl.Name = "TextBoxSl"
        Me.TextBoxSl.Size = New System.Drawing.Size(135, 18)
        Me.TextBoxSl.TabIndex = 265
        '
        'TextBoxZgs
        '
        Me.TextBoxZgs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxZgs.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxZgs.Location = New System.Drawing.Point(426, 114)
        Me.TextBoxZgs.Name = "TextBoxZgs"
        Me.TextBoxZgs.Size = New System.Drawing.Size(100, 18)
        Me.TextBoxZgs.TabIndex = 271
        '
        'LabelBz
        '
        Me.LabelBz.AutoSize = True
        Me.LabelBz.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelBz.Location = New System.Drawing.Point(177, 139)
        Me.LabelBz.Name = "LabelBz"
        Me.LabelBz.Size = New System.Drawing.Size(35, 19)
        Me.LabelBz.TabIndex = 274
        Me.LabelBz.Text = "备注"
        '
        'TextBoxBz
        '
        Me.TextBoxBz.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxBz.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxBz.Location = New System.Drawing.Point(231, 139)
        Me.TextBoxBz.Name = "TextBoxBz"
        Me.TextBoxBz.Size = New System.Drawing.Size(461, 18)
        Me.TextBoxBz.TabIndex = 273
        '
        'ComboBoxFP
        '
        Me.ComboBoxFP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBoxFP.Font = New System.Drawing.Font("微软雅黑", 11.0!)
        Me.ComboBoxFP.FormattingEnabled = True
        Me.ComboBoxFP.Items.AddRange(New Object() {"加工分配", "外协分配", "组装分配", "送货分配", "调拨分配", "毛坯购入计划", "成品购入计划"})
        Me.ComboBoxFP.Location = New System.Drawing.Point(12, 47)
        Me.ComboBoxFP.Name = "ComboBoxFP"
        Me.ComboBoxFP.Size = New System.Drawing.Size(121, 28)
        Me.ComboBoxFP.TabIndex = 275
        Me.ComboBoxFP.Text = "加工分配"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 163)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(680, 227)
        Me.DataGridView1.TabIndex = 276
        '
        'LabelSM
        '
        Me.LabelSM.AutoSize = True
        Me.LabelSM.Enabled = False
        Me.LabelSM.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelSM.Location = New System.Drawing.Point(12, 393)
        Me.LabelSM.Name = "LabelSM"
        Me.LabelSM.Size = New System.Drawing.Size(48, 19)
        Me.LabelSM.TabIndex = 277
        Me.LabelSM.Text = "说明："
        '
        'ButtonB
        '
        Me.ButtonB.BackColor = System.Drawing.Color.White
        Me.ButtonB.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.ButtonB.FlatAppearance.BorderSize = 0
        Me.ButtonB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonB.Font = New System.Drawing.Font("微软雅黑", 11.0!)
        Me.ButtonB.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonB.Location = New System.Drawing.Point(249, 47)
        Me.ButtonB.Name = "ButtonB"
        Me.ButtonB.Size = New System.Drawing.Size(100, 28)
        Me.ButtonB.TabIndex = 278
        Me.ButtonB.Text = "保存并退出"
        Me.ButtonB.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonB.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(532, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 19)
        Me.Label5.TabIndex = 280
        Me.Label5.Text = "计划数"
        '
        'TextBoxJhs
        '
        Me.TextBoxJhs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxJhs.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxJhs.Location = New System.Drawing.Point(586, 90)
        Me.TextBoxJhs.Name = "TextBoxJhs"
        Me.TextBoxJhs.ReadOnly = True
        Me.TextBoxJhs.Size = New System.Drawing.Size(82, 18)
        Me.TextBoxJhs.TabIndex = 279
        '
        'FCJH_FP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 421)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxJhs)
        Me.Controls.Add(Me.ButtonB)
        Me.Controls.Add(Me.LabelSM)
        Me.Controls.Add(Me.ComboBoxFP)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.LabelBz)
        Me.Controls.Add(Me.TextBoxBz)
        Me.Controls.Add(Me.LabelZgs)
        Me.Controls.Add(Me.LabelShr)
        Me.Controls.Add(Me.TextBoxShr)
        Me.Controls.Add(Me.LabelFzr)
        Me.Controls.Add(Me.TextBoxFzr)
        Me.Controls.Add(Me.LabelSl)
        Me.Controls.Add(Me.TextBoxSl)
        Me.Controls.Add(Me.TextBoxZgs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxTh)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxDdh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxKh)
        Me.Controls.Add(Me.ButtonA)
        Me.Controls.Add(Me.LabelMain)
        Me.Controls.Add(Me.TextBoxRq)
        Me.Controls.Add(Me.DataGridView1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FCJH_FP"
        Me.Text = "计划 - 任务分配"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonA As System.Windows.Forms.Button
    Friend WithEvents LabelMain As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRq As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTh As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDdh As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKh As System.Windows.Forms.TextBox
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents LabelZgs As System.Windows.Forms.Label
    Friend WithEvents LabelShr As System.Windows.Forms.Label
    Friend WithEvents TextBoxShr As System.Windows.Forms.TextBox
    Friend WithEvents LabelFzr As System.Windows.Forms.Label
    Friend WithEvents TextBoxFzr As System.Windows.Forms.TextBox
    Friend WithEvents LabelSl As System.Windows.Forms.Label
    Friend WithEvents TextBoxSl As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxZgs As System.Windows.Forms.TextBox
    Friend WithEvents LabelBz As System.Windows.Forms.Label
    Friend WithEvents TextBoxBz As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxFP As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelSM As System.Windows.Forms.Label
    Friend WithEvents ButtonB As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxJhs As System.Windows.Forms.TextBox
End Class
