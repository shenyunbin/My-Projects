Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Net

Public Class F2BJ
    Public DGV_Mode As Integer 'DGV显示模式 1=订单，2=部件，3=零件
    Public G_Kh, G_Ddh, G_Xh, G_Zt, G_Jhsl As String
    '设置显示模式
    Dim XSMS As String = "状态 ='报价' or 状态= '生产'  "  ' " or 状态= '生产'  or 状态 ='已完成' "
    Public runThread As Thread
    Dim Thread_ExcelName As String
    Dim ExcelProcess As String
    '报价单自定义列宽
    Public SetWidth() As Integer = {4, 15, 8, 8, 8, 12, 8, 8, 8, 8, 6, 8, 8, 10, 6, 6, 8, 10, 10, 11}
    Dim DataGridView5 As New DataGridView
    Dim Bjdbt As String = vbNullString

    Public SQL_Connection As String '= "Data Source=(local);Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String

    Dim DdxxOrder As String = " order by 状态,编号,订单号 asc"

    Dim Leftjoin = " left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                             "left join 订单信息 on  零件信息.订单号=订单信息.订单号" +
                             " left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 "

    Dim Leftjoin2 = "left join 订单信息 on  部件信息.订单号=订单信息.订单号"

    '定义订单信息表每列所在位置
    Public Const DGV1_Bh = 5
    Public Const DGV1_Ddh = 1
    Public Const DGV1_Kh = 2
    Public Const DGV1_Ph = 3
    Public Const DGV1_Bz = 4
    Public Const DGV1_Zt = 0
    '定义部件信息表每列所在位置
    Public Const DGV2_Hh = 0
    Public Const DGV2_Bh = 1
    Public Const DGV2_Xh = 2
    Public Const DGV2_Mc = 3
    Public Const DGV2_Hsdj = 4
    Public Const DGV2_Jhsl = 5
    Public Const DGV2_Jhrq = 6
    Public Const DGV2_Hszj = 7
    Public Const DGV2_Bz0 = 8
    Public Const DGV2_Zt = 9
    Public Const DGV2_Ddh = 10
    Public Const DGV2_Sbh = 11
    '定义零件信息表每列所在位置
    Public Const DGV3_Hh = 0
    Public Const DGV3_Bh = 1
    Public Const DGV3_Th = 2
    Public Const DGV3_Mc = 3
    Public Const DGV3_Jtsl = 4
    Public Const DGV3_Clj = 5
    Public Const DGV3_Xlf = 6
    Public Const DGV3_Jjgf = 7
    Public Const DGV3_Rclf = 8
    Public Const DGV3_Bmclf = 9
    Public Const DGV3_Qtjgf = 10
    Public Const DGV3_Sfgz = 11
    Public Const DGV3_Hsdj = 12
    Public Const DGV3_Hszj = 13
    Public Const DGV3_Bz1 = 14
    Public Const DGV3_Bz2 = 15
    Public Const DGV3_Bz3 = 16
    Public Const DGV3_Zt = 17
    Public Const DGV3_Ddh = 18
    Public Const DGV3_Xh = 19
    Public Const DGV3_Sbh = 20


    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////

    '窗口启动时操作
    Private Sub FormNBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)

        LabelBJ.Enabled = FormLogin.GlyMode
        ButtonC.Enabled = False
        DGV_Mode = 1 '设置显示模式
        SetDVG() '设置表格格式
        ButtonDisp(DGV_Mode)
        DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息

    End Sub

    '单击表格显示零件信息
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Select Case DGV_Mode
            Case 1
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Ddh).Value
                G_Zt = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Zt).Value
                DGV_Load(DGV_Mode + 1, G_Ddh, vbNullString, DataGridView2) '载入部件信息
                Exit Select
            Case 2
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                G_Jhsl = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Jhsl).Value
                DGV_Load(DGV_Mode + 1, G_Ddh, G_Xh, DataGridView2) '载入零件信息
                Exit Select
            Case 3
                LJXQDisp(DataGridView2)
                Exit Select
        End Select

    End Sub

    '双击表格到下一级
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Select Case DGV_Mode
            Case 1
                DGV_Mode = 2
                ButtonDisp(DGV_Mode)
                G_Kh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Kh).Value
                G_Ddh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Ddh).Value
                G_Zt = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV1_Zt).Value
                DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
                LabelMain.Text = "订单" + G_Ddh + " >> 部件信息"
                If DataGridView1.Rows.Count = 0 Then '表格为空时退出
                    Exit Select
                End If
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                G_Jhsl = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Jhsl).Value
                DGV_Load(DGV_Mode + 1, G_Ddh, G_Xh, DataGridView2) '载入零件信息
                Exit Select
            Case 2
                DGV_Mode = 3
                ButtonDisp(DGV_Mode)
                G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                G_Jhsl = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Jhsl).Value
                DGV_Load(DGV_Mode, G_Ddh, G_Xh, DataGridView1) '载入零件信息
                LabelMain.Text = "订单" + G_Ddh + " >> 部件" + G_Xh + " >> 零件信息"
                LJXQDisp(DataGridView2)
                Exit Select
            Case 3
                'DGV_Mode = 1
                Exit Select
        End Select
    End Sub

    '单击返回到上一级
    Private Sub LabelBack_Click(sender As Object, e As EventArgs) Handles LabelBack.Click, ButtonBack.Click
        Select Case DGV_Mode
            Case 1
                If ButtonSX.Text = "退出筛选" Then
                    XSMS = "状态 ='报价' or 状态= '生产'  "
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    ButtonSX.Text = "筛选订单"
                    ButtonSX.ForeColor = Color.Black
                End If
                Exit Select
            Case 2
                DGV_Mode = 1
                ButtonDisp(DGV_Mode)
                DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                LabelMain.Text = "订单信息 >>"
                Exit Select
            Case 3
                DGV_Mode = 2
                ButtonDisp(DGV_Mode)
                DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1) '载入部件信息
                LabelMain.Text = "订单" + G_Ddh + " >> 部件信息 >>"
                Exit Select
        End Select
    End Sub
    '全局操作模块===============================================================



    '各个按钮操作////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '按钮A事件
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        Select Case DGV_Mode
            Case 1 '新增订单
                FormNBJ_DDBJ.XZMode = True
                FormNBJ_DDBJ.Show()
                Exit Select
            Case 2 '新增部件
                FormNBJ_BJBJ.XZMode = True
                FormNBJ_BJBJ.Show()
                Exit Select
            Case 3 '新增零件
                FormNBJ_LJBJ.XZMode = True
                FormNBJ_LJBJ.Show()
                Exit Select
        End Select
    End Sub

    '按钮B事件
    Private Sub ButtonB_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '修改订单
                FormNBJ_DDBJ.XZMode = False
                FormNBJ_DDBJ.Show()
                Exit Select
            Case 2 '修改部件
                FormNBJ_BJBJ.XZMode = False
                FormNBJ_BJBJ.Show()
                Exit Select
            Case 3 '修改零件
                If DataGridView1.CurrentRow.Cells(DGV3_Bh).Value = 0 Then
                    MessageBox.Show("修改失败！此为部件，此处无法修改，请到上一级部件信息中修改本部件的信息。")
                    Exit Select
                End If
                FormNBJ_LJBJ.XZMode = False
                FormNBJ_LJBJ.Show()
                Exit Select
        End Select
    End Sub

    '按钮C事件
    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '删除订单
                Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定的订单 [ " +
                                                       DataGridView1.CurrentRow.Cells.Item(DGV1_Ddh).Value.ToString +
                                                       " ]？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
                If MsgOk = DialogResult.OK Then
                    Dim i As Integer
                    Try
                        '获取之前活动的单元格
                        i = DataGridView1.CurrentCellAddress.Y
                    Catch ex As Exception
                    End Try
                    '删除订单信息
                    DeleteDdxx(DataGridView1.CurrentRow.Cells.Item(DGV1_Ddh).Value.ToString)
                    '载入订单信息
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1)
                    If i > 0 Then
                        DataGridView1.CurrentCell = DataGridView1(0, i - 1)
                    End If
                End If
                Exit Select
            Case 2 '删除部件
                Dim MsgOk2 As Integer = MessageBox.Show("是否确认删除选定的部件 [ " +
                                                        DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString +
                                                        DataGridView1.CurrentRow.Cells.Item(DGV2_Mc).Value.ToString +
                                                        " ]？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
                If MsgOk2 = DialogResult.OK Then
                    Dim i As Integer = 0
                    Try
                        '获取之前活动的单元格
                        i = DataGridView1.CurrentCellAddress.Y
                    Catch ex As Exception
                    End Try
                    '删除部件信息
                    DeleteBjxx(G_Ddh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString)
                    '载入部件信息
                    DGV_Load(DGV_Mode, G_Ddh, vbNullString, DataGridView1)
                    If i > 1 Then
                        '设置之前的活动单元格
                        DataGridView1.CurrentCell = DataGridView1(0, i - 1)
                        G_Xh = DataGridView1.Rows(DataGridView1.CurrentCellAddress.Y).Cells(DGV2_Xh).Value
                        '载入零件信息
                        DGV_Load(DGV_Mode + 1, G_Ddh, G_Xh, DataGridView2)
                    End If
                End If
                Exit Select
            Case 3 '删除零件
                If DataGridView1.CurrentRow.Cells(DGV3_Bh).Value = 0 Then
                    MessageBox.Show("删除失败！此为部件，此处无法删除，请到上一级部件信息中删除本部件的信息。")
                    Exit Select
                End If
                Dim MsgOk2 As Integer = MessageBox.Show("是否确认删除选定的部件 [ " +
                        DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString +
                        DataGridView1.CurrentRow.Cells.Item(DGV2_Mc).Value.ToString +
                        " ]？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
                If MsgOk2 = DialogResult.OK Then
                    Dim i As Integer = 0
                    Try
                        '获取之前活动的单元格
                        i = DataGridView1.CurrentCellAddress.Y
                    Catch ex As Exception
                    End Try
                    '删除零件
                    DeleteLjxx(G_Ddh, G_Xh, DataGridView1.CurrentRow.Cells.Item(DGV3_Th).Value.ToString, DataGridView1.CurrentRow.Cells.Item(DGV3_Sbh).Value.ToString)
                    '载入零件信息
                    DGV_Load(DGV_Mode, G_Ddh, G_Xh, DataGridView1)
                    If i > 1 Then
                        '设置之前的活动单元格
                        DataGridView1.CurrentCell = DataGridView1(0, i - 1)

                        '载入零件详细信息（后补！！！）

                    End If
                End If
                Exit Select
        End Select
    End Sub

    '按钮D事件
    Private Sub ButtonD_Click(sender As Object, e As EventArgs) Handles ButtonD.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '导入Excel
                If FormSet.Visible = False Then
                    FormSet.SQL_Connection = SQL_Connection
                    FormSet.SQL_Adress = SQL_Adress
                    FormSet.WindowState = FormWindowState.Normal
                    FormSet.Opacity = 0
                    FormSet.Show()
                    FormSet.Hide()
                    FormSet.Opacity = 1
                End If
                FormSet.OpenFileDialogExcel.ShowDialog()
                Exit Select
            Case 2 '导入图片
                OpenFileDialogPicture.ShowDialog()
                Exit Select
            Case 3 '导入图片
                OpenFileDialogPicture.ShowDialog()
                Exit Select
        End Select
    End Sub

    '按钮E事件
    Private Sub ButtonE_Click(sender As Object, e As EventArgs) Handles ButtonE.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '删除当日导入的数据
                Dim Rq As String = Format(Now(), "yyyy-MM-dd")
                Dim MsgOk As Integer = MessageBox.Show("是否确认删除当日 [ " + Rq + " ] 导入的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
                If MsgOk = DialogResult.OK Then
                    Dim sql As String
                    Dim cn As New SqlConnection(SQL_Connection)
                    cn.Open() '插入前，必须连接  
                    sql = "delete from 零件信息 where 状态 like " & "'%" & Rq & "%' and 订单号 = '导入'"
                    Dim cm As New SqlCommand(sql, cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MessageBox.Show("当日导入的数据删除数据成功！")
                End If
                Exit Select
            Case 2 '打开图片按钮
                If FormLogin.OpenPic(G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString) = False Then
                    OpenFileDialogPicture.ShowDialog()
                End If
                Exit Select
            Case 3 '打开图片按钮
                If FormLogin.OpenPic(G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV3_Th).Value.ToString) = False Then
                    OpenFileDialogPicture.ShowDialog()
                End If
                Exit Select
        End Select
    End Sub

    '按钮F事件
    Private Sub ButtonF_Click(sender As Object, e As EventArgs) Handles ButtonF.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '进入生产
                Dim i As Integer
                Try
                    '获取之前活动的单元格
                    i = DataGridView1.CurrentCellAddress.Y
                Catch ex As Exception
                End Try
                '订单进入生产操作
                DdJrsc(DataGridView1.CurrentRow.Cells.Item(DGV1_Ddh).Value.ToString)
                '载入订单信息
                DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1)
                If i > 0 Then
                    DataGridView1.CurrentCell = DataGridView1(0, i)
                End If
                MessageBox.Show("所选订单状态已改为""生产""。")
                Exit Select
            Case 2

                Exit Select
            Case 3

                Exit Select
        End Select
    End Sub

    '按钮G事件
    Private Sub ButtonG_Click(sender As Object, e As EventArgs) Handles ButtonG.Click
        If DataGridView1.Rows.Count = 0 Then '表格为空时退出
            Exit Sub
        End If
        Select Case DGV_Mode
            Case 1 '导出订单
                '设置报价单标题
                Bjdbt = DataGridView1.CurrentRow.Cells(DGV1_Kh).Value.ToString + "-" + DataGridView1.CurrentRow.Cells(DGV1_Ddh).Value.ToString + "-报价单"
                '导出报价单到DGV5
                BjdOutput()
                '自动设置报价单标题
                SaveFileDialogExcel.FileName = Bjdbt
                SaveFileDialogExcel.ShowDialog()
                Exit Select
            Case 2

                Exit Select
            Case 3

                Exit Select
        End Select
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
            XSMS = "状态 ='报价' or 状态= '生产'  " + " or 状态 ='已完成' "
        Else
            XSMS = "状态 ='报价' or 状态= '生产'  "
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
            ButtonC.Enabled = True
        Else
            ButtonC.Enabled = False
        End If
    End Sub

    '筛选订单
    Private Sub ButtonSX_Click(sender As Object, e As EventArgs) Handles ButtonSX.Click
        If ButtonSX.Text = "筛选订单" Then
            CheckBox1.Checked = False
            XSMS = "订单号 like '%" + TextBoxSX.Text + "%' or 客户 like '%" + TextBoxSX.Text + "%'or 批号 like '%" + TextBoxSX.Text + "%'"
            Select Case DGV_Mode
                Case 1 '重新导入订单
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    Exit Select
            End Select
            ButtonSX.Text = "退出筛选"
            ButtonSX.ForeColor = Color.IndianRed
        Else
            XSMS = "状态 ='报价' or 状态= '生产'  "
            Select Case DGV_Mode
                Case 1 '重新导入订单
                    DGV_Load(DGV_Mode, vbNullString, vbNullString, DataGridView1) '载入订单信息
                    Exit Select
            End Select
            ButtonSX.Text = "筛选订单"
            ButtonSX.ForeColor = Color.Black
        End If
    End Sub

    '各个按钮操作=======================================================================


    '信息载入模块/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '载入订单信息
    Public Sub DGV_Load(iMode As Integer, data1 As String, data2 As String, DGV As DataGridView)
        Select Case iMode
            Case 1
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter("select 状态,订单号,客户,批号,备注,编号 from 订单信息  where " + XSMS + DdxxOrder, cn)
                Dim ds As New DataSet
                da.Fill(ds, "订单信息")
                DGV.Columns.Clear()
                DGV.DataSource = ds.Tables("订单信息")
                Exit Select
            Case 2
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter("select * from 部件信息 where 订单号 = '" + data1 + "' order by 编号 asc ", cn)
                Dim ds As New DataSet
                da.Fill(ds, "部件信息")
                '增加列
                ds.Tables(0).Columns.Add("行号")
                ds.Tables(0).Columns.Add("含税单价")
                ds.Tables(0).Columns.Add("含税总价")
                '修改列的排序
                ds.Tables(0).Columns("行号").SetOrdinal(DGV2_Hh)
                ds.Tables(0).Columns("编号").SetOrdinal(DGV2_Bh)
                ds.Tables(0).Columns("型号").SetOrdinal(DGV2_Xh)
                ds.Tables(0).Columns("名称").SetOrdinal(DGV2_Mc)
                ds.Tables(0).Columns("含税单价").SetOrdinal(DGV2_Hsdj)
                ds.Tables(0).Columns("计划数量").SetOrdinal(DGV2_Jhsl)
                ds.Tables(0).Columns("计划日期").SetOrdinal(DGV2_Jhrq)
                ds.Tables(0).Columns("含税总价").SetOrdinal(DGV2_Hszj)
                ds.Tables(0).Columns("备注").SetOrdinal(DGV2_Bz0)
                ds.Tables(0).Columns("状态").SetOrdinal(DGV2_Zt)
                ds.Tables(0).Columns("订单号").SetOrdinal(DGV2_Ddh)
                ds.Tables(0).Columns("识别号").SetOrdinal(DGV2_Sbh)
                '填写行号和价格
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Hsjg As Double = 0
                    '填写行号
                    ds.Tables(0).Rows(i).Item(DGV2_Hh) = i + 1
                    '填写含税部件单价  
                    Dim da2 As New SqlDataAdapter("select 台件,含税单价 from 零件信息 where 订单号 = '" + ds.Tables(0).Rows(i).Item(DGV2_Ddh) + "' AND 型号 = '" +
                                  ds.Tables(0).Rows(i).Item(DGV2_Xh) + "' order by 编号 asc", cn)
                    Dim ds2 As New DataSet
                    da2.Fill(ds2, "零件信息")
                    For ii = 0 To ds2.Tables(0).Rows.Count - 1
                        Try
                            Hsjg += ds2.Tables(0).Rows(ii).Item(0) * ds2.Tables(0).Rows(ii).Item(1)
                        Catch
                        End Try
                    Next
                    ds.Tables(0).Rows(i).Item(DGV2_Hsdj) = Hsjg '含税单价
                    '填写含税总价
                    ds.Tables(0).Rows(i).Item(DGV2_Hszj) = ds.Tables(0).Rows(i).Item(DGV2_Hsdj) * ds.Tables(0).Rows(i).Item(DGV2_Jhsl)
                Next
                DGV.Columns.Clear()
                DGV.DataSource = ds.Tables("部件信息")
                Exit Select
            Case 3
                Dim cn As New SqlConnection(SQL_Connection)
                Dim da As New SqlDataAdapter("select * from 零件信息 where 订单号 = '" + data1 + "' AND 型号 = '" + data2 + "' order by 编号 asc", cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                '增加列
                ds.Tables(0).Columns.Add("行号")
                ds.Tables(0).Columns.Add("含税总价")
                '修改列的排序
                'ds.Tables(0).Columns("行号").DataType = System.Type.GetType("System.Integer") ''''''行号的数据类型  错误
                ds.Tables(0).Columns("行号").SetOrdinal(DGV3_Hh)
                ds.Tables(0).Columns("编号").SetOrdinal(DGV3_Bh)
                ds.Tables(0).Columns("图号").SetOrdinal(DGV3_Th)
                ds.Tables(0).Columns("名称").SetOrdinal(DGV3_Mc)
                ds.Tables(0).Columns("台件").SetOrdinal(DGV3_Jtsl)
                ds.Tables(0).Columns("材料价").SetOrdinal(DGV3_Clj)
                ds.Tables(0).Columns("下料费").SetOrdinal(DGV3_Xlf)
                ds.Tables(0).Columns("精加工费").SetOrdinal(DGV3_Jjgf)
                ds.Tables(0).Columns("热处理费").SetOrdinal(DGV3_Rclf)
                ds.Tables(0).Columns("表面处理费").SetOrdinal(DGV3_Bmclf)
                ds.Tables(0).Columns("其他加工费").SetOrdinal(DGV3_Qtjgf)
                ds.Tables(0).Columns("实付工资").SetOrdinal(DGV3_Sfgz)
                ds.Tables(0).Columns("含税单价").SetOrdinal(DGV3_Hsdj)
                ds.Tables(0).Columns("含税总价").SetOrdinal(DGV3_Hszj)
                ds.Tables(0).Columns("备注1").SetOrdinal(DGV3_Bz1)
                ds.Tables(0).Columns("备注2").SetOrdinal(DGV3_Bz2)
                ds.Tables(0).Columns("备注3").SetOrdinal(DGV3_Bz3)
                ds.Tables(0).Columns("状态").SetOrdinal(DGV3_Zt)
                ds.Tables(0).Columns("订单号").SetOrdinal(DGV3_Ddh)
                ds.Tables(0).Columns("型号").SetOrdinal(DGV3_Xh)
                ds.Tables(0).Columns("识别号").SetOrdinal(DGV3_Sbh)
                '写入行号和含税总价
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ds.Tables(0).Rows(i).Item(DGV3_Hh) = i + 1
                    Try
                        ds.Tables(0).Rows(i).Item(DGV3_Hszj) = ds.Tables(0).Rows(i).Item(DGV3_Hsdj) * ds.Tables(0).Rows(i).Item(DGV3_Jtsl)
                    Catch
                        'ds.Tables(0).Rows(i).Item(DGV3_Hszj) = 0
                    End Try
                Next
                DGV.Columns.Clear()
                DGV.DataSource = ds.Tables("零件信息")
                Try
                    DGV.Rows(0).DefaultCellStyle.BackColor = Color.LightGray '''''''''''''''''''''''''
                Catch ex As Exception
                End Try
                Exit Select
        End Select
    End Sub

    '载入零件信息详情
    Private Sub LJXQDisp(DGV As DataGridView)
        DGV.Columns.Clear()
        Dim CH() As String = {"外购", "备料", "材质", "备料规格", "下料数量", "完工尺寸", "热处理", "工序", "毛坯重量", "毛坯单价", "材料价", "准备工时", "合计工时", "具体数量", "精加工费"}
        'Dim CH1() As String = {"外购", "自备料", "来料"}
        'Dim CH2() As String = {"材质", "备料规格", "下料数量", "完工尺寸", "热处理", "工序"}
        'Dim CH3() As String = {"毛坯重量", "毛坯单价", "材料价", "准备工时", "合计工时", "具体数量", "精加工费"}

        For i = 0 To CH.Length - 1
            Dim Col As New DataGridViewTextBoxColumn
            Col.HeaderText = CH(i)
            DGV.Columns.Add(Col)
        Next

        Dim DT1(15) As String

        Dim str(17) As String
        Dim str1(7) As String
        '提取备注1的信息
        InfoInput(DataGridView1.CurrentRow.Cells(DGV3_Bz1).Value.ToString, str1)

        '提取备注2的信息
        InfoInput(DataGridView1.CurrentRow.Cells(DGV3_Bz2).Value.ToString, str)
        If str(0) = "外购" Then
            DT1(0) = "是"
        Else
            DT1(0) = "否"
        End If
        If str(1) = "自" Then
            DT1(1) = "自备料"
        ElseIf str(1) = "来料" Then
            DT1(1) = "来料"
        Else
            DT1(1) = ""
        End If

        DT1(2) = str(3) '材质
        DT1(3) = str(5) '备料规格
        DT1(4) = str(7) '下料数量
        DT1(5) = str(9) '完工尺寸
        DT1(6) = str(11) '热处理
        DT1(7) = str(13) '工序
        DT1(8) = str(15) '毛坯重量
        DT1(9) = str1(1) '毛坯单价
        DT1(10) = DataGridView1.CurrentRow.Cells(DGV3_Clj).Value.ToString '材料价
        DT1(11) = str1(3) '准备工时
        DT1(12) = str1(5) '合计工时
        DT1(13) = G_Jhsl * DataGridView1.CurrentRow.Cells(DGV3_Jtsl).Value.ToString '改计划数量为具体数量
        DT1(14) = DataGridView1.CurrentRow.Cells(DGV3_Jjgf).Value.ToString '精加工费

        'TextBoxBz3.Text = DataGridView1.CurrentRow.Cells(DGV3_Bz3).Value.ToString
        'Dim cm As CurrencyManager = CType(BindingContext(DGV.DataSource), CurrencyManager)
        'cm.SuspendBinding()
        Dim index As Integer
        DGV.DataSource = Nothing
        index = DGV.Rows.Add
        For i = 0 To 14
            DGV.Rows(index).Cells.Item(i).Value = DT1(i)
        Next
    End Sub

    '长字符串分割模块
    Public Function InfoInput(inputString As String, str() As String)
        Dim ii As Integer = 0
        For i = 0 To inputString.Length - 1
            If inputString.Chars(i) = Chr(124) Or inputString.Chars(i) = Chr(58) Then
                str(ii) = Trim(str(ii))
                ii += 1
            Else
                str(ii) += inputString.Chars(i)
            End If
        Next
        str(ii) = Trim(str(ii))
        Return ii
    End Function

    '信息载入模块========================================================


    '数据处理模块//////////////////////////////////////////////////////////////////////////////////////////////
    '删除订单信息822
    Private Sub DeleteDdxx(Ddh As String)
        If Ddh = "" Then
            MessageBox.Show("订单号未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "Delete from 零件信息 where 订单号='" + Ddh + "'"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        sql = "Delete from 部件信息 where 订单号='" + Ddh + "'"
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        sql = "Delete from 订单信息 where 订单号='" + Ddh + "'"
        Dim cm3 As New SqlCommand(sql, cn)
        cm3.ExecuteNonQuery()
        cn.Close()
    End Sub

    '进入生产
    Public Sub DdJrsc(Ddh As String)
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "UPDATE 订单信息 SET  状态 = '生产' WHERE 订单号 = '" + Ddh + "'"
        Dim cm1 As New SqlCommand(sql, cn)
        cm1.ExecuteNonQuery()
        sql = "UPDATE 部件信息 SET 状态 = '生产' WHERE 订单号 = '" + Ddh + "'"
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        sql = "UPDATE 零件信息 SET  状态 = '生产' WHERE 订单号 = '" + Ddh + "'"
        Dim cm3 As New SqlCommand(sql, cn)
        cm3.ExecuteNonQuery()
        cn.Close()
    End Sub

    '删除部件信息
    Private Sub DeleteBjxx(Ddh As String, Xh As String)
        If Ddh = "" Or Xh = "" Then
            MessageBox.Show("订单号或者型号未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "DELETE FROM 零件信息 WHERE 型号 = '" + Xh + "' AND 订单号 = '" + Ddh + "'"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        sql = "DELETE FROM 部件信息 WHERE 型号 = '" + Xh + "' AND 订单号 = '" + Ddh + "'"
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        cn.Close()
    End Sub

    '删除零件信息
    Private Sub DeleteLjxx(Ddh As String, Xh As String, Th As String, Sbh As String)
        If Th = "" Then
            MessageBox.Show("图号未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "Delete from 零件信息 where 图号 = '" + Th + "' AND 订单号 = '" + Ddh + "' AND 型号 = '" + Xh + "' and 识别号='" + Sbh + "'"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub

    '数据处理模块======================================================




    '扩展功能模块//////////////////////////////////////////////////////////////////////////////////////

    '图片导入和打开功能/辅助功能******************************************

    '导入图片/复制图片到指定路径
    Private Sub OpenFileDialogPicture_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogPicture.FileOk
        Select Case DGV_Mode
            Case 2
                FormLogin.SavePic(OpenFileDialogPicture.FileName, G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV2_Xh).Value.ToString)
            Case 3
                FormLogin.SavePic(OpenFileDialogPicture.FileName, G_Kh, DataGridView1.CurrentRow.Cells.Item(DGV3_Th).Value.ToString)
        End Select
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



    '报价单导出到表格/辅助功能*************************************************************************************************************************************

    '导出报价单/辅助功能
    Private Function BjdOutput() As DataTable
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select 图号,名称,台件,材料价,精加工费,热处理费,其他加工费,下料费,表面处理费,含税单价,备注1,备注2,订单号,型号" +
                                     " from 零件信息 where 订单号 = '" + DataGridView1.CurrentRow.Cells(DGV1_Ddh).Value.ToString + "'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "零件信息")
        '增加列
        ds.Tables(0).Columns.Add("序号")
        ds.Tables(0).Columns.Add("材质")
        ds.Tables(0).Columns.Add("工序")
        ds.Tables(0).Columns.Add("准备工时")
        ds.Tables(0).Columns.Add("合计工时")
        ds.Tables(0).Columns.Add("毛坯重量")
        ds.Tables(0).Columns.Add("毛坯单价")
        ds.Tables(0).Columns.Add("计划日期")
        ds.Tables(0).Columns.Add("完工尺寸")
        ds.Tables(0).Columns.Add("备注")
        '修改列的排序
        ds.Tables(0).Columns("序号").SetOrdinal(0)
        ds.Tables(0).Columns("图号").SetOrdinal(1)
        ds.Tables(0).Columns("名称").SetOrdinal(2)
        ds.Tables(0).Columns("台件").SetOrdinal(3)
        ds.Tables(0).Columns("材质").SetOrdinal(4)
        ds.Tables(0).Columns("工序").SetOrdinal(5)
        ds.Tables(0).Columns("准备工时").SetOrdinal(6)
        ds.Tables(0).Columns("合计工时").SetOrdinal(7)
        ds.Tables(0).Columns("毛坯重量").SetOrdinal(8)
        ds.Tables(0).Columns("毛坯单价").SetOrdinal(9)
        ds.Tables(0).Columns("材料价").SetOrdinal(10)
        ds.Tables(0).Columns("精加工费").SetOrdinal(11)
        ds.Tables(0).Columns("热处理费").SetOrdinal(12)
        ds.Tables(0).Columns("其他加工费").SetOrdinal(13)
        ds.Tables(0).Columns("下料费").SetOrdinal(14)
        ds.Tables(0).Columns("表面处理费").SetOrdinal(15)
        ds.Tables(0).Columns("含税单价").SetOrdinal(16)
        ds.Tables(0).Columns("备注").SetOrdinal(17)
        ds.Tables(0).Columns("计划日期").SetOrdinal(18)
        ds.Tables(0).Columns("完工尺寸").SetOrdinal(19)
        ds.Tables(0).Columns("备注1").SetOrdinal(20)
        ds.Tables(0).Columns("备注2").SetOrdinal(21)
        ds.Tables(0).Columns("订单号").SetOrdinal(22)
        ds.Tables(0).Columns("型号").SetOrdinal(23)
        '将备注1/2信息转移到前面相应的条目中
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim str1(7) As String
            Dim str2(15) As String
            ds.Tables(0).Rows(i).Item(0) = i + 1
            InfoInput(ds.Tables(0).Rows(i).Item(20).ToString, str1)  '//////与备注信息有关！！！！！
            ds.Tables(0).Rows(i).Item(9) = str1(1) '毛坯单价
            ds.Tables(0).Rows(i).Item(6) = str1(3) '准备工时
            ds.Tables(0).Rows(i).Item(7) = str1(5) '合计工时
            InfoInput(ds.Tables(0).Rows(i).Item(21).ToString, str2)
            ds.Tables(0).Rows(i).Item(4) = str2(3) '材质
            ds.Tables(0).Rows(i).Item(19) = str2(9) '完工尺寸
            ds.Tables(0).Rows(i).Item(5) = str2(13) '工序
            ds.Tables(0).Rows(i).Item(8) = str2(15) '毛坯重量
            '寻找零件对于的计划日期并填写
            Dim da2 As New SqlDataAdapter("select 计划日期 from 部件信息 where 订单号 = '" + ds.Tables(0).Rows(i).Item(22) + "' AND 型号 = '" + ds.Tables(0).Rows(i).Item(23) + "'", cn)
            Dim ds2 As New DataSet
            da2.Fill(ds2, "部件信息")
            Try
                ds.Tables(0).Rows(i).Item(18) = ds2.Tables(0).Rows(0).Item(0)
            Catch ex As Exception
                ds.Tables(0).Rows(i).Item(18) = ""
            End Try
        Next
        BjdOutput = ds.Tables("零件信息")
        '设置后面几列不可见
        'For i = 20 To 23
        'DataGridView5.Columns(i).Visible = False
        'Next
        'For i = 0 To 19
        'DataGridView5.Columns(i).Width = SetWidth(i) * 6 '设置自定义列宽
        'Next
    End Function



    '保存对话框结束后新增表格###################################@多线程
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk
        Thread_ExcelName = Bjdbt
        runThread = New Thread(AddressOf ThreadToExcel)
        FormWait.F = runThread
        FormWait.Show()
        runThread.Start()
    End Sub

    '多线程支持函数###################################@多线程
    Private Sub ThreadToExcel()
        'Dim ExcelName As String = ComboBoxKh.Text + "-" + TextBoxDdh.Text + "-报价单"
        ExcelOutput(BjdOutput(), Thread_ExcelName, SaveFileDialogExcel.FileName)
    End Sub


    '定义委托-wait1###################################@多线程
    Public Delegate Sub VoidDelegate()
    '定义结束后关闭窗体方法-wait2
    Public Sub FormBJ_Wait_Close()
        FormWait.Close()
    End Sub

    '输出报价单数据到表格###################################@多线程
    Public Sub ExcelOutput(DGV As DataTable, ExcelName As String, FileName As String)
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        Dim yy As Integer
        Try
            WorkBook = myExcel.Workbooks.Add()
            Sheet = WorkBook.Sheets(1)
            '输入表格名称
            Sheet.Name = ExcelName
            '合并单元格设置格式并输入表格标题
            With Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(1, 15))
                .Merge()
                .Font.Size = 20
                .Font.FontStyle = "bold"
                .RowHeight = 31
            End With
            Sheet.Cells(1, 1) = ExcelName
            '输入表格标题栏并加粗
            For y = 0 To 19
                WorkBook.Sheets(1).Cells(2, y + 1) = DGV.Columns(y).ColumnName.ToString
            Next
            Sheet.Range(Sheet.Cells(2, 1), Sheet.Cells(2, DGV.Columns.Count)).Font.FontStyle = "bold"
            '输入表格内容
            For y = 0 To 19
                yy += 1
                Sheet.Range(Sheet.Cells(2, yy), Sheet.Cells(2, yy)).ColumnWidth = SetWidth(yy - 1) 'DGV.Columns(y).Width / 6
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
        Catch ex2 As Exception
            MessageBox.Show("输出报价单失败，请检查错误重新生成。", "操作错误！")
        End Try
        '跨线程关闭等待窗体-wait3
        Me.Invoke(New VoidDelegate(AddressOf FormBJ_Wait_Close))
        runThread.Abort()
    End Sub

    '扩展功能模块================================================

















    '辅助功能模块////////////////////////////////////////////////////////////////////////////

    '各个窗口之间快速转换
    Private Sub LabelBJ_Click(sender As Object, e As EventArgs) Handles LabelBJ.Click
        'FormLogin.BJLoad(Me)
    End Sub

    Private Sub LabelSC_Click(sender As Object, e As EventArgs) Handles LabelSC.Click
        FormLogin.SCLoad(Me)
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
                ButtonA.Text = "新增订单"
                ButtonB.Text = "修改订单"
                ButtonC.Text = "删除订单"
                ButtonD.Text = "导入Excel"
                ButtonE.Text = "删除导入"
                ButtonF.Text = "进入生产"
                ButtonF.Visible = True
                ButtonG.Text = "导出报价单"
                ButtonG.Visible = True

                ButtonSX.Visible = True
                TextBoxSX.Visible = True
                LabelSX.Visible = True

            Case 2
                ButtonA.Text = "新增部件"
                ButtonB.Text = "修改部件"
                ButtonC.Text = "删除部件"
                ButtonD.Text = "导入图纸"
                ButtonE.Text = "打开图纸"
                ButtonF.Text = "导入模板"
                ButtonF.Visible = False
                ButtonG.Text = "储存为模板"
                ButtonG.Visible = False

                ButtonSX.Visible = False
                TextBoxSX.Visible = False
                LabelSX.Visible = False

            Case 3
                ButtonA.Text = "新增零件"
                ButtonB.Text = "修改零件"
                ButtonC.Text = "删除零件"
                ButtonD.Text = "导入图纸"
                ButtonE.Text = "打开图纸"
                ButtonF.Visible = False
                ButtonG.Visible = False

                ButtonSX.Visible = False
                TextBoxSX.Visible = False
                LabelSX.Visible = False
        End Select
    End Sub

    '辅助功能模块=============================================


End Class