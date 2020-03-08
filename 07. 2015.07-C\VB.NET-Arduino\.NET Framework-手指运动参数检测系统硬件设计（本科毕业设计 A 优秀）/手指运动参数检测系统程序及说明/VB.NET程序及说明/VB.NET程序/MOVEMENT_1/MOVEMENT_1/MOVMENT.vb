Imports System
Imports System.IO.Ports
Imports System.Text.RegularExpressions


Public Class MOVMENT
    Dim T3D As New Tran3D
    Dim WData As New WriteData

    Dim threaddisp1 As New System.Threading.Thread(AddressOf disp1)
    Dim threaddisp2 As New System.Threading.Thread(AddressOf DISP2)

    Dim myPen4 As New System.Drawing.Pen(System.Drawing.Color.Black)
    Dim myPen As New System.Drawing.Pen(System.Drawing.Color.Black)
    Dim GRA1 As System.Drawing.Graphics
    Dim GRV As System.Drawing.Graphics

    Dim X0, Y0 As Integer
    Dim AX, AY, AZ, AXi, AYi, AZi, VX, VY, VZ, XX, XY, XZ, Ai As Integer
    Dim XXi(200), XYi(200), XZi(200) As Integer
    Dim VXi(200), VYi(200), VZi(200) As Integer
    Dim lock, ifmathcal As Boolean

    '程序初始化函数
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lock = True
        myPen4.Width = 1
        GRA1 = PictureBox1.CreateGraphics()
        Timer2.Interval = 50
        Timer2.Enabled = True

        '获取计算机有效串口
        portnamebox.Items.Clear()
        Dim ports As String() = SerialPort.GetPortNames() '必须用命名空间，用SerialPort,获取计算机的有效串口
        Dim port As String
        For Each port In ports
            portnamebox.Items.Add(port) '向combobox中添加项
        Next port
        SerialPort1.ReceivedBytesThreshold = 26
        '初始化界面
        baudratebox.Text = 115200
        Try
            portnamebox.SelectedIndex() = 0
            Serial_Port1() '初始化串口
            Label3.Text = SerialPort1.IsOpen
            SerialPort1.ReadTimeout = 1000
        Catch ex As Exception

        End Try
        RichTextBox1.Text = "Ax,Ay,Az,Xx,Xy,Xz数据："
        RichTextBox1.Text &= vbCrLf

        FileName.Text = "D:\MOVEMENT_DATA\"
    End Sub

    '窗口拖动函数
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

    '窗口拖动函数，按下时可以随鼠标拖动
    Private Sub moving_MouseDown(ByVal sender As Object, _
                            ByVal e As System.Windows.Forms.MouseEventArgs) Handles moving.MouseDown


        ReleaseCapture()
        SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
    End Sub

    '设置串口参数
    Private Sub Serial_Port1() '设置串口参数
        SerialPort1.BaudRate = Val(baudratebox.Text) '波特率
        SerialPort1.PortName = portnamebox.Text '串口名称
        SerialPort1.DataBits = 8 '数据位
        SerialPort1.StopBits = IO.Ports.StopBits.Two '停止位
        SerialPort1.Parity = IO.Ports.Parity.Even '校验位
    End Sub

    '打开串口函数
    Private Sub Buttonopen_Click(sender As Object, e As EventArgs) Handles Buttonopen.Click
        Try
            portnamebox.SelectedIndex() = 0
            Serial_Port1() '初始化串口
            Label3.Text = SerialPort1.IsOpen
        Catch ex As Exception

        End Try
        If threaddisp1.ThreadState = Threading.ThreadState.Unstarted Then
            threaddisp1.Start()
        End If
        If threaddisp2.ThreadState = Threading.ThreadState.Unstarted Then
            threaddisp2.Start()
        End If

        Serialclose = False
        If SerialPort1.IsOpen = False Then
            SerialPort1.Open() '打开串口
            SerialPort1.RtsEnable = True
            Label3.Text = SerialPort1.IsOpen
            If SerialPort1.IsOpen = True Then
                Label3.Text = "串口已连接"
            End If
        End If
    End Sub

    '关闭串口函数（串口速度过快时会死机）
    Dim Serialclose As Boolean
    Private Sub Buttonclose_Click(sender As Object, e As EventArgs) Handles Buttonclose.Click
        Serialclose = True
    End Sub

    '触发接收事件
    Public Sub Sp_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Me.Invoke(New EventHandler(AddressOf Sp_Receiving)) '调用接收数据函数
        If Serialclose = True Then
            SerialPort1.Close()
        End If
    End Sub

    '获取串口状态函数
    Private Sub portnamebox_Click(sender As Object, e As EventArgs) Handles portnamebox.Click
        '获取计算机有效串口
        portnamebox.Items.Clear()
        Dim ports As String() = SerialPort.GetPortNames() '必须用命名空间，用SerialPort,获取计算机的有效串口
        Dim port As String
        For Each port In ports
            portnamebox.Items.Add(port) '向combobox中添加项
        Next port
        '初始化界面
        baudratebox.Text = 115200
        Try
            portnamebox.SelectedIndex() = 0
            Serial_Port1() '初始化串口
            Label3.Text = SerialPort1.IsOpen
        Catch ex As Exception

        End Try
    End Sub

    '接收数据函数（important！）
    Dim c As Integer
    Dim bytes(26) As Byte
    Dim RF_eable As Boolean
    Private Sub Sp_Receiving(ByVal sender As Object, ByVal e As EventArgs)
        'c = SerialPort1.BytesToRead
        SerialPort1.Read(bytes, 1, 26)
        SerialPort1.DiscardInBuffer()
        RF_eable = True
        DProcess()
    End Sub

    '窗口关闭函数
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        threaddisp1.Abort()
        threaddisp2.Abort()
        Me.Close()
    End Sub

    '加速度传感器位置锁定函数
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        lock = Not lock
    End Sub

    '设置加速度传感器图形在空间的位置
    Private Sub SetLocation(x As Double, y As Double, z As Double)
        X0 = 350 + TranX(x, y)
        Y0 = 250 + TranY(x, z)
    End Sub

    '将三维坐标x，z转换为二维坐标y
    Function TranY(tx As Integer, tz As Integer) As Integer
        Return Int(tz + tx / 2.82842712)
    End Function

    '将三维坐标x，y转换为二维坐标x
    Function TranX(tx As Integer, ty As Integer) As Integer
        Return Int(ty - tx / 2.82842712)
    End Function

    '将三维图形线段转换为二维图形线段
    Private Sub SpaceToView(SpaceX1 As Integer, SpaceY1 As Integer, SpaceZ1 As Integer, SpaceX2 As Integer, SpaceY2 As Integer, SpaceZ2 As Integer)
        GRA1.DrawLine(myPen4, X0 + TranX(SpaceX1, SpaceY1), Y0 + TranY(SpaceX1, SpaceZ1), X0 + TranX(SpaceX2, SpaceY2), Y0 + TranY(SpaceX2, SpaceZ2))
    End Sub

    '将带旋转角度的三维图形线段转换为二维图形线段
    Private Sub SpaceToViewWithAngle(xi As Integer, yi As Integer, zi As Integer, xii As Integer, yii As Integer, zii As Integer, axi As Double, ayi As Double)
        Dim t(3) As Double
        T3D.CoRot(xi, yi, zi, axi, ayi)
        t = T3D.xyzout
        T3D.CoRot(xii, yii, zii, axi, ayi)
        SpaceToView(t(0), t(1), t(2), T3D.xout, T3D.yout, T3D.zout)
    End Sub

    '绘画加速度传感器的状态图形
    Private Sub DrawXYZ(x0 As Integer, y0 As Integer, z0 As Integer, axi As Double, ayi As Double)
        myPen4.Width = 2
        Dim size As Integer
        myPen4.Color = Color.Red
        T3D.CoRot(x0, 0, 0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        myPen4.Color = Color.Green
        T3D.CoRot(0, y0, 0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        myPen4.Color = Color.Black
        T3D.CoRot(0, 0, z0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        myPen4.Color = Color.Yellow
        size = 10
        SpaceToViewWithAngle(size, size, 0, size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(size, -size, 0, -size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, -size, 0, -size, size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, size, 0, size, size, 0, axi, ayi)
        size -= 4
        SpaceToViewWithAngle(size, size, 0, size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(size, -size, 0, -size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, -size, 0, -size, size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, size, 0, size, size, 0, axi, ayi)
    End Sub

    '清除加速度传感器的状态图形
    Private Sub ClearXYZ(x0 As Integer, y0 As Integer, z0 As Integer, axi As Double, ayi As Double)
        myPen4.Width = 2
        Dim size As Integer
        myPen4.Color = Color.White
        T3D.CoRot(x0, 0, 0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        T3D.CoRot(0, y0, 0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        T3D.CoRot(0, 0, z0, axi, ayi)
        SpaceToView(0, 0, 0, T3D.xout, T3D.yout, T3D.zout)
        size = 10
        SpaceToViewWithAngle(size, size, 0, size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(size, -size, 0, -size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, -size, 0, -size, size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, size, 0, size, size, 0, axi, ayi)
        size -= 4
        SpaceToViewWithAngle(size, size, 0, size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(size, -size, 0, -size, -size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, -size, 0, -size, size, 0, axi, ayi)
        SpaceToViewWithAngle(-size, size, 0, size, size, 0, axi, ayi)
    End Sub

    '画图形边界
    Private Sub DrawBound()
        SetLocation(0, 0, 0)
        Dim mlen As Integer
        mlen = 180
        myPen4.Color = Color.Black
        myPen4.Width = 1
        SpaceToView(mlen, mlen, mlen, -mlen, mlen, mlen)
        SpaceToView(mlen, mlen, mlen, mlen, -mlen, mlen)
        SpaceToView(mlen, mlen, mlen, mlen, mlen, -mlen)
        SpaceToView(-mlen, mlen, -mlen, -mlen, mlen, mlen) '  s
        SpaceToView(-mlen, -mlen, mlen, mlen, -mlen, mlen)
        SpaceToView(-mlen, -mlen, mlen, -mlen, mlen, mlen)
        SpaceToView(mlen, -mlen, -mlen, mlen, -mlen, mlen) 'cc
        SpaceToView(mlen, mlen, -mlen, mlen, -mlen, -mlen)
        SpaceToView(mlen, mlen, -mlen, -mlen, mlen, -mlen)
        SpaceToView(-mlen, -mlen, -mlen, -mlen, -mlen, mlen)
        SpaceToView(-mlen, -mlen, -mlen, mlen, -mlen, -mlen)
        SpaceToView(-mlen, -mlen, -mlen, -mlen, mlen, -mlen)
    End Sub

    '用于定时显示接收的数据
    Private Sub DATADISP()
        RichTextBox1.Text = "Ax,Ay,Az,Xx,Xy,Xz数据："
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += "Ax:   Ay:   Az:"
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += AX.ToString
        RichTextBox1.Text &= vbTab
        RichTextBox1.Text += AY.ToString
        RichTextBox1.Text &= vbTab
        RichTextBox1.Text += AZ.ToString
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += "Xx:   Xy:   Xz:"
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += XX.ToString
        RichTextBox1.Text &= vbTab
        RichTextBox1.Text += XY.ToString
        RichTextBox1.Text &= vbTab
        RichTextBox1.Text += XZ.ToString
        RichTextBox1.Text &= vbCrLf
        Labelx.Text = "x:"
        Labelx.Text += (XX / 4).ToString
        Labely.Text = "y:"
        Labely.Text += (XY / 4).ToString
        Labelz.Text = "z:"
        Labelz.Text += (-XZ / 4).ToString

        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += "X轴角度:"
        RichTextBox1.Text += (Math.Atan(iay / Math.Sqrt(iaz ^ 2 + iax ^ 2)) * 180 / Math.PI).ToString
        RichTextBox1.Text &= vbCrLf
        RichTextBox1.Text += "Y轴角度:"
        RichTextBox1.Text += (-Math.Atan(iax / Math.Sqrt(iaz ^ 2 + iay ^ 2)) * 180 / Math.PI).ToString
        RichTextBox1.Text &= vbCrLf

    End Sub

    '数组循环函数
    Private Sub Cir(ci() As Integer, num As Integer, temp As Double)
        For i = 0 To num - 2
            ci(i) = ci(i + 1)
        Next
        ci(num - 1) = temp
    End Sub

    '画速度变化曲线子函数
    Private Sub DRAWVi(vi() As Integer)
        For i = 0 To 197
            GRV.DrawLine(myPen, i, 50 - vi(i + 1) + vi(i), i + 1, 50 - vi(i + 2) + vi(i + 1))
        Next
    End Sub

    '画速度变化曲线
    Private Sub DRAWV(x1() As Integer, x2() As Integer, x3() As Integer)
        GRV = PictureBox4.CreateGraphics()
        GRV.Clear(Color.White)
        DRAWVi(x1)
        GRV = PictureBox5.CreateGraphics()
        GRV.Clear(Color.White)
        DRAWVi(x2)
        GRV = PictureBox6.CreateGraphics()
        GRV.Clear(Color.White)
        DRAWVi(x3)
    End Sub

    '画XY平面轨迹图函数
    Private Sub DRAWXY(ix() As Integer, iy() As Integer)
        GRV = PictureBox2.CreateGraphics()
        GRV.Clear(Color.White)
        For i = 150 To 198
            GRV.DrawLine(myPen, 100 + iy(i), 70 + ix(i), 100 + iy(i + 1), 70 + ix(i + 1))
        Next
    End Sub

    '定时显示写入数据函数
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        DATADISP()
        WData.WXYZ(AX, AY, AZ, AXi, AYi, AZi, VX, VY, VZ, XX, XY, XZ)
    End Sub

    '主界面显示函数
    Public Sub DISP1()
        While True
            DRAWALL()
            Threading.Thread.Sleep(10)
        End While
    End Sub

    '综合画图函数，负责画三轴加速度传感器的方向位移（important！）
    Dim ff, xx0, xy0, xz0, a0, b0, c0 As Integer
    Dim anglex, angley As Integer
    Dim iax, iay, iaz, ixx, ixy, ixz As Integer
    Public Sub DRAWALL()
        iax = AX
        iay = AY
        iaz = AZ
        ixx = XX
        ixy = XY
        ixz = XZ
        SetLocation(a0, b0, c0)
        ClearXYZ(xx0, xy0, xz0, anglex, angley)
        SetLocation(0, 0, 0)
        ClearXYZ(xx0, xy0, xz0, anglex, angley)
        If lock Then
            Try
                SetLocation(ixx, ixy, ixz)
                anglex = Math.Atan(iay / Math.Sqrt(iaz ^ 2 + iax ^ 2)) * 180 / Math.PI
                angley = -Math.Atan(iax / Math.Sqrt(iaz ^ 2 + iay ^ 2)) * 180 / Math.PI
                DrawXYZ(iax, iay, iaz, anglex, angley) 'Math.Atan(AY / AZ) * 180 / Math.PI, Math.Atan(AX / AZ) * 180 / Math.PI
                xx0 = iax
                xy0 = iay
                xz0 = iaz
                a0 = ixx
                b0 = ixy
                c0 = ixz
            Catch ex As Exception

            End Try
        Else
            Try
                SetLocation(0, 0, 0)
                anglex = Math.Atan(iay / iaz) * 180 / Math.PI
                angley = -Math.Atan(iax / iaz) * 180 / Math.PI
                DrawXYZ(iax, iay, iaz, anglex, angley) 'Math.Atan(AY / AZ) * 180 / Math.PI, Math.Atan(AX / AZ) * 180 / Math.PI
                xx0 = iax
                xy0 = iay
                xz0 = iaz
                a0 = ixx
                b0 = ixy
                c0 = ixz
            Catch ex As Exception
            End Try
        End If
        DrawBound()
    End Sub

    '处理接收数据函数
    Public Sub DProcess()
            If RF_eable = True Then
                If bytes(1) = 253 And bytes(26) = 254 Then
                    AX = -(bytes(2) * 256 + bytes(3) - 32767) / 2
                    AY = -(bytes(4) * 256 + bytes(5) - 32767) / 2
                    AZ = (bytes(6) * 256 + bytes(7) - 32767) / 2

                    AXi = (bytes(8) * 256 + bytes(9) - 32767)
                    AYi = (bytes(10) * 256 + bytes(11) - 32767)
                    AZi = -(bytes(12) * 256 + bytes(13) - 32767)

                    VX = (bytes(14) * 256 + bytes(15) - 32767)
                    VY = (bytes(16) * 256 + bytes(17) - 32767)
                    VZ = -(bytes(18) * 256 + bytes(19) - 32767)

                    XX = (bytes(20) * 256 + bytes(21) - 32767) / 50
                    XY = (bytes(22) * 256 + bytes(23) - 32767) / 50
                    XZ = -(bytes(24) * 256 + bytes(25) - 32767) / 50

                    'WData.WXYZ(AX, AY, AZ, AXi, AYi, AZi, VX, VY, VZ, XX, XY, XZ)

                    Cir(XXi, 200, XX / 4)
                    Cir(XYi, 200, XY / 4)
                    Cir(XZi, 200, XZ / 4)
                    RF_eable = False
                End If
            End If
    End Sub

    '用于数据清除
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        RichTextBox1.Clear()
        RichTextBox1.Text = "Ax,Ay,Az,Xx,Xy,Xz数据："
        RichTextBox1.Text &= vbCrLf
    End Sub

    '用于画边界和速度变化图
    Private Sub DISP2()
        While True
            DRAWV(XXi, XYi, XZi)
            DRAWXY(XXi, XYi)
        End While

    End Sub

    '数据开始写入到txt
    Private Sub WriteStart_Click(sender As Object, e As EventArgs) Handles WriteStart.Click
        WData.OpenFile(FileName.Text)
        WData.WE = True
        WriteStart.Text = "写入中…"
    End Sub

    '数据写入停止
    Private Sub WriteStop_Click(sender As Object, e As EventArgs) Handles WriteStop.Click
        WData.CloseFile()
        WData.WE = False
        WriteStart.Text = "开始写入"
    End Sub

    '打开所在文件夹
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim path As String = IO.Path.GetDirectoryName(WData.fname)
        Process.Start(path)
    End Sub

End Class




'数据写入txt文件
Public Class WriteData
    Public fname, nextline As String
    Public WE As Boolean
    Public Sub SaveFile(str As String)
        My.Computer.FileSystem.WriteAllText(str,
        "================================== MOVEMENT DETECTION SYSTEM ==================================", False)
    End Sub

    '打开已经存在的txt文件
    Public Function OpenFile(str As String)
        Dim nowtime As String
        nowtime = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")
        Try
            fname = str + "DATA " + nowtime + ".txt"
            SaveFile(fname)
            FileOpen(1, fname, OpenMode.Append)
            Print(1, vbCrLf)
            Print(1, " >>Data Acquisition Time:")
            PrintLine(1, nowtime)
            PrintLine(1, " >> Data Received From The Sensor:")
            PrintLine(1, " Ax0" + vbTab + "Ay0" + vbTab + "Az0" + vbTab +
                      "Ax" + vbTab + "Ay" + vbTab + "Az" + vbTab +
                      "Vx" + vbTab + "Vy" + vbTab + "Vz" + vbTab +
                      "Xx" + vbTab + "Xy" + vbTab + "Xz")
            PrintLine(1, "-----------------------------------------------------------------------------------------------")
        Catch ex As Exception

        End Try
        OpenFile = str
    End Function

    '写字符串
    Public Sub WS(str As String)
        Print(1, str)                '将字符串nextline写入文件后不换行
    End Sub

    '写整型数据
    Public Sub WD(int As Integer)
        Print(1, int)
        Print(1, vbTab)
    End Sub

    '写整型数据并换行
    Public Sub WL(int As Integer)
        PrintLine(1, int)   '将整型数recordNumber写入文件后再换行
    End Sub

    '写加速度和位移数据
    Public Sub WXYZ(ax As Integer, ay As Integer, az As Integer,
                    axi As Integer, ayi As Integer, azi As Integer,
                    vx As Integer, vy As Integer, vz As Integer,
                    xx As Integer, xy As Integer, xz As Integer)
        Try
            If WE = True Then
                WD(-ax * 2)
                WD(-ay * 2)
                WD(-az * 2)
                WD(axi)
                WD(ayi)
                WD(azi)
                WD(vx)
                WD(vy)
                WD(vz)
                WD(xx * 50)
                WD(xy * 50)
                WL(xz * 50)
            End If
        Catch ex As Exception

        End Try

    End Sub

    '关闭打开的txt文件
    Public Sub CloseFile()
        Try
            FileClose(1)
        Catch ex As Exception

        End Try
    End Sub

End Class




'空间坐标转换
Public Class Tran3D
    Public a11, a12, a13, a21, a22, a23, a31, a32, a33, xout, yout, zout, xyzout(3), axis_x(3), axis_y(3), agx, agy As Double

    '空间坐标初始化
    Public Sub init()
        axis_x = {1, 0, 0}
        axis_y = {0, 1, 0}
        a11 = a12 = a13 = a21 = a22 = a23 = a31 = a32 = a33 = xout = yout = 0
    End Sub

    '绕指定旋转轴旋转agi角度时的空间x坐标转换
    Public Function CoRotX(x As Double, y As Double, z As Double, a As Double, b As Double, c As Double, agi As Double) As Double
        agi = (agi / 180) * Math.PI
        a11 = a * a + (1 - a * a) * Math.Cos(agi)
        a21 = a * b * (1 - Math.Cos(agi)) - c * Math.Sin(agi)
        a31 = a * c * (1 - Math.Cos(agi)) + b * Math.Sin(agi)
        Return a11 * x + a21 * y + a31 * z
    End Function

    '绕指定旋转轴旋转agi角度时的空间y坐标转换
    Public Function CoRotY(x As Double, y As Double, z As Double, a As Double, b As Double, c As Double, agi As Double) As Double
        agi = (agi / 180) * Math.PI
        a12 = a * b * (1 - Math.Cos(agi)) + c * Math.Sin(agi)
        a22 = b * b + (1 - b * b) * Math.Cos(agi)
        a32 = b * c * (1 - Math.Cos(agi)) - a * Math.Sin(agi)
        Return a12 * x + a22 * y + a32 * z
    End Function

    '绕指定旋转轴旋转agi角度时的空间z坐标转换
    Public Function CoRotZ(x As Double, y As Double, z As Double, a As Double, b As Double, c As Double, agi As Double) As Double
        agi = (agi / 180) * Math.PI
        a11 = a * c * (1 - Math.Cos(agi)) - b * Math.Sin(agi)
        a21 = b * c * (1 - Math.Cos(agi)) + a * Math.Sin(agi)
        a31 = c * c + (1 - c * c) * Math.Cos(agi)
        Return a11 * x + a21 * y + a31 * z
    End Function

    '改变y轴位置
    Public Sub CoRotAxisY(x As Double, y As Double, z As Double, agi As Double)
        xout = CoRotX(x, y, z, axis_y(0), axis_y(1), axis_y(2), agi)
        yout = CoRotY(x, y, z, axis_y(0), axis_y(1), axis_y(2), agi)
        zout = CoRotZ(x, y, z, axis_y(0), axis_y(1), axis_y(2), agi)
    End Sub

    '改变x轴位置
    Public Sub CoRotAxisX(x As Double, y As Double, z As Double, agi As Double)
        xout = CoRotX(x, y, z, axis_x(0), axis_x(1), axis_x(2), agi)
        yout = CoRotY(x, y, z, axis_x(0), axis_x(1), axis_x(2), agi)
        zout = CoRotZ(x, y, z, axis_x(0), axis_x(1), axis_x(2), agi)
    End Sub

    '空间坐标绕任意轴旋转变换基础函数
    Public Sub CoRotBasic(x As Double, y As Double, z As Double, a As Double, b As Double, c As Double, agi As Double)
        Dim ll, t(3) As Double
        ll = Math.Sqrt(a ^ 2 + b ^ 2 + c ^ 2)
        t = {a / ll, b / ll, c / ll}
        xout = CoRotX(x, y, z, t(0), t(1), t(2), agi)
        yout = CoRotY(x, y, z, t(0), t(1), t(2), agi)
        zout = CoRotZ(x, y, z, t(0), t(1), t(2), agi)
    End Sub

    '三轴加速度传感器坐标转换（used）
    Public Sub CoRot(x As Double, y As Double, z As Double, agi1 As Double, agi2 As Double)
        axis_x = {1, 0, 0}
        axis_y = {0, 1, 0} ' axis_y = {0, Math.Cos(agi0), Math.Sin(agi0)}
        CoRotAxisX(x, y, z, agi1)
        CoRotAxisY(xout, yout, zout, agi2)
        xyzout = {xout, yout, zout}
    End Sub

End Class






