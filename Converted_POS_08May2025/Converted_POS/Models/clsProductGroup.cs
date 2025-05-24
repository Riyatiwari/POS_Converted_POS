using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsProductGroup
    {
         
        public int? category_id { get; set; }
         
        public string categoryName { get; set; }
      
        public int? cmp_id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modify_date { get; set; }
        public bool is_active { get; set; }
        public string ip_address { get; set; }
        public int login_id { get; set; }
        public int key_map_id { get; set; }
        public int Category_Detail_id { get; set; }
        public string mac_id { get; set; }
        public int machine_id { get; set; }
        public string Machine { get; set; }
        public int venue_id { get; set; }
        public int sorting_no { get; set; }
        public bool is_web_available { get; set; }
        [Required(ErrorMessage = "Sub Group is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Sub Group")]
        public int maincategory_id { get; set; }
        
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
        public List<int> SelectedMachineIds { get; set; } = new List<int>();
    }

    //public class clsProductGroup_DAL : Controller
    //{
    //    public static IConfiguration _configuration;
    //    string connectionString = _configuration.GetConnectionString("POS_Connection");

    //    public List<clsProductGroupMain> Index(int? c_id, string conn)
    //    {
    //        clsProductGroupMain ddl = new clsProductGroupMain();
    //        ddl.ListCat = PopulateFruits();
    //        List<clsProductGroupMain> result = new List<clsProductGroupMain>();
    //        result.Add(ddl);

    //        return result;
    //    }

    //    [HttpPost]
    //    public ActionResult Index(clsProductGroupMain ddl)
    //    {
    //        ddl.ListCat = PopulateFruits();
    //        var selectedItem = ddl.ListCat.Find(p => p.Value == ddl.category_id.ToString());
    //        if (selectedItem != null)
    //        {
    //            selectedItem.Selected = true;
    //            ViewBag.Message = "Fruit: " + selectedItem.Text;
    //            ViewBag.Message += "\\nQuantity: " + ddl.name;
    //        }

    //        return View(ddl);
    //    }

    //    private List<SelectListItem> PopulateFruits()
    //    {
    //        List<SelectListItem> list = new List<SelectListItem>();
    //        string constr = "Data Source=TMS-10\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            string query = "select maincategory_id, name from M_MainCategory where is_active = 1";

    //            using (SqlCommand cmd = new SqlCommand(query))
    //            {
    //                cmd.Connection = con;
    //                con.Open();
    //                using (SqlDataReader dr = cmd.ExecuteReader())
    //                {
    //                    while (dr.Read())
    //                    {
    //                        list.Add(new SelectListItem
    //                        {
    //                            Value = dr["maincategory_id"].ToString(),
    //                            Text = dr["name"].ToString()

    //                        });
    //                    }

    //                }
    //                con.Close();
    //            }
    //        }
    //        ViewBag.ddl = list;
    //        return list;
    //    }
    //    //public ActionResult BindModels (clsProductGroup model)
    //    //{
    //    //    List<clsProductGroup> list = new List<clsProductGroup>();
    //    //    foreach (var m in model.MainModel)
    //    //    {
    //    //        list.Add(new clsProductGroup
    //    //        {
    //    //            name = m.name,
    //    //            category_id = Convert.ToInt32(m.category_id)
    //    //        });
    //    //    }
    //    //    return View();

    //    //}


    //    //public ActionResult Index()
    //    //{
    //    //    //ViewBag.ddlSelectList = new SelectList(ddl(), "category_id", "name");
    //    //    clsProductGroup model = new clsProductGroup();
    //    //    model.list = new List<SelectListItem>
    //    //    {
    //    //        new SelectListItem { Value = "category_id", Text = "name"}
    //    //    };
    //    //    return (ActionResult)View(model);
    //    //}
    //    //[HttpPost]
    //    //public ActionResult Index(clsProductGroup viewModel)
    //    //{
    //    //int category_id = (int)viewModel.prdGrp.category_id;
    //    //return (ActionResult)View(viewModel);
    //    //clsProductGroup viewModel = new clsProductGroup();
    //    //viewModel.PrdGrpMain = GetPrdGrpMain();
    //    //viewModel.prdGrp = new clsProductGroup();
    //    //return (ActionResult)View(viewModel);
    //    //int category_id = (int)model.category_id;
    //    // Do something with the selected item

    //    //return (ActionResult)View(model);
    //    //}

    //    public IEnumerable<clsProductGroup> Bindddl(clsProductGroup model)
    //    {
    //        List<clsProductGroupMain> Main = new List<clsProductGroupMain>();
    //        string constr = "Data Source=TMS-10\\SQLEXPRESS;Initial Catalog=POS_MiteshTest118;User ID=sa;Password=TMS@2017;";
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            string query = "select name from M_MainCategory where is_active = 1";
             
    //            using (SqlCommand cmd = new SqlCommand(query))
    //            {
    //                cmd.Connection = con;
    //                con.Open();
    //                using (SqlDataReader dr = cmd.ExecuteReader())
    //                {
    //                    while (dr.Read())
    //                    {
    //                        Main.Add(new clsProductGroupMain
    //                        {
    //                            category_id = Convert.ToInt32(dr["category_id"]),
    //                            name = dr["name"].ToString()

    //                        });
    //                    }

    //                }
    //                con.Close();
    //            }
    //        }
    //        return (IEnumerable<clsProductGroup>)View(Main);
    //    }

    //    private clsProductGroup View(List<clsProductGroupMain> mainModel)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public List<clsProductGroup> SelectAll(int? c_id, string conn)
    //    //{
    //    //    List<clsProductGroup> listItems = new List<clsProductGroup>();

    //    //    using (SqlConnection con = new SqlConnection(conn))
    //    //    {
    //    //        SqlCommand cmd = new SqlCommand("Get_M_Category", con);
    //    //        cmd.CommandType = CommandType.StoredProcedure;

    //    //        cmd.Parameters.AddWithValue("@cmp_id", c_id);
               
    //    //        con.Open();
    //    //        SqlDataReader rdr = cmd.ExecuteReader();

    //    //        while (rdr.Read())
    //    //        {
    //    //            clsProductGroup prd = new clsProductGroup();

    //    //            prd.category_id = Convert.ToInt32(rdr["category_id"]);
    //    //            //prd.maincategory_id = Convert.ToInt32(rdr["maincategory_id"]);
    //    //            prd.name = rdr["name"].ToString();
    //    //            prd.description = rdr["description"].ToString();
    //    //            //prd.active = rdr["Active"].ToString();
    //    //            if (rdr["is_active"].ToString() == "Yes")
    //    //            {
    //    //                prd.is_active = true;
    //    //            }
    //    //            else
    //    //            {
    //    //                prd.is_active = false;
    //    //            }

    //    //            listItems.Add(prd);
    //    //        }
    //    //        con.Close();
    //    //    }
      
    //    //    //clsProductGroupMain list = TempData["MyList"] as clsProductGroupMain;
    //    //    //ViewBag.ddl = new SelectList(listItems);
    //    //    return listItems;
    //    //}
    //    public List<clsProductGroupMain> SelectM(int? c_id, string conn)
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
    //        //            Public TempDataDictionary TempData { get; set; }
    //        //            Public class TempDataDictionary : IDictionary<string, object="">
    //        //</string,>
    //        //TempData["mylist"] = list;
    //        //list = TempData["mylist"] as List<clsProductGroupMain>;
    //        //ViewState["MyList"] = list;
    //        return list;
    //        //return (IEnumerable<clsProductGroupMain>)RedirectToPage("/BackOffice/Product_Group_Master");
    //    }
    //    public List<clsProductGroupMain> SelectMain(int? c_id, string conn)
    //    {
    //        List<clsProductGroupMain> clsMain = new List<clsProductGroupMain>();
    //        using (SqlConnection con = new SqlConnection(conn))
    //        {
    //            SqlCommand cmd = new SqlCommand("Get_M_Category", con);
    //            cmd.CommandType = CommandType.StoredProcedure;

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

    //                clsMain.Add(prd);
    //            }
    //            con.Close();
    //        }

    //        //clsProductGroupMain list = TempData["MyList"] as clsProductGroupMain;
    //        ViewBag.ddl = new SelectList(clsMain);
    //        return clsMain;
    //    }
        

    //    public clsProductGroup Select(int? c_id, int? id, string conn)
    //    {
    //        clsProductGroup prd = new clsProductGroup();

    //        using (SqlConnection con = new SqlConnection(conn))
    //        {
    //            SqlCommand cmd = new SqlCommand("Get_M_Category", con);
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            cmd.Parameters.AddWithValue("@category_id", id);
    //            cmd.Parameters.AddWithValue("@cmp_id", c_id);

    //            con.Open();
    //            SqlDataReader rdr = cmd.ExecuteReader();

    //            while (rdr.Read())
    //            {
    //                prd.category_id = Convert.ToInt32(rdr["category_id"]);
    //                prd.name = rdr["name"].ToString();
    //                prd.description = rdr["description"].ToString();
    //                prd.web_sales_description = rdr["web_sales_description"].ToString();
    //                prd.sorting_no = Convert.ToInt32(rdr["sorting_no"]);
    //                //prd.click_and_collect = rdr["click_and_collect"]
    //                //prd.Order_at_table = 
    //                //prd.deliver = 
    //                prd.active = rdr["is_active"].ToString();

    //                if (rdr["is_active"].ToString() == "1")
    //                {
    //                    prd.is_active = true;
    //                }
    //                else
    //                {
    //                    prd.is_active = false;
    //                }
    //            }
    //            con.Close();
    //        }
    //        return prd;
    //    }
    //    public void Insert(clsProductGroup prd, string conn)
    //    {
    //        string ip_address = "TMS-10";
    //        int key_map_id = 1;
    //        using (SqlConnection con = new SqlConnection(conn))
    //        {
    //            SqlCommand cmd = new SqlCommand("P_M_Category", con);
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
    //    public void Update(clsProductGroup prd, string conn)
    //    {
    //        string ip_address = "TMS-10";
    //        int key_map_id = 1;
    //        using (SqlConnection con = new SqlConnection(conn))
    //        {
    //            SqlCommand cmd = new SqlCommand("P_M_Category", con);
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
    //    public void Delete(clsProductGroup prd, string conn)
    //    {
    //        string ip_address = "TMS-10";
    //        int key_map_id = 1;
    //        using (SqlConnection con = new SqlConnection(conn))
    //        {
    //            SqlCommand cmd = new SqlCommand("P_M_Category", con);
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
}
