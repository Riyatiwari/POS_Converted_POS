Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Telerik.Web.UI
Partial Class Cash_Declaration_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsdeclaration As New Controller_clsExpense()
    Public Sub bindGrid()
        Try
            oclsdeclaration.cmp_id = Val(Session("cmp_id"))
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            oclsdeclaration.start_date = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsdeclaration.end_date = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsdeclaration.Duration = IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today")

            Dim dt As DataTable = oclsdeclaration.Select_Cash_Declaration()

            If dt.Rows.Count > 0 Then
                rdexpense.DataSource = dt
                rdexpense.DataBind()
            Else
                rdexpense.DataSource = String.Empty
                rdexpense.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Cash_Declaration_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsdeclaration.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsdeclaration.Select_Cash_Declaration()

            If dt.Rows.Count > 0 Then
                rdexpense.DataSource = dt
            Else
                rdexpense.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Cash_Declaration_List:rebindGrid" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("edit") = 0
                    ViewState("add") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divBarcode.Visible = True
                Else
                    divBarcode.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now

            End If

        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Cash Declaration"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdexpense_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdexpense.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdexpense.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Cash_Declaration_List:rdPrinter_ItemCreated:" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdexpense_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdexpense.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdexpense.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            End If
    '            If Val(ViewState("edit")) = 1 Then
    '                CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '            End If
    '            If Val(ViewState("delete")) = 1 Then
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Cash_Declaration_List:rdexpense_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdexpense_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdexpense.ItemCommand
    '    Try
    '        If e.CommandName = "Edit" Then
    '            Session("Barcode_id") = Convert.ToInt32(e.CommandArgument)
    '            Response.Redirect("Bank_Expense.aspx", False)
    '        ElseIf e.CommandName = "Delete" Then
    '            deleteBarcode(Convert.ToInt32(e.CommandArgument))
    '            bindGrid()
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Cash_Declaration_List:rdexpense_ItemCommand" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub deleteBarcode(ByVal id As Integer)
        Try
            oclsdeclaration.csh_id = Val(id)
            oclsdeclaration.cmp_id = Val(Session("cmp_id"))
            oclsdeclaration.Card_Amount = 0.0
            oclsdeclaration.Cash_Amount = 0.0
            'oclsdeclaration.product_id = 0
            oclsdeclaration.Amount_Type = 0
            oclsdeclaration.Machine_id = 0
            oclsdeclaration.ip_address = ""
            oclsdeclaration.login_id = Val(Session("login_id"))
            oclsdeclaration.for_date = System.DateTime.Now

            oclsdeclaration.Delete_cash_decl()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Cash_Declaration_List:deleteBarcode" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Barcode_id") = Nothing
            Response.Redirect("Cash_Declaration.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdexpense_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdexpense.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Cash_Declaration_List:rdexpense_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Private Sub lnkImport_Click(sender As Object, e As EventArgs) Handles lnkImport.Click
        If divImport.Visible = False Then
            divImport.Visible = True
        Else
            divImport.Visible = False
        End If
    End Sub

    Private Sub lnkImportFile_Click(sender As Object, e As EventArgs) Handles lnkImportFile.Click
        Dim oClsDal As ClsDataccess = New ClsDataccess()
        If FileUpload1.HasFile Then
            Dim FileName As String = Now.ToString.Replace("/", "").Replace("\", "").Replace(" ", "").Replace(":", "") + Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

            Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            FileUpload1.SaveAs(FilePath)
            Dim dt As DataTable = Import_To_Grid(FilePath, Extension, "Yes")

            For index = 0 To dt.Rows.Count - 1

                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@venue_name", dt.Rows(index)(0).ToString(), SqlDbType.NVarChar)
                oColSqlparram.Add("@venue_id", 0, SqlDbType.Int)
                oColSqlparram.Add("@for_date", dt.Rows(index)(1).ToString(), SqlDbType.DateTime)
                oColSqlparram.Add("@Account", dt.Rows(index)(2).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@table", dt.Rows(index)(3).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@actualcash", dt.Rows(index)(4).ToString(), SqlDbType.Decimal)
                oColSqlparram.Add("@actualcard", dt.Rows(index)(5).ToString(), SqlDbType.Decimal)
                Dim dtImport As DataTable = oClsDal.GetdataTableSp("P_declaration_import", oColSqlparram)

            Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record imported successfully.');", True)
        End If
    End Sub
    Private Function Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String) As DataTable
        Dim conStr As String = ""
        Dim dt As DataTable = New DataTable()
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
        'Dim dcID = New Data.DataColumn("Status", GetType(String))
        'dt.Columns.Add(dcID)
        'For Each row As DataRow In dt.Rows
        '    If row(0).ToString = "" Then
        '        row.Delete()
        '    ElseIf row(0) Is Nothing Then
        '        row.Delete()

        '    Else
        '        row(6) = "New"
        '    End If
        'Next
        'dt.AcceptChanges()

        Return dt

    End Function
    Protected Sub rdexpense_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Barcode_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Bank_Expense.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteBarcode(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:rdexpense_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True

            Else
                divcustom.Visible = False

            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:radDuration_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub
    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try
            txtToDate.SelectedDate = txtForDate.SelectedDate
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:txtForDate_SelectedDateChanged" + ex.Message)
        End Try
    End Sub
    Protected Sub btnView_Click(sender As Object, e As EventArgs)
        Try

            bindGrid()

        Catch ex As Exception
            LogHelper.Error("Cash_Declaration_List:btnView_Click" + ex.Message)
        End Try
    End Sub
End Class
