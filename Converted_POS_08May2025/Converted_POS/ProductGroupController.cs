using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class ProductGroupController : Controller
    {
        //public static IConfiguration _configuration;
        //string connectionString = _configuration.GetConnectionString("POS_Connection");
        private readonly ILogger<ProductGroupController> _logger;
        [BindProperty]
        public clsProductGroup DTypePG { get; set; }
        public ProductGroupController(ILogger<ProductGroupController> logger)
        {
            _logger = logger;
        }

        public ProductGroupController()
        {
        }

        public List<SelectListItem> Index(int cmp_id, int? id, string conn)
        {
            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl = PopulateFruits(conn, cmp_id);
            //List<clsProductGroupMain> result = new List<clsProductGroupMain>();
            //result.Add(ddl);

            return ddl;

        }

        public IActionResult AssignTill(string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("AssignTill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
            //HttpContext.Session.SetString("Message", "Till assigned successfully"); // Store message in session
            return RedirectToAction("Index"); // Redirect to the desired action or page
        }


        [HttpPost]
        public ActionResult Index(clsProductGroup ddl, string conn, int cmp_id)
        {
            ddl.ListCat = PopulateFruits(conn, cmp_id);
            var selectedItem = ddl.ListCat.Find(p => p.Value == ddl.category_id.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                ViewBag.Message = "Fruit: " + selectedItem.Text;
                ViewBag.Message += "\\nQuantity: " + ddl.name;
            }

            return View(ddl);
        }

        private List<SelectListItem> PopulateFruits(string conn, int cmp_id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //string constr = "Data Source=TMS-10\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";
            using (SqlConnection con = new SqlConnection(conn))
            {
                //string query = "select maincategory_id, name from M_MainCategory where is_active = 1";

                SqlCommand cmd = new SqlCommand("Get_Main_Category_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmp_id);


                //cmd.Connection = con;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //using (SqlDataReader dr = cmd.ExecuteReader())
                //    {
                while (dr.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = dr["category_id"].ToString(),
                        Text = dr["name"].ToString()
                    });
                }

                //}
                con.Close();
                //}
            }
            ViewBag.ddl = list;
            //TempData["PrdDDL"] = list;
            return list;
        }

        public List<clsProductGroup> SelectAll(int? c_id, string conn)
        {
            List<clsProductGroup> listItems = new List<clsProductGroup>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProductGroup prd = new clsProductGroup();

                    prd.category_id = Convert.ToInt32(rdr["category_id"]);
                    //prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
                    prd.name = rdr["name"].ToString();
                    prd.categoryName = rdr["cname"].ToString();
                    prd.description = rdr["description"].ToString();
                    //prd.active = rdr["Active"].ToString();
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        prd.is_active = true;
                    }
                    else
                    {
                        prd.is_active = false;
                    }

                    listItems.Add(prd);
                }
                con.Close();
            }

            //clsProductGroupMain list = TempData["MyList"] as clsProductGroupMain;
            //ViewBag.ddl = new SelectList(listItems);
            return listItems;
        }
        public List<clsProductGroup> SelectM(int? c_id, string conn)
        {

            List<clsProductGroup> list = new List<clsProductGroup>();
            //string jsonList = JsonConvert.SerializeObject(list);

            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("Get_M_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@maincategory_id", maincategory_id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProductGroup prd = new clsProductGroup();

                    prd.category_id = Convert.ToInt32(rdr["category_id"]);
                    //prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
                    prd.name = rdr["name"].ToString();
                    prd.description = rdr["description"].ToString();
                    //prd.active = rdr["Active"].ToString();
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        prd.is_active = true;
                    }
                    else
                    {
                        prd.is_active = false;
                    }
                    list.Add(prd);
                }

                con.Close();
            }
            ViewData["MainList"] = list;
            ViewBag.DropDown = new SelectList(list);
            //            Public TempDataDictionary TempData { get; set; }
            //            Public class TempDataDictionary : IDictionary<string, object="">
            //</string,>
            //TempData["mylist"] = list;
            //list = TempData["mylist"] as List<clsProductGroupMain>;
            //ViewState["MyList"] = list;
            return list;
            //return (IEnumerable<clsProductGroupMain>)RedirectToPage("/BackOffice/Product_Group_Master");
        }

        public List<SelectListItem> GetGroupCat(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Main_Category_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["category_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }
                con.Close();
            }
            return categories;
        }

        internal List<string> GetMachineDetail(int cmpID, string connStr)
        {

            var machines = new List<string>();

            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("Get_Machine_Detail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cmp_id", cmpID);
                    //command.Parameters.AddWithValue("@Category_id", categoryId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var machine = new clsProductGroup
                            {
                                //category_id = reader.GetInt32(reader.GetOrdinal("Category_id")),
                                machine_id = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("Machine_id"))),
                                name = reader.GetString(reader.GetOrdinal("Machine"))
                                //machines.Add(name);
                            };
                            machines.Add(machine.name);
                        }
                    }
                }
            }
            return machines;
        }

        public List<int> GetSelectedMachineIds(int? cmpID, int? CategoryId, string conn, List<int> selectedMachineIds)
        {
            var machineIds = new List<int>();
            //    var selectedMachineIds = Request.Form["DTypePG.SelectedMachineIds"]
            //.Select(int.Parse)
            //.ToList();
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (var cmd = new SqlCommand("Get_Machine_By_Category", con))
                {
                    //SqlCommand cmd = new SqlCommand("Get_Machine_By_Category", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Category_id", CategoryId);
                    cmd.Parameters.AddWithValue("@cmp_id", cmpID);
                    con.Open();
                    //using (SqlDataReader rdr = cmd.ExecuteReader())
                    //{
                    //    while (rdr.Read())
                    //    {
                    //        machineIds.Add(Convert.ToInt32(rdr["machine_id"]));
                    //    }
                    //}
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int machineId = Convert.ToInt32(reader["Machine_id"]);
                            machineIds.Add(machineId);
                            //if (selectedMachineIds.Contains(machineId))
                            //{
                            //    machineIds.Add(machineId);
                            //}
                        }
                    }
                    //con.Close();
                }
            }

            if (selectedMachineIds != null && selectedMachineIds.Count > 0)
            {
                machineIds.AddRange(selectedMachineIds);
            }

            // Remove duplicates if necessary
            return selectedMachineIds;
        }

        public object GetMachinesByCategory(string cmpID, int categoryId, string connStr)
        {
            var machines = new List<clsProductGroup>();

            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("Get_Machine_By_Category", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cmp_id", cmpID);
                    command.Parameters.AddWithValue("@Category_id", categoryId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var machine = new clsProductGroup
                            {
                                category_id = reader.GetInt32(reader.GetOrdinal("Category_id")),
                                machine_id = reader.GetInt32(reader.GetOrdinal("Machine_id"))
                            };
                            machines.Add(machine);
                        }
                    }
                }
            }
            return machines;
        }

        //public void AssociateMachineWithProductGroup(int? categoryId, int machineId, string conn, string cmpID)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_Machine_By_Category", con);

        //        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
        //        cmd.Parameters.AddWithValue("@MachineId", machineId);
        //        cmd.Parameters.AddWithValue("@cmp_id", cmpID);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        public int GenerateSortingNumber(int cmpId, string formName, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Autogenerate_Sorting_No_Master", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cmp_id", cmpId);
                    command.Parameters.AddWithValue("@form_name", formName);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 1; // Default to 1 if null
                }
            }
        }
        public List<clsProductGroup> GetMachineDetail(string cmpID, int categoryId, string connStr, int machine_id)
        {
            var machines = new List<clsProductGroup>();

            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("Get_Machine_Detail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cmp_id", cmpID);
                    //command.Parameters.AddWithValue("@Category_id", categoryId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var machine = new clsProductGroup
                            {
                                //category_id = reader.GetInt32(reader.GetOrdinal("Category_id")),
                                machine_id = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("Machine_id"))),
                                Machine = reader.GetString(reader.GetOrdinal("Machine"))
                                //machines.Add(name);
                            };
                            machines.Add(machine);
                        }
                    }
                }
            }
            ViewBag.Machines = machines;

            // If you want to store just machine_id and Machine as separate lists
            ViewBag.MachineIds = machines.Select(m => m.machine_id).ToList();
            ViewBag.MachineNames = machines.Select(m => m.Machine).ToList();
            return machines;

        }

        public List<int> GetSaveMachine(string cmpID, string ConnStr, int Category_Detail_id, int? category_id, List<int> machineIds, int login_id, string ip_address)
        {
            var savedMachineIds = new List<int>();

            using (var connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                foreach (var machine_id in machineIds)
                {
                    using (var command = new SqlCommand("P_M_Category_Details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cmp_id", cmpID);
                        command.Parameters.AddWithValue("@Category_Detail_id", 0);
                        command.Parameters.AddWithValue("@Category_id", category_id);
                        command.Parameters.AddWithValue("@Machine_id", machine_id);
                        command.Parameters.AddWithValue("@login_id", login_id);
                        command.Parameters.AddWithValue("@ip_address", "::1");
                        command.Parameters.AddWithValue("@Tran_Type", "I");

                        //connection.Open();
                        command.ExecuteNonQuery();
                        savedMachineIds.Add(machine_id);

                        //command.CommandText = "Get_Machine_By_Category"; // Adjust this as necessary
                        //command.Parameters.Clear(); // Clear existing parameters
                        //command.Parameters.AddWithValue("@Machine_id", machine_id);
                        //command.Parameters.AddWithValue("@cmp_id", cmpID);
                        //command.Parameters.AddWithValue("@Category_id", categoryId);
                        //using (var reader = command.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        var machine = new clsProductGroup
                        //        {
                        //            machine_id = Convert.ToInt32(reader["Machine_id"]),
                        //            Machine = reader["Machine"].ToString()
                        //        };
                        //        machines.Add(machine);
                        //    }
                        //}
                        //using (var reader = command.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        var machine = new clsProductGroup
                        //        {
                        //            //category_id = reader.GetInt32(reader.GetOrdinal("Category_id")),
                        //            machine_id = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("Machine_id"))),
                        //            Machine = reader.GetString(reader.GetOrdinal("Machine"))
                        //            // machines.Add(name);

                        //        };
                        //        machines.Add(machine);
                        //    }
                        //}
                    }
                }
            }
            //var savedMachineIdsString = HttpContext.Session.GetString("SavedMachineIds");
            //return savedMachineIdsString != null ? JsonSerializer.Deserialize<List<int>>(savedMachineIdsString) : new List<int>();

            return savedMachineIds;
        }
        public clsProductGroup Select(int? c_id, int? id, string conn)
        {
            clsProductGroup prd = new clsProductGroup
            {
                SelectedMachineIds = new List<int>() // Initialize the list to store machine IDs
            };
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@category_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prd.category_id = Convert.ToInt32(rdr["category_id"]);
                    //HttpContext.Session.SetString("category_id", prd.category_id.ToString());
                    prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
                    prd.categoryName = rdr["cname"].ToString();
                    prd.name = rdr["name"].ToString();
                    prd.description = rdr["description"].ToString();
                    prd.web_sales_description = rdr["web_sales_description"].ToString();
                    prd.sorting_no = Convert.ToInt32(rdr["sorting_no"]);
                    //prd.machine_id = Convert.ToInt32(rdr["machine_id"].ToString());
                    if (!rdr.IsDBNull(rdr.GetOrdinal("machine_id")))
                    {
                        int machineId = Convert.ToInt32(rdr["machine_id"]);
                        prd.SelectedMachineIds.Add(machineId); // Add to the list
                    }
                    else
                    {
                        Console.WriteLine("Machine ID is null or not found.");
                    }
                    prd.Aimage = rdr["Aimage"] as byte[];
                    // prd.active = rdr["is_active"].ToString();

                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        prd.is_active = true;
                    }
                    else
                    {
                        prd.is_active = false;
                    }
                    if (rdr["click_and_collect"].ToString() == "Yes")
                    {
                        prd.click_and_collect = true;
                    }
                    else
                    {
                        prd.click_and_collect = false;
                    }
                    if (rdr["Order_at_table"].ToString() == "Yes")
                    {
                        prd.Order_at_table = true;
                    }
                    else
                    {
                        prd.Order_at_table = false;
                    }
                    if (rdr["deliver"].ToString() == "Yes")
                    {
                        prd.deliver = true;
                    }
                    else
                    {
                        prd.deliver = false;
                    }
                }
                con.Close();
            }
            return prd;
        }
        public void Insert(clsProductGroup prd, string conn, string ip_address, List<int> machineIds, int? selectedCategoryId)
        {
            prd.ip_address = ip_address;

            //int key_map_id = 0;
            //List<int> insertedMachineIds = new List<int>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open(); // Open the connection once at the beginning

                using (SqlCommand cmd = new SqlCommand("P_M_Category", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                    cmd.Parameters.AddWithValue("@machine_id", 0); // Change here
                    cmd.Parameters.AddWithValue("@category_id", selectedCategoryId);
                    cmd.Parameters.AddWithValue("@name", prd.name); // Ensure no null value
                    cmd.Parameters.AddWithValue("@main_category_id", prd.maincategory_id);
                    cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
                    cmd.Parameters.AddWithValue("@is_active", prd.is_active);
                    cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                    cmd.Parameters.AddWithValue("@description_sales", prd.web_sales_description);
                    cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                    cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                    cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                    cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                    cmd.Parameters.AddWithValue("@key_map_id", 0); // Assuming key_map_id is always 0

                    cmd.Parameters.AddWithValue("@description", prd.description ?? string.Empty); // Handle nulls
                    cmd.Parameters.Add("@Aimage", SqlDbType.VarBinary).Value = (object)prd.Aimage ?? DBNull.Value;
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    //con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void Update(clsProductGroup prd, string conn, string ip_address, List<int> machineIds, int? selectedCategoryId)
        {
            prd.ip_address = ip_address;
            int key_map_id = 0;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("P_M_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                cmd.Parameters.AddWithValue("@category_id ", selectedCategoryId);
                cmd.Parameters.AddWithValue("@name", prd.name);
                cmd.Parameters.AddWithValue("@main_category_id", prd.maincategory_id);
                cmd.Parameters.AddWithValue("@description_sales", prd.web_sales_description);
                cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
                cmd.Parameters.AddWithValue("@is_active", prd.is_active);
                cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
                cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
                cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                if (prd.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", prd.description);
                }
                cmd.Parameters.Add("@Aimage", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@Aimage"].Value = (object)prd.Aimage ?? DBNull.Value;
                //cmd.Parameters.AddWithValue("@categoryName", prd.categoryName);

                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                //con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(clsProductGroup prd, string conn, string ip_address)
        {
            prd.ip_address = ip_address;
            //string ip_address = "TMS-10";
            int key_map_id = 0;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("P_M_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                //cmd.Parameters.AddWithValue("@maincategory_id ", prd.maincategory_id);
                cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
                cmd.Parameters.AddWithValue("@category_id", prd.category_id);
                cmd.Parameters.AddWithValue("@name", prd.name);
                //cmd.Parameters.AddWithValue("@main_category_id", prd.categoryName);
                cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
                cmd.Parameters.AddWithValue("@is_active", prd.is_active);
                cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                cmd.Parameters.AddWithValue("@description_sales", prd.web_sales_description);
                cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                cmd.Parameters.AddWithValue("@key_map_id", key_map_id);

                if (prd.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", prd.description);
                }
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                //con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
