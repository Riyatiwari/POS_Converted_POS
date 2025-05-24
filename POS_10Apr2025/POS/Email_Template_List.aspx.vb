Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Reporting

Partial Class Email_Template_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsEmailTemplete As New Controller_clsTemplate

    Public Sub bindGrid()
        Try
            oclsEmailTemplete.Cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsEmailTemplete.SelectAll()

            If dt.Rows.Count > 0 Then
                rdTemplate.DataSource = dt
                rdTemplate.DataBind()
            Else
                rdTemplate.DataSource = String.Empty
                rdTemplate.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Email_Template_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsEmailTemplete.Cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsEmailTemplete.SelectAll()

            If dt.Rows.Count > 0 Then
                rdTemplate.DataSource = dt
            Else
                rdTemplate.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Email_Template_List:rebindGrid" + ex.Message)
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
            LogHelper.Error("Email_Template_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Email Template"
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
            LogHelper.Error("Email_Template_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdTemplate_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdTemplate.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(("delete")) = 0 Then
    '                rdTemplate.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            End If
    '            If Val(("edit")) = 1 Then
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
    '        LogHelper.Error("Email_Template_List:rdTemplate_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdTemplate_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdTemplate.ItemCommand

    'End Sub

    Protected Sub rdTemplate_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("TemplateID") = Val(e.CommandArgument)
                Response.Redirect("Email_Template_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteRole(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Template_List:rdTemplate_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteRole(ByVal id As Integer)
        Try
            oclsEmailTemplete.Template_id = Val(id)
            oclsEmailTemplete.Cmp_id = Val(Session("cmp_id"))
            oclsEmailTemplete.Template_name = ""
            oclsEmailTemplete.subject = ""
            oclsEmailTemplete.template_detail = ""
            oclsEmailTemplete.is_newsletter = 0
            oclsEmailTemplete.Tran_Type = "D"
            oclsEmailTemplete.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            LogHelper.Error("Email_Template_List:deleteRole" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("TemplateID") = Nothing
            Response.Redirect("Email_Template_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Email_Template_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdTemplate_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdTemplate.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Email_Template_List:rdTemplate_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

End Class
