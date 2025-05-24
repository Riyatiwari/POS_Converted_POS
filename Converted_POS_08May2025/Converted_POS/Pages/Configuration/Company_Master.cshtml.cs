using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Converted_POS.Controllers;
using Converted_POS.Models;
using System.Web;
using System.Data;

namespace Converted_POS.Pages.forms
{
    public class Company_MasterModel : PageModel
    {
        CompanyController obj = new CompanyController();
        [BindProperty]
        public clsCompany company { get; set; }
        public string ConnStr { get; set; }
        public string Domain { get; set; }
        public string cmp_id { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> CurrencyList { get; set; }
        public int? SelectedCountryId { get; set; }
        public int? SelectedStateId { get; set; }
        public int? SelectedCityId { get; set; }
        public IActionResult OnGet(int? id, int? country_id, int? state_id, int? city_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");
            company = new clsCompany();
            //company.Domain = HttpContext.Request.HttpContext.Session.GetString("Domain");
            //ProductList = GetProducts(ConnStr);
            CurrencyList = GetCurrency(ConnStr);

            CountryList = GetCountriesAsync(ConnStr);
            //int? selectedCountryId = country_id ?? HttpContext.Session.GetInt32("country_id");
            if (SelectedCountryId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCountryId", SelectedCountryId.Value);
                StateList = GetStates(ConnStr, SelectedCountryId.Value);
            }
            else
            {
                StateList = new List<SelectListItem>(); // Prevent null reference in view
            }

            // Fetch states for selected country
            if (SelectedStateId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedStateId", SelectedStateId.Value);
                CityList = GetCities(ConnStr, SelectedStateId.Value);
            }
            else
            {
                CityList = new List<SelectListItem>(); // Prevent null reference in view
            }
            if (SelectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", SelectedCityId.Value);
            }
            // Fetch states for selected country
            if (SelectedCountryId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCountryId", SelectedCountryId.Value);
                StateList = GetStates(ConnStr, SelectedCountryId.Value);
            }

            // Retrieve selected state_id from query parameters or session
            int? selectedStateId = state_id ?? HttpContext.Session.GetInt32("state_id");

            // Fetch cities for selected state
            if (selectedStateId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedStateId", selectedStateId.Value);
                CityList = GetCities(ConnStr, selectedStateId.Value);
            }

            int? selectedCityId = city_id ?? HttpContext.Session.GetInt32("city_id");
            if (selectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", selectedCityId.Value);

            }
            ViewData["CountryId"] = SelectedCountryId;
            ViewData["StateId"] = SelectedStateId;
            ViewData["CityId"] = SelectedCityId;
            return Page();
        }

        public void ClearAllData(int cmp_id)
        {
            //string connStr = "Data Source=TMS-LA04\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";
            ConnStr = HttpContext.Session.GetString("conString");
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("ClearAll_data", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", cmp_id);

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            // Redirect or return to another action as needed
            //return RedirectToPage("/Configuration/Company_Master");
        }
        private List<SelectListItem> GetCurrency(string connStr)
        {
            List<SelectListItem> currency = new List<SelectListItem>();
            try
            {
                //string constr = "Data Source=TMS-LA04\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";

                using (SqlConnection con = new SqlConnection(ConnStr))
                {
                    con.Open();
                    //string query = "SELECT  City_id,CityName FROM M_Cities where State_Id =@state_id  order by CityName";
                    using (SqlCommand cmd = new SqlCommand("Get_M_Currency", con))
                    {
                        //cmd.Parameters.AddWithValue("@state_id", state_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int currencyId = Convert.ToInt32(reader["Currency_Id"]);
                                string currencyName = reader["Currency"].ToString();
                                currency.Add(new SelectListItem
                                {
                                    Value = currencyId.ToString(),
                                    Text = currencyName
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving states from database: " + ex.Message);
            }
            return currency;
        }

        private List<SelectListItem> GetCities(string connStr, int state_id)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            try
            {
                //string constr = "Data Source=TMS-LA04\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";

                using (SqlConnection con = new SqlConnection(ConnStr))
                {
                    con.Open();
                    string query = "SELECT  City_id,CityName FROM M_Cities where State_Id =@state_id  order by CityName";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@state_id", state_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int cityId = Convert.ToInt32(reader["City_id"]);
                                string cityName = reader["CityName"].ToString();
                                cities.Add(new SelectListItem
                                {
                                    Value = cityId.ToString(),
                                    Text = cityName
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine("Error retrieving states from database: " + ex.Message);
            }
            //ViewBag.CityList = cities;
            return cities;
        }

        private List<SelectListItem> GetStates(string connStr, int country_id)
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

        private List<SelectListItem> GetCountriesAsync(string connStr)
        {
            Dictionary<int, List<SelectListItem>> statesDictionary = new Dictionary<int, List<SelectListItem>>();
            List<SelectListItem> list = new List<SelectListItem>();

            //string constr = "Data Source=TMS-LA04\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Get_Countries", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int countryId = Convert.ToInt32(dr["Country_id"]); // Extract country_id
                            list.Add(new SelectListItem
                            {
                                Value = countryId.ToString(),
                                Text = dr["CountryName"].ToString(),

                            });

                            //List<SelectListItem> statesForCountry = GetStatesForCountry(countryId, constr);
                            //statesDictionary.Add(countryId, statesForCountry);
                        }
                    }
                }
            }
            //ViewBag.CountryList = list;
            return list;
        }


        [HttpPost("Save")]
        public IActionResult OnPost(int? country_id, int? state_id, int? city_id)
        {
            ConnStr = HttpContext.Session.GetString("conString");

            SelectedCountryId = country_id ?? HttpContext.Session.GetInt32("selectedCountryId");
            SelectedStateId = state_id ?? HttpContext.Session.GetInt32("selectedStateId");
            SelectedCityId = city_id ?? HttpContext.Session.GetInt32("selectedCityId");

            country_id = SelectedCountryId;
            state_id = SelectedStateId;
            city_id = SelectedCityId;


            CountryList = GetCountriesAsync(ConnStr);
            //int? selectedCountryId = HttpContext.Session.GetInt32("selectedCountryId");
            if (SelectedCountryId.HasValue)
            {
                // Use selectedCountryId as needed
                // For example, set company.Country to selectedCountryId
                //company.Country = selectedCountryId.Value;
                StateList = GetStates(ConnStr, SelectedCountryId.Value);
            }
            //int? selectedStateId = HttpContext.Session.GetInt32("selectedStateId");
            if (SelectedStateId.HasValue)
            {
                // Use selectedCountryId as needed
                // For example, set company.Country to selectedCountryId
                //company.State = selectedStateId.Value;
                CityList = GetCities(ConnStr, SelectedStateId.Value);
            }

            int? selectedCountryId = country_id ?? HttpContext.Session.GetInt32("country_id");

            if (selectedCountryId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCountryId", selectedCountryId.Value);
                StateList = GetStates(ConnStr, selectedCountryId.Value);
                ViewData["States"] = StateList;
            }

            int? selectedStateId = state_id ?? HttpContext.Session.GetInt32("state_id");

            // Fetch cities for selected state
            if (selectedStateId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedStateId", selectedStateId.Value);
                CityList = GetCities(ConnStr, selectedStateId.Value);
                ViewData["Cities"] = CityList;
            }

            //int? selectedCityId = HttpContext.Session.GetInt32("city_id");
            int? selectedCityId = city_id ?? HttpContext.Session.GetInt32("city_id");
            if (selectedCityId.HasValue)
            {
                HttpContext.Session.SetInt32("selectedCityId", selectedCityId.Value);
            }
            ViewData["CountryId"] = company.Country;
            ViewData["StateId"] = company.State;
            ViewData["CityId"] = company.City;
            string selectedDateStr = Request.Form["hiddenStartDate"];
            if (DateTime.TryParse(selectedDateStr, out DateTime selectedDate))
            {
                // Set the company's Starting_Date property to the selected date/time
                company.Starting_Date = selectedDate;

                // Now you can proceed with database insertion using ADO.NET
                //Insert(company);
            }
            var storeName = HttpContext.Request.HttpContext.Session.GetString("Storename");
            var code = HttpContext.Request.HttpContext.Session.GetString("Storename");
            var Domain = (('@') + "jwtest"); /*$"@{jwt}";*/
            var Vat_No = 1234567890;
            var Receipt_Header = HttpContext.Request.HttpContext.Session.GetString("Storename");
            //int? selectedCountryId = country_id ?? HttpContext.Session.GetInt32("country_id");
            //int country = Convert.ToInt32(HttpContext.Request.HttpContext.Session.GetString("Country_id"));
            company.Domain = Domain;
            company.Company_id = Convert.ToInt32(HttpContext.Session.GetString("Company_id"));

            ConnStr = HttpContext.Session.GetString("conString");
            string formattedStartDate = "";
            if (DateTime.TryParseExact(formattedStartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startingDate))
            {
                company.Starting_Date = startingDate;
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (company.Company_id == null || company.Company_id == 0)
            {
                company.Company_id = 0;
                //string companyName = Request.Form["CompanyName"];
                //clsCompany company = new clsCompany();
                //company.Name = companyName;
                //if (IsDomainExists(Domain, ConnStr))
                //{
                //    ModelState.AddModelError(string.Empty, "Domain name already exists. Please use another domain.");
                //    return Page();
                //}
                obj.Insert(company, ConnStr, storeName, code, Domain, Vat_No.ToString(), Receipt_Header, selectedCountryId);
            }
            else
            {
                obj.Update(company, ConnStr);
            }
            if (Request.Form["ClearAllData"] == "Clear All Data")
            {
                int cmpId = 0;
                if (int.TryParse(cmp_id, out cmpId))
                {
                    ClearAllData(cmpId); // Invoke ClearAllData method with the integer value
                    //return (ActionResult)OnPostClearAllData();
                }

                // Optionally, you might want to clear other session variables or take additional actions here.
            }

            HttpContext.Session.Remove("Company_id");
            return RedirectToPage("/Configuration/Company_Master");
        }
        private bool IsDomainExists(string Domain, string connStr)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM M_Company WHERE Domain = @Domain", con);
                cmd.Parameters.AddWithValue("@Domain", Domain);
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;
            }
        }
        public IActionResult OnPostClearAllData(int cmpId)
        {
            ClearAllData(cmpId);
            return RedirectToPage("/Configuration/Company_Master");
        }
        //[HttpPost("Reset")]
        public IActionResult OnPostReset()
        {
            HttpContext.Session.Remove("Company_id");
            return RedirectToPage("/Configuration/Company_Master");
        }

        //[HttpPost("Cancel")]
        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("Company_id");
            return RedirectToPage("/Configuration/Dashboard");
        }
    }
}
