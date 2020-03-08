Imports System.Data.SqlClient

Public Class FormNBJ_DDBJ
    Public XZMode As Boolean = True  'True=新增订单 False=修改订单
    '对话框载入时触发的事件
    Private Sub FormNBJ_DDBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If XZMode Then
            '更改按钮标识
            Me.Text = "新建订单"
            ButtonXjdd.Text = "新建订单"
            LabelInfo.Text = "新建订单："
            '载入状态信息
            ComboBoxZt.Items.Add("报价")
            ComboBoxZt.Items.Add("生产")
            ComboBoxZt.SelectedItem = ComboBoxZt.Items(1)
            '载入客户信息
            KhLoad()
        Else
            '载入状态信息
            ComboBoxZt.Items.Add("报价")
            ComboBoxZt.Items.Add("生产")
            ComboBoxZt.Items.Add("已完成")
            '更改按钮标识
            Me.Text = "修改订单"
            LabelInfo.Text = "修改订单" + F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Ddh).Value.ToString + "："
            ButtonXjdd.Text = "修改订单"
            '载入订单信息
            TextBoxDdbh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Bh).Value.ToString
            TextBoxDdh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Ddh).Value.ToString
            ComboBoxKh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Kh).Value.ToString
            TextBoxPh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Ph).Value.ToString
            TextBoxBz.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Bz).Value.ToString
            ComboBoxZt.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Zt).Value.ToString
        End If

    End Sub


    '新增/修改订单信息按钮事件
    Private Sub ButtonXjdd_Click(sender As Object, e As EventArgs) Handles ButtonXjdd.Click
        Dim i As Integer
        Try
            '获取之前活动的单元格
            i = F2BJ.DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try

        If XZMode Then
            '新增订单信息
            InsertDdxx()
        Else
            '更新订单信息
            UpdateDdxx()
        End If

        '载入订单信息
        F2BJ.DGV_Load(F2BJ.DGV_Mode, vbNullString, vbNullString, F2BJ.DataGridView1)
        If i > 0 Then
            F2BJ.DataGridView1.CurrentCell = F2BJ.DataGridView1(0, i)
        End If
        Me.Close()
        MessageBox.Show("新增/修改订单信息成功!")
    End Sub


    '新增订单信息822
    Private Sub InsertDdxx()
        Dim Bh As String = TextBoxDdbh.Text
        Dim Ddh As String = TextBoxDdh.Text
        Dim Kh As String = ComboBoxKh.Text
        Dim Ph As String = TextBoxPh.Text
        Dim Bz As String = TextBoxBz.Text
        Dim Zt As String = ComboBoxZt.Text
        If Ddh = "" Or Kh = "" Then
            MessageBox.Show("订单号或者客户未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Try
            Dim cn As New SqlConnection(F2BJ.SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "insert into 订单信息 (编号,订单号,客户,批号,备注,状态) values(" + Bh + ",'" + Ddh + "','" + Kh + "','" + Ph + "','" + Bz + "','" + Zt + "')"
            Dim cm As New SqlCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("新建订单失败，请检查一下要新建的订单号与之前的订单号有没有重复。", "填写错误！")
        End Try
    End Sub

    '载入客户信息
    Private Sub KhLoad()
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        Dim da As New SqlDataAdapter("select 客户 from 订单信息  group by 客户 order by 客户 asc", cn)
        Dim ds As New DataSet
        da.Fill(ds, "订单信息")
        For i = 0 To ds.Tables(0).Rows.Count - 1
            ComboBoxKh.Items.Add(ds.Tables(0).Rows(i).Item(0).ToString)
        Next
    End Sub



    '修改订单信息822
    Private Sub UpdateDdxx()
        Dim DdhOld As String = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV1_Ddh).Value.ToString
        Dim Bh As String = TextBoxDdbh.Text
        Dim Ddh As String = TextBoxDdh.Text
        Dim Kh As String = ComboBoxKh.Text
        Dim Ph As String = TextBoxPh.Text
        Dim Bz As String = TextBoxBz.Text
        Dim Zt As String = ComboBoxZt.Text
        If Not DdhOld = Ddh Then
            MessageBox.Show("所选择要修改的订单号与上方填写的订单号不一致!", "填写错误！")
            Exit Sub
        End If
        If Ddh = "" Or Kh = "" Then
            MessageBox.Show("订单号或者客户未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "UPDATE 订单信息 SET 编号=" + Bh + ", 订单号='" + Ddh + "', 客户 = '" + Kh + "', 批号 = '" + Ph + "', 备注 = '" + Bz + "', 状态 = '" + Zt + "' WHERE 订单号 = '" + DdhOld + "'"
        Dim cm1 As New SqlCommand(sql, cn)
        cm1.ExecuteNonQuery()
        sql = "UPDATE 部件信息 SET 订单号='" + Ddh + "', 状态 = '" + Zt + "' WHERE 订单号 = '" + DdhOld + "'"
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        sql = "UPDATE 零件信息 SET 订单号='" + Ddh + "', 状态 = '" + Zt + "' WHERE 订单号 = '" + DdhOld + "'"
        Dim cm3 As New SqlCommand(sql, cn)
        cm3.ExecuteNonQuery()
        cn.Close()
    End Sub


    '清除订单数据
    Private Sub LabelQcsj1_Click(sender As Object, e As EventArgs) Handles LabelQcsj1.Click
        ClearDdxx()
    End Sub

    '清除订单数据
    Private Sub ClearDdxx()
        Dim Data As String = ""
        TextBoxDdbh.Text = Data
        TextBoxDdh.Text = Data
        ComboBoxKh.Text = Data
        TextBoxPh.Text = Data
        TextBoxBz.Text = Data
        ComboBoxZt.Text = "生产"
    End Sub


End Class