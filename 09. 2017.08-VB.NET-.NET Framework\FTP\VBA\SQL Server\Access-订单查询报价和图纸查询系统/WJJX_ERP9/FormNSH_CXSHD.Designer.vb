<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNSH_CXSHD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNSH_CXSHD))
        Me.DataGridViewShd = New System.Windows.Forms.DataGridView()
        Me.DataGridViewShdxx = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelShdyl = New System.Windows.Forms.Label()
        Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
        Me.LabelShdToExcel = New System.Windows.Forms.Label()
        CType(Me.DataGridViewShd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewShdxx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewShd
        '
        Me.DataGridViewShd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewShd.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridViewShd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewShd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewShd.Location = New System.Drawing.Point(12, 24)
        Me.DataGridViewShd.Name = "DataGridViewShd"
        Me.DataGridViewShd.RowHeadersVisible = False
        Me.DataGridViewShd.RowTemplate.Height = 23
        Me.DataGridViewShd.Size = New System.Drawing.Size(291, 545)
        Me.DataGridViewShd.TabIndex = 332
        '
        'DataGridViewShdxx
        '
        Me.DataGridViewShdxx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewShdxx.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridViewShdxx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridViewShdxx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewShdxx.Location = New System.Drawing.Point(309, 24)
        Me.DataGridViewShdxx.Name = "DataGridViewShdxx"
        Me.DataGridViewShdxx.RowHeadersVisible = False
        Me.DataGridViewShdxx.RowTemplate.Height = 23
        Me.DataGridViewShdxx.Size = New System.Drawing.Size(663, 545)
        Me.DataGridViewShdxx.TabIndex = 331
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 333
        Me.Label1.Text = "选择客户日期"
        '
        'LabelShdyl
        '
        Me.LabelShdyl.AutoSize = True
        Me.LabelShdyl.Location = New System.Drawing.Point(307, 9)
        Me.LabelShdyl.Name = "LabelShdyl"
        Me.LabelShdyl.Size = New System.Drawing.Size(65, 12)
        Me.LabelShdyl.TabIndex = 334
        Me.LabelShdyl.Text = "送货单预览"
        '
        'SaveFileDialogExcel
        '
        Me.SaveFileDialogExcel.Filter = "|*.xlsx"
        '
        'LabelShdToExcel
        '
        Me.LabelShdToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelShdToExcel.AutoSize = True
        Me.LabelShdToExcel.ForeColor = System.Drawing.Color.Teal
        Me.LabelShdToExcel.Location = New System.Drawing.Point(907, 9)
        Me.LabelShdToExcel.Name = "LabelShdToExcel"
        Me.LabelShdToExcel.Size = New System.Drawing.Size(65, 12)
        Me.LabelShdToExcel.TabIndex = 347
        Me.LabelShdToExcel.Text = "导出到表格"
        '
        'FormNSH_CXSHD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 581)
        Me.Controls.Add(Me.LabelShdToExcel)
        Me.Controls.Add(Me.LabelShdyl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridViewShd)
        Me.Controls.Add(Me.DataGridViewShdxx)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormNSH_CXSHD"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "送货系统-送货单查询"
        CType(Me.DataGridViewShd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DataGridViewShdxx,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents DataGridViewShd As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewShdxx As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelShdyl As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LabelShdToExcel As System.Windows.Forms.Label
End Class
