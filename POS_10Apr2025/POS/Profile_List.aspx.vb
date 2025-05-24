Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Partial Class Profile_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim objclsProfileMaster As New Controller_clsProfileMaster

    Public Sub bindGrid()
        Try

            objclsProfileMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsProfileMaster.SelectAll()
            If dt.Rows.Count > 0 Then
                rdprofile.DataSource = dt
                rdprofile.DataBind()
            Else
                rdprofile.DataSource = String.Empty
                rdprofile.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Profile_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If


                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    '  divLocation.Visible = True
                Else
                    '  divLocation.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Profile_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try
            Session("profile_id") = Nothing
            Response.Redirect("Profile_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Profile_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub rdprofile_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdprofile.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("profile_id") = Val(e.CommandArgument)
                Response.Redirect("Profile_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteProfile(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Profile_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteProfile(ByVal id As Integer)
        Try
            objclsProfileMaster.profile_id = Val(id)
            objclsProfileMaster.cmp_id = Val(Session("cmp_id"))
           
            objclsProfileMaster.profile_name = ""
            objclsProfileMaster.bonus_point = 0
            objclsProfileMaster.amount = 0
             objclsProfileMaster.Tran_Type = "D"

            objclsProfileMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Profile_List:deleteProfile" + ex.Message)
        End Try
    End Sub
End Class
