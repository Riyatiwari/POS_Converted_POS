Imports System.Data
Imports Telerik.Web.UI
Partial Class Bank_Cash_Expense_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsStock As New Controller_clsExpense_Detail
    Public Sub bindGrid()
        Try
            Dim dt As DataTable = oclsStock.SelectExpenseDetail()

            If dt.Rows.Count > 0 Then
                rdexpense.DataSource = dt
                rdexpense.DataBind()
            Else
                rdexpense.DataSource = String.Empty
                rdexpense.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Bank_Cash_Expense_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            Dim dt As DataTable = oclsStock.SelectExpenseDetail()

            If dt.Rows.Count > 0 Then
                rdexpense.DataSource = dt
            Else
                rdexpense.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Bank_Cash_Expense_List:rebindGrid" + ex.Message)
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
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Expense List"
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
            LogHelper.Error("Bank_Cash_Expense_List:RoleCheck" + ex.Message)
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
    '        LogHelper.Error("Bank_Cash_Expense_List:rdPrinter_ItemCreated:" + ex.Message)
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
    '        LogHelper.Error("Bank_Cash_Expense_List:rdexpense_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdexpense_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    Try
    '        If e.CommandName = "Edit" Then
    '            Session("Expense_Detail_id") = Convert.ToInt32(e.CommandArgument)
    '            Response.Redirect("Expense_Detail.aspx", False)
    '        ElseIf e.CommandName = "Delete" Then
    '            deletestock(Convert.ToInt32(e.CommandArgument))
    '            bindGrid()
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Bank_Cash_Expense_List:rdexpense_ItemCommand" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub deletestock(ByVal id As Integer)
        Try
            oclsStock.Expense_Detail_id = Val(id)
            oclsStock.From_Date = DateTime.Now
            oclsStock.Location_id = 0
            oclsStock.Expense_id = 0
            oclsStock.ip_address = Request.UserHostAddress
            oclsStock.PerDay_amount = 0
            oclsStock.PerDayTotal_amount = 0
            oclsStock.Delete()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Bank_Cash_Expense_List:deletestock" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Expense_Detail_id") = Nothing
            Response.Redirect("Bank_Cash_Expense.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdexpense_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdexpense.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Bank_Cash_Expense_List:rdexpense_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub rdexpense_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Expense_Detail_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Expense_Detail.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deletestock(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense_List:rdexpense_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class
