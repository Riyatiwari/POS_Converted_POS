using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Converted_POS.Controls
{
    public class menuaccordian
    {


        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public menuaccordian(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MenuItem> GetMenuDataFromStoredProcedure()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Session == null)
            {
                return menuItems;
            }
            var connectionString = "Data Source=" + httpContext.Session.GetString("db_server") + ";" +
                                       "Initial Catalog=" + httpContext.Session.GetString("db_name") + ";" +
                                       "User ID=" + httpContext.Session.GetString("user_name") + ";" +
                                       "Password=" + httpContext.Session.GetString("password");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_Load_Form_core", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Role_Id", Convert.ToInt32(httpContext.Session.GetString("staff_role_id")));
                    cmd.Parameters.AddWithValue("@Type", 0);
                    cmd.Parameters.AddWithValue("@FullAccess", 0);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //int formId = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("Form_id")));
                            //int formId = (int)(decimal)reader["Form_id"];

                            ////int formId = reader.GetInt32(reader.GetOrdinal("Form_id"));
                            //string url = reader.GetString(reader.GetOrdinal("URL"));
                            //string name = reader.GetString(reader.GetOrdinal("Alias"));
                            //int formIdOrdinal = reader.GetOrdinal("Form_id");
                            //int nameOrdinal = reader.GetOrdinal("Form_Name");
                            //int aliasOrdinal = reader.GetOrdinal("Alias");
                            //int parentOrdinal = reader.GetOrdinal("Parent_id");
                            //int sortingNoOrdinal = reader.GetOrdinal("Sorting_No");
                            //int urlOrdinal = reader.GetOrdinal("url");


                            int formId = Convert.ToInt32(reader["Form_id"]);
                            int? parentId = reader["Parent_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Parent_id"]);
                            string formName = reader["alias"].ToString();
                            string alias = reader["Alias"].ToString();

                            int sortingNo = Convert.ToInt32(reader["Sorting_No"]);
                            string url = reader["url"].ToString();
                            string image = reader["image"].ToString();




                            MenuItem menuItem = new MenuItem
                            {

                                Form_id = formId,
                                Form_Name = formName,
                                Alias = alias,
                                Parent_id = parentId,
                                Sorting_No = sortingNo,
                                url = url,
                                SubMenuItems = null,
                                image = image
                            };

                            menuItems.Add(menuItem);
                        }
                    }
                }
            }

            return menuItems;
        }



        public class MenuItem
        {
            public int Form_id { get; set; }
            public string Form_Name { get; set; }
            public int? Parent_id { get; set; }  //
            public int Sorting_No { get; set; }
            public string Alias { get; set; }
            public string url { get; set; }
            public string image { get; set; }
            public List<MenuItem> SubMenuItems { get; set; }
        }



        public class MenuData
        {
            public string MenuName { get; set; }
            public List<SubMenuData> SubMenus { get; set; }
        }

        public class SubMenuData
        {
            public string SubMenuName { get; set; }
            public string SubMenuLink { get; set; }
        }
    }
}
