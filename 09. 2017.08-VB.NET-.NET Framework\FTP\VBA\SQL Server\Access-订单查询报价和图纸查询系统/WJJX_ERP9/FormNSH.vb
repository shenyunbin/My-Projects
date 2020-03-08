Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Net

Public Class FormNSH
    Public runThread As Thread
    Dim Thread_Shrq As String
    Dim Thread_Kh As String
    '设置显示模式
    Dim XSMS As String = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  " ' " or 状态= '生产'  or 状态 ='已完成' "

    Public SQL_Connection As String '= "Data Source=(local);Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String

    Public DGV_Mode As Integer 'DGV显示模式 1=订单，2=部件
    Dim G_Ddh, G_Kh, G_Xh, G_Shrq As String


    Const SQL_CmdDdxx = "select 订单信息.状态,订单信息.订单号,订单信息.客户,订单信息.批号," +
        "部件计划数=(select top 1 sum(部件信息.计划数量) from 部件信息 where 部件信息.订单号=订单信息.订单号),部件送货数=(select top 1 sum(送货信息.送货数量) from 送货信息 where 送货信息.订单号=订单信息.订单号)," +
        "完成率=(select top 1 sum(送货信息.送货数量) from 送货信息 where 送货信息.订单号=订单信息.订单号)*1.0/(select top 1 sum(部件信息.计划数量) from 部件信息 where 部件信息.订单号=订单信息.订单号) " +
        "from 订单信息 where  "


    Const SQL_CmdBjxx1 = "select row_number() over (order by 部件信息.编号) as 行号,部件信息.型号,部件信息.名称,部件信息.计划日期," +
        "零件信息.含税单价,部件信息.计划数量,sum(送货信息.送货数量) as 送货数量,仓库信息.现有库存 " +
        "from 部件信息 left join 零件信息 on 部件信息.订单号=零件信息.订单号 and 部件信息.型号=零件信息.型号 and 部件信息.型号=零件信息.图号 " +
        "left join 送货信息 on  部件信息.订单号=送货信息.订单号 and 部件信息.型号=送货信息.型号 " +
        "left join 订单信息 " +
        "on  部件信息.订单号=订单信息.订单号 " +
        "left join 仓库信息 " +
        "on  部件信息.型号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
        "where  部件信息.订单号='"
    Const SQL_CmdBjxx2 = "'  group by 部件信息.编号,部件信息.型号,部件信息.名称,部件信息.计划数量,部件信息.计划日期,零件信息.含税单价,仓库信息.现有库存 order by 部件信息.编号 asc "

    '定义零件信息SQL语句(删除送货记录时用)
    Const SQL_CmdLjxx1 = "select row_number() over (order by 零件信息.编号) as 行号,零件信息.编号,零件信息.图号,零件信息.名称,零件信息.台件,零件信息.含税单价,部件信息.计划数量*零件信息.台件 as 具体计划数,仓库信息.现有库存,仓库信息.毛坯入库数,仓库信息.送货数量,订单信息.客户,零件信息.订单号,零件信息.型号,订单信息.状态 " +
                                 "from 零件信息 left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 left join 订单信息 on  零件信息.订单号=订单信息.订单号 left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
                                "where 订单信息.状态='生产' and 零件信息.订单号='"


    '定义零件信息各个列(删除送货记录时用)
    Const DGVLjxx_Th = 2
    Const DGVLjxx_Mc = 3
    Const DGVLjxx_Tj = 4
    Const DGVLjxx_Hsdj = 5
    Const DGVLjxx_Xykc = 7
    Const DGVLjxx_Mprks = 8
    Const DGVLjxx_Shsl = 9
    Const DGVLjxx_Kh = 10
    Const DGVLjxx_Ddh = 11
    Const DGVLjxx_Xh = 12
    Const DGVLjxx_Zt = 13


    '定义订单信息表每列所在位置
    Public Const DGV1_Zt = 0
    Public Const DGV1_Ddh = 1
    Public Const DGV1_Kh = 2
    Public Const DGV1_Ph = 3

    '定义部件信息各个列
    Const DGV2_Kh = 1
    Const DGV2_Xh = 1
    Const DGV2_Mc = 2
    Const DGV2_Jhrq = 3
    Const DGV2_Hsdj = 4
    Const DGV2_Jhsl = 5
    Const DGV2_Shsl = 6
    Const DGV2_Xykc = 7

    '定义送货信息各个列
    Const DGVShjl_Xh = 1
    Const DGVShjl_Shsl = 3
    Const DGVShjl_Hsdj = 4
    Const DGVShjl_Bz = 7
    Const DGVShjl_Kh = 8
    Const DGVShjl_Ddh = 9
    Const DGVShjl_Sbh = 10

    '定义送货单详情各个列
    Const DGVShdxx_Kh = 1
    Const DGVShdxx_Shrq = 2
    Const DGVShdxx_Xh = 3
    Const DGVShdxx_Mc = 4
    Const DGVShdxx_Shsl = 5
    Const DGVShdxx_Hsdj = 6
    Const DGVShdxx_Sbh = 10
    Const DGVShdxx_Ddh = 11



    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////

    '窗口启动时操作
    Private Sub FormNSH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)

        LabelBJ.Enabled = FormLogin.GlyMode
        LabelScjl.Enabled = False
        LabelShxqsc.Enabled = False
        G_Shrq = DateTimePickerShrq.Text '设置送货日期
        DGV_Mode = 1 '设置显示模式
        SetDVG() '设置表格格式
        ButtonDisp(DGV_Mode)
        DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
        Shdxx_Load(G_Shrq, "请选择订单")
    End Sub


    '单击表格显示零件信息
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Select Case DGV_Mode
            Case 1
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Ddh).Value
                Shdxx_Load(G_Shrq, G_Kh) '载入送货单信息
                Exit Select
            Case 2
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                DataDisp(True)
                Shjl_Load(G_Ddh, G_Xh) '载入送货记录
                'G_Jhsl = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Jhsl).Value
                'DGV_Load(DGV_Mode + 1, G_Ddh, G_Xh, DataGridView2) '载入零件信息
                Exit Select
            Case 3
                Exit Select
        End Select

    End Sub

    '双击表格到下一级
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Select Case DGV_Mode
            Case 1
                DGV_Mode = 2
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Ddh).Value
                Shdxx_Load(G_Shrq, G_Kh) '载入送货单信息
                ButtonDisp(DGV_Mode)
                DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
                LabelMain.Text = G_Kh + " - 订单" + G_Ddh + " >> 部件信息"
                LabelDdxx.Text = "部件信息（选择部件以开始送货）"
                If DataGridView1.Rows.Count = 0 Then '表格为空时退出
                    Exit Select
                End If
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                DataDisp(True)
                Shjl_Load(G_Ddh, G_Xh) '载入送货记录
                'G_Jhsl = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Jhsl).Value
                'DGV_Load(DGV_Mode + 1, G_Ddh, G_Xh, DataGridView2) '载入零件信息
                Exit Select
            Case 2
                'DGV_Mode = 1
                Exit Select
        End Select
    End Sub

    '单击返回到上一级
    Private Sub LabelBack_Click(sender As Object, e As EventArgs) Handles LabelBack.Click, ButtonBack.Click
        Select Case DGV_Mode
            Case 1
                If ButtonSX.Text = "退出筛选" Then
                    XSMS = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  "
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    ButtonSX.Text = "筛选订单"
                    ButtonSX.ForeColor = Color.Black
                End If
                Exit Select
            Case 2
                DGV_Mode = 1
                ButtonDisp(DGV_Mode)
                DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                If DataGridView1.Rows.Count = 0 Then '行数为0时退出
                    Exit Select
                End If
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Ddh).Value
                Shdxx_Load(G_Shrq, G_Kh) '载入送货单信息
                LabelMain.Text = "请在左上方表格中选择订单以开始送货单编辑 >>"
                LabelDdxx.Text = "所有在生产订单 - 请选择订单以开始送货单编辑 >>"
                DataGridViewShjl.Columns.Clear()
                Exit Select
        End Select
    End Sub




    '全局操作模块===================================================




    '各个按钮操作//////////////////////////////////////////////////////////////////////////////////////////


    '按钮A事件/图号搜索
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        If ButtonA.Text = "图/型号搜索" Then
            SSTH() '图号搜索
            ButtonA.Text = "退出搜索"
            ButtonA.ForeColor = Color.IndianRed
        Else
            TCSS() '退出搜索
            ButtonA.Text = "图/型号搜索"
            ButtonA.ForeColor = Color.Black
        End If
    End Sub

    '按钮C事件/打开图纸
    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        If FormLogin.OpenPic(G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString) = False Then
            OpenFileDialogPicture.ShowDialog()
        End If
    End Sub


    '按钮B事件/送货出库
    Private Sub ButtonB_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        If Not TextBoxXh.Text = DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString Then
            MessageBox.Show("输入的型号与下方选择的部件不符，无法送货出库。请重新在下方选择正确的部件。")
            Exit Sub
        End If
        Dim i As Integer = -1
        Try
            '获取之前活动的单元格
            i = DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try
        SHCK(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString, TextBoxShsl.Text)
        DataGridViewShjl.Columns.Clear()
        If i >= 0 Then     
            '载入部件信息
            DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
            '设置之前的活动单元格
            DataGridView1.CurrentCell = DataGridView1(0, i)
            G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
            '载入送货记录
            Shjl_Load(G_Ddh, G_Xh)
            '载入送货单详情
            Shdxx_Load(G_Shrq, G_Kh)
        End If
    End Sub


    '删除送货记录
    Private Sub LabelScjl_Click(sender As Object, e As EventArgs) Handles LabelScjl.Click
        Dim i As Integer = -1
        Try
            '获取之前活动的单元格
            i = DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try
        ScShjl()
        DataGridViewShjl.Columns.Clear()
        If i >= 0 Then
            '载入部件信息
            DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
            '设置之前的活动单元格
            DataGridView1.CurrentCell = DataGridView1(0, i)
            G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
            '载入送货记录
            Shjl_Load(G_Ddh, G_Xh)
            '载入送货单详情
            Shdxx_Load(G_Shrq, G_Kh)
        End If
    End Sub

    '更改送货日期
    Private Sub DateTimePickerShrq_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerShrq.ValueChanged
        G_Shrq = DateTimePickerShrq.Text
        Shdxx_Load(G_Shrq, G_Kh) '载入送货单信息
    End Sub

    '当图号框有更改时退出搜索
    Private Sub TextBoxXh_TextChanged(sender As Object, e As EventArgs) Handles TextBoxXh.TextChanged
        If ButtonA.Text = "退出搜索" Then
            TCSS()
            ButtonA.Text = "图/型号搜索"
            ButtonA.ForeColor = Color.Black
        End If
    End Sub

    '导出生产单到表格
    Private Sub LabelShdToExcel_Click(sender As Object, e As EventArgs) Handles LabelShdToExcel.Click
        SaveFileDialogExcel.FileName = LabelShrq.Text
        SaveFileDialogExcel.ShowDialog()
    End Sub


    '删除送货单详情记录
    Private Sub LabelShxqsc_Click(sender As Object, e As EventArgs) Handles LabelShxqsc.Click
        If DataGridViewShdxx.RowCount = 0 Then
            Exit Sub
        End If
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定的送货记录？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            '获取之前活动的单元格
            Dim Y1 As Integer = DataGridView1.CurrentCellAddress.Y
            Dim Y2 As Integer = DataGridViewShdxx.CurrentCellAddress.Y
            Dim Sbh As String = DataGridViewShdxx.CurrentRow.Cells.Item(DGVShdxx_Sbh).Value.ToString
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "Delete from 送货信息 where 识别号 = '" + Sbh + "'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            Dim Ddh As String = DataGridViewShdxx.CurrentRow.Cells.Item(DGVShdxx_Ddh).Value.ToString
            Dim Xh As String = DataGridViewShdxx.CurrentRow.Cells.Item(DGVShdxx_Xh).Value.ToString
            Dim Shsl As Integer = 0
            Try
                Shsl = DataGridViewShdxx.CurrentRow.Cells.Item(DGVShdxx_Shsl).Value
            Catch ex As Exception
            End Try
            '部件送货记录删除更改仓库信息
            ShToCkxx(Ddh, Xh, -Shsl)
            '载入部件信息
            DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
            '载入送货记录
            Shjl_Load(G_Ddh, G_Xh)
            '载入送货单详情
            Shdxx_Load(G_Shrq, G_Kh)
            Try
                If Y2 > 0 And Y2 <= (DataGridViewShdxx.Rows.Count + 1) Then
                    DataGridViewShdxx.CurrentCell = DataGridViewShdxx(0, Y2 - 1)
                End If
                '设置之前的活动单元格
                DataGridView1.CurrentCell = DataGridView1(0, Y1)
            Catch ex As Exception
            End Try
        End If
    End Sub

    '查询送货单按钮事件
    Private Sub ButtonD_Click(sender As Object, e As EventArgs) Handles ButtonD.Click
        FormNSH_CXSHD.Show()
    End Sub

    '订单已完成按钮事件
    Private Sub ButtonE_Click(sender As Object, e As EventArgs) Handles ButtonE.Click
        Dim i As Integer
        Try
            '获取之前活动的单元格
            i = DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try
        '订单进入生产操作
        Ddwc(DataGridView1.CurrentRow.Cells.Item(DGV1_Ddh).Value.ToString)
        '载入订单信息
        DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1)
        If i > 0 Then
            DataGridView1.CurrentCell = DataGridView1(0, i - 1)
        End If
        MessageBox.Show("所选订单状态已改为""已完成""。")
    End Sub

    '显示所有订单
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If ButtonSX.Text = "退出筛选" Then
            If CheckBox1.Checked Then
                MessageBox.Show("显示所有订单前，请您先""退出筛选""。")
                CheckBox1.Checked = False
            End If
            Exit Sub
        End If
        If CheckBox1.Checked Then
            XSMS = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  " + " or 订单信息.状态 ='已完成' "
        Else
            XSMS = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  "
        End If
        Select Case DGV_Mode
            Case 1 '重新导入订单
                DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                Exit Select
        End Select
    End Sub


    '启用删除功能
    Private Sub CheckBoxQysc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxQysc.CheckedChanged
        If CheckBoxQysc.Checked Then
            LabelScjl.Enabled = True
            LabelShxqsc.Enabled = True
        Else
            LabelScjl.Enabled = False
            LabelShxqsc.Enabled = False
        End If
    End Sub

    '筛选订单
    Private Sub ButtonSX_Click(sender As Object, e As EventArgs) Handles ButtonSX.Click
        If ButtonSX.Text = "筛选订单" Then
            CheckBox1.Checked = False
            XSMS = "订单信息.订单号 like '%" + TextBoxSX.Text + "%' or 订单信息.客户 like '%" + TextBoxSX.Text + "%'or 订单信息.批号 like '%" + TextBoxSX.Text + "%'"
            Select Case DGV_Mode
                Case 1 '重新导入订单
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    Exit Select
            End Select
            ButtonSX.Text = "退出筛选"
            ButtonSX.ForeColor = Color.IndianRed
        Else
            XSMS = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  "
            Select Case DGV_Mode
                Case 1 '重新导入订单
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    Exit Select
            End Select
            ButtonSX.Text = "筛选订单"
            ButtonSX.ForeColor = Color.Black
        End If
    End Sub

    '各个按钮操作=====================================================



    '信息载入模块/////////////////////////////////////////////////////////////////////////////////////////////
    '载入订单信息
    Public Sub DGV_Load(iMode As Integer, data1 As String, data2 As String, DGV As DataGridView)
        Select Case iMode
            Case 1
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_CmdDdxx + XSMS + " order by 订单信息.订单号 desc", cn)
                Dim ds As New DataSet
                da.Fill(ds, "订单信息")
                DGV.DataSource = ds.Tables("订单信息")
                Exit Select
            Case 2
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_CmdBjxx1 + data1 + SQL_CmdBjxx2, cn)
                Dim ds As New DataSet
                da.Fill(ds, "部件信息")
                DGV.DataSource = ds.Tables("部件信息")
                'DGV.Columns(DGVBjxx_Hsdj).Visible = FormLogin.GlyMode
        End Select
    End Sub


    '加载送货记录
    Private Sub Shjl_Load(Ddh As String, Xh As String)
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select top 100 * from 送货信息 where 订单号='" & Ddh & "' and 型号='" & Xh & "' order by 识别号 desc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "送货信息")
        DataGridViewShjl.DataSource = ds.Tables("送货信息")
        'DataGridView2.Columns(DGVShjl_Hsdj).Visible = FormLogin.GlyMode
    End Sub

    '送货单详情载入模块
    Private Sub Shdxx_Load(Shrq As String, Kh As String)
        LabelShrq.Text = Kh + "-" + Shrq + "送货单详情"
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select  row_number() over (order by 识别号) as 序号,客户,送货日期,型号,名称,送货数量,含税单价,已完工,未完工,备注,识别号,订单号 from 送货信息 where 送货日期='" + Shrq + "' and 客户='" + Kh + "'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "送货信息详情")
        DataGridViewShdxx.DataSource = ds.Tables("送货信息详情")
        DataGridViewShdxx.Columns(DGVShdxx_Hsdj).Visible = FormLogin.GlyMode
        DataGridViewShdxx.Columns(DGVShdxx_Sbh).Visible = False
        DataGridViewShdxx.Columns(DGVShdxx_Ddh).Visible = False
    End Sub

    '信息载入模块========================================================



    '辅助功能模块////////////////////////////////////////////////////////////////////////////

    '各个窗口之间快速转换
    Private Sub LabelBJ_Click(sender As Object, e As EventArgs) Handles LabelBJ.Click
        FormLogin.BJLoad(Me)
    End Sub

    Private Sub LabelSC_Click(sender As Object, e As EventArgs) Handles LabelSC.Click
        FormLogin.SCLoad(Me)
    End Sub

    Private Sub LabelSH_Click(sender As Object, e As EventArgs) Handles LabelSH.Click
        'FormLogin.SHLoad(Me)
    End Sub

    Private Sub LabelCK_Click(sender As Object, e As EventArgs) Handles LabelCK.Click
        FormLogin.CKLoad(Me)
    End Sub

    Private Sub LabelJS_Click(sender As Object, e As EventArgs) Handles LabelJS.Click
        FormLogin.JSLoad(Me)
    End Sub

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1, DataGridViewShjl, DataGridViewShdxx}
        For i = 0 To dgv.Length - 1
            dgv(i).ReadOnly = True
            dgv(i).SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv(i).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgv(i).AllowUserToAddRows = False
            dgv(i).AllowUserToDeleteRows = False
            dgv(i).AllowUserToOrderColumns = False
            dgv(i).AllowUserToResizeRows = False
        Next
    End Sub

    '按钮显示模块
    Private Sub ButtonDisp(iMode As Integer)
        Select Case iMode
            Case 1
                'ButtonA.Text = "导出生产单"
                Dim bol As Boolean = False
                ButtonA.Visible = bol
                ButtonB.Visible = bol
                ButtonC.Visible = bol

                LabelA.Visible = bol
                LabelB.Visible = bol
                LabelC.Visible = bol
                LabelD.Visible = bol
                LabelE.Visible = bol
                LabelF.Visible = bol

                TextBoxXh.Visible = bol
                TextBoxShsl.Visible = bol
                TextBoxYwg.Visible = bol
                TextBoxWwg.Visible = bol
                TextBoxBz.Visible = bol

                DateTimePickerShrq.Visible = bol

                ButtonD.Visible = Not bol
                ButtonE.Visible = Not bol

                ButtonSX.Visible = True
                TextBoxSX.Visible = True
                LabelSX.Visible = True

            Case 2
                'ButtonB.Text = "图号搜索"
                ButtonB.ForeColor = Color.Black
                Dim bol As Boolean = True
                ButtonA.Visible = bol
                ButtonB.Visible = bol
                ButtonC.Visible = bol

                LabelA.Visible = bol
                LabelB.Visible = bol
                LabelC.Visible = bol
                LabelD.Visible = bol
                LabelE.Visible = bol
                LabelF.Visible = bol

                TextBoxXh.Visible = bol
                TextBoxShsl.Visible = bol
                TextBoxYwg.Visible = bol
                TextBoxWwg.Visible = bol
                TextBoxBz.Visible = bol

                DateTimePickerShrq.Visible = bol

                ButtonD.Visible = Not bol
                ButtonE.Visible = Not bol

                ButtonSX.Visible = False
                TextBoxSX.Visible = False
                LabelSX.Visible = False
        End Select
    End Sub

    '送货出库信息显示模块
    Private Sub DataDisp(bol As Boolean) 'True=显示，False=清除
        If bol Then
            TextBoxXh.Text = DataGridView1.CurrentRow.Cells(DGV2_Xh).Value.ToString
        Else
            TextBoxXh.Text = vbNullString
        End If
    End Sub

    '辅助功能模块=============================================

    '扩展功能模块///////////////////////////////////////////////////////////////////////////

    '删除毛坯入库/送货记录--------------------------------------------------------------------------------------------------------------------------------------------------------------912
    Private Sub ScShjl()
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定的毛坯入库/送货记录？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            '部件送货记录
            Dim Sbh As String = DataGridViewShjl.CurrentRow.Cells.Item(DGVShjl_Sbh).Value.ToString
            If Sbh = "" Then
                MessageBox.Show("未选定送货记录！", "操作错误！")
                Exit Sub
            End If
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "Delete from 送货信息 where 识别号 = '" + Sbh + "'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()

            '部件送货记录删除更改仓库信息
            Dim Ddh As String = DataGridViewShjl.CurrentRow.Cells.Item(DGVShjl_Ddh).Value.ToString
            Dim Xh As String = DataGridViewShjl.CurrentRow.Cells.Item(DGVShjl_Xh).Value.ToString
            Dim Shsl As Integer = DataGridViewShjl.CurrentRow.Cells.Item(DGVShjl_Shsl).Value
            ShToCkxx(Ddh, Xh, -Shsl)

        End If
    End Sub

    '送货改变仓库信息
    Private Sub ShToCkxx(Ddh As String, Xh As String, Shsl As Integer)
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter(SQL_CmdLjxx1 + Ddh + "' and 零件信息.型号='" + Xh + "'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "送货信息")
        'DataGridViewLjxx.DataSource = ds.Tables("送货信息")
        Dim Kh As String = ""
        Dim Th As String = ""
        Dim Mc As String = ""
        Dim Tj As Integer = 0
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Kh = ds.Tables(0).Rows(i).Item(DGVLjxx_Kh).ToString
            Th = ds.Tables(0).Rows(i).Item(DGVLjxx_Th).ToString
            Mc = ds.Tables(0).Rows(i).Item(DGVLjxx_Mc).ToString
            Try
                Tj = ds.Tables(0).Rows(i).Item(DGVLjxx_Tj)
            Catch
            End Try
            CkxxSh(Kh, Th, Mc, Tj * Shsl)
            System.Threading.Thread.Sleep(1) '延迟1毫秒 
        Next
    End Sub

    '送货修改单个仓库信息
    Private Sub CkxxSh(Kh As String, Th As String, Mc As String, Shsl As Integer)
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        Dim cn As New SqlConnection(SQL_Connection)
        Dim sql As String = "if exists(select * from 仓库信息 where 客户='" + Kh + "' and 图号='" + Th + "') " +
                        " update 仓库信息 set 送货数量 =送货数量+" +
                        Shsl.ToString + ", 现有库存 =现有库存 - " + Shsl.ToString + " , 识别号 ='" + Sbh + "' where 客户='" + Kh + "' and 图号='" + Th + "' " +
                        "else insert into 仓库信息 (客户,图号,名称,毛坯入库数,送货数量,现有库存,识别号) values('" + Kh + "','" + Th + "','" + Mc + "', 0 ," + Shsl.ToString + ", " + (-Shsl).ToString + " ,'" + Sbh + "')" '" + Shsl.ToString + "
        cn.Open()
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub


    '送货系统模块******************************************************************************************************************************************************
    '送货出库按钮事件
    Private Sub SHCK(Ddh As String, Xh As String, Shsl As String)
        Try
            '新增送货记录
            Shjl_Insert(Ddh, Xh)
            '遍历零件信息修改仓库信息
            ShToCkxx(Ddh, Xh, Shsl)
        Catch ex As Exception
            MessageBox.Show("部件送货失败，请检查是否已选定部件。", "操作错误！")
        End Try
    End Sub

    '新增送货记录
    Private Sub Shjl_Insert(Ddh As String, Xh As String)
        Dim Shrq As String = DateTimePickerShrq.Text
        Dim Shsl As Integer = 0
        Try
            Shsl = TextBoxShsl.Text
        Catch
            MessageBox.Show("送货失败！送货数量只能为数字！", "填写错误！")
            Exit Sub
        End Try
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter(SQL_CmdBjxx1 + Ddh + "' and 部件信息.型号='" + Xh + SQL_CmdBjxx2, cn)
        Dim ds As New DataSet
        da.Fill(ds, "部件信息")
        Dim Mc As String = ds.Tables(0).Rows(0).Item(DGV2_Mc).ToString
        Dim Hsdj As String = ds.Tables(0).Rows(0).Item(DGV2_Hsdj).ToString
        Dim Kh As String = G_Kh
        Dim Ywg As String = TextBoxYwg.Text
        Dim Wwg As String = TextBoxWwg.Text
        Dim Bz As String = TextBoxBz.Text
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        'Dim cn As New SqlConnection(SQL_Connection)
        cn.Open()
        Dim cm As New SqlCommand("insert into 送货信息 (送货日期,型号,名称,送货数量,含税单价,已完工,未完工,备注,客户,订单号,识别号) values('" + Shrq + "','" + Xh + "','" + Mc + "'," +
             Shsl.ToString + ",'" + Hsdj + "','" + Ywg + "','" + Wwg + "','" + Bz + "','" + Kh + "','" + Ddh + "','" + Sbh + "')", cn)
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub


    '零件信息中筛选图号,搜索图号
    Private Sub SSTH()
        Dim Sslx As Integer
        Dim Ssstring1 As String = "*" + TextBoxXh.Text + "*"
        Sslx = DGV2_Xh
        '断开数据连接
        Try
            Dim cm As CurrencyManager = CType(BindingContext(DataGridView1.DataSource), CurrencyManager)
            cm.SuspendBinding()
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells.Item(Sslx).Value.ToString Like Ssstring1 Then
                    DataGridView1.Rows(i).Visible = True
                Else
                    DataGridView1.Rows(i).Visible = False
                End If
            Next
        Catch
        End Try
    End Sub

    '退出搜索事件
    Private Sub TCSS()
        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Visible = True
        Next
    End Sub

    '图片导入和打开功能/辅助功能******************************************

    '导入图片/复制图片到指定路径
    Private Sub OpenFileDialogPicture_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogPicture.FileOk
        FormLogin.SavePic(OpenFileDialogPicture.FileName, G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString)
    End Sub


    '文件名转换/将字符串转换为文件系统接受的文件名
    Public Function ThToFileName(Th As String)
        Dim str As String = vbNullString
        Dim ii As Integer = 0
        For i = 0 To Th.Length - 1
            If Th.Chars(i) = Chr(42) Or Th.Chars(i) = Chr(47) Or Th.Chars(i) = Chr(58) Or Th.Chars(i) = Chr(92) Or Th.Chars(i) = Chr(124) Or Th.Chars(i) = Chr(62) Or
                           Th.Chars(i) = Chr(60) Or Th.Chars(i) = Chr(22) Or Th.Chars(i) = Chr(63) Then
                str += Chr(45)
            Else
                str += Th.Chars(i)
            End If
        Next
        Return str
    End Function


    '导出生产单模块*****************************************************************************************************************************************************


    '保存对话框结束后新增表格
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk
        Thread_Shrq = G_Shrq
        Thread_Kh = G_Kh
        runThread = New Thread(AddressOf ThreadToExcel)
        FormWait.F = runThread
        FormWait.Show()
        runThread.Start()

    End Sub


    '多线程支持函数###################################@多线程
    Private Sub ThreadToExcel()
        ExcelOutput(DataGridViewShdxx, Thread_Kh, Thread_Shrq, SaveFileDialogExcel.FileName)
    End Sub


    '定义委托-wait1###################################@多线程
    Public Delegate Sub VoidDelegate()
    '定义结束后关闭窗体方法-wait2
    Public Sub FormBJ_Wait_Close()
        FormWait.Close()
    End Sub



    '输出报价单数据到表格-----送货单专用!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!请勿直接复制!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    Public Sub ExcelOutput(DGV As DataGridView, Kh As String, Shrq As String, FileName As String)
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        Dim Hjsl As Integer = 0
        Dim Hjje As Integer = 0
        Dim yy As Integer = 0
        Try
            WorkBook = myExcel.Workbooks.Add()
            Sheet = WorkBook.Sheets(1)
            '输入表格名称
            Sheet.Name = Kh + "-" + Shrq + "-送货单"
            '合并单元格设置格式并输入表格标题
            With Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(1, 9))
                .Merge()
                .Font.Size = 20
                .Font.FontStyle = "bold"
                .RowHeight = 31
            End With
            Sheet.Cells(1, 1) = "德清县下舍五金机械厂承揽送货单"
            Sheet.Range(Sheet.Cells(2, 1), Sheet.Cells(2, 9)).Merge()
            Sheet.Cells(2, 1) = "收货单位（客户）：" + Kh + "                    送货日期：" + Shrq
            '输入表格标题栏并加粗                  '从第三列开始写入表格!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            For y = 0 To 9 'DGV.Columns.Count - 1
                If y = 1 Then
                    Continue For
                End If
                yy += 1
                WorkBook.Sheets(1).Cells(3, yy) = DGV.Columns(y).HeaderText
            Next
            Sheet.Range(Sheet.Cells(3, 1), Sheet.Cells(3, yy)).Font.FontStyle = "bold"
            '输入表格内容
            yy = 0
            For y = 0 To 9 'DGV.Columns.Count - 1               '从第三列开始写入表格!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                If y = 1 Then
                    Continue For
                End If
                If DGV.Columns(y).Visible = False Then
                    yy += 1
                    Continue For
                End If
                yy += 1
                Sheet.Range(Sheet.Cells(3, yy), Sheet.Cells(3, yy)).ColumnWidth = DGV.Columns(yy).Width / 6 'SetWidth(yy - 1) 
                For x = 0 To DGV.Rows.Count - 1
                    WorkBook.Sheets(1).Cells(x + 4, yy) = DGV.Rows(x).Cells.Item(y).Value.ToString
                Next
            Next
            '写入合计数量，合计价格
            For x = 0 To DGV.Rows.Count - 1
                Try
                    Hjsl += DGV.Rows(x).Cells.Item(DGVShdxx_Shsl).Value
                    Hjje += DGV.Rows(x).Cells.Item(DGVShdxx_Hsdj).Value
                Catch ex As Exception
                End Try
            Next
            Sheet.Range(Sheet.Cells(DGV.Rows.Count + 4, 1), Sheet.Cells(DGV.Rows.Count + 4, 4)).Merge()
            Sheet.Cells(DGV.Rows.Count + 4, 1) = "合计数量：" + Hjsl.ToString
            Sheet.Range(Sheet.Cells(DGV.Rows.Count + 4, 5), Sheet.Cells(DGV.Rows.Count + 4, 9)).Merge()
            Sheet.Cells(DGV.Rows.Count + 4, 5) = "合计金额：" + Hjje.ToString
            '设置单元格边框格式并垂直居中水平靠左
            With Sheet.Range(Sheet.Cells(3, 1), Sheet.Cells(DGV.Rows.Count + 4, yy))
                .Borders.LineStyle = 1
            End With
            Sheet.Range(Sheet.Cells(DGV.Rows.Count + 6, 1), Sheet.Cells(DGV.Rows.Count + 6, 3)).Merge()
            Sheet.Cells(DGV.Rows.Count + 6, 1) = "送货人："
            Sheet.Range(Sheet.Cells(DGV.Rows.Count + 6, 4), Sheet.Cells(DGV.Rows.Count + 6, 6)).Merge()
            Sheet.Cells(DGV.Rows.Count + 6, 4) = "制单人："
            Sheet.Range(Sheet.Cells(DGV.Rows.Count + 6, 7), Sheet.Cells(DGV.Rows.Count + 6, 9)).Merge()
            Sheet.Cells(DGV.Rows.Count + 6, 7) = "收货人："
            With Sheet.Range(Sheet.Cells(3, 1), Sheet.Cells(DGV.Rows.Count + 6, yy))
                .HorizontalAlignment = -4131 'Left -4131 Right -4152
                .VerticalAlignment = -4108
            End With
            WorkBook.SaveAs(FileName)
            'WorkBook.Close()
            'myExcel.Quit()
            myExcel.Visible = True
        Catch ex As Exception
            MessageBox.Show("输出报价单失败，请检查错误重新生成。", "操作错误！")
        End Try
        '跨线程关闭等待窗体-wait3
        Me.Invoke(New VoidDelegate(AddressOf FormBJ_Wait_Close))
        runThread.Abort()
    End Sub

    '订单完成
    Public Sub Ddwc(Ddh As String)
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "UPDATE 订单信息 SET  状态 = '已完成' WHERE 订单号 = '" + Ddh + "'"
        Dim cm1 As New SqlCommand(sql, cn)
        cm1.ExecuteNonQuery()
        sql = "UPDATE 部件信息 SET 状态 = '已完成' WHERE 订单号 = '" + Ddh + "'"
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        sql = "UPDATE 零件信息 SET  状态 = '已完成' WHERE 订单号 = '" + Ddh + "'"
        Dim cm3 As New SqlCommand(sql, cn)
        cm3.ExecuteNonQuery()
        cn.Close()
    End Sub

    '扩展功能模块============================================


End Class