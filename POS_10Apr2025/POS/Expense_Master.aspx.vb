Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Expense_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsExpenseMaster As New Controller_clsExpenseMaster()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("Exp_id") = Nothing Then
                    BindExpense()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Expense_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsExpenseMaster.name = txexpense.Text.Trim()
            oclsExpenseMaster.Ip_address = Request.UserHostAddress
            oclsExpenseMaster.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsExpenseMaster.is_expense = rbExpense.SelectedValue
            If Session("Exp_id") = Nothing Then
                oclsExpenseMaster.Exp_id = 0
                oclsExpenseMaster.insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsExpenseMaster.Exp_id = Val(Session("Exp_id"))
                oclsExpenseMaster.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Exp_id") = Nothing
            Response.Redirect("Expense_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Expense_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Expense_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Expense_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txexpense.Text = ""
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Expense_Master:Clear" + ex.Message)
        End Try
    End Sub

    Private Sub BindExpense()
        Try
            oclsExpenseMaster.Exp_id = Val(Session("Exp_id"))

            Dim dt As DataTable = oclsExpenseMaster.Select()
            If dt.Rows.Count > 0 Then
                txexpense.Text = dt.Rows(0)("name").ToString
                rbExpense.SelectedValue = Val(dt.Rows(0)("is_expense").ToString())
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Expense_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub
End Class
