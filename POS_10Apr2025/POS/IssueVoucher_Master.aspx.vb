Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class IssueVoucher_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPromotion As New Controller_clsPromotion()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If


                BindVoucher()

            End If
        Catch ex As Exception
            LogHelper.Error("IssueVoucher_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindVoucher()
        Try
            oclsPromotion.Promo_name = ""
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            oclsPromotion.BindPromotion(ddVoucher)


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("IssueVoucher_Master:BindVoucher:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try

        Catch ex As Exception
            LogHelper.Error("IssueVoucher_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("IssueVoucher.aspx", False)
        Catch ex As Exception
            LogHelper.Error("IssueVoucher_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            'txtName.Text = ""
            'txtValue.Text = ""
            'txtValue1.Text = ""
            'ddMode.SelectedValue = 0
            'chkActive.Checked = True
            'txtEffDate.SelectedDate = Nothing
        Catch ex As Exception
            LogHelper.Error("IssueVoucher_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsPromotion.Promo_name = ""
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            oclsPromotion.Voucher_Code = ddVoucher.SelectedValue.ToString()
            oclsPromotion.Voucher_Start = txtStartValue.Text.ToString()
            oclsPromotion.Voucher_end = txtEndVal.Text.ToString()
            Dim dt As DataTable = oclsPromotion.Insert_Voucher()



            Session("Success") = "Record inserted successfully"
            Response.Redirect("IssueVoucher.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("IssueVoucher_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub


End Class

