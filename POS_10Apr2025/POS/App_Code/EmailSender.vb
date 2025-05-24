Imports System.Net.Mail
Imports Microsoft.VisualBasic

Public Class EmailSender
    Public Shared Function SendMail(ByVal emailTo As String, ByVal emailFrom As String, ByVal emailSubj As String, ByVal emailBody As String) As Boolean
        Try

            Dim fromAddress As New MailAddress(emailFrom, "Hello")
            Dim toAddress As New MailAddress(emailTo, "Hi")
            Const fromPassword As String = "pwd"
            ' SMTP Configuration
            Dim smtp As New SmtpClient()
            smtp.Host = "smtp.gmail.com"
            smtp.Port = 587
            smtp.EnableSsl = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            ' MAIL MESSAGE CONFIGURATION
            Dim message As New MailMessage(fromAddress, toAddress)
            message.Subject = emailSubj
            message.Body = emailBody
            'SEND EMAIL
            smtp.Send(message)
            Return True
        Catch ex As SmtpException
            Return False
        End Try
    End Function

End Class
