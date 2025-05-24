Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Copy_Product_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProduct As New Controller_clsProduct
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Copy_Product_List"
    Dim oclsLocation As Controller_clsLocation = New Controller_clsLocation()
    Dim sb As New StringBuilder()

    Public Sub bindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.active = Val(1)
            Dim dt As DataTable = oclsProduct.SelectAll()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
                rdcopyProduct.DataBind()
            Else
                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Copy_Product_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                ElseIf Not Session("Update") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("Update").ToString() + "');", True)
                    Session("Update") = Nothing
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
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    'lnkNew.Visible = True
                Else
                    'lnkNew.Visible = False
                End If
                bindGrid()
                bindLocation()
            End If

        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Product Master"
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
            LogHelper.Error("Copy_Product_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdcopyProduct_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdcopyProduct.ItemCreated
        Try
            If TypeOf e.Item Is GridFilteringItem Then
                For Each nColumn As GridColumn In rdcopyProduct.MasterTableView.Columns
                    If TypeOf nColumn Is GridBoundColumn Then
                        Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
                        Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
                        Try
                            Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
                            textItem.TabIndex = 1
                        Catch ex1 As Exception
                            Try
                                Dim textItem As RadComboBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), RadComboBox)
                                textItem.TabIndex = 1
                            Catch ex2 As Exception

                            End Try
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:rdcopyProduct_ItemCreated:" + ex.Message)
        End Try

    End Sub

    Protected Sub rdcopyProduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdcopyProduct.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                    rdcopyProduct.MasterTableView.GetColumn("TemplateColumn").Visible = False
                End If
                If Val(ViewState("edit")) = 1 Then
                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
                End If
                If Val(ViewState("delete")) = 1 Then
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
                End If

            ElseIf (TypeOf e.Item Is GridFilteringItem) Then
                oclsBind.BindProductGroup1(CType(e.Item.FindControl("ddlCategory"), RadComboBox), Val(Session("cmp_id")))

            End If
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:rdcopyProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try

            rdcopyProduct.Rebind()

            If Not IsPostBack Then
                Dim strVal As String = ""
                If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
                    For Each column As GridColumn In rdcopyProduct.MasterTableView.Columns
                        strVal += column.UniqueName + "#"
                    Next
                    oSessionManager.CreateSessionDT(Page_Name.ToString, strVal.ToString)
                Else
                    Dim FilterExpression As String = ""
                    For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
                        If row("FIN").ToString() <> "" And row("VAL").ToString() <> "" Then
                            Dim arr As Array = row("VAL").ToString().Split("#")
                            If Not [String].IsNullOrEmpty(FilterExpression) Then
                                FilterExpression += " AND "
                            End If
                            If arr.Length - 1 = 0 Then
                                Dim a As Integer = 0
                                Try
                                    DateTime.Parse(row("VAL"))
                                Catch ex As Exception
                                    a = 1
                                End Try

                                If a = 1 Then
                                    FilterExpression += "([" + row("FIN").ToString() + "] Like '%" + row("VAL").ToString() + "%')"
                                Else
                                    FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL") + "' )"
                                End If
                            Else
                                If row("VAL").ToString.Contains("01/01/1900") Then
                                    FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL").ToString().Replace("01/01/1900", "").Replace("#", "") + "' )"
                                Else
                                    FilterExpression += "(([" + row("FIN").ToString() + "] >= '" + arr(0) + "') AND ([" + row("FIN").ToString() + "] <= '" + arr(1) + "'))"
                                End If
                            End If
                        End If
                    Next
                    If FilterExpression = "" Then
                        rdcopyProduct.MasterTableView.FilterExpression = String.Empty
                        rdcopyProduct.MasterTableView.Rebind()
                    Else
                        rdcopyProduct.MasterTableView.FilterExpression = FilterExpression
                        rdcopyProduct.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
                        rdcopyProduct.MasterTableView.Rebind()
                    End If

                End If

            End If

            GridFilterBind()
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:Page_LoadComplete:" + ex.Message)
        End Try
    End Sub
    Protected Sub GridFilterBind()
        Try
            For Each item As GridFilteringItem In rdcopyProduct.MasterTableView.GetItems(GridItemType.FilteringItem)

                For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
                    If row("FIN").ToString() <> "" And row("val").ToString() <> "" Then
                        Dim val As Integer = 0
                        Try
                            TryCast(item(row("FIN").ToString()).Controls(0), TextBox).Text = row("VAL").ToString
                        Catch ex As Exception
                            val = 1
                        End Try
                        If val = 1 Then
                            Try
                                If row("VAL") = "" Then
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = "ALL"
                                Else
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = row("VAL").ToString
                                End If
                            Catch ex As Exception
                                val = 2
                            End Try
                        End If
                        If val = 2 Then
                            Try
                                If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing And Not TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker) Is Nothing Then
                                    If row("val").ToString().Contains("01/01/1900") Then
                                        Dim arr As Array = row("val").ToString().Split("#")
                                        If arr(0).ToString.Contains("01/01/1900") = False Then
                                            TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
                                        End If
                                        If arr(1).ToString.Contains("01/01/1900") = False Then
                                            TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
                                        End If
                                    ElseIf row("val").ToString().Contains("#") Then
                                        Dim arr As Array = row("val").ToString().Split("#")
                                        TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
                                        TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
                                    End If
                                End If
                            Catch ex As Exception
                                val = 3
                            End Try
                        End If
                        If val = 3 Then
                            Try
                                If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing Then
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = row("VAL").ToString
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                Next
                'TryCast(item("Branch_Name").Controls(0), TextBox).Text = Session("flt_Branch_Code")
            Next
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:GridFilterBind:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdcopyProduct_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdcopyProduct.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("product_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Product_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteProduct(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            ElseIf e.CommandName = "Clear" Then
                For Each column As GridColumn In rdcopyProduct.MasterTableView.Columns
                    'column.CurrentFilterFunction = GridKnownFunction.NoFilter
                    column.CurrentFilterValue = String.Empty
                Next
                rdcopyProduct.MasterTableView.FilterExpression = String.Empty
                rdcopyProduct.MasterTableView.Rebind()
                oSessionManager.ResetSessionDT(Page_Name.ToString)
            ElseIf e.CommandName = "Search" Then

                Dim filteringItem As GridFilteringItem = DirectCast(rdcopyProduct.MasterTableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
                Dim filterExpression As String = ""

                Dim tableView = e.Item.OwnerTableView
                Dim filterItem = DirectCast(tableView.GetItems(GridItemType.FilteringItem)(0), GridFilteringItem)
                Dim arg = Request("__EVENTARGUMENT")

                'parse the filter expressions to extract column name and filter value
                Dim filters = New Dictionary(Of String, String)()
                For Each item As String In arg.Split(New String() {"||"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim parts = item.Split(New String() {"|?"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim columnName = parts(0)
                    Dim functionName = parts(2)

                    filters(columnName) = functionName
                Next

                For Each column As GridColumn In tableView.RenderColumns
                    If Not column.SupportsFiltering() Then
                        Continue For
                    End If

                    If filters.ContainsKey(column.UniqueName) Then
                        column.RefreshCurrentFilterValue(filterItem, filters(column.UniqueName))
                    Else
                        column.RefreshCurrentFilterValue(filterItem)
                    End If
                    Dim a As Integer = 0
                    Try
                        Dim txt As TextBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(0), TextBox)
                        'Dim filterText = column.EvaluateFilterExpression(filterItem)
                        If txt.Text = "" Then
                            column.ResetCurrentFilterValue(filterItem)
                            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                            Continue For
                        End If

                        If Not [String].IsNullOrEmpty(filterExpression) Then
                            filterExpression += " AND "
                        End If

                        oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.Text)

                        filterExpression += "([" + column.UniqueName.ToString() + "] Like '%" + txt.Text + "%')"
                    Catch ex As Exception
                        a = 1
                    End Try
                    Try
                        If a = 1 Then
                            Dim txt As RadComboBox = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadComboBox)
                            'Dim filterText = column.EvaluateFilterExpression(filterItem)
                            If txt.SelectedValue = "" Then
                                column.ResetCurrentFilterValue(filterItem)
                                oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                                Continue For
                            End If

                            If Not [String].IsNullOrEmpty(filterExpression) Then
                                filterExpression += " AND "
                            End If

                            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedValue)

                            filterExpression += "([" + column.UniqueName.ToString() + "] = '" + txt.SelectedValue + "')"

                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If a = 1 Then
                            Dim txt As RadDatePicker = TryCast(filteringItem(column.UniqueName.ToString()).Controls(1), RadDatePicker)
                            'Dim filterText = column.EvaluateFilterExpression(filterItem)
                            If txt.SelectedDate Is Nothing Then
                                column.ResetCurrentFilterValue(filterItem)
                                oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), "")
                                Continue For
                            End If

                            If Not [String].IsNullOrEmpty(filterExpression) Then
                                filterExpression += " AND "
                            End If

                            oSessionManager.AddValInSessionDT(Page_Name.ToString, column.UniqueName.ToString(), txt.SelectedDate)

                            filterExpression += "( [" + column.UniqueName.ToString() + "] = '" + txt.SelectedDate + "' )"
                        End If
                    Catch ex As Exception

                    End Try
                Next
                If filterExpression = "" Then
                    'grdEmployee.MasterTableView.FilterExpression = String.Empty
                    'grdEmployee.MasterTableView.Rebind()
                Else
                    tableView.FilterExpression = filterExpression
                    tableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
                    tableView.Rebind()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:rdcopyProduct_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteProduct(ByVal id As Integer)
        Try
            oclsProduct.product_id = Val(id)
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Category_id = 0
            oclsProduct.key_map_id = 0
            oclsProduct.code = ""
            oclsProduct.name = ""
            oclsProduct.price = 0
            oclsProduct.barcode = ""
            oclsProduct.description = ""
            oclsProduct.Is_active = 0
            oclsProduct.Ip_address = ""
            oclsProduct.login_id = 0
            oclsProduct.department_id = 0
            oclsProduct.course_id = 0
            oclsProduct.list = 0
            oclsProduct.Tax_id = 0
            oclsProduct.Actual_Price = 0
            oclsProduct.Tax = 0
            oclsProduct.printer_id = ""
            oclsProduct.machine_id = 0
            oclsProduct.other_id = ""
            oclsProduct.other_size = ""
            oclsProduct.Is_Ingredient = 0
            oclsProduct.Is_Condiment = 0
            oclsProduct.Tran_Type = "D"
            oclsProduct.Base = ""
            oclsProduct.Unit_id = 0
            oclsProduct.size_zero = 0
            oclsProduct.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Copy_Product_List:deleteProduct:" + ex.Message)
        End Try
    End Sub
    'Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
    '    Try
    '        Session("product_id") = Nothing
    '        Response.Redirect("Product_Master.aspx", False)
    '    Catch ex As Exception
    '        LogHelper.Error("Product_List:lnkNew_Click:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rdcopyProduct_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdcopyProduct.NeedDataSource
        Try
            ReBindGrid()
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:rdcopyProduct_NeedDataSource:" + ex.Message)
        End Try
    End Sub

    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.active = Val(1)
            Dim dt As DataTable = oclsProduct.SelectAll()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    'Protected Sub lnkCopy_Click(sender As Object, e As EventArgs) Handles lnkCopy.Click
    '    Try
    '        If Session("copy_product_id") = Nothing Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
    '        Else
    '            Response.Redirect("Product_Master.aspx", False)
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Copy_Product_List:lnkCopy_Click:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCopy.Click
        Try
            For Each item As GridItem In rdcopyProduct.MasterTableView.Items
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)
                If checkBox.Checked Then
                    value = dataitem.GetDataKeyValue("product_id").ToString()
                    Session("copy_product_id") = value.ToString()
                End If
            Next
            If Session("copy_product_id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                sb.Append("")
                Dim str As String = ""

                For Each item As GridItem In rdMachineDetail.MasterTableView.Items
                    Dim value As String
                    Dim dataitem As GridDataItem = CType(item, GridDataItem)
                    Dim cell As TableCell = dataitem("ClientSelectColumn")
                    Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)
                    If checkBox.Checked Then
                        value = dataitem.GetDataKeyValue("Location_id").ToString()
                        If str.ToString = "" Then
                            str = value
                        Else
                            str = str & "#" + value.ToString()
                        End If
                    End If

                Next

                'For Each item As GridDataItem In rdMachineDetail.Items
                '    Dim Checkbox_id As CheckBox = TryCast(item("Location").FindControl("chk_Machine"), CheckBox)
                '    Dim Machine_id As Integer = TryCast(item("Location").FindControl("hdmachine_id"), HiddenField).Value

                '    If Checkbox_id.Checked = True Then
                '        If str.ToString = "" Then
                '            str = Machine_id
                '        Else
                '            str = str & "#" + Machine_id.ToString()
                '        End If
                '    End If
                'Next
                Session("copy_product_Location_Id") = str.ToString()
                Response.Redirect("Product_Master.aspx", False)
            End If
        Catch ex As Exception
            LogHelper.Error("Copy_Product_List:lnkCopy_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub bindLocation()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            Dim dtMachine As DataTable = oclsLocation.SelectLocationByCmp()
            If dtMachine.Rows.Count > 0 Then
                rdMachineDetail.DataSource = dtMachine
                rdMachineDetail.DataBind()
            Else
                rdMachineDetail.DataSource = Nothing
                rdMachineDetail.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:bindmachine:" + ex.Message)
        End Try

    End Sub
End Class
