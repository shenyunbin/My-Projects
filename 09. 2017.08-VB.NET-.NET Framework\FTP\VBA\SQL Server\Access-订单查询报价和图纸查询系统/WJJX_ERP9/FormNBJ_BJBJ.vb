Imports System.Data.SqlClient

Public Class FormNBJ_BJBJ
    Public XZMode As Boolean = True  'True=新增部件，false=修改部件
    Public BJXh As String

    Dim Leftjoin = " left join 部件信息 on 零件信息.订单号=部件信息.订单号 and 零件信息.型号=部件信息.型号 " +
                         "left join 订单信息 on  零件信息.订单号=订单信息.订单号" +
                         " left join 仓库信息 on  零件信息.图号=仓库信息.图号 and 订单信息.客户=仓库信息.客户 "

    Dim Leftjoin2 = "left join 订单信息 on  部件信息.订单号=订单信息.订单号"



    '全局操作模块//////////////////////////////////////////////////////////////////////////////////////

    '程序载入时操作
    Private Sub FormNBJ_BJBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxMxsfy.Text = FormLogin.Mxsfy
        If XZMode Then '新增部件
            '更改按钮标识
            Me.Text = "订单" + F2BJ.G_Ddh.ToString + " - 新增部件"
            ButtonXzBj.Text = "新增部件"
            If F2BJ.DataGridView1.Rows.Count = 0 Then
                Exit Sub
            End If
            '显示数据模块
            DataDisp()
            TextBoxBjbh.Text = F2BJ.DataGridView1.Rows.Count + 1
        Else '修改部件
            BJXh = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Xh).Value.ToString
            '更改按钮标识
            Me.Text = "订单" + F2BJ.G_Ddh.ToString + " - 修改部件" + BJXh
            ButtonXzBj.Text = "修改部件"
            '显示数据模块
            DataDisp()

        End If
    End Sub

    '新增/修改部件按钮事件
    Private Sub ButtonXzBj_Click(sender As Object, e As EventArgs) Handles ButtonXzBj.Click
        Dim i As Integer
        Try
            '获取之前活动的单元格
            i = F2BJ.DataGridView1.CurrentCellAddress.Y
        Catch ex As Exception
        End Try

        If XZMode Then '新增部件----------------------------

            If TextBoxBjbh.Text = "0" Or TextBoxBjbh.Text = "" Then
                MessageBox.Show("新增部件失败！新增部件编号不能为空或者0。", "输入错误！")
                Exit Sub
            End If
            CalculateHsdj()
            '检查图号名称是否有重复
            If Not ThMcTest(F2BJ.G_Kh, TextBoxXh.Text, TextBoxMc.Text) Then
                Exit Sub
            End If
            '新增部件信息
            If InsertBjxx() Then
                '新增零件信息
                InsertLjxx()
            End If

        Else '修改部件--------------------------------------

            If TextBoxBjbh.Text = "0" Or TextBoxBjbh.Text = "" Then
                MessageBox.Show("修改部件失败！部件编号不能为空或者0。", "输入错误！")
                Exit Sub
            End If
            CalculateHsdj()
            '检查图号名称是否有重复
            If Not ThMcTest(F2BJ.G_Kh, TextBoxXh.Text, TextBoxMc.Text) Then
                Exit Sub
            End If
            '修改部件信息
            If UpdateBjxx() = False Then
                Exit Sub
            End If
            Try
                '修改零件信息
                UpdateLjxx()
            Catch
                '新增零件信息
                InsertLjxx()
            End Try
        End If

        '载入部件信息
        F2BJ.DGV_Load(F2BJ.DGV_Mode, F2BJ.G_Ddh, vbNullString, F2BJ.DataGridView1)
        Try
            '设置之前的活动单元格
            F2BJ.DataGridView1.CurrentCell = F2BJ.DataGridView1(0, i)
        Catch
        End Try
        '载入部件信息
        F2BJ.DGV_Load(F2BJ.DGV_Mode + 1, F2BJ.G_Ddh, F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Xh).Value.ToString, F2BJ.DataGridView2)
        Me.Close()
        MessageBox.Show("新增/修改部件信息成功!")
    End Sub

    '型号搜索复制按钮事件
    Private Sub ButtonThss_Click(sender As Object, e As EventArgs) Handles ButtonThss.Click
        FormNBJ_THSS.SSMode = 12
        FormNBJ_THSS.SSnr = TextBoxXh.Text
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

        TextBoxBjbh.Text = vbNullString
        TextBoxXh.Text = vbNullString
        TextBoxMc.Text = vbNullString
        TextBoxJhsl.Text = vbNullString

        TextBoxJhsl0.Text = TextBoxJhsl.Text

        TextBoxJhrq.Text = vbNullString
        TextBoxBz0.Text = vbNullString

        TextBoxClj.Text = vbNullString
        TextBoxJjgf.Text = vbNullString
        TextBoxXlf.Text = vbNullString
        TextBoxRclf.Text = vbNullString
        TextBoxBmclf.Text = vbNullString
        TextBoxQtjgf.Text = vbNullString
        TextBoxSfgz.Text = vbNullString
        TextBoxBz3.Text = vbNullString
    End Sub

    '清空除型号日期外的数据
    Private Sub LabelQKSJ2_Click(sender As Object, e As EventArgs) Handles LabelQKSJ2.Click
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

        'TextBoxBjbh.Text = vbNullString
        TextBoxMc.Text = vbNullString
        TextBoxJhsl.Text = vbNullString

        TextBoxJhsl0.Text = TextBoxJhsl.Text

        'TextBoxJhrq.Text = vbNullString
        TextBoxBz0.Text = vbNullString

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

    '新增部件信息
    Private Function InsertBjxx()
        Dim Bh As String = TextBoxBjbh.Text
        Dim Xh As String = TextBoxXh.Text
        Dim Mc As String = TextBoxMc.Text
        Dim Jhsl As Integer = 0
        Dim Jhrq As String = TextBoxJhrq.Text
        Dim Bz As String = TextBoxBz0.Text
        Dim Zt As String = F2BJ.G_Zt
        Dim Ddh As String = F2BJ.G_Ddh
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        Try
            Jhsl = TextBoxJhsl.Text
        Catch ex As Exception
        End Try
        If Ddh = "" Or Xh = "" Then
            MessageBox.Show("订单号或型号未填写,请再次填写!", "填写错误！")
            Return False
            Exit Function
        End If
        For i = 0 To F2BJ.DataGridView1.Rows.Count - 1
            If Xh = F2BJ.DataGridView1.Rows(i).Cells(F2BJ.DGV2_Xh).Value.ToString Then
                MessageBox.Show("该型号部件已经存在在此订单中，无需再次新增！", "新增部件失败！")
                Return False
                Exit Function
            End If
        Next
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "insert into 部件信息 (编号,型号,名称,计划数量,计划日期,备注,状态,订单号,识别号) values('" + Bh + "','" + Xh + "','" + Mc + "','" +
            Jhsl.ToString + "','" + Jhrq + "','" + Bz + "','" + Zt + "','" + Ddh + "','" + Sbh + "')"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        Return True
    End Function


    '新增零件信息
    Private Sub InsertLjxx()
        '设置部件内第0个零件信息
        '根据部件产生的信息
        Dim Bh As String = "0"
        Dim Th As String = TextBoxXh.Text
        Dim Mc As String = TextBoxMc.Text
        Dim Jtsl As String = "1"
        '其他信息
        Dim Bz1 As String = vbNullString
        Dim Bz2 As String = vbNullString
        Scbz(Bz1, Bz2) '生成备注值
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
        Dim Xh As String = TextBoxXh.Text
        Dim Sbh As String = Format(Now(), "yyyy-MM-dd H:mm:ss ffff")
        If Ddh = "" Or Xh = "" Or Th = "" Then
            MessageBox.Show("图号、订单号或型号未填写,请再次填写!", "填写错误！")
            Exit Sub
        End If
        Dim cn As New SqlConnection(F2BJ.SQL_Connection)
        cn.Open() '插入前，必须连接  
        Dim sql As String = "if not exists(select * from 零件信息 where 订单号='" + Ddh + "' and 型号='" + Xh + "' and 图号='" + Th + "')  " +
            "insert into 零件信息 (编号,图号,名称,台件,备注1,备注2,材料价,下料费,精加工费,热处理费,表面处理费,其他加工费,实付工资,含税单价,备注3,状态,订单号,型号,识别号) " +
            "values(" + Bh + ",'" + Th + "','" + Mc + "','" + Jtsl + "','" + Bz1 + "','" + Bz2 + "','" + Clj + "','" + Xlf + "','" + Jjgf + "','" + Rclf + "','" +
        Bmclf + "','" + Qtjgf + "','" + Sfgz + "','" + Hsdj + "','" + Bz3 + "','" + Zt + "','" + Ddh + "','" + Xh + "','" + Sbh + "')"
        Dim cm As New SqlCommand(sql, cn)
        cm.ExecuteNonQuery()
        cn.Close()
        '修改每小时费用的值
        Try
            FormLogin.Mxsfy = TextBoxMxsfy.Text '修改每小时费用值
        Catch ex As Exception
        End Try
    End Sub


    '修改部件信息
    Private Function UpdateBjxx()
        Try
            Dim XhOld As String = BJXh
            Dim Bh As String = TextBoxBjbh.Text
            Dim Ddh As String = F2BJ.G_Ddh
            Dim Xh As String = TextBoxXh.Text
            Dim Mc As String = TextBoxMc.Text
            Dim Jhsl As String = TextBoxJhsl.Text
            Dim Jhrq As String = TextBoxJhrq.Text
            Dim Bz As String = TextBoxBz0.Text
            If Not XhOld = Xh Then
                MessageBox.Show("所选择要修改的型号与上方填写的型号不一致!", "填写错误！")
                Return False
                Exit Try
            End If
            If Ddh = "" Or Xh = "" Then
                MessageBox.Show("订单号或者型号未填写,请再次填写!", "填写错误！")
                Return False
                Exit Try
            End If
            Dim cn As New SqlConnection(F2BJ.SQL_Connection)
            cn.Open() '插入前，必须连接  
            Dim sql As String = "UPDATE 部件信息 SET 编号=" + Bh + ", 型号='" + Xh + "', 名称 = '" + Mc + "', 计划数量 = '" + Jhsl + "', 计划日期 = '" + Jhrq +
                "', 备注 = '" + Bz + "' WHERE 型号 = '" + XhOld + "' AND 订单号 = '" + Ddh + "'"
            Dim cm1 As New SqlCommand(sql, cn)
            cm1.ExecuteNonQuery()
            sql = "UPDATE 零件信息 SET 型号='" + Xh + "' WHERE 型号 = '" + XhOld + "' AND 订单号 = '" + Ddh + "'"
            Dim cm2 As New SqlCommand(sql, cn)
            cm2.ExecuteNonQuery()
            cn.Close()
            BJXh = TextBoxXh.Text
            Return True
        Catch ex As Exception
            MessageBox.Show("错误！未选择所要修改信息的部件。", "操作错误！")
            Return False
        End Try
    End Function


    '修改零件信息
    Private Function UpdateLjxx()
        Dim ThOld As String = BJXh

        Dim Bh As String = "0"
        Dim Th As String = TextBoxXh.Text
        Dim Mc As String = TextBoxMc.Text
        Dim Jtsl As String = "1"

        Dim Bz1 As String = vbNullString
        Dim Bz2 As String = vbNullString
        Scbz(Bz1, Bz2) '生成备注值
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
        Dim Xh As String = TextBoxXh.Text

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


    '检查以前订单中已经含有相同客户、相同图号的部件或零件 True=是，False=否
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
        InfoInput(F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Bz1).Value.ToString, str1)
        TextBoxMpdj.Text = str1(1) '毛坯单价
        TextBoxZbgs.Text = str1(3) '准备工时
        TextBoxHjgs.Text = str1(5) '合计工时
        '提取备注2的信息
        InfoInput(F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Bz2).Value.ToString, str)
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

        TextBoxBjbh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Bh).Value.ToString
        TextBoxXh.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Xh).Value.ToString
        TextBoxMc.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Mc).Value.ToString
        TextBoxJhsl.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Jhsl).Value.ToString
        TextBoxJhsl0.Text = TextBoxJhsl.Text
        TextBoxJhrq.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Jhrq).Value.ToString
        TextBoxBz0.Text = F2BJ.DataGridView1.CurrentRow.Cells(F2BJ.DGV2_Bz0).Value.ToString

        TextBoxClj.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Clj).Value.ToString
        TextBoxJjgf.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Jjgf).Value.ToString
        TextBoxXlf.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Xlf).Value.ToString
        TextBoxRclf.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Rclf).Value.ToString
        TextBoxBmclf.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Bmclf).Value.ToString
        TextBoxQtjgf.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Qtjgf).Value.ToString
        TextBoxSfgz.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Sfgz).Value.ToString
        TextBoxBz3.Text = F2BJ.DataGridView2.Rows(0).Cells(F2BJ.DGV3_Bz3).Value.ToString

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

    '实时同步输入计划数量
    Private Sub TextBoxJhsl0_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJhsl0.TextChanged
        TextBoxJhsl.Text = TextBoxJhsl0.Text
    End Sub

    '输入辅助子程序模块======================================



End Class