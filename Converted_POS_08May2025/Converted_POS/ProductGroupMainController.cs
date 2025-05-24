using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Converted_POS
{
    public class ProductGroupMainController : Controller
    {
        private readonly ILogger<ProductGroupMainController> _logger;
        [BindProperty]
        public clsProductGroupMain DType { get; set; }
        public ProductGroupMainController(ILogger<ProductGroupMainController> logger)
        {
            _logger = logger;
        }

        public ProductGroupMainController()
        {
        }

        public List<clsProductGroupMain> Index(int cmp_id, string conn)
        {
            clsProductGroupMain ddl = new clsProductGroupMain();
            ddl.ListCat = Populate(conn, cmp_id);
            List<clsProductGroupMain> result = new List<clsProductGroupMain>();
            result.Add(ddl);

            return result;
        }

        [HttpPost]
        public ActionResult Index(clsProductGroupMain ddl, string conn, int cmp_id)
        {
            ddl.ListCat = Populate(conn, cmp_id);
            var selectedItem = ddl.ListCat.Find(p => p.Value == ddl.category_id.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                ViewBag.Message = "Fruit: " + selectedItem.Text;
                ViewBag.Message += "\\nQuantity: " + ddl.name;
            }

            return View(ddl);
        }

        public List<SelectListItem> GetDepartment(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["department_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }
                con.Close();
            }
            return categories;
        }

        private List<SelectListItem> Populate(string conn, int cmp_id)
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
            return list;
        }

        public List<clsProductGroupMain> SelectAll(int? c_id, string conn)
        {
            List<clsProductGroupMain> list = new List<clsProductGroupMain>();
            //string jsonList = JsonConvert.SerializeObject(list);

            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("Get_M_MainCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@maincategory_id", maincategory_id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProductGroupMain prd = new clsProductGroupMain();

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
            //TempData["mylist"] = list;
            //list = TempData["mylist"] as List<clsProductGroupMain>;
            //ViewState["MyList"] = list;
            return list;
        }
        public clsProductGroupMain SelectS(int? c_id, int? id, string conn)
        {
            clsProductGroupMain prd = new clsProductGroupMain();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_MainCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@category_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prd.category_id = Convert.ToInt32(rdr["category_id"]);
                    prd.name = rdr["name"].ToString();
                    prd.description = rdr["description"].ToString();
                    prd.web_sales_description = rdr["web_sales_description"].ToString();
                    prd.sorting_no = Convert.ToInt32(rdr["sorting_no"]);
                    //prd.click_and_collect = Convert.ToBoolean(rdr["click_and_collect"]);
                    //prd.Order_at_table = 
                    //prd.deliver = 
                    //prd.active = rdr["is_active"].ToString();
                    prd.department_id = Convert.ToInt32(rdr["department_id"]);
                    prd.Aimage = rdr["Aimage"] as byte[];
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
        //public string ConvertImageToBase64String(byte[] imageBytes)
        //{

        //    if (imageBytes == null || imageBytes.Length == 0)
        //    {
        //        return null; // Handle case where no image is present
        //    }
        //    return Convert.ToBase64String(imageBytes);
        //}
        public void Insert(clsProductGroupMain prd, string conn, string ip_address)
        {
            //string ip_address = "TMS-10";
            //if (HttpContext?.Connection?.RemoteIpAddress != null)
            //{
            //    string ip_address = HttpContext.Connection.RemoteIpAddress.ToString();
            //    // Use ip_address as needed
            //}
            prd.ip_address = ip_address;
            int key_map_id = 0;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                //cmd.Parameters.AddWithValue("@maincategory_id ", prd.maincategory_id);
                cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
                cmd.Parameters.AddWithValue("@category_id", prd.category_id);
                cmd.Parameters.AddWithValue("@name", prd.name);
                cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
                cmd.Parameters.AddWithValue("@is_active", prd.is_active);
                cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                //cmd.Parameters.AddWithValue("@description", prd.description);
                cmd.Parameters.AddWithValue("@description_Sales", prd.web_sales_description);
                cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
                cmd.Parameters.AddWithValue("@department_id", prd.department_id);
                if (prd.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", prd.description);
                }
                //string base64Image = ConvertImageToBase64String(prd.Aimage);
                //prd.ImageFileName = base64Image;
                //cmd.Parameters.AddWithValue("@Aimage", (object)prd.ImageFileName ?? DBNull.Value);
                cmd.Parameters.Add("@Aimage", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@Aimage"].Value = (object)prd.Aimage ?? DBNull.Value;
                // Use DBNull if null
                //cmd.Parameters.AddWithValue("@ImageFileName", prd.ImageFileName ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Update(clsProductGroupMain prd, string conn, string ip_address)
        {
            prd.ip_address = ip_address;
            //string ip_address = "TMS-10";
            int key_map_id = 0;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                cmd.Parameters.AddWithValue("@category_id ", prd.category_id);
                cmd.Parameters.AddWithValue("@name", prd.name);
                //cmd.Parameters.AddWithValue("@description", prd.description);
                cmd.Parameters.AddWithValue("@description_Sales", prd.web_sales_description);
                cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
                cmd.Parameters.AddWithValue("@is_active", prd.is_active);
                cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
                cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
                cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                cmd.Parameters.AddWithValue("@department_id", prd.department_id);
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
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(clsProductGroupMain prd, string conn, string ip_address)
        {
            prd.ip_address = ip_address;
            //string ip_address = "TMS-10";
            int key_map_id = 0;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
                cmd.Parameters.AddWithValue("@category_id ", prd.category_id);
                cmd.Parameters.AddWithValue("@name", prd.name);
                cmd.Parameters.AddWithValue("@ip_address", prd.ip_address);
                cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
                cmd.Parameters.AddWithValue("@login_id", prd.login_id);
                cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
                cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
                cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
                cmd.Parameters.AddWithValue("@deliver", prd.deliver);
                cmd.Parameters.AddWithValue("@department_id", prd.department_id);
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

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
