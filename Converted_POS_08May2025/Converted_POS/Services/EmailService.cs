using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Converted_POS.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                // Get email settings from configuration
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUsername = _configuration["EmailSettings:Username"];
                var smtpPassword = _configuration["EmailSettings:Password"];
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var fromName = _configuration["EmailSettings:FromName"];
                var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

                using (var client = new SmtpClient(smtpServer))
                {
                    client.Port = smtpPort;
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(fromEmail, fromName);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = isHtml;

                        // Add recipients
                        foreach (var address in to.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            message.To.Add(address.Trim());
                        }

                        // Add attachments if any
                        if (attachments != null && attachments.Count > 0)
                        {
                            foreach (var attachment in attachments)
                            {
                                message.Attachments.Add(attachment);
                            }
                        }

                        // Send the email
                        await client.SendMailAsync(message);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                // Log the error here
                return false;
            }
        }
    }
} 