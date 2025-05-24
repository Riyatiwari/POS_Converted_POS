Imports AuthorizeNet
Imports System.Security.Cryptography 'required for fingerprint calculation
Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Public Class Booking_Confirm_Process
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oDbConnection As New DBConnection()

        Dim dtAccount As DataSet = oDbConnection.SelectData("select ad.firstname,ad.lastname,b.accountid,adds.email1st,adds.mobile,adds.Street_1 from Booking_Initiate b inner join account ad on ad.accountid = b.accountid inner join address adds on adds.addressid = ad.addressid where bookingref = '" + Request.QueryString("User_Id") + "'")
        Dim query As DataSet = oDbConnection.SelectData("select * FROM M_Payment_Gateway WHERE Gateway = 'Authorize.net'")

        simForm.Action = query.Tables(0).Rows(0)("URL").ToString
        
        ' start by setting the static values
        Dim loginID = query.Tables(0).Rows(0)("LoginID").ToString '"My-9Tr93wEw" '869AfjrU7p
        Dim transactionKey = query.Tables(0).Rows(0)("TransactionKey").ToString '"My-8Jvq6KdK444999UT" '3Z65rA77JmbKsc5T
        Dim amount As String
        Dim User_Id As String
        Dim description = Session("description_final").ToString
        ' The is the label on the 'submit' button
        Dim label = " "
        Dim testMode = "false"

        ' If an amount or description were posted to this page, the defaults are overidden
        If Request.Form("amount") <> "" Then
            amount = Request.Form("amount")
        End If
        If dtAccount.Tables(0).Rows(0)("AccountId").ToString <> "" Then
            User_Id = dtAccount.Tables(0).Rows(0)("AccountId").ToString
        End If

        If dtAccount.Tables(0).Rows(0)("firstName") <> "" Then
            x_first_name.Value = dtAccount.Tables(0).Rows(0)("FirstName").ToString
        End If
        If dtAccount.Tables(0).Rows(0)("LastName") <> "" Then
            x_last_name.Value = dtAccount.Tables(0).Rows(0)("LastName")
        End If
        If dtAccount.Tables(0).Rows(0)("email1st") <> "" Then
            x_email.Value = dtAccount.Tables(0).Rows(0)("email1st")
        End If
        If dtAccount.Tables(0).Rows(0)("mobile") <> "" Then
            x_phone.Value = dtAccount.Tables(0).Rows(0)("mobile")
        End If
        If dtAccount.Tables(0).Rows(0)("Street_1") <> "" Then
            x_address.Value = dtAccount.Tables(0).Rows(0)("Street_1")
        End If
        If Request.Form("description") <> "" Then
            description = Session("description_final")
        End If

        ' also check to see if the amount or description were sent using the GET method
        If Request.QueryString("amount") <> "" Then
            amount = Request.QueryString("amount")
        End If
       
        If Request.QueryString("description") <> "" Then
            description = Request.QueryString("description")
        End If

        ' an invoice is generated using the date and time
        Dim invoice = Request.QueryString("User_Id")  ' DateTime.Now.ToString("yyyyMMddHHmmss")

        ' a sequence number is randomly generated
        Dim random As New Random
        Dim sequence = random.Next(0, 1000)

        ' a time stamp is generated (using a function from simlib.asp)
        Dim timeStamp = CInt((DateTime.UtcNow - New DateTime(1970, 1, 1)).TotalSeconds)

        'generate a fingerprint
        Dim fingerprint = HMAC_MD5(transactionKey, loginID & "^" & sequence & "^" & timeStamp & "^" & amount & "^")

        'Print the Amount and Description to the page by placing them in the Spans
        'amountSpan.InnerHtml = amount
        'descriptionSpan.InnerHtml = description

        'Update the fields in the actual form 
        x_login.Value = loginID
        x_amount.Value = amount
        x_description.Value = description
        buttonLabel.Value = label
        x_test_request.Value = testMode
        x_invoice_num.Value = invoice
        x_fp_sequence.Value = sequence
        x_fp_timestamp.Value = timeStamp
        x_fp_hash.Value = fingerprint
        x_cust_id.Value = User_Id


        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Auto_Submit", "document.simForm.submit();", True)

    End Sub

    'This is a wrapper for the VB.NET's built-in HMACMD5 functionality
    'This function takes the data and key as strings and returns the hash as a hexadecimal value
    Function HMAC_MD5(ByVal Key, ByVal Value)
        ' The first two lines take the input values and convert them from strings to Byte arrays
        Dim HMACkey() As Byte = (New ASCIIEncoding()).GetBytes(Key)
        Dim HMACdata() As Byte = (New ASCIIEncoding()).GetBytes(Value)

        ' create a HMACMD5 object with the key set
        Dim myhmacMD5 As New HMACMD5(HMACkey)

        ' calculate the hash (returns a byte array)
        Dim HMAChash() As Byte = myhmacMD5.ComputeHash(HMACdata)

        ' loop through the byte array and add append each piece to a string to obtain a hash string
        Dim fingerprint = ""
        For i = 0 To HMAChash.Length - 1
            fingerprint &= HMAChash(i).ToString("x").PadLeft(2, "0")
        Next

        Return fingerprint
    End Function

End Class