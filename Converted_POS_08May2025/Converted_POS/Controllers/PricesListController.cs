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
    public class PricesListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PricesListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/PricesList/GetPrices/{companyId}
        [HttpGet("GetPrices/{companyId}")]
        public ActionResult<IEnumerable<PricesListModel>> GetPrices(int companyId)
        {
            try
            {
                var prices = new List<PricesListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var price = new PricesListModel
                        {
                            ProductPriceId = Convert.ToInt32(reader["Product_Price_Id"]),
                            PriceName = reader["PPrice"].ToString(),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes" || Convert.ToBoolean(reader["is_active"]),
                            IsDefault = reader["is_default"] != DBNull.Value && Convert.ToBoolean(reader["is_default"])
                        };
                        prices.Add(price);
                    }
                }

                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PricesList/GetPriceById/{id}/{companyId}
        [HttpGet("GetPriceById/{id}/{companyId}")]
        public ActionResult<PricesListModel> GetPriceById(int id, int companyId)
        {
            try
            {
                PricesListModel price = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_Price_Id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        price = new PricesListModel
                        {
                            ProductPriceId = id,
                            PriceName = reader["PPrice"].ToString(),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes" || Convert.ToBoolean(reader["is_active"]),
                            IsDefault = reader["is_default"] != DBNull.Value && Convert.ToBoolean(reader["is_default"])
                        };
                    }
                }

                if (price == null)
                {
                    return NotFound($"Price with ID {id} not found");
                }

                return Ok(price);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PricesList/GetDefaultPrice/{companyId}
        [HttpGet("GetDefaultPrice/{companyId}")]
        public ActionResult<PricesListModel> GetDefaultPrice(int companyId)
        {
            try
            {
                PricesListModel price = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Default_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        price = new PricesListModel
                        {
                            ProductPriceId = Convert.ToInt32(reader["Product_Price_Id"]),
                            PriceName = reader["PPrice"].ToString(),
                            CompanyId = companyId,
                            IsActive = true,
                            IsDefault = true
                        };
                    }
                }

                if (price == null)
                {
                    return NotFound("No default price found");
                }

                return Ok(price);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PricesList/GetPricesByProduct/{productId}/{companyId}
        [HttpGet("GetPricesByProduct/{productId}/{companyId}")]
        public ActionResult<IEnumerable<PricesListModel>> GetPricesByProduct(int productId, int companyId)
        {
            try
            {
                var prices = new List<PricesListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Prices_By_Product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_id", productId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var price = new PricesListModel
                        {
                            ProductPriceId = reader["Product_Price_Id"] != DBNull.Value ? Convert.ToInt32(reader["Product_Price_Id"]) : 0,
                            PriceId = reader["Price_id"] != DBNull.Value ? Convert.ToInt32(reader["Price_id"]) : (int?)null,
                            PriceName = reader["PPrice"] != DBNull.Value ? reader["PPrice"].ToString() : "",
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : (decimal?)null,
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : (decimal?)null,
                            Tax = reader["Tax"] != DBNull.Value ? Convert.ToDecimal(reader["Tax"]) : (decimal?)null,
                            LocationId = reader["Location_Id"] != DBNull.Value ? Convert.ToInt32(reader["Location_Id"]) : (int?)null,
                            SizeId = reader["Size_Id"] != DBNull.Value ? Convert.ToInt32(reader["Size_Id"]) : (int?)null,
                            ProductId = productId,
                            CompanyId = companyId,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        prices.Add(price);
                    }
                }

                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PricesList/AddPrice
        [HttpPost("AddPrice")]
        public ActionResult AddPrice([FromBody] PricesListModel price)
        {
            try
            {
                if (price == null)
                {
                    return BadRequest("Price data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Product_Price_Id", 0); // New price
                    cmd.Parameters.AddWithValue("@PPrice", price.PriceName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", price.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", price.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", price.LoginId ?? HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", price.IsActive);
                    cmd.Parameters.AddWithValue("@is_default", price.IsDefault);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Price added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PricesList/UpdatePrice/{id}
        [HttpPut("UpdatePrice/{id}")]
        public ActionResult UpdatePrice(int id, [FromBody] PricesListModel price)
        {
            try
            {
                if (price == null)
                {
                    return BadRequest("Price data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Product_Price_Id", id);
                    cmd.Parameters.AddWithValue("@PPrice", price.PriceName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", price.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", price.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", price.LoginId ?? HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", price.IsActive);
                    cmd.Parameters.AddWithValue("@is_default", price.IsDefault);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Price with ID {id} not found");
                    }
                }

                return Ok("Price updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PricesList/DeletePrice/{id}
        [HttpDelete("DeletePrice/{id}")]
        public ActionResult DeletePrice(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Product_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Product_Price_Id", id);
                    cmd.Parameters.AddWithValue("@PPrice", "");
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@is_default", false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Price with ID {id} not found");
                    }
                }

                return Ok("Price deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PricesList/AddPriceDetail
        [HttpPost("AddPriceDetail")]
        public ActionResult AddPriceDetail([FromBody] PricesListModel price)
        {
            try
            {
                if (price == null)
                {
                    return BadRequest("Price detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Price_id", 0); // New price detail
                    cmd.Parameters.AddWithValue("@cmp_id", price.CompanyId);
                    cmd.Parameters.AddWithValue("@Location_Id", price.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_Id", price.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", price.Price ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", price.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", price.LoginId ?? HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Actual_Price", price.ActualPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax", price.Tax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Product_id", price.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Price detail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PricesList/UpdatePriceDetail/{id}
        [HttpPut("UpdatePriceDetail/{id}")]
        public ActionResult UpdatePriceDetail(int id, [FromBody] PricesListModel price)
        {
            try
            {
                if (price == null)
                {
                    return BadRequest("Price detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Price_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", price.CompanyId);
                    cmd.Parameters.AddWithValue("@Location_Id", price.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_Id", price.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", price.Price ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", price.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", price.LoginId ?? HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Actual_Price", price.ActualPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax", price.Tax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Product_id", price.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Price detail with ID {id} not found");
                    }
                }

                return Ok("Price detail updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PricesList/DeletePriceDetail/{id}
        [HttpDelete("DeletePriceDetail/{id}")]
        public ActionResult DeletePriceDetail(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Price_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@Location_Id", 0);
                    cmd.Parameters.AddWithValue("@Size_Id", 0);
                    cmd.Parameters.AddWithValue("@Price", 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Actual_Price", 0);
                    cmd.Parameters.AddWithValue("@Tax", 0);
                    cmd.Parameters.AddWithValue("@Product_id", 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Price detail with ID {id} not found");
                    }
                }

                return Ok("Price detail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 