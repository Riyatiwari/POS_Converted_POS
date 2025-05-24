
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Partial Class Credit_Account_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCredit As New Controller_clsCreditAccount


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Credit_Account_List:Page_Load" + ex.Message)
        End Try
    End Sub


    Public Sub bindGrid()
        Try

            oclsCredit.start_date = System.DateTime.Now
            oclsCredit.end_date = System.DateTime.Now
            Dim dtCustomer As DataTable = oclsCredit.SelectAll()

            If dtCustomer.Rows.Count > 0 Then
                rdCustomer.DataSource = dtCustomer
                rdCustomer.DataBind()
            Else
                rdCustomer.DataSource = String.Empty
                rdCustomer.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Credit_Account_List:bindGrid" + ex.Message)
        End Try

    End Sub

End Class
