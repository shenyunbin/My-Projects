Imports System.Data.SqlClient
Public Class FormNCK
    Dim Order As String = "识别号 desc"
    Public SQL_Connection As String '= "Data Source=(local);Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=sa;Password=123456;"
    Public SQL_Adress As String

    Dim TEMP_Shrq As String

    '定义仓库信息各个列
    Public Const DGV1_Kh = 0
    Public Const DGV1_Th = 1
    Public Const DGV1_Mc = 2
    Public Const DGV1_Xykc = 3
    Const DGV1_Mpbz = 4
    Const DGV1_Sbh = 5


    Const SQL_CmdGlxx1 = "select top 200 订单信息.客户,零件信息.订单号,零件信息.型号,零件信息.图号,零件信息.名称,零件信息.台件,部件信息.计划数量,部件信息.计划日期,订单信息.状态 " +
                                     "from 零件信息 left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 left join 订单信息 on  零件信息.订单号=订单信息.订单号 left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 " +
                                    "where 订单信息.状态='生产' and 仓库信息.客户='"

    Const SQL_CmdGlxx2 = "'  group by 订单信息.客户,零件信息.订单号,零件信息.型号,零件信息.图号,零件信息.名称,零件信息.台件,部件信息.计划数量,部件信息.计划日期,订单信息.状态" 'group by 零件信息.订单号,订单信息.客户,订单信息.批号,订单信息.状态 


    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////

    '窗口载入时操作函数
    Private Sub FormNCK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Size = New Size(1694, 47 * Me.Size.Height / 740)


        LabelBJ.Enabled = FormLogin.GlyMode
        '设置表格格式
        SetDVG()
        '获取仓库信息列表
        Ckxx_Load()
    End Sub

    '点击表格后操作函数
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim Kh As String = DataGridView1.CurrentRow.Cells.Item(DGV1_Kh).Value.ToString
        Dim Th As String = DataGridView1.CurrentRow.Cells.Item(DGV1_Th).Value.ToString
        LJXXDisp(Kh, Th)
        TextBoxCTh.Text = Th
    End Sub

    '打开图纸按钮事件
    Private Sub ButtonB_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        If FormLogin.OpenPic(DataGridView1.CurrentRow.Cells.Item(DGV1_Kh).Value.ToString, DataGridView1.CurrentRow.Cells.Item(DGV1_Th).Value.ToString) = False Then
            OpenFileDialogPicture.ShowDialog()
        End If
    End Sub

    '图号搜索按钮事件
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        If ButtonA.Text = "图号搜索" Then
            Dim cn As New SqlConnection(SQL_Connection)
            Dim da As New SqlDataAdapter("select 客户,图号,名称,现有库存,毛坯备注,识别号 from 仓库信息 where 图号 like '%" & TextBoxCTh.Text & "%' order by 现有库存 desc", cn)
            Dim ds As New DataSet
            da.Fill(ds, "仓库信息")
            DataGridView1.DataSource = ds.Tables("仓库信息")
            ButtonA.Text = "退出搜索"
            ButtonA.ForeColor = Color.IndianRed
        Else
            Ckxx_Load()
            ButtonA.Text = "图号搜索"
            ButtonA.ForeColor = Color.Black
        End If
    End Sub

    '搜索框改变退出搜索
    Private Sub TextBoxCTh_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCTh.KeyDown
        If ButtonA.Text = "退出搜索" Then
            Ckxx_Load()
            ButtonA.Text = "图号搜索"
            ButtonA.ForeColor = Color.Black
        End If
    End Sub

    '启用高级功能
    Private Sub CheckBoxQysc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxQysc.CheckedChanged
        If CheckBoxQysc.Checked Then
            ButtonC.Enabled = True
        Else
            ButtonC.Enabled = False
        End If
    End Sub

    '更改库存按钮事件
    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        FormNCK_GGKC.Show()
    End Sub

    '全局操作模块================================================



    '信息载入模块/////////////////////////////////////////////////////////////////////////////

    '仓库信息显示函数
    Public Sub Ckxx_Load()
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter("select top 10000 客户,图号,名称,现有库存,毛坯备注,识别号 from 仓库信息  order by " + Order, cn)
        Dim ds As New DataSet
        da.Fill(ds, "仓库信息")
        DataGridView1.DataSource = ds.Tables("仓库信息")
    End Sub


    '载入零件其他信息Part1
    Private Sub LJXX_Load(Mode As Integer, Kh As String, Th As String, DGV As DataGridView) 'Mode 2=入库信息，3=送货信息，4=部件信息，5=零件信息
        Dim SqlCmd As String
        Select Case Mode
            Case 2
                SqlCmd = "select top 200 * from 入库信息 where 客户='" & Kh & "' and 图号='" & Th & "' order by 识别号 desc"
            Case 3
                SqlCmd = "select top 200 * from 送货信息 where 客户='" & Kh & "' and 型号='" & Th & "' order by 识别号 desc"
            Case 4
                SqlCmd = "select top 200 * from 部件信息 where 型号='" & Th & "' order by 识别号 desc"
            Case 5
                SqlCmd = SQL_CmdGlxx1 + Kh + "' and 仓库信息.图号='" + Th + SQL_CmdGlxx2
            Case Else
                Exit Sub
        End Select
        Dim cn As New SqlConnection(SQL_Connection)
        Dim da As New SqlDataAdapter(SqlCmd, cn)
        Dim ds As New DataSet
        da.Fill(ds, "零件关联信息")
        DGV.DataSource = ds.Tables("零件关联信息")
    End Sub

    '载入零件其他信息Part2 载入全部附加信息
    Private Sub LJXXDisp(Kh As String, Th As String)
        LJXX_Load(2, Kh, Th, DataGridView2)
        LJXX_Load(3, Kh, Th, DataGridView3)
        LJXX_Load(4, Kh, Th, DataGridView4)
        LJXX_Load(5, Kh, Th, DataGridView5)
    End Sub



    '信息载入模块==============================================





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
        'FormLogin.CKLoad(Me)
    End Sub

    Private Sub LabelJS_Click(sender As Object, e As EventArgs) Handles LabelJS.Click
        FormLogin.JSLoad(Me)
    End Sub

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1, DataGridView2, DataGridView3, DataGridView4, DataGridView5}
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



    '辅助功能模块=============================================


    '扩展功能模块//////////////////////////////////////////////////////////////////////////////////////

    '图片导入和打开功能/辅助功能******************************************

    '导入图片/复制图片到指定路径
    Private Sub OpenFileDialogPicture_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogPicture.FileOk
        FormLogin.SavePic(OpenFileDialogPicture.FileName, DataGridView1.CurrentRow.Cells.Item(DGV1_Kh).Value.ToString, DataGridView1.CurrentRow.Cells.Item(DGV1_Th).Value.ToString)
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

    '扩展功能模块===================================================



End Class