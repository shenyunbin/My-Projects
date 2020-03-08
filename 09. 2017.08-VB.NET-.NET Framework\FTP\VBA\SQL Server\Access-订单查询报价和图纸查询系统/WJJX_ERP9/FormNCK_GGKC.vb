Imports System.Data.SqlClient

Public Class FormNCK_GGKC
    Dim Kh, Mc, Th, Xykc As String

    '窗口载入时按钮事件
    Private Sub FormNCK_GGKC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kh = FormNCK.DataGridView1.CurrentRow.Cells.Item(FormNCK.DGV1_Kh).Value.ToString
        Th = FormNCK.DataGridView1.CurrentRow.Cells.Item(FormNCK.DGV1_Th).Value.ToString
        Mc = FormNCK.DataGridView1.CurrentRow.Cells.Item(FormNCK.DGV1_Mc).Value.ToString
        Xykc = FormNCK.DataGridView1.CurrentRow.Cells.Item(FormNCK.DGV1_Xykc).Value.ToString
        Label1.Text = Kh + "-" + Th + Mc + "更改库存："
        TextBox1.Text = Xykc
    End Sub

    '更改库存按钮事件
    Private Sub ButtonQRGG_Click(sender As Object, e As EventArgs) Handles ButtonQRGG.Click
        Dim Mprks As Integer = 0
        Try
            Mprks = TextBox2.Text - TextBox1.Text
        Catch ex As Exception
            MessageBox.Show("更改库存失败，请输入数字。")
            Exit Sub
        End Try
        Dim Sbh = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        '完成仓库数量的更改(仓库毛坯入库数增加,如仓库有货则修改信息,否则新增信息 if exists sql语句)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Dim sql As String = "if exists(select * from 仓库信息 where 客户='" + Kh + "' and 图号='" + Th + "') " +
                                        "update 仓库信息 set 毛坯入库数=毛坯入库数+" + Mprks.ToString + ", 现有库存=现有库存+" + Mprks.ToString + " where 客户='" + Kh + "' and 图号='" + Th + "' " +
                                        "else insert into 仓库信息 (客户,图号,名称,毛坯入库数,送货数量,现有库存,毛坯备注,识别号) values('" + Kh + "','" + Th + "','" + Mc + "'," + Mprks.ToString + ",0," + Mprks.ToString + ",'','" + Sbh + "')"
        Dim cn As New SqlConnection(FormNCK.SQL_Connection)
        cn.Open()
        Dim cm2 As New SqlCommand(sql, cn)
        cm2.ExecuteNonQuery()
        cn.Close()
        MessageBox.Show("更改库存成功！")
        FormNCK.Ckxx_Load()
        FormNCK.ButtonA.Text = "图号搜索"
        FormNCK.ButtonA.ForeColor = Color.Black
        Me.Close()
    End Sub
End Class