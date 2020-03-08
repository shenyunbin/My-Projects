Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Net

Public Class F3JH
    'Dim iMode As Integer = 1 '设置模式1=订单 2=零部件
    Dim ColSize1() As Integer = {120, 240, 180, 519, 180}
    Dim ColSize2() As Integer = {60, 120, 120, 80, 60, 60, 80, 142, 87, 87, 87, 132, 124}

    Dim G_Kh, G_Ddh, G_Jhrq As String
    Dim RwfpLx As String = "加工分配"

    Public SQL_Connection As String = "Data Source=127.0.0.1;Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String = "127.0.0.1"


    Public DGV_Mode As Integer = 1 '表格显示模式 1=订单，2=零件

    '设置显示模式
    Dim XSMS As String = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  " ' " or 状态= '生产'  or 状态 ='已完成' "

    '提取零件生产信息SQL语句
    Const SQL_Cmd1 = "select 零部件标记 as 标记,型号,图号,名称,台件,具体计划数 as 计划,单件工时,备注,加工分配,组装分配,送货分配,毛坯购入计划,成品购入计划 from 新零部件信息 where 订单号='"

    Dim FontU As Font = New Font("微软雅黑", 10.5, FontStyle.Bold)
    Dim FontN As Font = New Font("微软雅黑", 10.5, FontStyle.Regular)


    '定义订单分类各个列位置
    Const DGVDdfl_Kh = 0
    Const DGVDdfl_Ddh = 1
    Const DGVDdfl_Jhrq = 2
    Const DGVDdfl_Bz = 3
    Const DGVDdfl_Cjrq = 4


    '定义零件信息各个列位置
    Const DGVLjxx_Bj = 0
    Const DGVLjxx_Xh = 1
    Const DGVLjxx_Th = 2
    Const DGVLjxx_Mc = 1
    Const DGVLjxx_Tj = 2
    Const DGVLjxx_Jh = 5
    Const DGVLjxx_Djgs = 6
    Const DGVLjxx_Bz = 7
    Const DGVLjxx_Jgfp = 8
    Const DGVLjxx_Zzfp = 9
    Const DGVLjxx_Ssfp = 10
    Const DGVLjxx_Mpgrjh = 11
    Const DGVLjxx_Cpgrjh = 12

    '全局操作模块/////////////////////////////////////////////////////////////////////////////////////

    Private Sub FCJH_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ButtonDisp(DGV_Mode)
        DGV_Mode = 1 '设置显示模式
        DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息



    End Sub


    '表格双击事件
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Select Case DGV_Mode
            Case 1
                DGV_Mode = 2
                ButtonDisp(DGV_Mode)
                G_Kh = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Kh).Value.ToString
                G_Ddh = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString
                G_Jhrq = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Jhrq).Value.ToString
                LabelMain.Text = "计划 - " + G_Ddh + " - 计划日期 " + G_Jhrq + " >>"
                DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入零件信息
                FPXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView2, RwfpLx) '载入分配信息
                CKXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView3) '载入仓库信息
        End Select
    End Sub

    '表格单击事件
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Select Case DGV_Mode
            Case 2
                FPXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView2, RwfpLx) '载入分配信息
                CKXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView3) '载入仓库信息
        End Select
    End Sub



    '返回按钮事件
    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Select Case DGV_Mode
            Case 2
                DGV_Mode = 1
                ButtonDisp(DGV_Mode)
                DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                DataGridView2.Columns.Clear()
                DataGridView3.Columns.Clear()
        End Select
    End Sub

    '物料信息查看按钮加工分配/送货分配/购入计划
    Private Sub LabelJG_Click(sender As Object, e As EventArgs) Handles LabelJG.Click
        Select Case DGV_Mode
            Case 2
                LabelJG.Font = FontU
                LabelSH.Font = FontN
                LabelGR.Font = FontN
                RwfpLx = "加工分配"
                FPXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView2, RwfpLx) '载入分配信息
        End Select
    End Sub
    Private Sub LabelSH_Click(sender As Object, e As EventArgs) Handles LabelSH.Click
        Select Case DGV_Mode
            Case 2
                LabelJG.Font = FontN
                LabelSH.Font = FontU
                LabelGR.Font = FontN
                RwfpLx = "送货分配"
                FPXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView2, RwfpLx) '载入分配信息
        End Select
    End Sub
    Private Sub LabelGR_Click(sender As Object, e As EventArgs) Handles LabelGR.Click
        Select Case DGV_Mode
            Case 2
                LabelJG.Font = FontN
                LabelSH.Font = FontN
                LabelGR.Font = FontU
                RwfpLx = "毛坯购入计划"
                FPXX_Load(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView2, RwfpLx) '载入分配信息
        End Select
    End Sub


    '全局操作模块======================================================

    '各个按钮功能模块///////////////////////////////////////////////////////////////////////////////////////

    '导入订单按钮事件
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        F3JH_DDDR.Show()
    End Sub

    '加工分配按钮事件
    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click, ButtonD.Click, ButtonE.Click, ButtonF.Click, ButtonG.Click
        'MessageBox.Show(CType(sender, Control).Name)
        If DGV_Mode = 2 Then
            F3JH_FP.Ld(G_Kh, G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Jh).Value.ToString, G_Jhrq, sender.Text)
        End If
    End Sub

    '各个按钮功能模块==================================================





    '信息载入模块//////////////////////////////////////////////////////////////////////////////////////////////
    '载入订单信息
    Public Sub DGV_Load(iMode As Integer, data1 As String, data2 As String, DGV As DataGridView) '1=载入订单，2=载入零件，data1=订单号，data2=部件型号
        DGV.Columns.Clear()
        Select Case iMode
            Case 1 '载入订单列表
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter("select * from 新订单信息  order by 创建日期 desc", cn)
                Dim ds As New DataSet
                da.Fill(ds, "订单分类信息")
                DGV.DataSource = ds.Tables("订单分类信息")
                Exit Select
            Case 2    '载入订单的零件的信息
                Dim Sql As String = vbNullString
                Sql = SQL_Cmd1 + data1 + "' order by 标记 asc" '订单信息.状态='生产' and
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter(Sql, cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                DGV.DataSource = ds.Tables("零件信息")
                If DGV.RowCount = 0 Then
                    Exit Sub
                End If
                For i = 0 To DGV.RowCount - 1
                    If DGV.Rows(i).Cells.Item(DGVLjxx_Xh).Value.ToString = DGV.Rows(i).Cells.Item(DGVLjxx_Th).Value.ToString Then
                        DGV.Rows(i).DefaultCellStyle.BackColor = Color.LightGray  '设置部件为灰色
                    End If
                Next
                Exit Select
        End Select
        SetDVG() '设置表格格式
    End Sub

    '载入任务分配信息
    Public Sub FPXX_Load(Ddh As String, Th As String, DGV As DataGridView, Lx As String)
        'Dim Lx As String
        Select Case Lx
            Case "加工分配", "外协分配", "组装分配"
                Lx = "' and ( 记录类型 ='加工分配' or 记录类型='外协分配' or 记录类型='组装分配')"
                Exit Select
            Case "送货分配", "调拨分配"
                Lx = "' and ( 记录类型 ='送货分配' or 记录类型='调拨分配' )"
                Exit Select
            Case "毛坯购入计划", "成品购入计划"
                Lx = "' and ( 记录类型 ='毛坯购入计划' or 记录类型='成品购入计划' )"
                Exit Select
            Case Else
                Exit Sub
        End Select
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select 记录类型,日期,数量,负责人,审核人,其他信息,备注,识别号 from 新物料信息 where 订单号='" + Ddh + "' and 图号='" + Th + Lx + " order by 识别号 asc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "新物料信息")
        DGV.DataSource = ds.Tables("新物料信息")
    End Sub

    '载入零部件仓库信息
    Public Sub CKXX_Load(Kh As String, Th As String, DGV As DataGridView)
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select 仓库,数量,备注 from 新仓库信息 where 客户='" + Kh + "' and 图号='" + Th + "' order by 仓库 desc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "新物料信息")
        DGV.DataSource = ds.Tables("新物料信息")
    End Sub


    '信息载入模块============================================

    '辅助功能模块////////////////////////////////////////////////////////////////////////////


    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1, DataGridView2, DataGridView3}
        For i = 0 To dgv.Length - 1
            dgv(i).ReadOnly = True
            dgv(i).SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv(i).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv(i).AllowUserToAddRows = False
            dgv(i).AllowUserToDeleteRows = False
            dgv(i).AllowUserToOrderColumns = False
            dgv(i).AllowUserToResizeRows = False
        Next
        Select Case DGV_Mode
            Case 1
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                For i = 0 To 4
                    DataGridView1.Columns(i).Width = ColSize1(i)
                    DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next
            Case Else
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                For i = 0 To 12
                    DataGridView1.Columns(i).Width = ColSize2(i)
                    DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next

        End Select
    End Sub

    '按钮显示模块
    Private Sub ButtonDisp(iMode As Integer)
        Select Case iMode
            Case 1
                Dim bol As Boolean = False
                ButtonA.Visible = Not bol
                ButtonB.Visible = bol
                ButtonC.Visible = bol
                ButtonD.Visible = bol
                ButtonE.Visible = bol
                ButtonF.Visible = bol
                ButtonG.Visible = bol

            Case 2
                'ButtonA.Text = "导出生产单"
                Dim bol As Boolean = True
                ButtonA.Visible = Not bol
                ButtonB.Visible = bol
                ButtonC.Visible = bol
                ButtonD.Visible = bol
                ButtonE.Visible = bol
                ButtonF.Visible = bol
                ButtonG.Visible = bol

        End Select
    End Sub

    '辅助功能模块=============================================


    '删除分配记录
    Private Sub LabelSCFP_Click(sender As Object, e As EventArgs) Handles LabelSCFP.Click

    End Sub
End Class