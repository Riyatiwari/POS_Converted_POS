
Imports System.Net.Mail

Partial Class check_image
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btn_Click(sender As Object, e As EventArgs)

        Dim Smtp_Server As New SmtpClient
        Dim e_mail As New MailMessage()
        Smtp_Server.Host = "smtp.gmail.com"
        Smtp_Server.EnableSsl = True
        Smtp_Server.UseDefaultCredentials = True
        Smtp_Server.Credentials = New Net.NetworkCredential("bstenderpos@gmail.com", "Do4safet$123")
        Smtp_Server.Port = 587

        e_mail = New MailMessage()
        e_mail.From = New MailAddress("bstenderpos@gmail.com")
        e_mail.To.Add("developer@technometrics.in")
        e_mail.Subject = "Email Sending"
        e_mail.Body = "test mail"
        e_mail.IsBodyHtml = False

        Smtp_Server.Send(e_mail)
        MsgBox("Mail Sent")

    End Sub
End Class
