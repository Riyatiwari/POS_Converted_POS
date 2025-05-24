Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Partial Class Credit_Account_Payment_Master
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCredit As New Controller_clsCreditAccount


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                bind_ddl()
                txtCreditDate.SelectedDate = System.DateTime.Now


            End If
        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_Master:Page_Load:" + ex.Message)
        End Try

    End Sub

    Public Sub bind_ddl()
        Try
            oclsCredit.cmp_id = Session("cmp_id")

            Dim dt As DataTable = oclsCredit.SelectAccount()
            If (dt.Rows.Count > 0) Then

                RadAccount.DataSource = dt
                RadAccount.DataTextField = "CustomerName"
                RadAccount.DataValueField = "customer_id"
                RadAccount.DataBind()
                Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
                RadAccount.Items.Insert(0, li)

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Credit_Account_Payment_Master:bind_ddl" + ex.Message)
        End Try

    End Sub

    Protected Sub RadAccount_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If RadAccount.SelectedIndex = 0 Then
                txtMobileNo.Text = ""
            Else
                oclsCredit.cmp_id = Session("cmp_id")
                oclsCredit.customer_web_id = Val(RadAccount.SelectedIndex)
                Dim dt As DataTable = oclsCredit.Get_CustDetail()
                If (dt.Rows.Count > 0) Then
                    txtMobileNo.Text = dt.Rows(0)("Mobile")
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_Master:RadAccount_SelectedIndexChanged" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            oclsCredit.cmp_id = Val(Session("cmp_id"))
            oclsCredit.customer_web_id = Val(IIf(RadAccount.SelectedValue = "", 0, RadAccount.SelectedValue))
            oclsCredit.customer_mobile_no = txtMobileNo.Text
            oclsCredit.Amount = txtAmount.Text
            If txtCreditDate.SelectedDate IsNot Nothing Then
                oclsCredit.Credit_date = txtCreditDate.SelectedDate.ToString()
            Else
                oclsCredit.Credit_date = ""
            End If

            If Session("transcation_id") = Nothing Then
                oclsCredit.transcation_id = 0
                oclsCredit.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsCredit.transcation_id = Val(Session("transcation_id"))
                oclsCredit.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("transcation_id") = Nothing
            Response.Redirect("Credit_Account_Payment_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Credit_Account_Payment_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("transcation_id") = Nothing Then
                Clear()
            Else
                bind_ddl()
                txtCreditDate.SelectedDate = System.DateTime.Now

            End If
        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_Master:btnReset_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Credit_Account_Payment_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try
            RadAccount.SelectedValue = 0
            txtMobileNo.Text = ""
            txtCreditDate.SelectedDate = System.DateTime.Now
            txtAmount.Text = ""

        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_Master:Clear" + ex.Message)
        End Try

    End Sub


End Class
