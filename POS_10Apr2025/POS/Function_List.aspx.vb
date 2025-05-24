Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Function_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsFunction As New Controller_clsFunction()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Function_List"

    Public Sub bindGrid()
        Try
            'oclsFunction.cmp_id = Val(Session("cmp_id"))
            'Dim dt As DataTable = oclsFunction.SelectAll()

            'If dt.Rows.Count > 0 Then
            '    rdFunction.DataSource = dt
            '    rdFunction.DataBind()
            'Else
            '    rdFunction.DataSource = String.Empty
            '    rdFunction.DataBind()
            'End If

            oclsFunction.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsFunction.SelectAllMainFunction()

            If dt.Rows.Count > 0 Then
                rdFunction.DataSource = dt
                rdFunction.DataBind()
            Else
                rdFunction.DataSource = String.Empty
                rdFunction.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_List:bindGrid" + ex.Message)
        End Try
        
    End Sub
    Public Sub rebindGrid()
        Try
            'oclsFunction.cmp_id = Val(Session("cmp_id"))
            'Dim dt As DataTable = oclsFunction.SelectAll()

            'If dt.Rows.Count > 0 Then
            '    rdFunction.DataSource = dt
            'Else
            '    rdFunction.DataSource = String.Empty
            'End If

            oclsFunction.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsFunction.SelectAllMainFunction()

            If dt.Rows.Count > 0 Then
                rdFunction.DataSource = dt
            Else
                rdFunction.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_List:rebindGrid" + ex.Message)
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
                    ViewState("edit") = 1
                    ViewState("add") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("edit") = 0
                    ViewState("add") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divFunction.Visible = True
                Else
                    divFunction.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                'If ViewState("edit") = 1 Then
                '    lnkNew.Visible = True
                'Else
                '    lnkNew.Visible = False
                'End If
                'If ViewState("delete") = 1 Then
                '    lnkNew.Visible = True
                'Else
                '    lnkNew.Visible = False
                'End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Function_List:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Function Map"
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
            LogHelper.Error("Function_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdFunction_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdFunction.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdFunction.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        ElseIf (TypeOf e.Item Is GridFilteringItem) Then
    '            'oclsBind.BindFunctionTypeAll(CType(e.Item.FindControl("ddlFunctionType"), RadComboBox), Val(Session("cmp_id")))
    '            'oclsBind.BindPanel1(CType(e.Item.FindControl("ddlPanel"), RadComboBox), Val(Session("cmp_id")))

    '            'oclsBind.BindParentFunction1(CType(e.Item.FindControl("ddlParent"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindMachine1(CType(e.Item.FindControl("RadMachine"), RadComboBox), Val(Session("cmp_id")))

    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Function_List:rdFunction_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub
    'Protected Sub rdFunction_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdFunction.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdFunction.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Function_List:rdFunction_ItemCreated" + ex.Message)
    '    End Try

    'End Sub
    'Protected Sub rdFunction_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdFunction.ItemCommand

    'End Sub

    Protected Sub deleteFunction(ByVal id As Integer)
        Try
            oclsFunction.mainfunction_id = Val(id)
            oclsFunction.cmp_id = Val(Session("cmp_id"))
            oclsFunction.name = ""
            oclsFunction.is_active = 1
            oclsFunction.login_id = 0
            oclsFunction.ip_address = ""
            oclsFunction.machine_id = 0
            oclsFunction.Tran_Type = "D"
            oclsFunction.DeleteMainFunction()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_List:deleteFunction" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("mainfunction_id") = Nothing
            Response.Redirect("Function_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Function_List:lnkNew_Click" + ex.Message)
        End Try
      
    End Sub

    'Protected Sub rdFunction_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdFunction.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Function_List:rdFunction_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub
    'Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
    '    Try

    '        'rdFunction.Rebind()

    '        If Not IsPostBack Then
    '            Dim strVal As String = ""
    '            If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
    '                For Each column As GridColumn In rdFunction.MasterTableView.Columns
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
    '                    rdFunction.MasterTableView.FilterExpression = String.Empty
    '                    rdFunction.MasterTableView.Rebind()
    '                Else
    '                    rdFunction.MasterTableView.FilterExpression = FilterExpression
    '                    rdFunction.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
    '                    rdFunction.MasterTableView.Rebind()
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
    '        For Each item As GridFilteringItem In rdFunction.MasterTableView.GetItems(GridItemType.FilteringItem)

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

    Private Sub lnkCopy_Click(sender As Object, e As EventArgs) Handles lnkCopy.Click
        Try
            For Each item As RepeaterItem In rdFunction.Items
                Dim value As String
                'Dim dataitem As GridDataItem = CType(item, GridDataItem)
                'Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim hd_FunctionId As HiddenField = item.FindControl("hd_FunctionId")
                Dim checkBox As CheckBox = item.FindControl("chk_FunctionId")
                If checkBox.Checked Then
                    value = hd_FunctionId.Value
                    Session("mainfunction_id") = value.ToString()
                End If
            Next
            If Session("mainfunction_id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                oclsFunction.mainfunction_id = Val(Session("mainfunction_id"))
                oclsFunction.cmp_id = Val(Session("cmp_id"))
                oclsFunction.Copy_Function()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record copy successfully');", True)
                bindGrid()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub rdFunction_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("mainfunction_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Function_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteFunction(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Function_List:rdFunction_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class

