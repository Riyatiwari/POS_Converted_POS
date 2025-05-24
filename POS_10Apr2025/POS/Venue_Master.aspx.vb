Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Venue_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsVenue As New Controller_clsVenue()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("venue_id") = Nothing Then
                    BindVenue()
                Else
                    bindSortingNo()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Venue_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub bindSortingNo()
        Try
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            oclsVenue.form_name = "Venue"
            Dim dtTable As DataTable = oclsVenue.Select1()
            If dtTable.Rows.Count > 0 Then
                txtSortingno.Text = Val(dtTable.Rows(0)("SNO"))
            End If
        Catch ex As Exception
            LogHelper.Error("Venue_Master:bindSortingNo:" + ex.Message)
        End Try
    End Sub
    Private Sub BindVenue()
        Try
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            oclsVenue.venue_id = Val(Session("venue_id"))
            Dim dtVenue As DataTable = oclsVenue.Select()
            If dtVenue.Rows.Count > 0 Then
                txtVName.Text = dtVenue.Rows(0)("venue_name").ToString
                txtdesc.Text = dtVenue.Rows(0)("description").ToString
                txtSortingno.Text = dtVenue.Rows(0)("sorting_no").ToString
                txtdatetime.TextWithLiterals = dtVenue.Rows(0)("date_time").ToString
                If dtVenue.Rows(0)("print_receipt") = "Yes" Then
                    chkprintreceipt.Checked = True
                Else
                    chkprintreceipt.Checked = False
                End If

                If dtVenue.Rows(0)("print_duplicate_receipt") = "Yes" Then
                    chkprintduplicatereceipt.Checked = True
                Else
                    chkprintduplicatereceipt.Checked = False
                End If
                If dtVenue.Rows(0)("Group by") = "Yes" Then
                    chkgroupby.Checked = True
                Else
                    chkgroupby.Checked = False
                End If
                If dtVenue.Rows(0)("consile date") = "Yes" Then
                    chkdate.Checked = True
                Else
                    chkdate.Checked = False
                End If
                If chkgroupby.Checked = True Then
                    divgroupby.Visible = True
                    ddlgroupby.SelectedValue = dtVenue.Rows(0)("group_by_with")
                End If

                If dtVenue.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Venue_Master:BindVenue:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("venue_id") = Nothing Then
                Clear()
            Else
                BindVenue()
            End If
        Catch ex As Exception
            LogHelper.Error("Venue_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Venue_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Venue_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtVName.Text = ""
            txtdesc.Text = ""
            txtdatetime.TextWithLiterals = "00:00"
            chkprintduplicatereceipt.Checked = True
            chkprintreceipt.Checked = True
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Venue_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            oclsVenue.venue_name = txtVName.Text.Trim()
            oclsVenue.description = txtdesc.Text
            oclsVenue.sorting_no = Val(txtSortingno.Text)
            oclsVenue.mac_id = ""
            oclsVenue.print_receipt = IIf(chkprintreceipt.Checked, 1, 0)
            oclsVenue.print_duplicate_receipt = IIf(chkprintduplicatereceipt.Checked, 1, 0)
            oclsVenue.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsVenue.login_id = Val(Session("login_id"))
            oclsVenue.machine_id = 0
            oclsVenue.date_time = txtdatetime.TextWithLiterals
            If chkgroupby.Checked = True Then
                oclsVenue.group_by = 1
                oclsVenue.group_by_with = ddlgroupby.SelectedValue
            Else
                oclsVenue.group_by = 0
                oclsVenue.group_by_with = 0
            End If
            If chkdate.Checked = True Then
                oclsVenue.consile_date = 1
            Else
                oclsVenue.consile_date = 0
            End If
            If Session("venue_id") = Nothing Then
                oclsVenue.venue_id = 0
                oclsVenue.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsVenue.venue_id = Val(Session("venue_id"))
                oclsVenue.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("venue_id") = Nothing
            Response.Redirect("Venue_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Venue_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkgroupby_CheckedChanged(sender As Object, e As EventArgs) Handles chkgroupby.CheckedChanged
        Try
            If chkgroupby.Checked = True Then
                divgroupby.Visible = True
            Else
                divgroupby.Visible = False
            End If
         
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub
End Class
