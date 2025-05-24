using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailKit.Security;
using Converted_POS.Helpers;
using Converted_POS.Services;
using Converted_POS.Extensions;

namespace Converted_POS
{
    public class ZReportController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");

        private readonly EmailService _emailService;

        // Inject the EmailService into the controller
        public ZReportController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public ZReportController()
        {
        }

        public List<SelectListItem> GetVenue(int? c_id, string conn)
        {
            List<SelectListItem> venue = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    venue.Add(new SelectListItem
                    {
                        Value = rdr["venue_id"].ToString(),
                        Text = rdr["venue_name"].ToString()
                    });
                }

                con.Close();
            }

            return venue;
        }
        public List<SelectListItem> GetLocation(int? c_id, int venue_id, string conn)
        {
            List<SelectListItem> location = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Venue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@venue_id", venue_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    location.Add(new SelectListItem
                    {
                        Value = rdr["location_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return location;
        }
        [HttpGet]
        public IActionResult ViewReport(int? c_id, string conn, DateTime? txtForDate, DateTime? txtToDate, string locationId, string machineId, string venueId, string duration, string salesType, string shiftName, string loginId)
        {
            try
            {
                BindTable(c_id, conn, txtForDate, txtToDate, locationId, machineId, venueId, duration, salesType, shiftName, loginId); // Assuming BindTable is a method you want to call here.
                return View();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"ZReport:ViewReport:{ex.Message}");
                return View("Error"); // Redirect or display an error view
            }
        }

        public List<SelectListItem> GetOperator(int? c_id, string conn)
        {
            List<SelectListItem> operatorList = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Staff_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    operatorList.Add(new SelectListItem
                    {
                        Value = rdr["staff_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return operatorList;
        }
        [HttpGet] // Use this if you're calling via GET method (or HttpPost if needed)
        public IActionResult BindTable(string duration)
        {
            // Log the incoming duration for debugging purposes
            Console.WriteLine("Received Duration: " + duration);

            // Make sure that the duration value is correctly passed and not null
            if (string.IsNullOrEmpty(duration))
            {
                duration = "Today"; // Set a default value if duration is not provided
            }

            // Assuming you have other required parameters here (like cmp_id, conn, etc.)
            var c_id = 1;  // Example: Company ID (Replace with actual value)
            var conn = "your_connection_string";  // Example: DB connection string
            var txtForDate = DateTime.Now.AddDays(-1);  // Example: Date for filter
            var txtToDate = DateTime.Now;  // Example: Date to filter
            var locationId = "0";  // Example: Location ID
            var machineId = "0";  // Example: Machine ID
            var venueId = "0";  // Example: Venue ID
            var salesType = "All";  // Example: Sales type
            var shiftName = "0";  // Example: Shift name
            var loginId = "0";  // Example: Login ID

            // Call the BindTable method and pass the duration
            var selectListItems = BindTable(c_id, conn, txtForDate, txtToDate, locationId, machineId, venueId, duration, salesType, shiftName, loginId);

            // Return the data back to the client (for example, as HTML or JSON)
            return Json(selectListItems);  // Return as JSON for AJAX success callback
        }
        public IEnumerable<SelectListItem> BindTable(int? c_id, string conn, DateTime? txtForDate, DateTime? txtToDate, string locationId, string machineId, string venueId, string duration, string salesType, string shiftName, string loginId)
        {
            var selectListItems = new List<SelectListItem>();

            try
            {
                // Check if connection string is valid
                if (string.IsNullOrEmpty(conn))
                {
                    throw new Exception("Connection string is empty.");
                }

                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("WS_Get_Z_Report", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@date1", txtForDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@date2", txtToDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmp_id", c_id ?? 0);
                    cmd.Parameters.AddWithValue("@location_id", string.IsNullOrEmpty(locationId) ? "0" : locationId);
                    cmd.Parameters.AddWithValue("@machine_id", string.IsNullOrEmpty(machineId) ? "0" : machineId);
                    cmd.Parameters.AddWithValue("@venue_id", string.IsNullOrEmpty(venueId) ? "0" : venueId);
                    cmd.Parameters.AddWithValue("@duration", string.IsNullOrEmpty(duration) ? "Today" : duration);
                    cmd.Parameters.AddWithValue("@salestype", string.IsNullOrEmpty(salesType) ? "All" : salesType);
                    cmd.Parameters.AddWithValue("@shift_name", string.IsNullOrEmpty(shiftName) ? "0" : shiftName);
                    cmd.Parameters.AddWithValue("@login_id", string.IsNullOrEmpty(loginId) ? "0" : loginId);
                    // Add other parameters (same as your original method's parameters)
                    //cmd.Parameters.AddWithValue("@location_id", radLocation.SelectedIndex > 0 ? radLocation.SelectedValue : "0");
                    //cmd.Parameters.AddWithValue("@machine_id", radMachine.SelectedIndex > 0 ? radMachine.SelectedValue : "0");
                    //cmd.Parameters.AddWithValue("@venue_id", radVenue.SelectedIndex > 0 ? radVenue.SelectedValue : "0");
                    //cmd.Parameters.AddWithValue("@duration", radDuration.SelectedIndex > 0 ? radDuration.SelectedValue.ToString() : "Today");
                    //cmd.Parameters.AddWithValue("@salestype", radsalesType.SelectedIndex > 0 ? radsalesType.SelectedValue.ToString() : "All");
                    //cmd.Parameters.AddWithValue("@shift_name", radshifttype.SelectedIndex > 0 ? radshifttype.SelectedItem.Value.ToString() : "0");
                    //cmd.Parameters.AddWithValue("@login_id", rdOperator.SelectedIndex > 0 ? rdOperator.SelectedValue : "0");

                    // Open the connection
                    con.Open();

                    // Execute the command using SqlDataAdapter to fetch the data
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);  // This will fetch data from DB into the DataTable

                    // Check if data was fetched correctly
                    if (dt.Rows.Count > 0)
                    {
                        List<SelectListItem> selectList = new List<SelectListItem>();

                        // Optionally, you may want to assign the values to labels from the first row
                        DataRow firstRow = dt.Rows[0];

                        // Assign values from the first row to your labels
                        //lblFirst.Text = firstRow["Tran_First_Dt"].ToString();
                        //lblLsttransction.Text = firstRow["tran_last_dt"].ToString();
                        //lblfromdate.Text = firstRow["FromDate"].ToString();
                        //lblTodate.Text = firstRow["ToDate"].ToString();
                        clsZReport reportInstance = new clsZReport();
                        reportInstance.NoOfReturns = Convert.ToInt32(firstRow["noofreturns"]);
                        //lblReturn.Text = firstRow["noofreturns"].ToString();

                        reportInstance.NumOfTran = Convert.ToInt32(firstRow["noofsales"]);
                        reportInstance.receipt_header = firstRow["receipt_header"].ToString();
                        ViewBag.NoOfReturns = reportInstance.NoOfReturns;
                        ViewBag.NumOfTran = reportInstance.NumOfTran;
                        // Assign value to hidden field
                        //hf_NumOfTran.Value = Val(firstRow["noofsales"].ToString());
                        foreach (DataRow row in dt.Rows)
                        {
                            string description = row["Description"].ToString();

                            // Step 1: Preserve the &nbsp; (non-breaking space) but replace it with the HTML entity for space (&#160;)
                            description = description.Replace("&nbsp;", "");

                            // Step 2: Remove any existing <b> tags from the description
                            description = Regex.Replace(description, "<b>(.*?)</b>", "$1");

                            // Define your bold keywords
                            string[] boldKeywords = { "Payments", "Sales Mix", "Tax Information", "Miscellaneous Information" };

                            // Apply bold Unicode conversion for each keyword (case-insensitive match)
                            foreach (var keyword in boldKeywords)
                            {
                                description = Regex.Replace(description, Regex.Escape(keyword), match => ConvertToBoldUnicode(match.Value), RegexOptions.IgnoreCase);
                            }

                            // Add the processed description to SelectListItems
                            selectListItems.Add(new SelectListItem
                            {
                                Text = description, // This will now include Unicode bold styling
                                Value = row["Number"].ToString()
                            });
                        }
                    }
                    else
                    {
                        // Handle case where no data is returned from the DB
                        Console.WriteLine("No data was returned from the query.");
                    }

                    con.Close();  // Always ensure to close the connection
                }
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception details
                Console.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                // Log any other exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            ViewBag.DTBindTable = selectListItems;
            // Return the list, which could be empty if no data is fetched
            return selectListItems;
        }

        private string ConvertToBoldUnicode(string input)
        {
            //StringBuilder boldText = new StringBuilder();

            //foreach (char c in input)
            //{
            //    // For uppercase letters (A-Z)
            //    if (c >= 'A' && c <= 'Z')
            //    {
            //        // Convert to bold Unicode (A -> 𝗔)
            //        boldText.Append((char)(c - 'A' + 0x1D400));
            //    }
            //    // For lowercase letters (a-z)
            //    else if (c >= 'a' && c <= 'z')
            //    {
            //        // Convert to bold Unicode (a -> 𝗮)
            //        boldText.Append((char)(c - 'a' + 0x1D41A));
            //    }
            //    else
            //    {
            //        // Non-alphabet characters remain unchanged
            //        boldText.Append(c);
            //    }
            //}

            //return boldText.ToString();
            return  input ;
        }

        [HttpGet]  // This attribute ensures the method can handle GET requests
        public IActionResult BindTablee(string duration)
        {
            // Log the incoming duration value for debugging
            Console.WriteLine("Duration received: " + duration);

            // Handle your logic here, including calling the BindTable logic that uses the duration
            var result = BindTable(duration);  // Call your logic to fetch data

            // Return a response, for example, a View or JSON data
            return Json(result);  // Respond with JSON for AJAX
        }
        [HttpPost]
        public IActionResult SendEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { success = false, message = "Email address is required." });
                }
                //_emailService.SendEmail(email);
                // Call your email sending logic here (e.g., using SMTP, MailKit, etc.)
                SendEmailToRecipient(email);  // Example: Send email using the entered email address

                return Json(new { success = true, message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                // Log or handle error here
                Console.WriteLine("Error sending email: " + ex.Message);
                return Json(new { success = false, message = "An error occurred while sending the email." });
            }
        }

        public void SendEmailToRecipient(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {

                    // Create the email message
                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress("TenderPOS", "no-reply@mytenderpos.com"));
                    emailMessage.To.Add(new MailboxAddress("", email)); // Recipient's email
                    emailMessage.Subject = "Z Report";  // Subject of the email

                    // Prepare the HTML body of the email
                    string emailBody = GenerateHtmlReport();
                    emailMessage.Body = new TextPart("html") { Text = emailBody };

                    // SMTP client setup
                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())

                    {
                        // Connect to the SMTP server
                        client.Connect("mail.privateemail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                        // Authenticate with the SMTP server
                        client.Authenticate("no-reply@mytenderpos.com", "Not4any4");

                        // Send the email
                        client.Send(emailMessage);

                        // Disconnect after sending
                        client.Disconnect(true);
                    }



                }
                else
                {
                    Console.WriteLine("Email address is required.");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or throw it as necessary
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
            }
        }

        public string GenerateHtmlReport()
        {
            StringBuilder htmlReport = new StringBuilder();
            // Add the details to the HTML report (you can reuse your VB logic here)
            htmlReport.Append("<html><body>");
            htmlReport.Append("<h2>Z Report</h2>");
            // Add more report content based on your logic
            htmlReport.Append("</body></html>");
            return htmlReport.ToString();
        }

        [HttpGet]
        public IActionResult GetShifts(string duration, DateTime? fromDate, DateTime? toDate, string connStr)
        {
            try
            {
                // Set default dates if not provided
                fromDate = fromDate ?? DateTime.Now;
                toDate = toDate ?? DateTime.Now;

                // Fetch the shifts based on the duration and date range
                var shifts = GetShiftsByDuration(duration, fromDate.Value, toDate.Value, connStr);

                return Json(shifts); // Return shifts as a JSON response
            }
            catch (Exception ex)
            {
                // Log the error and return a failure response
                // LogHelper.Error("ZReport:GetShifts:" + ex.Message);
                return Json(new { success = false, message = "An error occurred while fetching shifts." });
            }
        }

        public List<Shift> GetShiftsByDuration(string duration, DateTime fromDate, DateTime toDate, string connStr)
        {
            List<Shift> shifts = new List<Shift>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Shift_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@AccountID", id);
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    //shifts.ShiftId = Convert.ToInt32(rdr["customer_id"]);
                    //shifts.Sh = rdr["first_name"].ToString();
                    var shift = new Shift
                    {
                        ShiftId = rdr.GetInt32(rdr.GetOrdinal("ShiftId")),
                        ShiftName = rdr.GetString(rdr.GetOrdinal("ShiftName")),
                        ShiftStartTime = rdr.GetDateTime(rdr.GetOrdinal("StartTime")),
                        ShiftEndTime = rdr.GetDateTime(rdr.GetOrdinal("EndTime"))
                    };
                    shifts.Add(shift);
                }
                rdr.Close();
                con.Close();
            }
            return shifts;
        }

        public List<Shift> BindShiftByDuration(string duration, DateTime fromDate, DateTime toDate, string connStr)
        {
            List<Shift> shifts = new List<Shift>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Shift_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@AccountID", id);
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    //shifts.ShiftId = Convert.ToInt32(rdr["customer_id"]);
                    //shifts.Sh = rdr["first_name"].ToString();

                }
                con.Close();
            }
            return shifts;
        }

        public DataTable GetData(string cmp_id, string connStr, DateTime? txtForDate, DateTime? txtToDate, string locationId, string machineId, string venueId, string duration, string salesType, string shiftName, string loginId)
        {
            DataTable dt = new DataTable(); // Initialize the DataTable to hold the data

            try
            {
                // Check if connection string is valid
                if (string.IsNullOrEmpty(connStr))
                {
                    throw new Exception("Connection string is empty.");
                }

                // Create a SQL connection using the provided connection string
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Create a SQL command for the stored procedure
                    SqlCommand cmd = new SqlCommand("WS_Get_Z_Report", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@date1", txtForDate ?? DateTime.Now);  // Default to current date if null
                    cmd.Parameters.AddWithValue("@date2", txtToDate ?? DateTime.Now);    // Default to current date if null
                    cmd.Parameters.AddWithValue("@cmp_id", cmp_id);  // Company ID
                    cmd.Parameters.AddWithValue("@location_id", string.IsNullOrEmpty(locationId) ? "0" : locationId);
                    cmd.Parameters.AddWithValue("@machine_id", string.IsNullOrEmpty(machineId) ? "0" : machineId);
                    cmd.Parameters.AddWithValue("@venue_id", string.IsNullOrEmpty(venueId) ? "0" : venueId);
                    cmd.Parameters.AddWithValue("@duration", string.IsNullOrEmpty(duration) ? "Today" : duration);
                    cmd.Parameters.AddWithValue("@salestype", string.IsNullOrEmpty(salesType) ? "All" : salesType);
                    cmd.Parameters.AddWithValue("@shift_name", string.IsNullOrEmpty(shiftName) ? "0" : shiftName);
                    cmd.Parameters.AddWithValue("@login_id", string.IsNullOrEmpty(loginId) ? "0" : loginId);

                    // Open the SQL connection
                    con.Open();

                    // Create a SqlDataAdapter to execute the command and fill the DataTable
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);  // This will populate the DataTable with the data returned by the stored procedure

                    con.Close();  // Close the connection after retrieving the data
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                Console.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("Error: " + ex.Message);
            }

            return dt;
        }

        private DataTable GetDataFromDatabase(string storedProcedureName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();

            string connectionString = _configuration.GetConnectionString("POS_Connection"); // Get the connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddRange(parameters.ToArray());

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    //_logHelper.Error("Database error: " + ex.Message);
                }
            }

            return dt;
        }

    }

    //private readonly IBindingService _bindingService;
    //private readonly IRoleService _roleService;
    //private readonly IDataAccessService _dataAccessService;
    //private readonly ILogger<ZReportController> _logger;

    //public string FromDate { get; set; }
    //public string ToDate { get; set; }

    //public ZReportController(IBindingService bindingService, IRoleService roleService, IDataAccessService dataAccessService, ILogger<ZReportController> logger)
    //{
    //    _bindingService = bindingService;
    //    _roleService = roleService;
    //    _dataAccessService = dataAccessService;
    //    _logger = logger;
    //}

    //public async Task<IActionResult> Index()
    //{
    //    try
    //    {
    //        if (HttpContext.Session.GetSessionString("cmp_id") == null)
    //        {
    //            return RedirectToAction("SignIn", "Account");
    //        }

    //        if (int.Parse(HttpContext.Session.GetSessionString("staff_role_id")) != 0)
    //        {
    //            await RoleCheck();
    //        }

    //        ViewBag.View = ViewState.GetInt("view") == 1;
    //        SetInitialDates();

    //        await BindDropdowns();
    //        await GetShifts();
    //        await BindTable();

    //        return View();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"ZReport:Index:{ex.Message}");
    //        return View();
    //    }
    //}

    //private async Task BindTable()
    //{
    //    try
    //    {
    //        var oColSqlparram = new ColSqlparram
    //        {
    //            { "@cmp_id", Session["cmp_id"] },
    //            { "@date1", DateTime.Parse(FromDate).ToString("yyyy-MM-dd hh:mm tt") },
    //            { "@date2", DateTime.Parse(ToDate).ToString("yyyy-MM-dd hh:mm tt") },
    //            { "@location_id", radLocation.SelectedIndex > 0 ? radLocation.SelectedValue : "0" },
    //            { "@machine_id", radMachine.SelectedIndex > 0 ? radMachine.SelectedValue : "0" },
    //            { "@venue_id", radVenue.SelectedIndex > 0 ? radVenue.SelectedValue : "0" },
    //            { "@duration", radDuration.SelectedIndex > 0 ? radDuration.SelectedValue : "Today" },
    //            { "@salestype", radsalesType.SelectedIndex > 0 ? radsalesType.SelectedValue : "All" },
    //            { "@shift_name", radshifttype.SelectedIndex > 0 ? radshifttype.SelectedItem.Value : "0" },
    //            { "@login_id", rdOperator.SelectedIndex > 0 ? rdOperator.SelectedValue : "0" }
    //        };

    //        var dt = await _dataAccessService.GetDataTableAsync("WS_Get_Z_Report", oColSqlparram);

    //        if (dt.Rows.Count > 0)
    //        {
    //            ViewData["ReceiptHeader"] = dt.Rows[0]["receipt_header"].ToString();
    //            ViewData["FirstTransactionDate"] = dt.Rows[0]["Tran_First_Dt"].ToString();
    //            ViewData["LastTransactionDate"] = dt.Rows[0]["tran_last_dt"].ToString();
    //            ViewData["FromDate"] = dt.Rows[0]["FromDate"].ToString();
    //            ViewData["ToDate"] = dt.Rows[0]["ToDate"].ToString();
    //            ViewData["NoOfTransaction"] = dt.Rows[0]["noofsales"].ToString();
    //            ViewData["NoOfReturns"] = dt.Rows[0]["noofreturns"].ToString();
    //        }

    //        ViewData["ZReportData"] = dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"ZReport:BindTable:{ex.Message}");
    //    }
    //}

    //private void SetInitialDates()
    //{
    //    FromDate = DateTime.Now.ToString("yyyy-MM-dd");
    //    ToDate = DateTime.Now.ToString("yyyy-MM-dd");
    //}

    //private async Task BindDropdowns()
    //{
    //    try
    //    {
    //        await _bindingService.BindStaff(rdOperator, int.Parse(HttpContext.Session.GetSessionString("cmp_id")));
    //        await _bindingService.BindVenue(radVenue, int.Parse(HttpContext.Session.GetSessionString("cmp_id")));
    //        // More bindings like BindLocationByVenue, etc.
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"ZReport:BindDropdowns:{ex.Message}");
    //    }
    //}

    //private async Task GetShifts()
    //{
    //    await _bindingService.BindShiftByDuration(radshifttype, int.Parse(HttpContext.Session.GetSessionString("cmp_id")), FromDate, ToDate, radDuration.SelectedValue);
    //}

    //private async Task RoleCheck()
    //{
    //    try
    //    {
    //        var role = await _roleService.GetRoleDetails(int.Parse(HttpContext.Session.GetSessionString("cmp_id")), int.Parse(HttpContext.Session.GetSessionString("staff_role_id")));
    //        if (role.CanView)
    //        {
    //            ViewState.Set("view", 1);
    //        }
    //        else
    //        {
    //            ViewState.Set("view", 0);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"ZReport:RoleCheck:{ex.Message}");
    //    }
    //}
}

