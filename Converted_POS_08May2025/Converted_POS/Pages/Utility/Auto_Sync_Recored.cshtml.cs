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
using Newtonsoft.Json;


namespace Converted_POS.Pages.Utility
{
    public class Auto_Sync_RecoredModel : PageModel
    {
        AutoSyncRecoredController obj = new AutoSyncRecoredController();
        [BindProperty]
        public clsAutoSyncRecored autosyncrecored { get; set; }
        public List<SelectListItem> DTVenue { get; set; }
        public List<SelectListItem> DTLocation { get; set; }
        public List<SelectListItem> MachineList { get; set; }
        public List<Page> PageList { get; set; }
        public List<TillItem> TillList { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public string venue_id { get; set; }
        public string location_id { get; set; }
        public int? SelectedVenueId { get; set; }
        [BindProperty]
        public string SelectedMachineIds { get; set; }
        public class Page
        {
            public int Page_ID { get; set; }
            public string Page_name { get; set; }
        }
        public class TillItem
        {
            public int MachineId { get; set; }
            public string Name { get; set; }
        }
        public IActionResult OnGet(int? id, int? venue_id, int? location_id, int? till_id, string machine_ids)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            
            //if(id == null)
            //{

            //    HttpContext.Session.SetString("AutoSync_Id", HttpContext.Request.Query["ID"].ToString());
            //    //autosyncrecored = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);

            //    if (autosyncrecored == null)
            //    {
            //        return NotFound();
            //    }
                
            //    return Page();
            //}
            //HttpContext.Session.SetString("venue_id", venue_id);  // Store venue_id in session
            int? selectedVenueId = venue_id ?? HttpContext.Session.GetInt32("venue_id");
            int? selectedLocationId = location_id ?? HttpContext.Session.GetInt32("location_id");
            int? selectedTillId = till_id ?? HttpContext.Session.GetInt32("machine_id");
            //venue_id = Convert.ToInt32(HttpContext.Session.GetString("venue_id"));
            List<int> selectedMachineIds = new List<int>();

            // If 'machine_ids' is passed in the query string, store them in the session
            if (!string.IsNullOrEmpty(machine_ids))
            {
                selectedMachineIds = machine_ids.Split(',').Select(int.Parse).ToList();
                // Store in session
                HttpContext.Session.SetString("selectedMachineIds", string.Join(",", selectedMachineIds));
            }
            else
            {
                // If no machine_ids are passed, try to get them from the session
                string serializedMachineIds = HttpContext.Session.GetString("selectedMachineIds");
                if (!string.IsNullOrEmpty(serializedMachineIds))
                {
                    selectedMachineIds = serializedMachineIds.Split(',').Select(int.Parse).ToList();
                }
            }
            //location_id = HttpContext.Session.GetString("location_id");

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
            if (selectedTillId.HasValue)
            {
                HttpContext.Session.SetInt32("till_id", selectedTillId.Value);
            }
            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
            PageList = obj.GetPageList(Convert.ToInt32(cmp_id), ConnStr);
            MachineList = obj.GetMachineByLocation(Convert.ToInt32(cmp_id), selectedLocationId.GetValueOrDefault(), ConnStr);
            //TillList = obj.GetTillList();


            //DTLocation = obj.GetLocation(Convert.ToInt32(cmp_id), ConnStr, Convert.ToInt32(venue_id));

            return Page();
        }
        [HttpPost("Save")]
        public IActionResult OnPost(int? venue_id, int? location_id, int? till_id, string selectedPages)
        {
            autosyncrecored.AutoSync_Id = Convert.ToInt32(HttpContext.Session.GetString("AutoSyncId"));
            autosyncrecored.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            int? selectedVenueId = venue_id ?? HttpContext.Session.GetInt32("venue_id");
            int? selectedLocationId = location_id ?? HttpContext.Session.GetInt32("location_id");
            int? selectedTillId = till_id ?? HttpContext.Session.GetInt32("machine_id");
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            List<Page> selectedPagesList = new List<Page>();
            if (!string.IsNullOrEmpty(selectedPages))
            {
                selectedPagesList = JsonConvert.DeserializeObject<List<Page>>(selectedPages);
            }

            // Check if no pages are selected
            if (selectedPagesList == null || !selectedPagesList.Any())
            {
                ModelState.AddModelError("SelectedPages", "No pages selected.");
                return Page();  // Return the current Razor Page to show the error
            }

            // If pages are selected, save them to session (optional) or handle further
            HttpContext.Session.SetString("selectedPages", selectedPages);

            string pageListJson = JsonConvert.SerializeObject(selectedPagesList);
            string pageIdsCsv = string.Join(",", selectedPagesList.Select(p => p.Page_ID));
            string pageNamesCsv = string.Join(",", selectedPagesList.Select(p => p.Page_name));
            List<int> selectedMachineIdList = new List<int>();
            if (!string.IsNullOrEmpty(SelectedMachineIds))
            {
                // Split the string by commas and convert each to an integer
                selectedMachineIdList = SelectedMachineIds.Split(',')
                                                         .Select(id => int.Parse(id))
                                                         .ToList();
            }

            // If no machine is selected, you could return the same page with an error message

            //    if (selectedMachineId == null || !selectedMachineId.Any())
            //{
            //    // If no machine IDs are selected in the form, use the ones from the session
            //    string serializedMachineIds = HttpContext.Session.GetString("selectedMachineIds");
            //    if (!string.IsNullOrEmpty(serializedMachineIds))
            //    {
            //        selectedMachineId = serializedMachineIds.Split(',').Select(int.Parse).ToList();
            //    }
            //}

            if (autosyncrecored.AutoSync_Id == null || autosyncrecored.AutoSync_Id == 0)
            {
                autosyncrecored.AutoSync_Id = 0;
                obj.Insert(autosyncrecored, ConnStr, selectedVenueId, selectedLocationId, selectedTillId, selectedMachineIdList, selectedPagesList, pageIdsCsv, pageNamesCsv);
            }
            else
            {
                //obj.Update(autosyncrecored, ConnStr);
            }

            HttpContext.Session.Remove("AutoSync_Id");
            return RedirectToPage("/Utility/Auto_Sync_Recored");
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
    }
}
