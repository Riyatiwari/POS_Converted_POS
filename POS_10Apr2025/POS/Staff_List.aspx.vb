Imports System.Data
Imports Telerik.Web.UI


Partial Class Staff_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsStaff As New Controller_clsStaff
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Staff_List"
    Private tillCodeHiddenField As HiddenField
    Dim oclsKeyMap As New Controller_clsKey_Map()

    Public Sub bindGrid()
        Try
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsStaff.SelectAll()

            If dt.Rows.Count > 0 Then
                rdStaff.DataSource = dt
                rdStaff.DataBind()
            Else
                rdStaff.DataSource = String.Empty
                rdStaff.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Staff_List:bindGrid:" + ex.Message)
        End Try
      
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' tillCodeHiddenField = DirectCast(FindControl("till_code"), HiddenField)
            ' Dim tillCode As Integer = tillCodeHiddenField.Value
            ' Session("tillcode") = tillCode
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
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divStaff.Visible = True
                Else
                    divStaff.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If
            'bindGrid()
        Catch ex As Exception
            LogHelper.Error("Staff_List:bindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "User"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If
            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Staff_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdStaff_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdStaff.ItemCommand
    '    Try
    '        If e.CommandName = "Edit" Then
    '            Session("staff_id") = Val(e.CommandArgument)
    '            Response.Redirect("Staff_Master.aspx", False)
    '        ElseIf e.CommandName = "Delete" Then
    '            deleteStaff(Val(e.CommandArgument))
    '            bindGrid()
    '        ElseIf e.CommandName = "Clear" Then
    '            For Each column As GridColumn In rdStaff.MasterTableView.Columns
    '                'column.CurrentFilterFunction = GridKnownFunction.NoFilter
    '                column.CurrentFilterValue = String.Empty
    '            Next
    '            rdStaff.MasterTableView.FilterExpression = String.Empty
    '            rdStaff.MasterTableView.Rebind()
    '            oSessionManager.ResetSessionDT(Page_Name.ToString)
    '        ElseIf e.CommandName = "Search" Then

    '            Dim filteringItem As GridFilteringItem = DirectCast(rdStaff.MasterTableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
    '            Dim filterExpression As String = ""

    '            Dim tableView = e.Item.OwnerTableView
    '            Dim filterItem = DirectCast(tableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
    '            Dim arg = Request("__EVENTARGUMENT")

    '            'parse the filter expressions to extract column name and filter value
    '            Dim filters = New Dictionary(Of String, String)()
    '            For Each item As String In arg.Split(New String() {"||"}, StringSplitOptions.RemoveEmptyEntries)
    '                Dim parts = item.Split(New String() {"|?"}, StringSplitOptions.RemoveEmptyEntries)
    '                Dim columnName = parts(0)
    '                Dim functionName = parts(2)

    '                filters(columnName) = functionName
    '            Next

    '            For Each column As GridColumn In tableView.RenderColumns
    '                If Not column.SupportsFiltering() Then
    '                    Continue For
    '                End If

    '                If filters.ContainsKey(column.UniqueName) Then
    '                    column.RefreshCurrentFilterValue(filterItem, filters(column.UniqueName))
    '                Else
    '                    column.RefreshCurrentFilterValue(filterItem)
    '                End If
    '                Dim a As Integer = 0
    '                Try
    '                    Dim txt As TextBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(0), TextBox)
    '                    'Dim filterText = column.EvaluateFilterExpression(filterItem)
    '                    If txt.Text = "" Then
    '                        column.ResetCurrentFilterValue(filterItem)
    '                        oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
    '                        Continue For
    '                    End If

    '                    If Not [String].IsNullOrEmpty(filterExpression) Then
    '                        filterExpression += " AND "
    '                    End If

    '                    oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.Text)

    '                    filterExpression += "([" + column.UniqueName.ToString() + "] Like '%" + txt.Text + "%')"
    '                Catch ex As Exception
    '                    a = 1
    '                End Try
    '                Try
    '                    If a = 1 Then
    '                        Dim txt As RadComboBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadComboBox)
    '                        'Dim filterText = column.EvaluateFilterExpression(filterItem)
    '                        If txt.SelectedValue = "" Then
    '                            column.ResetCurrentFilterValue(filterItem)
    '                            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
    '                            Continue For
    '                        End If

    '                        If Not [String].IsNullOrEmpty(filterExpression) Then
    '                            filterExpression += " AND "
    '                        End If

    '                        oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedValue)

    '                        filterExpression += "([" + column.UniqueName.ToString() + "] = '" + txt.SelectedValue + "')"

    '                    End If
    '                Catch ex As Exception

    '                End Try
    '                Try
    '                    If a = 1 Then
    '                        Dim txt As RadDatePicker = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadDatePicker)
    '                        'Dim filterText = column.EvaluateFilterExpression(filterItem)
    '                        If txt.SelectedDate Is Nothing Then
    '                            column.ResetCurrentFilterValue(filterItem)
    '                            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
    '                            Continue For
    '                        End If

    '                        If Not [String].IsNullOrEmpty(filterExpression) Then
    '                            filterExpression += " AND "
    '                        End If

    '                        oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedDate)

    '                        filterExpression += "( [" + column.UniqueName.ToString() + "] = '" + txt.SelectedDate + "' )"
    '                    End If
    '                Catch ex As Exception

    '                End Try
    '            Next
    '            If filterExpression = "" Then
    '                'grdEmployee.MasterTableView.FilterExpression = String.Empty
    '                'grdEmployee.MasterTableView.Rebind()
    '            Else
    '                tableView.FilterExpression = filterExpression
    '                tableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
    '                tableView.Rebind()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Staff_List:rdStaff_ItemCommand:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rdStaff_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then

                Session("staff_id") = Val(e.CommandArgument)
                Response.Redirect("Staff_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteStaff(Val(e.CommandArgument))
                bindGrid()
                'ElseIf e.CommandName = "Clear" Then
                '    For Each column As GridColumn In rdStaff.MasterTableView.Columns
                '        'column.CurrentFilterFunction = GridKnownFunction.NoFilter
                '        column.CurrentFilterValue = String.Empty
                '    Next
                '    rdStaff.MasterTableView.FilterExpression = String.Empty
                '    rdStaff.MasterTableView.Rebind()
                '    oSessionManager.ResetSessionDT(Page_Name.ToString)
                'ElseIf e.CommandName = "Search" Then

                '    Dim filteringItem As GridFilteringItem = DirectCast(rdStaff.MasterTableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
                '    Dim filterExpression As String = ""

                '    Dim tableView = e.Item.OwnerTableView
                '    Dim filterItem = DirectCast(tableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
                '    Dim arg = Request("__EVENTARGUMENT")

                '    'parse the filter expressions to extract column name and filter value
                '    Dim filters = New Dictionary(Of String, String)()
                '    For Each item As String In arg.Split(New String() {"||"}, StringSplitOptions.RemoveEmptyEntries)
                '        Dim parts = item.Split(New String() {"|?"}, StringSplitOptions.RemoveEmptyEntries)
                '        Dim columnName = parts(0)
                '        Dim functionName = parts(2)

                '        filters(columnName) = functionName
                '    Next

                '    For Each column As GridColumn In tableView.RenderColumns
                '        If Not column.SupportsFiltering() Then
                '            Continue For
                '        End If

                '        If filters.ContainsKey(column.UniqueName) Then
                '            column.RefreshCurrentFilterValue(filterItem, filters(column.UniqueName))
                '        Else
                '            column.RefreshCurrentFilterValue(filterItem)
                '        End If
                '        Dim a As Integer = 0
                '        Try
                '            Dim txt As TextBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(0), TextBox)
                '            'Dim filterText = column.EvaluateFilterExpression(filterItem)
                '            If txt.Text = "" Then
                '                column.ResetCurrentFilterValue(filterItem)
                '                oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                '                Continue For
                '            End If

                '            If Not [String].IsNullOrEmpty(filterExpression) Then
                '                filterExpression += " AND "
                '            End If

                '            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.Text)

                '            filterExpression += "([" + column.UniqueName.ToString() + "] Like '%" + txt.Text + "%')"
                '        Catch ex As Exception
                '            a = 1
                '        End Try
                '        Try
                '            If a = 1 Then
                '                Dim txt As RadComboBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadComboBox)
                '                'Dim filterText = column.EvaluateFilterExpression(filterItem)
                '                If txt.SelectedValue = "" Then
                '                    column.ResetCurrentFilterValue(filterItem)
                '                    oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                '                    Continue For
                '                End If

                '                If Not [String].IsNullOrEmpty(filterExpression) Then
                '                    filterExpression += " AND "
                '                End If

                '                oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedValue)

                '                filterExpression += "([" + column.UniqueName.ToString() + "] = '" + txt.SelectedValue + "')"

                '            End If
                '        Catch ex As Exception

                '        End Try
                '        Try
                '            If a = 1 Then
                '                Dim txt As RadDatePicker = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadDatePicker)
                '                'Dim filterText = column.EvaluateFilterExpression(filterItem)
                '                If txt.SelectedDate Is Nothing Then
                '                    column.ResetCurrentFilterValue(filterItem)
                '                    oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                '                    Continue For
                '                End If

                '                If Not [String].IsNullOrEmpty(filterExpression) Then
                '                    filterExpression += " AND "
                '                End If

                '                oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedDate)

                '                filterExpression += "( [" + column.UniqueName.ToString() + "] = '" + txt.SelectedDate + "' )"
                '            End If
                '        Catch ex As Exception

                '        End Try
                '    Next
                '    If filterExpression = "" Then
                '        'grdEmployee.MasterTableView.FilterExpression = String.Empty
                '        'grdEmployee.MasterTableView.Rebind()
                '    Else
                '        tableView.FilterExpression = filterExpression
                '        tableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
                '        tableView.Rebind()
                '    End If
            End If
        Catch ex As Exception
            LogHelper.Error("Staff_List:rdStaff_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteStaff(ByVal id As Integer)
        Try
            oclsStaff.staff_id = Val(id)
            oclsStaff.photo = ""
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.staff_code = ""
            oclsStaff.name = ""
            oclsStaff.till_active = 0
            oclsStaff.joining_date = DateTime.Now
            oclsStaff.branch_id = 0
            oclsStaff.department_id = 0
            oclsStaff.designation_id = 0
            oclsStaff.role_id = 0
            oclsStaff.email = ""
            oclsStaff.contact_no = ""
            oclsStaff.till_code = ""
            oclsStaff.address = ""
            oclsStaff.country_id = 0
            oclsStaff.state_id = 0
            oclsStaff.city_id = 0
            oclsStaff.national_id = ""
            oclsStaff.last_working_date = DateTime.Now
            oclsStaff.is_active = 1
            oclsStaff.ip_address = ""
            oclsStaff.other_id = ""
            oclsStaff.login_id = 0
            oclsStaff.machine_id = 0
            oclsStaff.Authentication_Code = ""
            oclsStaff.Function_type_id = ""
            oclsStaff.Tran_Type = "D"
            oclsStaff.Delete()
            'If Session("staff_id_login") = Val(id) Then
            '    Session("Msg") = "Record deleted successfully."
            '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + Session("Msg") + "');window.location='SignIn.aspx';", True)
            'End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Staff_List:deleteStaff:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            'Dim tillCodeHiddenField As HiddenField = DirectCast(FindControl("till_code"), HiddenField)
            ' tillCodeHiddenField = DirectCast(FindControl("till_code"), HiddenField)
            ' Dim tillCode As Integer = tillCodeHiddenField.Value
            'Session("tillcode") = tillCode
            Session("staff_id") = Nothing

            Response.Redirect("Staff_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Staff_List:lnkNew_Click:" + ex.Message)
        End Try

    End Sub


    'Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
    '    Try

    '        rdStaff.Rebind()

    '        If Not IsPostBack Then
    '            Dim strVal As String = ""
    '            If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
    '                For Each column As GridColumn In rdStaff.MasterTableView.Columns
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
    '                    rdStaff.MasterTableView.FilterExpression = String.Empty
    '                    rdStaff.MasterTableView.Rebind()
    '                Else
    '                    rdStaff.MasterTableView.FilterExpression = FilterExpression
    '                    rdStaff.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
    '                    rdStaff.MasterTableView.Rebind()
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
    '        For Each item As GridFilteringItem In rdStaff.MasterTableView.GetItems(GridItemType.FilteringItem)

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


    Protected Sub lnkSyncallTill_Click(sender As Object, e As EventArgs)
        Try
            oclsKeyMap.cmp_id = Val(Session("cmp_id"))
            oclsKeyMap.SyncKeymapAllTill()
        Catch ex As Exception
            LogHelper.Error("Key_Map_List:rdKayMap_ItemCommand" + ex.Message)
        End Try
    End Sub


End Class
