Imports System.Data
Imports Telerik.Web.UI


Partial Class Role_List
    Inherits BaseClass
    Dim oclsRole As New Controller_clsRole
    Dim oClsDataccess As New ClsDataccess

    Public Sub bindGrid()
        Try
            oclsRole.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsRole.SelectAll()
            If dt.Rows.Count > 0 Then
                RadGridMain.DataSource = dt
                RadGridMain.DataBind()
            Else
                RadGridMain.DataSource = String.Empty
                RadGridMain.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Role_List:bindGrid:" + ex.Message)
        End Try
      
    End Sub
    'Public Sub rebindGrid()
    '    Try
    '        oclsRole.cmp_id = Val(Session("cmp_id"))
    '        Dim dt As DataTable = oclsRole.SelectAll()
    '        If dt.Rows.Count > 0 Then
    '            RadGridMain.DataSource = dt
    '        Else
    '            RadGridMain.DataSource = String.Empty
    '        End If
    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
    '        LogHelper.Error("Role_List:rebindGrid:" + ex.Message)
    '    End Try

    'End Sub
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
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divRole.Visible = True
                Else
                    divRole.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Role_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Role"
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
           LogHelper.Error("Role_List:RoleCheck:" + ex.Message)
        End Try
    End Sub


    Protected Sub deleteRole(ByVal id As Integer)
        Try
            oclsRole.Role_Id = Val(id)
            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Name = ""
            oclsRole.Type = 0
            oclsRole.Role_Detail = ""
            oclsRole.is_active = 0
            oclsRole.Ip_address = ""
            oclsRole.login_id = 0
            oclsRole.Tran_Type = "D"
            oclsRole.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Role_List:deleteRole:" + ex.Message)
        End Try
    End Sub


    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("RoleEdit") = Nothing
            Response.Redirect("Role_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Role_List:lnkNew_Click:" + ex.Message)
        End Try
       
    End Sub
    Protected Sub RadGridMain_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("RoleEdit") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Role_Master.aspx", False)
            ElseIf e.CommandName = "Del" Then
                deleteRole(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Role_List:RadGridMain_ItemCommand:" + ex.Message)
        End Try
    End Sub
End Class
