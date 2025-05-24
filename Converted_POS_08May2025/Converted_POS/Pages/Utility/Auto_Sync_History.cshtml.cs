using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages.Utility
{
    public class Auto_Sync_HistoryModel : PageModel
    {
        AutoSyncRecoredController obj = new AutoSyncRecoredController();
        [BindProperty]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        public DateTime? ToDate { get; set; }
        [BindProperty]
        public string Duration { get; set; }
        [BindProperty]
        public int? LocationId { get; set; }

        [BindProperty]
        public int? MachineId { get; set; }

        [BindProperty]
        public int? VenueId { get; set; }
        [BindProperty]
        public clsAutoSyncRecored autosyncrecored { get; set; }
        public List<SelectListItem> DTVenue { get; set; }
        public List<SelectListItem> DTLocation { get; set; }
        public List<SelectListItem> MachineList { get; set; }
        [BindProperty]
        public List<clsAutoSyncRecored> autosyncrecord { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public IActionResult OnGet(int? id, int? venue_id, int? location_id, int? machine_id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            int? selectedVenueId = venue_id ?? HttpContext.Session.GetInt32("venue_id");
            int? selectedLocationId = location_id ?? HttpContext.Session.GetInt32("location_id");
            int? selectedMachineId = machine_id ?? HttpContext.Session.GetInt32("machine_id");

            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
            if (selectedVenueId.HasValue)
            {
                HttpContext.Session.SetInt32("venue_id", selectedVenueId.Value);
                DTLocation = GetLocation(ConnStr, Convert.ToInt32(cmp_id), selectedVenueId.Value);
            }
            if (selectedLocationId.HasValue)
            {
                HttpContext.Session.SetInt32("location_id", selectedLocationId.Value);
                MachineList = obj.GetMachineByLocation(Convert.ToInt32(cmp_id), selectedLocationId.Value, ConnStr);
            }
            if (selectedMachineId.HasValue)
            {
                HttpContext.Session.SetInt32("machine_id", selectedMachineId.Value);
                //MachineList = obj.GetMachineByLocation(Convert.ToInt32(cmp_id), selectedLocationId.Value, ConnStr);
            }
            return Page();
        }

        public List<SelectListItem> GetLocation(string connStr, int c_id, int venue_id)
        {
            List<SelectListItem> location = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Venue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@venue_id", venue_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);

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
        public JsonResult OnGetLoadLocations(int venueId)
        {
            string connStr = HttpContext.Session.GetString("conString");
            int cmpId = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));

            // Fetch locations for the selected venue
            List<SelectListItem> locations = obj.GetLocation(cmpId, connStr, venueId);

            // Return locations as a JSON object
            var locationItems = locations.Select(loc => new
            {
                value = loc.Value,
                text = loc.Text
            }).ToList();

            return new JsonResult(locationItems);
        }
        public IActionResult Index(DateTime? fromDate, DateTime? toDate, int? locationId, int? machineId, int? venueId, string duration)
        {
            try
            {
                var records = new List<clsAutoSyncRecored>();

                // Setting default values if parameters are not provided
                if (!fromDate.HasValue)
                    fromDate = DateTime.Now;

                if (!toDate.HasValue)
                    toDate = DateTime.Now;

                if (!locationId.HasValue)
                    locationId = 0;

                if (!machineId.HasValue)
                    machineId = 0;

                if (!venueId.HasValue)
                    venueId = 0;

                if (string.IsNullOrEmpty(duration))
                    duration = "Today";

                // Create the SQL connection and command
                using (var connection = new SqlConnection(ConnStr))
                {
                    using (var command = new SqlCommand("get_M_AutoSyncRecord_Detail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cmp_id", HttpContext.Session.GetInt32("cmp_id"));
                        command.Parameters.AddWithValue("@date1", fromDate.Value.ToString("yyyy-MM-dd HH:mm tt"));
                        command.Parameters.AddWithValue("@date2", toDate.Value.ToString("yyyy-MM-dd HH:mm tt"));
                        command.Parameters.AddWithValue("@location_id", locationId);
                        command.Parameters.AddWithValue("@machine_id", machineId);
                        command.Parameters.AddWithValue("@venue_id", venueId);
                        command.Parameters.AddWithValue("@duration", duration);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var record = new clsAutoSyncRecored
                                {
                                    Venue_Name = reader["venue_name"].ToString(),
                                    //Locations = reader["location"].ToString(),
                                    //Till_Id = reader["till"].ToString(),
                                    //Page_name = reader["Page_name"].ToString(),
                                    //SyncFlag = reader["Sync_status"].ToString(),
                                    //sync = Convert.ToDateTime(reader["sync_date"])
                                };

                                records.Add(record);
                            }
                        }
                    }
                }

                // Pass the data to the Razor Page (no need for `View()` in Razor Pages)
                ViewData["Records"] = records;  // Storing records in ViewData

                return Page();  // This renders the Razor Page (no need for `View()`)
            }
            catch (Exception ex)
            {
                // Log error (for example using Serilog or any other logger)
                Console.WriteLine("Error: " + ex.Message);
                return Page();  // Return the same page on error
            }
        }
        public IActionResult OnPost(int? venue_id, int? location_id, int? machine_id)
        {
            try
            {
                var records = new List<clsAutoSyncRecored>();

                // Retrieve ConnStr from session
                ConnStr = HttpContext.Session.GetString("conString");
                cmp_id = HttpContext.Session.GetString("cmp_id");
                if (string.IsNullOrEmpty(ConnStr))
                {
                    // Handle case where ConnStr is missing
                    Console.WriteLine("Connection string is missing from session.");
                    return RedirectToPage("/Error");  // Redirect to an error page
                }

                int? VenueId = HttpContext.Session.GetInt32("venue_id");
                int? LocationId = HttpContext.Session.GetInt32("location_id");
                int? MachineId = machine_id ?? HttpContext.Session.GetInt32("machine_id");

                // Setting default values if parameters are not provided
                if (!FromDate.HasValue)
                    FromDate = DateTime.Now;

                if (!ToDate.HasValue)
                    ToDate = DateTime.Now;

                if (!LocationId.HasValue)
                    LocationId = 0;

                if (!MachineId.HasValue)
                    MachineId = 0;

                if (!VenueId.HasValue)
                    VenueId = 0;

                if (string.IsNullOrEmpty(Duration))
                    Duration = "Today";

                // Create the SQL connection and command
                using (var connection = new SqlConnection(ConnStr))
                {
                    connection.Open();  // Open connection

                    using (var command = new SqlCommand("get_M_AutoSyncRecord_Detail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cmp_id", cmp_id);
                        command.Parameters.AddWithValue("@date1", FromDate.Value.ToString("yyyy-MM-dd HH:mm tt"));
                        command.Parameters.AddWithValue("@date2", ToDate.Value.ToString("yyyy-MM-dd HH:mm tt"));
                        command.Parameters.AddWithValue("@location_id", LocationId);
                        command.Parameters.AddWithValue("@machine_id", MachineId);
                        command.Parameters.AddWithValue("@venue_id", VenueId);
                        command.Parameters.AddWithValue("@duration", Duration);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var record = new clsAutoSyncRecored
                                {
                                    Venue_Name = reader["venue_name"].ToString(),
                                    Location_Name = reader["location"].ToString(),
                                    Till_Name = reader["till"].ToString(),
                                    Page_name = reader["Page_name"].ToString(),
                                    modify_date = reader["sync_date"] != DBNull.Value
              ? Convert.ToDateTime(reader["sync_date"])  // Convert directly to DateTime
              : (DateTime?)null,
                                    SyncFlag = reader["Sync_status"] as bool? ?? false,
                                // Populate other properties as needed
                            };

                                records.Add(record);
                            }
                        }
                    }
                }

                // Set the data to the autosyncrecord property in the PageModel
                autosyncrecord = records;
                return Page();  // This will return to the same page with updated data
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine(ex.Message);
                return Page();  // Return to the same page
            }
        }
    }
}

