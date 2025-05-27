using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;

namespace Converted_POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCondimentListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProductCondimentListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/ProductCondimentList/GetCondiments/{companyId}
        [HttpGet("GetCondiments/{companyId}")]
        public ActionResult<IEnumerable<ProductCondimentModel>> GetCondiments(int companyId)
        {
            try
            {
                var condiments = new List<ProductCondimentModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var condiment = new ProductCondimentModel
                        {
                            CondimentId = Convert.ToInt32(reader["Condiment_id"]),
                            CompanyId = companyId,
                            Condiment = reader["Condiment"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            IsAddSubstract = reader["is_add_substract"] != DBNull.Value && Convert.ToBoolean(reader["is_add_substract"]),
                            Quantity = reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : (int?)null,
                            UnitId = reader["unit"] != DBNull.Value ? Convert.ToInt32(reader["unit"]) : (int?)null,
                            Choice = reader["choices"] != DBNull.Value ? Convert.ToInt32(reader["choices"]) : 0,
                            IsBySize = reader["IsBySize"] != DBNull.Value && Convert.ToBoolean(reader["IsBySize"]),
                            SizeId = reader["sizeID"] != DBNull.Value ? Convert.ToInt32(reader["sizeID"]) : (int?)null,
                            UseProductCondiment = reader["UseProductCondi"] != DBNull.Value && Convert.ToBoolean(reader["UseProductCondi"])
                        };
                        condiments.Add(condiment);
                    }
                }

                return Ok(condiments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductCondimentList/GetCondimentById/{id}/{companyId}
        [HttpGet("GetCondimentById/{id}/{companyId}")]
        public ActionResult<ProductCondimentModel> GetCondimentById(int id, int companyId)
        {
            try
            {
                ProductCondimentModel condiment = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Condiment_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        condiment = new ProductCondimentModel
                        {
                            CondimentId = id,
                            CompanyId = companyId,
                            Condiment = reader["Condiment"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            IsAddSubstract = reader["is_add_substract"] != DBNull.Value && Convert.ToBoolean(reader["is_add_substract"]),
                            Quantity = reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : (int?)null,
                            UnitId = reader["unit"] != DBNull.Value ? Convert.ToInt32(reader["unit"]) : (int?)null,
                            Choice = reader["choices"] != DBNull.Value ? Convert.ToInt32(reader["choices"]) : 0,
                            IsBySize = reader["IsBySize"] != DBNull.Value && Convert.ToBoolean(reader["IsBySize"]),
                            SizeId = reader["sizeID"] != DBNull.Value ? Convert.ToInt32(reader["sizeID"]) : (int?)null,
                            UseProductCondiment = reader["UseProductCondi"] != DBNull.Value && Convert.ToBoolean(reader["UseProductCondi"])
                        };
                    }
                }

                if (condiment == null)
                {
                    return NotFound($"Condiment with ID {id} not found");
                }

                return Ok(condiment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ProductCondimentList/GetCondimentsByProduct/{productId}/{companyId}
        [HttpGet("GetCondimentsByProduct/{productId}/{companyId}")]
        public ActionResult<IEnumerable<ProductCondimentModel>> GetCondimentsByProduct(int productId, int companyId)
        {
            try
            {
                var condiments = new List<ProductCondimentModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Condiment_By_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_Id", productId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var condiment = new ProductCondimentModel
                        {
                            CondimentId = reader["Condiment_id"] != DBNull.Value ? Convert.ToInt32(reader["Condiment_id"]) : 0,
                            CompanyId = companyId,
                            ProductId = productId,
                            Condiment = reader["Condiment"].ToString(),
                            ProductName = reader["Product"].ToString(),
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0,
                            Choice = reader["Choice"] != DBNull.Value ? Convert.ToInt32(reader["Choice"]) : 0,
                            MinSelect = reader["Min_select"] != DBNull.Value ? Convert.ToInt32(reader["Min_select"]) : 0,
                            MaxSelect = reader["Max_select"] != DBNull.Value ? Convert.ToInt32(reader["Max_select"]) : 0
                        };
                        condiments.Add(condiment);
                    }
                }

                return Ok(condiments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ProductCondimentList/AddCondiment
        [HttpPost("AddCondiment")]
        public ActionResult<int> AddCondiment([FromBody] ProductCondimentModel condiment)
        {
            try
            {
                if (condiment == null)
                {
                    return BadRequest("Condiment data is null");
                }

                int condimentId = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Condiment_id", 0); // New condiment
                    cmd.Parameters.AddWithValue("@cmp_id", condiment.CompanyId);
                    cmd.Parameters.AddWithValue("@Condiment", condiment.Condiment ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", condiment.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_add_substract", condiment.IsAddSubstract);
                    cmd.Parameters.AddWithValue("@quantity", condiment.Quantity ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@unit", condiment.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@choices", condiment.Choice);
                    cmd.Parameters.AddWithValue("@IsBySize", condiment.IsBySize ?? false);
                    cmd.Parameters.AddWithValue("@sizeID", condiment.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UseProductCondi", condiment.UseProductCondiment ?? false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        condimentId = Convert.ToInt32(reader["Condiment_Id"]);
                    }
                }

                return Ok(new { condimentId = condimentId, message = "Condiment added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ProductCondimentList/UpdateCondiment/{id}
        [HttpPut("UpdateCondiment/{id}")]
        public ActionResult UpdateCondiment(int id, [FromBody] ProductCondimentModel condiment)
        {
            try
            {
                if (condiment == null)
                {
                    return BadRequest("Condiment data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Condiment_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", condiment.CompanyId);
                    cmd.Parameters.AddWithValue("@Condiment", condiment.Condiment ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", condiment.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_add_substract", condiment.IsAddSubstract);
                    cmd.Parameters.AddWithValue("@quantity", condiment.Quantity ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@unit", condiment.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@choices", condiment.Choice);
                    cmd.Parameters.AddWithValue("@IsBySize", condiment.IsBySize ?? false);
                    cmd.Parameters.AddWithValue("@sizeID", condiment.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UseProductCondi", condiment.UseProductCondiment ?? false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Condiment with ID {id} not found");
                    }
                }

                return Ok("Condiment updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ProductCondimentList/DeleteCondiment/{id}
        [HttpDelete("DeleteCondiment/{id}")]
        public ActionResult DeleteCondiment(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Condiment_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@Condiment", "");
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_add_substract", false);
                    cmd.Parameters.AddWithValue("@quantity", 0);
                    cmd.Parameters.AddWithValue("@unit", 0);
                    cmd.Parameters.AddWithValue("@choices", 0);
                    cmd.Parameters.AddWithValue("@IsBySize", false);
                    cmd.Parameters.AddWithValue("@sizeID", 0);
                    cmd.Parameters.AddWithValue("@UseProductCondi", false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Condiment with ID {id} not found");
                    }
                }

                return Ok("Condiment deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ProductCondimentList/AssignCondimentToProduct
        [HttpPost("AssignCondimentToProduct")]
        public ActionResult AssignCondimentToProduct([FromBody] ProductCondimentModel condiment)
        {
            try
            {
                if (condiment == null)
                {
                    return BadRequest("Condiment assignment data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Trans_Product_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_Id", 0); // New transaction
                    cmd.Parameters.AddWithValue("@Condiment_Id", condiment.CondimentId);
                    cmd.Parameters.AddWithValue("@product_id", condiment.ProductId);
                    cmd.Parameters.AddWithValue("@cmp_id", condiment.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Price", condiment.Price);
                    cmd.Parameters.AddWithValue("@Choice", condiment.Choice);
                    cmd.Parameters.AddWithValue("@min_select", condiment.MinSelect);
                    cmd.Parameters.AddWithValue("@max_select", condiment.MaxSelect);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Condiment assigned to product successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ProductCondimentList/UpdateProductCondiment
        [HttpPut("UpdateProductCondiment")]
        public ActionResult UpdateProductCondiment([FromBody] ProductCondimentModel condiment)
        {
            try
            {
                if (condiment == null)
                {
                    return BadRequest("Product condiment data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Trans_Product_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_Id", condiment.TransactionId);
                    cmd.Parameters.AddWithValue("@Condiment_Id", condiment.CondimentId);
                    cmd.Parameters.AddWithValue("@product_id", condiment.ProductId);
                    cmd.Parameters.AddWithValue("@cmp_id", condiment.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Price", condiment.Price);
                    cmd.Parameters.AddWithValue("@Choice", condiment.Choice);
                    cmd.Parameters.AddWithValue("@min_select", condiment.MinSelect);
                    cmd.Parameters.AddWithValue("@max_select", condiment.MaxSelect);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Product condiment with Transaction ID {condiment.TransactionId} not found");
                    }
                }

                return Ok("Product condiment updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ProductCondimentList/RemoveProductCondiment/{transactionId}
        [HttpDelete("RemoveProductCondiment/{transactionId}")]
        public ActionResult RemoveProductCondiment(int transactionId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Trans_Product_Condiment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_Id", transactionId);
                    cmd.Parameters.AddWithValue("@Condiment_Id", 0);
                    cmd.Parameters.AddWithValue("@product_id", 0);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Price", 0);
                    cmd.Parameters.AddWithValue("@Choice", 0);
                    cmd.Parameters.AddWithValue("@min_select", 0);
                    cmd.Parameters.AddWithValue("@max_select", 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Product condiment with Transaction ID {transactionId} not found");
                    }
                }

                return Ok("Product condiment removed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 