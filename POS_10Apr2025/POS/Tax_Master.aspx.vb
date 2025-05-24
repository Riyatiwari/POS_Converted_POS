Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Tax_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsTax As New Controller_clsTax()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("Tax_id") = Nothing Then
                    BindTax()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Tax_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindTax()
        Try
            oclsTax.Tax_id = Val(Session("Tax_id"))
            oclsTax.cmp_id = Val(Session("cmp_id"))
            oclsTax.Login_id = Val(Session("login_id"))
            Dim dtTax As DataTable = oclsTax.Select()
            If dtTax.Rows.Count > 0 Then
                txtName.Text = dtTax.Rows(0)("Name").ToString
                ddMode.ClearSelection()
                ddMode.SelectedValue = dtTax.Rows(0)("Mode").ToString
                If ddMode.SelectedValue = "%" Then
                    txtValue.Visible = True
                    txtValue1.Visible = False
                Else
                    txtValue1.Visible = True
                    txtValue.Visible = False
                End If
                txtValue1.Text = dtTax.Rows(0)("Value").ToString
                txtValue.Text = dtTax.Rows(0)("Value").ToString
                txtEffDate.SelectedDate = dtTax.Rows(0)("Effective_Date").ToString()

                If dtTax.Rows(0)("is_Active").ToString = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Tax_master:BindTax:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Tax_id") = Nothing Then
                Clear()
            Else
                BindTax()
            End If
        Catch ex As Exception
            LogHelper.Error("Tax_master:btnReset_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Tax_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Tax_master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtName.Text = ""
            txtValue.Text = ""
            txtValue1.Text = ""
            ddMode.SelectedValue = 0
            chkActive.Checked = True
            txtEffDate.SelectedDate = Nothing
        Catch ex As Exception
            LogHelper.Error("Tax_master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsTax.cmp_id = Val(Session("cmp_id"))
            oclsTax.Name = txtName.Text.Trim()
            'oclsTax.Value = txtValue.Text

            If ddMode.SelectedItem.Value.ToString() = "SELECT" Then
                oclsTax.Mode = ""
            Else
                oclsTax.Mode = ddMode.SelectedValue.ToString
            End If
            If ddMode.SelectedItem.Value.ToString() = "%" Then
                oclsTax.Value = Val(txtValue.Text)
            Else
                oclsTax.Value = Val(txtValue1.Text)
            End If

            oclsTax.Is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsTax.Ip_address = Request.UserHostAddress
            oclsTax.machine_id = 0

            If txtEffDate.SelectedDate IsNot Nothing Then
                oclsTax.Effective_Date = txtEffDate.SelectedDate.ToString()
            Else
                oclsTax.Effective_Date = ""
            End If
            oclsTax.Login_id = Val(Session("login_id"))
            If Session("Tax_id") = Nothing Then
                oclsTax.Tax_id = 0
                oclsTax.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsTax.Tax_id = Val(Session("Tax_id"))
                oclsTax.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Tax_id") = Nothing
            Response.Redirect("Tax_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Tax_master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddMode_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddMode.SelectedValue.ToString() = "%" Then
                txtValue.Visible = True
                txtValue1.Visible = False
            ElseIf ddMode.SelectedValue.ToString() = "Amt" Then
                txtValue.Visible = False
                txtValue1.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

