Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Partial Class Credit_Account_Payment_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCredit As New Controller_clsCreditAccount

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_List:Page_Load" + ex.Message)
        End Try
    End Sub


    Public Sub bindGrid()
        Try
            Dim dtCustomer As DataTable = oclsCredit.Select_payment()

            If dtCustomer.Rows.Count > 0 Then
                rdCustomer.DataSource = dtCustomer
                rdCustomer.DataBind()
            Else
                rdCustomer.DataSource = String.Empty
                rdCustomer.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Credit_Account_Payment_List:bindGrid" + ex.Message)
        End Try

    End Sub


    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            'Session("product_id") = Nothing
            Response.Redirect("Credit_Account_Payment_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Credit_Account_Payment_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub


    Public Sub rdCustomer_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdCustomer.ItemCommand

        If e.CommandName = "Delete" Then

            oclsCredit.transcation_id = Val(Convert.ToInt32(e.CommandArgument))
            oclsCredit.cmp_id = 0
            oclsCredit.customer_web_id = 0
            oclsCredit.customer_mobile_no = ""
            oclsCredit.Amount = 0.0
            oclsCredit.Credit_date = System.DateTime.Now
            oclsCredit.Tran_Type = "D"

            oclsCredit.Delete()
            Session("Success") = "Record Deleted successfully"
            Response.Redirect("Credit_Account_Payment_List.aspx", False)
        Else

        End If

    End Sub


End Class
