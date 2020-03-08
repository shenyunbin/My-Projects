Imports System.Data.SqlClient
Public Class FormNBJ_LJBJ
    Public XZMode As Boolean 'True=新增零件，False=修改零件

    Dim LJTH As String = vbNullString

    Dim Leftjoin = " left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                     "left join 订单信息 on  零件信息.订单号=订单信息.订单号" +
                     " left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 "

    Dim Leftjoin2 = "left join 订单信息 on  部件信息.订单号=订单信息.订单号"

    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////

    '程序载入时操作
    Private Sub FormNBJ_LJBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxMxsfy.Text = FormLogin.Mxsfy
        If XZMode Then '新增零件
            '更改按钮标识
            Me.Text = "订单" + F2BJ.G_Ddh.ToString + "部件" + F2BJ.G_Xh.ToString + " - 新增零件"
            ButtonXzBj.Text = "新增零件"
            '显示数据模块
            DataDisp()
            TextBoxLjbh.Text = F2BJ.DataGridView1.Rows.Count
        Else '修改零件
            LJTH = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Th).Value.ToString
            '更改按钮标识
            Me.Text = "订单" + F2BJ.G_Ddh.ToString + "部件" + F2BJ.G_Xh.ToString + " - 修改零件" + LJTH
            ButtonXzBj.Text = "修改零件"
            '显示数据模块
            DataDisp()

        End If
    End Sub

    '新增/修改零件按钮事件
    Private Sub ButtonXzBj_Click(sender As Object, e As EventArgs) Handles ButtonXzBj.Click
        Dim i As Integer
        Try
            '获取之前活动的单元格
            i = F2BJ.DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try

        If XZMode Then '新增零件----------------------------

            If Not ThMcTest(F2BJ.G_Kh, TextBoxTh.Text, TextBoxMc0.Text) Then
                Exit Sub
            End If
            If TextBoxLjbh.Text = "0" Or TextBoxTj.Text = "0" Or TextBoxLjbh.Text = "" Or TextBoxTj.Text = "" Then
                MessageBox.Show("新增零件失败！新增零件编号和台件数不能为空或者0。", "输入错误！")
                Exit Sub
            End If
            Try
                '新增零件信息
                InsertLjxx()
            Catch ex As Exception
                MessageBox.Show("编号和含税单价只能为数字。", "输入错误！")
            End Try

        Else '修改零件--------------------------------------

            If TextBoxLjbh.Text = "0" Or TextBoxTj.Text = "0" Or TextBoxLjbh.Text = "" Or TextBoxTj.Text = "" Then
                MessageBox.Show("修改零件失败！零件编号和台件数不能为空或者0。", "输入错误！")
                Exit Sub
            End If
            '检测是否编辑的是代表部件的零件信息，并提示
            'If FormNBJ.DataGridView1.CurrentRow.Cells.Item(FormNBJ.DGV3_Bh).Value = 0 Then
            'MessageBox.Show("修改失败！此为部件，此处无法修改，请到上一级部件信息中修改本部件的信息。")
            'End If
            If Not ThMcTest(F2BJ.G_Kh, TextBoxTh.Text, TextBoxMc0.Text) Then
                Exit Sub
            End If
            CalculateHsdj()
            '修改零件信息
            UpdateLjxx()
        End If

        '载入零件信息
        F2BJ.DGV_Load(F2BJ.DGV_Mode, F2BJ.G_Ddh, F2BJ.G_Xh, F2BJ.DataGridView1)
        Try
            '设置之前的活动单元格
            F2BJ.DataGridView1.CurrentCell = F2BJ.DataGridView1(0, i)
        Catch
        End Try

        '载入零件详细信息（后补！！）

        Me.Close()
        MessageBox.Show("新增/修改零件信息成功!")
    End Sub

    '零件搜索并复制载入功能
    Private Sub ButtonThss_Click(sender As Object, e As EventArgs) Handles ButtonThss.Click
        FormNBJ_THSS.SSMode = 13
        FormNBJ_THSS.SSnr = TextBoxTh.Text
        FormNBJ_THSS.Show()
    End Sub

    '清空输入数据
    Private Sub LabelQKSJ_Click(sender As Object, e As EventArgs) Handles LabelQKSJ.Click
        TextBoxMpdj.Text = vbNullString  '毛坯单价
        TextBoxZbgs.Text = vbNullString '准备工时
        TextBoxHjgs.Text = vbNullString '合计工时

        CheckBoxWg.Checked = False

        CheckBoxZbl.Checked = False

        CheckBoxLl.Checked = False

        TextBoxCz.Text = vbNullString '材质
        TextBoxBlgg.Text = vbNullString '备料规格
        TextBoxXlsl.Text = vbNullString '下料数量
        TextBoxWgcc.Text = vbNullString '完工尺寸
        TextBoxRcl.Text = vbNullString '热处理
        TextBoxGx.Text = vbNullString '工序
        TextBoxMpzl.Text = vbNullString '毛坯重量

        TextBoxLjbh.Text = vbNullString
        TextBoxTh.Text = vbNullString
        TextBoxMc0.Text = vbNullString
        TextBoxTj.Text = vbNullString

        TextBoxClj.Text = vbNullString
        TextBoxJjgf.Text = vbNullString
        TextBoxXlf.Text = vbNullString
        TextBoxRclf.Text = vbNullString
        TextBoxBmclf.Text = vbNullString
        TextBoxQtjgf.Text = vbNullString
        TextBoxSfgz.Text = vbNullString
        TextBoxBz3.Text = vbNullString
    End Sub

    '全局操作模块================================================



    '数据处理模块////////////////////////////////////////////////////////////////////////////////

    '新增零件信息
    Private Sub InsertLjxx()
        Dim Bh As String = TextBoxLjbh.Text
        Dim Th As String = TextBoxTh.Text
        Dim Mc As String = TextBoxMc0.Text
        Dim Jtsl As String = TextBoxTj.Text
        Dim Bz1 As String = vbNullString
        Dim Bz2 As String = vbNullString
        Scbz(Bz1, Bz2)
        Dim Clj As String = TextBoxClj.Text
        Dim Xlf As String = TextBoxXlf.Text
        Dim Jjgf As String = TextBoxJjgf.Text
        Dim Rclf As String = TextBoxRclf.Text
        Dim Bmclf As String = TextBoxBmclf.Text
        Dim Qtjgf As String = TextBoxQtjgf.Text
        Dim Sfgz As String = TextBoxSfgz.Text
        Dim Hsdj As String = TextBoxHsdj.Text
        Dim Bz3 As String = TextBoxBz3.Text
        Dim Zt As String = F2BJ.G_Zt
        Dim Ddh As String = F2BJ.G_Ddh
        Dim Xh As String = F2BJ.G_Xh
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        If Ddh = "" Or Xh = "" Or Th = "" Then
            MessageBox.Show("图号、订单号或型号未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        For i = 0 To F2BJ.DataGridView1.Rows.Count - 1
            If Th = F2BJ.DataGridView1.Rows(i).Cells(F2BJ.DGV3_Th).Value.ToString Then
                MessageBox.Show("图号重复,不能新增零件!", "填写错误！")
                Exit Sub
            End If
        Next
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "if not exists(select * from 零件信息 where 订单号='" + Ddh + "' and 型号='" + Xh + "' and 图号='" + Th + "')  " +
            "insert into 零件信息 (编号,图号,名称,台件,备注1,备注2,材料价,下料费,精加工费,热处理费,表面处理费,其他加工费,实付工资,含税单价,备注3,状态,订单号,型号,识别号) " +
            "values(" + Bh + ",'" + Th + "','" + Mc + "','" + Jtsl + "','" + Bz1 + "','" + Bz2 + "','" + Clj + "','" + Xlf + "','" + Jjgf + "','" + Rclf + "','" +
        Bmclf + "','" + Qtjgf + "','" + Sfgz + "','" + Hsdj + "','" + Bz3 + "','" + Zt + "','" + Ddh + "','" + Xh + "','" + Sbh + "')"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub

    '修改零件信息
    Private Function UpdateLjxx()
        Dim ThOld As String
        Try
            ThOld = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Th).Value.ToString
        Catch
            MessageBox.Show("未选择零件。", "操作错误！")
            Return False
            Exit Function
        End Try
        Dim Bh As String = TextBoxLjbh.Text
        Dim Th As String = TextBoxTh.Text
        Dim Mc As String = TextBoxMc0.Text
        Dim Jtsl As String = TextBoxTj.Text
        Dim Bz1 As String = vbNullString
        Dim Bz2 As String = vbNullString
        Scbz(Bz1, Bz2)
        Dim Clj As String = TextBoxClj.Text
        Dim Xlf As String = TextBoxXlf.Text
        Dim Jjgf As String = TextBoxJjgf.Text
        Dim Rclf As String = TextBoxRclf.Text
        Dim Bmclf As String = TextBoxBmclf.Text
        Dim Qtjgf As String = TextBoxQtjgf.Text
        Dim Sfgz As String = TextBoxSfgz.Text
        Dim Hsdj As String = TextBoxHsdj.Text
        Dim Bz3 As String = TextBoxBz3.Text
        Dim Ddh As String = F2BJ.G_Ddh
        Dim Xh As String = F2BJ.G_Xh
        If Not ThOld = Th Then
            MessageBox.Show("所选择要修改的图号与上方填写的图号不一致!", "填写错误！")
            Return False
            Exit Function
        End If
        If Ddh = "" Or Xh = "" Or Th = "" Then
            MessageBox.Show("图号、订单号或型号未填写,请再次填写!", "填写错误！")
            Return False
            Exit Function
        End If
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "UPDATE 零件信息 SET  编号=" + Bh + ", 图号='" + Th + "', 名称 = '" + Mc + "', 台件 = '" + Jtsl + "', 备注1 = '" + Bz1 + "', 备注2 = '" + Bz2 +
                           "', 材料价 = '" + Clj + "', 下料费 = '" + Xlf + "', 精加工费 = '" + Jjgf + "', 热处理费 = '" + Rclf +
                            "', 表面处理费 = '" + Bmclf + "', 其他加工费 = '" + Qtjgf + "', 实付工资 = '" + Sfgz + "', 含税单价 = '" + Hsdj +
                              "', 备注3 = '" + Bz3 + "' WHERE 图号 = '" + ThOld + "' AND 订单号 = '" + Ddh + "' AND 型号 = '" + Xh + "'"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        Return True
    End Function



    '检查以前订单中已经含有相同客户、相同图号的部件或零件 True=未出现异常，False=异常
    Private Function ThMcTest(Kh As String, Th As String, Mc As String)
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        Dim sql As String = "select 订单信息.客户,零件信息.图号,零件信息.名称 " +
                                 "from 零件信息  " +
                                 "left join 订单信息 on  零件信息.订单号=订单信息.订单号 " +
                                 "where  订单信息.客户='" + Kh + "' and 零件信息.图号='" + Th + "' and 名称 !='" + Mc + "' group by 订单信息.客户,零件信息.图号,零件信息.名称"
        Dim da As New SqlDataAdapter(sql, cn)
        Dim ds As New DataSet
        da.Fill(ds, "客户图号名称检测")
        If ds.Tables(0).Rows.Count = 0 Then
            Return True
        Else
            Dim msg As String = "经检查，以前订单中已经含有相同客户、相同图号的部件或零件 [" + ds.Tables(0).Rows(0).Item(0).ToString() + " " + ds.Tables(0).Rows(0).Item(1).ToString() + " " + ds.Tables(0).Rows(0).Item(2).ToString() +
                "] ，是否需要更改所有对应此图号的零件或者部件的名称？" '点击 [ 确定/OK ]  后，会在所有系统中示新的名称并新建或更改零件。点击 [ 确定/OK ]  将不会保存更改。
            If MessageBox.Show(msg, "警告！", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                Dim Yth As String = Th
                Dim Xmc As String = Mc
                'Dim Kh As String = TextBoxKh.Text
                'Dim Sql As String
                'Dim cn As New SqlConnection(SQL_Connection)
                cn.Open() '插入前，必须连接  

                sql = "UPDATE 部件信息  SET 部件信息.名称='" + Xmc + "' FROM 部件信息 " + Leftjoin2 + " WHERE 部件信息.型号='" + Yth + "' and 订单信息.客户='" + Kh + "'"
                Dim cm1 As New SqlCommand(sql, cn)
                cm1.ExecuteNonQuery()

                sql = "UPDATE 零件信息  SET 零件信息.名称='" + Xmc + "' FROM 零件信息 " + Leftjoin + " WHERE 零件信息.图号='" + Yth + "' and 订单信息.客户='" + Kh + "'"
                Dim cm2 As New SqlCommand(sql, cn)
                cm2.ExecuteNonQuery()

                sql = "UPDATE 仓库信息 SET 名称='" + Xmc + "' WHERE 图号='" + Yth + "' and 客户='" + Kh + "'"
                Dim cm3 As New SqlCommand(sql, cn)
                cm3.ExecuteNonQuery()

                sql = "UPDATE 入库信息 SET 名称='" + Xmc + "' WHERE 图号='" + Yth + "' and 客户='" + Kh + "'"
                Dim cm4 As New SqlCommand(sql, cn)
                cm4.ExecuteNonQuery()

                sql = "UPDATE 送货信息 SET 名称='" + Xmc + "' WHERE 型号='" + Yth + "' and 客户='" + Kh + "'"
                Dim cm5 As New SqlCommand(sql, cn)
                cm5.ExecuteNonQuery()

                sql = "UPDATE 模板信息 SET 名称='" + Xmc + "' WHERE 图号='" + Yth + "' and 客户='" + Kh + "'"
                Dim cm6 As New SqlCommand(sql, cn)
                cm6.ExecuteNonQuery()

                cn.Close()
                'MessageBox.Show("更改名称成功！")
                Return True
            Else
                Return False
            End If
        End If
    End Function


    '生成备注信息
    Private Sub Scbz(ByRef Bz1 As String, ByRef Bz2 As String)
        '生成价格
        Scjg()
        '写入备注
        Bz1 = "毛坯单价: " + TextBoxMpdj.Text + " | 准备工时: " + TextBoxZbgs.Text + " | 合计工时: " + TextBoxHjgs.Text
        If CheckBoxWg.Checked = True Then
            Bz2 = "外购 "
        Else
            Bz2 = ""
        End If
        If CheckBoxZbl.Checked = True Then
            Bz2 += "| 自 "
        Else
            Bz2 += "|"
        End If
        If CheckBoxLl.Checked = True Then
            Bz2 += " 来料 "
        Else
            Bz2 += ""
        End If
        Bz2 += "| 材质: " + TextBoxCz.Text + " | 备料: " + TextBoxBlgg.Text + " | 下料数: " + TextBoxXlsl.Text + " | 完工: " + TextBoxWgcc.Text +
            " | 热处理: " + TextBoxRcl.Text + " | 工序: " + TextBoxGx.Text + " | 毛坯重: " + TextBoxMpzl.Text
    End Sub


    '数据处理模块==============================================





    '输入辅助子程序模块///////////////////////////////////////////////////////////////////////

    '显示数据模块
    Private Sub DataDisp()
        Dim str(17) As String
        Dim str1(7) As String
        '提取备注1的信息
        InfoInput(F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Bz1).Value.ToString, str1)
        TextBoxMpdj.Text = str1(1) '毛坯单价
        TextBoxZbgs.Text = str1(3) '准备工时
        TextBoxHjgs.Text = str1(5) '合计工时
        '提取备注2的信息
        InfoInput(F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Bz2).Value.ToString, str)
        If str(0) = "外购" Then
            CheckBoxWg.Checked = True
        Else
            CheckBoxWg.Checked = False
        End If
        If str(1) = "自" Then
            CheckBoxZbl.Checked = True
        Else
            CheckBoxZbl.Checked = False
        End If

        If str(1) = "来料" Then
            CheckBoxLl.Checked = True
        Else
            CheckBoxLl.Checked = False
        End If

        TextBoxCz.Text = str(3) '材质
        TextBoxBlgg.Text = str(5) '备料规格
        TextBoxXlsl.Text = str(7) '下料数量
        TextBoxWgcc.Text = str(9) '完工尺寸
        TextBoxRcl.Text = str(11) '热处理
        TextBoxGx.Text = str(13) '工序
        TextBoxMpzl.Text = str(15) '毛坯重量

        TextBoxLjbh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Bh).Value.ToString
        TextBoxTh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Th).Value.ToString
        TextBoxMc0.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Mc).Value.ToString
        TextBoxTj.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Jtsl).Value.ToString

        TextBoxJhsl.Text = F2BJ.G_Jhsl * TextBoxTj.Text '改计划数量为具体数量

        TextBoxClj.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Clj).Value.ToString
        TextBoxJjgf.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Jjgf).Value.ToString
        TextBoxXlf.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Xlf).Value.ToString
        TextBoxRclf.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Rclf).Value.ToString
        TextBoxBmclf.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Bmclf).Value.ToString
        TextBoxQtjgf.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Qtjgf).Value.ToString
        TextBoxSfgz.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Sfgz).Value.ToString
        TextBoxBz3.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV3_Bz3).Value.ToString

        TextBoxMxsfy.Text = FormLogin.Mxsfy.ToString
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


    '生成价格
    Private Sub Scjg()
        If TextBoxJhsl.Text = vbNullString Then
            Exit Sub
        End If
        Dim Mpzl As Double = 0
        Dim Mpdj As Double = 0
        Dim Zbgs As Double = 0
        Dim Hjgs As Double = 0
        Dim Mxsfy As Double = 20
        Dim Jhsl As Double = 0
        Try
            Mpzl = TextBoxMpzl.Text
        Catch ex As Exception
        End Try
        Try
            Mpdj = TextBoxMpdj.Text
        Catch ex As Exception
        End Try
        Try
            Zbgs = TextBoxZbgs.Text
        Catch ex As Exception
        End Try
        Try
            Hjgs = TextBoxHjgs.Text
        Catch ex As Exception
        End Try
        Try
            Mxsfy = TextBoxMxsfy.Text
        Catch ex As Exception
        End Try
        Try
            Jhsl = TextBoxJhsl.Text
        Catch ex As Exception
        End Try
        TextBoxClj.Text = Math.Round((Mpzl * Mpdj), 4).ToString
        TextBoxJjgf.Text = Math.Round((Mxsfy * (Zbgs / Jhsl + Hjgs) / 60), 4).ToString

    End Sub

    '实时产生价格
    Private Sub TextBoxMpdj_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMpdj.TextChanged
        Scjg()
    End Sub
    Private Sub TextBoxMpzl_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMpzl.TextChanged
        Scjg()
    End Sub
    Private Sub TextBoxZbgs_TextChanged(sender As Object, e As EventArgs) Handles TextBoxZbgs.TextChanged
        Scjg()
    End Sub
    Private Sub TextBoxHjgs_TextChanged(sender As Object, e As EventArgs) Handles TextBoxHjgs.TextChanged
        Scjg()
    End Sub
    Private Sub TextBoxMxsfy_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMxsfy.TextChanged
        Scjg()
    End Sub


    '实时改变含税单价
    Private Sub TextBoxXlf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxXlf.TextChanged
        CalculateHsdj()
    End Sub
    Private Sub TextBoxRclf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxRclf.TextChanged
        CalculateHsdj()
    End Sub
    Private Sub TextBoxBmclf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxBmclf.TextChanged
        CalculateHsdj()
    End Sub
    Private Sub TextBoxQtjgf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxQtjgf.TextChanged
        CalculateHsdj()
    End Sub
    Private Sub TextBoxClj_TextChanged(sender As Object, e As EventArgs) Handles TextBoxClj.TextChanged
        CalculateHsdj()
    End Sub
    Private Sub TextBoxJjgf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJjgf.TextChanged
        CalculateHsdj()
    End Sub

    '含税单价计算
    Private Sub CalculateHsdj()
        Dim Clj As Double = 0
        Dim Xlf As Double = 0
        Dim Jjgf As Double = 0
        Dim Rclf As Double = 0
        Dim Bmclf As Double = 0
        Dim Qtjgf As Double = 0
        Try
            Clj = TextBoxClj.Text
        Catch ex As Exception
        End Try
        Try
            Xlf = TextBoxXlf.Text
        Catch ex As Exception
        End Try
        Try
            Jjgf = TextBoxJjgf.Text
        Catch ex As Exception
        End Try
        Try
            Rclf = TextBoxRclf.Text
        Catch ex As Exception
        End Try
        Try
            Bmclf = TextBoxBmclf.Text
        Catch ex As Exception
        End Try
        Try
            Qtjgf = TextBoxQtjgf.Text
        Catch ex As Exception
        End Try
        TextBoxHsdj.Text = Math.Round((Clj + Xlf + Jjgf + Rclf + Bmclf + Qtjgf), 4).ToString
    End Sub

    '自备料来料自动切换及纠正
    Private Sub CheckBoxLl_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLl.CheckedChanged
        If CheckBoxLl.Checked = True Then
            CheckBoxZbl.Checked = False
        End If
    End Sub
    Private Sub CheckBoxZbl_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxZbl.CheckedChanged
        If CheckBoxZbl.Checked = True Then
            CheckBoxLl.Checked = False
        End If
    End Sub

    '改变台件时改变具体数量
    Private Sub TextBoxTj_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTj.TextChanged
        Try
            TextBoxJhsl.Text = F2BJ.G_Jhsl * TextBoxTj.Text '改计划数量为具体数量
        Catch ex As Exception
        End Try
    End Sub

    '输入辅助子程序模块======================================



End Class