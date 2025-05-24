
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Security.Authentication

Partial Class RequestPaybyLink
    Inherits System.Web.UI.Page
    Dim oclspaybylink As New Controller_clsPayByLink
    Dim objclsLocation As New clsLocation()
    Dim lid, tid, refid, accoutid As Integer
    Dim cid As Integer
    Dim LinkPay, store, pay_uuid, venue_name, Custemail, uemail, trans_uuid As String
    Dim amount As Double

    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Not IsPostBack = True Then
            amount = Double.Parse(Request.QueryString("Amount").ToString())
            refid = Integer.Parse(Request.QueryString("refid").ToString())
            store = Request.QueryString("store").ToString()
            tid = Integer.Parse(Request.QueryString("tid").ToString())
            accoutid = Integer.Parse(Request.QueryString("accountid").ToString())
            pay_uuid = Request.QueryString("pay_uuid").ToString()
            uemail = Request.QueryString("email").ToString()
            ' trans_uuid = Request.QueryString("trans_uuid").ToString()
            If Not String.IsNullOrEmpty(Request.QueryString("trans_uuid")) Then
                trans_uuid = Request.QueryString("trans_uuid").ToString()
            Else
                trans_uuid = String.Empty
            End If

            Session("store") = store
            CheckStore(store)
            oclspaybylink.pay_uuid = pay_uuid
            oclspaybylink.till_id = tid
            Dim dt As DataTable = oclspaybylink.checkRequest()
            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)(0).ToString() = "0") Then
                    lid = Int32.Parse(dt.Rows(0)("lid").ToString())
                    cid = Int32.Parse(dt.Rows(0)("cvid").ToString())
                    venue_name = dt.Rows(0)("venue_name").ToString()
                    ViewState("locationContact") = dt.Rows(0)("contact").ToString()
                    ViewState("venueName") = venue_name
                    ViewState("locationMail") = dt.Rows(0)("email").ToString()
                    ViewState("cmpname") = dt.Rows(0)("name").ToString()
                    GenerateLink()
                    Save_Request()
                    EmailPayment()
                ElseIf (dt.Rows(0)(0).ToString() = "1") Then
                    lid = Int32.Parse(dt.Rows(0)("lid").ToString())
                    cid = Int32.Parse(dt.Rows(0)("cvid").ToString())
                    venue_name = dt.Rows(0)("venue_name").ToString()
                    ViewState("venueName") = venue_name
                    Custemail = uemail
                    GenerateLink()
                    EmailPayment()
                End If

            End If
        End If
    End Sub
    Public Sub GenerateLink()
        Try

            If Not String.IsNullOrEmpty(trans_uuid) Then
                LinkPay = "http://core.mytenderpos.com/Receipt_payment?table_uuid=" + trans_uuid + "&Amount=" + amount.ToString() + "&refid=" + refid.ToString() + "&store=" + store + "&lid=" + lid.ToString() + "&cv=" + cid.ToString() + "&payuuid=" + pay_uuid.ToString() + "&tid=" + tid.ToString()
            Else
                LinkPay = "https://Live.mytenderpos.com/PaybyLink.aspx?Amount=" + amount.ToString() + "&refid=" + refid.ToString() + "&store=" + store + "&lid=" + lid.ToString() + "&cv=" + cid.ToString() + "&payuuid=" + pay_uuid.ToString() + "&tid=" + tid.ToString()
            End If
            'LinkPay = "http://core.mytenderpos.com/Receipt_payment?table_uuid=" + trans_uuid.ToString() + "&Amount=" + amount.ToString() + "&refid=" + refid.ToString() + "&store=" + store + "&lid=" + lid.ToString() + "&cv=" + cid.ToString() + "&payuuid=" + pay_uuid.ToString() + "&tid=" + tid.ToString()
            'LinkPay = "https://Live.mytenderpos.com/PaybyLink.aspx?Amount=" + amount.ToString() + "&refid=" + refid.ToString() + "&store=" + store + "&lid=" + lid.ToString() + "&cv=" + cid.ToString() + "&payuuid=" + pay_uuid.ToString() + "&tid=" + tid.ToString()
        Catch ex As Exception
            LogHelper.Error("Paybylink:GenerateLink:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Sub Save_Request()
        Try

            oclspaybylink.pay_uuid = pay_uuid
            oclspaybylink.Amount = amount
            oclspaybylink.ref_id = refid
            oclspaybylink.link = LinkPay
            oclspaybylink.account_id = accoutid
            oclspaybylink.till_id = tid
            oclspaybylink.emailid = uemail
            Dim dt As DataTable = oclspaybylink.saveRequest()
            If (dt.Rows.Count > 0) Then
                Custemail = dt.Rows(0)("email")
            ElseIf (uemail <> "") Then
                Custemail = uemail
            End If
        Catch ex As Exception
            LogHelper.Error("Paybylink:Save_Request:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Sub EmailPayment()
        Try
            Dim srtbhtmlreport As StringBuilder = New StringBuilder()




            ''  Dim srtbhtmlreport As StringBuilder = New StringBuilder()
            'srtbhtmlreport.Append("<br/><div style='font-family:verdana;font-size:13px;background-color:beige;padding:5px;border:brown 1px solid;min-height:300px;'> <br/> <br/>")
            'srtbhtmlreport.Append("<h3>Payment Request from " + ViewState("venueName") + "</h3>")
            'srtbhtmlreport.Append("This email has been sent to you from the " + ViewState("venueName") + " accounts team, via our TenderPOS system:<br/><br/>")
            'srtbhtmlreport.Append("Please click on the link below to pay for goods and services ordered. (Tax Receipt below)<br/><br/>")
            'srtbhtmlreport.Append("Please note clicking on this link you will be taken to our hosted card payment page<br/>")
            'srtbhtmlreport.Append("<br/><a href='" + LinkPay.ToString() + "'>Click here to make payment</a><br/><br/>")
            'srtbhtmlreport.Append("If you have any questions at all, please contact us on " + ViewState("locationContact") + " or " + ViewState("locationMail") + ".<br/><br/>")
            'srtbhtmlreport.Append("Thank you.<br/><br/>")
            'srtbhtmlreport.Append("Accounts Team<br/>")
            'srtbhtmlreport.Append(ViewState("venueName") + "<br/>")
            '' srtbhtmlreport.Append(ViewState("website") + "<br/>")
            'srtbhtmlreport.Append("</div>")



            'Dim srtbhtmlreport As StringBuilder = New StringBuilder()

            srtbhtmlreport.Append("<div style='font-family:verdana;font-size:13px;padding:5px;min-height:300px;'>")
            'srtbhtmlreport.Append("Payment Request from " + ViewState("venueName") + " <br/><br/>")
            srtbhtmlreport.Append("<strong>This email has been sent to you from the " + ViewState("venueName") + " accounts team, via our TenderPOS system:</strong><br/><br/>")
            srtbhtmlreport.Append("<br/><br/>There is a new charge against your account, <br/><br/>")
            srtbhtmlreport.Append("Please click below link to see all details including detailed receipt and payment options<br/>")
            srtbhtmlreport.Append("<br/><a href='" + LinkPay.ToString() + "' style='font-size:16px;'>Click here to make payment</a><br/><br/><br/>")
            srtbhtmlreport.Append("If you have any questions at all, please contact us on " + ViewState("locationContact") + " or " + ViewState("locationMail") + ".<br/><br/><br/>")
            srtbhtmlreport.Append("Thank you.<br/><br/>")
            srtbhtmlreport.Append("Accounts Team<br/>")
            srtbhtmlreport.Append(ViewState("cmpname") + "<br/>")
            'srtbhtmlreport.Append("[Insert Site Name – " + ViewState("cmpname") + "]<br/>")
            srtbhtmlreport.Append("</div>")






            'srtbhtmlreport.Append("<div style='font-family:verdana;font-size:13px;background-color:beige;padding:5px;border:brown 1px solid;min-height:300px;'>Thank you for your business.<br/><br/>")
            'srtbhtmlreport.Append("<br/>Please follow the link below to complete your purchase for £" + amount.ToString("n2") + " <br/>")
            'srtbhtmlreport.Append("<br/><a href='" + LinkPay.ToString() + "'>Click here to make payment</a><br/>")
            'srtbhtmlreport.Append("<br/> For any questions please contact us on 📞" + ViewState("locationContact") + " or 📩" + ViewState("locationMail") + "<br/><br/>")
            'srtbhtmlreport.Append("<br/>Thank You<br/></div>")



            MailTo_receipt(cid, lid, Custemail, "Payment Request from " + ViewState("venueName") + "", srtbhtmlreport.ToString(), "", "", "")
        Catch ex As Exception
            LogHelper.Error("Paybylink:EmailPayment:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub CheckStore(ByVal store As String)
        Try

            strcon.Open()
            Dim n As String = store

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"
            Else


            End If
            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Paybylink:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub

    Public Sub MailTo_receipt(ByVal CmpID As Int32, ByVal LocationID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        Dim mailSettings As SmtpSection = CType(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), SmtpSection)

        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Network.Port
            MailServer = mailSettings.Network.Host
            MailServer_Password = mailSettings.Network.Password
            MailServer_UserName = mailSettings.Network.UserName
            From_Email = mailSettings.From
            Ssl = True
            alas = venue_name
        End If

        Try

            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            oSmtpclient.Send(oE_Mail)

        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub

End Class
