Imports System.Data
Imports Telerik.Web.UI

Partial Class Function_Type_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsFunctionType As New Controller_clsFunctionType

    Public Sub bindGrid()
        Try
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsFunctionType.SelectAll()
            If dt.Rows.Count > 0 Then
                rdFunctionType.DataSource = dt
                rdFunctionType.DataBind()
            Else
                rdFunctionType.DataSource = String.Empty
                rdFunctionType.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_Type_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Public Sub rebindGrid()
        Try
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsFunctionType.SelectAll()
            If dt.Rows.Count > 0 Then
                rdFunctionType.DataSource = dt
            Else
                rdFunctionType.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_Type_List:rebindGrid" + ex.Message)
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
                If Val(ViewState("view")) = 1 Or Val(("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divFunctionType.Visible = True
                Else
                    divFunctionType.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Function_Type_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Function Type"
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
            LogHelper.Error("Function_Type_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdFunctionType_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdFunctionType.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdFunctionType.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Printer_List:rdPrinter_ItemCreated:" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdFunctionType_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdFunctionType.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdFunctionType.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Function_Type_List:rdFunctionType_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdFunctionType_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdFunctionType.ItemCommand

    'End Sub

    Protected Sub rdFunctionType_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("function_type_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Function_Type_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteFunctionType(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            ElseIf e.CommandName = "Update" Then
                UpdateFunction(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            ElseIf e.CommandName = "UpdatePanel" Then
                UpdateFunctionPanel(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Function_Type_List:rdFunctionType_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub UpdateFunction(ByVal id As Integer)
        Try
            Dim s As String
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.function_type_id = id
            Dim dtFunction As DataTable = oclsFunctionType.Select()
            If dtFunction.Rows.Count > 0 Then
                s = dtFunction.Rows(0)("Active")
            End If
            oclsFunctionType.function_type_id = Val(id)
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.is_active = IIf(s.ToString() = "Active", 0, 1)
            oclsFunctionType.UpdateActiveInactive()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record update successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Type_List:deleteFunctionType" + ex.Message)
        End Try
    End Sub

    Protected Sub UpdateFunctionPanel(ByVal id As Integer)
        Try
            Dim s As String
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.function_type_id = id
            Dim dtFunction As DataTable = oclsFunctionType.Select()
            If dtFunction.Rows.Count > 0 Then
                s = dtFunction.Rows(0)("Panel")
            End If
            oclsFunctionType.function_type_id = Val(id)
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.is_panel = IIf(s.ToString() = "Is Panel", 0, 1)
            oclsFunctionType.UpdatePanelNotPanel()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record update successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Type_List:deleteFunctionType" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteFunctionType(ByVal id As Integer)
        Try
            oclsFunctionType.function_type_id = Val(id)
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.name = ""
            oclsFunctionType.Ip_address = ""
            oclsFunctionType.login_id = 0
            oclsFunctionType.Tran_Type = "D"
            oclsFunctionType.machine_id = 0
            oclsFunctionType.is_active = 0
            oclsFunctionType.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Type_List:deleteFunctionType" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("function_Type_id") = Nothing
            Response.Redirect("Function_Type_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Function_Type_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdFunctionType_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdFunctionType.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Function_Type_List:rdFunctionType_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

End Class