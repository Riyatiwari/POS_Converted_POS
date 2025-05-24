Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Imports System.Data.OleDb
Partial Class Sales_Import_List
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsRole As Controller_clsRole
    Dim oclExpense As New Controller_clsExpenseMaster()

    Dim dt As DataTable = New DataTable()

    Dim totalmon_amount As Decimal
    Dim totaltue_amount As Decimal
    Dim totalwed_amount As Decimal
    Dim totalthu_amount As Decimal
    Dim totalfri_amount As Decimal
    Dim totalsat_amount As Decimal
    Dim totalsun_amount As Decimal


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            RoleCheck()

        Catch ex As Exception
            LogHelper.Error("bank_report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            If Not IsPostBack = True Then
                btnSave.Visible = False
            End If
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Sales Import"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If


        Catch ex As Exception
            LogHelper.Error("bank_report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If FileUpload1.HasFile Then
            Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

            Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            FileUpload1.SaveAs(FilePath)
            Import_To_Grid(FilePath, Extension, "Yes")
        End If
    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07
                conStr = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                Exit Select
        End Select
        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()


        cmdExcel.Connection = connExcel

        'Get the name of First Sheet
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()
        Dim dcID = New Data.DataColumn("Status", GetType(String))
        dt.Columns.Add(dcID)
        For Each row As DataRow In dt.Rows
            If row(0).ToString = "" Then
                row.Delete()
            ElseIf row(0) Is Nothing Then
                row.Delete()

            Else
                row(6) = "New"
            End If
        Next
        dt.AcceptChanges()
        'Bind Data to GridView
        GridView1.Caption = Path.GetFileName(FilePath)
        GridView1.DataSource = dt
        GridView1.DataBind()
        Session("dt") = dt
        btnSave.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            dt = Session("dt")
            For index = 0 To dt.Rows.Count - 1

                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@venue_name", dt.Rows(index)(0).ToString(), SqlDbType.NVarChar)
                oColSqlparram.Add("@venue_id", 0, SqlDbType.Int)
                oColSqlparram.Add("@for_date", dt.Rows(index)(1).ToString(), SqlDbType.DateTime)
                oColSqlparram.Add("@total", dt.Rows(index)(2).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@cash", dt.Rows(index)(3).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@card", dt.Rows(index)(4).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@balance", dt.Rows(index)(5).ToString(), SqlDbType.Decimal)
                Dim dtImport As DataTable = oClsDal.GetdataTableSp("P_sales_import", oColSqlparram)


                If dtImport.Rows.Count > 0 Then
                    If dtImport.Rows(0)(0).ToString() = "0" Then
                        dt.Rows(index)(6) = "Error: Location Not found"
                    Else
                        dt.Rows(index)(6) = "Sucess"
                    End If
                End If

            Next

            Session("dt") = dt
            GridView1.DataSource = dt
            GridView1.DataBind()

        Catch ex As Exception
            LogHelper.Error("import sales:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            Session("dt") = ""
            btnSave.Visible = False
            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class
