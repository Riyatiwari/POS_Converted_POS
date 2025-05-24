Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Security.Authentication
Imports System.Net
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Net.Configuration
Imports System.Data.SqlClient

Partial Class Reset_Password
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess
    Dim objclsLogin As New clsLogin
    Dim oLogin As New Controller_clsLogin()


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try

            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim username As String = txtUname.Text.Trim()
            Dim cmd As SqlCommand = New SqlCommand("reset_password_withmail", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.Add("@Storeuuid", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output

            cmd.ExecuteNonQuery()
            Dim emailstoreUUID As String = If(cmd.Parameters("@Storeuuid").Value IsNot DBNull.Value, cmd.Parameters("@Storeuuid").Value.ToString(), Nothing)
            strcon.Close()

            Session("emailstoreuuid") = emailstoreUUID



            If Not String.IsNullOrEmpty(Session("emailstoreuuid")) Then

                Dim resetlink As String = GeneratePasswordResetLink()

                Dim resetpasswordmessage As String = "You can Reset your Password to click <a href='" & resetlink & "'>here</a>"
                Dim signinlink As String = "  <a href='http://live.mytenderpos.com/User_Access.aspx'> Sign In</a>"

                Dim congratulatorymessage As String = "RESET PASSWORD"
                Dim loginbackofficemessage As String = " Once you have set your password click here 🔜" & signinlink & "To Sign into your Account"
                Dim thankyoumessage As String = "Thank you For choosing our service! "
                Dim emailbody As String = congratulatorymessage & "<br/><br/>" & resetpasswordmessage & "<br/><br/>" & loginbackofficemessage & "<br/><br/>" & thankyoumessage


                Dim email As String
                Dim Subject As String

                email = txtUname.Text.ToString()

                'madhvanimitesh@gmail.com
                Subject = "User Registration"

                LogHelper.Error("staff_Master:email_id:" & email)
                MailTo_receipt(email, Subject, emailbody, "", "", "")


                Dim script As String = "alert('Email sent successfully!');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "EmailSuccess", script, True)

            Else

                lblMsg.Text = "Incorrect username or password"

            End If




        Catch ex As Exception
            LogHelper.Error("Reset_Password:btnReset_Click:" + ex.Message)
        End Try


    End Sub




    Private Function GeneratePasswordResetLink() As String
        Dim encryptedEmail As String = txtUname.Text
        
        Dim encryptedStoreUuid As String = Session("emailstoreUUID").ToString()






        Dim expirationTime As DateTime = DateTime.Now.AddMinutes(29)
        'Dim expirationTime As DateTime = DateTime.Now.AddMinutes(30)


        Dim encryptvalue As DateTime = expirationTime.ToString("yyyy-MM-dd HH:mm:ss")

        Dim encryptedExpirationTime As DateTime = encryptvalue





        Return "http://live.mytenderpos.com/mailReset_Password.aspx?el=" & encryptedEmail & "&sid=" & encryptedStoreUuid & "&exp=" & encryptedExpirationTime
    End Function


    Public Sub MailTo_receipt(ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        'Dim dtMailDetail As DataTable = oClsDataccess.Getdatatable("select * from S_Email_Settings where Company_id = " + CmpID.ToString() + " and location_id = " + LocationID.ToString())
        'If dtMailDetail.Rows.Count > 0 Then
        '    MailServer = dtMailDetail.Rows(0)("MailServer")
        '    MailServer_UserName = dtMailDetail.Rows(0)("MailServer_UserName")
        '    MailServer_Password = dtMailDetail.Rows(0)("MailServer_Password")
        '    MailServer_Port = dtMailDetail.Rows(0)("Port")
        '    From_Email = dtMailDetail.Rows(0)("From_Email")
        '    Ssl = IIf(dtMailDetail.Rows(0)("ssl") = "1", True, False)
        '    alas = dtMailDetail.Rows(0)("Alias")
        'Else
        Dim configurationFile As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
        Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")
        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Smtp.Network.Port
            MailServer = mailSettings.Smtp.Network.Host
            MailServer_Password = mailSettings.Smtp.Network.Password
            MailServer_UserName = mailSettings.Smtp.Network.UserName
            From_Email = mailSettings.Smtp.From
            Ssl = True
            alas = "TenderPOS"
        End If
        'End If

        LogHelper.Error("Base_class: 1:" + MailServer_Port.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_Password.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_UserName.ToString())
        LogHelper.Error("Base_class: 1:" + From_Email.ToString())
        Try

            '  LogHelper.Error("Base_class: 1:" + System.DateTime.Now.ToString())
            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            '    LogHelper.Error("Base_class: 2:" + System.DateTime.Now.ToString())
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
            'LogHelper.Error("Base_class: 3:" + System.DateTime.Now.ToString())
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network


            ' LogHelper.Error("Base_class: 4:" + System.DateTime.Now.ToString())
            oSmtpclient.Send(oE_Mail)
            'LogHelper.Error("Base_class: 5:" + System.DateTime.Now.ToString())
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
