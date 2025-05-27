using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;

namespace Converted_POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingSizeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BuyingSizeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/BuyingSize/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<BuyingSizeModel>> GetAllBuyingSizes()
        {
            try
            {
                var buyingSizes = new List<BuyingSizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Buying_Sizes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var buyingSize = new BuyingSizeModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Size = Convert.ToDecimal(reader["size"]),
                            Unit = reader["unit"].ToString(),
                            Cost = reader["cost"] != DBNull.Value ? Convert.ToDecimal(reader["cost"]) : 0,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                        buyingSizes.Add(buyingSize);
                    }
                }

                return Ok(buyingSizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BuyingSize/GetByProductId/{productId}
        [HttpGet("GetByProductId/{productId}")]
        public ActionResult<IEnumerable<BuyingSizeModel>> GetBuyingSizesByProductId(int productId)
        {
            try
            {
                var buyingSizes = new List<BuyingSizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Buying_Sizes_By_Product_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", productId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var buyingSize = new BuyingSizeModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Size = Convert.ToDecimal(reader["size"]),
                            Unit = reader["unit"].ToString(),
                            Cost = reader["cost"] != DBNull.Value ? Convert.ToDecimal(reader["cost"]) : 0,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                        buyingSizes.Add(buyingSize);
                    }
                }

                return Ok(buyingSizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BuyingSize/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<BuyingSizeModel> GetBuyingSizeById(int id)
        {
            try
            {
                BuyingSizeModel buyingSize = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Buying_Size_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        buyingSize = new BuyingSizeModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Size = Convert.ToDecimal(reader["size"]),
                            Unit = reader["unit"].ToString(),
                            Cost = reader["cost"] != DBNull.Value ? Convert.ToDecimal(reader["cost"]) : 0,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                    }
                }

                if (buyingSize == null)
                {
                    return NotFound($"Buying size with ID {id} not found");
                }

                return Ok(buyingSize);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/BuyingSize/Add
        [HttpPost("Add")]
        public ActionResult AddBuyingSize([FromBody] BuyingSizeModel buyingSize)
        {
            try
            {
                if (buyingSize == null)
                {
                    return BadRequest("Buying size data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Buying_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", buyingSize.ProductId);
                    cmd.Parameters.AddWithValue("@name", buyingSize.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", buyingSize.Size);
                    cmd.Parameters.AddWithValue("@unit", buyingSize.Unit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cost", buyingSize.Cost);
                    cmd.Parameters.AddWithValue("@is_active", buyingSize.IsActive);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Buying size added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/BuyingSize/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateBuyingSize(int id, [FromBody] BuyingSizeModel buyingSize)
        {
            try
            {
                if (buyingSize == null)
                {
                    return BadRequest("Buying size data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Buying_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@product_id", buyingSize.ProductId);
                    cmd.Parameters.AddWithValue("@name", buyingSize.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", buyingSize.Size);
                    cmd.Parameters.AddWithValue("@unit", buyingSize.Unit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cost", buyingSize.Cost);
                    cmd.Parameters.AddWithValue("@is_active", buyingSize.IsActive);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Buying size with ID {id} not found");
                    }
                }

                return Ok("Buying size updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/BuyingSize/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteBuyingSize(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Buying_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Buying size with ID {id} not found");
                    }
                }

                return Ok("Buying size deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 