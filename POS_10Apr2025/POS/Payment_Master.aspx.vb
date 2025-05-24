Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Payment_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPayment As New Controller_clsPayment()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("Payment_id") = Nothing Then
                    BindPayment()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Payment_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindPayment()
        Try
            oclsPayment.cmp_id = Val(Session("cmp_id"))
            oclsPayment.Payment_id = Val(Session("Payment_id"))
            Dim dtPayment As DataTable = oclsPayment.Select()
            If dtPayment.Rows.Count > 0 Then
                txtPName.Text = dtPayment.Rows(0)("Name").ToString
                txthostname.Text = dtPayment.Rows(0)("Hostname").ToString
                txtpassword.Text = dtPayment.Rows(0)("Password").ToString
                hfpass.Value = txtpassword.Text
                txtcurrencycode.Text = dtPayment.Rows(0)("CurrencyCode").ToString
                ddTransactionType.ClearSelection()
                ddTransactionType.SelectedValue = dtPayment.Rows(0)("TransactionType").ToString

                If dtPayment.Rows(0)("Active").ToString = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Payment_Master:BindPayment:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Payment_id") = Nothing Then
                Clear()
            Else
                BindPayment()
            End If
        Catch ex As Exception
            LogHelper.Error("Payment_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Payment_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Payment_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtPName.Text = ""
            txthostname.Text = ""
            txtcurrencycode.Text = ""
            txtpassword.Text = ""
            ddTransactionType.ClearSelection()
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Payment_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
          
            If hfpass.Value = "" And txtpassword.Text = "" Then
                If Not txtpassword.Text = "" Then
                    rqPass.Enabled = False
                Else
                    rqPass.Enabled = True
                    Exit Sub
                End If
            Else
                rqPass.Enabled = False
            End If

            oclsPayment.cmp_id = Val(Session("cmp_id"))
            oclsPayment.Name = txtPName.Text.Trim()
            oclsPayment.Hostname = txthostname.Text
            oclsPayment.Password = IIf(txtpassword.Text = "", hfpass.Value, txtpassword.Text)
            oclsPayment.CurrencyCode = Val(txtcurrencycode.Text)
            oclsPayment.TransactionType = ddTransactionType.SelectedItem.Text
            oclsPayment.Is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsPayment.Ip_address = Request.UserHostAddress
            oclsPayment.login_id = Val(Session("login_id"))

            If Session("Payment_id") = Nothing Then
                oclsPayment.Payment_Id = 0
                oclsPayment.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsPayment.Payment_Id = Val(Session("Payment_id"))
                oclsPayment.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Payment_id") = Nothing
            Response.Redirect("Payment_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Payment_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
End Class
