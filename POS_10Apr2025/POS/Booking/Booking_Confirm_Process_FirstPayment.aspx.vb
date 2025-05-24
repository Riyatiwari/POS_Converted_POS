Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Class Booking_Confirm_Process_FirstPayment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim oDbConnection As New DBConnection()

        Dim dtAccount As DataSet = oDbConnection.SelectData("select ad.firstname,ad.lastname,b.accountid,adds.email1st,adds.mobile,adds.Street_1,adds.PCode from Booking_Initiate b inner join account ad on ad.accountid = b.accountid inner join address adds on adds.addressid = ad.addressid where bookingref = '" + Request.QueryString("User_Id") + "'")
        Dim query As DataSet = oDbConnection.SelectData("select * FROM M_Payment_Gateway WHERE Gateway = 'FirstPayment'")

        'CType(Me.FindControl("idname"), HtmlInputHidden).Value = query.Tables(0).Rows(0)("LoginID").ToString
        Form1.Action = query.Tables(0).Rows(0)("URL").ToString '"https://gateway.firstpayments.co.uk/paymentform/"
        Dim price As String
        If Request.QueryString("amount") <> "" Then
            price = Request.QueryString("amount")
        End If
        Dim a As Integer
        a = price.Replace(".", "")
        amount.Value = a
        merchantID.Value = query.Tables(0).Rows(0)("LoginID").ToString '"101118"
        countryCode.Value = query.Tables(0).Rows(0)("Currency").ToString '"826"
        currencyCode.Value = query.Tables(0).Rows(0)("Currency").ToString '"826"
        transactionUnique.Value = Request.QueryString("User_Id").ToString()
        orderRef.Value = Session("description_final").ToString
        redirectURL.Value = query.Tables(0).Rows(0)("ReturnURL").ToString '"http://localhost:52985/BookingEasy/BookingEasy/TableBookConfime_FirstPayment.aspx"
        signature.Value = "123"
        customerEmail.Value = dtAccount.Tables(0).Rows(0)("email1st") '"zanar.vora@gmail.com"
        customerPhone.Value = dtAccount.Tables(0).Rows(0)("mobile") '"9974465823"
        customerAddress.Value = dtAccount.Tables(0).Rows(0)("Street_1") '"7/78 pooja apt"
        customerPostCode.Value = dtAccount.Tables(0).Rows(0)("PCode") '"380015"
    End Sub
End Class
