Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Partial Class Issue_Voucher_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim objclsIssueVoucherMaster As New Controller_clsIssueVoucher()
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
            LogHelper.Error("Issue_Voucher_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub rdvoucher_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdvoucher.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("Issuevoucher_id") = Val(e.CommandArgument)
                Response.Redirect("Issue_Voucher_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteVoucher(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Issue_Voucher_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Session("Issuevoucher_id") = Nothing
        Response.Redirect("Issue_Voucher_Master.aspx")
    End Sub
    Protected Sub deleteVoucher(ByVal id As Integer)
        Try
           
            objclsIssueVoucherMaster.Issuevoucher_id = Val(id)
            objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))
            objclsIssueVoucherMaster.account = 0
            objclsIssueVoucherMaster.Voucher_id = 0
            objclsIssueVoucherMaster.deposit_amount = 0
            objclsIssueVoucherMaster.issue_datetime = DateTime.Now
            objclsIssueVoucherMaster.ref_no = ""
            objclsIssueVoucherMaster.Tran_Type = "D"

            objclsIssueVoucherMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Voucher:deleteAlrgy" + ex.Message)
        End Try
    End Sub
    Public Sub bindGrid()
        Try
            objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsIssueVoucherMaster.SelectAll()
            If dt.Rows.Count > 0 Then
                rdvoucher.DataSource = dt
                rdvoucher.DataBind()
            Else
                rdvoucher.DataSource = String.Empty
                rdvoucher.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Issue_Voucher_List:bindGrid" + ex.Message)
        End Try
    End Sub
End Class
