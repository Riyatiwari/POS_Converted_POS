using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
 
using Converted_POS.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
//using System.Web.Script.Serialization;

//using Telerik;


namespace Converted_POS.Pages
{




    public class DashboardModel : PageModel
    {
        public string ErrorMessage { get; set; }
        private string selectedDuration;
        public string conn { get; set; }
        private Dashboard clsdashboard;
        private readonly ILogger<DashboardModel> _logger;
        public string SelectedDuration { get; set; }

        public DashboardModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
            clsdashboard = new Dashboard(new HttpContextAccessor());
            Venues = new List<SelectListItem>();
        }

        public string SelectedVenueId { get; set; }
        public List<SelectListItem> Venues { get; set; } = new List<SelectListItem>();
        public async Task<IActionResult> OnGetAsync(string selectedVenueId, string selectedDuration)
        {
            try
            {
                if (HttpContext.Session.GetString("cmp_id") == null)
                {
                    return RedirectToPage("/SignIn");
                }

                selectedDuration = Request.Query["selectedDuration"];

                string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                                           "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                                           "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                                           "Password=" + HttpContext.Session.GetString("password");

                SelectedVenueId = HttpContext.Session.GetString("selectedVenueId");

                ViewData["SelectedDuration"] = selectedDuration ?? "This Week";
                selectedVenueId = string.IsNullOrEmpty(selectedVenueId) ? "0" : selectedVenueId;


                BindData(selectedVenueId, connectionString, selectedDuration);
                Venues = clsdashboard.GetVenues(Convert.ToInt32(HttpContext.Session.GetString("cmp_id")), connectionString) ?? new List<SelectListItem>();
                if (Venues == null || !Venues.Any())
                {
                    _logger.LogWarning("Venues list is empty or null in OnGetAsync");
                    ErrorMessage = "No venues available. Please check your data.";
                }
                return Page();
            }
            catch (Exception ex)
            {

                //_logger.LogError(ex, "Error retrieving chart data in OnGetAsync");

                _logger.LogError(ex, "Error retrieving chart data in OnGetAsync");
                ErrorMessage = "Could not load the dashboard. Please contact support if the issue persists.";
                return Page();


            }
        }



        public IActionResult OnPost(string selectedVenueId, string duration)
        {
            try
            {
                string defaultDuration = "This Week";
                if (string.IsNullOrEmpty(duration))
                {
                    duration = defaultDuration;
                }

                SelectedDuration = duration;

                string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                                           "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                                           "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                                           "Password=" + HttpContext.Session.GetString("password");

                BindData(selectedVenueId, connectionString, SelectedDuration);
                Venues = clsdashboard.GetVenues(Convert.ToInt32(HttpContext.Session.GetString("cmp_id")), connectionString);
                return Page();
            }
            catch (Exception ex)
            {

                return BadRequest("Error retrieving chart data: " + ex.Message);
            }
        }

        private void BindData(string selectedVenueId, string connectionString, string selectedDuration)
        {

            BindPieChartData(selectedDuration, selectedVenueId);
            BindSalesLineChart(selectedVenueId, selectedDuration);
            LoadChartData(connectionString, selectedVenueId, selectedDuration);
            ViewData["ChartData"] = ViewData["ChartData"];
            ViewData["PieChartData"] = ViewData["PieChartData"];
            ViewData["SelectedVenueId"] = selectedVenueId;
            DateTime date1 = DateTime.Today.AddDays(-7);
            DateTime date2 = DateTime.Today;
            //  string duration = "This Week";
            //Venues = clsdashboard.GetVenues(Convert.ToInt32(HttpContext.Session.GetString("cmp_id")), connectionString);

            ViewData["GetDepartmentChartData"] = GetDepartmentChartData(date1, date2, selectedVenueId, selectedDuration);
        }



