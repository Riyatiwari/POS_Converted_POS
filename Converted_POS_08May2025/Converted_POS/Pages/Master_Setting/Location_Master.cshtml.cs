using System;
using System.Collections.Generic;
using System.Data;

using Converted_POS.Models;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace Converted_POS.Pages.Master_Setting
{

    public class Location_MasterModel : PageModel
    {


        private readonly IConfiguration _configuration;

        private readonly LocationController obj;
        public Location_MasterModel(IConfiguration configuration)
        {
            _configuration = configuration;
            obj = new LocationController(_configuration);
        }

        [BindProperty]
        public clsLocation location { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> DTTable { get; set; }
        public List<SelectListItem> DTCountry { get; set; }
        public List<SelectListItem> DTState { get; set; }
        public List<SelectListItem> DTCity { get; set; }
        public List<SelectListItem> DTVenue { get; set; }
        public int? SelectedCountryId { get; set; }
        public int? SelectedStateId { get; set; }
        public int? SelectedCityId { get; set; }


        public List<SelectListItem> Index()
        {
            return new List<SelectListItem>
    {
        new SelectListItem { Text = "Select", Value = "0" },
        new SelectListItem { Text = "Judo", Value = "1" },
        new SelectListItem { Text = "Cashflow", Value = "2" },
        new SelectListItem { Text = "CustomPay", Value = "3" },
        new SelectListItem { Text = "CardStream", Value = "4" },
        new SelectListItem { Text = "Positive Payment", Value = "5" }
    };
        }

        public IActionResult OnGet(int? id, int? country_id, int? state_id, int? city_id)
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


                var storeName = HttpContext.Session.GetString("Lstore");


                HttpContext.Session.SetString("location_id", id.Value.ToString());

                location = obj.Select(Convert.ToInt32(cmp_id), id.Value, ConnStr, HttpContext);
                DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
                var wposUrl = _configuration["AppSettings:wpos_URL"];
                string str = wposUrl + "?sv=" + storeName + "&cv=" + cmp_id + "&lv=" + id.Value;

                ViewData["aCLick"] = str.Replace("OptionDelivery", "GroupCategory") + "&dv=0";
                ViewData["aDelivery"] = str.Replace("OptionDelivery", "GroupCategory") + "&dv=1";
                ViewData["aOrder"] = str.Replace("OptionDelivery", "GroupCategory") + "&dv=2";
                ViewData["akiosk"] = str.Replace("OptionDelivery", "GroupCategory") + "&dv=3";

                if (location == null)
                {
                    return NotFound();
                }

                //if (location != null)
                //{
                //    country_id = location.country_id;
                //    state_id = location.state_Id;
                //    city_id = location.city_ID;
                //}
                SelectedCountryId = country_id;
                SelectedStateId = state_id;
                SelectedCityId = city_id;


                location.PaymentMethods = Index();
                if (location.POS_logo == null || location.POS_logo.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64POSImage = ConvertImageToBase64String(location.POS_logo);
                    ViewData["Base64POSImage"] = $"data:image/png;base64,{base64POSImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.POS_logo;
                if (location.Click_Collect_image == null || location.Click_Collect_image.Length == 0)
                {

                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64CACImage = ConvertImageToBase64String(location.Click_Collect_image);
                    ViewData["Base64CACImage"] = $"data:image/png;base64,{base64CACImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.Click_Collect_image;
                if (location.Delivery_image == null || location.Delivery_image.Length == 0)
                {

                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64DeliveryImage = ConvertImageToBase64String(location.Delivery_image);
                    ViewData["Base64DeliveryImage"] = $"data:image/png;base64,{base64DeliveryImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.Delivery_image;
                if (location.OrderAtTable_image == null || location.OrderAtTable_image.Length == 0)
                {

                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64OATImage = ConvertImageToBase64String(location.OrderAtTable_image);
                    ViewData["Base64OATImage"] = $"data:image/png;base64,{base64OATImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.OrderAtTable_image;
                if (location.L_image == null || location.L_image.Length == 0)
                {

                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64ULDbImage = ConvertImageToBase64String(location.GraphicViewBackground);
                    ViewData["Base64ULDBImage"] = $"data:image/png;base64,{base64ULDbImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.GraphicViewBackground;
                if (location.GraphicViewBackground == null || location.GraphicViewBackground.Length == 0)
                {

                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64ULDImage = ConvertImageToBase64String(location.L_image);
                    ViewData["Base64ULDImage"] = $"data:image/png;base64,{base64ULDImage}";  // Store it in ViewData to use in the view
                }
                ViewData["ImageFileName"] = location.L_image;
                //if (SelectedCountryId.HasValue)
                //{
                //    DTState = GetState( ConnStr, SelectedCountryId.Value);
                //}

                //if (SelectedStateId.HasValue)
                //{
                //    DTCity = GetCity( ConnStr, SelectedStateId.Value);
                //}

                // Ensure dropdowns are available for the view
                //DTCountry = obj.GetCountry(Convert.ToInt32(HttpContext.Session.GetString("cmp_id")), ConnStr);


            }


            DTCountry = obj.GetCountry(Convert.ToInt32(cmp_id), ConnStr);
            if (SelectedCountryId.HasValue)
            {
                DTState = GetState(ConnStr, SelectedCountryId.Value);
                HttpContext.Session.SetInt32("selectedCountryId", SelectedCountryId.Value);
            }

            if (SelectedStateId.HasValue)
            {
                DTCity = GetCity(ConnStr, SelectedStateId.Value);
                HttpContext.Session.SetInt32("selectedStateId", SelectedStateId.Value);
            }

            if (SelectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", SelectedCityId.Value);
            }

            // Ensure country dropdown is always available
            Console.WriteLine($"Selected Country ID: {SelectedCountryId}");
            Console.WriteLine($"Selected State ID: {SelectedStateId}");
            Console.WriteLine($"Selected City ID: {SelectedCityId}");

            //location.PaymentMethods = Index();
            if (location == null)
            {

                location = new clsLocation
                {
                    onlinepayment = "0",
                    judo_id = "",
                    judo_token = "",
                    judo_secret = ""
                };

            }
            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr) ?? new List<SelectListItem>();
            //  DTCountry = obj.GetCountry(Convert.ToInt32(cmp_id), ConnStr);
            return Page();

        }
        private string ConvertImageToBase64String(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null; // Handle case where no image is present
            }
            return Convert.ToBase64String(imageBytes);
        }

        [HttpPost("Save")]
        public IActionResult OnPost(IFormFile POSlogoFile, IFormFile DeliveryimageFile, IFormFile ULDimageFile, IFormFile ULDGimageFile, IFormFile CACFile, IFormFile OATimageFile, IFormFile ULDGBimageFile, int? id, int? country_id, int? state_id, int? city_id, string ImageFileName)
        {

            location.location_id = Convert.ToInt32(HttpContext.Session.GetString("location_id"));
            location.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            // If the query parameters are not passed, fall back to session
            if (!country_id.HasValue)
            {
                country_id = HttpContext.Session.GetInt32("selectedCountryId");
            }

            if (!state_id.HasValue)
            {
                state_id = HttpContext.Session.GetInt32("selectedStateId");
            }

            if (!city_id.HasValue)
            {
                city_id = HttpContext.Session.GetInt32("selectedCityId");
            }

            // Save the values to session so they can be used later
            HttpContext.Session.SetInt32("selectedCountryId", country_id ?? 0);
            HttpContext.Session.SetInt32("selectedStateId", state_id ?? 0);
            HttpContext.Session.SetInt32("selectedCityId", city_id ?? 0);

            //if (string.IsNullOrEmpty(SelectedRole))
            //{
            //    ModelState.AddModelError("SelectedRole", "This field is required.");
            //    return Page();
            //} 
            Console.WriteLine($"Selected Payment Method: {location.onlinepayment}");
            Console.WriteLine($"Judo ID: {location.judo_id}, Judo Token: {location.judo_token}, Judo Secret: {location.judo_secret}");
            Console.WriteLine($"Cashflow ID: {location.CashflowId}, Cashflow URL: {location.CashflowUrl}, Cashflow API Key: {location.CashflowApiKey}");
            Console.WriteLine($"CustomPay ID: {location.CustomPay_Id}, CustomPay Token: {location.CustomPay_Token}, CustomPay Secret: {location.CustomPay_Secret}");
            Console.WriteLine($"CardStream ID: {location.CashflowId}, CardStream URL: {location.CashflowUrl}, CardStream API Key: {location.CashflowApiKey}");

            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId");
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId");
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId");

            country_id = SelectedCountryId;
            state_id = SelectedStateId;
            city_id = SelectedCityId;
            //city_id = 155118;
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
            DTVenue = obj.GetVenue(Convert.ToInt32(location.cmp_id), ConnStr) ?? new List<SelectListItem>();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var posLogoResult = ProcessFileUpload(POSlogoFile, location.POS_logo != null ? Convert.ToBase64String(location.POS_logo) : null, "POS_logo");
            var deliveryImageResult = ProcessFileUpload(DeliveryimageFile, location.Delivery_image != null ? Convert.ToBase64String(location.Delivery_image) : null, "Delivery_image");
            var uldImageResult = ProcessFileUpload(ULDimageFile, location.L_image != null ? Convert.ToBase64String(location.L_image) : null, "L_image");
            var graphicViewBackgroundResult = ProcessFileUpload(ULDGBimageFile, location.GraphicViewBackground != null ? Convert.ToBase64String(location.GraphicViewBackground) : null, "GraphicViewBackground");
            var graphicViewTableResult = ProcessFileUpload(ULDGimageFile, location.GraphicViewTable != null ? Convert.ToBase64String(location.GraphicViewTable) : null, "GraphicViewTable");
            var clickCollectImageResult = ProcessFileUpload(CACFile, location.Click_Collect_image != null ? Convert.ToBase64String(location.Click_Collect_image) : null, "Click_Collect_image");
            var orderAtTableImageResult = ProcessFileUpload(OATimageFile, location.OrderAtTable_image != null ? Convert.ToBase64String(location.OrderAtTable_image) : null, "OrderAtTable_image");

            // Assign the processed results back to the location object
            location.POS_logo = posLogoResult.imageData ?? location.POS_logo;
            location.Delivery_image = deliveryImageResult.imageData ?? location.Delivery_image;
            location.L_image = uldImageResult.imageData ?? location.L_image;
            location.GraphicViewBackground = graphicViewBackgroundResult.imageData ?? location.GraphicViewBackground;
            location.GraphicViewTable = graphicViewTableResult.imageData ?? location.GraphicViewTable;
            location.Click_Collect_image = clickCollectImageResult.imageData ?? location.Click_Collect_image;
            location.OrderAtTable_image = orderAtTableImageResult.imageData ?? location.OrderAtTable_image;

            // Assign the file names to ImageFileName
            location.ImageFileName = posLogoResult.fileName ?? clickCollectImageResult.fileName ??
                                     deliveryImageResult.fileName ?? orderAtTableImageResult.fileName ??
                                     uldImageResult.fileName ?? graphicViewBackgroundResult.fileName ??
                                     graphicViewTableResult.fileName;

            if (location.location_id == null || location.location_id == 0)
            {
                location.location_id = 0;
                obj.Insert(location, ConnStr, country_id, state_id, city_id);
                //location.onlinepayment = "0";
            }
            else
            {
                obj.Update(location, ConnStr, country_id, state_id, city_id, location.location_id);
            }
            HttpContext.Session.Remove("location_id");

            return RedirectToPage("/Master_Setting/Location_List");
        }

        private (byte[] imageData, string fileName) ProcessFileUpload(IFormFile file, string existingBase64String, string imageType)
        {
            if (file != null && file.Length > 0)
            {
               
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return (memoryStream.ToArray(), file.FileName);
                }
            }
            else if (!string.IsNullOrEmpty(existingBase64String))
            {
                return (Convert.FromBase64String(existingBase64String.Replace("data:image/png;base64,", "")), null);
            }
            return (null, null);
        }

        private bool IsValidFileType(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".gif", ".png" };
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
            return allowedExtensions.Contains(fileExtension);
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
        public List<SelectListItem> GetCountry(int? c_id, string conn)
        {
            List<SelectListItem> country = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Countries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    country.Add(new SelectListItem
                    {
                        Value = rdr["Country_id"].ToString(),
                        Text = rdr["CountryName"].ToString()
                    });
                }

                con.Close();
            }

            return country;
        }
        [HttpPost("Reset")]
        public ActionResult OnPostReset(int? id, int? country_id, int? state_id, int? city_id)
        {

            HttpContext.Session.Remove("location_id");
            OnGet(id, country_id, state_id, city_id);
            return RedirectToPage("/Master_Setting/Location_Master");

            //if (HttpContext.Session.GetString("location_id") == null)
            //{
            //    Clear();
            //}
            //else
            //{

            //}
            //  return Page();
        }
        public void Clear()
        {
            // Clear session variables if necessary
            HttpContext.Session.Remove("location_id");

            // Reset any other variables or fields here
            ViewData["name"] = null;
            ViewData["code"] = null;
            ViewData["address"] = null;
            ViewData["countryDropdown"] = null;
            ViewData["stateDropdown"] = null;
            ViewData["cityDropdown"] = null;

            // Add other logic to reset your form fields if applicable
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("location_id");
            return RedirectToPage("/Master_Setting/Location_List");
        }
        public JsonResult OnGetGetState(int country_id)
        {
            var states = GetState(ConnStr, country_id);
            return new JsonResult(states);
        }

        public JsonResult OnGetGetCity(int state_id)
        {
            var cities = GetCity(ConnStr, state_id);
            return new JsonResult(cities);
        }

        //[HttpGet]
        //public IActionResult GetState(int? country_id)
        //{
        //    var states = obj.GetState(Convert.ToInt32(cmp_id), ConnStr, country_id);
        //    return new JsonResult(states);
        //}
        //[HttpGet]
        //public IActionResult GetCity(int state_id)
        //{
        //    var cities = obj.GetCity(Convert.ToInt32(cmp_id), ConnStr, state_id);
        //    return new JsonResult(cities);
        //}
        //public IActionResult OnPostStoreSelectedCountry(int countryId)
        //{
        //    HttpContext.Session.SetInt32("SelectedCountryId", countryId);
        //    return new JsonResult(new { success = true });
        //}

    }
}
