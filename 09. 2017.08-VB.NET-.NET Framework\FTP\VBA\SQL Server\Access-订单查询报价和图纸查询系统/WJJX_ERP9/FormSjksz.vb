Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class FormSjksz

    '界面载入时函数
    Private Sub FormSjksz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxIP.Text = FormLogin.SQLIP
        TextBoxUsr.Text = FormLogin.SQLUsr
        TextBoxKey.Text = FormLogin.SQLKey
    End Sub

    '保存设置按钮事件
    Private Sub ButtonBcsz_Click(sender As Object, e As EventArgs) Handles ButtonBcsz.Click
        Try
            '更改登陆系统的数据库连接设置
            FormLogin.SQLIP = TextBoxIP.Text
            FormLogin.TextBoxSqlAdress.Text = TextBoxIP.Text
            FormLogin.SQLUsr = TextBoxUsr.Text
            FormLogin.SQLKey = TextBoxKey.Text
            '保存之前的登陆信息
            Dim str As String = Environment.CurrentDirectory
            Dim cn As New OleDbConnection
            cn.ConnectionString = "provider=Microsoft.Jet.oledb.4.0;Data source='" + str + "\WJJX_ERP.mdb'"
            cn.Open()
            Dim sql As String = "UPDATE 登陆信息 SET 用户名 = '" + TextBoxUsr.Text + "', 密码 = '" + TextBoxKey.Text + "' WHERE ID =1 "
            Dim cm As New OleDbCommand(sql, cn)
            cm.ExecuteNonQuery()
            'MessageBox.Show("数据库连接设置已保存！")
            Me.Close()
        Catch
            MessageBox.Show("数据库连接设置保存失败，请重试！")
        End Try
    End Sub

    '测试连接按钮事件
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If My.Computer.Network.Ping(TextBoxIP.Text) Then
            Else
                MsgBox("数据库连接失败！")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("数据库连接失败！")
            Exit Sub
        End Try
        Dim SQL_Connection As String = "Data Source=" + TextBoxIP.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + TextBoxUsr.Text + ";Password=" + TextBoxKey.Text + ";"
        Try
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            cn.Close() '断开连接
            MessageBox.Show("数据库连接成功！")
        Catch
            MessageBox.Show("数据库连接失败！")
        End Try
    End Sub
End Class