        public void LoadChartData(string connectionString, string selectedVenueId, string selectedDuration)
        {
            try
            {

                if (string.IsNullOrEmpty(selectedDuration))
                {
                    selectedDuration = "This Week";
                }
                List<string> labels = new List<string>();
                List<decimal> values = new List<decimal>();
                //DataTable dt = new DataTable();
                List<SelectListItem> chart = new List<SelectListItem>();
                int venueId = string.IsNullOrEmpty(selectedVenueId) ? 0 : Convert.ToInt32(selectedVenueId);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("WS_Get_Dashboard", con))
                    {
                        cmd.Parameters.AddWithValue("@cmp_id", Convert.ToInt32(HttpContext.Session.GetString("cmp_id")));
                        cmd.Parameters.AddWithValue("@date1", "");
                        cmd.Parameters.AddWithValue("@date2", "");
                        cmd.Parameters.AddWithValue("@location_id", "0");
                        cmd.Parameters.AddWithValue("@machine_id", "0");
                        cmd.Parameters.AddWithValue("@venue_id", venueId);
                        cmd.Parameters.AddWithValue("@Display_Report", 2);
                        cmd.Parameters.AddWithValue("@duration", selectedDuration);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt = ds.Tables[0];
                                foreach (DataRow row in dt.Rows)
                                {
                                    labels.Add(row["display_name"].ToString());
                                    decimal value = (row["NettTotal"] != DBNull.Value && !string.IsNullOrEmpty(row["NettTotal"].ToString())) ? Convert.ToDecimal(row["NettTotal"]) : 0;
                                    values.Add(value);
                                }
                            }
                            else
                            {
                                labels.AddRange(new[] { "Food", "Beverages", "Other" });
                                values.AddRange(new decimal[] { 0, 0, 0 });
                            }
                        }
                    }
                }

                var BarChartData = new
                {
                    labels,
                    datasets = new[]
                    {
                new
                {
                    data = values,
                    backgroundColor = new[]
                    {
                        "rgba(54, 162, 235, 0.8)",
                        "rgba(255, 99, 132, 0.8)",
                        "rgba(255, 159, 64, 0.8)",
                        "rgba(153, 102, 255, 0.8)",
                        "rgba(255, 206, 86, 0.8)",
                        "rgba(75, 192, 192, 0.8)"
                    }
                }
            }
                };

                var jsonData = JsonConvert.SerializeObject(BarChartData);
                ViewData["ChartData"] = jsonData;
                ViewData["Chart"] = BarChartData;
            }
            catch (Exception ex)
            {

            }



        }




        private List<SelectListItem> GetVenues(int cmpId, string connectionString)

        {
            List<SelectListItem> venues = new List<SelectListItem>();
            try
            {
                //string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                //                           "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                //                           "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                //                           "Password=" + HttpContext.Session.GetString("password");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", cmpId);


                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        venues.Add(new SelectListItem
                        {
                            Value = rdr["venue_id"].ToString(),
                            Text = rdr["venue_name"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //OnGet(selectedVenueId);

            return venues;

        }



        public void BindPieChartData(string selectedDuration, string selectedVenueId)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedDuration))
                {
                    selectedDuration = "This Week";
                }
                List<string> labels = new List<string>();
                List<decimal> values = new List<decimal>();
                int venueId = string.IsNullOrEmpty(selectedVenueId) ? 0 : Convert.ToInt32(selectedVenueId);
                string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                                             "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                                             "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                                             "Password=" + HttpContext.Session.GetString("password");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPieChartData", con))
                    {
                        cmd.Parameters.AddWithValue("@date1", "");
                        cmd.Parameters.AddWithValue("@date2", "");
                        cmd.Parameters.AddWithValue("@cmp_id", Convert.ToInt32(HttpContext.Session.GetString("cmp_id")));
                        cmd.Parameters.AddWithValue("@duration", selectedDuration);

                        cmd.Parameters.AddWithValue("@venue_id", venueId);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt = ds.Tables[0];
                                foreach (DataRow row in dt.Rows)
                                {
                                    labels.Add(row["Category"].ToString());
                                    decimal value = (row["Value"] != DBNull.Value && !string.IsNullOrEmpty(row["Value"].ToString())) ? Convert.ToDecimal(row["Value"]) : 0;
                                    values.Add(value);
                                }
                            }
                            else
                            {
                                labels.AddRange(new[] { "Food", "Beverages", "Other" });
                                values.AddRange(new decimal[] { 0, 0, 0 });
                            }
                        }
                    }
                }

                var pieChartData = new
                {
                    labels,
                    datasets = new[]
                    {
                new
                {
                    data = values,
                    backgroundColor = new[]
                    {
                        "rgba(54, 162, 235, 0.8)",
                        "rgba(255, 99, 132, 0.8)",
                        "rgba(255, 159, 64, 0.8)",
                        "rgba(153, 102, 255, 0.8)",
                        "rgba(255, 206, 86, 0.8)",
                        "rgba(75, 192, 192, 0.8)"
                    }
                }
            }
                };

                var jsonData = JsonConvert.SerializeObject(pieChartData);
                ViewData["PieChartData"] = jsonData;
            }
            catch (Exception ex)
            {

                // _logger.LogError("Page_Load:BindPieChartData:" + ex.Message);
                _logger.LogError(ex, "Error in BindPieChartData.");
                ViewData["ErrorMessage"] = "Unable to load Pie Chart data.";
            }
        }
        private void BindSalesLineChart(string selectedVenueId, string selectedDuration)
        {
            try
            {

                if (string.IsNullOrEmpty(selectedDuration))
                {
                    selectedDuration = "This Week";
                }
                List<string> labels = new List<string>();
                List<int> amounts = new List<int>();
                List<int> quantities = new List<int>();
                int venueId = string.IsNullOrEmpty(selectedVenueId) ? 0 : Convert.ToInt32(selectedVenueId);
                string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                                             "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                                             "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                                             "Password=" + HttpContext.Session.GetString("password");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSalesData", con))
                    {
                        cmd.Parameters.AddWithValue("@date1", "");
                        cmd.Parameters.AddWithValue("@date2", "");
                        cmd.Parameters.AddWithValue("@cmp_id", Convert.ToInt32(HttpContext.Session.GetString("cmp_id")));
                        cmd.Parameters.AddWithValue("@duration", selectedDuration);

                        cmd.Parameters.AddWithValue("@venue_id", venueId);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    string name = (row["name"] != DBNull.Value) ? row["name"].ToString() : "";
                                    int amount = (row["TotalAmount"] != DBNull.Value) ? Convert.ToInt32(row["TotalAmount"]) : 0;
                                    int quantity = (row["TotalQuantity"] != DBNull.Value) ? Convert.ToInt32(row["TotalQuantity"]) : 0;
                                    labels.Add(name);
                                    amounts.Add(amount);
                                    quantities.Add(quantity);
                                }
                            }
                            else
                            {

                                labels.Add("");
                                amounts.Add(0);
                                quantities.Add(0);
                            }
                        }
                    }
                }

                var chartData = new
                {
                    labels,
                    datasets = new[]
                    {
                new
                {
                    label = "Sales Amount",
                    data = amounts,
                    backgroundColor = "rgba(75, 192, 192, 1)",
                    borderColor = "rgba(75, 192, 192, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Sales Quantity",
                    data = quantities,
                    backgroundColor = "rgba(255, 99, 132, 1)",
                    borderColor = "rgba(255, 99, 132, 1)",
                    borderWidth = 1
                }
            }
                };

                var jsonData = JsonConvert.SerializeObject(chartData);
                ViewData["SalesLineChartData"] = jsonData;

            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
        public class ChartData1
        {
            public string DepartmentName { get; set; }
            public string DayOfWeek { get; set; }
            public decimal ChartValue { get; set; }
        }
        public List<ChartData1> GetDepartmentChartData(DateTime date1, DateTime date2, string selectedVenueId, string selectedDuration)
        {

            if (string.IsNullOrEmpty(selectedDuration))
            {
                selectedDuration = "This Week";
            }
            List<ChartData1> chartData2 = new List<ChartData1>();
            int venueId = string.IsNullOrEmpty(selectedVenueId) ? 0 : Convert.ToInt32(selectedVenueId);
            string connectionString = "Data Source=" + HttpContext.Session.GetString("db_server") + ";" +
                                    "Initial Catalog=" + HttpContext.Session.GetString("db_name") + ";" +
                                    "User ID=" + HttpContext.Session.GetString("user_name") + ";" +
                                    "Password=" + HttpContext.Session.GetString("password");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetDepartmentChartData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@date1", "");
                    command.Parameters.AddWithValue("@date2", "");
                    command.Parameters.AddWithValue("@duration", selectedDuration);
                    command.Parameters.AddWithValue("@venue_id", venueId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string departmentName = reader["DepartmentName"].ToString();
                            string dayOfWeek = reader["DayOfWeek"].ToString();
                            decimal value = Convert.ToDecimal(reader["Value"]);


                            var existingData = chartData2.FirstOrDefault(d => d.DepartmentName == departmentName && d.DayOfWeek == dayOfWeek);
                            if (existingData != null)
                            {

                                existingData.ChartValue += value;
                            }
                            else
                            {

                                chartData2.Add(new ChartData1 { DepartmentName = departmentName, DayOfWeek = dayOfWeek, ChartValue = value });
                            }
                        }
                    }
                }
            }
            ViewData["GetDepartmentChartData"] = chartData2;
            return chartData2;
        }



    }
}