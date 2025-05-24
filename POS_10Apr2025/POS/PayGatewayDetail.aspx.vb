Imports System.Web.Services
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing
Imports System.Data

Partial Class PayGatewayDetail
    Inherits System.Web.UI.Page


    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            If Session("refdt") IsNot Nothing Then

                Dim dt As DataTable = DirectCast(Session("refdt"), DataTable)

                If (dt.Rows.Count > 0) Then
                    Dim D As DateTime = Convert.ToDateTime(dt.Rows(0)("Date").ToString())

                    lblDate.Text = D.ToString("dd/MM/yyyy")
                    lblTime.Text = dt.Rows(0)("Time").ToString()
                    lblRef.Text = dt.Rows(0)("Ref").ToString()
                    lblRltdTran.Text = dt.Rows(0)("RelatedTransaction").ToString()
                    lblAcquirerRef.Text = dt.Rows(0)("AcquirerRef").ToString()
                    lblType.Text = dt.Rows(0)("Type").ToString()
                    lblClass.Text = dt.Rows(0)("Class").ToString()

                    lblCurrency.Text = dt.Rows(0)("Currency").ToString()
                    lblAmount.Text = dt.Rows(0)("Amount").ToString()
                    lblCard.Text = dt.Rows(0)("Card").ToString()
                    lblCardType.Text = dt.Rows(0)("Card_Type").ToString()
                    lblCardCountry.Text = dt.Rows(0)("CardCountry").ToString()
                    lblRegion.Text = dt.Rows(0)("Region").ToString()
                    lblExpiry.Text = dt.Rows(0)("Expiry").ToString()

                    lblAuthCode.Text = dt.Rows(0)("AuthCode").ToString()
                    lblSettleStatus.Text = dt.Rows(0)("SettleStatus").ToString()
                    lblAuthMessage.Text = dt.Rows(0)("AuthMessage").ToString()
                    lblAuthStatus.Text = dt.Rows(0)("AuthStatus").ToString()

                    Session("refdt") = ""
                End If

            End If



        End If
    End Sub

End Class
