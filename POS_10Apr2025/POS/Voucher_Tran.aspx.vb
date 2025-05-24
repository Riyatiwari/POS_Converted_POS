Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI
Partial Class Voucher_Tran
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

                txtForDate.SelectedDate = DateAndTime.Now
                txtToDate.SelectedDate = DateAndTime.Now

                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("Voucher_Tran:Page_Load" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))

            Dim dt As DataTable = objclsIssueVoucherMaster.SelectAll_Tran()
            If dt.Rows.Count > 0 Then
                rdvoucher.DataSource = dt
                rdvoucher.DataBind()
            Else
                rdvoucher.DataSource = String.Empty
                rdvoucher.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Voucher_Tran:bindGrid" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Try
            'bindGrid()

            If txtForDate.SelectedDate Is Nothing Then
                objclsIssueVoucherMaster.start_date = DateAndTime.Now
            Else
                objclsIssueVoucherMaster.start_date = txtForDate.SelectedDate
            End If

            If txtToDate.SelectedDate Is Nothing Then
                objclsIssueVoucherMaster.end_date = DateAndTime.Now
            Else
                objclsIssueVoucherMaster.end_date = txtToDate.SelectedDate
            End If

            objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))

            Dim dt As DataTable = objclsIssueVoucherMaster.SelectDateWise_Tran()
            If dt.Rows.Count > 0 Then
                rdvoucher.DataSource = dt
                rdvoucher.DataBind()
            Else
                rdvoucher.DataSource = String.Empty
                rdvoucher.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Voucher_Tran:bindGrid" + ex.Message)
        End Try
    End Sub
    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try

            txtToDate.SelectedDate = txtForDate.SelectedDate

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkClear_Click(sender As Object, e As EventArgs)
        Try

            txtForDate.SelectedDate = DateAndTime.Now
            txtToDate.SelectedDate = DateAndTime.Now

            bindGrid()

        Catch ex As Exception

        End Try
    End Sub
End Class
