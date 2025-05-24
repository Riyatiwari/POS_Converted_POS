using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
 
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
 

namespace Converted_POS.Models
{
    public class clsProductGroupMain
    {
        public int? maincategory_id { get; set; }
        public int? cmp_id { get; set; }
        public int? category_id { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public int department_id { get; set; }
        public string department_name { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modify_date { get; set; }
        public bool is_active { get; set; }
        public string ip_address { get; set; }
        public int login_id { get; set; }
        public int machine_id { get; set; }
        public int venue_id { get; set; }
        public int sorting_no { get; set; }
        public bool is_available_online { get; set; }
        public string location_id { get; set; }
        public bool click_and_collect { get; set; }
        public bool deliver { get; set; }
        public bool Order_at_table { get; set; }
        public string web_sales_description { get; set; }
        public Byte[] Aimage { get; set; }
        public string ImageFileName { get; set; }
        public String Tran_Type { get; set; }
        public String active { get; set; }
        public List<SelectListItem> ListCat { get; set; }
    }
}
//public class clsProductGroupMain_DAL : Controller
//{
//    public ActionResult Index(int cmpID, string conn)
//    {
//        clsProductGroupMain fruit = new clsProductGroupMain();
//        fruit.Fruits = PopulateFruits();
//        return View(fruit);
//    }

//    private List<SelectListItem> PopulateFruits()
//    {
//        throw new NotImplementedException();
//    }

//    [HttpPost]
//    public ActionResult Index(clsProductGroupMain fruit)
//    {
//        fruit.Fruits = PopulateFruits();
//        var selectedItem = fruit.Fruits.Find(p => p.Value == fruit.category_id.ToString());
//        if (selectedItem != null)
//        {
//            selectedItem.Selected = true;
//            ViewBag.Message = "Fruit: " + selectedItem.Text;
//            ViewBag.Message += "\\nQuantity: " + fruit.name;
//        }
//        return View(fruit);
//    }

//    public IEnumerable<clsProductGroupMain> PopulateFruits(int? c_id, string connectionString)
//    {   
//        List<clsProductGroupMain> items = new List<clsProductGroupMain>();
//        //string connectionString = _configuration.GetConnectionString("POS_Connection");
//        using (SqlConnection con = new SqlConnection(connectionString))
//        {
//            string query = "select name from M_MainCategory where is_active = 1";
//            using (SqlCommand cmd = new SqlCommand(query))
//            {
//                cmd.Parameters.AddWithValue("@cmp_id", c_id);
//                cmd.Connection = con;
//                con.Open();
//                using (SqlDataReader rdr = cmd.ExecuteReader())
//                {
//                    while (rdr.Read())
//                    {
//                        clsProductGroupMain prd = new clsProductGroupMain();

//                        prd.category_id = Convert.ToInt32(rdr["category_id"]);
//                        //prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
//                        prd.name = rdr["name"].ToString();
//                        prd.description = rdr["description"].ToString();
//                        //prd.active = rdr["Active"].ToString();
//                        if (rdr["is_active"].ToString() == "Yes")
//                        {
//                            prd.is_active = true;
//                        }
//                        else
//                        {
//                            prd.is_active = false;
//                        }
//                        items.Add(prd);
//                    }
//                }
//                con.Close();
//            }
//        }
//        return items;
//    }

//    public static IConfiguration _configuration;
//    string connectionString = _configuration.GetConnectionString("POS_Connection");
//    public List<clsProductGroupMain> ddlGrpCat(int? c_id, string connectionString)
//    {
//        List<clsProductGroupMain> listItems = new List<clsProductGroupMain>();

//        using (SqlConnection con = new SqlConnection(connectionString))
//        {
//            SqlCommand cmd = new SqlCommand("select name from M_MainCategory where is_active = 1", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@cmp_id", c_id);
//            con.Open();
//            SqlDataReader rdr = cmd.ExecuteReader();

//            while (rdr.Read())
//            {
//                listItems.Add(new clsProductGroupMain
//                {
//                    name = rdr["name"].ToString(),
//                    category_id = Convert.ToInt32(rdr["category_id"])
//                });
//            }
//        }

//        return listItems;
//    }
//    public ActionResult YourAction()
//    {
//        clsProductGroupMain Main = new clsProductGroupMain
//        {
//            name = "Dhruvi"
//        };

//        ViewBag.ListDDL = "DDL";
//        return View(Main);
//}
//    [HttpPost]
//    public ActionResult Index(clsProductGroupMain Main)
//    {
//        string obj = Main.name;
//        return View();
//    }
//To View all Allergy details

//public List<clsProductGroupMain> SelectAll(int? c_id, string conn)
//    {

//        List<clsProductGroupMain> list = new List<clsProductGroupMain>();
//        //string jsonList = JsonConvert.SerializeObject(list);

//        using (SqlConnection con = new SqlConnection(conn))
//        {

//            SqlCommand cmd = new SqlCommand("Get_M_MainCategory", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            //cmd.Parameters.AddWithValue("@maincategory_id", maincategory_id);
//            cmd.Parameters.AddWithValue("@cmp_id", c_id);

//            con.Open();
//            SqlDataReader rdr = cmd.ExecuteReader();

//            while (rdr.Read())
//            {
//                clsProductGroupMain prd = new clsProductGroupMain();

//                prd.category_id = Convert.ToInt32(rdr["category_id"]);
//                //prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
//                prd.name = rdr["name"].ToString();
//                prd.description = rdr["description"].ToString();
//                //prd.active = rdr["Active"].ToString();
//                if (rdr["is_active"].ToString() == "Yes")
//                {
//                    prd.is_active = true;
//                }
//                else
//                {
//                    prd.is_active = false;
//                }
//                list.Add(prd);
//            }

//            con.Close();
//        }

//        ViewData["MainList"] = list;
//        ViewBag.DropDown = new SelectList(list);
//            Public TempDataDictionary TempData { get; set; }
//            Public class TempDataDictionary : IDictionary<string, object="">
//</string,>
//TempData["mylist"] = list;
//list = TempData["mylist"] as List<clsProductGroupMain>;
//ViewState["MyList"] = list;
//    return list;
//}

//Get the details of a particular Allergy  
//public clsProductGroupMain SelectS(int? c_id, int? id, string conn)
//{
//    clsProductGroupMain prd = new clsProductGroupMain();

//    using (SqlConnection con = new SqlConnection(conn))
//    {
//        SqlCommand cmd = new SqlCommand("Get_M_MainCategory", con);
//        cmd.CommandType = CommandType.StoredProcedure;

//        cmd.Parameters.AddWithValue("@category_id", id);
//        cmd.Parameters.AddWithValue("@cmp_id", c_id);

//        con.Open();
//        SqlDataReader rdr = cmd.ExecuteReader();

//        while (rdr.Read())
//        {
//            prd.category_id = Convert.ToInt32(rdr["category_id"]);
//            prd.name = rdr["name"].ToString();
//            prd.description = rdr["description"].ToString();
//            prd.web_sales_description = rdr["web_sales_description"].ToString();
//            prd.sorting_no = Convert.ToInt32(rdr["sorting_no"]);
//            //prd.click_and_collect = rdr["click_and_collect"]
//            //prd.Order_at_table = 
//            //prd.deliver = 
//            prd.active = rdr["is_active"].ToString();

//            if (rdr["is_active"].ToString() == "1")
//            {
//                prd.is_active = true;
//            }
//            else
//            {
//                prd.is_active = false;
//            }
//        }
//        con.Close();
//    }
//    return prd;
//}

//To Add new Allergy record    
//    public void Insert(clsProductGroupMain prd, string conn)
//    {
//        string ip_address = "TMS-10";
//        int key_map_id = 0;
//        using (SqlConnection con = new SqlConnection(conn))
//        {
//            SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
//            //cmd.Parameters.AddWithValue("@maincategory_id ", prd.maincategory_id);
//            cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
//            cmd.Parameters.AddWithValue("@category_id", prd.category_id);
//            cmd.Parameters.AddWithValue("@name", prd.name);
//            cmd.Parameters.AddWithValue("@sorting_no", prd.sorting_no);
//            cmd.Parameters.AddWithValue("@is_active", prd.is_active);
//            cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
//            //cmd.Parameters.AddWithValue("@description", prd.description);
//            //cmd.Parameters.AddWithValue("@web_sales_description", prd.web_sales_description);
//            cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
//            cmd.Parameters.AddWithValue("@deliver", prd.deliver);
//            cmd.Parameters.AddWithValue("@ip_address", ip_address);
//            cmd.Parameters.AddWithValue("@login_id", prd.login_id);
//            cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
//            if (prd.description is null)
//            {
//                cmd.Parameters.AddWithValue("@description ", "");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@description ", prd.description);
//            }

//            cmd.Parameters.AddWithValue("@Aimage", null);
//            cmd.Parameters.AddWithValue("@Tran_Type", "I");

//            con.Open();
//            cmd.ExecuteNonQuery();
//            con.Close();
//        }
//    }

//    //To Update Allergy record    
//    public void Update(clsProductGroupMain prd, string conn)
//    {
//        string ip_address = "TMS-10";
//        int key_map_id = 1;
//        using (SqlConnection con = new SqlConnection(conn))
//        {
//            SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
//            cmd.Parameters.AddWithValue("@category_id ", prd.category_id);
//            cmd.Parameters.AddWithValue("@name", prd.name);
//            cmd.Parameters.AddWithValue("@is_active", prd.is_active);
//            cmd.Parameters.AddWithValue("@ip_address", ip_address);
//            cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
//            cmd.Parameters.AddWithValue("@login_id", prd.login_id);
//            cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
//            cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
//            cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
//            cmd.Parameters.AddWithValue("@deliver", prd.deliver);
//            if (prd.description is null)
//            {
//                cmd.Parameters.AddWithValue("@description ", "");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@description ", prd.description);
//            }
//            cmd.Parameters.AddWithValue("@Aimage", null);
//            cmd.Parameters.AddWithValue("@Tran_Type", "U");

//            con.Open();
//            cmd.ExecuteNonQuery();
//            con.Close();
//        }
//    }

//    //To Delete Allergy record    
//    public void Delete(clsProductGroupMain prd, string conn)
//    {
//        string ip_address = "TMS-10";
//        int key_map_id = 1;
//        using (SqlConnection con = new SqlConnection(conn))
//        {
//            SqlCommand cmd = new SqlCommand("P_M_MainCategory", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@cmp_id", prd.cmp_id);
//            cmd.Parameters.AddWithValue("@category_id ", prd.category_id);
//            cmd.Parameters.AddWithValue("@name", prd.name);
//            cmd.Parameters.AddWithValue("@ip_address", ip_address);
//            cmd.Parameters.AddWithValue("@key_map_id", key_map_id);
//            cmd.Parameters.AddWithValue("@login_id", prd.login_id);
//            cmd.Parameters.AddWithValue("@machine_id", prd.machine_id);
//            cmd.Parameters.AddWithValue("@click_and_collect", prd.click_and_collect);
//            cmd.Parameters.AddWithValue("@Order_at_table", prd.Order_at_table);
//            cmd.Parameters.AddWithValue("@deliver", prd.deliver);
//            if (prd.description is null)
//            {
//                cmd.Parameters.AddWithValue("@description ", "");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@description ", prd.description);
//            }
//            //cmd.Parameters.AddWithValue("@Aimage", null);
//            cmd.Parameters.AddWithValue("@Tran_Type", "D");

//            con.Open();
//            cmd.ExecuteNonQuery();
//            con.Close();
//        }
//    }
//}
//}