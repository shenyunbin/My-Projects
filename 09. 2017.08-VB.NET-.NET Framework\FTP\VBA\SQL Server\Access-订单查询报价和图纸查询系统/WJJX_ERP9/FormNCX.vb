Imports System.Data.SqlClient
Imports System.Threading

Public Class FormNCX
    Public runThread As Thread
    Dim Thread_ExcelName As String

    Public SQL_Connection As String '= "Data Source=(local);Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String

    '全局操作模块//////////////////////////////////////////////////////////////////////////////////

    '窗口载入时操作
    Private Sub FormNCX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)

        LabelBJ.Enabled = FormLogin.GlyMode

        '设置搜索选择框格式
        ComboBoxSslx.Items.Add("订单信息-订单号")

        ComboBoxSslx.Items.Add("订单信息-客户/日期*")

        ComboBoxSslx.Items.Add("部件信息-订单号")

        ComboBoxSslx.Items.Add("部件信息-计划日期")

        ComboBoxSslx.Items.Add("部件信息-客户/日期*")

        If FormLogin.GlyMode = True Then

            ComboBoxSslx.Items.Add("零件信息-订单号")

            ComboBoxSslx.Items.Add("零件信息-图号")

            ComboBoxSslx.Items.Add("零件信息-备注2")

            ComboBoxSslx.Items.Add("零件信息-客户/日期*")

        End If

        ComboBoxSslx.Items.Add("送货记录-订单号")

        ComboBoxSslx.Items.Add("送货记录-型号")

        ComboBoxSslx.Items.Add("送货记录-日期")

        ComboBoxSslx.Items.Add("毛坯入库-图号")

        ComboBoxSslx.Items.Add("毛坯入库-日期")

        ComboBoxSslx.SelectedItem = ComboBoxSslx.Items(1)
        ComboBoxSslx.DropDownStyle = ComboBoxStyle.DropDownList
        '设置表格格式
        SetDVG()
    End Sub

    '单击历史记录搜索按钮事件
    Private Sub ButtonSs_Click(sender As Object, e As EventArgs) Handles ButtonSs.Click
        Dim Sslx, BG As String
        Sslx = "图号"
        BG = "零件信息"
        Select Case ComboBoxSslx.Text
            Case "订单信息-订单号"
                BG = "订单信息"
                Sslx = "订单号"
            Case "订单信息-客户/日期*"
                BG = "订单信息"
                Sslx = "状态"
            Case "部件信息-订单号"
                BG = "部件信息"
                Sslx = "订单号"
            Case "部件信息-计划日期"
                BG = "部件信息"
                Sslx = "计划日期"
            Case "部件信息-客户/日期*"
                BG = "部件信息"
                Sslx = "状态"
            Case "零件信息-订单号"
                BG = "零件信息"
                Sslx = "订单号"
            Case "零件信息-图号"
                BG = "零件信息"
                Sslx = "图号"
            Case "其他"
                BG = "零件信息-备注2"
                Sslx = "备注2"
            Case "零件信息-客户/日期*"
                BG = "零件信息"
                Sslx = "状态"
            Case "送货记录-订单号"
                BG = "送货信息"
                Sslx = "订单号"
            Case "送货记录-型号"
                BG = "送货信息"
                Sslx = "型号"
            Case "送货记录-日期"
                BG = "送货信息"
                Sslx = "送货日期"
            Case "毛坯入库-图号"
                BG = "入库信息"
                Sslx = "图号"
            Case "毛坯入库-日期"
                BG = "入库信息"
                Sslx = "入库日期"
        End Select
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select * from " + BG + " where " + Sslx + " like " & "'%" & TextBoxSsnr.Text & "%'", cn)
        Dim ds As New DataSet
        da.Fill(ds, "搜索结果")
        DataGridViewSsjg.DataSource = ds.Tables("搜索结果")
    End Sub

    '全局操作模块=============================================


    '扩展功能模块/////////////////////////////////////////////////////////////////////////////

    '导出搜索结果按钮事件
    Private Sub ButtonDcssjg_Click(sender As Object, e As EventArgs) Handles ButtonDcssjg.Click
        SaveFileDialogExcel.FileName = ComboBoxSslx.Text + " - " + TextBoxSsnr.Text + " - 搜索结果"
        SaveFileDialogExcel.ShowDialog()
    End Sub

    '保存对话框结束后新增表格
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk
        Thread_ExcelName = ComboBoxSslx.Text + " - " + TextBoxSsnr.Text + " - 搜索结果"
        runThread = New Thread(AddressOf ThreadToExcel)
        FormWait.F = runThread
        FormWait.Show()
        runThread.Start()
    End Sub

    '多线程支持函数###################################@多线程
    Private Sub ThreadToExcel()
        ExcelOutput(DataGridViewSsjg, Thread_ExcelName, SaveFileDialogExcel.FileName)
    End Sub


    '定义委托-wait1###################################@多线程
    Public Delegate Sub VoidDelegate()
    '定义结束后关闭窗体方法-wait2
    Public Sub FormBJ_Wait_Close()
        FormWait.Close()
    End Sub


    '输出报价单数据到表格
    Public Sub ExcelOutput(DGV As DataGridView, ExcelName As String, FileName As String)
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        Dim yy As Integer = 0
        Try
            WorkBook = myExcel.Workbooks.Add()
            Sheet = WorkBook.Sheets(1)
            '输入表格名称
            Sheet.Name = ExcelName
            '合并单元格设置格式并输入表格标题
            With Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(1, 7))
                .Merge()
                .Font.Size = 20
                .Font.FontStyle = "bold"
                .RowHeight = 31
            End With
            Sheet.Cells(1, 1) = ExcelName
            '输入表格标题栏并加粗                  '从第三列开始写入表格!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            For y = 0 To DGV.Columns.Count - 1
                If DGV.Columns(y).Visible = False Then
                    Continue For
                End If
                yy += 1
                WorkBook.Sheets(1).Cells(2, yy) = DGV.Columns(y).HeaderText
            Next
            Sheet.Range(Sheet.Cells(2, 1), Sheet.Cells(2, yy)).Font.FontStyle = "bold"
            '输入表格内容
            yy = 0
            For y = 0 To DGV.Columns.Count - 1               '从第三列开始写入表格!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                If DGV.Columns(y).Visible = False Then
                    Continue For
                End If
                yy += 1
                Sheet.Range(Sheet.Cells(2, yy), Sheet.Cells(2, yy)).ColumnWidth = DGV.Columns(y).Width / 6 'SetWidth(yy - 1) 
                For x = 0 To DGV.Rows.Count - 1
                    WorkBook.Sheets(1).Cells(x + 3, yy) = DGV.Rows(x).Cells.Item(y).Value.ToString
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

    '扩展功能模块============================================



    '辅助功能模块////////////////////////////////////////////////////////////////////////////

    '各个窗口之间快速转换
    Private Sub LabelBJ_Click(sender As Object, e As EventArgs) Handles LabelBJ.Click
        FormLogin.BJLoad(Me)
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
        'FormLogin.JSLoad(Me)
    End Sub

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridViewSsjg}
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

    '辅助功能模块===========================================


End Class