using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages.Reports
{
    public class ZReportModel : PageModel
    {

        ZReportController obj = new ZReportController();
        [BindProperty]
        public clsZReport zreport { get; set; }
        [BindProperty]
        public string email { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public string locationId { get; set; }
        public string machineId { get; set; }
        public string venueId { get; set; }
        public string duration { get; set; }
        public string salesType { get; set; }
        public string shiftName { get; set; }
        public string loginId { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public string TranFirstDt { get; set; }
        public string TranLastDt { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string NoOfSales { get; set; }
        public string NoOfReturns { get; set; }
        public string NumOfTran { get; set; }
        public string receipt_header { get; set; }
        public int venue_id { get; set; }
        DateTime? txtForDate = DateTime.Now;
        DateTime? txtToDate = DateTime.Now;
        public IEnumerable<SelectListItem> DTVenue { get; set; }
        public IEnumerable<SelectListItem> DTLocation { get; set; }
        public IEnumerable<SelectListItem> DTOperator { get; set; }
        public IEnumerable<SelectListItem> DTBindTable { get; set; }
        public IEnumerable<SelectListItem> DTEmail { get; set; }
        public IActionResult OnGet(int? id, string duration, string email)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            //if (id.HasValue)
            //{
            //    HttpContext.Session.SetString("staff_id", id.Value.ToString());
            //    zreport = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr);

            //    if (zreport == null)
            //    {
            //        return NotFound();
            //    }
            //    //var staffJson = JsonConvert.SerializeObject(staff);
            //    //HttpContext.Session.SetString("staff", staffJson);
            //    //         HttpContext.Session.SetString("staff_id", id.Value.ToString());
            //    //HttpContext.Session.SetString("staff", JsonConvert.SerializeObject(staff));

            //}


            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
            DTLocation = obj.GetLocation(Convert.ToInt32(cmp_id), venue_id, ConnStr);
            DTOperator = obj.GetOperator(Convert.ToInt32(cmp_id), ConnStr);
            DTBindTable = obj.BindTable(Convert.ToInt32(cmp_id), ConnStr, txtForDate, txtToDate, locationId, machineId, venueId, duration, salesType, shiftName, loginId);
            if (!string.IsNullOrEmpty(email))
            {
                var emailResult = obj.SendEmail(email);  // Call SendEmail method here if needed
                                                         // Handle the result of the email (e.g., logging, sending a success message, etc.)
            }

            DataTable dt = obj.GetData(cmp_id, ConnStr, txtForDate, txtToDate, locationId, machineId, venueId, duration, salesType, shiftName, loginId);

            if (dt.Rows.Count > 0)
            {
                DataRow Row = dt.Rows[0];

                // Assign the values to the model properties
                TranFirstDt = Row["Tran_First_Dt"].ToString();
                TranLastDt = Row["tran_last_dt"].ToString();
                FromDate = DateTime.Parse(Row["FromDate"].ToString()); // Adjust as needed
                ToDate = DateTime.Parse(Row["ToDate"].ToString());
                NoOfSales = Row["noofsales"].ToString();
                NoOfReturns = Row["noofreturns"].ToString();
                NumOfTran = Row["noofsales"].ToString(); // Assuming hf_NumOfTran is used to store this value
                receipt_header = Row["receipt_header"].ToString();
            }
            ViewData["SelectListItems"] = DTBindTable;
            return Page();
        }

        public IActionResult OnPostSendEmail()
        {
            if (string.IsNullOrEmpty(email))
            {
                return new JsonResult(new { success = false, message = "Email address is required." });
            }

            try
            {
                // Call your email sending logic here
                SendEmailToRecipient(email);  // Example: Send email using the entered email address

                return new JsonResult(new { success = true, message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                // Handle error
                return new JsonResult(new { success = false, message = "An error occurred while sending the email." });
            }
        }

        private void SendEmailToRecipient(string email)
        {
            throw new NotImplementedException();
        }

        public void SendEmail(string email)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.example.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@example.com", "your-password"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@example.com"),
                    Subject = "Test Email",
                    Body = "This is a test email sent from your Razor Page.",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email); // Recipient email

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle any errors
                throw new InvalidOperationException("An error occurred while sending the email: " + ex.Message);
            }
        }
    }
}
