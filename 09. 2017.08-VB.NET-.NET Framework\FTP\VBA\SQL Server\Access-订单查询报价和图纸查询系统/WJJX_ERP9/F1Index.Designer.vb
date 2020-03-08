<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F1Index
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1Index))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBoxPkt = New System.Windows.Forms.Label()
        Me.PictureBoxSearch = New System.Windows.Forms.PictureBox()
        Me.PictureBoxSet = New System.Windows.Forms.PictureBox()
        Me.PictureBoxOben = New System.Windows.Forms.PictureBox()
        Me.PictureBoxMain = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxOben, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 680)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        '
        'PictureBoxPkt
        '
        Me.PictureBoxPkt.AutoSize = True
        Me.PictureBoxPkt.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.PictureBoxPkt.Location = New System.Drawing.Point(180, 90)
        Me.PictureBoxPkt.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.PictureBoxPkt.Name = "PictureBoxPkt"
        Me.PictureBoxPkt.Size = New System.Drawing.Size(11, 12)
        Me.PictureBoxPkt.TabIndex = 5
        Me.PictureBoxPkt.Text = " "
        '
        'PictureBoxSearch
        '
        Me.PictureBoxSearch.Image = CType(resources.GetObject("PictureBoxSearch.Image"), System.Drawing.Image)
        Me.PictureBoxSearch.Location = New System.Drawing.Point(809, 617)
        Me.PictureBoxSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxSearch.Name = "PictureBoxSearch"
        Me.PictureBoxSearch.Size = New System.Drawing.Size(149, 64)
        Me.PictureBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxSearch.TabIndex = 3
        Me.PictureBoxSearch.TabStop = False
        '
        'PictureBoxSet
        '
        Me.PictureBoxSet.Image = CType(resources.GetObject("PictureBoxSet.Image"), System.Drawing.Image)
        Me.PictureBoxSet.Location = New System.Drawing.Point(624, 617)
        Me.PictureBoxSet.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxSet.Name = "PictureBoxSet"
        Me.PictureBoxSet.Size = New System.Drawing.Size(151, 64)
        Me.PictureBoxSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxSet.TabIndex = 2
        Me.PictureBoxSet.TabStop = False
        '
        'PictureBoxOben
        '
        Me.PictureBoxOben.Image = Global.WJJX_ERP10.My.Resources.Resources.标题栏lan
        Me.PictureBoxOben.Location = New System.Drawing.Point(-10, -1)
        Me.PictureBoxOben.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxOben.Name = "PictureBoxOben"
        Me.PictureBoxOben.Size = New System.Drawing.Size(1125, 71)
        Me.PictureBoxOben.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxOben.TabIndex = 1
        Me.PictureBoxOben.TabStop = False
        '
        'PictureBoxMain
        '
        Me.PictureBoxMain.Image = Global.WJJX_ERP10.My.Resources.Resources.MRP流程主界面3
        Me.PictureBoxMain.Location = New System.Drawing.Point(60, 112)
        Me.PictureBoxMain.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBoxMain.Name = "PictureBoxMain"
        Me.PictureBoxMain.Size = New System.Drawing.Size(862, 503)
        Me.PictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxMain.TabIndex = 0
        Me.PictureBoxMain.TabStop = False
        '
        'F1Index
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(984, 701)
        Me.Controls.Add(Me.PictureBoxPkt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBoxSearch)
        Me.Controls.Add(Me.PictureBoxSet)
        Me.Controls.Add(Me.PictureBoxOben)
        Me.Controls.Add(Me.PictureBoxMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "F1Index"
        Me.Opacity = 0.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormNIndex"
        CType(Me.PictureBoxSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxOben, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxMain As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxOben As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxSet As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxSearch As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxPkt As System.Windows.Forms.Label
End Class
