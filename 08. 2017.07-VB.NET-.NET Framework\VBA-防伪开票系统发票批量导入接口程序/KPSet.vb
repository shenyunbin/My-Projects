Imports System.Data.OleDb
Public Class KPSet
    Dim MDB_ADR As String
    '主窗口载入时读取信息并显示
    Private Sub KPSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Path As String = Environment.CurrentDirectory
        MDB_ADR = "provider=Microsoft.Jet.oledb.4.0;Data source='" + Path + "\KPXX.mdb'"
        '读取数据库各个信息并显示
        DataGridViewInit()
        '设置Dataview控件的属性
        DataGridViewKPSet.ReadOnly = True
        DataGridViewKPSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridViewKPSet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewKPSet.AllowUserToAddRows = False
        DataGridViewKPSet.AllowUserToDeleteRows = False
        DataGridViewKPSet.AllowUserToOrderColumns = False
        DataGridViewKPSet.AllowUserToResizeRows = False
    End Sub

    '修改其他开票信息（复核人、收款人和备注）
    Private Sub ButtonChange_Click(sender As Object, e As EventArgs) Handles ButtonChange.Click
        Dim Fhr As String = TextBoxFhr.Text
        Dim Skr As String = TextBoxSkr.Text
        Dim Bz As String = TextBoxBz.Text
        Dim Sl As String = TextBoxSl.Text
        If Fhr = "" Or Skr = "" Then
            MessageBox.Show("复核人或收款人未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "UPDATE 其他开票信息 SET 复核人 = '" + Fhr + "', 收款人 = '" + Skr + "', 备注 = '" + Bz + "', 税率 = '" + Sl + "' WHERE ID = 1"
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show("开票复核人、收款人和备注信息修改成功！！！", "修改完成！")
        Catch ex As Exception
            MessageBox.Show("开票复核人、收款人和备注信息修改失败！", "错误！")
        End Try
    End Sub

    '增加开票信息条目
    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim Qyid As String = TextBoxQyid.Text
        Dim Qyjc As String = TextBoxQyjc.Text
        Dim Gfmc As String = TextBoxGfmc.Text
        Dim Gfsh As String = TextBoxGfsh.Text
        Dim Yhzh As String = TextBoxYhzh.Text
        Dim Dzdh As String = TextBoxDzdh.Text
        If Qyid = "" Or Gfmc = "" Then
            MessageBox.Show("企业ID或者购方名称未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "insert into 各单位开票信息 (ID,购方名称,购方税号,购方银行账号,购方地址电话,企业简称) values('" + Qyid + "','" + Gfmc + "','" + Gfsh + "','" + Yhzh + "','" + Dzdh + "','" + Qyjc + "')"
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show(Gfmc + "的开票信息添加成功！！！", "添加完成！")
        Catch ex As Exception
            MessageBox.Show("添加开票信息条目失败！", "错误！")
        End Try
    End Sub

    '更新表格中开票信息条目
    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Dim Qyid As String = TextBoxQyid.Text
        Dim Qyjc As String = TextBoxQyjc.Text
        Dim Gfmc As String = TextBoxGfmc.Text
        Dim Gfsh As String = TextBoxGfsh.Text
        Dim Yhzh As String = TextBoxYhzh.Text
        Dim Dzdh As String = TextBoxDzdh.Text
        If Qyid = "" Or Gfmc = "" Then
            MessageBox.Show("企业ID或者购方名称未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "UPDATE 各单位开票信息 SET 购方名称 = '" + Gfmc + "', 购方税号 = '" + Gfsh + "', 购方银行账号 = '" + Yhzh + "', 购方地址电话 = '" + Dzdh + "', 企业简称 = '" + Qyjc + "' WHERE ID = " + Qyid
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show(Gfmc + "的开票信息修改成功！！！", "修改完成！")
        Catch ex As Exception
            MessageBox.Show("开票信息修改失败！", "错误！")
        End Try
    End Sub

    '点击表格读取表格中各个企业开票信息
    Private Sub DataGridViewKPSet_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewKPSet.CellClick
        TextBoxQyid.Text = DataGridViewKPSet.CurrentRow.Cells(0).Value.ToString
        TextBoxGfmc.Text = DataGridViewKPSet.CurrentRow.Cells(1).Value.ToString
        TextBoxGfsh.Text = DataGridViewKPSet.CurrentRow.Cells(2).Value.ToString
        TextBoxYhzh.Text = DataGridViewKPSet.CurrentRow.Cells(3).Value.ToString
        TextBoxDzdh.Text = DataGridViewKPSet.CurrentRow.Cells(4).Value.ToString
        TextBoxQyjc.Text = DataGridViewKPSet.CurrentRow.Cells(5).Value.ToString
    End Sub

    '删除开票信息条目
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Dim Qyid As String = TextBoxQyid.Text
        Dim Gfmc As String = TextBoxGfmc.Text
        If Qyid = "" Then
            MessageBox.Show("企业ID未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "Delete from 各单位开票信息 where ID=" + Qyid
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show(Gfmc + "的开票信息删除成功！！！", "删除完成！")
        Catch ex As Exception
            MessageBox.Show("删除开票信息条目失败！", "错误！")
        End Try
    End Sub

    '读取数据库各个信息并显示
    Private Sub DataGridViewInit()
        '读取数据库各单位开票信息
        Dim cn As New OleDbConnection
        cn.ConnectionString = MDB_ADR
        cn.Open()
        Dim sql As String = "select * from 各单位开票信息 order by ID asc"
        Dim da As New OleDbDataAdapter(sql, cn)
        Dim ds As New System.Data.DataSet
        da.Fill(ds, "各单位开票信息")
        DataGridViewKPSet.DataSource = ds.Tables(0)
        '读取复核人、收款人、备注信息
        'cn.Open()
        Dim sql2 As String = "select * from 其他开票信息 order by ID asc"
        Dim da2 As New OleDbDataAdapter(sql2, cn)
        Dim ds2 As New System.Data.DataSet
        da2.Fill(ds2, "其他开票信息")
        TextBoxFhr.Text = ds2.Tables(0).Rows(0).Item(1).ToString     '将复核人值赋予Fhr
        TextBoxSkr.Text = ds2.Tables(0).Rows(0).Item(2).ToString    '将收款人值赋予Skr
        TextBoxBz.Text = ds2.Tables(0).Rows(0).Item(3).ToString     '将备注的值赋予Bz
        TextBoxSsflbm.Text = ds2.Tables(0).Rows(0).Item(4).ToString     '显示税收分类编号
        TextBoxSpbm.Text = ds2.Tables(0).Rows(0).Item(5).ToString     '显示商品编码
        TextBoxSsflbmmc.Text = ds2.Tables(0).Rows(0).Item(6).ToString '显示税收分类编码名称
        TextBoxSl.Text = ds2.Tables(0).Rows(0).Item(8).ToString '显示税率
        cn.Close()
    End Sub

    '关闭窗口后刷新主窗口数据
    Private Sub KPSet_Closed(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Try
            KPv1.DataGridViewInit()
            '表格初始化及检查表格格式是否正确
            KPv1.DataCheck()
        Catch ex As Exception

        End Try
    End Sub

    '税收编码更改并保存
    Private Sub ButtonSsflbmgg_Click(sender As Object, e As EventArgs) Handles ButtonSsflbmgg.Click
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "UPDATE 其他开票信息 SET 税收编码 = '" + TextBoxSsflbm.Text + "'  ,税收编码名称 = '" + TextBoxSsflbmmc.Text + "' WHERE ID = 1"
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show("税收编码修改成功！！！", "修改完成！")
        Catch ex As Exception
            MessageBox.Show("税收编码修改失败！", "错误！")
        End Try
    End Sub

    '商品编码更改保存
    Private Sub ButtonSpbmgg_Click(sender As Object, e As EventArgs) Handles ButtonSpbmgg.Click
        Try
            Dim cn As New OleDbConnection(MDB_ADR)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "UPDATE 其他开票信息 SET 商品编码 = '" + TextBoxSpbm.Text + "' WHERE ID = 1"
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            cn.Close()
            '读取数据库各个信息并显示
            DataGridViewInit()
            MessageBox.Show("商品编码修改成功！！！", "修改完成！")
        Catch ex As Exception
            MessageBox.Show("商品编码修改失败！", "错误！")
        End Try
    End Sub

    Private Sub TextBoxSsflbm_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSsflbm.TextChanged
        TextBoxSsbmws.Text = "已输入" + TextBoxSsflbm.Text.Length.ToString + "位"
    End Sub

    Private Sub TextBoxSpbm_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSpbm.TextChanged
        TextBoxSpbmws.Text = "已输入" + TextBoxSpbm.Text.Length.ToString + "位，共19位"
    End Sub
End Class