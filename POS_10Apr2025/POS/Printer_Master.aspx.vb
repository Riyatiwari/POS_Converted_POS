Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Printer_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPrinter As New Controller_clsPrinter()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("printer_id") = Nothing Then
                    BindPrinter()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Printer_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindPrinter()
        Try
            oclsPrinter.cmp_id = Val(Session("cmp_id"))
            oclsPrinter.printer_id = Val(Session("printer_id"))
            Dim dtPrinter As DataTable = oclsPrinter.Select()
            If dtPrinter.Rows.Count > 0 Then
                txtPName.Text = dtPrinter.Rows(0)("name").ToString
                txtPAlias.Text = dtPrinter.Rows(0)("alias").ToString
                If dtPrinter.Rows(0)("is_Active").ToString = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

                If dtPrinter.Rows(0)("OrderFlag").ToString = "Yes" Then
                    chkOrderFlag.Checked = True
                Else
                    chkOrderFlag.Checked = False
                End If

                rbprintcondiments.SelectedValue = Val(dtPrinter.Rows(0)("is_condiment_small_large"))
                rbprintproduct.SelectedValue = Val(dtPrinter.Rows(0)("is_product_small_large"))
                If dtPrinter.Rows(0)("Group by") = "Yes" Then
                    chkgroupby.Checked = True

                Else
                    chkgroupby.Checked = False
                End If
                If chkgroupby.Checked = True Then
                    divgroupby.Visible = True
                    ddlgroupby.SelectedValue = dtPrinter.Rows(0)("group_by_with")
                End If
                If dtPrinter.Rows(0)("consile date") = "Yes" Then
                    chkdate.Checked = True
                Else
                    chkdate.Checked = False
                End If

                If dtPrinter.Rows(0)("Port").ToString() = "Yes" Then
                    chkDefaultPrinter.Checked = True
                Else
                    chkDefaultPrinter.Checked = False
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Printer_Master:BindPrinter:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("printer_id") = Nothing Then
                Clear()
            Else
                BindPrinter()
            End If
        Catch ex As Exception
            LogHelper.Error("Printer_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Printer_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Printer_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtPName.Text = ""
            txtPAlias.Text = ""
            chkActive.Checked = True
            chkActive.Checked = False
            chkOrderFlag.Checked = False
            rbprintcondiments.ClearSelection()
            rbprintproduct.ClearSelection()
        Catch ex As Exception
            LogHelper.Error("Printer_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsPrinter.cmp_id = Val(Session("cmp_id"))
            oclsPrinter.name = txtPName.Text.Trim()
            oclsPrinter.palias = txtPAlias.Text
            oclsPrinter.Is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsPrinter.OrderFlag = IIf(chkOrderFlag.Checked = True, 1, 0)
            oclsPrinter.Ip_address = Request.UserHostAddress
            oclsPrinter.login_id = Val(Session("login_id"))
            oclsPrinter.machine_id = 0
            oclsPrinter.network_type = ""
            oclsPrinter.budrate = ""
            oclsPrinter.device_name = ""
            oclsPrinter.vender_id = 0
            oclsPrinter.printer_ip_address = ""
            oclsPrinter.port = IIf(chkDefaultPrinter.Checked = True, 1, 0)
            oclsPrinter.is_product_small_large = Val(rbprintproduct.SelectedItem.Value)
            oclsPrinter.is_condiment_small_large = Val(rbprintcondiments.SelectedItem.Value)
            If chkgroupby.Checked = True Then
                oclsPrinter.group_by = 1
                oclsPrinter.group_by_with = ddlgroupby.SelectedValue
            Else
                oclsPrinter.group_by = 0
                oclsPrinter.group_by_with = 0
            End If
            If chkdate.Checked = True Then
                oclsPrinter.consile_date = 1
            Else
                oclsPrinter.consile_date = 0
            End If
            If Session("printer_id") = Nothing Then
                oclsPrinter.printer_id = 0
                oclsPrinter.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsPrinter.printer_id = Val(Session("printer_id"))
                oclsPrinter.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("printer_id") = Nothing
            Response.Redirect("Printer_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Printer_Master:btnSave_Click:" + ex.Message)
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
            LogHelper.Error("Printer_Master:chkgroupby_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtPName_TextChanged(sender As Object, e As EventArgs)
        Try

            If txtPName.Text.ToUpper().Trim() = "PRINTER" Or txtPName.Text.ToUpper().Trim() = "RECEIPT PRINTER" Then

                chkDefaultPrinter.Checked = True
            Else
                chkDefaultPrinter.Checked = False

            End If
        Catch ex As Exception
            LogHelper.Error("Printer_Master:txtPName_TextChanged:" + ex.Message)
        End Try
    End Sub
End Class



