using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Converted_POS.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email with optional attachments
        /// </summary>
        Task<bool> SendEmailAsync(
            string to,
            string subject,
            string body,
            bool isHtml = false,
            List<Attachment> attachments = null);
    }
} 