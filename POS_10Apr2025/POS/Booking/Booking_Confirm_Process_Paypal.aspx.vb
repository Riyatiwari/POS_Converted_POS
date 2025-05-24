Imports System.Data
Imports System.Net.Mail
Imports System.IO
Imports RedirectToPaypal

Partial Class BookingEasy_Booking_Confirm_Process_Paypal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim oDbConnection As New DBConnection()

        Dim dtAccount As DataSet = oDbConnection.SelectData("select ad.firstname,ad.lastname,b.accountid,adds.email1st,adds.Mobile,adds.Street_1,adds.city,adds.pcode from Booking_Initiate b inner join account ad on ad.accountid = b.accountid inner join address adds on adds.addressid = ad.addressid where bookingref = '" + Request.QueryString("User_Id") + "'")
        Dim query As DataSet = oDbConnection.SelectData("select * FROM M_Payment_Gateway WHERE Gateway = 'Paypal'")


        Dim itemName As String
        If Not String.IsNullOrEmpty(Session("description_final")) Then
            itemName = Session("description_final").ToString
        Else
            itemName = "-"
        End If
        '"xyzasdlkjasdlkn"
        Dim itemPrice As String = Request.QueryString("amount") '"100"
        Dim uEmail As String = query.Tables(0).Rows(0)("LoginID").ToString '"zankar.vora@gmail.com"
        Dim iURL As String = query.Tables(0).Rows(0)("URL").ToString '"https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business="
        Dim icurrency As String = query.Tables(0).Rows(0)("Currency").ToString
        Dim ireturn As String = query.Tables(0).Rows(0)("ReturnURL").ToString
        Dim icancel As String = query.Tables(0).Rows(0)("CancelURL").ToString

        Dim x_first_name As String = dtAccount.Tables(0).Rows(0)("FirstName").ToString

        Dim x_last_name As String = dtAccount.Tables(0).Rows(0)("LastName")

        Dim x_email As String = dtAccount.Tables(0).Rows(0)("email1st")

        Dim x_phone As String = dtAccount.Tables(0).Rows(0)("Mobile")

        Dim x_address As String = dtAccount.Tables(0).Rows(0)("Street_1")

        Dim x_invoice As String = Request.QueryString("User_Id").ToString()

        Dim responseURL As String = RedirectToPaypal.getItemNameAndCost(itemName, itemPrice, uEmail, iURL, icurrency, ireturn, icancel, x_first_name, x_last_name, x_email, x_address, x_invoice, x_phone)

        Response.Redirect(responseURL)

        'Dim oDbConnection As New DBConnection()

        'Dim dtAccount As DataSet = oDbConnection.SelectData("select ad.firstname,ad.lastname,b.accountid,adds.email1st,adds.Mobile,adds.Street_1,adds.city,adds.pcode from Booking_Initiate b inner join account ad on ad.accountid = b.accountid inner join address adds on adds.addressid = ad.addressid where bookingref = '" + Request.QueryString("User_Id") + "'")
        'Dim query As DataSet = oDbConnection.SelectData("select * FROM M_Payment_Gateway WHERE Gateway = 'Paypal'")

        'If dtAccount.Tables(0).Rows.Count > 0 Then
        '    Dim itemName As String
        '    If Not String.IsNullOrEmpty(Session("description_final")) Then
        '        itemName = Session("description_final").ToString
        '    Else
        '        itemName = "-"
        '    End If
        '    '"xyzasdlkjasdlkn"
        '    Dim itemPrice As String = Request.QueryString("amount") '"100"
        '    Dim uEmail As String = query.Tables(0).Rows(0)("LoginID").ToString '"zankar.vora@gmail.com"
        '    Dim iURL As String = query.Tables(0).Rows(0)("URL").ToString '"https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business="
        '    Dim icurrency As String = query.Tables(0).Rows(0)("Currency").ToString
        '    Dim ireturn As String = query.Tables(0).Rows(0)("ReturnURL").ToString
        '    Dim icancel As String = query.Tables(0).Rows(0)("CancelURL").ToString

        '    Dim x_first_name As String = dtAccount.Tables(0).Rows(0)("FirstName").ToString

        '    Dim x_last_name As String = dtAccount.Tables(0).Rows(0)("LastName")

        '    Dim x_email As String = dtAccount.Tables(0).Rows(0)("email1st")

        '    Dim x_phone As String = dtAccount.Tables(0).Rows(0)("Mobile")

        '    Dim x_address As String = dtAccount.Tables(0).Rows(0)("Street_1")

        '    Dim x_invoice As String = Request.QueryString("User_Id").ToString()

        '    Dim responseURL As String = RedirectToPaypal.getItemNameAndCost(itemName, itemPrice, uEmail, iURL, icurrency, ireturn, icancel, x_first_name, x_last_name, x_email, x_address, x_invoice, x_phone)

        '    Response.Redirect(responseURL)
        'Else
        '    Dim dtAccountRoom As DataSet = oDbConnection.SelectData("select ad.firstname,ad.lastname,b.accountid,adds.email1st,adds.Mobile,adds.Street_1,adds.city,adds.pcode  from bookings b inner join account ad on ad.accountid = b.accountid inner join address adds on adds.addressid = ad.addressid where b.bookingref = '" + Request.QueryString("User_Id") + "'")
        '    Dim itemName As String
        '    If Not String.IsNullOrEmpty(Session("description_final")) Then
        '        itemName = Session("description_final").ToString
        '    Else
        '        itemName = "-"
        '    End If
        '    '"xyzasdlkjasdlkn"
        '    Dim itemPrice As String = Request.QueryString("amount") '"100"
        '    Dim uEmail As String = query.Tables(0).Rows(0)("LoginID").ToString '"zankar.vora@gmail.com"
        '    Dim iURL As String = query.Tables(0).Rows(0)("URL").ToString '"https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business="
        '    Dim icurrency As String = query.Tables(0).Rows(0)("Currency").ToString
        '    Dim ireturn As String = query.Tables(0).Rows(0)("ReturnURL").ToString
        '    Dim icancel As String = query.Tables(0).Rows(0)("CancelURL").ToString

        '    Dim x_first_name As String = dtAccountRoom.Tables(0).Rows(0)("FirstName").ToString

        '    Dim x_last_name As String = dtAccountRoom.Tables(0).Rows(0)("LastName")

        '    Dim x_email As String = dtAccountRoom.Tables(0).Rows(0)("email1st")

        '    Dim x_phone As String = dtAccountRoom.Tables(0).Rows(0)("Mobile")

        '    Dim x_address As String = dtAccountRoom.Tables(0).Rows(0)("Street_1")

        '    Dim x_invoice As String = Request.QueryString("User_Id").ToString()

        '    Dim responseURL As String = RedirectToPaypal.getItemNameAndCost(itemName, itemPrice, uEmail, iURL, icurrency, ireturn, icancel, x_first_name, x_last_name, x_email, x_address, x_invoice, x_phone)

        '    Response.Redirect(responseURL)
        'End If
    End Sub

End Class
