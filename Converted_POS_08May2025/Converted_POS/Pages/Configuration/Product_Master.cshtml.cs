using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using Converted_POS.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Converted_POS.Pages.forms
{
    public class Product_MasterModel : PageModel
    {

       
        
        ProductController obj = new ProductController();
        [BindProperty]
        //public clsProduct DType { get; set; }
        //[BindProperty]
        public List<SelectListItem> DT { get; set; }
        public List<SelectListItem> DTdepartment { get; set; }
        public List<SelectListItem> DTcourse { get; set; }
        public List<SelectListItem> DTUnit { get; set; }
        public List<SelectListItem> DTLocation { get; set; }
        public List<SelectListItem> DTPrice { get; set; }
        public List<SelectListItem> DTprinter { get; set; }
        public List<SelectListItem> DTPriceLevel { get; set; }
        public List<SelectListItem> DTTax { get; set; }
        public List<SelectListItem> DTSize { get; set; }
        public List<SelectListItem> DTSizePrice { get; set; }
        public SelectList DTdepartments { get; set; }
        [BindProperty]

        public List<clsProduct> Products { get; set; } /*= new List<clsProduct>();*/
        [BindProperty]

        public List<clsProduct> productsList { get; set; }
        [BindProperty]

        public List<clsProduct> BarcodeList { get; set; }
        [BindProperty]

        public List<clsProduct> IngredientsList { get; set; }
        [BindProperty]

        public List<clsProduct> CondimentsList { get; set; }
        [BindProperty]

        public List<clsProduct> AllergyList { get; set; } 
        [BindProperty]

        public List<clsProduct> Bproducts { get; set; } /*= new List<clsProduct>();*/
        [BindProperty]
        public clsProduct product { get; set; }

        public string ConnStr { get; set; }
        public string cmpID { get; set; }
        [BindProperty]
        public clsProduct NewProduct { get; set; }


        public readonly IConfiguration _configuration;

        public Product_MasterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int? ProductId { get; set; }
        public void OnGet(int? product_id, int? sorting_no, IFormFile fUImage, string fileName, int c_id, int product_Id, int location_id, clsProduct products, int Size_id, string Name)
        {

            if (product_id.HasValue)
            {
                // Store the product_id in the session
                HttpContext.Session.SetString("ProductId", product_id.Value.ToString());
                ProductId = product_id.Value;

                // Load product data if necessary
            }
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                RedirectToPage("/SignIn");
            }
            else
            {
                if (product_id.HasValue)
                {

                    HttpContext.Session.SetString("Product_id", product_id.Value.ToString());
                }

                string ConnStr = HttpContext.Session.GetString("conString");
                int cmpID = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                c_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
                var controller = new ProductController(_configuration);
                // DTdepartments = new SelectList(obj.GetDepartment(Convert.ToInt32(cmpID), ConnStr), "Department_id", "name");


                DTdepartments = new SelectList(obj.GetDepartment(Convert.ToInt32(c_id), ConnStr), "Department_id", "name");

                DT = obj.GetCategories(Convert.ToInt32(cmpID), ConnStr);
                DTdepartment = obj.GetDepartment(Convert.ToInt32(c_id), ConnStr);
                DTcourse = obj.GetCourse(Convert.ToInt32(cmpID), ConnStr);
                DTUnit = obj.GetUnit(Convert.ToInt32(cmpID), ConnStr);
                DTLocation = obj.GetLocation(Convert.ToInt32(cmpID), ConnStr);
                DTPrice = obj.GetPrice(Convert.ToInt32(cmpID), ConnStr);
                DTprinter = obj.GetPrinter(Convert.ToInt32(cmpID), ConnStr);
                DTTax = obj.GetTax(Convert.ToInt32(cmpID), ConnStr);
                DTSize = obj.GetSize(Convert.ToInt32(cmpID), ConnStr, product_Id);
                AllergyList = controller.Allergy(cmpID, ConnStr, product_Id);
                if (product_id != null)
                {
                    HttpContext.Session.SetString("category_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("department_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("course_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("Unit_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("Location_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("Product_Price_Id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("printer_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("Tax_id", HttpContext.Request.Query["ID"].ToString());
                    HttpContext.Session.SetString("row_Id", HttpContext.Request.Query["ID"].ToString());
                    //HttpContext.Session.SetString("product_id", HttpContext.Request.Query["ID"].ToString());
                    // DType = obj.SelectS(Convert.ToInt32(cmpID), id, sorting_no, ConnStr, fUImage);


                    if (Products == null)
                    {
                        Products = new List<clsProduct>();
                    }

                    if (productsList == null)
                    {
                        productsList = new List<clsProduct>();

                    }
                    if (BarcodeList == null)
                    {
                        BarcodeList = new List<clsProduct>();

                    }
                    if (IngredientsList == null)
                    {
                        IngredientsList = new List<clsProduct>();

                    }
                    if (CondimentsList == null)
                    {
                        CondimentsList = new List<clsProduct>();

                    }
                    if (AllergyList == null)
                    {
                        AllergyList = new List<clsProduct>();

                    }
                    //if (Id.HasValue)
                    //{

                    //}
                    if (product_id != null)
                    {

                        product = new clsProduct();
                        product = controller.GetProductById(product_id.Value, ConnStr, cmpID, product);
                        product = controller.GetLevelById(product_id.Value, ConnStr, cmpID, product);
                        productsList = controller.GetSizePriceByProduct(product_id.Value, ConnStr, cmpID, location_id);
                        ////product = controller.GetSizeCostByProduct(product_id.Value, ConnStr, cmpID, product, location_id);
                        //Products = controller.GetSizeCostByProduct(product_id.Value, ConnStr, cmpID, location_id);
                        // Products = controller.GetSizePriceByProduct( product_id.Value, ConnStr, cmpID, products,  location_id);
                        //product = controller.GetSizePriceByProduct(product_id.Value, ConnStr, cmpID, product, location_id);
                        Products = controller.GetSizeCostByProduct(product_id.Value, ConnStr, cmpID, location_id);
                        product.Locations = controller.GetLocation(cmpID, ConnStr);

                        product.Taxes = controller.GetTax(cmpID, ConnStr);
                        product.Size_Zero = controller.GetSize(cmpID, ConnStr, product_Id);


                        IngredientsList = controller.Select_Ingredient_By_Product(cmpID, ConnStr, product_Id);
                        BarcodeList = controller.SelectBarcodeSize(cmpID, ConnStr, product_Id);
                        CondimentsList = controller.Select_Condiment_By_Product(cmpID, ConnStr, product_Id);
                        
                        // JsonResult result = SaveSellingSizesAndPrices(productsList);
                        Products.Add(product);
                        productsList.Add(product);
                        //BarcodeList.Add(product);
                        if (product == null)
                        {
                            product = new clsProduct();
                        }
                        //if (product_Id==null)
                        //{
                        //    OnPost(product_Id);

                        //}
                        //else
                        //{ 
                        //    OnPost(product_Id);
                        //}
                    }
                    else
                    {
                        product = new clsProduct();
                        Bproducts.Add(NewProduct);
                    }
                }
            }
        }


        //[BindProperty]
        //public clsProduct DType { get; set; }

        [HttpPost("Save")]
        public ActionResult OnPost([FromBody] List<clsProduct> allergies, [FromBody] List<clsProduct> productList)
        {
            //var productIdFromSession = HttpContext.Session.GetString("ProductId");
            //if (!string.IsNullOrEmpty(productIdFromSession) && int.TryParse(productIdFromSession, out int productId))
            //{
            //    // Use the productId
            //    // product.ProductId = productId; // Assign it to your model or object
            //    product.ProductId = productId;
            //    product.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            //    ConnStr = HttpContext.Session.GetString("conString");

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Perform save operation here

            // Or wherever you want to redirect

            //}


            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Product ID is missing.");
            //    return Page();
            //}
            product.ProductId = Convert.ToInt32(HttpContext.Session.GetString("ProductId"));
            product.cmp_id = Convert.ToInt32(HttpContext.Session.GetString("cmp_id"));
            ConnStr = HttpContext.Session.GetString("conString");

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
          

             
            if (product.ProductId == null || product.ProductId == 0)
            {
                product.ProductId = 0;
                obj.Insert(product, ConnStr);
               // obj.Insert_Allergy_By_Product(product, ConnStr);
               //Products = obj.SaveSellingSizesAndPrices(ConnStr, productList);
                foreach (var allergy in allergies)
                {
                    var product = new clsProduct
                    {

                       // cmp_id = cmp_id,
                        Allergyid = allergy.Allergyid,  // Assuming you map Allergy name to Allergyid
                        //ProductId = productId
                    };
                    obj.Insert_Allergy_By_Product(product, ConnStr);

                }
                //obj.Insert_Allergy_By_Product(product, ConnStr);
            }
            else
            {
                obj.Update(product, ConnStr);
            }

            HttpContext.Session.Remove("ProductId");
            return RedirectToPage("/Configuration/Product_List");
        }
        [HttpPost]
        public JsonResult SaveSellingSizesAndPrices([FromBody] List<clsProduct> productList)
        {
            try
            {
                ConnStr = HttpContext.Session.GetString("conString");
                using (SqlConnection connection = new SqlConnection(ConnStr))
                {
                    connection.Open();
                    foreach (var prdt in productList)
                    {
                        using (SqlCommand command = new SqlCommand("P_M_Size_N_Cost", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Add parameters with validation
                            command.Parameters.AddWithValue("@product_id", prdt.ProductId);
                            command.Parameters.AddWithValue("@Size_id", prdt.SizeId);
                            command.Parameters.AddWithValue("@Name", prdt.Name);
                            command.Parameters.AddWithValue("@Size", prdt.Size);
                            command.Parameters.AddWithValue("@Unit_Id", prdt.UnitId);
                            command.Parameters.AddWithValue("@is_active", prdt.IsActive);
                            command.Parameters.AddWithValue("@Location_id", prdt.LocationId);
                            command.Parameters.AddWithValue("@Price_Id", prdt.PriceId);
                            command.Parameters.AddWithValue("@Price", prdt.Price);
                            command.Parameters.AddWithValue("@Actual_Price", prdt.ActualPrice);
                            command.Parameters.AddWithValue("@Tax", prdt.Tax);
                            command.Parameters.AddWithValue("@cmp_id", prdt.CompanyId);
                            command.Parameters.AddWithValue("@login_id", prdt.LoginId);
                            command.Parameters.AddWithValue("@ip_address", prdt.IpAddress);
                            command.Parameters.AddWithValue("@action", 1);
                            command.Parameters.AddWithValue("@Tax_id", prdt.TaxId);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
                return new JsonResult(new { success = true, message = "Data saved successfully!" });
            }
            catch (Exception ex)
            {
                // Log the error (you could use a logging library)
                Console.Error.WriteLine($"Exception: {ex.Message}");
                return new JsonResult(new { success = false, message = "An error occurred while saving data." });
            }
        }

        [HttpPost("Reset")]
        public ActionResult OnPostReset()
        {
            HttpContext.Session.Remove("ProductId");
            return RedirectToPage("/Configuration/Product_Master");
        }

        [HttpPost("Cancel")]
        public ActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("ProductId");
            return RedirectToPage("/Configuration/Product_List");
        }
    }


}


