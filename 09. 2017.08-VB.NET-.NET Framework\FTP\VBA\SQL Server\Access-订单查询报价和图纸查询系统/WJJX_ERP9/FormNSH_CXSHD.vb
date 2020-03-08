Imports System.Data.SqlClient
Imports System.Threading

Public Class FormNSH_CXSHD
    Dim TEMP_Shrq As String
    Dim TEMP_Kh As String


    Public runThread As Thread
    Dim Thread_Shrq As String
    Dim Thread_Kh As String
    Dim Thread_ExcelName As String
    Dim ExcelProcess As String

    '定义送货单信息各个列
    Const DGVShd_Shrq = 0
    Const DGVShd_Kh = 1

    '定义送货单详情各个列
    Const DGVShdxx_Kh = 1
    Const DGVShdxx_Shrq = 2
    Const DGVShdxx_Xh = 3
    Const DGVShdxx_Mc = 4
    Const DGVShdxx_Shsl = 5
    Const DGVShdxx_Hsdj = 6
    Const DGVShdxx_Sbh = 10
    Const DGVShdxx_Ddh = 11

    Private Sub FormNSH_CXSHD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDVG()
        '获取送货单信息列表
        Shd_Load()
    End Sub

    '送货日期/送货单浏览模块
    Private Sub Shd_Load()
        Dim cn As New SqlConnection(FormNSH.SQL_Connection)
        Dim da As New SqlDataAdapter("select 送货日期, 客户, count(型号) as 零部件种类数, sum(送货数量) as 零件总数 from 送货信息 group by 送货日期,客户  order by 送货日期 desc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "送货信息")
        DataGridViewShd.DataSource = ds.Tables("送货信息")
    End Sub


    '送货单详情载入模块
    Private Sub Shdxx_Load(Shrq As String, Kh As String)
        TEMP_Shrq = Shrq
        TEMP_Kh = Kh
        Dim cn As New SqlConnection(FormNSH.SQL_Connection)
        Dim da As New SqlDataAdapter("select  row_number() over (order by 识别号) as 序号,客户,送货日期,型号,名称,送货数量,含税单价,已完工,未完工,备注,识别号,订单号 from 送货信息 where 送货日期='" + Shrq + "' and 客户='" + Kh + "'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "送货信息详情")
        DataGridViewShdxx.DataSource = ds.Tables("送货信息详情")
        DataGridViewShdxx.Columns(DGVShdxx_Hsdj).Visible = FormLogin.GlyMode
        DataGridViewShdxx.Columns(DGVShdxx_Sbh).Visible = False
        DataGridViewShdxx.Columns(DGVShdxx_Ddh).Visible = False
    End Sub

    '单击送货单表格事件
    Private Sub DataGridViewShd_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewShd.CellClick
        Dim Shrq As String = DataGridViewShd.CurrentRow.Cells.Item(DGVShd_Shrq).Value.ToString
        Dim Kh As String = DataGridViewShd.CurrentRow.Cells.Item(DGVShd_Kh).Value.ToString
        LabelShdyl.Text = Kh + "-" + Shrq + " - 送货单详情"
        Shdxx_Load(Shrq, Kh)
    End Sub

    '双击送货单表格事件
    Private Sub DataGridViewShd_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewShd.CellDoubleClick
        Dim Shrq As String = DataGridViewShd.CurrentRow.Cells.Item(DGVShd_Shrq).Value.ToString
        Dim Kh As String = DataGridViewShd.CurrentRow.Cells.Item(DGVShd_Kh).Value.ToString
        LabelShdyl.Text = Kh + "-" + Shrq + " - 送货单详情"
        Shdxx_Load(Shrq, Kh)
    End Sub



    '导出生产单模块*****************************************************************************************************************************************************
    '导出生产单到表格
    Private Sub ButtonShdToExcel_Click(sender As Object, e As EventArgs) Handles LabelShdToExcel.Click
        SaveFileDialogExcel.FileName = LabelShdyl.Text
        SaveFileDialogExcel.ShowDialog()
    End Sub

    '保存对话框结束后新增表格
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk

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

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridViewShd, DataGridViewShdxx}
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

End Class