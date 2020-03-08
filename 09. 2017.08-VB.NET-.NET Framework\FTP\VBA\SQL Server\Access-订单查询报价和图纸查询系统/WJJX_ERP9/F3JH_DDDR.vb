Imports System.Data.SqlClient
Public Class F3JH_DDDR
    Dim iMode As Integer = 1 '状态1=订单,2=零部件


    '设置显示模式
    Dim XSMS As String = "订单信息.状态 ='报价' or 订单信息.状态= '生产'  " ' " or 状态= '生产'  or 状态 ='已完成' "


    '按订单分类SQL语句
    Const SQL_CmdDdfl1 = "select 编号,订单号,客户,批号,备注,状态,计划日期=(select top 1 部件信息.计划日期 from 部件信息 where 部件信息.订单号=订单信息.订单号) from 订单信息 where "
    Const SQL_CmdDdfl2 = "order by 订单信息.订单号 desc"

    '提取零件生产信息SQL语句
    Const SQL_CmdLbjxx = "select 零件信息.型号,零件信息.图号,零件信息.名称,零件信息.台件," +
        "(select top 1 部件信息.计划数量 from 部件信息 where 部件信息.订单号=零件信息.订单号 and 部件信息.型号=零件信息.型号)*零件信息.台件 as 具体计划数," +
        "'' as 单件工时,零件信息.备注3,部件编号=(select top 1 部件信息.编号 from 部件信息 where 部件信息.订单号=零件信息.订单号 and 部件信息.型号=零件信息.型号)," +
        "零件信息.编号 as 零件编号 from 零件信息  "
    '(select top 1 部件信息.计划数量 from 部件信息 where 部件信息.订单号=零件信息.订单号 and 部件信息.型号=零件信息.型号)*零件信息.台件 as 具体计划数,部件信息.计划日期


    '定义订单分类各个列位置
    Const DGVDdfl_Bh = 0
    Const DGVDdfl_Ddh = 1
    Const DGVDdfl_Kh = 2
    Const DGVDdfl_Ph = 3
    Const DGVDdfl_Bz = 4
    Const DGVDdfl_Zt = 5
    Const DGVDdfl_Jhrq = 6


    '定义零件信息各个列位置
    Const DGVLjxx_Bj = 0
    Const DGVLjxx_Xh = 1
    Const DGVLjxx_Th = 2
    Const DGVLjxx_Mc = 3
    Const DGVLjxx_Tj = 4
    Const DGVLjxx_Jtjhs = 5
    Const DGVLjxx_Djgs = 6
    Const DGVLjxx_Bz3 = 7



    '全局操作模块////////////////////////////////////////////////////////////////////////

    '程序载入后操作
    Private Sub FCJH_DDDR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DGV_Load(1, vbNullString, vbNullString, DataGridView1)

    End Sub

    '表格双击后操作
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Rows.Count = 0 Then
            Exit Sub
        End If
        Select Case iMode
            Case 1
                iMode = 2
                TextBoxKh.Text = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Kh).Value.ToString
                TextBoxDdh.Text = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Ddh).Value.ToString
                TextBoxBz.Text = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Bz).Value.ToString
                TextBoxJhrq.Text = DataGridView1.CurrentRow.Cells.Item(DGVDdfl_Jhrq).Value.ToString
                DGV_Load(2, TextBoxDdh.Text, vbNullString, DataGridView1)
            Case Else
                Exit Select
        End Select
    End Sub

    '返回按钮事件
    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Select Case iMode
            Case 2
                iMode = 1
                DGV_Load(1, vbNullString, vbNullString, DataGridView1)
            Case Else
                Exit Select
        End Select
    End Sub


    '全局操作模块===============================================

    '数据处理模块///////////////////////////////////////////////////////////////////////////////////////

    '创建生产单
    Private Sub CreateSCD()
        Dim Cjrq As String = Format(Now(), "yyyy-MM-dd")
        Dim cn As New SqlConnection(F3JH.SQL_Connection)
        cn.Open() '插入前，必须连接 
        Dim Sql As String = "if exists(select * from 新订单信息 where 订单号='" + TextBoxDdh.Text + "') " +
                                                            " update 新订单信息 set 客户='" + TextBoxKh.Text + "' ,订单号 ='" +
                                                           TextBoxDdh.Text + "', 计划日期 ='" + TextBoxJhrq.Text + "', 备注 ='" + TextBoxBz.Text +
                                                             "', 创建日期='" + Cjrq +
                                                             "' where 订单号='" + TextBoxDdh.Text + "'  " +
                                                            "else insert into 新订单信息 (客户,订单号,计划日期,备注,创建日期)" +
                                                            " values('" + TextBoxKh.Text + "','" + TextBoxDdh.Text + "','" + TextBoxJhrq.Text + "','" + TextBoxBz.Text + "','" + Cjrq + "')"
        Dim cm As New SqlCommand(Sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()

        Dim DT(9) As String
        DT(0) = TextBoxDdh.Text
        For i = 0 To DataGridView1.Rows.Count - 1

            For ii = 0 To 7  'DT(1-8) 为1标记2型号3图号4名称5台件6具体计划数7单件工时8备注,0为订单号
                DT(ii + 1) = DataGridView1.Rows(i).Cells.Item(ii).Value.ToString
            Next
            Dim cn2 As New SqlConnection(F3JH.SQL_Connection)
            cn2.Open() '插入前，必须连接 
            Sql = "if exists(select * from 新零部件信息 where 订单号='" + DT(0) + "' and 型号='" + DT(2) + "' and 图号='" + DT(3) + "') " +
                                                            "update 新零部件信息 set 零部件标记='" + DT(1) + "' ,名称 ='" +
                                                           DT(4) + "', 台件 ='" + DT(5) + "', 具体计划数 ='" + DT(6) +
                                                             "', 单件工时 ='" + DT(7) + "',备注 ='" + DT(8) +
                                                             "' where 订单号='" + DT(0) + "' and 型号='" + DT(2) + "' and 图号='" + DT(3) + "'  " +
                                                            "else insert into 新零部件信息 (订单号,零部件标记,型号,图号,名称,台件,具体计划数,单件工时,备注,加工分配,已加工,组装分配,已组装,送货分配,已送货,毛坯购入计划,毛坯已购,成品购入计划,成品已购)" +
                                                            " values('" + DT(0) + "','" + DT(1) + "','" + DT(2) + "','" + DT(3) + "','" + DT(4) + "','" + DT(5) + "','" + DT(6) + "','" + DT(7) + "','" + DT(8) + "','0','0','0','0','0','0','0','0','0','0')"
            'Dim sql As String = "UPDATE 仓库信息 SET  毛坯入库数 = " + Mprks.ToString + ", 送货数量= " + Shsl.ToS11tring + " WHERE 客户 = '" + Kh + "' and 图号='" + Th + "'"
            Dim cm2 As New SqlCommand(Sql, cn2)
            cm2.ExecuteNonQuery()
            cn2.Close()
        Next
    End Sub

    '数据处理模块===================================================



    '信息载入模块//////////////////////////////////////////////////////////////////////////////////////////////
    '载入订单信息
    Public Sub DGV_Load(iMode As Integer, data1 As String, data2 As String, DGV As DataGridView) '1=载入订单，2=载入零件，data1=订单号，data2=部件型号
        DGV.Columns.Clear()
        Select Case iMode
            Case 1 '载入订单列表
                Dim cn As New SqlConnection(F3JH.SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_CmdDdfl1 + XSMS + SQL_CmdDdfl2, cn)
                Dim ds As New DataSet
                da.Fill(ds, "订单分类信息")
                DGV.DataSource = ds.Tables("订单分类信息")
                Exit Select
            Case 2    '载入订单的零件的信息
                Dim Sql As String = vbNullString
                Sql = "where 零件信息.订单号='" + data1 + "' order by 部件编号,零件编号 asc" '订单信息.状态='生产' and
                Dim cn As New SqlConnection(F3JH.SQL_Connection)
                Dim da As New SqlDataAdapter(SQL_CmdLbjxx + Sql, cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                ds.Tables(0).Columns.Add("标记")
                '修改列的排序
                ds.Tables(0).Columns("标记").SetOrdinal(DGVLjxx_Bj)
                ds.Tables(0).Columns("型号").SetOrdinal(DGVLjxx_Xh)
                ds.Tables(0).Columns("图号").SetOrdinal(DGVLjxx_Th)
                ds.Tables(0).Columns("名称").SetOrdinal(DGVLjxx_Mc)
                ds.Tables(0).Columns("台件").SetOrdinal(DGVLjxx_Tj)
                ds.Tables(0).Columns("具体计划数").SetOrdinal(DGVLjxx_Jtjhs)
                ds.Tables(0).Columns("单件工时").SetOrdinal(DGVLjxx_Djgs)
                ds.Tables(0).Columns("备注3").SetOrdinal(DGVLjxx_Bz3)
                ds.Tables(0).Columns.Remove("部件编号")
                ds.Tables(0).Columns.Remove("零件编号")
                DGV.DataSource = ds.Tables("零件信息")
                If DGV.RowCount = 0 Then
                    Exit Sub
                End If
                For i = 0 To DGV.RowCount - 1
                    If DGV.Rows(i).Cells.Item(DGVLjxx_Xh).Value.ToString = DGV.Rows(i).Cells.Item(DGVLjxx_Th).Value.ToString Then
                        DGV.Rows(i).DefaultCellStyle.BackColor = Color.LightGray '设置部件为灰色
                        DGV.Rows(i).Cells.Item(0).Value = (i + 1001).ToString + "A"
                    ElseIf DGV.Rows(i - 1).Cells.Item(0).Value = (i + 1000).ToString + "A" Then
                        DGV.Rows(i - 1).Cells.Item(0).Value = (i + 1000).ToString + "B"
                        DGV.Rows(i).Cells.Item(0).Value = (i + 1001).ToString + "C"
                    Else
                        DGV.Rows(i).Cells.Item(0).Value = (i + 1001).ToString + "C"
                    End If
                Next
                Exit Select
        End Select
        SetDVG2()
    End Sub
    '信息载入模块=====================================================




    '辅助功能模块////////////////////////////////////////////////////////////////////////////


    '设置图表格式函数
    Private Sub SetDVG2()
        Dim dgv = DataGridView1
        dgv.ReadOnly = True
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToOrderColumns = False
        dgv.AllowUserToResizeRows = False
        For ii = 0 To dgv.Columns.Count - 1
            dgv.Columns.Item(ii).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        If iMode = 2 Then
            dgv.ReadOnly = False
            dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
            For i = 0 To 5
                dgv.Columns(i).ReadOnly = True
            Next
            For i = 6 To 7
                dgv.Columns(i).DefaultCellStyle.ForeColor = Color.Red
                dgv.Columns(i).DefaultCellStyle.Font = New Font(DataGridView1.Font, FontStyle.Bold)
                dgv.Columns(i).HeaderCell.Style.Font = New Font(DataGridView1.Font, FontStyle.Bold)
                dgv.Columns(i).ReadOnly = False
            Next
        End If
    End Sub

    '辅助功能模块==============================================


    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        Select Case iMode
            Case 2
                CreateSCD()
                MessageBox.Show("导入到生产单成功!")
            Case Else
                Exit Sub
        End Select
    End Sub
End Class