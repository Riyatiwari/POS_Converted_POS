Imports System.Data

Partial Class TillShifts_List
    Inherits System.Web.UI.Page

    Dim objclsTillShiftMaster As New Controller_clsTillShifts()
    Dim oClsDataccess As New ClsDataccess()
    Public Sub bindGrid()
        Try
            objclsTillShiftMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsTillShiftMaster.SelectAll()
            If dt.Rows.Count > 0 Then
                rdtill.DataSource = dt
                rdtill.DataBind()
            Else
                rdtill.DataSource = String.Empty
                rdtill.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TillShifts_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
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
                LogHelper.Error("TillShifts_Master:Page_Load" + ex.Message)
            End Try

        Catch ex As Exception
            LogHelper.Error("TillShifts_List:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try

            Session("tillshift_id") = Nothing

            Response.Redirect("TillShifts_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("TillShifts_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub rdtill_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdtill.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("tillshift_id") = Val(e.CommandArgument)
                Response.Redirect("TillShifts_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                delete(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("TillShifts_List:rdtill_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub delete(ByVal id As Integer)
        Try
            objclsTillShiftMaster.tillshift_id = Val(id)
            objclsTillShiftMaster.cmp_id = Val(Session("cmp_id"))
            objclsTillShiftMaster.machine_id = 0
            objclsTillShiftMaster.active = 0
            objclsTillShiftMaster.Tran_Type = "D"

            objclsTillShiftMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("TillShifts_List:deleteAlrgy" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCreateShift_Click(sender As Object, e As EventArgs)
        Try


            objclsTillShiftMaster.cmp_id = Session("cmp_id")

            objclsTillShiftMaster.InsertD()
            Session("Success") = "Record inserted successfully"
            bindGrid()
        Catch ex As Exception
            LogHelper.Error("TillShifts_Master:lnkNew_Click" + ex.Message)
        End Try
    End Sub
End Class



