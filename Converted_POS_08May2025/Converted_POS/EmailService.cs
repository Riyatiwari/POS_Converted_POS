using Converted_POS.Models;
using Converted_POS.Services;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        // Inject IOptions<MailSettings>
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value; // This gives you the populated values from appsettings.json
        }

        public async Task<bool> SendEmailAsync(
            string to, 
            string subject, 
            string body, 
            bool isHtml = false, 
            List<Attachment> attachments = null)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("No-Reply", _mailSettings.SmtpFrom));
                
                // Add recipients
                foreach (var address in to.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    emailMessage.To.Add(new MailboxAddress("", address.Trim()));
                }
                
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // Connect to the SMTP server
                    client.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, _mailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

                    // Authenticate with SMTP server using values from MailSettings
                    client.Authenticate(_mailSettings.SmtpUsername, _mailSettings.SmtpPassword);

                    // Send the email
                    await client.SendAsync(emailMessage);
                    client.Disconnect(true);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}