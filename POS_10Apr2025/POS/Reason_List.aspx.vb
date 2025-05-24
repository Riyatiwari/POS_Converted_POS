Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Reason_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsreasion As New Controller_clsReason

    Public Sub bindGrid()
        Try
            oclsreasion.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsreasion.SelectAll()
            If dt.Rows.Count > 0 Then
                rdCategory.DataSource = dt
                rdCategory.DataBind()
            Else
                rdCategory.DataSource = String.Empty
                rdCategory.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Reason_List:bindGrid:" + ex.Message)
        End Try


    End Sub
    Public Sub rebindGrid()
        Try
            oclsreasion.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsreasion.SelectAll()
            If dt.Rows.Count > 0 Then
                rdCategory.DataSource = dt
            Else
                rdCategory.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Reason_List:rebindGrid:" + ex.Message)
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
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Reason_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Product Group"
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
            LogHelper.Error("Reason_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdCategory_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdCategory.ItemCreated
    '    If TypeOf e.Item Is GridFilteringItem Then
    '        For Each nColumn As GridColumn In rdCategory.MasterTableView.Columns
    '            If TypeOf nColumn Is GridBoundColumn Then
    '                Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                textItem.TabIndex = 1
    '            End If
    '        Next
    '    End If
    'End Sub

    'Protected Sub rdcategory_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdCategory.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdCategory.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Reason_List:rdcategory_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdcategory_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdCategory.ItemCommand

    'End Sub

    Protected Sub rdCategory_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("reason_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Reason_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteProdct_Group(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Reason_List:rdcategory_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteProdct_Group(ByVal id As Integer)
        Try
            oclsreasion.Reason_id = Val(id)
            oclsreasion.cmp_id = Val(Session("cmp_id"))
            oclsreasion.key_map_id = 0
            oclsreasion.Reason = ""
            oclsreasion.description = ""
            oclsreasion.Is_active = 0
            oclsreasion.Ip_address = ""
            oclsreasion.login_id = 0
            oclsreasion.machine_id = 0
            oclsreasion.sorting_no = 0
            oclsreasion.Tran_Type = "D"
            oclsreasion.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Reason_List:deleteProdct_Group:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("reason_id") = Nothing
            Response.Redirect("Reason_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Reason_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdCategory_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdCategory.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Reason_List:rdCategory_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

End Class
