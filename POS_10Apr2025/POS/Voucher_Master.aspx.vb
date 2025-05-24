Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Voucher_Master

    Inherits BaseClass
    Dim objclsVoucherMaster As New Controller_clsVoucher()

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

                If Not Session("voucher_id") = Nothing Then
                    BindVaucher()

                Else
                    txtstarttime.SelectedDate = DateAndTime.Now
                    txtenddate.SelectedDate = DateAndTime.Now
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Voucher_Master_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Private Sub BindVaucher()
        Try
            Dim sender As Object

            objclsVoucherMaster.cmp_id = Val(Session("cmp_id"))
            objclsVoucherMaster.voucher_id = Val(Session("voucher_id"))
            Dim dt As DataTable = objclsVoucherMaster.Select()
            If dt.Rows.Count > 0 Then
                txtvochername.Text = dt.Rows(0)("voucher_name").ToString

                RadVoucherType.ClearSelection()
                RadVoucherType.SelectedValue = dt.Rows(0)("Voucher_Type")
                txtstarttime.SelectedDate = dt.Rows(0)("start_date")
                txtenddate.SelectedDate = dt.Rows(0)("end_date")
                If dt.Rows(0)("endless").ToString() = 1 Then
                    chk_endless.Checked = True
                Else
                    chk_endless.Checked = False
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
            LogHelper.Error("Allergy_Master:BindProfile:" + ex.Message)
        End Try

    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs)

        txtvochername.Text = ""
        RadVoucherType.ClearSelection()
        txtstarttime.SelectedDate = DateAndTime.Now
        txtenddate.SelectedDate = DateAndTime.Now
        chk_endless.Checked = False
        radDuration.ClearSelection()
        end_date.Visible = True
    End Sub


    Protected Sub RadVoucherType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)

    End Sub

    Protected Sub chk_endless_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnSave_Click1(sender As Object, e As EventArgs)
        Try

            If RadVoucherType.SelectedValue = "" Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Kindly select Voucher Type.');", True)
            Else

                objclsVoucherMaster.cmp_id = Session("cmp_id")
                objclsVoucherMaster.voucher_name = txtvochername.Text
                objclsVoucherMaster.Voucher_Type = IIf(RadVoucherType.SelectedValue = "", "", RadVoucherType.SelectedValue)
                objclsVoucherMaster.voucher_duration = radDuration.SelectedItem.Value

                If txtstarttime.SelectedDate Is Nothing Then
                    objclsVoucherMaster.start_date = DateAndTime.Now
                Else
                    objclsVoucherMaster.start_date = txtstarttime.SelectedDate
                End If


                If txtenddate.SelectedDate Is Nothing Then
                    objclsVoucherMaster.end_date = DateAndTime.Now
                Else
                    objclsVoucherMaster.end_date = txtenddate.SelectedDate
                End If
                objclsVoucherMaster.endless = chk_endless.Checked

                If chk_endless.Checked = True Then
                    objclsVoucherMaster.endless = 1
                Else
                    objclsVoucherMaster.endless = 0
                End If


                If Session("voucher_id") = Nothing Then
                    objclsVoucherMaster.voucher_id = 0
                    objclsVoucherMaster.Insert()
                    Session("Success") = "Record inserted successfully"
                Else
                    objclsVoucherMaster.voucher_id = Val(Session("voucher_id"))
                    objclsVoucherMaster.Update()
                    Session("Success") = "Record updated successfully"
                End If

                Session("voucher_id") = Nothing
                Response.Redirect("Voucher_List.aspx", False)

            End If


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Voucher_Master_Master:BindAllergy:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("Voucher_List.aspx", False)
    End Sub
    Protected Sub txtstarttime_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try
            If radDuration.SelectedItem.Value = "Today" Then
                txtenddate.SelectedDate = txtstarttime.SelectedDate
            ElseIf radDuration.SelectedItem.Value = "This Week" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddDays(7)
            ElseIf radDuration.SelectedItem.Value = "This Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(1)
            ElseIf radDuration.SelectedItem.Value = "6 Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(6)
            ElseIf radDuration.SelectedItem.Value = "This Year" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddYears(1)
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
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If radDuration.SelectedItem.Value = "Today" Then
                txtenddate.SelectedDate = txtstarttime.SelectedDate
            ElseIf radDuration.SelectedItem.Value = "This Week" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddDays(7)
            ElseIf radDuration.SelectedItem.Value = "This Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(1)
            ElseIf radDuration.SelectedItem.Value = "6 Month" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddMonths(6)
            ElseIf radDuration.SelectedItem.Value = "This Year" Then
                txtenddate.SelectedDate = txtstarttime.FocusedDate.AddYears(1)
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
End Class
