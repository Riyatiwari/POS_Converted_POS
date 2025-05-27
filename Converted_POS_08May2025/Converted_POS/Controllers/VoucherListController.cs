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
    public class VoucherListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public VoucherListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/VoucherList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<VoucherListModel>> GetAllVouchers()
        {
            try
            {
                var vouchers = new List<VoucherListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var voucher = new VoucherListModel
                        {
                            VoucherId = Convert.ToInt32(reader["voucher_id"]),
                            VoucherName = reader["voucher_name"].ToString(),
                            VoucherType = reader["voucher_type"].ToString(),
                            StartDate = Convert.ToDateTime(reader["start_date"]),
                            EndDate = Convert.ToDateTime(reader["end_date"]),
                            IsEndless = reader["endless"] != DBNull.Value && Convert.ToBoolean(reader["endless"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            VoucherDuration = reader["voucher_duration"] != DBNull.Value ? reader["voucher_duration"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        vouchers.Add(voucher);
                    }
                }

                return Ok(vouchers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VoucherList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<VoucherListModel>> GetVouchersByCompanyId(int companyId)
        {
            try
            {
                var vouchers = new List<VoucherListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var voucher = new VoucherListModel
                        {
                            VoucherId = Convert.ToInt32(reader["voucher_id"]),
                            VoucherName = reader["voucher_name"].ToString(),
                            VoucherType = reader["voucher_type"].ToString(),
                            StartDate = Convert.ToDateTime(reader["start_date"]),
                            EndDate = Convert.ToDateTime(reader["end_date"]),
                            IsEndless = reader["endless"] != DBNull.Value && Convert.ToBoolean(reader["endless"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            VoucherDuration = reader["voucher_duration"] != DBNull.Value ? reader["voucher_duration"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        vouchers.Add(voucher);
                    }
                }

                return Ok(vouchers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VoucherList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<VoucherListModel> GetVoucherById(int id)
        {
            try
            {
                VoucherListModel voucher = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@voucher_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        voucher = new VoucherListModel
                        {
                            VoucherId = Convert.ToInt32(reader["voucher_id"]),
                            VoucherName = reader["voucher_name"].ToString(),
                            VoucherType = reader["voucher_type"].ToString(),
                            StartDate = Convert.ToDateTime(reader["start_date"]),
                            EndDate = Convert.ToDateTime(reader["end_date"]),
                            IsEndless = reader["endless"] != DBNull.Value && Convert.ToBoolean(reader["endless"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            VoucherDuration = reader["voucher_duration"] != DBNull.Value ? reader["voucher_duration"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (voucher == null)
                {
                    return NotFound($"Voucher with ID {id} not found");
                }

                return Ok(voucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VoucherList/Add
        [HttpPost("Add")]
        public ActionResult AddVoucher([FromBody] VoucherListModel voucher)
        {
            try
            {
                if (voucher == null)
                {
                    return BadRequest("Voucher data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@voucher_id", 0);
                    cmd.Parameters.AddWithValue("@voucher_name", voucher.VoucherName);
                    cmd.Parameters.AddWithValue("@voucher_type", voucher.VoucherType);
                    cmd.Parameters.AddWithValue("@start_date", voucher.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", voucher.EndDate);
                    cmd.Parameters.AddWithValue("@endless", voucher.IsEndless ? 1 : 0);
                    cmd.Parameters.AddWithValue("@cmp_id", voucher.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@voucher_duration", voucher.VoucherDuration ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Voucher added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/VoucherList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateVoucher(int id, [FromBody] VoucherListModel voucher)
        {
            try
            {
                if (voucher == null)
                {
                    return BadRequest("Voucher data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@voucher_id", id);
                    cmd.Parameters.AddWithValue("@voucher_name", voucher.VoucherName);
                    cmd.Parameters.AddWithValue("@voucher_type", voucher.VoucherType);
                    cmd.Parameters.AddWithValue("@start_date", voucher.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", voucher.EndDate);
                    cmd.Parameters.AddWithValue("@endless", voucher.IsEndless ? 1 : 0);
                    cmd.Parameters.AddWithValue("@cmp_id", voucher.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@voucher_duration", voucher.VoucherDuration ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Voucher with ID {id} not found");
                    }
                }

                return Ok("Voucher updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/VoucherList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteVoucher(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the voucher exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Voucher", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@voucher_id", id);
                    
                    connection.Open();
                    
                    VoucherListModel voucher = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            voucher = new VoucherListModel
                            {
                                VoucherId = id,
                                VoucherName = reader["voucher_name"].ToString(),
                                VoucherType = reader["voucher_type"].ToString(),
                                StartDate = Convert.ToDateTime(reader["start_date"]),
                                EndDate = Convert.ToDateTime(reader["end_date"]),
                                IsEndless = reader["endless"] != DBNull.Value && Convert.ToBoolean(reader["endless"]),
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                                VoucherDuration = reader["voucher_duration"] != DBNull.Value ? reader["voucher_duration"].ToString() : null
                            };
                        }
                    }
                    
                    if (voucher == null)
                    {
                        return NotFound($"Voucher with ID {id} not found");
                    }

                    // Now delete the voucher
                    SqlCommand cmd = new SqlCommand("P_M_Voucher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@voucher_id", id);
                    cmd.Parameters.AddWithValue("@voucher_name", voucher.VoucherName);
                    cmd.Parameters.AddWithValue("@voucher_type", voucher.VoucherType);
                    cmd.Parameters.AddWithValue("@start_date", voucher.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", voucher.EndDate);
                    cmd.Parameters.AddWithValue("@endless", voucher.IsEndless ? 1 : 0);
                    cmd.Parameters.AddWithValue("@cmp_id", voucher.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@voucher_duration", voucher.VoucherDuration ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Voucher deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 