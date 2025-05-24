Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Partial Class Voucher_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim objclsVoucherMaster As New Controller_clsVoucher()
    Public Sub bindGrid()
        Try
            objclsVoucherMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsVoucherMaster.SelectAll()
            If dt.Rows.Count > 0 Then
                rdvoucher.DataSource = dt
                rdvoucher.DataBind()
            Else
                rdvoucher.DataSource = String.Empty
                rdvoucher.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Voucher_List:bindGrid" + ex.Message)
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
            LogHelper.Error("Voucher_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try
            Session("voucher_id") = Nothing

            Response.Redirect("Voucher_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Voucher_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub rdvoucher_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdvoucher.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("voucher_id") = Val(e.CommandArgument)
                Response.Redirect("Voucher_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteVoucher(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Voucher_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteVoucher(ByVal id As Integer)
        Try
            objclsVoucherMaster.voucher_id = Val(id)
            objclsVoucherMaster.cmp_id = Val(Session("cmp_id"))
            objclsVoucherMaster.voucher_name = ""
            objclsVoucherMaster.Voucher_Type = 0
            objclsVoucherMaster.start_date = DateTime.Now
            objclsVoucherMaster.end_date = DateTime.Now
            objclsVoucherMaster.endless = 0
            objclsVoucherMaster.Tran_Type = "D"

            objclsVoucherMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Voucher:deleteAlrgy" + ex.Message)
        End Try
    End Sub
End Class
