using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Pages.forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Configuration;
 
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using javax.xml.crypto;


namespace Converted_POS
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller

    {

        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductController()
        {
        }
        List<clsProduct> Products = new List<clsProduct>();
        public IEnumerable<clsProduct> SelectAll(int? c_id, string conn, bool isActive)
        {
            List<clsProduct> list = new List<clsProduct>();


            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@active", isActive ? 1 : 0);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct al = new clsProduct();
                    al.ProductId = Convert.ToInt32(rdr["Product_id"]);
                    al.Name = rdr["name"].ToString();
                    al.DepartmentName = rdr["Department"].ToString();
                    al.GroupName = rdr["category_name"].ToString();
                    al.baseSize = rdr["BaseSize"].ToString();
                    al.CreatedDate = DateTime.Parse(rdr["Created_Date"].ToString());

                    list.Add(al);

                    //Dept_cat.department_category_id = Convert.ToInt32(rdr["department_category_id"]);
                    //Dept_cat.department_category_name = rdr["department_category_name"].ToString();
                    //Dept_cat.active = rdr["Active"].ToString();

                    //if (rdr["Active"].ToString() == "Yes")
                    //{
                    //    Dept_cat.is_active = true;
                    //}
                    //else
                    //{
                    //    Dept_cat.is_active = false;
                    //}

                    //list.Add(Dept_cat);
                }
                con.Close();
            }
            return list;
        }


        public void Delete(clsProduct al, string connStr)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product_For_Inactive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", al.cmp_id);
                cmd.Parameters.AddWithValue("@product_id ", al.ProductId);
                //cmd.Parameters.AddWithValue("@name", alrgy.name);
                //if (alrgy.description is null)
                //{
                //    cmd.Parameters.AddWithValue("@description ", "");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                //}
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public List<SelectListItem> GetSize(int c_id, string conn, int product_Id)
        {
            List<SelectListItem> size = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Size_N_Price_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@product_id", product_Id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    size.Add(new SelectListItem
                    {
                        Value = rdr["Size_Id"].ToString(),
                        Text = rdr["Size_Unit"].ToString()
                    });
                }

                con.Close();
            }
            return size;
        }

        public void Act(List<clsProduct> product, string connStr, string tranType)
        {
            foreach (var al in product)
            {
                int newStatus;

                // Retrieve the current status from the database
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT is_active FROM M_Product WHERE product_id = @product_id AND cmp_id = @cmp_id", con))
                    {
                        cmd.Parameters.AddWithValue("@product_id", al.ProductId);
                        cmd.Parameters.AddWithValue("@cmp_id", al.cmp_id);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            byte currentStatus = Convert.ToByte(result);
                            newStatus = (currentStatus == 1) ? 0 : 1;
                            tranType = (newStatus == 1) ? "A" : "D";
                        }
                        else
                        {

                            throw new Exception($"Product with ID {al.ProductId} not found.");
                        }
                    }
                }

                // Perform the update with the new status
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand updateCmd = new SqlCommand("P_M_Product_For_Inactive", con))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;
                        updateCmd.Parameters.AddWithValue("@cmp_id", al.cmp_id);
                        updateCmd.Parameters.AddWithValue("@product_id", al.ProductId);
                        updateCmd.Parameters.AddWithValue("@is_active", newStatus);
                        updateCmd.Parameters.AddWithValue("@Tran_Type", tranType);

                        con.Open();
                        updateCmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }


        public clsProduct Select(int? c_id, int? id, string connStr)
        {
            clsProduct product = new clsProduct();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_product", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.ProductId = Convert.ToInt32(rdr["Product_id"]);
                    //product.name = rdr["name"].ToString();
                    //product.description = rdr["description"].ToString();

                }
                con.Close();
            }
            return product;
        }


        private DataTable GetProductPriceLocation(int locationId, int productId, string connStr)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("Get_Product_Price_Location", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.Add(new SqlParameter("@Location_Id", SqlDbType.Int)).Value = locationId;
                    command.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int)).Value = productId;


                    connection.Open();


                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }


        public DataTable Insert_Allergy_By_Product(clsProduct product, string conn)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("P_M_Product_Allergy", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", product.cmp_id);
                    cmd.Parameters.AddWithValue("@product_id",2 );    //HttpContext.Session.GetString("Id")
                    cmd.Parameters.AddWithValue("@allergy_id", product.Allergyid);
                    cmd.Parameters.AddWithValue("@tran_type", "I");

                    con.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtResult);  
                    }
                }
            }

            return dtResult;  
        }

        //protected void Insert_Allergy(int id)
        //{
        //    try
        //    {
        //        DataTable dtAllergy = (DataTable)ViewState["Allergy_Data"];
        //        oclsProduct.product_id = id;
        //        oclsProduct.cmp_id = Convert.ToInt32(Session["cmp_id"]);
        //        oclsProduct.delete_Allergy_By_Product();

        //        for (int i = 0; i < dtAllergy.Rows.Count; i++)
        //        {
        //            oclsProduct.product_id = id;
        //            oclsProduct.cmp_id = Convert.ToInt32(Session["cmp_id"]);
        //            oclsProduct.allergy_id = Convert.ToInt32(dtAllergy.Rows[i]["allergy_id"].ToString());
        //            oclsProduct.Insert_Allergy_By_Product();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("Product_Master:insert_Allergy:" + ex.Message);
        //    }
        //}
        public void Insert(clsProduct product, string conn)
        {
            int newProductId;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", product.cmp_id);
                cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@login_id", product.LoginId);
                cmd.Parameters.AddWithValue("@course_id", product.CourseId);
                cmd.Parameters.AddWithValue("@department_id", product.DepartmentId);
                cmd.Parameters.AddWithValue("@list", product.List);
                cmd.Parameters.AddWithValue("@other_id", product.OtherId);
                cmd.Parameters.AddWithValue("@other_size", product.OtherSize);
                cmd.Parameters.AddWithValue("@ForKiosk", product.ForKiosk);
                cmd.Parameters.AddWithValue("@Is_OutOfStock", product.IsOutOfStock);
                cmd.Parameters.AddWithValue("@Cloak_Room", product.CloakRoom);
                //cmd.Parameters.AddWithValue("@is_active", product.IsActive);
                cmd.Parameters.AddWithValue("@Is_Condiment", product.IsCondiment);
                cmd.Parameters.AddWithValue("@IsHouse", product.IsHouse);
                cmd.Parameters.AddWithValue("@IsPkgProduct", product.IsPkgProduct);
                cmd.Parameters.AddWithValue("@Is_PriceOnScaleWeight", product.IsPriceOnScaleWeight);
                cmd.Parameters.AddWithValue("@is_stock", product.IsStock);
                cmd.Parameters.AddWithValue("@size_zero", product.SizeZero);
                cmd.Parameters.AddWithValue("@key_map_id", product.KeyMapId);
                cmd.Parameters.AddWithValue("@machine_id", product.MachineId);
                cmd.Parameters.AddWithValue("@Tax_id", product.TaxId);
                cmd.Parameters.AddWithValue("@Base", product.Base);
                cmd.Parameters.AddWithValue("@Unit_Id", product.UnitId);
                cmd.Parameters.AddWithValue("@Actual_Price", product.ActualPrice);
                cmd.Parameters.AddWithValue("@ip_address", (object)product.IpAddress ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tax", (object)product.Tax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@printer_id", (object)product.PrinterId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@description", (object)product.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@barcode", string.IsNullOrEmpty(product.Barcode) ? "" : product.Barcode);
                cmd.Parameters.AddWithValue("@SortingNo", product.SortingNo);
                //SqlParameter outputIdParam = new SqlParameter("@product_id", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(outputIdParam);
                //cmd.Parameters.AddWithValue("@SortingNo", product.Sorting_No);
                if (!string.IsNullOrEmpty(product.Name))
                {
                    cmd.Parameters.AddWithValue("@name", product.Name);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@name", DBNull.Value); // Or handle according to your stored procedure's requirement
                }
                //if (!string.IsNullOrEmpty(product.SName))
                //{
                //    cmd.Parameters.AddWithValue("@name", product.SName);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@name", DBNull.Value); // Or handle according to your stored procedure's requirement
                //}
                cmd.Parameters.AddWithValue("@code", (object)product.Code ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");
                //newProductId = (int)outputIdParam.Value;
                //HttpContext.Session.SetString("Id", newProductId.ToString());

                con.Open();

                cmd.ExecuteNonQuery();
                // newProductId = (int)cmd.Parameters["@product_id"].Value;

                // Store the product ID in session (if needed)
               // HttpContext.Session.SetString("Id", newProductId.ToString());


                con.Close();
                //newProductId = (int)outputIdParam.Value;
                //HttpContext.Session.SetString("Id", newProductId.ToString());
            }
        }

        //private async Task<List<AllergyModel>> LoadAllergies(int productId)
        //{
        //    var allergies = new List<AllergyModel>();
        //    string connectionString = _configuration.GetConnectionString("YourConnectionString");

        //    using (var conn = new SqlConnection(connectionString))
        //    {
        //        using (var cmd = new SqlCommand("Get_Product_Allergy", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = productId;

        //            await conn.OpenAsync();

        //            using (var rdr = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await rdr.ReadAsync())
        //                {
        //                    var allergy = new AllergyModel
        //                    {
        //                        AllergyId = rdr.GetInt32(rdr.GetOrdinal("AllergyId")),
        //                        AllergyName = rdr.GetString(rdr.GetOrdinal("Allergy"))
        //                    };
        //                    allergies.Add(allergy);
        //                }
        //            }
        //        }
        //    }

        //    return allergies;
        //}
   

    public void Update(clsProduct product, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", product.cmp_id);
                cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@login_id", product.LoginId);
                cmd.Parameters.AddWithValue("@course_id", product.CourseId);
                cmd.Parameters.AddWithValue("@department_id", product.DepartmentId);
                cmd.Parameters.AddWithValue("@list", product.List);
                cmd.Parameters.AddWithValue("@other_id", product.OtherId);
                cmd.Parameters.AddWithValue("@other_size", product.OtherSize);
                cmd.Parameters.AddWithValue("@ForKiosk", product.ForKiosk);
                cmd.Parameters.AddWithValue("@Is_OutOfStock", product.IsOutOfStock);
                cmd.Parameters.AddWithValue("@Cloak_Room", product.CloakRoom);
                //cmd.Parameters.AddWithValue("@is_active", product.IsActive);
                cmd.Parameters.AddWithValue("@Is_Condiment", product.IsCondiment);
                cmd.Parameters.AddWithValue("@IsHouse", product.IsHouse);
                cmd.Parameters.AddWithValue("@IsPkgProduct", product.IsPkgProduct);
                cmd.Parameters.AddWithValue("@Is_PriceOnScaleWeight", product.IsPriceOnScaleWeight);
                cmd.Parameters.AddWithValue("@is_stock", product.IsStock);
                cmd.Parameters.AddWithValue("@size_zero", product.SizeZero);
                cmd.Parameters.AddWithValue("@key_map_id", product.KeyMapId);
                cmd.Parameters.AddWithValue("@machine_id", product.MachineId);
                cmd.Parameters.AddWithValue("@Tax_id", product.TaxId);
                cmd.Parameters.AddWithValue("@Base", product.Base);
                cmd.Parameters.AddWithValue("@Unit_Id", product.UnitId);
                cmd.Parameters.AddWithValue("@Actual_Price", product.ActualPrice);
                cmd.Parameters.AddWithValue("@ip_address", (object)product.IpAddress ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tax", (object)product.Tax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@printer_id", (object)product.PrinterId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@description", (object)product.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@barcode", string.IsNullOrEmpty(product.Barcode) ? "" : product.Barcode);
                cmd.Parameters.AddWithValue("@SortingNo", product.SortingNo);
                //cmd.Parameters.AddWithValue("@SortingNo", product.Sorting_No);
                if (!string.IsNullOrEmpty(product.Name))
                {
                    cmd.Parameters.AddWithValue("@name", product.Name);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@name", DBNull.Value); // Or handle according to your stored procedure's requirement
                }
                cmd.Parameters.AddWithValue("@code", (object)product.Code ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetProductPrice(int locationId, int productId, string connStr)
        {
            var data = GetProductPriceLocation(locationId, productId, connStr);

            return base.View(data);
        }
        public clsProduct GetProductById(int product_Id, string ConnStr, int c_id, clsProduct product)
        {
            //clsProduct product = null;

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Get_m_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@product_id", product_Id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    // product.ProductId = Convert.ToInt32(rdr["Product_id"]);
                    product.Name = rdr["name"].ToString();

                    product.List = Convert.ToInt32(rdr["List"].ToString());

                    product.DepartmentId = Convert.ToInt32(rdr["Department_id"].ToString());
                    product.PrinterId = rdr["printer_id"].ToString();
                    product.CategoryId = Convert.ToInt32(rdr["Category_id"].ToString());
                    product.CourseId = Convert.ToInt32(rdr["Course_id"]);
                    //product.LocationId = Convert.ToInt32(rdr["location_id"]);
                    //product.ProductPriceId = Convert.ToInt32(rdr["Product_Price_Id"]);
                    product.Base = Convert.ToInt32(rdr["Base"]);
                    product.ActualPrice = Convert.ToDecimal(rdr["Actual_Price"]);
                    product.UnitId = Convert.ToInt32(rdr["Unit_id"].ToString());
                    product.DepartmentName = rdr["Department"].ToString();
                    product.GroupName = rdr["category_name"].ToString();
                    product.PrinterId = rdr["Printer_id"].ToString();
                    product.Description = rdr["description"].ToString();
                    product.UnitName = rdr["Unit"].ToString();
                    product.Sorting_No = Convert.ToDecimal(rdr["SortingNo"]);
                    // product.TaxId = Convert.ToInt32(rdr["Tax_id"].ToString());
                    // product.TaxName = rdr["tax_name"].ToString();
                    product.Price = float.Parse(rdr["price"].ToString());
                    product.OtherSize = rdr["other_size"].ToString();
                    product.OtherId = rdr["other_id"].ToString();
                    product.List = Convert.ToInt32(rdr["List"].ToString());
                    product.Barcode = rdr["barcode"].ToString();

                    product.IsActive = Convert.ToByte(rdr["is_active"]);
                    product.IsCondiment = Convert.ToByte(rdr["Is_Condiment"].ToString());
                    product.IsHouse = Convert.ToByte(rdr["IsHouse"].ToString());
                    product.ForKiosk = Convert.ToByte(rdr["ForKiosk"].ToString());

                    product.IsStock = Convert.ToByte(rdr["is_stock"]);
                    product.IsPkgProduct = Convert.ToByte(rdr["IsPkgProduct"].ToString());
                    product.IsPriceOnScaleWeight = Convert.ToByte(rdr["Is_PriceOnScaleWeight"].ToString());

                    product.SizeZero = Convert.ToByte(rdr["size_zero"].ToString());
                    product.IsOutOfStock = Convert.ToByte(rdr["Is_OutOfStock"].ToString());

                    product.CloakRoom = Convert.ToByte(rdr["Cloak_Room"].ToString());

                }
                con.Close();
            }
            //product.PrinterIds = product.PrinterId?.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            product.PrinterId = product.PrinterId?.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

            return product;
        }
        public List<clsProduct> Select_Ingredient_By_Product(int cmpID, string connStr, int product_Id)
        {
            List<clsProduct> IngredientsList = new List<clsProduct>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Ingredient_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmpID);
                cmd.Parameters.AddWithValue("@Product_Id", product_Id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct prod = new clsProduct();
                    prod.Name = rdr["Product"].ToString();
                    prod.Size = float.Parse(rdr["Qty"].ToString());
                    prod.baseSize = rdr["base_size"].ToString();
                    prod.UnitId = Convert.ToInt32(rdr["Unit_id"].ToString());
                    IngredientsList.Add(prod);

                }
                con.Close();
            }
            return IngredientsList;
        }
        public List<clsProduct> Select_Condiment_By_Product(int cmpID, string connStr, int product_Id)
        {
            List<clsProduct> CondimentsList = new List<clsProduct>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Condiment_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmpID);
                cmd.Parameters.AddWithValue("@Product_Id", product_Id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct pro = new clsProduct();
                    pro.Condiment = rdr["Condiment"].ToString();
                    pro.ProductName = rdr["Product"].ToString();
                    pro.Price = float.Parse(rdr["Price"].ToString());
                    //product.baseSize = rdr["base_size"].ToString();
                    //product.UnitId = Convert.ToInt32(rdr["Unit_id"].ToString());
                    pro.ChoiceId = Convert.ToInt32(rdr["Choice"].ToString());

                    pro.maxselect = float.Parse(rdr["Max_select"].ToString());
                    pro.minselect = float.Parse(rdr["Min_select"].ToString());
                    CondimentsList.Add(pro);

                }



                con.Close();
            }

            return CondimentsList;
        }
        public List<clsProduct> Select_Allergy_By_Product(int cmpID, string connStr, int product_Id)
        {
            List<clsProduct> AllergyList = new List<clsProduct>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Get_Product_Allergy", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_Id", product_Id);
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            clsProduct pro = new clsProduct();
                            pro.Allergy = rdr["name"].ToString();
                            AllergyList.Add(pro);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception as needed
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            return AllergyList;
        }
        public List<clsProduct> Allergy(int cmpID, string ConnStr, int productId)
        {
             

            // Allergy list ko get karna
            List<clsProduct> allergyList = new List<clsProduct>();

            // Allergy data ko fetch karne ke liye method ko call karna
            allergyList = Select_Allergy_By_Product(cmpID, ConnStr, productId);

            // Allergy list ko return karna
            return allergyList;
        }

        public List<clsProduct> SelectBarcodeSize(int cmpID, string ConnStr, int product_Id)
        {
            string barcode = string.Empty;
            List<clsProduct> BarcodeList = new List<clsProduct>();
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Barcode_Size_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmpID);
                cmd.Parameters.AddWithValue("@Product_Id", product_Id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct pro = new clsProduct();
                    pro.Barcode = rdr["Barcode_size"].ToString();
                    pro.BarcodeID = Convert.ToInt32(rdr["Size_id"].ToString());
                    pro.SizeName = rdr["Size_unit"].ToString();
                    BarcodeList.Add(pro);
                }

                con.Close();
            }

            return BarcodeList;
        }

        public IActionResult Product_Master(int product_id, string ConnStr, int c_id, clsProduct product)
        {
            var products = GetProductById(product_id, ConnStr, c_id, product);
            var level = GetLevelById(product_id, ConnStr, c_id, product);
            if (products == null)
            {
                return new NotFoundResult();
            }

            var viewModel = new clsProduct()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                DepartmentId = product.DepartmentId,
                CategoryId = product.CategoryId,
                CourseId = product.CourseId,
                DTdepartment = GetDepartment(c_id, ConnStr),
                Base = product.Base,
                UnitId = product.UnitId,
                TaxId = product.TaxId,
                PrinterId = product.PrinterId,
                Description = product.Description,
                UnitName = product.UnitName,
                //LocationId = product.LocationId
            };

           // return View(viewModel);
            return base.View(viewModel);
        }

        public List<clsProduct> GetSizePriceByProduct(int? product_id, string connStr, int c_id,/* clsProduct products,*/ int location_id)
        {
            List<clsProduct> productsList = new List<clsProduct>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Size_N_Price_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", product_id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@location_id", location_id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct product = new clsProduct();
                    product.LocationId = Convert.ToInt32(rdr["Location_Id"]);
                    product.UnitId = Convert.ToInt32(rdr["Unit_Id"]);
                    product.ProductPriceId = Convert.ToInt32(rdr["Product_Price_Id"]);
                    product.TaxId = Convert.ToInt32(rdr["Tax_Id"]);
                    product.ClickAndCollect = Convert.ToInt32(rdr["click_and_collect"]);
                    product.Deliver = Convert.ToInt32(rdr["deliver"]);
                    product.OrderAtTable = Convert.ToInt32(rdr["Order_at_table"]);
                    product.IsActive = Convert.ToByte(rdr["active"]);
                    product.SName = rdr["name"].ToString();
                    product.tName = rdr["name"].ToString();
                    //product.UnitName = rdr["Unit"].ToString();
                    product.sbaseSize = rdr["Size"].ToString();
                    product.Price = float.Parse(rdr["price"].ToString());
                    product.SortingNo = Convert.ToDecimal(rdr["Sorting_No"]);
                    productsList.Add(product);
                }
                con.Close();
            }
            return productsList;
        }
        public List<clsProduct> GetSizeCostByProduct(int? product_id, string connStr, int c_id, /*clsProduct products,*/ int location_id)
        {
            List<clsProduct> Products = new List<clsProduct>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Size_N_Cost_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", product_id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProduct product = new clsProduct();
                    product.BLocationId = Convert.ToInt32(rdr["Location_Id"]);
                    //products.ProductPriceId = Convert.ToInt32(rdr["Product_Price_Id"]);
                    product.BTaxId = Convert.ToInt32(rdr["Tax_Id"]);
                    //products.ClickAndCollect = Convert.ToInt32(rdr["click_and_collect"]);
                    //products.Deliver = Convert.ToInt32(rdr["deliver"]);
                    //products.OrderAtTable = Convert.ToInt32(rdr["Order_at_table"]);
                    //products.IsActive = Convert.ToByte(rdr["active"]);
                    product.BUnitId = Convert.ToByte(rdr["Unit_id"]);
                    product.tName = rdr["name"].ToString();
                    //   products.BUnitName = rdr["Unit"].ToString();
                    product.BbaseSize = rdr["Size"].ToString();
                    product.BPrice = float.Parse(rdr["price"].ToString());
                    //products.SortingNo = Convert.ToDecimal(rdr["sorting_no"]);
                    Products.Add(product);
                }
                con.Close();
            }
            return Products;
        }


        public clsProduct GetLevelById(int? product_id, string connStr, int c_id, clsProduct product)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Size_N_Price_By_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@product_id", product_id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.ProductPriceId = Convert.ToInt32(rdr["Product_Price_Id"]);
                    product.ClickAndCollect = Convert.ToInt32(rdr["click_and_collect"]);
                    product.Deliver = Convert.ToInt32(rdr["deliver"]);
                    product.OrderAtTable = Convert.ToInt32(rdr["Order_at_table"]);
                    product.IsActive = Convert.ToByte(rdr["active"]);
                    product.SName = rdr["name"].ToString();
                    product.LocationId = Convert.ToInt32(rdr["Location_Id"]);
                    product.baseSize = rdr["Size"].ToString();
                    product.Price = float.Parse(rdr["price"].ToString());
                    product.SortingNo = Convert.ToDecimal(rdr["Sorting_No"]);
                }
                con.Close();
            }
            return product;
        }


        public List<SelectListItem> GetCategories(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Category_By_Cmp", con);
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
        public List<SelectListItem> GetDepartment(int? cmpID, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", cmpID);

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

        public List<SelectListItem> GetCourse(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["course_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return categories;
        }
        public List<SelectListItem> GetUnit(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Unit_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["Unit_id"].ToString(),
                        Text = rdr["Unit"].ToString()
                    });
                }

                con.Close();
            }

            return categories;
        }
        public List<SelectListItem> GetLocation(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["Location_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return categories;
        }
        public List<SelectListItem> GetPrice(int? c_id, string conn)
        {
            List<SelectListItem> price = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_PricesBy_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    price.Add(new SelectListItem
                    {
                        Value = rdr["Product_Price_Id"].ToString(),
                        Text = rdr["PPrice"].ToString()
                    });
                }

                con.Close();
            }

            return price;
        }

        public List<SelectListItem> GetPrinter(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Printer_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["printer_Id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return categories;
        }

        public IActionResult Index(int? c_id, string conn, bool isActive)
        {

            bool isActiveValue = isActive;
            var model = new Product_MasterModel(_configuration)
            {
                DT = GetCategories(c_id, conn),
                DTdepartment = GetDepartment(c_id, conn),
                DTcourse = GetCourse(c_id, conn),
                DTUnit = GetUnit(c_id, conn),
                DTLocation = GetLocation(c_id, conn),
                DTPrice = GetPrice(c_id, conn)


            };


            var products = SelectAll(null, conn, isActiveValue);

            //ViewDataDictionary ViewData = new ViewDataDictionary();
            ViewData["Products"] = products;


            return base.View(model);
        }



        public List<SelectListItem> GetTax(int c_id, string conn)
        {
            List<SelectListItem> tax = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Tax_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tax.Add(new SelectListItem
                    {
                        Value = rdr["Tax_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }
            return tax;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public List<clsProduct> SaveSellingSizesAndPrices(string conn, [FromBody] List<clsProduct> productList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    if (productList == null || !productList.Any())
                    {
                        throw new Exception("Product list is empty or not received correctly.");
                    }
                    foreach (var prdt in productList)
                    {
                        using (SqlCommand command = new SqlCommand("P_M_Size_N_Cost", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters for the stored procedure
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
                            //  command.Parameters.AddWithValue("@Tax2", product.Tax2);
                            //command.Parameters.AddWithValue("@Tax_id2", product.TaxId2);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                return productList;
            }
            catch (SqlException ex)
            {
                // Handle the exception, maybe log it
                throw new Exception("An error occurred while saving data.", ex);
            }
        }
    }
}
