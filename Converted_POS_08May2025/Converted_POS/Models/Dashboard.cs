using Converted_POS.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class Dashboard 
    {
        public int cmpId { get; set; }
        public String cmp_name { get; set; }
        public string ConnStr { get; set; }
        public int SelectedVenueId { get; set; }

        [BindProperty]
        public List<SelectListItem> Venues { get; set; }

        //public string venue_name { get; set; }
        //public int venue_id { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Dashboard(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }



        public DataTable SelectVenueByCmp(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<Dashboard> list = new List<Dashboard>();

                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmpId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                return dt;
            }
        }


        public List<SelectListItem> GetVenues(int companyId, string connectionString)
        {
            List<SelectListItem> venues = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", companyId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string venueId = rdr["venue_id"].ToString();
                    string venueName = rdr["venue_name"].ToString();
                    venues.Add(new SelectListItem { Value = venueId, Text = venueName });
                }
            }

            return venues;
        }
        public async Task<object> GetDepartmentChartDataAsync(DateTime date1, DateTime date2, int venue_id, string duration)
        {
            try
            {
                var chartData = new List<object>();
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null)
                    throw new InvalidOperationException("HttpContext is null");

                var connectionString = "Data Source=" + httpContext.Session.GetString("db_server") + ";" +
                                        "Initial Catalog=" + httpContext.Session.GetString("db_name") + ";" +
                                        "User ID=" + httpContext.Session.GetString("user_name") + ";" +
                                        "Password=" + httpContext.Session.GetString("password");

                using (var con = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand("GetDepartmentChartData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@date1", date1);
                        cmd.Parameters.AddWithValue("@date2", date2);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@venue_id", venue_id);

                        await con.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var departmentName = reader["DepartmentName"].ToString();
                                var dateValue = reader["DayOfWeek"].ToString();
                                var value = Convert.ToDecimal(reader["Value"]);

                                var datasetObject = new
                                {
                                    label = departmentName,
                                    data = new object[] { dateValue, value }
                                };

                                chartData.Add(datasetObject);
                            }
                        }
                    }
                }

                return chartData;
            }
            catch (Exception ex)
            {
               // LogHelper.Error("Page_Load:BindChartData:" + ex.Message);
                throw; // Re-throwing the exception to handle it elsewhere
            }
        }



    }
}
