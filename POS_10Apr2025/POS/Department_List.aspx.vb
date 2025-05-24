Imports System.Data
Imports Telerik.Web.UI

Partial Class Department_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsDept As New Controller_clsDepartment

    Public Sub bindGrid()
        Try
            oclsDept.Department_id = 0
            oclsDept.Cmp_id = Val(Session("cmp_id"))
            Dim dtDepartment As DataTable = oclsDept.Select()

            If dtDepartment.Rows.Count > 0 Then
                rdDepartment.DataSource = dtDepartment
                rdDepartment.DataBind()
            Else
                rdDepartment.DataSource = String.Empty
                rdDepartment.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Department_List:bindGrid" + ex.Message)
        End Try

    End Sub

    Public Sub rebindGrid()
        Try
            oclsDept.Department_id = 0
            oclsDept.Cmp_id = Val(Session("cmp_id"))
            Dim dtDepartment As DataTable = oclsDept.Select()

            If dtDepartment.Rows.Count > 0 Then
                rdDepartment.DataSource = dtDepartment
            Else
                rdDepartment.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Department_List:rebindGrid" + ex.Message)
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
                    divDepartment.Visible = True
                Else
                    divDepartment.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Department_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Department"
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

            LogHelper.Error("Department_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdDepartment_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdDepartment.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdDepartment.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Department_List:rdDepartment_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdDepartment_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdDepartment.ItemCommand
    '    Try
    '        If e.CommandName = "Edit" Then
    '            Session("Department_id") = Convert.ToInt32(e.CommandArgument)
    '            Response.Redirect("Department_Master.aspx", False)
    '        ElseIf e.CommandName = "Delete" Then
    '            deleteRole(Convert.ToInt32(e.CommandArgument))
    '            bindGrid()
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Department_List:rdDepartment_ItemCommand" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub deleteRole(ByVal id As Integer)
        Try
            oclsDept.Department_id = Val(id)
            oclsDept.cmp_id = Val(Session("cmp_id"))
            oclsDept.Name = ""
            oclsDept.Value = 0
            oclsDept.Ip_Address = ""
            oclsDept.login_id = 0
            oclsDept.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Department_List:deleteRole" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Department_id") = Nothing
            Response.Redirect("Department_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Department_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub
    'Protected Sub rdDepartment_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdDepartment.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Department_List:rdDepartment_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub rdDepartment_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Department_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Department_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteRole(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Department_List:rdDepartment_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class
