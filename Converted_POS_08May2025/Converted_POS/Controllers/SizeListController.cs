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
    public class SizeListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SizeListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/SizeList/GetSizes/{companyId}
        [HttpGet("GetSizes/{companyId}")]
        public ActionResult<IEnumerable<SizeModel>> GetSizes(int companyId)
        {
            try
            {
                var sizes = new List<SizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var size = new SizeModel
                        {
                            SizeId = Convert.ToInt32(reader["Size_id"]),
                            CompanyId = companyId,
                            Name = reader["Name"].ToString(),
                            Size = reader["Size"].ToString(),
                            UnitId = reader["Unit_id"] != DBNull.Value ? Convert.ToInt32(reader["Unit_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null
                        };
                        sizes.Add(size);
                    }
                }

                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SizeList/GetSizeById/{id}/{companyId}
        [HttpGet("GetSizeById/{id}/{companyId}")]
        public ActionResult<SizeModel> GetSizeById(int id, int companyId)
        {
            try
            {
                SizeModel size = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        size = new SizeModel
                        {
                            SizeId = id,
                            CompanyId = companyId,
                            Name = reader["Name"].ToString(),
                            Size = reader["Size"].ToString(),
                            UnitId = reader["Unit_id"] != DBNull.Value ? Convert.ToInt32(reader["Unit_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null
                        };
                    }
                }

                if (size == null)
                {
                    return NotFound($"Size with ID {id} not found");
                }

                return Ok(size);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SizeList/GetSizesByProduct/{productId}/{companyId}
        [HttpGet("GetSizesByProduct/{productId}/{companyId}")]
        public ActionResult<IEnumerable<SizeModel>> GetSizesByProduct(int productId, int companyId)
        {
            try
            {
                var sizes = new List<SizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product_By_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_id", productId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var size = new SizeModel
                        {
                            SizeId = Convert.ToInt32(reader["Size_id"]),
                            CompanyId = companyId,
                            ProductId = productId,
                            Name = reader["Name"].ToString(),
                            Size = reader["Size"].ToString(),
                            UnitId = reader["Unit_id"] != DBNull.Value ? Convert.ToInt32(reader["Unit_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            Deliver = reader["deliver"] != DBNull.Value && Convert.ToBoolean(reader["deliver"]),
                            OrderAtTable = reader["Order_at_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_at_table"]),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null
                        };
                        sizes.Add(size);
                    }
                }

                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SizeList/GetSizesByProductAndLocation/{productId}/{locationId}/{companyId}
        [HttpGet("GetSizesByProductAndLocation/{productId}/{locationId}/{companyId}")]
        public ActionResult<IEnumerable<SizeModel>> GetSizesByProductAndLocation(int productId, int locationId, int companyId)
        {
            try
            {
                var sizes = new List<SizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product_By_Size_Location", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_id", productId);
                    cmd.Parameters.AddWithValue("@Location_Id", locationId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var size = new SizeModel
                        {
                            SizeId = Convert.ToInt32(reader["Size_id"]),
                            CompanyId = companyId,
                            ProductId = productId,
                            LocationId = locationId,
                            Name = reader["Name"].ToString(),
                            Size = reader["Size"].ToString(),
                            UnitId = reader["Unit_id"] != DBNull.Value ? Convert.ToInt32(reader["Unit_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : (decimal?)null,
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : (decimal?)null,
                            Tax = reader["Tax"] != DBNull.Value ? Convert.ToDecimal(reader["Tax"]) : (decimal?)null,
                            TaxId = reader["Tax_id"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id"]) : (decimal?)null,
                            Tax2 = reader["Tax2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax2"]) : (decimal?)null,
                            TaxId2 = reader["Tax_id2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id2"]) : (decimal?)null,
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            Deliver = reader["deliver"] != DBNull.Value && Convert.ToBoolean(reader["deliver"]),
                            OrderAtTable = reader["Order_at_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_at_table"]),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null
                        };
                        sizes.Add(size);
                    }
                }

                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SizeList/AddSize
        [HttpPost("AddSize")]
        public ActionResult<int> AddSize([FromBody] SizeModel size)
        {
            try
            {
                if (size == null)
                {
                    return BadRequest("Size data is null");
                }

                int sizeId = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Size_id", 0); // New size
                    cmd.Parameters.AddWithValue("@cmp_id", size.CompanyId);
                    cmd.Parameters.AddWithValue("@Size", size.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@product_id", size.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit_id", size.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Name", size.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                    cmd.Parameters.AddWithValue("@is_active", size.IsActive);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sizeId = Convert.ToInt32(reader["Size_Id"]);
                    }
                }

                return Ok(new { sizeId = sizeId, message = "Size added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SizeList/UpdateSize/{id}
        [HttpPut("UpdateSize/{id}")]
        public ActionResult UpdateSize(int id, [FromBody] SizeModel size)
        {
            try
            {
                if (size == null)
                {
                    return BadRequest("Size data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", size.CompanyId);
                    cmd.Parameters.AddWithValue("@Size", size.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@product_id", size.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit_id", size.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Name", size.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                    cmd.Parameters.AddWithValue("@is_active", size.IsActive);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Size with ID {id} not found");
                    }
                }

                return Ok("Size updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SizeList/DeleteSize/{id}
        [HttpDelete("DeleteSize/{id}")]
        public ActionResult DeleteSize(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@Size", "");
                    cmd.Parameters.AddWithValue("@product_id", 0);
                    cmd.Parameters.AddWithValue("@Unit_id", 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    cmd.Parameters.AddWithValue("@is_active", 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Size with ID {id} not found");
                    }
                }

                return Ok("Size deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SizeList/SaveSizeAndPrice
        [HttpPost("SaveSizeAndPrice")]
        public ActionResult<int> SaveSizeAndPrice([FromBody] SizeModel size)
        {
            try
            {
                if (size == null)
                {
                    return BadRequest("Size and price data is null");
                }

                int sizeId = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size_N_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", size.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", size.SizeId);
                    cmd.Parameters.AddWithValue("@Name", size.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", size.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit_id", size.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", size.IsActive);

                    cmd.Parameters.AddWithValue("@Location_Id", size.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price_id", size.PriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", size.Price ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Actual_Price", size.ActualPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax", size.Tax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id", size.TaxId ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@Tax2", size.Tax2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id2", size.TaxId2 ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@cmp_id", size.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@action", size.Action ?? "A");

                    cmd.Parameters.AddWithValue("@click_and_collect", size.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@deliver", size.Deliver);
                    cmd.Parameters.AddWithValue("@Order_at_table", size.OrderAtTable);
                    cmd.Parameters.AddWithValue("@Product_Price_Id", size.ProductPriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", size.SortingNo ?? (object)DBNull.Value);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sizeId = Convert.ToInt32(reader["Size_Id"]);
                    }
                }

                return Ok(new { sizeId = sizeId, message = "Size and price saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SizeList/DeleteSizeAndPrice
        [HttpDelete("DeleteSizeAndPrice")]
        public ActionResult DeleteSizeAndPrice([FromBody] SizeModel size)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_M_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Size_id", size.SizeId);
                    cmd.Parameters.AddWithValue("@cmp_id", size.CompanyId);
                    cmd.Parameters.AddWithValue("@Product_Price_Id", size.ProductPriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price_Id", size.PriceId ?? (object)DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Size and price deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SizeList/UpdateActiveStatus/{id}
        [HttpPut("UpdateActiveStatus/{id}")]
        public ActionResult UpdateActiveStatus(int id, [FromBody] SizeModel size)
        {
            try
            {
                if (size == null)
                {
                    return BadRequest("Size data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("M_Size_Update", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", size.CompanyId);
                    cmd.Parameters.AddWithValue("@is_active", size.IsActive);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Size active status updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 