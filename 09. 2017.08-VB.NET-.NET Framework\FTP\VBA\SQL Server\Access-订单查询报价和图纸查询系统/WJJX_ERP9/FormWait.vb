Imports System.Threading

Public Class FormWait
    Public F As Thread
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        F.Abort()
        Me.Close()
    End Sub
End Class