Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Partial Class Allergy_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim objclsAllergyMaster As New Controller_clsAllergy()
    Public Sub bindGrid()
        Try
            objclsAllergyMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsAllergyMaster.SelectAll()
            If dt.Rows.Count > 0 Then
                rdalrgy.DataSource = dt
                rdalrgy.DataBind()
            Else
                rdalrgy.DataSource = String.Empty
                rdalrgy.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Allergy_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If


                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("Allergy_Master:Page_Load" + ex.Message)
        End Try
    End Sub


    Protected Sub rdprofile_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdalrgy.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("allergy_id") = Val(e.CommandArgument)
                Response.Redirect("Allergy_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteAlrgy(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Allergy_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteAlrgy(ByVal id As Integer)
        Try
            objclsAllergyMaster.allergy_id = Val(id)
            objclsAllergyMaster.cmp_id = Val(Session("cmp_id"))
            objclsAllergyMaster.name = ""
            objclsAllergyMaster.description = 0
            objclsAllergyMaster.Tran_Type = "D"

            objclsAllergyMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Allergy_List:deleteAlrgy" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try

            Session("allergy_id") = Nothing

            Response.Redirect("Allergy_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Allergy_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub
End Class
