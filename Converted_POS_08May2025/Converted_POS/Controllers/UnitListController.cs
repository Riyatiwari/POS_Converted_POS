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
    public class UnitListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UnitListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/UnitList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UnitListModel>> GetAllUnits()
        {
            try
            {
                var units = new List<UnitListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var unit = new UnitListModel
                        {
                            UnitId = Convert.ToInt32(reader["Unit_id"]),
                            UnitName = reader["Unit"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        units.Add(unit);
                    }
                }

                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/UnitList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<UnitListModel>> GetUnitsByCompanyId(int companyId)
        {
            try
            {
                var units = new List<UnitListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var unit = new UnitListModel
                        {
                            UnitId = Convert.ToInt32(reader["Unit_id"]),
                            UnitName = reader["Unit"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        units.Add(unit);
                    }
                }

                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/UnitList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<UnitListModel> GetUnitById(int id)
        {
            try
            {
                UnitListModel unit = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Unit_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        unit = new UnitListModel
                        {
                            UnitId = Convert.ToInt32(reader["Unit_id"]),
                            UnitName = reader["Unit"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (unit == null)
                {
                    return NotFound($"Unit with ID {id} not found");
                }

                return Ok(unit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/UnitList/Add
        [HttpPost("Add")]
        public ActionResult AddUnit([FromBody] UnitListModel unit)
        {
            try
            {
                if (unit == null)
                {
                    return BadRequest("Unit data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Unit_id", 0);
                    cmd.Parameters.AddWithValue("@Unit", unit.UnitName);
                    cmd.Parameters.AddWithValue("@cmp_id", unit.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", unit.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Unit added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/UnitList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateUnit(int id, [FromBody] UnitListModel unit)
        {
            try
            {
                if (unit == null)
                {
                    return BadRequest("Unit data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Unit_id", id);
                    cmd.Parameters.AddWithValue("@Unit", unit.UnitName);
                    cmd.Parameters.AddWithValue("@cmp_id", unit.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", unit.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Unit with ID {id} not found");
                    }
                }

                return Ok("Unit updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/UnitList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteUnit(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the unit exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Unit", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@Unit_id", id);
                    
                    connection.Open();
                    
                    UnitListModel unit = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            unit = new UnitListModel
                            {
                                UnitId = id,
                                UnitName = reader["Unit"].ToString(),
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                                IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                            };
                        }
                    }
                    
                    if (unit == null)
                    {
                        return NotFound($"Unit with ID {id} not found");
                    }

                    // Now delete the unit
                    SqlCommand cmd = new SqlCommand("P_M_Unit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Unit_id", id);
                    cmd.Parameters.AddWithValue("@Unit", unit.UnitName);
                    cmd.Parameters.AddWithValue("@cmp_id", unit.CompanyId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@is_active", unit.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Unit deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 