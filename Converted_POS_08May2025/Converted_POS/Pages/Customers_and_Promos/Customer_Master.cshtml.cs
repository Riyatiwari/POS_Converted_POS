using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages.forms
{
    public class Customer_MasterModel : PageModel
    {
        CustomerController obj = new CustomerController();

        [BindProperty]
        public clsCustomer customer { get; set; }
        public List<SelectListItem> DTProfile { get; set; }
        public List<SelectListItem> DTPriceLevel { get; set; }
        public List<SelectListItem> DTCountry { get; set; }
        public List<SelectListItem> DTState { get; set; }
        public List<SelectListItem> DTCity { get; set; }
        public int? SelectedCountryId { get; set; }
        public int? SelectedStateId { get; set; }
        public int? SelectedCityId { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }

        public ActionResult OnGet(int? id, int? country_id, int? state_id, int? city_id)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId");
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId");
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId");

            if (id.HasValue)
            {
                //HttpContext.Session.SetString("staff_id", id.Value.ToString());
                //staff = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr);
                HttpContext.Session.SetString("customer_id", HttpContext.Request.Query["ID"].ToString());
                customer = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr, country_id, state_id);
                if (customer == null)
                {
                    return NotFound();
                }
                if (customer.CustomerProfile == null || customer.CustomerProfile.Length == 0)
                {
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    string base64Image = ConvertImageToBase64String(customer.CustomerProfile);
                    ViewData["Base64Image"] = $"data:image/png;base64,{base64Image}";  // Store it in ViewData to use in the view
                }
            }
            DTCountry = obj.GetCountry(Convert.ToInt32(cmp_id), ConnStr);
            if (SelectedCountryId.HasValue)
            {
                DTState = GetState(ConnStr, SelectedCountryId.Value);
            }
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

            int? selectedStateId = state_id ?? HttpContext.Session.GetInt32("state_id");

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

            DTProfile = obj.GetProfile(Convert.ToInt32(cmp_id), ConnStr);
            DTPriceLevel = obj.GetPriceLevel(Convert.ToInt32(cmp_id), ConnStr);
            //if (id == null)
            //{
            //    return Page();
            //}
            //else
            //{
            //    HttpContext.Session.SetString("customer_id", HttpContext.Request.Query["ID"].ToString());
            //    customer = obj.Select(Convert.ToInt32(cmp_id), id, ConnStr);

            //    if (customer == null)
            //    {
            //        return NotFound();
            //    }
            //if (customer.Aimage == null || customer.Aimage.Length == 0)
            //{
            //    // Log or set a breakpoint here to inspect DType
            //    Console.WriteLine("DType.Aimage is null or empty.");
            //}
            //else
            //{
            //    // Convert the image byte array to Base64 string for display
            //    string base64Image = ConvertImageToBase64String(allergy.Aimage);
            //    ViewData["Base64Image"] = $"data:image/png;base64,{base64Image}";  // Store it in ViewData to use in the view
            //}

            return Page();
        }

        private string ConvertImageToBase64String(byte[] customerProfile)
        {
            if (customerProfile == null || customerProfile.Length == 0)
            {
                return null; // Handle case where no image is present
            }
            return Convert.ToBase64String(customerProfile);
        }

        //}

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
        [HttpPost("Save")]
        public ActionResult OnPost(IFormFile CustomerProfileFile, int? id, int? country_id, int? state_id, int? city_id, string ImageFileName)
        {
            customer.customer_id = Convert.ToInt32(HttpContext.Session.GetString("customer_id"));
            customer.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            DTCountry = obj.GetCountry(Convert.ToInt32(cmp_id), ConnStr);
            DTState = obj.GetState(Convert.ToInt32(cmp_id), ConnStr, country_id);
            DTCity = obj.GetCity(Convert.ToInt32(cmp_id), ConnStr, state_id);
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId");
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId");
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId");

            country_id = SelectedCountryId;
            state_id = SelectedStateId;
            city_id = SelectedCityId;

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

            //if (AimageFile != null && AimageFile.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        AimageFile.CopyToAsync(memoryStream); // Asynchronously copy the file
            //        customer.Aimage = memoryStream.ToArray(); // Convert to byte array
            //        customer.ImageFileName = AimageFile.FileName; // Store the file name
            //    }
            //}
            //else
            //{
            //    customer.Aimage = null; // Handle no image uploaded
            //    customer.ImageFileName = null; // Handle no file name
            //}
            if (CustomerProfileFile != null && CustomerProfileFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    CustomerProfileFile.CopyToAsync(memoryStream); // Asynchronously copy the file
                    customer.CustomerProfile = memoryStream.ToArray(); // Convert to byte array
                    customer.ImageFileName = CustomerProfileFile.FileName; // Store the file name
                }
            }
            else
            {
                //customer.CustomerProfile = null; // Handle no image uploaded
                //customer.ImageFileName = null; // Handle no file name
                if (!string.IsNullOrEmpty(ImageFileName))
                {
                    // Decode the base64 image data and save it
                    byte[] existingImage = Convert.FromBase64String(ImageFileName.Replace("data:image/png;base64,", ""));
                    customer.CustomerProfileFile = existingImage;
                }
                else
                {
                    customer.CustomerProfileFile = null; // If no image exists (i.e., no base64 string), set it to null
                }
            }
            if (customer.customer_id == null || customer.customer_id == 0)
            {
                customer.customer_id = 0;
                obj.Insert(customer, ConnStr, country_id, state_id, city_id);
            }
            else
            {
                obj.Update(customer, ConnStr, country_id, state_id, city_id);
            }

            HttpContext.Session.Remove("customer_id");
            return RedirectToPage("/Customers_and_Promos/Customer_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("customer_id");
            return RedirectToPage("/Customers_and_Promos/Customer_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("customer_id");
            return RedirectToPage("/Customers_and_Promos/Customer_List");
        }
    }
}
