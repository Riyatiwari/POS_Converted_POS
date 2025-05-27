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
    public class StaffRulesMasterListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StaffRulesMasterListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/StaffRulesMasterList/GetStaffRulesMaster/{companyId}
        [HttpGet("GetStaffRulesMaster/{companyId}")]
        public ActionResult<IEnumerable<StaffRulesMasterModel>> GetStaffRulesMaster(int companyId)
        {
            try
            {
                var staffRules = new List<StaffRulesMasterModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_staff_rules_master", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var staffRule = new StaffRulesMasterModel
                        {
                            MStaffId = Convert.ToInt32(reader["m_staff_id"]),
                            CompanyId = companyId,
                            Name = reader["name"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        staffRules.Add(staffRule);
                    }
                }

                return Ok(staffRules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/StaffRulesMasterList/GetStaffRulesMasterById/{id}
        [HttpGet("GetStaffRulesMasterById/{id}")]
        public ActionResult<StaffRulesMasterModel> GetStaffRulesMasterById(int id)
        {
            try
            {
                StaffRulesMasterModel staffRule = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_staff_rules_master_forEdit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@m_staff_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        staffRule = new StaffRulesMasterModel
                        {
                            MStaffId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Name = reader["name"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                    }
                }

                if (staffRule == null)
                {
                    return NotFound($"Staff Rule Master with ID {id} not found");
                }

                return Ok(staffRule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/StaffRulesMasterList/AddStaffRulesMaster
        [HttpPost("AddStaffRulesMaster")]
        public ActionResult AddStaffRulesMaster([FromBody] StaffRulesMasterModel staffRule)
        {
            try
            {
                if (staffRule == null)
                {
                    return BadRequest("Staff Rule Master data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff_rules", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@m_staff_id", 0); // New staff rule
                    cmd.Parameters.AddWithValue("@cmp_id", staffRule.CompanyId);
                    cmd.Parameters.AddWithValue("@name", staffRule.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", staffRule.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Staff Rule Master added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/StaffRulesMasterList/UpdateStaffRulesMaster/{id}
        [HttpPut("UpdateStaffRulesMaster/{id}")]
        public ActionResult UpdateStaffRulesMaster(int id, [FromBody] StaffRulesMasterModel staffRule)
        {
            try
            {
                if (staffRule == null)
                {
                    return BadRequest("Staff Rule Master data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff_rules", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@m_staff_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", staffRule.CompanyId);
                    cmd.Parameters.AddWithValue("@name", staffRule.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", staffRule.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Staff Rule Master with ID {id} not found");
                    }
                }

                return Ok("Staff Rule Master updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/StaffRulesMasterList/DeleteStaffRulesMaster/{id}
        [HttpDelete("DeleteStaffRulesMaster/{id}")]
        public ActionResult DeleteStaffRulesMaster(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff_rules", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@m_staff_id", id);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Staff Rule Master with ID {id} not found");
                    }
                }

                return Ok("Staff Rule Master deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 