using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Converted_POS.Pages.forms
{
    public class Staff_MasterModel : PageModel
    {
        StaffController obj = new StaffController();

        [BindProperty]
        public clsStaff staff { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> DTRole { get; set; }
        public List<SelectListItem> DTAccessBackOfficeRole { get; set; }
        public IEnumerable<SelectListItem> DTVenue { get; set; }
        public IEnumerable<string> SelectedVenueValues { get; set; }
        public List<SelectListItem> DTCountry { get; set; }
        public List<SelectListItem> DTState { get; set; }
        public List<SelectListItem> DTCity { get; set; }
        public int? SelectedCountryId { get; set; }
        public int? SelectedStateId { get; set; }
        public int? SelectedCityId { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string SelectedRole { get; set; }
        public IActionResult OnGet(int? id, int? country_id, int? state_id, int? city_id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            // Retrieve selected country ID from session or default to 108

            var nextTillCode = GenerateTillCode(Convert.ToInt32(cmp_id), ConnStr);

            staff = new clsStaff
            {
                till_code = nextTillCode
            };
            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId") ?? 108; // Default to India (Country ID 108)
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId") ?? 284; // Default to State ID 284
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId") ?? 20005;

            if (id.HasValue)
            {
                HttpContext.Session.SetString("staff_id", id.Value.ToString());
                staff = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr, staff.country_id, staff.state_id, staff.city_id);

                if (staff == null)
                {
                    return NotFound();
                }
                //var staffJson = JsonConvert.SerializeObject(staff);
                //HttpContext.Session.SetString("staff", staffJson);
                //         HttpContext.Session.SetString("staff_id", id.Value.ToString());
                //HttpContext.Session.SetString("staff", JsonConvert.SerializeObject(staff));

            }
            //else
            //{
            //    // Handle the case where id is null
            //    // For example, initialize a new staff object or show an empty form
            //}

            //var states = GetState(SelectedCountryId);  // Ensure SelectedCountryId is used
            //ViewData["States"] = states;

            //GetState(country_id);
            ////GetCity(state_id);
            DTCountry = obj.GetCountry(Convert.ToInt32(cmp_id), ConnStr);
            if (SelectedCountryId.HasValue)
            {
                DTState = GetState(ConnStr, SelectedCountryId.Value);
            }

            // Fetch cities for selected state
            if (SelectedStateId.HasValue)
            {
                DTCity = GetCity(ConnStr, SelectedStateId.Value);
            }

            int? selectedCountryId = country_id ?? HttpContext.Session.GetInt32("country_id");

            // Fetch states for selected country
            if (selectedCountryId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCountryId", selectedCountryId.Value);
                DTState = GetState(ConnStr, selectedCountryId.Value);
                ViewData["States"] = DTState;
            }

            // Retrieve selected state_id from query parameters or session
            int? selectedStateId = state_id ?? HttpContext.Session.GetInt32("state_id");

            // Fetch cities for selected state
            if (selectedStateId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedStateId", selectedStateId.Value);
                DTCity = GetCity(ConnStr, selectedStateId.Value);
                ViewData["Cities"] = DTCity;
            }

            int? selectedCityId = city_id ?? HttpContext.Session.GetInt32("city_id");
            if (selectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", selectedCityId.Value);
            }

            //int? sessionCountryId = HttpContext.Session.GetInt32("SelectedCountryId");
            //int? SelectedCountryId = sessionCountryId ?? countryId;
            //SelectedCountryId = SelectedCountryId.GetValueOrDefault();

            //if (SelectedCountryId != null)
            //{
            //    DTState = obj.GetState(Convert.ToInt32(cmp_id), ConnStr, SelectedCountryId);
            //}
            //OnGetStates(countryId);

            // Load states based on the selected country
            //DTState = obj.GetState(Convert.ToInt32(cmp_id), ConnStr, countryId);

            // Load cities if a stateId is provided
            //if (Request.Query.TryGetValue("stateId", out var stateIdValues) && int.TryParse(stateIdValues.FirstOrDefault(), out var stateId))
            //{
            //    DTCity = obj.GetCity(Convert.ToInt32(cmp_id), ConnStr, stateId);
            //}
            HttpContext.Session.SetInt32("selectedCountryId", SelectedCountryId.Value);
            HttpContext.Session.SetInt32("selectedStateId", SelectedStateId.Value);
            HttpContext.Session.SetInt32("selectedCityId", SelectedCityId.Value);

            // Pass the selected values to the view
            ViewData["CountryId"] = SelectedCountryId;
            ViewData["StateId"] = SelectedStateId;
            ViewData["CityId"] = SelectedCityId;

            //ViewData["CountryId"] = country_id;
            //ViewData["StateId"] = state_id;
            //ViewData["CityId"] = city_id;
            DTRole = obj.GetPOSRole(Convert.ToInt32(cmp_id), ConnStr);
            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
            DTAccessBackOfficeRole = obj.GetAccessRole(Convert.ToInt32(cmp_id), ConnStr);
            //ViewData["staff"] = staff;

            return Page();
        }

        private List<SelectListItem> GetCity(string connStr, int state_id)
        {
            List<SelectListItem> city = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_City", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@state_id", state_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    city.Add(new SelectListItem
                    {
                        Value = rdr["City_id"].ToString(),
                        Text = rdr["CityName"].ToString()
                    });
                }

                con.Close();
            }

            return city;
        }

        private List<SelectListItem> GetState(string connStr, int country_id)
        {
            List<SelectListItem> state = new List<SelectListItem>();

            //if (!country_id.HasValue)
            //{
            //    return state; // Return an empty list or handle accordingly
            //}

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_States", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@country_id", country_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int stateId = Convert.ToInt32(rdr["State_Id"]);
                    string stateName = rdr["StateName"].ToString();
                    state.Add(new SelectListItem
                    {
                        Value = stateId.ToString(),
                        Text = stateName
                    });
                }
                con.Close();
            }
            return state;
        }
        [HttpGet]
        public IActionResult GetState(int? country_id)
        {
            var states = obj.GetState(Convert.ToInt32(cmp_id), ConnStr, country_id);
            return new JsonResult(states);
        }
        [HttpGet]
        public IActionResult GetCity(int state_id)
        {
            var cities = obj.GetCity(Convert.ToInt32(cmp_id), ConnStr, state_id);
            return new JsonResult(cities);
        }
        public IActionResult OnPostStoreSelectedCountry(int countryId)
        {
            HttpContext.Session.SetInt32("SelectedCountryId", countryId);
            return new JsonResult(new { success = true });
        }
        public string GenerateTillCode(int cmpId, string connStr)
        {
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (var command = new SqlCommand("get_autofill_fields", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cmp_id", cmpId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["tillcode"].ToString();
                        }
                    }
                }
            }
            return "1000";
        }

        [HttpPost("Save")]
        public IActionResult OnPost(int? id, int? country_id, int? state_id, int? city_id)
        {
            staff.staff_id = Convert.ToInt32(HttpContext.Session.GetString("staff_id"));
            staff.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            //if (string.IsNullOrEmpty(SelectedRole))
            //{
            //    ModelState.AddModelError("SelectedRole", "This field is required.");
            //    return Page();
            //} 
            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId");
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId");
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId");

            country_id = SelectedCountryId;
            state_id = SelectedStateId;
            city_id = SelectedCityId;

            DTCountry = obj.GetCountry(Convert.ToInt32(staff.cmp_id), ConnStr);
            if (SelectedCountryId.HasValue)
            {
                DTState = GetState(ConnStr, SelectedCountryId.Value);
            }

            // Fetch cities for selected state
            if (SelectedStateId.HasValue)
            {
                DTCity = GetCity(ConnStr, SelectedStateId.Value);
            }

            int? selectedCountryId = country_id ?? HttpContext.Session.GetInt32("country_id");

            // Fetch states for selected country
            if (selectedCountryId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCountryId", selectedCountryId.Value);
                DTState = GetState(ConnStr, selectedCountryId.Value);
                ViewData["States"] = DTState;
            }

            // Retrieve selected state_id from query parameters or session
            int? selectedStateId = state_id ?? HttpContext.Session.GetInt32("state_id");

            // Fetch cities for selected state
            if (selectedStateId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedStateId", selectedStateId.Value);
                DTCity = GetCity(ConnStr, selectedStateId.Value);
                ViewData["Cities"] = DTCity;
            }

            int? selectedCityId = city_id ?? HttpContext.Session.GetInt32("city_id");
            if (selectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", selectedCityId.Value);
            }
            ViewData["CountryId"] = staff.country_id;
            ViewData["StateId"] = staff.state_id;
            ViewData["CityId"] = staff.city_id;
            DTRole = obj.GetPOSRole(Convert.ToInt32(staff.cmp_id), ConnStr);
            DTVenue = obj.GetVenue(Convert.ToInt32(staff.cmp_id), ConnStr);
            DTAccessBackOfficeRole = obj.GetAccessRole(Convert.ToInt32(staff.cmp_id), ConnStr);

            //int? selectedCountryId = HttpContext.Session.GetInt32("selectedCountryId");
            //if (selectedCountryId.HasValue)
            //{
            //    staff.country_id = selectedCountryId.Value;
            //}
            //int? selectedStateId = HttpContext.Session.GetInt32("selectedStateId");
            //if (selectedStateId.HasValue)
            //{
            //    staff.state_id = selectedStateId.Value;
            //}
            //int? selectedCityId = HttpContext.Session.GetInt32("city_id");
            //if (selectedCityId.HasValue)
            //{
            //    staff.city_id = selectedCityId.Value;

            //}
            if (!ModelState.IsValid)
            {
                // Log the ModelState errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }
            if (staff.staff_id == null || staff.staff_id == 0)
            {
                staff.staff_id = 0;
                obj.Insert(staff, ConnStr, country_id, state_id, city_id);
            }
            else
            {
                obj.Update(staff, ConnStr, country_id, state_id, city_id);
            }

            HttpContext.Session.Remove("staff_id");
            return RedirectToPage("/Configuration/Staff_List");
        }
        //public IActionResult OnPost(int CmpId)
        //{
        //    // Extract countryId from form and convert to int?
        //    if (int.TryParse(Request.Form["country_id"], out var countryId))
        //    {
        //        SelectedCountryId = countryId;
        //    }
        //    else
        //    {
        //        // Handle invalid conversion if necessary
        //        SelectedCountryId = 0; // or another default value or logic
        //    }

        //    // Extract stateId from form and convert to int?
        //    if (int.TryParse(Request.Form["state_id"], out var stateId))
        //    {
        //        SelectedStateId = stateId;
        //    }
        //    else
        //    {
        //        // Handle invalid conversion if necessary
        //        SelectedStateId = null; // or another default value or logic
        //    }

        //    // Store selected values in session
        //    HttpContext.Session.SetInt32("SelectedCountryId", SelectedCountryId);
        //    HttpContext.Session.SetInt32("SelectedStateId", SelectedStateId.GetValueOrDefault());

        //    // Load states and cities based on the new selection
        //    DTState = obj.GetState(Convert.ToInt32(CmpId), ConnStr, countryId);
        //    if (SelectedStateId.HasValue)
        //    {
        //        DTCity = obj.GetCity(Convert.ToInt32(CmpId), ConnStr, SelectedStateId.Value);
        //    }

        //    return Page(); // return to the same page to reflect changes
        //}

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("staff_id");
            return RedirectToPage("/Configuration/Staff_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("staff_id");
            return RedirectToPage("/Configuration/Staff_List");
        }
    }
}
