using Microsoft.AspNetCore.Mvc;
using Converted_POS.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Converted_POS.Controllers
{
    public class CompanyController : Controller
    {
        public string ConnStr { get; set; }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("Storename", "YourStoreName");
            return View();
        }

        public void Update(clsCompany company, string connStr)
        {
            throw new NotImplementedException();
        }

        public void Insert(clsCompany company, string connStr, string storeName, string code, string Domain, string Vat_No, string Receipt_Header, int? selectedCountryId)
        {
            DateTime startingDate = company.Starting_Date;
            //if (startingDate == DateTime.MinValue)
            //{
            //    startingDate = selectedDatePickerDate;
            //}
            //else if (startingDate < SqlDateTime.MinValue.Value)
            //{
            //    // If the company starting date is less than SqlDateTime.MinValue, set it to SqlDateTime.MinValue
            //    startingDate = SqlDateTime.MinValue.Value;
            //}
            //var storeName = HttpContext.Request.HttpContext.Session.GetString("Storename");
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                //if (IsDomainExists(Domain, connStr))
                //{
                //    throw new Exception("Domain name already exists. Please use another domain.");
                //}
                SqlCommand cmd = new SqlCommand("P_M_Company", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Company_id", company.Company_id);
                cmd.Parameters.AddWithValue("@Name", storeName);
                cmd.Parameters.AddWithValue("@Domain", company.Domain);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Vat_No", Vat_No);
                cmd.Parameters.AddWithValue("@Receipt_Header", Receipt_Header);
                cmd.Parameters.Add("@Starting_Date", SqlDbType.DateTime).Value = startingDate;
                cmd.Parameters.AddWithValue("@Country", company.Country);
                cmd.Parameters.AddWithValue("@State", company.State);
                cmd.Parameters.AddWithValue("@City", company.City);
                //cmd.Parameters.AddWithValue("@IP_Address", company.IP_Address);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }   
    }
}
