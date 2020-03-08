Imports System.Data.SqlClient
Public Class FormNBJ_THSS
    Public SSMode As Integer '12=部件编辑，13=零件编辑，
    Public SSnr As String '初始搜索内容

    '程序载入时操作
    Private Sub FormNBJ_THSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "型号/图号搜索"
        SetDVG()
        TextBoxTh.Text = SSnr
        Select Case SSMode
            Case 12, 13 '部件/零件
                Dim cn As New SqlConnection(F2BJ.SQL_Connection)
                Dim da As New SqlDataAdapter("select * from 零件信息 where 图号 like " & "'%" & SSnr & "%'", cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                DataGridView4.DataSource = ds.Tables("零件信息")
        End Select

    End Sub

    '图号搜索按钮事件
    Private Sub ButtonThss_Click(sender As Object, e As EventArgs) Handles ButtonThss.Click
        Select Case SSMode
            Case 12, 13 '部件/零件
                Dim cn As New SqlConnection(F2BJ.SQL_Connection)
                Dim da As New SqlDataAdapter("select * from 零件信息 where 图号 like " & "'%" & TextBoxTh.Text & "%'", cn)
                Dim ds As New DataSet
                da.Fill(ds, "零件信息")
                DataGridView4.DataSource = ds.Tables("零件信息")
        End Select
    End Sub

    '导入零部件操作
    Private Sub DataCopyTo(sender As Object, e As EventArgs) Handles ButtonDr.Click, DataGridView4.CellDoubleClick
        Select Case SSMode
            Case 12 '导入到部件
                DataCopyToBJ()
                Me.Close()
            Case 13 '导入到零件
                DataCopyToLJ()
                Me.Close()
        End Select
    End Sub


    '复制信息到零件信息
    Private Sub DataCopyToLJ()
        Dim F = FormNBJ_LJBJ
        Dim RowNum As Integer
        Dim data As String
        Dim dgv = DataGridView4

        RowNum = dgv.CurrentCellAddress.Y

        'data = dgv.Rows(RowNum).Cells(0).Value.ToString '编号
        'TextBoxLjbh.Text = data

        data = dgv.Rows(RowNum).Cells(1).Value.ToString '图号
        F.TextBoxTh.Text = data
        data = dgv.Rows(RowNum).Cells(2).Value.ToString '名称
        F.TextBoxMc0.Text = data
        data = dgv.Rows(RowNum).Cells(3).Value.ToString '台件
        F.TextBoxTj.Text = data

        '备注1/2数据处理
        Dim str(17) As String
        Dim str1(7) As String
        '提取备注1的信息
        InfoInput(dgv.Rows(RowNum).Cells(4).Value.ToString, str1)
        F.TextBoxMpdj.Text = str1(1) '毛坯单价
        F.TextBoxZbgs.Text = str1(3) '准备工时
        F.TextBoxHjgs.Text = str1(5) '合计工时
        '提取备注2的信息
        InfoInput(dgv.Rows(RowNum).Cells(5).Value.ToString, str)
        If str(0) = "外购" Then
            F.CheckBoxWg.Checked = True
        Else
            F.CheckBoxWg.Checked = False
        End If
        If str(1) = "自" Then
            F.CheckBoxZbl.Checked = True
        Else
            F.CheckBoxZbl.Checked = False
        End If
        If str(1) = "来料" Then
            F.CheckBoxLl.Checked = True
        Else
            F.CheckBoxLl.Checked = False
        End If
        F.TextBoxCz.Text = str(3) '材质
        F.TextBoxBlgg.Text = str(5) '备料规格
        F.TextBoxXlsl.Text = str(7) '下料数量
        F.TextBoxWgcc.Text = str(9) '完工尺寸
        F.TextBoxRcl.Text = str(11) '热处理
        F.TextBoxGx.Text = str(13) '工序
        F.TextBoxMpzl.Text = str(15) '毛坯重量

        data = dgv.Rows(RowNum).Cells(6).Value.ToString '材料价
        F.TextBoxClj.Text = data
        data = dgv.Rows(RowNum).Cells(7).Value.ToString '下料费
        F.TextBoxXlf.Text = data
        data = dgv.Rows(RowNum).Cells(8).Value.ToString '精加工费
        F.TextBoxJjgf.Text = data
        data = dgv.Rows(RowNum).Cells(9).Value.ToString '热处理费
        F.TextBoxRclf.Text = data
        data = dgv.Rows(RowNum).Cells(10).Value.ToString '表面处理费
        F.TextBoxBmclf.Text = data
        data = dgv.Rows(RowNum).Cells(11).Value.ToString '其他加工费
        F.TextBoxQtjgf.Text = data
        data = dgv.Rows(RowNum).Cells(12).Value.ToString '实付工资
        F.TextBoxSfgz.Text = data
        data = dgv.Rows(RowNum).Cells(13).Value.ToString '含税单价
        F.TextBoxHsdj.Text = data
        data = dgv.Rows(RowNum).Cells(14).Value.ToString '备注3
        F.TextBoxBz3.Text = data

    End Sub

    '复制信息到部件信息
    Private Sub DataCopyToBJ()
        Dim F = FormNBJ_BJBJ
        Dim RowNum As Integer
        Dim data As String
        Dim dgv = DataGridView4
        Try
            RowNum = dgv.CurrentCellAddress.Y

            'data = dgv.Rows(RowNum).Cells(0).Value.ToString '编号
            'TextBoxLjbh.Text = data

            data = dgv.Rows(RowNum).Cells(1).Value.ToString '型号
            F.TextBoxXh.Text = data
            data = dgv.Rows(RowNum).Cells(2).Value.ToString '名称
            F.TextBoxMc.Text = data

            '备注1/2数据处理
            Dim str(17) As String
            Dim str1(7) As String
            '提取备注1的信息
            InfoInput(dgv.Rows(RowNum).Cells(4).Value.ToString, str1)
            F.TextBoxMpdj.Text = str1(1) '毛坯单价
            F.TextBoxZbgs.Text = str1(3) '准备工时
            F.TextBoxHjgs.Text = str1(5) '合计工时
            '提取备注2的信息
            InfoInput(dgv.Rows(RowNum).Cells(5).Value.ToString, str)
            If str(0) = "外购" Then
                F.CheckBoxWg.Checked = True
            Else
                F.CheckBoxWg.Checked = False
            End If
            If str(1) = "自" Then
                F.CheckBoxZbl.Checked = True
            Else
                F.CheckBoxZbl.Checked = False
            End If
            If str(1) = "来料" Then
                F.CheckBoxLl.Checked = True
            Else
                F.CheckBoxLl.Checked = False
            End If
            F.TextBoxCz.Text = str(3) '材质
            F.TextBoxBlgg.Text = str(5) '备料规格
            F.TextBoxXlsl.Text = str(7) '下料数量
            F.TextBoxWgcc.Text = str(9) '完工尺寸
            F.TextBoxRcl.Text = str(11) '热处理
            F.TextBoxGx.Text = str(13) '工序
            F.TextBoxMpzl.Text = str(15) '毛坯重量

            data = dgv.Rows(RowNum).Cells(6).Value.ToString '材料价
            F.TextBoxClj.Text = data
            data = dgv.Rows(RowNum).Cells(7).Value.ToString '下料费
            F.TextBoxXlf.Text = data
            data = dgv.Rows(RowNum).Cells(8).Value.ToString '精加工费
            F.TextBoxJjgf.Text = data
            data = dgv.Rows(RowNum).Cells(9).Value.ToString '热处理费
            F.TextBoxRclf.Text = data
            data = dgv.Rows(RowNum).Cells(10).Value.ToString '表面处理费
            F.TextBoxBmclf.Text = data
            data = dgv.Rows(RowNum).Cells(11).Value.ToString '其他加工费
            F.TextBoxQtjgf.Text = data
            data = dgv.Rows(RowNum).Cells(12).Value.ToString '实付工资
            F.TextBoxSfgz.Text = data
            data = dgv.Rows(RowNum).Cells(13).Value.ToString '含税单价
            F.TextBoxHsdj.Text = data
            data = dgv.Rows(RowNum).Cells(14).Value.ToString '备注3
            F.TextBoxBz3.Text = data

        Catch
        End Try
    End Sub


    '长字符串分割模块
    Public Function InfoInput(inputString As String, str() As String)
        Dim ii As Integer = 0
        For i = 0 To inputString.Length - 1
            If inputString.Chars(i) = Chr(124) Or inputString.Chars(i) = Chr(58) Then
                str(ii) = Trim(str(ii))
                ii += 1
            Else
                str(ii) += inputString.Chars(i)
            End If
        Next
        str(ii) = Trim(str(ii))
        Return ii
    End Function

    '设置图表格式函数
    Private Sub SetDVG()
        Dim dgv() = {DataGridView4}
        For i = 0 To dgv.Length - 1
            dgv(i).ReadOnly = True
            dgv(i).SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv(i).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgv(i).AllowUserToAddRows = False
            dgv(i).AllowUserToDeleteRows = False
            dgv(i).AllowUserToOrderColumns = False
            dgv(i).AllowUserToResizeRows = False
        Next
    End Sub
End Class