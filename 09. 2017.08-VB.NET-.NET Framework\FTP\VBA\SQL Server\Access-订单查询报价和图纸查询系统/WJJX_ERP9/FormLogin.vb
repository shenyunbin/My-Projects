Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
Imports System.Net

Public Class FormLogin
    Public SQLIP As String = ""
    Public SQLUsr As String = "sa"
    Public SQLKey As String = "123456"
    Public Mxsfy As Double = 0
    Public GlyMode As Boolean = False
    Public Zhlx As String = "生产"
    Public Yhm As String = ""
    Public SetBj As Boolean = False
    Dim X As Long
    Dim Y As Long

    Private Sub FromLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RectangleShape1.Location = New System.Drawing.Point(-11, 203 * Me.Size.Height / 265)
        'F1Index.Show()


        ''让窗体居中
        X = Screen.PrimaryScreen.Bounds.Width
        Y = Screen.PrimaryScreen.Bounds.Height
        X = (X - 1280) / 2
        Y = (Y - 750) / 2
        '设置按钮不可见
        'LabelX.Visible = False
        LabelExit.Visible = False
        ButtonBj.Visible = False
        ButtonSc.Visible = False
        ButtonCk.Visible = False
        ButtonJs.Visible = False
        ButtonSh.Visible = False
        LabelAdmin.Visible = False

        ComboBox1.Items.Add("主界面")
        ComboBox1.SelectedItem = ComboBox1.Items(0)
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

    '窗体移动函数
    Dim expression As New Regex("(.*?),")
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" ( _
                                        ByVal hwnd As IntPtr, _
                                        ByVal wMsg As Integer, _
                                        ByVal wParam As Integer, _
                                        ByVal lParam As Integer) _
                                        As Boolean
    Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Const WM_SYSCOMMAND = &H112
    Const SC_MOVE = &HF010&
    Const HTCAPTION = 2

    Private Sub RectangleShape2_MouseDown(ByVal sender As Object, _
                            ByVal e As System.Windows.Forms.MouseEventArgs) _
                            Handles RectangleShape2.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
    End Sub

    Private Sub label1_MouseDown(ByVal sender As Object, _
                        ByVal e As System.Windows.Forms.MouseEventArgs) _
                        Handles Label1.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
    End Sub



    '关闭窗体
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        '保存之前的登陆信息
        Dim str As String = Environment.CurrentDirectory
        Dim cn As New OleDbConnection
        cn.ConnectionString = "provider=Microsoft.Jet.oledb.4.0;Data source='" + str + "\WJJX_ERP.mdb'"
        cn.Open()
        Dim sql As String = "UPDATE 登陆信息 SET IP地址 = '" + TextBoxSqlAdress.Text + "', 用户名 = '" + TextBoxUsrName.Text + "', 密码 = '" + TextBoxPassword.Text +
            "', 每小时费用 = " + Mxsfy.ToString + " WHERE ID =1 "
        Dim cm As New OleDbCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles LabelLogin.Click
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
                    LoginFertig(True)
                Case "会计主管"
                    SetBj = False
                    LoginFertig(True)
                Case "生产主管"
                    SetBj = False
                    LoginFertig(False)
                Case Else
                    SetBj = False
                    LoginFertig(False)
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL数据库连接失败!", "错误!")
        End Try
    End Sub

    Private Sub LoginFertig(mode As Boolean)
        LabelSjksz.Visible = False
        'Label3.Visible = False
        LabelUsrName.Visible = False
        LabelTo.Visible = False
        LabelPassword.Visible = False
        LabelSqlAdress.Visible = False
        TextBoxPassword.Visible = False
        TextBoxSqlAdress.Visible = False
        TextBoxUsrName.Visible = False
        ComboBox1.Visible = False
        LabelExit.Visible = True
        LabelLogin.Visible = False

        LabelX.Visible = True
        LabelExit.Visible = True
        ButtonSc.Visible = True
        ButtonCk.Visible = True
        ButtonSh.Visible = True

        If mode = True Then
            ButtonBj.Visible = True
            ButtonJs.Visible = True
            ButtonBj.Enabled = True
            ButtonJs.Enabled = True
            LabelAdmin.Visible = True
        Else
            ButtonBj.Visible = True
            ButtonJs.Visible = True
            ButtonBj.Enabled = False
            ButtonJs.Enabled = True
            LabelAdmin.Visible = True
        End If

        GlyMode = mode
    End Sub

    '关闭窗体
    Private Sub LabelExit_Click(sender As Object, e As EventArgs) Handles LabelExit.Click
        'Me.Close()
    End Sub

    '报价系统
    Private Sub ButtonBj_Click(sender As Object, e As EventArgs) Handles ButtonBj.Click
        Dim F = F2BJ
        If F.Created Then
            F.Opacity = 0
            F.Show()
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Opacity = 1
        Else
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Show()
        End If
    End Sub


    '生产系统
    Private Sub ButtonSc_Click(sender As Object, e As EventArgs) Handles ButtonSc.Click
        Dim F = FormNRK
        If F.Created Then
            F.Opacity = 0
            F.Show()
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Opacity = 1
        Else
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Show()
        End If
    End Sub

    '仓库系统
    Private Sub ButtonCk_Click(sender As Object, e As EventArgs) Handles ButtonCk.Click
        Dim F = FormNCK
        If F.Created Then
            F.Opacity = 0
            F.Show()
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Opacity = 1
        Else
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Show()
        End If
    End Sub

    '结算系统
    Private Sub ButtonJs_Click(sender As Object, e As EventArgs) Handles ButtonJs.Click
        Dim F = FormNCX
        If F.Created Then
            F.Opacity = 0
            F.Show()
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Opacity = 1
        Else
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Show()
        End If
    End Sub

    Private Sub LabelX_Click(sender As Object, e As EventArgs) Handles LabelX.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    '送货系统
    Private Sub ButtonSh_Click(sender As Object, e As EventArgs) Handles ButtonSh.Click
        Dim F = FormNSH
        If F.Created Then
            F.Opacity = 0
            F.Show()
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Opacity = 1
        Else
            F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
            F.SQL_Adress = TextBoxSqlAdress.Text
            F.Location = New Point(X, Y)
            F.WindowState = FormWindowState.Normal
            F.Show()
        End If
    End Sub

    '打开管理系统
    Private Sub LabelAdmin_Click(sender As Object, e As EventArgs) Handles LabelAdmin.Click
        FormSet.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        FormSet.SQL_Adress = TextBoxSqlAdress.Text
        FormSet.WindowState = FormWindowState.Normal
        FormSet.Show()
        'FormSet.TopMost = True
        'FormSet.TopMost = False
    End Sub

    '各个窗口之间快速转换
    Public Sub BJLoad(FME As Form)
        Dim F = F2BJ
        F.Location = FME.Location
        F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        F.SQL_Adress = TextBoxSqlAdress.Text
        F.Opacity = 0
        F.Show()
        F.WindowState = FME.WindowState
        System.Threading.Thread.Sleep(1)
        F.Opacity = 1
        FME.Hide()
    End Sub

    Public Sub SCLoad(FME As Form)
        Dim F = FormNRK
        F.Location = FME.Location
        F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        F.SQL_Adress = TextBoxSqlAdress.Text
        F.Opacity = 0
        F.Show()
        F.WindowState = FME.WindowState
        System.Threading.Thread.Sleep(1)
        F.Opacity = 1
        FME.Hide()
    End Sub

    Public Sub SHLoad(FME As Form)
        Dim F = FormNSH
        F.Location = FME.Location
        F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        F.SQL_Adress = TextBoxSqlAdress.Text
        F.Opacity = 0
        F.Show()
        F.WindowState = FME.WindowState
        System.Threading.Thread.Sleep(1)
        F.Opacity = 1
        FME.Hide()
    End Sub

    Public Sub CKLoad(FME As Form)
        Dim F = FormNCK
        F.Location = FME.Location
        F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        F.SQL_Adress = TextBoxSqlAdress.Text
        F.Opacity = 0
        F.Show()
        F.WindowState = FME.WindowState
        System.Threading.Thread.Sleep(1)
        F.Opacity = 1
        FME.Hide()
    End Sub

    Public Sub JSLoad(FME As Form)
        Dim F = FormNCX
        F.Location = FME.Location
        F.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        F.SQL_Adress = TextBoxSqlAdress.Text
        F.Opacity = 0
        F.Show()
        F.WindowState = FME.WindowState
        System.Threading.Thread.Sleep(1)
        F.Opacity = 1
        FME.Hide()
    End Sub

    Public Sub FClose()

        ' Dim X As Long
        ' Dim Y As Long
        ''让窗体居中
        ' X = Screen.PrimaryScreen.Bounds.Width
        'Y = Screen.PrimaryScreen.Bounds.Height
        ' X = (X - 1280) / 2
        'Y = (Y - 750) / 2
        'Try
        ' FormBJ.Location = New Point(X, Y)
        ' FormSC.Location = New Point(X, Y)
        ' FormSH.Location = New Point(X, Y)
        '  FormCK.Location = New Point(X, Y)
        ' FormJS.Location = New Point(X, Y)
        ' Catch ex As Exception
        '  End Try
    End Sub


    '数据库设置
    Private Sub LabelSjksz_Click_1(sender As Object, e As EventArgs) Handles LabelSjksz.Click
        FormSjksz.Show()
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


    Private Sub TextBoxSqlAdress_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSqlAdress.DoubleClick

        'FormNCK.SQL_Connection = "Data Source=" + TextBoxSqlAdress.Text + ";Initial Catalog=WJJX_ERP;Integrated Security=False;User ID=" + SQLUsr + ";Password=" + SQLKey + ";"
        'FormNCK.SQL_Adress = TextBoxSqlAdress.Text
        'FormNCK.Show()
    End Sub


End Class
