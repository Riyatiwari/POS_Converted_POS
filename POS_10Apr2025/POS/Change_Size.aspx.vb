Imports System.Data
Imports Telerik.Web.UI
Partial Class Change_Size
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProduct As New Controller_clsProduct()
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Change_Product_Group_List"


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
                oclsBind.BindTax(radTax, Val(Session("cmp_id")))
                oclsBind.BindProductGroupMain(rdGroupCateogry, Val(Session("cmp_id")))
                oclsBind.BindProductGroup(rdProductGroup, Val(Session("cmp_id")))
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Change_Product_Group_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCategory.SelectAll()
            Dim dv As DataView = dt.DefaultView
            If rdGroupCateogry.SelectedIndex > 0 Then
                dv.RowFilter = "maincategory_id = " + rdGroupCateogry.SelectedValue.ToString()
                dt = dv.ToTable()
            End If
            If rdProductGroup.SelectedIndex > 0 Then
                dv.RowFilter = "category_id = " + rdProductGroup.SelectedValue.ToString()
                dt = dv.ToTable()
            End If
            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
                rdcopyProduct.DataBind()
            Else
                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Change_Product_Group_List:bindGrid:" + ex.Message)
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
            LogHelper.Error("Change_Product_Group_List:RoleCheck:" + ex.Message)
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
            LogHelper.Error("Change_Product_Group_List:rdcopyProduct_ItemCreated:" + ex.Message)
        End Try

    End Sub


    Protected Sub rdcopyProduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdcopyProduct.ItemDataBound
        Try
            'If (TypeOf e.Item Is GridDataItem) Then
            '    Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            '    If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
            '        rdcopyProduct.MasterTableView.GetColumn("TemplateColumn").Visible = False
            '    End If
            '    If Val(ViewState("edit")) = 1 Then
            '        CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
            '    Else
            '        CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
            '    End If
            '    If Val(ViewState("delete")) = 1 Then
            '        CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
            '    Else
            '        CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
            '    End If

            'ElseIf (TypeOf e.Item Is GridFilteringItem) Then
            '    oclsBind.BindProductGroup1(CType(e.Item.FindControl("ddlCategory"), RadComboBox), Val(Session("cmp_id")))
            '    oclsBind.BindPrinter1(CType(e.Item.FindControl("ddlPrinter"), RadComboBox), Val(Session("cmp_id")))
            '    oclsBind.BindDepartment1(CType(e.Item.FindControl("ddlDepartment"), RadComboBox), Val(Session("cmp_id")))
            '    oclsBind.BindCourse1(CType(e.Item.FindControl("ddlCourse"), RadComboBox), Val(Session("cmp_id")))
            '    oclsBind.BindUnit1(CType(e.Item.FindControl("ddlUnit"), RadComboBox), Val(Session("cmp_id")))
            'End If
        Catch ex As Exception
            LogHelper.Error("Change_Product_Group_List:rdcopyProduct_ItemDataBound:" + ex.Message)
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
            LogHelper.Error("Change_Product_Group_List:Page_LoadComplete:" + ex.Message)
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
            LogHelper.Error("Change_Product_Group_List:GridFilterBind:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdcopyProduct_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdcopyProduct.ItemCommand
        Try
            If e.CommandName = "Clear" Then
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
            LogHelper.Error("Change_Product_Group_List:rdcopyProduct_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdcopyProduct_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdcopyProduct.NeedDataSource
        Try
            ReBindGrid()
        Catch ex As Exception
            LogHelper.Error("Change_Product_Group_List:rdcopyProduct_NeedDataSource:" + ex.Message)
        End Try
    End Sub

    Public Sub ReBindGrid()
        Try
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCategory.SelectAll()
            Dim dv As DataView = dt.DefaultView
            If rdGroupCateogry.SelectedIndex > 0 Then
                dv.RowFilter = "maincategory_id = " + rdGroupCateogry.SelectedValue.ToString()
                dt = dv.ToTable()
            End If
            If rdProductGroup.SelectedIndex > 0 Then
                dv.RowFilter = "category_id = " + rdProductGroup.SelectedValue.ToString()
                dt = dv.ToTable()
            End If

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Change_Product_Group_List:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim value As String = "0"
            For Each item As GridItem In rdcopyProduct.MasterTableView.Items

                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)
                If checkBox.Checked Then
                    value = "1"
                    oclsCategory.cmp_id = Val(Session("cmp_id"))
                    oclsProduct.Category_id = dataitem.GetDataKeyValue("category_id").ToString()
                    oclsProduct.Tax_id = Val(radTax.SelectedValue)

                    oclsProduct.Change_ProductSizeBulk()

                End If

            Next
            If value = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record changed successfully');", True)
                bindGrid()


            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Change_Product_Group_List:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub rdGroupCateogry_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rdGroupCateogry.SelectedIndexChanged
        Try
            bindGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdProductGroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rdProductGroup.SelectedIndexChanged

        Try
            bindGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class
