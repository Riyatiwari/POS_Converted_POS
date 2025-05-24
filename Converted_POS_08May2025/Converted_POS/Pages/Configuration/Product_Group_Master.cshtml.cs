using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
 
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Converted_POS.Pages.Configuration
{
    public class Product_Group_MasterModel : PageModel
    {
        ProductGroupController obj = new ProductGroupController();

        [BindProperty]
        public clsProductGroup DTypePG { get; set; }
        public List<clsProductGroup> DTypedetail{ get; set; }
        public List<clsProductGroup> DTSaveMachine { get; set; }
        [BindProperty]
        public List<SelectListItem> DType_LCPGM { get; set; }
        public List<SelectListItem> DT { get; set; }
        // {
        //List<clsProductGroupMain> MainModel = new List<clsProductGroupMain>();
        //foreach (var m in model.MainModel)
        //{
        //    MainModel.Add(new clsProductGroupMain
        //    {
        //        name = m.name,
        //        category_id = Convert.ToInt32(m.category_id)
        //    });
        //}
        //return View(model);
        //return View(model);
        //}
        public string ConnStr { get; set; }
        public int categoryId { get; set; }
        public int machine_id { get; set; }
        public string cmpID { get; set; }
        public List<int> SelectedMachineIds { get; set; } = new List<int>();
        public List<int> machineIds { get; set; }
        //public class ProductGroupController : Controller
        //{

        //    internal clsProductGroup Select(int v, int? id, string connStr)
        //    {
        //        throw new NotImplementedException();
        //    }

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
        //}
        //public ActionResult OnGet()
        //{
        //    if (HttpContext.Session.GetString("cmp_id") == null)
        //    {
        //        return RedirectToPage("/SignIn");
        //    }

        //    cmpID = HttpContext.Session.GetString("cmp_id");
        //    ConnStr = HttpContext.Session.GetString("conString");

        //    DType_PGM = obj.Index(Convert.ToInt32(cmpID), ConnStr).ToList();

        //    return Page();
        //}
        public ActionResult OnGet(int? id, int Category_Detail_id, int category_id, int machine_id, int login_id, string ip_address)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }
            
            DTypePG = DTypePG ?? new clsProductGroup();
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            if (id.HasValue)
            {
                DTypePG = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);
                if (DTypePG != null)
                {
                    string selectedMachineIdsStr = HttpContext.Session.GetString("SelectedMachineIds");
                    List<int> selectedMachineIds = new List<int>();

                    if (!string.IsNullOrEmpty(selectedMachineIdsStr))
                    {
                        selectedMachineIds = selectedMachineIdsStr.Split(',')
                            .Select(int.Parse)
                            .ToList();
                    }

                    DTypePG.SelectedMachineIds = obj.GetSelectedMachineIds(Convert.ToInt32(cmpID), DTypePG.maincategory_id, ConnStr, selectedMachineIds);

                    // Store the combined IDs back in the session
                    HttpContext.Session.SetString("SelectedMachineIds", string.Join(",", DTypePG.SelectedMachineIds));
                    //DTypePG.SelectedMachineIds = obj.GetSelectedMachineIds(Convert.ToInt32(cmpID), DTypePG.maincategory_id, ConnStr);
                    //HttpContext.Session.SetString("SelectedMachineIds", string.Join(",", DTypePG.SelectedMachineIds));
                    //machineIds = DTypePG.SelectedMachineIds.ToList();

                }
            }
            string sessionCategoryId = HttpContext.Session.GetString("selectedCategoryId");
            int? selectedCategoryId = string.IsNullOrEmpty(sessionCategoryId) ? DTypePG.maincategory_id : (int?)int.Parse(sessionCategoryId);

            // Use the selectedCategoryId for further logic
            if (selectedCategoryId.HasValue)
            {
                DTypePG.category_id = selectedCategoryId.Value;
            }

            categoryId = Convert.ToInt32(HttpContext.Session.GetString("Category_id"));
            machine_id = Convert.ToInt32(HttpContext.Session.GetString("Machine_id"));
            DType_LCPGM = obj.Index(Convert.ToInt32(cmpID), id, ConnStr).ToList();
            int sortingNumber = obj.GenerateSortingNumber(Convert.ToInt32(cmpID), "Product Group", ConnStr);
            DTypePG.sorting_no = sortingNumber;
            
            //HttpContext.Session.SetString("category_id", HttpContext.Request.Query["ID"].ToString());
            // Call the method to get machines by category
            DTypedetail = obj.GetMachineDetail(cmpID,categoryId, ConnStr, machine_id);
            //DTSaveMachine = obj.GetSaveMachine(cmpID, ConnStr, Category_Detail_id, categoryId, machine_id, login_id, ip_address);
            if (id != null)
            {
                // Convert cmpID to int if necessary
                int cmpIdInt = Convert.ToInt32(cmpID);
                DTypePG = obj.Select(cmpIdInt, id, ConnStr); // Ensure id is of type int
                string selectedMachineIdsStr = HttpContext.Session.GetString("SelectedMachineIds");
                List<int> selectedMachineIds = new List<int>();

                if (!string.IsNullOrEmpty(selectedMachineIdsStr))
                {
                    selectedMachineIds = selectedMachineIdsStr.Split(',')
                        .Select(int.Parse)
                        .ToList();
                }
                DTypePG.SelectedMachineIds = obj.GetSelectedMachineIds(Convert.ToInt32(cmpID), DTypePG.maincategory_id, ConnStr, selectedMachineIds);
            }

            if (id == null)
            {
                return Page();
            }
            else
            {
                //int categoryId = Convert.ToInt32(HttpContext.Request.Query["ID"].ToString());
                //HttpContext.Session.SetString("category_id", categoryId.ToString());
                //HttpContext.Session.SetString("category_id", HttpContext.Request.Query["ID"].ToString());
                // Call the method to get machines by category
                //var Till = obj.GetMachinesByCategory(cmpID, categoryId, ConnStr);

                HttpContext.Session.SetString("category_id", HttpContext.Request.Query["ID"].ToString());
                //DTSaveMachine = obj.GetSaveMachine(cmpID, ConnStr, Category_Detail_id, category_id, machine_id, login_id, ip_address);
                DT = obj.GetGroupCat(Convert.ToInt32(cmpID), ConnStr);
                DTypePG = obj.Select(Convert.ToInt32(cmpID), id, ConnStr);
                string selectedMachineIdsStr = HttpContext.Session.GetString("SelectedMachineIds");
                List<int> selectedMachineIds = new List<int>();

                if (!string.IsNullOrEmpty(selectedMachineIdsStr))
                {
                    selectedMachineIds = selectedMachineIdsStr.Split(',')
                        .Select(int.Parse)
                        .ToList();
                }
                DTypePG.SelectedMachineIds = obj.GetSelectedMachineIds(Convert.ToInt32(cmpID), DTypePG.maincategory_id, ConnStr, selectedMachineIds);
                if (DTypePG == null)
                {
                    return NotFound();
                }
                if (DTypePG.Aimage == null || DTypePG.Aimage.Length == 0)
                {
                    // Log or set a breakpoint here to inspect DType
                    Console.WriteLine("DType.Aimage is null or empty.");
                }
                else
                {
                    // Convert the image byte array to Base64 string for display
                    string base64Image = ConvertImageToBase64String(DTypePG.Aimage);
                    ViewData["Base64Image"] = $"data:image/png;base64,{base64Image}";  // Store it in ViewData to use in the view
                }
                
                return Page();
            }
        }

        private string ConvertImageToBase64String(byte[] aimage)
        {
            if (aimage == null || aimage.Length == 0)
            {
                return null; // Handle case where no image is present
            }
            return Convert.ToBase64String(aimage);
        }

        //private List<clsProductGroup> GetMachinesByCategory(string cmpID, int categoryId, string connStr)
        //{
        //    var machines = new List<clsProductGroup>();

        //    using (var connection = new SqlConnection(connStr))
        //    {
        //        using (var command = new SqlCommand("Get_Machine_By_Category", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@cmp_id", cmpID);
        //            command.Parameters.AddWithValue("@Category_id", categoryId);

        //            connection.Open();
        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var machine = new clsProductGroup
        //                    {
        //                        category_id = reader.GetInt32(reader.GetOrdinal("Category_id")),
        //                        machine_id = reader.GetInt32(reader.GetOrdinal("Machine_id"))
        //                    };
        //                    machines.Add(machine);
        //                }
        //            }
        //        }
        //    }

        //    return machines;
        //}

        [HttpPost("SaveSelectedMachines")]
        public ActionResult OnPost(int? id, IFormFile AimageFile, string ipAddress, int cmp_id,int category_id, int Category_Detail_id, int login_id, List<int> machineIds,string ImageFileName)
        {
            DTypePG.SelectedMachineIds = Request.Form["DTypePG.SelectedMachineIds"].Select(int.Parse).ToList();
            HttpContext.Session.SetString("SelectedMachineIds", string.Join(",", DTypePG.SelectedMachineIds));
            int? selectedCategoryId = DTypePG.maincategory_id;
            //DTypePG.category_id = Convert.ToInt32(HttpContext.Session.GetString("maincategory_id"));
            if (selectedCategoryId.HasValue)
            {
                HttpContext.Session.SetString("selectedCategoryId", selectedCategoryId.Value.ToString());
            }
            clsProductGroup prd = new clsProductGroup
            {
                cmp_id = cmp_id,
                category_id = selectedCategoryId ?? 0, // Set category_id, use 0 if selectedCategoryId is null
                ip_address = ipAddress,
                SelectedMachineIds = DTypePG.SelectedMachineIds // Assuming you have this property
            };
            DTypePG.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");
            cmpID = HttpContext.Session.GetString("cmp_id");
            machine_id = Convert.ToInt32(HttpContext.Session.GetString("Machine_id"));
            string ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            List<int> savedMachineIds = obj.GetSaveMachine(cmpID, ConnStr, Category_Detail_id, selectedCategoryId, DTypePG.SelectedMachineIds, login_id, ip_address);
            DTypePG.SelectedMachineIds = savedMachineIds;
            //DTypePG.SelectedMachineIds = obj.GetSelectedMachineIds(Convert.ToInt32(cmpID), DTypePG.category_id, ConnStr);
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (AimageFile != null && AimageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    AimageFile.CopyToAsync(memoryStream); // Asynchronously copy the file
                    DTypePG.Aimage = memoryStream.ToArray(); // Convert to byte array
                    DTypePG.ImageFileName = AimageFile.FileName; // Store the file name
                }
            }
            else
            {
                //DTypePG.Aimage = null; // Handle no image uploaded
                // DTypePG.ImageFileName = null; // Handle no file name
                if (!string.IsNullOrEmpty(ImageFileName))
                {
                    // Decode the base64 image data and save it
                    byte[] existingImage = Convert.FromBase64String(ImageFileName.Replace("data:image/png;base64,", ""));
                    DTypePG.Aimage = existingImage;
                }
                else
                {
                    DTypePG.Aimage = null; // If no image exists (i.e., no base64 string), set it to null
                }
            }

            if (category_id != null && category_id != 0)
            {
                // If category_id is valid, perform an update
                obj.Update(DTypePG, ConnStr, ip_address, machineIds, category_id);
                TempData["SuccessMessage"] = "Record updated successfully!";
            }
            else
            {
                // If category_id is not valid, prepare to insert a new record
                DTypePG.category_id = id ?? 0; // Use the id from the parameter or default to 0
                obj.Insert(DTypePG, ConnStr, ip_address, machineIds, DTypePG.category_id);
                TempData["SuccessMessage"] = "Record inserted successfully!";
            }
            //foreach (var machineId in DTypePG.SelectedMachineIds)
            //{
            //    cmpID = HttpContext.Session.GetString("cmp_id");
            // You need a method to associate the selected machines with the product group
            // obj.AssociateMachineWithProductGroup(DTypePG.category_id, machineId, ConnStr, cmpID);
            //}
            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_List");
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("category_id");
            return RedirectToPage("/Configuration/Product_Group_List");
        }

    }
}