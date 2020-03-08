

Public Class F1Index

    Public SQL_Connection, SQL_Adress As String
    Dim LoginMode As Integer = 0


    Private Sub FormNIndex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        Me.Enabled = False
        '主界面DPI调整模块
        PictureBoxPkt.Visible = False
        PictureBoxOben.Location = New Point(-13, -1)
        Me.Size = New Size(1000, 740)
        PictureBoxMain.Location = New Point(60, 112)
        PictureBoxSet.Location = New Point(624, 617)
        PictureBoxSearch.Location = New Point(809, 617)

        F0Login.Show()


    End Sub



    '各个系统载入模块
    Private Sub PictureBoxMain_Click(sender As Object, e As EventArgs) Handles PictureBoxMain.Click
        Select Case LoginMode
            Case 1
                FormLogin.Show()
            Case 2
                F3JH.Show()
                Exit Sub
            Case 3
            Case 4
        End Select
    End Sub
























    '辅助功能模块////////////////////////////////////////////////////////////////////////////

    '主界面显示模块
    Private Sub PictureBoxMain_MMove(sender As Object, e As EventArgs) Handles PictureBoxMain.MouseMove
        Dim X, Y As Integer
        X = System.Windows.Forms.Cursor.Position.X.ToString() - Me.Location.X.ToString
        Y = System.Windows.Forms.Cursor.Position.Y.ToString() - Me.Location.Y.ToString
        Label1.Text = X.ToString + " X:Y " + Y.ToString
        If Y < 500 And Y > 155 Then
            Select Case X
                Case Is < 60
                    Exit Select
                Case Is < 275
                    LoginMode = 1
                    PictureBoxPkt.Visible = True
                    PictureBoxPkt.Location = New Point(180, 90)
                    Exit Select
                Case Is < 491
                    LoginMode = 2
                    PictureBoxPkt.Visible = True
                    PictureBoxPkt.Location = New Point(375, 90)
                    Exit Select
                Case Is < 706
                    LoginMode = 3
                    PictureBoxPkt.Visible = True
                    PictureBoxPkt.Location = New Point(570, 90)
                    Exit Select
                Case Is < 912
                    LoginMode = 4
                    PictureBoxPkt.Visible = True
                    PictureBoxPkt.Location = New Point(765, 90)
                    Exit Select
                Case Else
                    LoginMode = 0
                    PictureBoxPkt.Visible = False
            End Select
        Else
            PictureBoxPkt.Visible = False
        End If
    End Sub
    Private Sub PictureBoxMain_MLeave(sender As Object, e As EventArgs) Handles PictureBoxMain.MouseLeave
        PictureBoxPkt.Visible = False
    End Sub

End Class