Imports System.Data.SqlClient
Imports System.Threading
Public Class FormSet
    Public runThread As Thread
    Public loadThread As Thread
    Dim Thread_ExcelName As String
    Dim Thread_FileNames() As String
    Dim ExcelProcess As String
    Dim ExcelCount As Integer
    Dim SheetCount As Integer
    Dim LineCount As Integer

    Public SQL_Connection As String '= "Data Source=(local);Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String

    Dim Leftjoin = " left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                                 "left join 订单信息 on  零件信息.订单号=订单信息.订单号" +
                                 " left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 "

    Dim Leftjoin2 = "left join 订单信息 on  部件信息.订单号=订单信息.订单号"

    Private Sub FormSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)

        'CheckBoxQysc.Visible = FormLogin.SetBj
        Dim bol As Boolean = False
        ButtonScdd.Enabled = bol
        ButtonScbj.Enabled = bol
        ButtonSclj.Enabled = bol
        ButtonScrk.Enabled = bol
        ButtonScsh.Enabled = bol
        ButtonGgmc.Enabled = bol
        ButtonHfsj2.Enabled = bol
        '设置图表格式函数
        SetDVG()
        OpenFileDialogExcel.Multiselect = True
        TextBoxDcsj.Text = Format(Now(), "yyyy-MM-dd")
        TextBoxScsj.Text = Format(Now(), "yyyy-MM-dd")
        '载入用户名
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select 用户名 from 登陆信息  group by 用户名 order by 用户名 asc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "订单信息")
        For i = 0 To ds.Tables(0).Rows.Count - 1
            ComboBoxYhm.Items.Add(ds.Tables(0).Rows(i).Item(0).ToString)
        Next
        ComboBoxYhm.Text = FormLogin.Yhm
        '载入账户类型
        ComboBoxLx.Items.Add("生产主管")
        ComboBoxLx.Items.Add("会计主管")
        ComboBoxLx.Items.Add("管理员")
        'ComboBoxLx.SelectedItem = ComboBoxLx.Items(0)
        ComboBoxLx.Text = FormLogin.Zhlx
        ComboBoxYhm.Enabled = False
        ComboBoxLx.Enabled = False
        ButtonXjyh.Enabled = False
        ButtonScyh.Enabled = False
    End Sub

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1, DataGridView2, DataGridView3, DataGridView4, DataGridView5, DataGridView6, DataGridViewThmc}
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

    '新增用户
    Private Sub ButtonXjyh_Click(sender As Object, e As EventArgs) Handles ButtonXjyh.Click
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "insert into 登陆信息 (用户名,密码,账户类型) values('" + ComboBoxYhm.Text + "', '" + TextBoxMm.Text + "' ,'" + ComboBoxLx.Text + "')"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        MessageBox.Show("新增用户成功！")
    End Sub

    '更改用户
    Private Sub ButtonGgyh_Click(sender As Object, e As EventArgs) Handles ButtonGgyh.Click
        If TextBoxMm.Text = "" Then
            MessageBox.Show("密码不能为空！", "数据错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "update 登陆信息 set 密码='" + TextBoxMm.Text + "', 账户类型='" + ComboBoxLx.Text + "' where 用户名='" + ComboBoxYhm.Text + "'"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        MessageBox.Show("更改用户信息成功！")
    End Sub

    '删除用户
    Private Sub ButtonScyh_Click(sender As Object, e As EventArgs) Handles ButtonScyh.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除此用户？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "delete from 登陆信息 where 用户名='" + ComboBoxYhm.Text + "'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除用户成功！")
        End If
    End Sub

    '导出数据按钮事件
    Private Sub ButtonDcsj_Click(sender As Object, e As EventArgs) Handles ButtonDcsj.Click
        SaveFileDialogExcel.FileName = TextBoxDcsj.Text + "-生产数据"
        SaveFileDialogExcel.ShowDialog()
    End Sub

    '保存对话框结束后新增表格
    Private Sub SaveFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogExcel.FileOk
        'Thread_ExcelName = TextBoxDcsj.Text + "-生产数据"
        runThread = New Thread(AddressOf ThreadToExcel)
        FormWait.F = runThread
        FormWait.Show()
        runThread.Start()
    End Sub

    '多线程支持函数###################################@多线程
    Private Sub ThreadToExcel()
        Dim ds(5) As DataSet
        For i = 0 To 4
            ds(i) = New DataSet
        Next
        XxToDataSet("订单信息", TextBoxScsj.Text, ds(0))
        XxToDataSet("部件信息", TextBoxScsj.Text, ds(1))
        XxToDataSet("零件信息", TextBoxScsj.Text, ds(2))
        XxToDataSet("入库信息", TextBoxScsj.Text, ds(3))
        XxToDataSet("送货信息", TextBoxScsj.Text, ds(4))
        Dim ExcelName As String = TextBoxDcsj.Text + "-生产数据"
        DataSetToExcel(ds, ExcelName, SaveFileDialogExcel.FileName)
        '跨线程关闭等待窗体-wait3
        Me.Invoke(New VoidDelegate(AddressOf FormBJ_Wait_Close))
        runThread.Abort()
    End Sub


    '定义委托-wait1###################################@多线程
    Public Delegate Sub VoidDelegate()
    '定义结束后关闭窗体方法-wait2
    Public Sub FormBJ_Wait_Close()
        FormWait.Close()
    End Sub


    '信息载入到Dataset
    Private Sub XxToDataSet(Sslx As String, Rq As String, ds As DataSet)
        Dim sql As String
        Dim cn As New SqlConnection(SQL_Connection)
        Select Case Sslx
            Case "导入数据"
                sql = "select * from 零件信息 where 状态 like " & "'%" & Rq & "%' and 订单号 = '导入'"
            Case "零件信息"
                sql = "select * from " + Sslx + " where 状态 like " & "'%" & Rq & "%' and 订单号 != '导入'"
            Case "入库信息"
                sql = "select * from " + Sslx + " where 入库日期 like " & "'%" & Rq & "%'"
            Case "送货信息"
                sql = "select * from " + Sslx + " where 送货日期 like " & "'%" & Rq & "%'"
            Case Else
                sql = "select * from " + Sslx + " where 状态 like " & "'%" & Rq & "%'"
        End Select
        Dim da As New SqlDataAdapter(sql, cn)
        da.Fill(ds, Sslx)
    End Sub


    '导出DataSet数据到表格
    Private Sub DataSetToExcel(ds() As DataSet, ExcelName As String, FileName As String)
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        Dim SheetName() As String = {"1-订单信息", "2-部件信息", "3-零件信息", "4-入库信息", "5-送货信息"}
        Dim y As Integer
        WorkBook = myExcel.Workbooks.Add()
        For T = 4 To 0 Step -1
            If T = 4 Then
                Sheet = WorkBook.Sheets(1)
            Else
                Sheet = WorkBook.Sheets.Add
            End If
            '输入表格名称
            Sheet.Name = SheetName(T)
            '输入表格标题栏并加粗
            For y = 0 To ds(T).Tables(0).Columns.Count - 1
                WorkBook.Sheets(1).Cells(1, y + 1) = ds(T).Tables(0).Columns(y).ColumnName
            Next
            Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(1, ds(T).Tables(0).Columns.Count)).Font.FontStyle = "bold"
            '输入表格内容
            For y = 0 To ds(T).Tables(0).Columns.Count - 1
                For x = 0 To ds(T).Tables(0).Rows.Count - 1
                    WorkBook.Sheets(1).Cells(x + 2, y + 1) = ds(T).Tables(0).Rows(x).Item(y).ToString
                Next
            Next
            '设置单元格边框格式并垂直居中水平靠左
            With Sheet.Range(Sheet.Cells(1, 1), Sheet.Cells(ds(T).Tables(0).Rows.Count + 1, y))
                '.Borders.LineStyle = 1
                .HorizontalAlignment = -4131 'Left -4131 Right -4152
                .VerticalAlignment = -4108
            End With
        Next
        WorkBook.SaveAs(FileName)
        'WorkBook.Close()
        'myExcel.Quit()
        myExcel.Visible = True
        'MessageBox.Show("导出数据成功！")
    End Sub


    '备份数据
    Private Sub ButtonBfsj_Click(sender As Object, e As EventArgs) Handles ButtonBfsj.Click
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd")
        SaveFileDialogBak.FileName = Sbh + "-数据备份"
        SaveFileDialogBak.ShowDialog()
    End Sub

    '备份数据
    Private Sub SaveFileDialogBak_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogBak.FileOk
        Try
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "BACKUP DATABASE WJJX_ERP TO disk = '" + SaveFileDialogBak.FileName + "' WITH FORMAT, NAME = 'WJJX_ERP数据库备份'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("备份数据成功！")
        Catch ex As Exception
            MessageBox.Show("备份数据失败！请勿备份到受保护的系统盘（如C盘）内。")
        End Try
    End Sub

    '恢复数据
    Private Sub ButtonHfsj_Click(sender As Object, e As EventArgs) Handles ButtonHfsj.Click
        OpenFileDialogBak.ShowDialog()
    End Sub

    '恢复数据
    Private Sub OpenFileDialogBak_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogBak.FileOk
        Try
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "use master; restore database WJJX_ERP from disk= '" + OpenFileDialogBak.FileName + "'"  'WITH REPLACE
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("恢复数据成功！")
        Catch ex As Exception
            MessageBox.Show("恢复数据失败！")
        End Try
    End Sub

    '恢复并覆盖数据
    Private Sub ButtonHfsj2_Click(sender As Object, e As EventArgs) Handles ButtonHfsj2.Click
        OpenFileDialogBakPro.ShowDialog()
    End Sub

    '恢复并覆盖数据
    Private Sub OpenFileDialogBakPro_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogBakPro.FileOk
        Try
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "use master; restore database WJJX_ERP from disk= '" + OpenFileDialogBakPro.FileName + "' with replace"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("恢复数据成功！")
        Catch ex As Exception
            MessageBox.Show("恢复数据失败！")
        End Try
    End Sub


    '导入Excel表格数据
    Private Sub ButtonDrsj_Click(sender As Object, e As EventArgs) Handles ButtonDrsj.Click
        OpenFileDialogExcel.ShowDialog()
    End Sub

    '选择Excel表格时间对话框结束
    Private Sub OpenFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogExcel.FileOk
        Thread_FileNames = OpenFileDialogExcel.FileNames
        loadThread = New Thread(AddressOf ThreadInputExcel)
        FormLoad.F = loadThread
        FormLoad.Show()
        loadThread.Start()
    End Sub

    '多线程支持函数###################################@多线程
    Private Sub ThreadInputExcel()
        ExcelCount = 0
        SheetCount = 0
        LineCount = 0
        For i = 0 To Thread_FileNames.Length - 1
            ExcelCount += 1
            ExcelDataInput(Thread_FileNames(i))
        Next
        '跨线程关闭等待窗体-wait3
        Me.Invoke(New LoadDelegate(AddressOf FormWait_Close))
        MessageBox.Show("导入数据成功！")
        loadThread.Abort()
    End Sub

    '定义委托-wait1###################################@多线程
    Public Delegate Sub LoadDelegate()

    '定义结束后关闭窗体方法-wait2
    Public Sub FormWait_Close()
        FormLoad.Close()
    End Sub

    'ExcelCount += 1
    'Me.Invoke(New LoadDelegate(AddressOf FormWait_Process))

    Public Sub FormWait_Process()
        FormLoad.LabelProcess.Text = "已导入" + ExcelCount.ToString + "个文件，" + SheetCount.ToString + "张表，" + LineCount.ToString + "行数据。"
    End Sub

    '导出DataSet数据到表格
    Private Sub ExcelDataInput(Adress As String)
        Dim myExcel As New Excel.Application  '定义进程
        Dim WorkBook As Excel.Workbook '定义工作簿
        Dim Sheet As Excel.Worksheet '定义工作表
        WorkBook = myExcel.Workbooks.Open(Adress)
        'Dim SheetNUm As Integer = 0
        For SheetNUm = 1 To WorkBook.Sheets.Count

            SheetCount += 1
            Sheet = WorkBook.Sheets(SheetNUm)   '获得第1个工作表的控制句柄

            '定义每个数据类型及所在表格的列的位置
            Dim ColNum(27) As Integer
            Dim ColStr(27) As String
            For i = 0 To 26
                ColNum(i) = 0
                'ColStr(i) = vbNullString
            Next
            '查找表格标题的起始行号
            Dim StartRowNum As Integer = 0
            For i = 1 To Sheet.UsedRange.Rows.Count
                Try
                    Select Case Replace(Sheet.Cells(i, 1).Value.ToString, " ", "") '去除标题空格
                        Case "序号", "图号"
                            StartRowNum = i
                            Exit For
                    End Select
                    Select Case Replace(Sheet.Cells(i, 2).Value.ToString, " ", "") '去除标题空格
                        Case "型号", "图号", "名称"
                            StartRowNum = i
                            Exit For
                    End Select
                Catch
                End Try
            Next
            If StartRowNum = 0 Then
                Continue For
            End If
            '查找每个数据类型所在列的位置
            For y = 1 To Sheet.UsedRange.Columns.Count
                Try
                    Select Case Replace(Sheet.Cells(StartRowNum, y).Value.ToString, " ", "") '去除标题空格
                        Case "编号", "序号"
                            ColNum(0) = y
                            Exit Select
                        Case "图号", "型号"
                            ColNum(1) = y
                            Exit Select
                        Case "名称"
                            ColNum(2) = y
                            Exit Select
                        Case "台件", "件/套"
                            ColNum(3) = y
                            Exit Select
                        Case "毛坯单价", "元/kg"
                            ColNum(15) = y
                            Exit Select
                        Case "准备工时", "单件准备"
                            ColNum(16) = y
                            Exit Select
                        Case "合计工时", "单件合计", "单件合计工时"
                            ColNum(17) = y
                            Exit Select
                        Case "是否外购", "外购"  '-----
                            ColNum(18) = y
                            Exit Select
                        Case "是否自备料", "自备料"
                            ColNum(19) = y
                            Exit Select
                        Case "材质", "材料"
                            ColNum(20) = y
                            Exit Select
                        Case "备料", "备料规格", "毛坯规格", "毛坯尺寸", "下料规格", "备料尺寸"
                            ColNum(21) = y
                            Exit Select
                        Case "下料数量", "下料数"
                            ColNum(22) = y
                            Exit Select
                        Case "完工尺寸"
                            ColNum(23) = y
                            Exit Select
                        Case "热处理"
                            ColNum(24) = y
                            Exit Select
                        Case "工序"
                            ColNum(25) = y
                            Exit Select
                        Case "毛坯重量", "重量"
                            ColNum(26) = y
                            Exit Select
                        Case "材料价", "料价", "材料价格"
                            ColNum(6) = y
                            Exit Select
                        Case "下料费", "切割费"
                            ColNum(7) = y
                            Exit Select
                        Case "精加工费", "加工费"
                            ColNum(8) = y
                            Exit Select
                        Case "热处理费"
                            ColNum(9) = y
                            Exit Select
                        Case "表面处理费"
                            ColNum(10) = y
                            Exit Select
                        Case "其他加工费", "其他"
                            ColNum(11) = y
                            Exit Select
                        Case "实付工资", "工资"
                            ColNum(12) = y
                            Exit Select
                        Case "含税单价", "现价", "单价"
                            ColNum(13) = y
                            Exit Select
                        Case "备注", "备注3"
                            ColNum(14) = y
                            Exit Select
                    End Select
                Catch
                End Try
            Next
            For RowNum = StartRowNum + 1 To Sheet.UsedRange.Rows.Count
                For i = 0 To 26
                    ColStr(i) = vbNullString
                Next
                For i = 0 To 26
                    If ColNum(i) = 0 Then
                        Continue For
                    End If
                    Try
                        ColStr(i) = Sheet.Cells(RowNum, ColNum(i)).Value.ToString
                    Catch ex As Exception
                    End Try
                Next
                If Not (ColStr(1) = "") Then 'And ColStr(2) = ""  目前只识别图号不能为0
                    '导入数据到零件信息
                    DrLjxx(ColStr)
                    '显示导入进度！！！！！！！！！！！！！！！！！！！！！！！！bug
                    LineCount += 1
                    Me.Invoke(New LoadDelegate(AddressOf FormWait_Process))
                End If
            Next
        Next
        'WorkBook.Save()
        WorkBook.Close(SaveChanges:=False)
        myExcel.Quit()
    End Sub


    '导入零件信息
    Private Sub DrLjxx(str() As String)
        Try
            str(4) = "毛坯单价: " + str(15) + " | 准备工时: " + str(16) + " | 合计工时: " + str(17)
            str(5) = "| | 材质: " + str(20) + " | 备料: " + str(21) + " | 下料数: " + str(22) + " | 完工: " + str(23) +
                " | 热处理: " + str(24) + " | 工序: " + str(25) + " | 毛坯重: " + str(26)
            Dim Bh As String = str(0)
            Dim Th As String = str(1)
            Dim Mc As String = str(2)
            Dim Tj As String = str(3)
            Dim Bz1 As String = str(4)
            Dim Bz2 As String = str(5)
            Dim Clj As String = str(6)
            Dim Xlf As String = str(7)
            Dim Jjgf As String = str(8)
            Dim Rclf As String = str(9)
            Dim Bmclf As String = str(10)
            Dim Qtjgf As String = str(11)
            Dim Sfgz As String = str(12)
            Dim Hsdj As String = str(13)
            Dim Bz3 As String = str(14)
            Dim Zt As String = "导入数据-" + DateTimePickerDrsj.Text 'Format(Now(), "yyyy-MM-dd")
            Dim Ddh As String = "导入"
            Dim Xh As String = "导入"
            Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  'if not exists(select * from 零件信息 where 图号='" + Th + "' and 名称='" + Mc + "')  
            Dim sql As String = "insert into 零件信息 (编号,图号,名称,台件,备注1,备注2,材料价,下料费,精加工费,热处理费,表面处理费,其他加工费,实付工资,含税单价,备注3,状态,订单号,型号,识别号) " +
                "values('" + Bh + "','" + Th + "','" + Mc + "','" + Tj + "','" + Bz1 + "','" + Bz2 + "','" + Clj + "','" + Xlf + "','" + Jjgf + "','" + Rclf + "','" +
            Bmclf + "','" + Qtjgf + "','" + Sfgz + "','" + Hsdj + "','" + Bz3 + "','" + Zt + "','" + Ddh + "','" + Xh + "','" + Sbh + "')"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            System.Threading.Thread.Sleep(1) '延迟1毫秒 
        Catch ex As Exception
        End Try
    End Sub

    '搜索删除模块**********************************************************************************************************************************************************
    '搜索按钮事件
    Private Sub ButtonSs_Click(sender As Object, e As EventArgs) Handles ButtonSs.Click
        SS_Load("订单信息", TextBoxScsj.Text, DataGridView1)
        SS_Load("部件信息", TextBoxScsj.Text, DataGridView2)
        SS_Load("零件信息", TextBoxScsj.Text, DataGridView3)
        DataGridView3.Columns.Item(6).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(7).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(8).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(9).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(10).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(11).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(12).Visible = FormLogin.GlyMode
        DataGridView3.Columns.Item(13).Visible = FormLogin.GlyMode
        SS_Load("入库信息", TextBoxScsj.Text, DataGridView4)
        SS_Load("送货信息", TextBoxScsj.Text, DataGridView5)
        DataGridView5.Columns.Item(4).Visible = FormLogin.GlyMode
        SS_Load("导入数据", TextBoxScsj.Text, DataGridView6)
        DataGridView6.Columns.Item(6).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(7).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(8).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(9).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(10).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(11).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(12).Visible = FormLogin.GlyMode
        DataGridView6.Columns.Item(13).Visible = FormLogin.GlyMode
    End Sub

    '搜索信息载入
    Private Sub SS_Load(Sslx As String, Rq As String, DGV As DataGridView)
        Dim sql As String
        Dim cn As New SqlConnection(SQL_Connection)
        Select Case Sslx
            Case "导入数据"
                sql = "select top 10000 * from 零件信息 where 状态 like " & "'%" & Rq & "%' and 订单号 = '导入'"
            Case "零件信息"
                sql = "select top 10000 * from " + Sslx + " where 状态 like " & "'%" & Rq & "%' and 订单号 != '导入'"
            Case "入库信息"
                sql = "select top 10000 * from " + Sslx + " where 入库日期 like " & "'%" & Rq & "%'"
            Case "送货信息"
                sql = "select top 10000 * from " + Sslx + " where 送货日期 like " & "'%" & Rq & "%'"
            Case Else
                sql = "select top 10000 * from " + Sslx + " where 状态 like " & "'%" & Rq & "%'"
        End Select
        Dim da As New SqlDataAdapter(sql, cn)
        Dim ds As New DataSet
        da.Fill(ds, Sslx)
        DGV.DataSource = ds.Tables(Sslx)
    End Sub



    '删除订单
    Private Sub ButtonScdd_Click(sender As Object, e As EventArgs) Handles ButtonScdd.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Sql = "delete from 订单信息 where 状态 like " & "'%" & TextBoxScsj.Text & "%'"
            Dim cm As New SqlCommand(Sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    '删除部件
    Private Sub ButtonScbj_Click(sender As Object, e As EventArgs) Handles ButtonScbj.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Sql = "delete from 部件信息 where 状态 like " & "'%" & TextBoxScsj.Text & "%'"
            Dim cm As New SqlCommand(Sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    '删除零件
    Private Sub ButtonSclj_Click(sender As Object, e As EventArgs) Handles ButtonSclj.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Sql = "delete from 零件信息 where 状态 like " & "'%" & TextBoxScsj.Text & "%' and 订单号 != '导入'"
            Dim cm As New SqlCommand(Sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    '删除入库
    Private Sub ButtonScrk_Click(sender As Object, e As EventArgs) Handles ButtonScrk.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Sql = "delete from 入库信息 where 入库日期 like " & "'%" & TextBoxScsj.Text & "%'"
            Dim cm As New SqlCommand(Sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    '删除送货
    Private Sub ButtonScsh_Click(sender As Object, e As EventArgs) Handles ButtonScsh.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            Sql = "delete from 送货信息 where 送货日期 like " & "'%" & TextBoxScsj.Text & "%'"
            Dim cm As New SqlCommand(Sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    '删除导入
    Private Sub ButtonScsj_Click(sender As Object, e As EventArgs) Handles ButtonScdr.Click
        Dim MsgOk As Integer = MessageBox.Show("是否确认删除选定日期的数据？删除后将不可恢复。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            sql = "delete from 零件信息 where 状态 like " & "'%" & TextBoxScsj.Text & "%' and 订单号 = '导入'"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MessageBox.Show("删除数据成功！")
        End If
    End Sub

    Private Sub CheckBoxQysc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxQysc.CheckedChanged
        If CheckBoxQysc.Checked = True Then
            Dim bol As Boolean = FormLogin.SetBj
            ButtonScdd.Enabled = bol
            ButtonScbj.Enabled = bol
            ButtonSclj.Enabled = bol
            ButtonScrk.Enabled = bol
            ButtonScsh.Enabled = bol
            ComboBoxYhm.Enabled = bol
            ComboBoxLx.Enabled = bol
            ButtonXjyh.Enabled = bol
            ButtonScyh.Enabled = bol
            ButtonHfsj2.Enabled = bol
            ButtonGgmc.Enabled = True
        Else
            Dim bol As Boolean = False
            ButtonScdd.Enabled = bol
            ButtonScbj.Enabled = bol
            ButtonSclj.Enabled = bol
            ButtonScrk.Enabled = bol
            ButtonScsh.Enabled = bol
            ComboBoxYhm.Enabled = bol
            ComboBoxLx.Enabled = bol
            ButtonXjyh.Enabled = bol
            ButtonScyh.Enabled = bol
            ButtonGgmc.Enabled = bol
            ButtonHfsj2.Enabled = bol
        End If
    End Sub


    '功能转换
    Private Sub LabelSjsc_Click(sender As Object, e As EventArgs) Handles LabelSjsc.Click
        LabelSjsc.Font = New System.Drawing.Font("微软雅黑", 14.25, FontStyle.Bold)
        LabelJbsjgl.Font = New System.Drawing.Font("微软雅黑", 14.25, FontStyle.Regular)
        Panel1.Visible = False
    End Sub

    Private Sub LabelJbsjgl_Click(sender As Object, e As EventArgs) Handles LabelJbsjgl.Click
        LabelSjsc.Font = New System.Drawing.Font("微软雅黑", 14.25, FontStyle.Regular)
        LabelJbsjgl.Font = New System.Drawing.Font("微软雅黑", 14.25, FontStyle.Bold)
        Panel1.Visible = True
    End Sub


    '更改零件名称模块**************************************************************************************************
    '搜索图号按钮
    Private Sub ButtonSsth_Click(sender As Object, e As EventArgs) Handles ButtonSsth.Click
        Dim sql As String
        Dim cn As New SqlConnection(SQL_Connection)
        sql = "select top 100 订单信息.客户,零件信息.图号,零件信息.名称,零件信息.台件,零件信息.备注1,零件信息.备注2,零件信息.备注3,零件信息.型号,零件信息.订单号,零件信息.状态,零件信息.识别号 from 零件信息" + Leftjoin + "where 零件信息.图号 like " & "'%" & TextBoxYth.Text & "%' "
        Dim da As New SqlDataAdapter(sql, cn)
        Dim ds As New DataSet
        da.Fill(ds, "图号名称")
        DataGridViewThmc.DataSource = ds.Tables("图号名称")
    End Sub

    '图号表格单击事件
    Private Sub DataGridViewThmc_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewThmc.CellClick
        If DataGridViewThmc.RowCount = 0 Then
            Exit Sub
        End If
        TextBoxKh.Text = DataGridViewThmc.CurrentRow.Cells.Item(0).Value.ToString
        TextBoxYth.Text = DataGridViewThmc.CurrentRow.Cells.Item(1).Value.ToString
        TextBoxYmc.Text = DataGridViewThmc.CurrentRow.Cells.Item(2).Value.ToString
    End Sub

    '更改名称按钮
    Private Sub ButtonGgmc_Click(sender As Object, e As EventArgs) Handles ButtonGgmc.Click
        Dim MsgOk As Integer = MessageBox.Show("是否更改此图号对应零件的名称？更改名称后，会在所有系统中都显示新的名称。", "警告！", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Dim Ymc As String = TextBoxYmc.Text
            Dim Yth As String = TextBoxYth.Text
            Dim Xmc As String = TextBoxXmc.Text
            Dim Kh As String = TextBoxKh.Text
            Dim Sql As String
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  

            Sql = "UPDATE 部件信息  SET 部件信息.名称='" + Xmc + "' FROM 部件信息 " + Leftjoin2 + " WHERE 部件信息.名称 = '" + Ymc + "' and 部件信息.型号='" + Yth + "' and 订单信息.客户='" + Kh + "'"
            Dim cm1 As New SqlCommand(Sql, cn)
            cm1.ExecuteNonQuery()

            Sql = "UPDATE 零件信息  SET 零件信息.名称='" + Xmc + "' FROM 零件信息 " + Leftjoin + " WHERE 零件信息.名称 = '" + Ymc + "' and 零件信息.图号='" + Yth + "' and 订单信息.客户='" + Kh + "'"
            Dim cm2 As New SqlCommand(Sql, cn)
            cm2.ExecuteNonQuery()

            Sql = "UPDATE 仓库信息 SET 名称='" + Xmc + "' WHERE 名称 = '" + Ymc + "' and 图号='" + Yth + "' and 客户='" + Kh + "'"
            Dim cm3 As New SqlCommand(Sql, cn)
            cm3.ExecuteNonQuery()

            Sql = "UPDATE 入库信息 SET 名称='" + Xmc + "' WHERE 名称 = '" + Ymc + "' and 图号='" + Yth + "' and 客户='" + Kh + "'"
            Dim cm4 As New SqlCommand(Sql, cn)
            cm4.ExecuteNonQuery()

            Sql = "UPDATE 送货信息 SET 名称='" + Xmc + "' WHERE 名称 = '" + Ymc + "' and 型号='" + Yth + "' and 客户='" + Kh + "'"
            Dim cm5 As New SqlCommand(Sql, cn)
            cm5.ExecuteNonQuery()

            Sql = "UPDATE 模板信息 SET 名称='" + Xmc + "' WHERE 名称 = '" + Ymc + "' and 图号='" + Yth + "' and 客户='" + Kh + "'"
            Dim cm6 As New SqlCommand(Sql, cn)
            cm6.ExecuteNonQuery()

            cn.Close()
            MessageBox.Show("更改名称成功！")
        End If
    End Sub


End Class