Imports System.Data.OleDb
'Imports Microsoft.Office.Interop.Excel

Imports System.IO
Imports System.Collections

Public Class KPv1
    Dim MDB_ADR As String

    Dim myExcel As New Excel.Application  '定义进程
    Dim WorkBook As Excel.Workbook '定义工作簿
    Dim Sheet As Excel.Worksheet '定义工作表

    'Dim myExcel As New Microsoft.Office.Interop.Excel.Application  '定义进程
    'Dim WorkBook As Microsoft.Office.Interop.Excel.Workbook '定义工作簿
    'Dim Sheet As Microsoft.Office.Interop.Excel.Worksheet '定义工作表

    '定义复核人，收款人，备注信息字符串
    Dim Fhr, Skr, Bz, Sl As String

    '定义可查询许可
    Dim SearchEnable As Boolean = True

    '定义各个标题所在的列的序号
    Const ExXH = 1 '商品型号
    Const ExMC = 2 '商品名称
    Const ExDW = 3 '商品单位
    Const ExSL = 4 '商品数量
    Const ExDJ = 5 '商品单价
    Const ExJE = 6 '商品金额

    '定义表格的行数和列数
    Dim X_Max, Y_Max As New Integer

    '定义Excel、Txt、Xml文件的地址
    Dim File_Adress, Txt_Adress, Xml_Adress As String

    'Txt文件的开头格式
    Dim TxtStart As String =
        "{商品编码}[分隔符]" + Chr(34) + " " + Chr(34) + vbCrLf +
        "// 每行格式 :" + vbCrLf +
        "// 编码 名称 简码 商品税目 税率 规格型号 计量单位 单价 含税价标志 隐藏标志 中外合作油气田 税收分类编码 是否享受优惠政策 税收分类编码名称 优惠政策类型 零税率标识 编码版本号" + vbCrLf

    'Xml文件的开头格式
    Dim XmlStart As String =
        "<?xml version=""1.0"" encoding=""GBK""?>" + vbCrLf +
        "<Kp>" + vbCrLf +
        "<Version>2.0</Version>" + vbCrLf +
        "<Fpxx>" + vbCrLf +
        "<Zsl>1</Zsl>" + vbCrLf +
        "<Fpsj>" + vbCrLf +
        "<Fp>" + vbCrLf +
        "<Djh>1</Djh>" + vbCrLf

    '程序加载时加载数据并检查快速生成功能是否可用
    Private Sub KPv1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '获取软件当前路径
        Dim Path As String = Environment.CurrentDirectory
        MDB_ADR = "provider=Microsoft.Jet.oledb.4.0;Data source='" + Path + "\KPXX.mdb'"
        '设置Xml、Txt文件保存路径
        TextBoxTxtAdress.Text = Path + "\导出的Txt和Xml文件\清单导入文件.txt"
        TextBoxXmlAdress.Text = Path + "\导出的Txt和Xml文件\发票导入文件.Xml"

        '检测并有条件创建文件夹
        If Not Directory.Exists(Path + "\导出的Txt和Xml文件") Then
            Directory.CreateDirectory(Path + "\导出的Txt和Xml文件")
        End If

        Path += "\导入表格模板.xls"
        TextBoxFileAdress.Text = Path
        '检测并有条件创建表格模板文件
        If Not System.IO.File.Exists(Path) Then
            Dim b() As Byte = My.Resources.导入表格模板
            Dim s As IO.Stream = File.Create(Path)
            s.Write(b, 0, b.Length)
            s.Close()
            MessageBox.Show("系统检测到表格模板文件 " + Path + " 不存在，现已为您自动生成新的表格模板文件。")
        End If

        '读取数据库各个信息并显示
        DataGridViewInit()
        '设置Dataview控件的属性
        DataGridViewKP.ReadOnly = True
        DataGridViewKP.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridViewKP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewKP.AllowUserToAddRows = False
        DataGridViewKP.AllowUserToDeleteRows = False
        DataGridViewKP.AllowUserToOrderColumns = False
        DataGridViewKP.AllowUserToResizeRows = False
        '表格初始化及检查表格格式是否正确
        DataCheck()
        '初始化Txt和Xml路径选择文字框
        SaveAdressInit()
    End Sub


    '表格初始化及检查表格格式是否正确，Adress表格地址，Sheet表格序号，MsMode是否发送消息框
    Public Sub SheetInit(Adress As String, SheetNum As Integer, MsgMode As Boolean)
        '定义表格地址和参数
        myExcel.Visible = False
        WorkBook = myExcel.Workbooks.Open(Adress)
        Sheet = WorkBook.Sheets(SheetNum)   '获得第1个工作表的控制句柄

        '定义表格标题数据数组和获取表格标题数据的字符串
        Dim ExRight(6) As String
        Dim ExError As String = "输入表格格式：[ "
        Dim ExErrorCount As Integer = 0

        '正确的表格标题格式
        ExRight = {"型号", "名称", "单位", "数量", "单价", "金额"}

        '检测每一列的标题是否正确
        For i = 1 To 6
            If Sheet.Cells(1, i).Value.ToString = ExRight(i - 1) Then
                ExError += Sheet.Cells(1, i).Value.ToString + Space(1)
            Else
                ExError += Sheet.Cells(1, i).Value.ToString + Space(1)
                ExErrorCount += 1
            End If
        Next

        '检测结果通知系统
        If ExErrorCount = 0 Then
            RichTextBoxInformation.Text = "税率：" + Sl + vbCrLf
            RichTextBoxInformation.Text += "表格标题格式正确，"
            X_Max = Sheet.UsedRange.Columns.Count
            Y_Max = Sheet.UsedRange.Rows.Count
            RichTextBoxInformation.Text += "总计"
            RichTextBoxInformation.Text += X_Max.ToString
            RichTextBoxInformation.Text += "列"
            RichTextBoxInformation.Text += Y_Max.ToString
            RichTextBoxInformation.Text += "行。" + vbCrLf
            RichTextBoxInformation.Text += "发票其他信息：" + vbCrLf + "复核人：" + Fhr + Space(2)
            RichTextBoxInformation.Text += "收款人：" + Skr + Space(2)
            If Bz = vbNullString Then
                RichTextBoxInformation.Text += "备注：无" + vbCrLf
            Else
                RichTextBoxInformation.Text += "备注：" + Bz + vbCrLf
            End If
            RichTextBoxInformation.Text += "可直接生成Txt和Xml文件！" + vbCrLf
        Else
            If MsgMode = True Then
                MessageBox.Show(ExError + "] 与标准表格标题格式：[ 型号 名称 单位 数量 单价 金额 ] 不符合。", "表格标题格式错误！")
            Else
                RichTextBoxInformation.Text = "默认模板格式错误，快速生成功能不可用！" + vbCrLf + ExError + "] 与标准表格标题格式：[ 型号 名称 单位 数量 单价 金额 ] 不符合。"
            End If

        End If


    End Sub

    'Txt文件生成
    Private Sub ButtonTxtOut_Click(sender As Object, e As EventArgs) Handles ButtonTxtOut.Click
        Dim Er As Integer
        '定义实际保存的文件路径及文件名
        Dim SaveFileAdress As String
        SaveFileAdress = SaveAdressAutoChange(TextBoxTxtAdress.Text, "Txt")
        '表格初始化及检查表格格式是否正确
        Try
            SheetInit(TextBoxFileAdress.Text, 1, True)
        Catch ex As Exception
            MessageBox.Show("读取电子表格信息失败！无法打开位于" + TextBoxFileAdress.Text + "的电子表格！", "错误！")
        End Try
        Dim wr As StreamWriter = New StreamWriter(SaveFileAdress, False, System.Text.Encoding.GetEncoding("GB2312"))
        Try
            wr.Write(TxtStart)
            wr.Write("00" + CUInt(TextBoxCompanyNum.Text).ToString + Chr(32) + TextBoxCompany.Text + Chr(32) + Chr(34) + Chr(34) + vbCrLf)
            For Sheetrow As Integer = 2 To Y_Max
                Er = Sheetrow
                wr.Write("00" + (TextBoxCompanyNum.Text * 1000 + Sheetrow - 1).ToString + Chr(32))    '编码
                wr.Write(Replace(Sheet.Cells(Sheetrow, ExMC).Value, Chr(32), "") + Chr(32))  '名称------商品名称
                wr.Write(Chr(34) + Chr(34) + Chr(32))   '简码
                wr.Write(Chr(34) + Chr(34) + Chr(32))    '商品税目-------商品图号，现已去除
                wr.Write(Sl + Chr(32))   '税率 "0.17"
                wr.Write(Sheet.Cells(Sheetrow, ExXH).Value.ToString + Chr(32))    '规格型号-------商品型号
                wr.Write(Sheet.Cells(Sheetrow, ExDW).Value.ToString + Chr(32))     '计量单位--------商品单位
                wr.Write(Sheet.Cells(Sheetrow, ExDJ).Value.ToString + Chr(32))     '单价-------商品单价
                wr.Write("True" + Chr(32))    '含税价标志
                wr.Write("0000000000" + Chr(32))      '隐藏标志
                wr.Write("False" + Chr(32))      '中外合作油气田
                wr.Write(TextBoxSsflbm.Text.ToString + Chr(32))      '税收分类编码"1090199"
                wr.Write("否" + Chr(32))      '是否享受优惠政策
                wr.Write(TextBoxSsflbmmc.Text.ToString + Chr(32))      '税收分类编码名称
                wr.Write(Chr(34) + Chr(34) + Chr(32))       '优惠政策类型
                wr.Write(Chr(34) + Chr(34) + Chr(32))       '零税率标识
                wr.WriteLine("13.0")        '编码版本号
            Next
            wr.Close()
            RichTextBoxInformation.Text += "Txt文件生成成功！" + vbCrLf
        Catch ex As Exception
            wr.Close()
            MessageBox.Show("Txt文件生成失败，请检查表格第" + Er.ToString + "行内是否有空格。", "Txt文件生成失败！")
        End Try
        Try
            WorkBook.Close(SaveChanges:=False)
            myExcel.Quit()
        Catch ex As Exception
        End Try

    End Sub

    'Xml文件生成
    Private Sub ButtonXmlOut_Click(sender As Object, e As EventArgs) Handles ButtonXmlOut.Click
        Dim Er As Integer
        '定义实际保存的文件路径及文件名
        Dim SaveFileAdress As String
        SaveFileAdress = SaveAdressAutoChange(TextBoxXmlAdress.Text, "Xml")
        '表格初始化及检查表格格式是否正确
        Try
            SheetInit(TextBoxFileAdress.Text, 1, True)
        Catch ex As Exception
            MessageBox.Show("读取电子表格信息失败！无法打开位于" + TextBoxFileAdress.Text + "的电子表格！", "错误！")
        End Try
        Dim KPXXNum As Integer = TextBoxCompanyNum.Text
        Dim wr As StreamWriter = New StreamWriter(SaveFileAdress, False, System.Text.Encoding.GetEncoding("GB2312"))
        Try
            wr.Write(XmlStart)
            wr.WriteLine("<Gfmc>" + DataGridViewKP.Rows(KPXXNum - 1).Cells.Item(1).Value.ToString + "</Gfmc>") 'KPXXDataSet.Tables(0).Rows(KPXXNum - 1).Item(4).ToString
            wr.WriteLine("<Gfsh>" + DataGridViewKP.Rows(KPXXNum - 1).Cells.Item(2).Value.ToString + "</Gfsh>")
            wr.WriteLine("<Gfyhzh>" + DataGridViewKP.Rows(KPXXNum - 1).Cells.Item(3).Value.ToString + "</Gfyhzh>")
            wr.WriteLine("<Gfdzdh>" + DataGridViewKP.Rows(KPXXNum - 1).Cells.Item(4).Value.ToString + "</Gfdzdh>")
            wr.WriteLine("<Bz>" + Bz + "</Bz>") '备注
            wr.WriteLine("<Fhr>" + Fhr + "</Fhr>")
            wr.WriteLine("<Skr>" + Skr + "</Skr>")
            wr.WriteLine("<Spbmbbh>13.0</Spbmbbh>")
            wr.WriteLine("<Hsbz>0</Hsbz>")
            wr.WriteLine("<Spxx>")
            For Sheetrow = 2 To Y_Max
                Er = Sheetrow
                wr.WriteLine("<Sph>")
                wr.WriteLine("<Xh>" + (Sheetrow - 1).ToString + "</Xh>")
                wr.WriteLine("<Spmc>" + Replace(Sheet.Cells(Sheetrow, ExMC).Value.ToString, Chr(32), "") + "</Spmc>") '------商品名称
                wr.WriteLine("<Ggxh>" + Sheet.Cells(Sheetrow, ExXH).Value.ToString + "</Ggxh>") '------商品型号
                wr.WriteLine("<Jldw>" + Sheet.Cells(Sheetrow, ExDW).Value.ToString + "</Jldw>") '------商品单位
                wr.WriteLine("<Spbm>" + TextBoxSpbm.Text.ToString + "</Spbm>") '商品税收编码
                wr.WriteLine("<Qyspbm>" + "00" + (KPXXNum * 1000 + Sheetrow - 1).ToString + "</Qyspbm>") '企业商品编码------商品图号,现已去除
                wr.WriteLine("<Syyhzcbz>0</Syyhzcbz>")
                wr.WriteLine("<Lslbz>0</Lslbz>")
                wr.WriteLine("<Yhzcsm></Yhzcsm>") '优惠政策说明
                wr.WriteLine("<Dj>" + Sheet.Cells(Sheetrow, ExDJ).Value.ToString + "</Dj>") '------商品单价
                wr.WriteLine("<Sl>" + Sheet.Cells(Sheetrow, ExSL).Value.ToString + "</Sl>") '------商品数量
                wr.WriteLine("<Je>" + Sheet.Cells(Sheetrow, ExJE).Value.ToString + "</Je>") '------商品金额
                wr.WriteLine("<Slv>" + Sl + "</Slv>") '税率 "0.17"
                wr.WriteLine("<Kce>0</Kce>")
                wr.WriteLine("</Sph>")
            Next
            wr.WriteLine("</Spxx>")
            wr.WriteLine("</Fp>")
            wr.WriteLine("</Fpsj>")
            wr.WriteLine("</Fpxx>")
            wr.WriteLine("</Kp>")
            wr.Close()
            RichTextBoxInformation.Text += "Xml文件生成成功！" + vbCrLf
        Catch ex As Exception
            wr.Close()
            MessageBox.Show("Xml文件生成失败，请检查表格内第" + Er.ToString + "行内是否有空格、公司编码和各单位开票信息是否正确。", "Xml文件生成失败！")
        End Try
        Try
            WorkBook.Close(SaveChanges:=False)
            myExcel.Quit()
        Catch ex As Exception

        End Try
    End Sub

    '打开Excel文件选择窗口
    Private Sub ButtenFileSearch_Click(sender As Object, e As EventArgs) Handles ButtenFileSearch.Click
        OpenFileDialogExcel.ShowDialog()
    End Sub

    'Excel文件选择窗口文件选择完成后操作
    Private Sub OpenFileDialogExcel_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogExcel.FileOk
        File_Adress = OpenFileDialogExcel.FileName
        TextBoxFileAdress.Text = File_Adress
        '表格初始化及检查表格格式是否正确
        Try
            SheetInit(TextBoxFileAdress.Text, 1, True)
        Catch ex As Exception
            MessageBox.Show("读取电子表格信息失败！无法打开位于" + TextBoxFileAdress.Text + "的电子表格！", "错误！")
        End Try
        Try
            WorkBook.Close()
            myExcel.Quit()
        Catch ex As Exception
        End Try
    End Sub

    '打开Txt文件保存窗口
    Private Sub ButtonTxtSave_Click(sender As Object, e As EventArgs) Handles ButtonTxtSave.Click
        Try
            Dim FileName As String = TextBoxTxtAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Dim FileNameShort As String = IO.Path.GetFileNameWithoutExtension(FileName)
            SaveFileDialogTxt.InitialDirectory = path
            SaveFileDialogTxt.FileName = FileNameShort
            SaveFileDialogTxt.ShowDialog()
        Catch ex As Exception
            SaveFileDialogTxt.ShowDialog()
        End Try

    End Sub

    'Txt文件选择窗口文件选择完成后操作
    Private Sub SaveFileDialogTxt_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogTxt.FileOk
        Try
            Txt_Adress = SaveFileDialogTxt.FileName
            '根据系统时间改变文件名
            TextBoxTxtAdress.Text = SaveFileDialogTxt.FileName
        Catch ex As Exception
            MessageBox.Show("创建文件名时发生错误！", "错误！")
        End Try
    End Sub

    '打开Xml文件保存窗口
    Private Sub ButtonXmlSave_Click(sender As Object, e As EventArgs) Handles ButtonXmlSave.Click
        Try
            Dim FileName As String = TextBoxXmlAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Dim FileNameShort As String = IO.Path.GetFileNameWithoutExtension(FileName)
            SaveFileDialogXml.InitialDirectory = path
            SaveFileDialogXml.FileName = FileNameShort
            SaveFileDialogXml.ShowDialog()
        Catch ex As Exception
            SaveFileDialogXml.ShowDialog()
        End Try
    End Sub

    'Xml文件选择窗口文件选择完成后操作
    Private Sub SaveFileDialogXml_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialogXml.FileOk
        Try
            Xml_Adress = SaveFileDialogXml.FileName
            '根据系统时间改变文件名
            TextBoxXmlAdress.Text = SaveFileDialogXml.FileName
        Catch ex As Exception
            MessageBox.Show("创建文件名时发生错误！", "错误！")
        End Try
    End Sub

    '程序结束后操作
    Private Sub KPv1_Closed(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Try
            WorkBook.Close()
            myExcel.Quit()
        Catch ex As Exception

        End Try
    End Sub

    '打开选定的Excel表格
    Private Sub ButtonOpenExcel_Click(sender As Object, e As EventArgs) Handles ButtonOpenExcel.Click
        Try            
            myExcel.Workbooks.Open(TextBoxFileAdress.Text)
            myExcel.Visible = True
        Catch ex As Exception
            myExcel.Visible = True
        End Try
    End Sub

    '企业ID输入后自动搜索显示企业名称
    Private Sub TextBoxCompanyNum_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCompanyNum.TextChanged
        If SearchEnable = True Then
            Try
                DataGridViewInit()
                TextBoxCompany.Text = DataGridViewKP.Rows(TextBoxCompanyNum.Text - 1).Cells.Item(5).Value
                If TextBoxCompany.Text = vbNullString Then
                    TextBoxCompany.Text = DataGridViewKP.Rows(TextBoxCompanyNum.Text - 1).Cells.Item(1).Value
                End If
            Catch ex As Exception
                TextBoxCompany.Text = "未知的公司编码哦！"
            End Try
        End If
    End Sub

    '打开设置窗口
    Private Sub LabelProSet_Click(sender As Object, e As EventArgs) Handles LabelProSet.Click
        Try
            KPSet.Show()
        Catch ex As Exception
            MessageBox.Show("打开设置窗口时发生错误！", "错误！")
        End Try

    End Sub

    '点击表格选择公司名称及编码
    Private Sub DataGridViewKP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewKP.CellClick
        SearchEnable = False        '搜索功能关闭
        Try
            TextBoxCompanyNum.Text = DataGridViewKP.CurrentRow.Cells.Item(0).Value
            TextBoxCompany.Text = DataGridViewKP.CurrentRow.Cells.Item(5).Value
            If TextBoxCompany.Text = vbNullString Then
                TextBoxCompanyNum.Text = DataGridViewKP.CurrentRow.Cells.Item(0).Value
                TextBoxCompany.Text = DataGridViewKP.CurrentRow.Cells.Item(1).Value
            End If
        Catch ex As System.Exception
            TextBoxCompany.Text = "公司名称别忘了！"
        End Try
        SearchEnable = True        '搜索功能回复
    End Sub

    '读取数据库各个信息并显示
    Public Sub DataGridViewInit()

        Try
            '读取数据库各单位开票信息
            Dim cn As New OleDbConnection
            cn.ConnectionString = "provider=Microsoft.Jet.oledb.4.0;Data source='" + Environment.CurrentDirectory + "\KPXX.mdb'"
            cn.Open()
            Dim sql As String = "select * from 各单位开票信息 order by ID asc"
            Dim da As New OleDbDataAdapter(sql, cn)
            Dim ds As New System.Data.DataSet
            da.Fill(ds, "各单位开票信息")
            DataGridViewKP.DataSource = ds.Tables(0)
            cn.Close()
            '读取复核人、收款人、备注信息
            cn.Open()
            Dim sql2 As String = "select * from 其他开票信息 order by ID asc"
            Dim da2 As New OleDbDataAdapter(sql2, cn)
            Dim ds2 As New System.Data.DataSet
            da2.Fill(ds2, "其他开票信息")
            Fhr = ds2.Tables(0).Rows(0).Item(1).ToString     '将复核人值赋予Fhr
            Skr = ds2.Tables(0).Rows(0).Item(2).ToString    '将收款人值赋予Skr
            Bz = ds2.Tables(0).Rows(0).Item(3).ToString     '将备注的值赋予Bz
            TextBoxSsflbm.Text = ds2.Tables(0).Rows(0).Item(4).ToString     '显示税收分类编码
            TextBoxSpbm.Text = ds2.Tables(0).Rows(0).Item(5).ToString     '显示商品编码
            TextBoxSsflbmmc.Text = ds2.Tables(0).Rows(0).Item(6).ToString '显示税收分类编码名称
            Sl = ds2.Tables(0).Rows(0).Item(8).ToString '税率

            cn.Close()
        Catch ex As Exception
            MessageBox.Show("读取开票信息失败！请检查位于" + Environment.CurrentDirectory + "\KPXX.mdb的数据库是否正确！", "错误！")
        End Try

    End Sub

    '表格初始化及检查表格格式是否正确并关闭表格对象
    Public Sub DataCheck()
        '表格初始化及检查表格格式是否正确
        Try
            SheetInit(TextBoxFileAdress.Text, 1, False)
        Catch ex As Exception
            MessageBox.Show("读取电子表格信息失败！无法打开位于" + TextBoxFileAdress.Text + "的Excel表格模板！此表格或处于保护状态，请问是否手动打开并保存此Excel表格？", "错误！", MessageBoxButtons.OKCancel)
            If DialogResult.OK Then
                Try
                    Process.Start(Environment.CurrentDirectory)
                    'myExcel.Workbooks.Open(TextBoxFileAdress.Text)  '此举可能会影响程序反应速度，已舍弃
                    'myExcel.Visible = True
                Catch ex1 As Exception

                End Try
            End If
        End Try
        Try
            WorkBook.Close(SaveChanges:=False)
            myExcel.Quit()
        Catch ex As Exception

        End Try
    End Sub

    '双击文字框打开Xml所在文件夹
    Private Sub TextBoxXmlAdress_Click(sender As Object, e As EventArgs) Handles TextBoxXmlAdress.DoubleClick
        Try
            Dim FileName As String = TextBoxXmlAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Process.Start(path)
        Catch ex As Exception
            MessageBox.Show("打开Xml文件所在文件夹失败，请检查Xml文件保存地址是否正确！", "错误！")
        End Try
    End Sub

    '双击文字框打开Txt所在文件夹
    Private Sub TextBoxTxtAdress_Click(sender As Object, e As EventArgs) Handles TextBoxTxtAdress.DoubleClick
        Try
            Dim FileName As String = TextBoxTxtAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Process.Start(path)
        Catch ex As Exception
            MessageBox.Show("打开Txt文件所在文件夹失败，请检查Txt文件保存地址是否正确！", "错误！")
        End Try
    End Sub

    '双击文字框打开Excel所在文件夹
    Private Sub TextBoxFileAdress_Click(sender As Object, e As EventArgs) Handles TextBoxFileAdress.DoubleClick
        Try
            Dim FileName As String = TextBoxFileAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Process.Start(path)
        Catch ex As Exception
            MessageBox.Show("打开Excel文件所在文件夹失败，请检查Excel文件保存地址是否正确！", "错误！")
        End Try
    End Sub

    '初始化Txt和Xml路径选择文字框——————————————————————————————————————————————————————————————
    Public Sub SaveAdressInit()
        Dim Path As String = Environment.CurrentDirectory
        TextBoxTxtAdress.Text = Path + "\导出的Txt和Xml文件\清单导入文件.txt"
        TextBoxXmlAdress.Text = Path + "\导出的Txt和Xml文件\发票导入文件.Xml"
    End Sub

    '自动在保存的文件名后加上日期
    Public Function SaveAdressAutoChange(FileName As String, Type As String) As String
        Dim path As String
        Dim FileNameShort As String
        If Type = "Txt" Then
            path = IO.Path.GetDirectoryName(FileName)
            FileNameShort = IO.Path.GetFileNameWithoutExtension(FileName)
            Return path + "\" + FileNameShort + " - " + DateTime.Now.Year.ToString + "年" + DateTime.Now.Month.ToString + "月" + DateTime.Now.Day.ToString + "日 - " +
                DateTime.Now.Hour.ToString + "时" + DateTime.Now.Minute.ToString + "分" + DateTime.Now.Second.ToString + "秒" + ".txt"
        ElseIf Type = "Xml" Then
            path = IO.Path.GetDirectoryName(FileName)
            FileNameShort = IO.Path.GetFileNameWithoutExtension(FileName)
            Return path + "\" + FileNameShort + " - " + DateTime.Now.Year.ToString + "年" + DateTime.Now.Month.ToString + "月" + DateTime.Now.Day.ToString + "日 - " +
                DateTime.Now.Hour.ToString + "时" + DateTime.Now.Minute.ToString + "分" + DateTime.Now.Second.ToString + "秒" + ".xml"
        Else
            Return FileName
        End If
    End Function

    '刷新数据
    Private Sub LabelRefresh_Click(sender As Object, e As EventArgs) Handles LabelRefresh.Click
        DataGridViewInit()
        DataCheck()
    End Sub

    '打开Txt文件的文件夹
    Private Sub ButtonOpenTxt_Click(sender As Object, e As EventArgs) Handles ButtonOpenTxt.Click
        Try
            Dim FileName As String = TextBoxTxtAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Process.Start(path)
        Catch ex As Exception
            MessageBox.Show("打开Txt文件所在文件夹失败，请检查Txt文件保存地址是否正确！", "错误！")
        End Try
    End Sub

    '打开Xml文件的文件夹
    Private Sub ButtonOpenXml_Click(sender As Object, e As EventArgs) Handles ButtonOpenXml.Click
        Try
            Dim FileName As String = TextBoxXmlAdress.Text
            Dim path As String = IO.Path.GetDirectoryName(FileName)
            Process.Start(path)
        Catch ex As Exception
            MessageBox.Show("打开Xml文件所在文件夹失败，请检查Xml文件保存地址是否正确！", "错误！")
        End Try
    End Sub

    '新建模板
    Private Sub ButtonXjmb_Click(sender As Object, e As EventArgs) Handles ButtonXjmb.Click
        Try
            Dim Path As String = Environment.CurrentDirectory
            Dim b() As Byte = My.Resources.导入表格模板
            Dim s As IO.Stream = File.Create(Path + "\导入表格模板.xls")
            s.Write(b, 0, b.Length)
            s.Close()
            MessageBox.Show("新建模板成功！")
            Try
                myExcel.Workbooks.Open(TextBoxFileAdress.Text)
                myExcel.Visible = True
            Catch ex As Exception
                myExcel.Visible = True
            End Try
        Catch ex As Exception
            MessageBox.Show("新建模板失败！请检查表格模板是否在使用！")
        End Try

    End Sub
End Class



