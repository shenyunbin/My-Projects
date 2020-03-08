Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
Imports System.Net

Public Class F0Login
    Public SQLIP As String = ""
    Public SQLUsr As String = "sa"
    Public SQLKey As String = "123456"
    Public Mxsfy As Double = 0
    Public GlyMode As Boolean = False
    Public Zhlx As String = "生产"
    Public Yhm As String = ""
    Public SetBj As Boolean = False

    Dim iMode As Boolean = False

    Private Sub FromLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'F1Index.Show()
        '载入登陆信息
        Dim str As String = Environment.CurrentDirectory
        Dim cn As New OleDbConnection
        cn.ConnectionString = "provider=Microsoft.Jet.oledb.4.0;Data source='" + str + "\WJJX_ERP.mdb'"
        cn.Open()
        Dim sql As String = "select * from 登陆信息 order by ID asc"
        Dim da As New OleDbDataAdapter(sql, cn)
        Dim ds As New System.Data.DataSet
        da.Fill(ds, "登陆信息")
        Dim sql2 As String = "select * from 数据库设置 order by ID asc"
        Dim da2 As New OleDbDataAdapter(sql2, cn)
        Dim ds2 As New System.Data.DataSet
        da2.Fill(ds2, "登陆信息")
        cn.Close()
        TextBoxSqlAdress.Text = ds.Tables(0).Rows(0).Item(1).ToString  '"192.168.1.66" '"192.168.2.102"127.0.0.1
        TextBoxUsrName.Text = ds.Tables(0).Rows(0).Item(2).ToString '"sa"
        TextBoxPassword.Text = ds.Tables(0).Rows(0).Item(3).ToString ' "123456"
        Mxsfy = ds.Tables(0).Rows(0).Item(4).ToString
        SQLIP = TextBoxSqlAdress.Text
        SQLUsr = ds2.Tables(0).Rows(0).Item(1).ToString
        SQLKey = ds2.Tables(0).Rows(0).Item(2).ToString
    End Sub

   


    '登陆按钮事件
    Private Sub Login_Click(sender As Object, e As EventArgs) Handles PictureBoxLogin.Click
        Try
            If My.Computer.Network.Ping(TextBoxSqlAdress.Text) Then
            Else
                MsgBox("数据库连接失败！")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("数据库连接失败！")
            Exit Sub
        End Try
        Dim SQL_Connection As String = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        Try
            Dim cn As New SqlConnection(SQL_Connection)
            cn.Open() '插入前，必须连接  
            '用户名
            Yhm = TextBoxUsrName.Text
            Dim sql As String = "select 账户类型 from 登陆信息 where 用户名='" + Yhm + "' and 密码='" + TextBoxPassword.Text + "'"
            Dim da As New SqlDataAdapter(sql, cn)
            Dim ds As New System.Data.DataSet
            da.Fill(ds, "登陆信息")
            cn.Close() '断开连接
            '账户类型
            Zhlx = ds.Tables(0).Rows(0).Item(0).ToString
            Select Case Zhlx
                Case "管理员"
                    SetBj = True
                Case "会计主管"
                    SetBj = False
                Case "生产主管"
                    SetBj = False
                Case Else
                    SetBj = False
            End Select

            Dim F = F1Index
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Enabled = True
            F.Opacity = 1
            iMode = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("连接失败或者用户名或密码有误，新输入正确的IP、用户名密码。", "错误提示")
        End Try
    End Sub





    '数据库设置
    Private Sub Sjksz_Click_1(sender As Object, e As EventArgs) Handles PictureBoxSZ.Click
        FormSjksz.Show()
    End Sub


    '其他数据处理模块///////////////////////////////////////////////////////////////


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

    '打开图纸模块
    Public Function OpenPic(Kh As String, Th As String) As Boolean
        Dim PicTo As String = "FTP://" + TextBoxSqlAdress.Text + "/各单位图纸/" + ThToFileName(Kh) + "/" + ThToFileName(Th)
        Try
            My.Computer.Network.DownloadFile(PicTo + ".jpg", "D:\temp.jpg", "wjjx", "123456", True, 100, True)
            System.Diagnostics.Process.Start("D:\temp.jpg")
            Return True
            Exit Function
        Catch ex As Exception
        End Try
        Try
            My.Computer.Network.DownloadFile(PicTo + ".pdf", "D:\temp.pdf", "wjjx", "123456", True, 100, True)
            System.Diagnostics.Process.Start("D:\temp.pdf")
            Return True
            Exit Function
        Catch ex As Exception
        End Try
        Try
            My.Computer.Network.DownloadFile(PicTo + ".bmp", "D:\temp.bmp", "wjjx", "123456", True, 100, True)
            System.Diagnostics.Process.Start("D:\temp.bmp")
            Return True
            Exit Function
        Catch ex As Exception
        End Try
        Try
            My.Computer.Network.DownloadFile(PicTo + ".dwg", "D:\temp.dwg", "wjjx", "123456", True, 100, True)
            System.Diagnostics.Process.Start("D:\temp.dwg")
            Return True
            Exit Function
        Catch ex As Exception
        End Try
        Dim MsgOk As Integer = MessageBox.Show("您要打开的图纸不存在，是否要上传此图纸？", "提示", MessageBoxButtons.OKCancel)
        If MsgOk = DialogResult.OK Then
            Return False
        Else
            Return True
        End If
    End Function

    '保存图片函数
    Public Sub SavePic(PicFrom As String, Kh As String, Th As String)
        Dim Ext As String = IO.Path.GetExtension(PicFrom)
        Dim PicTo As String = "FTP://" + TextBoxSqlAdress.Text + "/各单位图纸/" + ThToFileName(Kh) + "/" + ThToFileName(Th) + Ext
        Try
            My.Computer.Network.UploadFile(PicFrom, PicTo, "wjjx", "123456", True, 100, FileIO.UICancelOption.DoNothing)
            Exit Sub
        Catch ex As Exception
        End Try
        Try
            CreatFolder(Kh)
            My.Computer.Network.UploadFile(PicFrom, PicTo, "wjjx", "123456", True, 100, FileIO.UICancelOption.DoNothing)
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub


    Public Sub CreatFolder(Kh As String)
        'System.IO.Directory.CreateDirectory("ftp://" & "wjjx" & ":" & "123456" & "@" & "10.0.0.66" & "/" & "文件夹名称")
        Try
            Dim frq As FtpWebRequest = CType(FtpWebRequest.Create("ftp://" & TextBoxSqlAdress.Text & "/各单位图纸/" & Kh), FtpWebRequest)
            Dim fcr As New NetworkCredential("wjjx", "123456")
            Dim frp As FtpWebResponse
            frq.Credentials = fcr '认证信息  
            frq.Method = WebRequestMethods.Ftp.MakeDirectory
            frp = CType(frq.GetResponse, FtpWebResponse) '发送、操作、并返回  
            frp.Close()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub MEClosed() Handles Me.FormClosed
        If Not iMode Then
            F1Index.Close()
        End If
    End Sub



End Class
