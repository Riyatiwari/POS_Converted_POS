using Converted_POS.Models;
using Converted_POS.Pages.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    //[Route("SalesReport")]
    public class SalesReportController : Controller
    {
        private readonly ITransactionService _transactionService;

        // Injecting the service into the constructor
        public SalesReportController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public SalesReportController()
        {
        }
        [Route("SalesReport/GetLocation")]
        public List<SelectListItem> GetLocation(int? c_id, string conn)
        {
            List<SelectListItem> location = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@venue_id", venue_id);

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

        [Route("SalesReport/GetVenue")]
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
        //[HttpGet]
        //public IActionResult ViewReport(int? c_id, string conn, DateTime? txtForDate, DateTime? txtToDate, string locationId, string machineId, string venueId, string duration, string salesType, string shiftName, string loginId)
        //{
        //    try
        //    {
        //        BindTable(c_id, conn, txtForDate, txtToDate, locationId, venueId, duration); // Assuming BindTable is a method you want to call here.
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError($"ZReport:ViewReport:{ex.Message}");
        //        return View("Error"); // Redirect or display an error view
        //    }
        //}
        //public IEnumerable<SalesReportItem> BindTable(int? c_id, string connStr, DateTime? txtForDate, DateTime? txtToDate, string locationId, string venueId, string duration)
        //{
        //    var selectListItems = new List<SelectListItem>();

        //    try
        //    {
        //        // Check if connection string is valid
        //        if (string.IsNullOrEmpty(connStr))
        //        {
        //            throw new Exception("Connection string is empty.");
        //        }

        //        using (SqlConnection con = new SqlConnection(connStr))
        //        {
        //            SqlCommand cmd = new SqlCommand("WS_R_Sales", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            // Add parameters for the stored procedure
        //            cmd.Parameters.AddWithValue("@date1", txtForDate ?? DateTime.Now);
        //            cmd.Parameters.AddWithValue("@date2", txtToDate ?? DateTime.Now);
        //            cmd.Parameters.AddWithValue("@cmp_id", c_id ?? 0);
        //            cmd.Parameters.AddWithValue("@location_id", string.IsNullOrEmpty(locationId) ? "0" : locationId);
        //            //cmd.Parameters.AddWithValue("@machine_id", string.IsNullOrEmpty(machineId) ? "0" : machineId);
        //            cmd.Parameters.AddWithValue("@venue_id", string.IsNullOrEmpty(venueId) ? "0" : venueId);
        //            cmd.Parameters.AddWithValue("@duration", string.IsNullOrEmpty(duration) ? "Today" : duration);
        //            //cmd.Parameters.AddWithValue("@salestype", string.IsNullOrEmpty(salesType) ? "All" : salesType);
        //            //cmd.Parameters.AddWithValue("@shift_name", string.IsNullOrEmpty(shiftName) ? "0" : shiftName);
        //            //cmd.Parameters.AddWithValue("@login_id", string.IsNullOrEmpty(loginId) ? "0" : loginId);

        //            con.Open();

        //            // Execute the command using SqlDataAdapter to fetch the data
        //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adp.Fill(dt);  // This will fetch data from DB into the DataTable

        //            // Check if data was fetched correctly
        //            if (dt.Rows.Count > 0)
        //            {
        //                List<SelectListItem> selectList = new List<SelectListItem>();

        //                // Optionally, you may want to assign the values to labels from the first row
        //                DataRow firstRow = dt.Rows[0];

        //                // Assign values from the first row to your labels
        //                //lblFirst.Text = firstRow["Tran_First_Dt"].ToString();
        //                //lblLsttransction.Text = firstRow["tran_last_dt"].ToString();
        //                //lblfromdate.Text = firstRow["FromDate"].ToString();
        //                //lblTodate.Text = firstRow["ToDate"].ToString();
        //                clsSalesReport reportInstance = new clsSalesReport();
        //                reportInstance.sales_id = Convert.ToInt32(firstRow["sales_id"]);
        //                reportInstance.created_date = Convert.ToDateTime(firstRow["Date"]);
        //                reportInstance.table_name = Convert.ToString(firstRow["table_name"]);
        //                //lblReturn.Text = firstRow["noofreturns"].ToString();

        //                //reportInstance.NumOfTran = Convert.ToInt32(firstRow["noofsales"]);
        //                //reportInstance.receipt_header = firstRow["receipt_header"].ToString();
        //                //ViewBag.NoOfReturns = reportInstance.NoOfReturns;
        //                //ViewBag.NumOfTran = reportInstance.NumOfTran;
        //                // Assign value to hidden field
        //                //hf_NumOfTran.Value = Val(firstRow["noofsales"].ToString());
        //                //foreach (DataRow row in dt.Rows)
        //                //{
        //                //    string description = row["Description"].ToString();

        //                //    // Step 1: Preserve the &nbsp; (non-breaking space) but replace it with the HTML entity for space (&#160;)
        //                //    description = description.Replace("&nbsp;", "&#160;");

        //                //    // Step 2: Remove any existing <b> tags from the description
        //                //    description = Regex.Replace(description, "<b>(.*?)</b>", "$1");

        //                //    // Define your bold keywords
        //                //    string[] boldKeywords = { "Payments", "Sales Mix", "Tax Information", "Miscellaneous Information" };

        //                //    // Apply bold Unicode conversion for each keyword (case-insensitive match)
        //                //    foreach (var keyword in boldKeywords)
        //                //    {
        //                //        description = Regex.Replace(description, Regex.Escape(keyword), match => ConvertToBoldUnicode(match.Value), RegexOptions.IgnoreCase);
        //                //    }

        //                //    // Add the processed description to SelectListItems
        //                //    selectListItems.Add(new SelectListItem
        //                //    {
        //                //        Text = description, // This will now include Unicode bold styling
        //                //        Value = row["Number"].ToString()
        //                //    });
        //                //}
        //            }
        //            else
        //            {
        //                // Handle case where no data is returned from the DB
        //                Console.WriteLine("No data was returned from the query.");
        //            }

        //            con.Close();  // Always ensure to close the connection
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        // Log the SQL exception details
        //        Console.WriteLine("SQL Error: " + sqlEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log any other exceptions
        //        Console.WriteLine("Error: " + ex.Message);
        //    }
        //    ViewBag.DTBindTable = selectListItems;
        //    // Return the list, which could be empty if no data is fetched
        //    return selectListItems;
        //}
        [Route("SalesReport/BindTable")]
        public IEnumerable<SalesReportItem> BindTable(int? c_id, string connStr, DateTime? txtForDate, DateTime? txtToDate, string locationId, string venueId, string selectedDuration)
        {
            var salesReportItems = new List<SalesReportItem>();

            try
            {
                // Check if connection string is valid
                if (string.IsNullOrEmpty(connStr))
                {
                    throw new Exception("Connection string is empty.");
                }

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("WS_R_Sales", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@date1", txtForDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@date2", txtToDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmp_id", c_id ?? 0);
                    cmd.Parameters.AddWithValue("@location_id", string.IsNullOrEmpty(locationId) ? "0" : locationId);
                    cmd.Parameters.AddWithValue("@venue_id", string.IsNullOrEmpty(venueId) ? "0" : venueId);
                    cmd.Parameters.AddWithValue("@duration", string.IsNullOrEmpty(selectedDuration) ? "Today" : selectedDuration);

                    con.Open();

                    // Execute the command using SqlDataAdapter to fetch the data
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);  // This will fetch data from DB into the DataTable

                    // Check if data was fetched correctly
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            // Create a new SalesReportItem for each row in the data
                            var salesReportItem = new SalesReportItem
                            {
                                //Date = Convert.ToDateTime(row["Date"].ToString()),
                                //Till = row["Till"].ToString(),
                                //UserName = row["UserName"].ToString(),
                                //TableName = row["table_name"].ToString(),
                                //TotalAmount = Convert.ToDecimal(row["total_amount"]),
                                //TotalDiscount = Convert.ToDecimal(row["total_discount"]),
                                //NetAmount = Convert.ToDecimal(row["net_amount"]),
                                //Payment = row["payment"].ToString(),
                                //SaleType = row["sale_type"].ToString(),
                                //Srno = row["srno"].ToString(),
                                //TableUuid = row["table_uuid"].ToString(),
                                //TranUuid = row["tran_uuid"].ToString(),
                                //SalesId = Convert.ToInt32(row["sales_id"])


                                //Date = row["Date"] != DBNull.Value ? Convert.ToDateTime(row["Date"]) : DateTime.MinValue,

                                //// Convert Till, UserName, and TableName as string values
                                //Till = row["Till"] != DBNull.Value ? row["Till"].ToString() : string.Empty,
                                //UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty,
                                //TableName = row["table_name"] != DBNull.Value ? row["table_name"].ToString() : string.Empty,

                                //// Convert TotalAmount, TotalDiscount, and NetAmount to decimal
                                //TotalAmount = row["total_amount"] != DBNull.Value ? Convert.ToDecimal(row["total_amount"]) : 0m,
                                //TotalDiscount = row["total_discount"] != DBNull.Value ? Convert.ToDecimal(row["total_discount"]) : 0m,
                                //NetAmount = row["net_amount"] != DBNull.Value ? Convert.ToDecimal(row["net_amount"]) : 0m,

                                //// Convert Payment and SaleType to string
                                //Payment = row["payment"] != DBNull.Value ? row["payment"].ToString() : string.Empty,
                                //SaleType = row["sale_type"] != DBNull.Value ? row["sale_type"].ToString() : string.Empty,

                                //// Convert Srno, TableUuid, and TranUuid to string
                                //Srno = row["srno"] != DBNull.Value ? row["srno"].ToString() : string.Empty,
                                //TableUuid = row["table_uuid"] != DBNull.Value ? row["table_uuid"].ToString() : string.Empty,
                                //TranUuid = row["tran_uuid"] != DBNull.Value ? row["tran_uuid"].ToString() : string.Empty,

                                //// Convert SalesId to int, handling DBNull if necessary
                                //SalesId = row["sales_id"] != DBNull.Value ? Convert.ToInt32(row["sales_id"]) : 0



                                Date = row["Date"] != DBNull.Value ?
                   (DateTime.TryParse(row["Date"].ToString(), out var dateValue) ? dateValue : DateTime.MinValue) :
                   DateTime.MinValue,

                                // Handle Till conversion
                                Till = row["Till"] != DBNull.Value ? row["Till"].ToString() : string.Empty,
                                UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty,

                                // TableName conversion
                                TableName = row["table_name"] != DBNull.Value ? row["table_name"].ToString() : string.Empty,
                                // Handle TotalAmount conversion safely
                                TotalAmount = row["total_amount"] != DBNull.Value && decimal.TryParse(row["total_amount"].ToString(), out var totalAmountValue) ? totalAmountValue : 0m,
                                FormattedTotalAmount = row["total_amount"] != DBNull.Value && decimal.TryParse(row["total_amount"].ToString(), out var totalAmountFormattedValue) ?
                                   "$" + totalAmountFormattedValue.ToString("0.00") : "$0.00",


                                // Handle TotalDiscount conversion safely
                                TotalDiscount = row["total_discount"] != DBNull.Value && decimal.TryParse(row["total_discount"].ToString(), out var totalDiscountValue) ? totalDiscountValue : 0m,
                                FormattedTotalDiscount = row["total_discount"] != DBNull.Value && decimal.TryParse(row["total_discount"].ToString(), out var totalDiscountFormattedValue) ?
                                     "$" + totalDiscountFormattedValue.ToString("0.00") : "$0.00",

                                // Optional fields that are not always used
                                // Uncomment and handle them as needed

                                // Handle NetAmount conversion safely (if needed)
                                NetAmount = row["net_amount"] != DBNull.Value && decimal.TryParse(row["net_amount"].ToString(), out var netAmountValue) ? netAmountValue : 0m,
                                FormattedNetAmount = row["net_amount"] != DBNull.Value && decimal.TryParse(row["net_amount"].ToString(), out var netAmountFormattedValue) ?
                                 "$" + netAmountFormattedValue.ToString("0.00") : "$0.00",

                                // Handle Payment conversion safely
                                Payment = row["payment"] != DBNull.Value ? row["payment"].ToString() : string.Empty,

                                // Handle SaleType conversion safely
                                SaleType = row["sale_type"] != DBNull.Value ? row["sale_type"].ToString() : string.Empty,

                                // Handle Srno conversion safely
                                Srno = row["srno"] != DBNull.Value ? row["srno"].ToString() : string.Empty,

                                // Handle TableUuid conversion safely
                                TableUuid = row["table_uuid"] != DBNull.Value ? row["table_uuid"].ToString() : string.Empty,

                                // Handle TranUuid conversion safely
                                TranUuid = row["tran_uuid"] != DBNull.Value ? row["tran_uuid"].ToString() : string.Empty,

                                // Handle SalesId conversion safely
                                SalesId = row["sales_id"] != DBNull.Value && int.TryParse(row["sales_id"].ToString(), out var salesIdValue) ? salesIdValue : 0
                            };


                            // Add the populated SalesReportItem to the list
                            salesReportItems.Add(salesReportItem);
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

            // Return the list of SalesReportItem
            return salesReportItems;
        }
        [HttpPost]
        [Route("reports/SalesReport")]
        public IActionResult ViewTransactionDetail(int id)
        {
            try
            {
                // Split the commandArgument received from the link
                //var arr = commandArgument.Argument.Split('#');
                //var salesId = Convert.ToInt32(arr[0]);
                //var tranUuid = arr[1];
                //var saleType = arr[2];
                //var payType = arr[3];  // Make sure this matches what you're passing

                // Log or store values for debugging
                //TempData["Tran_Report"] = saleType;
                //TempData["type"] = payType;  // Assuming payType is being passed correctly

                // Simulate fetching transaction details or any logic you want
                //var transactionDetails = new { SalesId = salesId, TranUuid = tranUuid, SaleType = saleType, PayType = payType };

                return Json(new { success = true /*data = *//*transactionDetails*/ });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        private string GetPayType(string saleType)
        {
            if (saleType.Contains("Card") || saleType.Contains("Cash") || saleType.Contains("Deposit") ||
            saleType.Contains("Integrated") || saleType.Contains("Other") || saleType.Contains("Credit") ||
            saleType.Contains("Package"))
            {
                return saleType.Contains("Cash") || saleType.Contains("Card") ? "0" : "1";
            }

            return "0";
        }

        private bool IsTableTransaction(string saleType)
        {
            var tableTypes = new[] {
            "Table", "Cash", "Card", "Integrated - Room Payment", "Integrated - Helpout",
            "Gift Card", "Deposit Sale", "Integrated - Elina", "Integrated - Add Pay",
            "Credit Account", "Integrated - Positive", "Other Voucher", "Prepaid Package",
            "Other Deposit", "Integrated - Kinetic", "Payby Link"
        };

            return tableTypes.Contains(saleType);
        }

        public DataTable GetData(string cmp_id, string connStr, DateTime? txtForDate, DateTime? txtToDate, string locationId, string venueId, string duration)
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
                    SqlCommand cmd = new SqlCommand("WS_R_Sales", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@date1", txtForDate ?? DateTime.Now);  // Default to current date if null
                    cmd.Parameters.AddWithValue("@date2", txtToDate ?? DateTime.Now);    // Default to current date if null
                    cmd.Parameters.AddWithValue("@cmp_id", cmp_id);  // Company ID
                    cmd.Parameters.AddWithValue("@location_id", string.IsNullOrEmpty(locationId) ? "0" : locationId);
                    //cmd.Parameters.AddWithValue("@machine_id", string.IsNullOrEmpty(machineId) ? "0" : machineId);
                    cmd.Parameters.AddWithValue("@venue_id", string.IsNullOrEmpty(venueId) ? "0" : venueId);
                    cmd.Parameters.AddWithValue("@duration", string.IsNullOrEmpty(duration) ? "Today" : duration);
                    //cmd.Parameters.AddWithValue("@salestype", string.IsNullOrEmpty(salesType) ? "All" : salesType);
                    //cmd.Parameters.AddWithValue("@shift_name", string.IsNullOrEmpty(shiftName) ? "0" : shiftName);
                    //cmd.Parameters.AddWithValue("@login_id", string.IsNullOrEmpty(loginId) ? "0" : loginId);

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

        //[HttpGet]  // This attribute ensures the method can handle GET requests
        //public IActionResult BindTablee(string duration)
        //{
        //    // Log the incoming duration value for debugging
        //    Console.WriteLine("Duration received: " + duration);

        //    // Handle your logic here, including calling the BindTable logic that uses the duration
        //    var result = BindTable(duration);  // Call your logic to fetch data

        //    // Return a response, for example, a View or JSON data
        //    return Json(result);  // Respond with JSON for AJAX
        //}

        //[HttpGet] // Use this if you're calling via GET method (or HttpPost if needed)
        //public IActionResult BindTable(string duration)
        //{
        //    // Log the incoming duration for debugging purposes
        //    Console.WriteLine("Received Duration: " + duration);

        //    // Make sure that the duration value is correctly passed and not null
        //    if (string.IsNullOrEmpty(duration))
        //    {
        //        duration = "Today"; // Set a default value if duration is not provided
        //    }

        //    // Assuming you have other required parameters here (like cmp_id, conn, etc.)
        //    var c_id = 1;  // Example: Company ID (Replace with actual value)
        //    var conn = "your_connection_string";  // Example: DB connection string
        //    var txtForDate = DateTime.Now.AddDays(-1);  // Example: Date for filter
        //    var txtToDate = DateTime.Now;  // Example: Date to filter
        //    var locationId = "0";  // Example: Location ID
        //    var machineId = "0";  // Example: Machine ID
        //    var venueId = "0";  // Example: Venue ID
        //    var salesType = "All";  // Example: Sales type
        //    var shiftName = "0";  // Example: Shift name
        //    var loginId = "0";  // Example: Login ID

        //    // Call the BindTable method and pass the duration
        //    var selectListItems = BindTable(c_id, conn, txtForDate, txtToDate, locationId, venueId,  duration);

        //    // Return the data back to the client (for example, as HTML or JSON)
        //    return Json(selectListItems);  // Return as JSON for AJAX success callback
        //}
       
        [Route("SalesReport/ViewDetail")]
        public JsonResult ViewDetail(int? salesId, string connStr, string tranUuid, string saleType)
        {
            List<SelectListItem> selectListItems = GetSelectListItemsFromSomeSource(salesId, connStr, tranUuid, saleType);
            //try
            //{
            //    // Call the service method to execute the stored procedure
            //    var transactionDetails = _transactionService.GetTransactionDetails(salesId, tranUuid, saleType);

            //    if (transactionDetails != null)
            //    {
            //        // Return the data (you can also return a redirect URL, or other data as needed)
            //        return Json(new { success = true, transactionDetails = transactionDetails });
            //    }
            //    else
            //    {
            //        // Return a failure response if no data found
            //        return Json(new { success = false, message = "Transaction details not found." });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the error and return a failure response
            //    return Json(new { success = false, message = "An error occurred: " + ex.Message });
            //}
            return new JsonResult(selectListItems);
        }

        private List<SelectListItem> GetSelectListItemsFromSomeSource(int? salesId, string connStr, string tranUuid, string saleType)
        {
            var selectListItems = new List<SelectListItem>();

            try
            {
                // Check if the connection string is valid
                if (string.IsNullOrEmpty(connStr))
                {
                    throw new Exception("Connection string is empty.");
                }

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Define the command to call the stored procedure
                    SqlCommand cmd = new SqlCommand("Transaction_report_Detail_View_CashCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@Sales_Id", salesId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TranUuid", tranUuid ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SaleType", saleType ?? (object)DBNull.Value);

                    con.Open();

                    // Execute the command using SqlDataAdapter to fetch data
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);  // Fetch data from the DB into the DataTable

                    // Check if any rows are returned
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            // Assuming the stored procedure returns a column 'Description' and 'Id' (adjust as needed)
                            var selectListItem = new SelectListItem
                            {
                                Text = row["Description"] != DBNull.Value ? row["Description"].ToString() : string.Empty,
                                Value = row["Id"] != DBNull.Value ? row["Id"].ToString() : string.Empty
                            };

                            // Add the populated SelectListItem to the list
                            selectListItems.Add(selectListItem);
                        }
                    }
                    else
                    {
                        // Handle case where no data is returned
                        Console.WriteLine("No data was returned from the stored procedure.");
                    }

                    con.Close();  // Always close the connection
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

            // Return the list of SelectListItem
            return selectListItems;
        }
    }
}
