Imports System.Data.SqlClient

Public Class F3JH_FP
    Dim G_Kh, G_Ddh, G_Th, G_Rq, G_Jhs As String

    '窗口载入模块
    Private Sub FCJH_FP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDVG()
        FP_Load(ComboBoxFP.Text)
        ComboBoxFP.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    '任务分配类型选择框操作事件
    Private Sub ComboBoxFP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFP.SelectedIndexChanged
        FP_Load(ComboBoxFP.Text)
        '载入分配记录信息
        F3JH.FPXX_Load(TextBoxDdh.Text, TextBoxTh.Text, DataGridView1, ComboBoxFP.Text)
    End Sub

    '数调用此窗体函数
    Public Sub Ld(Kh As String, Ddh As String, Th As String, Jhs As String, Rq As String, Lx As String)
        Me.Show()
        TextBoxKh.Text = kh
        TextBoxDdh.Text = Ddh
        TextBoxTh.Text = Th
        TextBoxJhs.Text = Jhs
        TextBoxRq.Text = Rq
        ComboboxAdd(Lx)
        '载入分配记录信息
        F3JH.FPXX_Load(TextBoxDdh.Text, TextBoxTh.Text, DataGridView1, ComboBoxFP.Text)
    End Sub

    '保存记录按钮事件
    Private Sub ButtonA_Click(sender As Object, e As EventArgs) Handles ButtonA.Click
        Dim DT(9) As String
        DT(0) = ComboBoxFP.Text
        DT(1) = TextBoxKh.Text
        DT(2) = TextBoxDdh.Text
        DT(3) = TextBoxTh.Text
        DT(4) = TextBoxRq.Text
        DT(5) = TextBoxSl.Text
        DT(6) = TextBoxFzr.Text
        DT(7) = TextBoxShr.Text
        DT(8) = TextBoxZgs.Text
        DT(9) = TextBoxBz.Text
        WD_FPXX(DT)
        '载入分配记录信息
        F3JH.FPXX_Load(TextBoxDdh.Text, TextBoxTh.Text, DataGridView1, ComboBoxFP.Text)
        MessageBox.Show("保存记录成功!")
    End Sub

    '保存记录并退出按钮事件
    Private Sub ButtonB_Click(sender As Object, e As EventArgs) Handles ButtonB.Click
        Dim DT(9) As String
        DT(0) = ComboBoxFP.Text
        DT(1) = TextBoxKh.Text
        DT(2) = TextBoxDdh.Text
        DT(3) = TextBoxTh.Text
        DT(4) = TextBoxRq.Text
        DT(5) = TextBoxSl.Text
        DT(6) = TextBoxFzr.Text
        DT(7) = TextBoxShr.Text
        DT(8) = TextBoxZgs.Text
        DT(9) = TextBoxBz.Text
        WD_FPXX(DT)
        '载入分配记录信息
        F3JH.FPXX_Load(TextBoxDdh.Text, TextBoxTh.Text, DataGridView1, ComboBoxFP.Text)
        Me.Close()
        MessageBox.Show("保存记录成功!")
    End Sub



    '数据操作模块///////////////////////////////////////////////////////////////////////////////////////////////

    '任务分配记录保存及更改
    Public Sub WD_FPXX(DT() As String) '0记录类型1客户2订单号3图号4日期5数量6负责人7审核人8其他信息9备注 
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        Dim cn As New SqlConnection(F3JH.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "insert into 新物料信息 (记录类型,客户,订单号,图号,日期,数量,负责人,审核人,其他信息,备注,识别号) " +
           "values('" + DT(0) + "','" + DT(1) + "','" + DT(2) + "','" + DT(3) + "','" + DT(4) + "','" + DT(5) + "','" + DT(6) + "','" + DT(7) +
           "','" + DT(8) + "','" + DT(9) + "','" + Sbh + "')"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        Dim ZJ() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} '1加工分配2已加工3组装分配4已组装5送货分配6已送货7毛坯购入计划8毛坯已购9成品购入计划10成品已购
        Select Case DT(0)
            Case "加工分配"
                ZJ(0) = DT(5)
                Exit Select
            Case "外协分配"
                ZJ(0) = DT(5)
                Exit Select
            Case "组装分配"
                ZJ(2) = DT(5)
                Exit Select
            Case "送货分配"
                ZJ(4) = DT(5)
                Exit Select
            Case "调拨分配"
                ZJ(4) = DT(5)
                Exit Select
            Case "毛坯购入计划"
                ZJ(6) = DT(5)
                Exit Select
            Case "成品购入计划"
                ZJ(8) = DT(5)
                Exit Select
        End Select
        '新物料信息记录增减
        ZJ_Xwlxx(DT(2), DT(3), ZJ)
    End Sub

    '新物料信息记录增减
    Public Sub ZJ_Xwlxx(Ddh As String, Th As String, ZJ() As Integer) '0加工分配1已加工2组装分配3已组装4送货分配5已送货6毛坯购入计划7毛坯已购8成品购入计划9成品已购
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        Dim cn As New SqlConnection(F3JH.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = " update 新零部件信息 set 加工分配 =加工分配+" + ZJ(0).ToString + ", 已加工 =已加工 + " + ZJ(1).ToString +
            ", 组装分配 =组装分配 + " + ZJ(2).ToString + ", 已组装 =已组装 + " + ZJ(3).ToString + ", 送货分配 =送货分配 + " + ZJ(4).ToString + ", 已送货 =已送货 + " + ZJ(5).ToString +
            ", 毛坯购入计划 =毛坯购入计划 + " + ZJ(6).ToString + ", 毛坯已购 =毛坯已购 + " + ZJ(7).ToString +
            ", 成品购入计划 =成品购入计划 + " + ZJ(8).ToString + ", 成品已购 =成品已购 + " + ZJ(9).ToString +
            " where 订单号='" + Ddh + "' and 图号='" + Th + "' "
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub

    '数据操作模块=======================================================




    '数据载入模块////////////////////////////////////////////////////////////////////////////////////////////////
    '数据载入模块=======================================================



    '辅助功能/显示模块///////////////////////////////////////////////////////////////////////////////////////////

    '日期选择1
    Private Sub TextBoxRq_TextChanged(sender As Object, e As EventArgs) Handles TextBoxRq.Click
        MonthCalendar1.Visible = True
    End Sub
    '日期选择2
    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        TextBoxRq.Text = MonthCalendar1.SelectionStart.ToString("yyyy-MM-dd")
        MonthCalendar1.Visible = False
    End Sub

    '任务分配类型选择模块
    Public Sub FP_Load(Lx As String)
        Select Case Lx
            Case "加工分配"
                LabelFzr.Text = "负责人"
                LabelZgs.Text = "总工时"
                LabelSM.Text = "加工分配（说明）：毛坯 --（加工）--> 成品，毛坯减少成品增加，日期为计划日期。"
                Exit Sub
            Case "外协分配"
                LabelFzr.Text = "负责方"
                LabelZgs.Text = "总金额"
                LabelSM.Text = "外协分配（说明）：（外协加工）--> 成品，毛坯不变成品增加，日期为计划日期。"
                Exit Sub
            Case "组装分配"
                LabelFzr.Text = "负责人"
                LabelZgs.Text = "总工时"
                LabelSM.Text = "组装分配（说明）：零件成品 --（组装）--> 部件成品，零件成品减少部件成品增加，日期为计划日期。"
                Exit Sub
            Case "送货分配"
                LabelFzr.Text = "负责人"
                LabelZgs.Text = "总金额"
                LabelSM.Text = "送货分配（说明）：毛坯/成品 --（送货）--> 出库，毛坯/成品减少，日期为计划日期。"
                Exit Sub
            Case "调拨分配"
                LabelFzr.Text = "负责人"
                LabelZgs.Text = "接收方"
                LabelSM.Text = "调拨分配（说明）：毛坯/成品 --（调拨）--> 接收方，毛坯/成品减少，日期为计划日期。"
                Exit Sub
            Case "毛坯购入计划"
                LabelFzr.Text = "供应商"
                LabelZgs.Text = "总金额"
                LabelSM.Text = "毛坯购入计划（说明）：（毛坯购入）--> 入库，毛坯增加，日期为计划日期。"
                Exit Sub
            Case "成品购入计划"
                LabelFzr.Text = "供应商"
                LabelZgs.Text = "总金额"
                LabelSM.Text = "成品购入计划：（成品购入）--> 入库，成品增加，日期为计划日期。"
                Exit Sub
        End Select
    End Sub


    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView1}
        For i = 0 To dgv.Length - 1
            dgv(i).ReadOnly = True
            dgv(i).SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv(i).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgv(i).AllowUserToAddRows = False
            dgv(i).AllowUserToDeleteRows = False
            dgv(i).AllowUserToOrderColumns = False
            dgv(i).AllowUserToResizeRows = False
        Next
    End Sub

    'combobox选项加载模块
    Private Sub ComboboxAdd(Lx As String)
        Select Case Lx
            Case "加工分配", "外协分配"
                ComboBoxFP.Items.Clear()
                ComboBoxFP.Items.Add("加工分配")
                ComboBoxFP.Items.Add("外协分配")
                Exit Select
            Case "组装分配"
                ComboBoxFP.Items.Clear()
                ComboBoxFP.Items.Add(Lx)
                Exit Select
            Case "送货分配", "调拨分配"
                ComboBoxFP.Items.Clear()
                ComboBoxFP.Items.Add("送货分配")
                ComboBoxFP.Items.Add("调拨分配")
                Exit Select
            Case "毛坯购入计划"
                ComboBoxFP.Items.Clear()
                ComboBoxFP.Items.Add(Lx)
                Exit Select
            Case "成品购入计划"
                ComboBoxFP.Items.Clear()
                ComboBoxFP.Items.Add(Lx)
                Exit Select
        End Select
        ComboBoxFP.Text = Lx
    End Sub


    '辅助功能模块====================================================






End Class