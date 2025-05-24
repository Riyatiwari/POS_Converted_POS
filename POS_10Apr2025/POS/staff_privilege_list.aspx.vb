Imports System.Data
Imports Telerik.Web.UI

Partial Class staff_privilege_list
    Inherits BaseClass
    Dim oclsRole As New Controller_clsRole
    Dim oClsDataccess As New ClsDataccess
    Dim oclsStaff As New Controller_clsStaff()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If

                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("staff_privilege_list:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsStaff.[SelectAll_rules]()
            If dt.Rows.Count > 0 Then
                RadGridMain.DataSource = dt
                RadGridMain.DataBind()
            Else
                RadGridMain.DataSource = String.Empty
                RadGridMain.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege_list:bindGrid:" + ex.Message)
        End Try

    End Sub


    Protected Sub RadGridMain_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("m_staff_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("staff_privilege.aspx", False)
            ElseIf e.CommandName = "Del" Then
                deleteRole(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("staff_privilege_list:lnkNew_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteRole(ByVal id As Integer)
        Try
            oclsStaff.m_staff_id = Val(id)
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.name = ""
            oclsStaff.is_active = 0
            oclsStaff.DeleteStaffRulesMaster()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("staff_privilege_list:deleteRole:" + ex.Message)
        End Try
    End Sub


    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("staff_privilege.aspx", False)
        Catch ex As Exception
            LogHelper.Error("staff_privilege_list:lnkNew_Click:" + ex.Message)
        End Try
    End Sub
End Class
