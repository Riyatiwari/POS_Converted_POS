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
    public class BarcodeListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BarcodeListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/BarcodeList/GetBarcodes/{companyId}
        [HttpGet("GetBarcodes/{companyId}")]
        public ActionResult<IEnumerable<BarcodeModel>> GetBarcodes(int companyId)
        {
            try
            {
                var barcodes = new List<BarcodeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Barcode_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var barcode = new BarcodeModel
                        {
                            BarcodeSizeId = Convert.ToInt32(reader["Barcode_Size_id"]),
                            CompanyId = companyId,
                            Barcode = reader["Barcode"].ToString(),
                            SizeId = reader["Size_Id"] != DBNull.Value ? Convert.ToInt32(reader["Size_Id"]) : (int?)null,
                            ProductId = reader["product_id"] != DBNull.Value ? Convert.ToInt32(reader["product_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        barcodes.Add(barcode);
                    }
                }

                return Ok(barcodes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BarcodeList/GetBarcodeById/{id}/{companyId}
        [HttpGet("GetBarcodeById/{id}/{companyId}")]
        public ActionResult<BarcodeModel> GetBarcodeById(int id, int companyId)
        {
            try
            {
                BarcodeModel barcode = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Barcode_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);
                    cmd.Parameters.AddWithValue("@Barcode_Size_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        barcode = new BarcodeModel
                        {
                            BarcodeSizeId = id,
                            CompanyId = companyId,
                            Barcode = reader["Barcode"].ToString(),
                            SizeId = reader["Size_Id"] != DBNull.Value ? Convert.ToInt32(reader["Size_Id"]) : (int?)null,
                            ProductId = reader["product_id"] != DBNull.Value ? Convert.ToInt32(reader["product_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                    }
                }

                if (barcode == null)
                {
                    return NotFound($"Barcode with ID {id} not found");
                }

                return Ok(barcode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BarcodeList/CheckBarcodeExists/{barcode}/{productId}/{companyId}
        [HttpGet("CheckBarcodeExists/{barcode}/{productId}/{companyId}")]
        public ActionResult CheckBarcodeExists(string barcode, int productId, int companyId)
        {
            try
            {
                bool exists = false;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Barcode_product", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);
                    cmd.Parameters.AddWithValue("@product_id", productId);
                    cmd.Parameters.AddWithValue("@barcode", barcode);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    exists = reader.HasRows;
                }

                return Ok(new { exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/BarcodeList/AddBarcode
        [HttpPost("AddBarcode")]
        public ActionResult AddBarcode([FromBody] BarcodeModel barcode)
        {
            try
            {
                if (barcode == null)
                {
                    return BadRequest("Barcode data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Barcode_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Barcode_Size_id", 0); // New barcode
                    cmd.Parameters.AddWithValue("@cmp_id", barcode.CompanyId);
                    cmd.Parameters.AddWithValue("@Size_Id", barcode.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Barcode", barcode.Barcode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@product_id", barcode.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", barcode.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Barcode added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/BarcodeList/UpdateBarcode/{id}
        [HttpPut("UpdateBarcode/{id}")]
        public ActionResult UpdateBarcode(int id, [FromBody] BarcodeModel barcode)
        {
            try
            {
                if (barcode == null)
                {
                    return BadRequest("Barcode data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Barcode_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Barcode_Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", barcode.CompanyId);
                    cmd.Parameters.AddWithValue("@Size_Id", barcode.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Barcode", barcode.Barcode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@product_id", barcode.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", barcode.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Barcode with ID {id} not found");
                    }
                }

                return Ok("Barcode updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/BarcodeList/DeleteBarcode/{id}
        [HttpDelete("DeleteBarcode/{id}")]
        public ActionResult DeleteBarcode(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Barcode_Size", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Barcode_Size_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@Size_Id", 0);
                    cmd.Parameters.AddWithValue("@Barcode", "");
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@product_id", 0);
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Barcode with ID {id} not found");
                    }
                }

                return Ok("Barcode deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 