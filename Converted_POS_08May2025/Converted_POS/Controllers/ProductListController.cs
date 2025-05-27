using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

namespace Converted_POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProductListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/ProductList/GetProducts/{companyId}
        [HttpGet("GetProducts/{companyId}")]
        public ActionResult<IEnumerable<ProductModel>> GetProducts(int companyId)
        {
            try
            {
                var products = new List<ProductModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var product = new ProductModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            CompanyId = companyId,
                            CategoryId = reader["Category_id"] != DBNull.Value ? Convert.ToInt32(reader["Category_id"]) : (int?)null,
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0,
                            Barcode = reader["barcode"].ToString(),
                            Description = reader["description"].ToString(),
                            IsActive = reader["Is_active"] != DBNull.Value && Convert.ToBoolean(reader["Is_active"]),
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : (int?)null,
                            CourseId = reader["course_id"] != DBNull.Value ? Convert.ToInt32(reader["course_id"]) : (int?)null,
                            TaxId = reader["Tax_id"] != DBNull.Value ? Convert.ToInt32(reader["Tax_id"]) : (int?)null,
                            Tax = reader["Tax"].ToString(),
                            PrinterId = reader["printer_id"].ToString(),
                            IsIngredient = reader["Is_Ingredient"] != DBNull.Value && Convert.ToBoolean(reader["Is_Ingredient"]),
                            IsCondiment = reader["Is_Condiment"] != DBNull.Value && Convert.ToBoolean(reader["Is_Condiment"]),
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : (decimal?)null,
                            SortingNo = reader["SortingNo"] != DBNull.Value ? Convert.ToDecimal(reader["SortingNo"]) : (decimal?)null
                        };
                        products.Add(product);
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductList/GetProductById/{id}
        [HttpGet("GetProductById/{id}")]
        public ActionResult<ProductModel> GetProductById(int id)
        {
            try
            {
                ProductModel product = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        product = new ProductModel
                        {
                            ProductId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            CategoryId = reader["Category_id"] != DBNull.Value ? Convert.ToInt32(reader["Category_id"]) : (int?)null,
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0,
                            Barcode = reader["barcode"].ToString(),
                            Description = reader["description"].ToString(),
                            IsActive = reader["Is_active"] != DBNull.Value && Convert.ToBoolean(reader["Is_active"]),
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : (int?)null,
                            CourseId = reader["course_id"] != DBNull.Value ? Convert.ToInt32(reader["course_id"]) : (int?)null,
                            TaxId = reader["Tax_id"] != DBNull.Value ? Convert.ToInt32(reader["Tax_id"]) : (int?)null,
                            Tax = reader["Tax"].ToString(),
                            PrinterId = reader["printer_id"].ToString(),
                            IsIngredient = reader["Is_Ingredient"] != DBNull.Value && Convert.ToBoolean(reader["Is_Ingredient"]),
                            IsCondiment = reader["Is_Condiment"] != DBNull.Value && Convert.ToBoolean(reader["Is_Condiment"]),
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : (decimal?)null,
                            SortingNo = reader["SortingNo"] != DBNull.Value ? Convert.ToDecimal(reader["SortingNo"]) : (decimal?)null,
                            UnitId = reader["Unit_id"] != DBNull.Value ? Convert.ToInt32(reader["Unit_id"]) : (int?)null,
                            Image = reader["P_Image"] != DBNull.Value ? (byte[])reader["P_Image"] : null
                        };
                    }
                }

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductList/GetProductsByCategory/{categoryId}
        [HttpGet("GetProductsByCategory/{categoryId}")]
        public ActionResult<IEnumerable<ProductModel>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = new List<ProductModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Products_By_Category", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Category_id", categoryId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var product = new ProductModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            CategoryId = categoryId,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0,
                            IsActive = reader["Is_active"] != DBNull.Value && Convert.ToBoolean(reader["Is_active"])
                        };
                        products.Add(product);
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductList/GetProductsByDepartment/{departmentId}
        [HttpGet("GetProductsByDepartment/{departmentId}")]
        public ActionResult<IEnumerable<ProductModel>> GetProductsByDepartment(int departmentId)
        {
            try
            {
                var products = new List<ProductModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Products_By_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@department_id", departmentId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var product = new ProductModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            DepartmentId = departmentId,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0,
                            IsActive = reader["Is_active"] != DBNull.Value && Convert.ToBoolean(reader["Is_active"])
                        };
                        products.Add(product);
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ProductList/AddProduct
        [HttpPost("AddProduct")]
        public ActionResult AddProduct([FromBody] ProductModel product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", 0); // New product
                    cmd.Parameters.AddWithValue("@cmp_id", product.CompanyId);
                    cmd.Parameters.AddWithValue("@Category_id", product.CategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@code", product.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", product.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@barcode", product.Barcode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_active", product.IsActive);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@department_id", product.DepartmentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@course_id", product.CourseId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_id", product.PrinterId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Actual_Price", product.ActualPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id", product.TaxId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SortingNo", product.SortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_Ingredient", product.IsIngredient);
                    cmd.Parameters.AddWithValue("@Is_Condiment", product.IsCondiment);
                    cmd.Parameters.AddWithValue("@Unit_id", product.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@P_Image", product.Image ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Product added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ProductList/UpdateProduct/{id}
        [HttpPut("UpdateProduct/{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] ProductModel product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", product.CompanyId);
                    cmd.Parameters.AddWithValue("@Category_id", product.CategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@code", product.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", product.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@barcode", product.Barcode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_active", product.IsActive);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@department_id", product.DepartmentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@course_id", product.CourseId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_id", product.PrinterId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Actual_Price", product.ActualPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id", product.TaxId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SortingNo", product.SortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_Ingredient", product.IsIngredient);
                    cmd.Parameters.AddWithValue("@Is_Condiment", product.IsCondiment);
                    cmd.Parameters.AddWithValue("@Unit_id", product.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@P_Image", product.Image ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Product with ID {id} not found");
                    }
                }

                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ProductList/DeleteProduct/{id}
        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", id);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Product with ID {id} not found");
                    }
                }

                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ProductList/UploadProductImage/{id}
        [HttpPost("UploadProductImage/{id}")]
        public async Task<ActionResult> UploadProductImage(int id, IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file was uploaded.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("Update_Product_Image", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@product_id", id);
                        cmd.Parameters.AddWithValue("@P_Image", imageBytes);
                        cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                        cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"Product with ID {id} not found");
                        }
                    }
                }

                return Ok("Product image updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductList/GetProductImage/{id}
        [HttpGet("GetProductImage/{id}")]
        public ActionResult GetProductImage(int id)
        {
            try
            {
                byte[] image = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT P_Image FROM M_Product WHERE product_id = @product_id", connection);
                    cmd.Parameters.AddWithValue("@product_id", id);

                    connection.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        image = (byte[])result;
                    }
                }

                if (image == null || image.Length == 0)
                {
                    return NotFound($"Image for product ID {id} not found");
                }

                return File(image, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 