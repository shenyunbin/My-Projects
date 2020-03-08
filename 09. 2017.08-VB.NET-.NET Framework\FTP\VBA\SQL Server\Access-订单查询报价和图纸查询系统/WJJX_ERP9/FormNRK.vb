Imports System.Data.SqlClient
Imports System.Threading

Public Class FormNRK
    Public DGV_Mode As Integer '表格显示模式 1=订单，2=零件
    Dim G_Ddh, G_Kh, G_Xh, G_Th As String

    '设置显示模式
    Dim XSMS As String = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  " ' " or 状态= '生产'  or 状态 ='已完成' "

    Public runThread As Thread
    Dim Thread_ExcelName As String

    '报价单自定义列宽
    Public SetWidth() As Integer = {4, 15, 10, 8, 12, 12, 8, 8, 16, 8, 8, 8, 13, 11, 20}

    Public SQL_Connection As String '= SQL_Connection
    Public SQL_Adress As String

    '按订单分类SQL语句
    Const SQL_CmdDdfl1 = "select 订单信息.状态,零件信息.订单号,订单信息.客户,订单信息.批号,sum(部件信息.计划数量*零件信息.台件) as 计划生产数,sum(仓库信息.现有库存) as 现有库存数,订单信息.备注 " +
                                        "from 零件信息 left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号" +
                                        " left join 订单信息 on  零件信息.订单号=订单信息.订单号 left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
                                       "where "
    Const SQL_CmdDdfl2 = " group by 零件信息.订单号,订单信息.客户,订单信息.批号,订单信息.备注,订单信息.状态 order by 零件信息.订单号 desc"

    '提取零件生产信息SQL语句
    Const SQL_Cmd1 = "select top 500 订单信息.客户,零件信息.订单号,零件信息.型号,零件信息.图号,零件信息.名称,部件信息.计划数量 as 部件计划数,零件信息.台件,部件信息.计划数量*零件信息.台件 as 具体数量,部件信息.计划日期," +
                                    "仓库信息.现有库存,仓库信息.毛坯备注,零件信息.备注3,零件信息.备注2,零件信息.状态  " +
                                    "from 零件信息  " +
                                    "left join 部件信息 " +
                                    "on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                                    "left join 订单信息 " +
                                    "on  零件信息.订单号=订单信息.订单号 " +
                                    "left join 仓库信息 " +
                                    "on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 "

    Const SQL_Cmd12 = " group by 订单信息.客户,零件信息.订单号,零件信息.型号,零件信息.图号,零件信息.名称,部件信息.计划数量,零件信息.台件,仓库信息.现有库存,零件信息.备注3,零件信息.备注2,仓库信息.毛坯备注,零件信息.状态,部件信息.计划日期,部件信息.编号,零件信息.编号 "

    '导出生产单SQL语句
    Const SQL_CmdDcscd = "select 零件信息.图号,零件信息.名称,部件信息.计划数量*零件信息.台件 as 数量,部件信息.计划日期,零件信息.备注3,零件信息.备注2 " +
                              "from 零件信息  " +
                              "left join 部件信息 " +
                              "on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                              "left join 订单信息 " +
                              "on  零件信息.订单号=订单信息.订单号 " +
                              "left join 仓库信息 " +
                              "on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
                              "where 零件信息.订单号= '"



    '定义订单分类各个列位置
    Const DGVDdfl_Zt = 0
    Const DGVDdfl_Ddh = 1
    Const DGVDdfl_Kh = 2
    Const DGVDdfl_Ph = 3
    'Const DGVDdfl_Bz = 3

    '定义部件分类各个列位置
    Const DGVBjfl_Ddh = 0
    Const DGVBjfl_Xh = 1
    Const DGVBjfl_Mc = 2
    Const DGVBjfl_Jhsl = 3
    Const DGVBjfl_Jhrq = 4
    Const DGVBjfl_bz = 5

    '定义零件信息各个列位置
    Const DGVLjxx_Hh = 0
    Const DGVLjxx_Kh = 1
    Const DGVLjxx_Ddh = 2
    Const DGVLjxx_Xh = 3
    Const DGVLjxx_Th = 4
    Const DGVLjxx_Mc = 5
    Const DGVLjxx_Tj = 6
    Const DGVLjxx_Bjjhs = 7
    Const DGVLjxx_Jtsl = 8
    Const DGVLjxx_Jhrq = 9
    Const DGVLjxx_Xykc = 10
    Const DGVLjxx_Mpbz = 11
    Const DGVLjxx_Bz3 = 12
    Const DGVLjxx_Bz2 = 13
    Const DGVLjxx_Zt = 14

    '定义毛坯入库记录各个列
    Const DGVMprkjl_Kh = 0
    Const DGVMprkjl_Th = 1
    Const DGVMprkjl_Mc = 2
    Const DGVMprkjl_Rkrq = 3
    Const DGVMprkjl_Mprks = 4
    Const DGVMprkjl_Sbh = 5

    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////


    '窗口启动时操作
    Private Sub FormNBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)

        LabelBJ.Enabled = FormLogin.GlyMode
        LabelScjl.Enabled = False
        DGV_Mode = 1 '设置显示模式
        SetDVG() '设置表格格式
        ButtonDisp(DGV_Mode)
        DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
    End Sub

    '单击表格显示毛坯入库信息
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Select Case DGV_Mode
            Case 1
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVDdfl_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVDdfl_Ddh).Value
                'DGV_Load(DGV_Mode + 1, G_Ddh, vbNullString, DataGridView2) '载入部件信息
                Exit Select
            Case 2
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVLjxx_Xh).Value
                G_Th = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVLjxx_Th).Value
                Mprkjl_Load(G_Kh, G_Th, DataGridView2) '载入毛坯入库记录
                DataDisp(True) '显示图号及毛坯备注信息
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
                ButtonDisp(DGV_Mode)
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVDdfl_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVDdfl_Ddh).Value
                DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入零部件信息
                LabelMain.Text = "订单" + G_Ddh + " >> 毛坯入库"

                If DataGridView1.Rows.Count = 0 Then '表格为空时退出
                    Exit Select
                End If
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVLjxx_Xh).Value
                G_Th = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGVLjxx_Th).Value
                Mprkjl_Load(G_Kh, G_Th, DataGridView2) '载入毛坯入库记录
                DataDisp(True) '显示图号及毛坯备注信息
                Exit Select
            Case 2

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
                DataGridView2.Columns.Clear()
                DataDisp(False) '清除图号及毛坯备注信息
                LabelMain.Text = "请选择订单以开始毛坯入库 >>"
                Exit Select
        End Select
    End Sub
    '全局操作模块=======================================================


    '各个按钮操作////////////////////////////////////////////////////////////////////////////////////////////

    '按钮A事件
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        '导出生产单
        SaveFileDialogExcel.FileName = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Kh).Value.ToString + "-" + DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString + "-生产计划单"
        SaveFileDialogExcel.ShowDialog()
    End Sub

    '按钮B事件
    Private Sub ButtonB_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        If ButtonB.Text = "图号搜索" Then
            SSTH() '图号搜索
            ButtonB.Text = "退出搜索"
            ButtonB.ForeColor = Color.IndianRed
        Else
            TCSS() '退出搜索
            ButtonB.Text = "图号搜索"
            ButtonB.ForeColor = Color.Black
        End If

    End Sub


    '按钮C事件
    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        '毛坯入库
        Mprk()
        TCSS() '退出搜索
        ButtonB.Text = "图号搜索"
        ButtonB.ForeColor = Color.Black
    End Sub

    '按钮D事件
    Private Sub ButtonD_Click(sender As Object, e As EventArgs) Handles ButtonD.Click
        '导入图纸
        OpenFileDialogPicture.ShowDialog()
    End Sub

    '按钮E事件
    Private Sub ButtonE_Click(sender As Object, e As EventArgs) Handles ButtonE.Click
        '打开图纸
        If FormLogin.OpenPic(G_Kh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString) = False Then
            OpenFileDialogPicture.ShowDialog()
        End If
    End Sub


    '当图号框有更改时退出搜索
    Private Sub TextBoxTh_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTh.TextChanged
        If ButtonB.Text = "退出搜索" Then
            TCSS()
            ButtonB.Text = "图号搜索"
            ButtonB.ForeColor = Color.Black
        End If
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
        Else
            LabelScjl.Enabled = False
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



    '数据处理模块////////////////////////////////////////////////////////////////////////////////////////////

    '毛坯入库模块
    Private Sub Mprk()
        If Not TextBoxTh.Text = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString Then
            MessageBox.Show("输入的图号与下方选择的图号不符，无法入库。请重新在下方选择正确的图号。")
            Exit Sub
        End If
        Dim Y As Integer = 0
        Dim Ddh As String = ""
        Dim Xh As String = ""
        Dim Kh As String = ""
        Dim Th As String = ""
        Dim Mc As String = ""
        Dim Wgrq As String = ""
        Dim Mprks As Integer = 0
        Dim Sbh As String = ""
        Try
            Y = DataGridView1.CurrentCellAddress.Y
            Ddh = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Ddh).Value.ToString
            Xh = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Xh).Value.ToString
            Kh = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Kh).Value.ToString
            Th = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString
            Mc = DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Mc).Value.ToString
            Wgrq = DateTimePickerRkrq.Text
            Sbh = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
            Mprks = TextBoxMprks.Text
        Catch ex As Exception
            MessageBox.Show("未选择零件或毛坯入库数量格式有误。", "操作或输入错误！")
        End Try
        '毛坯入库数为0时不运行！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        If Mprks = 0 Then
            Exit Sub
        End If
        '新增毛坯入库记录
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open()
        Dim cm As New SqlCommand("insert into 入库信息 (客户,图号,名称,入库日期,毛坯入库数,识别号) values('" + Kh + "','" + Th + "','" + Mc + "','" +
             Wgrq + "'," + Mprks.ToString + ",'" + Sbh + "')", cn)
        cm.ExecuteNonQuery()
        cn.Close()
        '完成仓库数量的更改(仓库毛坯入库数增加,如仓库有货则修改信息,否则新增信息 if exists sql语句)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Dim sql As String = "if exists(select * from 仓库信息 where 客户='" + Kh + "' and 图号='" + Th + "') " +
                                        "update 仓库信息 set 毛坯入库数=毛坯入库数+" + Mprks.ToString + ", 现有库存=现有库存+" + Mprks.ToString + ", 识别号 ='" + Sbh + "',毛坯备注='" + TextBoxMpbz.Text + "' where 客户='" + Kh + "' and 图号='" + Th + "' " +
                                        "else insert into 仓库信息 (客户,图号,名称,毛坯入库数,送货数量,现有库存,毛坯备注,识别号) values('" + Kh + "','" + Th + "','" + Mc + "'," + Mprks.ToString + ",0," + Mprks.ToString + ",'" + TextBoxMpbz.Text + "','" + Sbh + "')"
        cn.Open()
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        cn.Close()
        TextBoxMprks.Text = ""
        TextBoxMpbz.Text = ""
        '修改部件入库信息
        GxBjRkjl(Ddh, Xh)
        '载入毛坯入库信息
        Mprkjl_Load(Kh, Th, DataGridView2)
        '载入零部件信息
        DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1)
        If Y > 0 And Y < DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1(1, Y)
        End If
    End Sub


    '修改部件入库记录
    Private Sub GxBjRkjl(Ddh As String, Xh As String)
        Dim sql As String = "select 订单信息.客户,部件信息.名称, isnull( case 零件信息.台件 when 0 then 仓库信息.毛坯入库数 else 仓库信息.毛坯入库数/零件信息.台件 end,0) as 可装配部件数量 from 零件信息 " +
                        "left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                        "left join 订单信息 on  零件信息.订单号=订单信息.订单号 " +
                        "left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
                        "where 零件信息.图号! = 零件信息.型号 and 零件信息.订单号='" + Ddh + "' and 零件信息.型号='" + Xh + "'" +
                        "order by 可装配部件数量 asc"
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter(sql, cn)
        Dim ds As New DataSet
        da.Fill(ds, "可装配部件数量")
        If ds.Tables(0).Rows.Count > 0 Then
            Dim Kh As String = ds.Tables(0).Rows(0).Item(0).ToString
            Dim Mc As String = ds.Tables(0).Rows(0).Item(1).ToString
            Dim Mprks As Integer = ds.Tables(0).Rows(0).Item(2)
            GxBjRkjl_Ck(Kh, Xh, Mc, Mprks)
        Else
            Exit Sub
        End If
    End Sub

    '修改部件入库记录_仓库信息的毛坯入库数
    Private Sub GxBjRkjl_Ck(Kh As String, Th As String, Mc As String, Mprks As String)
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        '完成仓库数量的更改(仓库毛坯入库数增加,如仓库有货则修改信息,否则新增信息 if exists sql语句)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Dim sql As String = "if exists(select * from 仓库信息 where 客户='" + Kh + "' and 图号='" + Th + "') " +
                                        "update 仓库信息 set 毛坯入库数=" + Mprks.ToString + ", 现有库存=" + Mprks.ToString + "-送货数量, 识别号 ='" + Sbh + "',毛坯备注='" + "部套件数量(自动生成)" + "' where 客户='" + Kh + "' and 图号='" + Th + "' " +
                                        "else insert into 仓库信息 (客户,图号,名称,毛坯入库数,送货数量,现有库存,毛坯备注,识别号) values('" + Kh + "','" + Th + "','" + Mc + "'," + Mprks.ToString + ",0," + Mprks.ToString + ",'" + "部套件数量(自动生成)" + "','" + Sbh + "')"
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open()
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        cn.Close()
    End Sub

    '删除毛坯入库记录
    Private Sub LabelScjl_Click(sender As Object, e As EventArgs) Handles LabelScjl.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定的毛坯入库记录？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            '删除毛坯送货记录
            Dim Sbh As String = DataGridView2.CurrentRow.Cells.Item(DGVMprkjl_Sbh).Value.ToString
            If Sbh = "" Then
                MessageBox.Show("未选定毛坯入库记录！", "操作错误！")
                Exit Sub
            End If
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "Delete from 入库信息 where 识别号 = '" + Sbh + "'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()

            '完成仓库数量的更改(仓库毛坯入库数增加,如仓库有货则修改信息,否则新增信息 if exists sql语句)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Dim Mprks As String = DataGridView2.CurrentRow.Cells.Item(DGVMprkjl_Mprks).Value.ToString
            Mprks = 0 - Mprks
            Dim Kh As String = DataGridView2.CurrentRow.Cells.Item(DGVMprkjl_Kh).Value.ToString
            Dim Th As String = DataGridView2.CurrentRow.Cells.Item(DGVMprkjl_Th).Value.ToString
            Dim Ddh As String = G_Ddh
            Dim Xh As String = G_Xh
            Sbh = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
            sql = "update 仓库信息 set 毛坯入库数=毛坯入库数+" + Mprks.ToString + ", 现有库存=现有库存 +" + Mprks.ToString + ", 识别号 ='" + Sbh + "' where 客户='" + Kh + "' and 图号='" + Th + "' "
            cn.Open()
            Dim cm2 As New SqlCommand(sql, cn)
            cm2.ExecuteNonQuery()
            cn.Close()
            '修改部件入库信息
            GxBjRkjl(Ddh, Xh)
            '载入毛坯入库信息
            Mprkjl_Load(Kh, Th, DataGridView2)
            '载入零部件信息
            DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1)
        End If
    End Sub



    '零件信息中筛选图号,搜索图号
    Private Sub SSTH()
        Dim Sslx As Integer
        Dim Ssstring1 As String = "*" + TextBoxTh.Text + "*"
        Sslx = DGVLjxx_Th
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



    '数据处理模块=====================================================


    '信息载入模块//////////////////////////////////////////////////////////////////////////////////////////////
    '载入订单信息
    Public Sub DGV_Load(iMode As Integer, data1 As String, data2 As String, DGV As DataGridView) '1=载入订单，2=载入零件，data1=订单号，data2=部件型号
        DGV.Columns.Clear()
        Select Case iMode
            Case 1 '载入订单列表
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_CmdDdfl1 + XSMS + SQL_CmdDdfl2, cn)
                Dim ds As New DataSet
                da.Fill(ds, "订单分类信息")
                DGV.DataSource = ds.Tables("订单分类信息")
                Exit Select
            Case 2    '载入订单的零件的信息
                Dim Sql As String = vbNullString
                Sql = "where 零件信息.订单号='" + data1 + "' " + SQL_Cmd12 + " order by 零件信息.订单号,部件信息.编号,零件信息.型号,零件信息.编号 asc" '订单信息.状态='生产' and
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_Cmd1 + Sql, cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                ds.Tables(0).Columns.Add("行号")
                '修改列的排序
                ds.Tables(0).Columns("行号").SetOrdinal(DGVLjxx_Hh)
                ds.Tables(0).Columns("客户").SetOrdinal(DGVLjxx_Kh)
                ds.Tables(0).Columns("订单号").SetOrdinal(DGVLjxx_Ddh)
                ds.Tables(0).Columns("型号").SetOrdinal(DGVLjxx_Xh)
                ds.Tables(0).Columns("图号").SetOrdinal(DGVLjxx_Th)
                ds.Tables(0).Columns("名称").SetOrdinal(DGVLjxx_Mc)
                ds.Tables(0).Columns("台件").SetOrdinal(DGVLjxx_Tj)
                ds.Tables(0).Columns("部件计划数").SetOrdinal(DGVLjxx_Bjjhs)
                ds.Tables(0).Columns("具体数量").SetOrdinal(DGVLjxx_Jtsl)
                ds.Tables(0).Columns("计划日期").SetOrdinal(DGVLjxx_Jhrq)
                ds.Tables(0).Columns("现有库存").SetOrdinal(DGVLjxx_Xykc)
                ds.Tables(0).Columns("毛坯备注").SetOrdinal(DGVLjxx_Mpbz)
                ds.Tables(0).Columns("备注3").SetOrdinal(DGVLjxx_Bz3)
                ds.Tables(0).Columns("备注2").SetOrdinal(DGVLjxx_Bz2)
                ds.Tables(0).Columns("状态").SetOrdinal(DGVLjxx_Zt)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ds.Tables(0).Rows(i).Item(DGVLjxx_Hh) = i + 1
                Next
                DGV.DataSource = ds.Tables("零件信息")
                If DGV.RowCount = 0 Then
                    Exit Sub
                End If
                For i = 0 To DGV.RowCount - 1
                    If DGV.Rows(i).Cells.Item(DGVLjxx_Xh).Value.ToString = DGV.Rows(i).Cells.Item(DGVLjxx_Th).Value.ToString Then
                        DGV.Rows(i).DefaultCellStyle.BackColor = Color.LightGray '设置部件为灰色
                    End If
                Next
                Exit Select
        End Select
    End Sub


    '载入毛坯入库记录
    Private Sub Mprkjl_Load(Kh As String, Th As String, DGV As DataGridView)
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select * from 入库信息 where 客户='" + Kh + "' and 图号 ='" + Th + "'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "毛坯入库记录")
        DGV.DataSource = ds.Tables("毛坯入库记录")
    End Sub

    '毛坯入库显示模块
    Private Sub DataDisp(bol As Boolean) 'True=显示，False=清除
        If bol Then
            TextBoxTh.Text = DataGridView1.CurrentRow.Cells(DGVLjxx_Th).Value.ToString
            TextBoxMpbz.Text = DataGridView1.CurrentRow.Cells(DGVLjxx_Mpbz).Value.ToString
        Else
            TextBoxTh.Text = vbNullString
            TextBoxMpbz.Text = vbNullString
        End If
    End Sub

    '信息载入模块=================================================









    '辅助功能模块////////////////////////////////////////////////////////////////////////////

    '各个窗口之间快速转换
    Private Sub LabelBJ_Click(sender As Object, e As EventArgs) Handles LabelBJ.Click
        FormLogin.BJLoad(Me)
    End Sub

    Private Sub LabelSC_Click(sender As Object, e As EventArgs) Handles LabelSC.Click
        'FormLogin.SCLoad(Me)
    End Sub

    Private Sub LabelSH_Click(sender As Object, e As EventArgs) Handles LabelSH.Click
        FormLogin.SHLoad(Me)
    End Sub

    Private Sub LabelCK_Click(sender As Object, e As EventArgs) Handles LabelCK.Click
        FormLogin.CKLoad(Me)
    End Sub

    Private Sub LabelJS_Click(sender As Object, e As EventArgs) Handles LabelJS.Click
        FormLogin.JSLoad(Me)
    End Sub

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1, DataGridView2}
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
                ButtonA.Text = "导出生产单"
                Dim bol As Boolean = False
                ButtonB.Visible = bol
                ButtonC.Visible = bol
                ButtonD.Visible = bol
                ButtonE.Visible = bol

                LabelA.Visible = bol
                LabelB.Visible = bol
                LabelC.Visible = bol
                LabelD.Visible = bol

                TextBoxTh.Visible = bol
                TextBoxMprks.Visible = bol
                TextBoxMpbz.Visible = bol

                DateTimePickerRkrq.Visible = bol

                ButtonSX.Visible = True
                TextBoxSX.Visible = True
                LabelSX.Visible = True

            Case 2
                ButtonB.Text = "图号搜索"
                ButtonB.ForeColor = Color.Black
                'ButtonA.Text = "导出生产单"
                Dim bol As Boolean = True
                ButtonB.Visible = bol
                ButtonC.Visible = bol
                ButtonD.Visible = bol
                ButtonE.Visible = bol

                LabelA.Visible = bol
                LabelB.Visible = bol
                LabelC.Visible = bol
                LabelD.Visible = bol

                TextBoxTh.Visible = bol
                TextBoxMprks.Visible = bol
                TextBoxMpbz.Visible = bol

                DateTimePickerRkrq.Visible = bol

                ButtonSX.Visible = False
                TextBoxSX.Visible = False
                LabelSX.Visible = False
        End Select
    End Sub

    '实时设置部件为灰色
    Private Sub DataGridView1_CellContentClick() Handles DataGridView1.ColumnHeaderMouseClick
        If DGV_Mode = 2 Then
            Dim DGV = DataGridView1
            For i = 0 To DGV.RowCount - 1
                If DGV.Rows(i).Cells.Item(DGVLjxx_Xh).Value.ToString = DGV.Rows(i).Cells.Item(DGVLjxx_Th).Value.ToString Then
                    DGV.Rows(i).DefaultCellStyle.BackColor = Color.LightGray '设置部件为灰色
                End If
            Next
        End If
    End Sub

    '辅助功能模块=============================================



    '扩展功能模块//////////////////////////////////////////////////////////////////////////////////////

    '图片导入和打开功能/辅助功能******************************************

    '导入图片/复制图片到指定路径
    Private Sub OpenFileDialogPicture_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogPicture.FileOk
        FormLogin.SavePic(OpenFileDialogPicture.FileName, G_Kh, DataGridView1.CurrentRow.Cells.Item(DGVLjxx_Th).Value.ToString)
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
    '导出生产单
    Private Function SCDOutput() As DataTable
        '导入生产单
        Dim Ddh As String = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter(SQL_CmdDcscd + Ddh + "' order by 部件信息.编号,零件信息.编号 asc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "生产单预览")
        '增加列
        ds.Tables(0).Columns.Add("序号")
        ds.Tables(0).Columns.Add("材质")
        ds.Tables(0).Columns.Add("热处理")
        ds.Tables(0).Columns.Add("备料")
        ds.Tables(0).Columns.Add("送货")
        ds.Tables(0).Columns.Add("备料规格")
        ds.Tables(0).Columns.Add("下料数量")
        ds.Tables(0).Columns.Add("备注")
        ds.Tables(0).Columns.Add("完工尺寸")
        '修改列的排序
        ds.Tables(0).Columns("序号").SetOrdinal(0)
        ds.Tables(0).Columns("图号").SetOrdinal(1)
        ds.Tables(0).Columns("名称").SetOrdinal(2)
        ds.Tables(0).Columns("数量").SetOrdinal(3)
        ds.Tables(0).Columns("材质").SetOrdinal(4)
        ds.Tables(0).Columns("热处理").SetOrdinal(5)
        ds.Tables(0).Columns("备料").SetOrdinal(6)
        ds.Tables(0).Columns("送货").SetOrdinal(7)
        ds.Tables(0).Columns("备料规格").SetOrdinal(8)
        ds.Tables(0).Columns("下料数量").SetOrdinal(9)
        ds.Tables(0).Columns("备注").SetOrdinal(10)
        ds.Tables(0).Columns("备注3").SetOrdinal(11)
        ds.Tables(0).Columns("完工尺寸").SetOrdinal(12)
        ds.Tables(0).Columns("计划日期").SetOrdinal(13)
        ds.Tables(0).Columns("备注2").SetOrdinal(14)
        '将备注1/2信息转移到前面相应的条目中
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim str2(17) As String
            ds.Tables(0).Rows(i).Item(0) = i + 1 '行号
            F2BJ.InfoInput(ds.Tables(0).Rows(i).Item(14).ToString, str2) '//////与备注信息有关！！！！！
            ds.Tables(0).Rows(i).Item(4) = str2(3) '材质
            ds.Tables(0).Rows(i).Item(5) = str2(11) '热处理
            ds.Tables(0).Rows(i).Item(8) = str2(5) '备料规格
            ds.Tables(0).Rows(i).Item(9) = str2(7) '下料数量
            ds.Tables(0).Rows(i).Item(12) = str2(9) '完工尺寸
            If str2(0) = "外购" Then
                ds.Tables(0).Rows(i).Item(10) = "外购" '备注/外购
            End If
        Next
        SCDOutput = ds.Tables("生产单预览")
        '设置后面几列不可见
        'DataGridViewScdyl.Columns(14).Visible = False
        '导出生产单到表格
    End Function




    '保存对话框结束后新增表格###################################@多线程
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk
        Thread_ExcelName = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Kh).Value.ToString + "-" + DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString + "-生产计划单"
        runThread = New Thread(AddressOf ThreadToExcel)
        FormWait.F = runThread
        FormWait.Show()
        runThread.Start()
        'Dim ExcelName As String = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Kh).Value.ToString + "-" + DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString + "-生产计划单"
        'ExcelOutput(DataGridViewScdyl, ExcelName, SaveFileDialogExcel.FileName)
    End Sub

    '多线程支持函数###################################@多线程
    Private Sub ThreadToExcel()
        ExcelOutput(SCDOutput, Thread_ExcelName, SaveFileDialogExcel.FileName)
    End Sub


    '定义委托-wait1###################################@多线程
    Public Delegate Sub VoidDelegate()
    '定义结束后关闭窗体方法-wait2
    Public Sub FormBJ_Wait_Close()
        FormWait.Close()
    End Sub



    '输出报价单数据到表格###################################@多线程
    Public Sub ExcelOutput(DGV As DataTable, ExcelName As String, FileName As String)
        Dim Unvisible As Integer = 14 '设置不可见的列号
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        Dim yy As Integer
        'try
        WorkBook = myExcel.Workbooks.Add()
        Sheet = WorkBook.Sheets(1)
        '输入表格名称
        Sheet.Name = ExcelName
        '合并单元格设置格式并输入表格标题
        With Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(1, 14))
            .Merge()
            .Font.Size = 20
            .Font.FontStyle = "bold"
            .RowHeight = 31
        End With
        Sheet.Cells(1, 1) = ExcelName
        '输入表格标题栏并加粗
        For y = 0 To DGV.Columns.Count - 1
            If y = Unvisible Then
                Continue For
            End If
            WorkBook.Sheets(1).Cells(2, y + 1) = DGV.Columns(y).ColumnName.ToString
        Next
        Sheet.Range(Sheet.Cells(2, 1), Sheet.Cells(2, DGV.Columns.Count)).Font.FontStyle = "bold"
        '输入表格内容
        For y = 0 To DGV.Columns.Count - 1
            If y = Unvisible Then
                Continue For
            End If
            yy += 1
            Sheet.Range(Sheet.Cells(2, yy), Sheet.Cells(2, yy)).ColumnWidth = SetWidth(yy - 1) 'DGV.Columns(yy).Width / 6 '
            For x = 0 To DGV.Rows.Count - 1
                WorkBook.Sheets(1).Cells(x + 3, yy) = DGV.Rows(x).Item(y).ToString
            Next
        Next
        '设置单元格边框格式并垂直居中水平靠左
        With Sheet.Range(Sheet.Cells(2, 1), Sheet.Cells(DGV.Rows.Count + 2, yy))
            .Borders.LineStyle = 1
            .HorizontalAlignment = -4131 'Left -4131 Right -4152
            .VerticalAlignment = -4108
        End With
        WorkBook.SaveAs(FileName)
        'WorkBook.Close()
        'myExcel.Quit()
        myExcel.Visible = True
        'Catch ex2 As Exception
        'MessageBox.Show("输出报价单失败，请检查错误重新生成。", "操作错误！")
        'End try
        '跨线程关闭等待窗体-wait3
        Me.Invoke(New VoidDelegate(AddressOf FormBJ_Wait_Close))
        runThread.Abort()
    End Sub



    '扩展功能模块================================================


End Class