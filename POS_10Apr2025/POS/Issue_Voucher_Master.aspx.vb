Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Issue_Voucher_Master

    Inherits BaseClass

    Dim objclsIssueVoucherMaster As New Controller_clsIssueVoucher()
    Dim objclsVoucherMaster As New Controller_clsVoucher()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtrefno.Enabled = False
                AccountBind()
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("Issuevoucher_id") = Nothing Then
                    BindIssueVaucher()
                Else
                    GenerateRefNO()
                    'txtissuetime.SelectedDate = DateAndTime.Now
                    txtstarttime.SelectedDate = DateAndTime.Now
                    txtenddate.SelectedDate = DateAndTime.Now
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GenerateRefNO()

        objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))
        objclsIssueVoucherMaster.Issuevoucher_id = 0
        Dim dt As DataTable = objclsIssueVoucherMaster.GetRefno()
        If (dt.Rows.Count > 0) Then

            txtrefno.Text = dt.Rows(0)("ref_no").ToString()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("Something Went Wrong").ToString() + "');", True)
        End If
    End Sub
    Private Sub BindIssueVaucher()
        Try

            objclsIssueVoucherMaster.Issuevoucher_id = Val(Session("Issuevoucher_id"))

            objclsIssueVoucherMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsIssueVoucherMaster.Select()
            If dt.Rows.Count > 0 Then
                RadTxtdepamount.Text = dt.Rows(0)("deposit_amount").ToString()

                RadAccount.ClearSelection()
                RadAccount.SelectedValue = dt.Rows(0)("account")

                RadVoucher.ClearSelection()
                RadVoucher.SelectedValue = dt.Rows(0)("Voucher_id")
                txtissuetime.SelectedDate = dt.Rows(0)("issue_datetime")
                txtrefno.Text = dt.Rows(0)("ref_no")

                Session("voucher_refno") = dt.Rows(0)("ref_no").ToString()

                If dt.Rows(0)("start_date").ToString() Is Nothing Then
                    txtstarttime.SelectedDate = DateAndTime.Now
                Else
                    txtstarttime.SelectedDate = dt.Rows(0)("start_date")
                End If
                If dt.Rows(0)("end_date").ToString() Is Nothing Then
                    txtenddate.SelectedDate = DateAndTime.Now
                Else
                    txtenddate.SelectedDate = dt.Rows(0)("end_date")
                End If


                radDuration.SelectedValue = dt.Rows(0)("voucher_duration").ToString()

                If dt.Rows(0)("voucher_duration").ToString() = "Endless" Then
                    end_date.Visible = False
                Else
                    end_date.Visible = True
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Issue_Voucher_Master:BindProfile:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            objclsIssueVoucherMaster.cmp_id = Session("cmp_id")

            If RadAccount.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please select account');", True)
                Return
            Else
                objclsIssueVoucherMaster.account = RadAccount.SelectedValue
            End If
            objclsIssueVoucherMaster.Voucher_id = RadVoucher.SelectedValue
            objclsIssueVoucherMaster.deposit_amount = Val(IIf(RadTxtdepamount.Text = "", 0, RadTxtdepamount.Text))

            If txtissuetime.SelectedDate Is Nothing Then
                objclsIssueVoucherMaster.issue_datetime = DateAndTime.Now
            Else
                objclsIssueVoucherMaster.issue_datetime = txtissuetime.SelectedDate
            End If

            objclsIssueVoucherMaster.ref_no = txtrefno.Text
            objclsIssueVoucherMaster.voucher_duration = radDuration.SelectedItem.Value

            If txtstarttime.SelectedDate Is Nothing Then
                objclsIssueVoucherMaster.start_date = DateAndTime.Now
            Else
                objclsIssueVoucherMaster.start_date = txtstarttime.SelectedDate
            End If


            If txtenddate.SelectedDate Is Nothing Then
                objclsIssueVoucherMaster.end_date = DateAndTime.Now
            Else
                objclsIssueVoucherMaster.end_date = txtenddate.SelectedDate
            End If


            If Session("Issuevoucher_id") = Nothing Then
                objclsIssueVoucherMaster.Insert()

                objclsIssueVoucherMaster.Insert_VoucherTran()
                Session("Success") = "Record inserted successfully"
            Else
                objclsIssueVoucherMaster.Issuevoucher_id = Val(Session("Issuevoucher_id"))
                objclsIssueVoucherMaster.Update()

                objclsIssueVoucherMaster.ref_no = Session("voucher_refno")
                objclsIssueVoucherMaster.Update_VoucherTran()

                Session("Success") = "Record updated successfully"
            End If

            Session("Issuevoucher_id") = Nothing
            Response.Redirect("Issue_Voucher_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Issue_Voucher_List:BindVoucher:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)

        RadTxtdepamount.Text = ""
        txtissuetime.SelectedDate = DateAndTime.Now
        RadVoucher.SelectedValue = 0
        RadAccount.SelectedValue = 0

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("Issue_Voucher_List.aspx")

    End Sub
    Protected Sub AccountBind()
        Try
            objclsIssueVoucherMaster.cmp_id = Session("cmp_id")

            Dim dt As DataTable = objclsIssueVoucherMaster.SelectAccount()
            If (dt.Rows.Count > 0) Then

                RadAccount.DataSource = dt
                RadAccount.DataTextField = "CustomerName"
                RadAccount.DataValueField = "customer_id"
                RadAccount.DataBind()
                Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
                RadAccount.Items.Insert(0, li)

            End If

            Dim dtv As DataTable = objclsIssueVoucherMaster.SelectVoucher()
            If (dtv.Rows.Count > 0) Then
                RadVoucher.DataSource = dtv
                RadVoucher.DataTextField = "voucher_name"
                RadVoucher.DataValueField = "voucher_id"
                RadVoucher.DataBind()
                Dim lik As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
                RadVoucher.Items.Insert(0, lik)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RadTxtdepamount_TextChanged(sender As Object, e As EventArgs)
        If System.Text.RegularExpressions.Regex.IsMatch(RadTxtdepamount.Text, "[^0-9]") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please enter only numbers.');", True)
            'RadTxtdepamount.Text = RadTxtdepamount.Text.Remove(RadTxtdepamount.Text.Length - 1)
        End If
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If radDuration.SelectedItem.Value = "Today" Then
                txtenddate.SelectedDate = txtstarttime.SelectedDate

            ElseIf radDuration.SelectedItem.Value = "This Week" Then
                Dim day_last As DateTime = txtstarttime.SelectedDate
                day_last = day_last.AddDays(7 - day_last.DayOfWeek.GetHashCode())
                txtenddate.SelectedDate = day_last
            ElseIf radDuration.SelectedItem.Value = "This Month" Then
                Dim endOfMonth As DateTime = New DateTime(Convert.ToDateTime(txtstarttime.SelectedDate).Year, Convert.ToDateTime(txtstarttime.SelectedDate).Month, DateTime.DaysInMonth(Convert.ToDateTime(txtstarttime.SelectedDate).Year, Today.Month))
                txtenddate.SelectedDate = endOfMonth

            ElseIf radDuration.SelectedItem.Value = "6 Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(6)
            ElseIf radDuration.SelectedItem.Value = "This Year" Then

                Dim lastday_year As DateTime = New DateTime(DateTime.Now.Year, 12, 31)
                txtenddate.SelectedDate = lastday_year

            ElseIf radDuration.SelectedItem.Value = "Endless" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddYears(10)

            End If

            If radDuration.SelectedItem.Value = "Endless" Then
                end_date.Visible = False
            Else
                end_date.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtstarttime_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try
            If radDuration.SelectedItem.Value = "Today" Then
                txtenddate.SelectedDate = txtstarttime.SelectedDate

            ElseIf radDuration.SelectedItem.Value = "This Week" Then
                Dim day_last As DateTime = DateTime.Today
                day_last = day_last.AddDays(7 - day_last.DayOfWeek.GetHashCode())
                txtenddate.SelectedDate = day_last
            ElseIf radDuration.SelectedItem.Value = "This Month" Then
                Dim endOfMonth As DateTime = New DateTime(Convert.ToDateTime(txtstarttime.SelectedDate).Year, Convert.ToDateTime(txtstarttime.SelectedDate).Month, DateTime.DaysInMonth(Convert.ToDateTime(txtstarttime.SelectedDate).Year, Today.Month))
                txtenddate.SelectedDate = endOfMonth

            ElseIf radDuration.SelectedItem.Value = "6 Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(6)
            ElseIf radDuration.SelectedItem.Value = "This Year" Then

                Dim lastday_year As DateTime = New DateTime(DateTime.Now.Year, 12, 31)
                txtenddate.SelectedDate = lastday_year

            ElseIf radDuration.SelectedItem.Value = "Endless" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddYears(10)

            End If

            If radDuration.SelectedItem.Value = "Endless" Then
                end_date.Visible = False
            Else
                end_date.Visible = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub RadVoucher_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            objclsVoucherMaster.cmp_id = Val(Session("cmp_id"))
            objclsVoucherMaster.voucher_id = RadVoucher.SelectedValue
            Dim dt As DataTable = objclsVoucherMaster.Select()
            If dt.Rows.Count > 0 Then
                txtstarttime.SelectedDate = dt.Rows(0)("start_date")
                txtenddate.SelectedDate = dt.Rows(0)("end_date")
                radDuration.SelectedValue = dt.Rows(0)("voucher_duration").ToString()

                If dt.Rows(0)("voucher_duration").ToString() = "Endless" Then
                    end_date.Visible = False
                Else
                    end_date.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
