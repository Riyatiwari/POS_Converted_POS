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
    public class ShiftListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ShiftListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/ShiftList/GetShifts/{companyId}
        [HttpGet("GetShifts/{companyId}")]
        public ActionResult<IEnumerable<ShiftModel>> GetShifts(int companyId)
        {
            try
            {
                var shifts = new List<ShiftModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_TillShifts", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var shift = new ShiftModel
                        {
                            TillShiftId = Convert.ToInt32(reader["tillshift_id"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            ShiftName = reader["shift_name"].ToString(),
                            Active = reader["active"] != DBNull.Value && Convert.ToBoolean(reader["active"]),
                            CompanyId = companyId,
                            ShiftNo = reader["shift_no"] != DBNull.Value ? Convert.ToInt32(reader["shift_no"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null
                        };
                        shifts.Add(shift);
                    }
                }

                return Ok(shifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ShiftList/GetShiftById/{id}
        [HttpGet("GetShiftById/{id}")]
        public ActionResult<ShiftModel> GetShiftById(int id)
        {
            try
            {
                ShiftModel shift = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_TillShift_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tillshift_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        shift = new ShiftModel
                        {
                            TillShiftId = id,
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            ShiftName = reader["shift_name"].ToString(),
                            Active = reader["active"] != DBNull.Value && Convert.ToBoolean(reader["active"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            ShiftNo = reader["shift_no"] != DBNull.Value ? Convert.ToInt32(reader["shift_no"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null
                        };
                    }
                }

                if (shift == null)
                {
                    return NotFound($"Shift with ID {id} not found");
                }

                return Ok(shift);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ShiftList/GetShiftsByMachine/{machineId}
        [HttpGet("GetShiftsByMachine/{machineId}")]
        public ActionResult<IEnumerable<ShiftModel>> GetShiftsByMachine(int machineId)
        {
            try
            {
                var shifts = new List<ShiftModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_TillShifts_By_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@machine_id", machineId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var shift = new ShiftModel
                        {
                            TillShiftId = Convert.ToInt32(reader["tillshift_id"]),
                            MachineId = machineId,
                            ShiftName = reader["shift_name"].ToString(),
                            Active = reader["active"] != DBNull.Value && Convert.ToBoolean(reader["active"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            ShiftNo = reader["shift_no"] != DBNull.Value ? Convert.ToInt32(reader["shift_no"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null
                        };
                        shifts.Add(shift);
                    }
                }

                return Ok(shifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ShiftList/AddShift
        [HttpPost("AddShift")]
        public ActionResult AddShift([FromBody] ShiftModel shift)
        {
            try
            {
                if (shift == null)
                {
                    return BadRequest("Shift data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_TillShift", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tillshift_id", 0); // New shift
                    cmd.Parameters.AddWithValue("@cmp_id", shift.CompanyId);
                    cmd.Parameters.AddWithValue("@machine_id", shift.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@shift_name", shift.ShiftName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@active", shift.Active);
                    cmd.Parameters.AddWithValue("@shift_no", shift.ShiftNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", shift.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Shift added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ShiftList/UpdateShift/{id}
        [HttpPut("UpdateShift/{id}")]
        public ActionResult UpdateShift(int id, [FromBody] ShiftModel shift)
        {
            try
            {
                if (shift == null)
                {
                    return BadRequest("Shift data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_TillShift", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tillshift_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", shift.CompanyId);
                    cmd.Parameters.AddWithValue("@machine_id", shift.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@shift_name", shift.ShiftName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@active", shift.Active);
                    cmd.Parameters.AddWithValue("@shift_no", shift.ShiftNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", shift.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Shift with ID {id} not found");
                    }
                }

                return Ok("Shift updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ShiftList/DeleteShift/{id}
        [HttpDelete("DeleteShift/{id}")]
        public ActionResult DeleteShift(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_TillShift", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tillshift_id", id);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Shift with ID {id} not found");
                    }
                }

                return Ok("Shift deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 