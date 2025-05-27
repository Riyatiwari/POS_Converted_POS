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
    public class PriceBySizeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PriceBySizeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/PriceBySize/GetPricesByProductSize/{productId}/{sizeId}/{companyId}
        [HttpGet("GetPricesByProductSize/{productId}/{sizeId}/{companyId}")]
        public ActionResult<IEnumerable<PriceBySizeModel>> GetPricesByProductSize(int productId, int sizeId, int companyId)
        {
            try
            {
                var pricesList = new List<PriceBySizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Price_By_Product_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_id", productId);
                    cmd.Parameters.AddWithValue("@Size_id", sizeId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var priceBySize = new PriceBySizeModel
                        {
                            ProductId = productId,
                            SizeId = sizeId,
                            CompanyId = companyId,
                            PriceId = reader["Price_id"] != DBNull.Value ? Convert.ToInt32(reader["Price_id"]) : (int?)null,
                            LocationId = reader["Location_Id"] != DBNull.Value ? Convert.ToInt32(reader["Location_Id"]) : (int?)null,
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0,
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : 0,
                            Tax = reader["Tax"] != DBNull.Value ? Convert.ToDecimal(reader["Tax"]) : 0,
                            TaxId = reader["Tax_id"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id"]) : (decimal?)null,
                            Tax2 = reader["Tax2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax2"]) : (decimal?)null,
                            TaxId2 = reader["Tax_id2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id2"]) : (decimal?)null,
                            ProductPriceId = reader["Product_Price_Id"] != DBNull.Value ? Convert.ToInt32(reader["Product_Price_Id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        pricesList.Add(priceBySize);
                    }
                }

                return Ok(pricesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PriceBySize/GetPricesByLocation/{locationId}/{companyId}
        [HttpGet("GetPricesByLocation/{locationId}/{companyId}")]
        public ActionResult<IEnumerable<PriceBySizeModel>> GetPricesByLocation(int locationId, int companyId)
        {
            try
            {
                var pricesList = new List<PriceBySizeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Prices_By_Location", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Location_Id", locationId);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var priceBySize = new PriceBySizeModel
                        {
                            ProductId = reader["Product_id"] != DBNull.Value ? Convert.ToInt32(reader["Product_id"]) : (int?)null,
                            SizeId = reader["Size_id"] != DBNull.Value ? Convert.ToInt32(reader["Size_id"]) : 0,
                            CompanyId = companyId,
                            PriceId = reader["Price_id"] != DBNull.Value ? Convert.ToInt32(reader["Price_id"]) : (int?)null,
                            LocationId = locationId,
                            Name = reader["Name"]?.ToString(),
                            Size = reader["Size"]?.ToString(),
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0,
                            ActualPrice = reader["Actual_Price"] != DBNull.Value ? Convert.ToDecimal(reader["Actual_Price"]) : 0,
                            Tax = reader["Tax"] != DBNull.Value ? Convert.ToDecimal(reader["Tax"]) : 0,
                            TaxId = reader["Tax_id"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id"]) : (decimal?)null,
                            Tax2 = reader["Tax2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax2"]) : (decimal?)null,
                            TaxId2 = reader["Tax_id2"] != DBNull.Value ? Convert.ToDecimal(reader["Tax_id2"]) : (decimal?)null,
                            ProductPriceId = reader["Product_Price_Id"] != DBNull.Value ? Convert.ToInt32(reader["Product_Price_Id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        pricesList.Add(priceBySize);
                    }
                }

                return Ok(pricesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PriceBySize/SavePriceBySize
        [HttpPost("SavePriceBySize")]
        public ActionResult<int> SavePriceBySize([FromBody] PriceBySizeModel priceBySize)
        {
            try
            {
                if (priceBySize == null)
                {
                    return BadRequest("Price data is null");
                }

                int priceId = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size_N_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", priceBySize.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", priceBySize.SizeId);
                    cmd.Parameters.AddWithValue("@Name", priceBySize.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", priceBySize.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit_id", priceBySize.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", priceBySize.IsActive);

                    cmd.Parameters.AddWithValue("@Location_Id", priceBySize.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price_id", priceBySize.PriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", priceBySize.Price);
                    cmd.Parameters.AddWithValue("@Actual_Price", priceBySize.ActualPrice);
                    cmd.Parameters.AddWithValue("@Tax", priceBySize.Tax);
                    cmd.Parameters.AddWithValue("@Tax_id", priceBySize.TaxId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax2", priceBySize.Tax2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id2", priceBySize.TaxId2 ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@cmp_id", priceBySize.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@action", priceBySize.Action ?? "A");

                    cmd.Parameters.AddWithValue("@click_and_collect", priceBySize.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@deliver", priceBySize.Deliver);
                    cmd.Parameters.AddWithValue("@Order_at_table", priceBySize.OrderAtTable);
                    cmd.Parameters.AddWithValue("@Product_Price_Id", priceBySize.ProductPriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", priceBySize.SortingNo ?? (object)DBNull.Value);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        priceId = Convert.ToInt32(reader["Price_Id"] != DBNull.Value ? reader["Price_Id"] : 0);
                    }
                }

                return Ok(new { priceId = priceId, message = "Price by size saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PriceBySize/UpdatePriceBySize/{priceId}
        [HttpPut("UpdatePriceBySize/{priceId}")]
        public ActionResult UpdatePriceBySize(int priceId, [FromBody] PriceBySizeModel priceBySize)
        {
            try
            {
                if (priceBySize == null)
                {
                    return BadRequest("Price data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Size_N_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@product_id", priceBySize.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", priceBySize.SizeId);
                    cmd.Parameters.AddWithValue("@Name", priceBySize.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", priceBySize.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit_id", priceBySize.UnitId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", priceBySize.IsActive);

                    cmd.Parameters.AddWithValue("@Location_Id", priceBySize.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price_id", priceId);
                    cmd.Parameters.AddWithValue("@Price", priceBySize.Price);
                    cmd.Parameters.AddWithValue("@Actual_Price", priceBySize.ActualPrice);
                    cmd.Parameters.AddWithValue("@Tax", priceBySize.Tax);
                    cmd.Parameters.AddWithValue("@Tax_id", priceBySize.TaxId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax2", priceBySize.Tax2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tax_id2", priceBySize.TaxId2 ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@cmp_id", priceBySize.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@action", priceBySize.Action ?? "U");

                    cmd.Parameters.AddWithValue("@click_and_collect", priceBySize.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@deliver", priceBySize.Deliver);
                    cmd.Parameters.AddWithValue("@Order_at_table", priceBySize.OrderAtTable);
                    cmd.Parameters.AddWithValue("@Product_Price_Id", priceBySize.ProductPriceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", priceBySize.SortingNo ?? (object)DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Price by size updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PriceBySize/DeletePriceBySize/{priceId}
        [HttpDelete("DeletePriceBySize/{priceId}")]
        public ActionResult DeletePriceBySize(int priceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_M_Price", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Price_id", priceId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Price by size deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PriceBySize/UpdatePricesBatch
        [HttpPost("UpdatePricesBatch")]
        public ActionResult UpdatePricesBatch([FromBody] List<PriceBySizeModel> prices)
        {
            try
            {
                if (prices == null || prices.Count == 0)
                {
                    return BadRequest("Prices data is null or empty");
                }

                int successCount = 0;
                string errorMessages = "";

                foreach (var priceBySize in prices)
                {
                    try
                    {
                        using (var connection = new SqlConnection(_connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("P_M_Size_N_Price", connection);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@product_id", priceBySize.ProductId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Size_id", priceBySize.SizeId);
                            cmd.Parameters.AddWithValue("@Name", priceBySize.Name ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Size", priceBySize.Size ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Unit_id", priceBySize.UnitId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@is_active", priceBySize.IsActive);

                            cmd.Parameters.AddWithValue("@Location_Id", priceBySize.LocationId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Price_id", priceBySize.PriceId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Price", priceBySize.Price);
                            cmd.Parameters.AddWithValue("@Actual_Price", priceBySize.ActualPrice);
                            cmd.Parameters.AddWithValue("@Tax", priceBySize.Tax);
                            cmd.Parameters.AddWithValue("@Tax_id", priceBySize.TaxId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Tax2", priceBySize.Tax2 ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Tax_id2", priceBySize.TaxId2 ?? (object)DBNull.Value);

                            cmd.Parameters.AddWithValue("@cmp_id", priceBySize.CompanyId);
                            cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                            cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                            cmd.Parameters.AddWithValue("@action", priceBySize.Action ?? "A");

                            cmd.Parameters.AddWithValue("@click_and_collect", priceBySize.ClickAndCollect);
                            cmd.Parameters.AddWithValue("@deliver", priceBySize.Deliver);
                            cmd.Parameters.AddWithValue("@Order_at_table", priceBySize.OrderAtTable);
                            cmd.Parameters.AddWithValue("@Product_Price_Id", priceBySize.ProductPriceId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@sorting_no", priceBySize.SortingNo ?? (object)DBNull.Value);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            successCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages += $"Error processing price for product {priceBySize.ProductId}, size {priceBySize.SizeId}: {ex.Message}. ";
                    }
                }

                if (successCount == prices.Count)
                {
                    return Ok($"All {successCount} prices updated successfully");
                }
                else
                {
                    return Ok($"{successCount} of {prices.Count} prices updated successfully. Errors: {errorMessages}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PriceBySize/UpdatePricesForLevel
        [HttpPost("UpdatePricesForLevel")]
        public ActionResult UpdatePricesForLevel([FromBody] PriceBySizeModel priceLevel)
        {
            try
            {
                if (priceLevel == null)
                {
                    return BadRequest("Price level data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Update_PriceByLevel", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FromlevelID", priceLevel.FromLevelId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TolevelID", priceLevel.ToLevelId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PriceType", priceLevel.PriceType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prc_Value", priceLevel.PriceValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", priceLevel.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Prices updated successfully for the specified level");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 