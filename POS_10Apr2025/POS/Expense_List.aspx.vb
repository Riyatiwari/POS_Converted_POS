Imports System.Data
Imports Telerik.Web.UI
Partial Class Expense_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsExpenseMaster As New Controller_clsExpenseMaster
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Expense_List"

    Public Sub bindGrid()
        Try
            Dim dt As DataTable = oclsExpenseMaster.SelectAll()

            If dt.Rows.Count > 0 Then
                rdExpense.DataSource = dt
                rdExpense.DataBind()
            Else
                rdExpense.DataSource = String.Empty
                rdExpense.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Expense_List:bindGrid" + ex.Message)
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
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Expense_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Expense"
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
            LogHelper.Error("Expense_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    Protected Sub rdExpense_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Exp_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Expense_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteExpense(Convert.ToInt32(e.CommandArgument))
                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("Expense_List:rdPanel_ItemCommand" + ex.Message)
        End Try
    End Sub


    Protected Sub deleteExpense(ByVal id As Integer)
        Try
            oclsExpenseMaster.Exp_id = Val(id)
            oclsExpenseMaster.name = ""
            oclsExpenseMaster.Tran_Type = "D"
            oclsExpenseMaster.is_active = 0
            oclsExpenseMaster.Ip_address = ""
            oclsExpenseMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Expense_List:deleteBarcode" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Exp_id") = Nothing
            Response.Redirect("Expense_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Expense_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
    '    Try

    '        rdExpense.Rebind()

    '        If Not IsPostBack Then
    '            Dim strVal As String = ""
    '            If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
    '                For Each column As GridColumn In rdExpense.MasterTableView.Columns
    '                    strVal += column.UniqueName + "#"
    '                Next
    '                oSessionManager.CreateSessionDT(Page_Name.ToString, strVal.ToString)
    '            Else
    '                Dim FilterExpression As String = ""
    '                For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
    '                    If row("FIN").ToString() <> "" And row("VAL").ToString() <> "" Then
    '                        Dim arr As Array = row("VAL").ToString().Split("#")
    '                        If Not [String].IsNullOrEmpty(FilterExpression) Then
    '                            FilterExpression += " AND "
    '                        End If
    '                        If arr.Length - 1 = 0 Then
    '                            Dim a As Integer = 0
    '                            Try
    '                                DateTime.Parse(row("VAL"))
    '                            Catch ex As Exception
    '                                a = 1
    '                            End Try

    '                            If a = 1 Then
    '                                FilterExpression += "([" + row("FIN").ToString() + "] Like '%" + row("VAL").ToString() + "%')"
    '                            Else
    '                                FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL") + "' )"
    '                            End If
    '                        Else
    '                            If row("VAL").ToString.Contains("01/01/1900") Then
    '                                FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL").ToString().Replace("01/01/1900", "").Replace("#", "") + "' )"
    '                            Else
    '                                FilterExpression += "(([" + row("FIN").ToString() + "] >= '" + arr(0) + "') AND ([" + row("FIN").ToString() + "] <= '" + arr(1) + "'))"
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '                If FilterExpression = "" Then
    '                    rdExpense.MasterTableView.FilterExpression = String.Empty
    '                    rdExpense.MasterTableView.Rebind()
    '                Else
    '                    rdExpense.MasterTableView.FilterExpression = FilterExpression
    '                    rdExpense.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
    '                    rdExpense.MasterTableView.Rebind()
    '                End If

    '            End If

    '        End If

    '        GridFilterBind()
    '    Catch ex As Exception
    '        LogHelper.Error("Employee_list:Page_LoadComplete:" + ex.Message)
    '    End Try
    'End Sub
    'Protected Sub GridFilterBind()
    '    Try
    '        For Each item As GridFilteringItem In rdExpense.MasterTableView.GetItems(GridItemType.FilteringItem)

    '            For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
    '                If row("FIN").ToString() <> "" And row("val").ToString() <> "" Then
    '                    Dim val As Integer = 0
    '                    Try
    '                        TryCast(item(row("FIN").ToString()).Controls(0), TextBox).Text = row("VAL").ToString
    '                    Catch ex As Exception
    '                        val = 1
    '                    End Try
    '                    If val = 1 Then
    '                        Try
    '                            If row("VAL") = "" Then
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = "ALL"
    '                            Else
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = row("VAL").ToString
    '                            End If
    '                        Catch ex As Exception
    '                            val = 2
    '                        End Try
    '                    End If
    '                    If val = 2 Then
    '                        Try
    '                            If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing And Not TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker) Is Nothing Then
    '                                If row("val").ToString().Contains("01/01/1900") Then
    '                                    Dim arr As Array = row("val").ToString().Split("#")
    '                                    If arr(0).ToString.Contains("01/01/1900") = False Then
    '                                        TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
    '                                    End If
    '                                    If arr(1).ToString.Contains("01/01/1900") = False Then
    '                                        TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
    '                                    End If
    '                                ElseIf row("val").ToString().Contains("#") Then
    '                                    Dim arr As Array = row("val").ToString().Split("#")
    '                                    TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
    '                                    TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
    '                                End If
    '                            End If
    '                        Catch ex As Exception
    '                            val = 3
    '                        End Try
    '                    End If
    '                    If val = 3 Then
    '                        Try
    '                            If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing Then
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = row("VAL").ToString
    '                            End If
    '                        Catch ex As Exception

    '                        End Try
    '                    End If
    '                End If
    '            Next
    '            'TryCast(item("Branch_Name").Controls(0), TextBox).Text = Session("flt_Branch_Code")
    '        Next
    '    Catch ex As Exception
    '        LogHelper.Error("Employee_list:GridFilterBind:" + ex.Message)
    '    End Try
    'End Sub


End Class
