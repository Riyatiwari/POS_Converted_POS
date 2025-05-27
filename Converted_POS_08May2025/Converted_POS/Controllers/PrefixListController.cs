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
    public class PrefixListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PrefixListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/PrefixList/GetPrefixes/{companyId}
        [HttpGet("GetPrefixes/{companyId}")]
        public ActionResult<IEnumerable<PrefixModel>> GetPrefixes(int companyId)
        {
            try
            {
                var prefixes = new List<PrefixModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Prefix", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var prefix = new PrefixModel
                        {
                            PrefixId = Convert.ToInt32(reader["Prefix_id"]),
                            CompanyId = companyId,
                            Prefix = reader["Prefix"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        prefixes.Add(prefix);
                    }
                }

                return Ok(prefixes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PrefixList/GetPrefixById/{id}/{companyId}
        [HttpGet("GetPrefixById/{id}/{companyId}")]
        public ActionResult<PrefixModel> GetPrefixById(int id, int companyId)
        {
            try
            {
                PrefixModel prefix = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Prefix", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);
                    cmd.Parameters.AddWithValue("@Prefix_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        prefix = new PrefixModel
                        {
                            PrefixId = id,
                            CompanyId = companyId,
                            Prefix = reader["Prefix"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                    }
                }

                if (prefix == null)
                {
                    return NotFound($"Prefix with ID {id} not found");
                }

                return Ok(prefix);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PrefixList/AddPrefix
        [HttpPost("AddPrefix")]
        public ActionResult AddPrefix([FromBody] PrefixModel prefix)
        {
            try
            {
                if (prefix == null)
                {
                    return BadRequest("Prefix data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Prefix", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Prefix_id", 0); // New prefix
                    cmd.Parameters.AddWithValue("@Prefix", prefix.Prefix ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", prefix.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", prefix.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Prefix added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PrefixList/UpdatePrefix/{id}
        [HttpPut("UpdatePrefix/{id}")]
        public ActionResult UpdatePrefix(int id, [FromBody] PrefixModel prefix)
        {
            try
            {
                if (prefix == null)
                {
                    return BadRequest("Prefix data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Prefix", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Prefix_id", id);
                    cmd.Parameters.AddWithValue("@Prefix", prefix.Prefix ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", prefix.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", prefix.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Prefix with ID {id} not found");
                    }
                }

                return Ok("Prefix updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PrefixList/DeletePrefix/{id}
        [HttpDelete("DeletePrefix/{id}")]
        public ActionResult DeletePrefix(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Prefix", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Prefix_id", id);
                    cmd.Parameters.AddWithValue("@Prefix", "");
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Prefix with ID {id} not found");
                    }
                }

                return Ok("Prefix deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 