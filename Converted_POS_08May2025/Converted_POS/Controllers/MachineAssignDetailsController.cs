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
    public class MachineAssignDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MachineAssignDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/MachineAssignDetails/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<MachineAssignDetailsModel>> GetAllMachineAssignDetails()
        {
            try
            {
                var machineDetails = new List<MachineAssignDetailsModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var machineDetail = new MachineAssignDetailsModel
                        {
                            MachineDetailId = Convert.ToInt32(reader["Machine_Detail_id"]),
                            MachineId = Convert.ToInt32(reader["Machine_id"]),
                            PrinterId = reader["Printer_id"] != DBNull.Value ? Convert.ToInt32(reader["Printer_id"]) : (int?)null,
                            DeviceId = reader["Device_id"] != DBNull.Value ? Convert.ToInt32(reader["Device_id"]) : (int?)null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            DeviceName = reader["device_name"] != DBNull.Value ? reader["device_name"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        machineDetails.Add(machineDetail);
                    }
                }

                return Ok(machineDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MachineAssignDetails/GetByMachineId/{machineId}
        [HttpGet("GetByMachineId/{machineId}")]
        public ActionResult<IEnumerable<MachineAssignDetailsModel>> GetMachineAssignDetailsByMachineId(int machineId)
        {
            try
            {
                var machineDetails = new List<MachineAssignDetailsModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Machine_id", machineId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var machineDetail = new MachineAssignDetailsModel
                        {
                            MachineDetailId = Convert.ToInt32(reader["Machine_Detail_id"]),
                            MachineId = Convert.ToInt32(reader["Machine_id"]),
                            PrinterId = reader["Printer_id"] != DBNull.Value ? Convert.ToInt32(reader["Printer_id"]) : (int?)null,
                            DeviceId = reader["Device_id"] != DBNull.Value ? Convert.ToInt32(reader["Device_id"]) : (int?)null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            DeviceName = reader["device_name"] != DBNull.Value ? reader["device_name"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        machineDetails.Add(machineDetail);
                    }
                }

                return Ok(machineDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MachineAssignDetails/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<MachineAssignDetailsModel> GetMachineAssignDetailsById(int id)
        {
            try
            {
                MachineAssignDetailsModel machineDetail = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Machine_Detail_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        machineDetail = new MachineAssignDetailsModel
                        {
                            MachineDetailId = Convert.ToInt32(reader["Machine_Detail_id"]),
                            MachineId = Convert.ToInt32(reader["Machine_id"]),
                            PrinterId = reader["Printer_id"] != DBNull.Value ? Convert.ToInt32(reader["Printer_id"]) : (int?)null,
                            DeviceId = reader["Device_id"] != DBNull.Value ? Convert.ToInt32(reader["Device_id"]) : (int?)null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            DeviceName = reader["device_name"] != DBNull.Value ? reader["device_name"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (machineDetail == null)
                {
                    return NotFound($"Machine assign detail with ID {id} not found");
                }

                return Ok(machineDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MachineAssignDetails/Add
        [HttpPost("Add")]
        public ActionResult AddMachineAssignDetail([FromBody] MachineAssignDetailsModel machineDetail)
        {
            try
            {
                if (machineDetail == null)
                {
                    return BadRequest("Machine assign detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Machine_Detail_id", 0);
                    cmd.Parameters.AddWithValue("@Machine_id", machineDetail.MachineId);
                    cmd.Parameters.AddWithValue("@Printer_id", machineDetail.PrinterId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Device_id", machineDetail.DeviceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", machineDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Machine assign detail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MachineAssignDetails/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateMachineAssignDetail(int id, [FromBody] MachineAssignDetailsModel machineDetail)
        {
            try
            {
                if (machineDetail == null)
                {
                    return BadRequest("Machine assign detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Machine_Detail_id", id);
                    cmd.Parameters.AddWithValue("@Machine_id", machineDetail.MachineId);
                    cmd.Parameters.AddWithValue("@Printer_id", machineDetail.PrinterId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Device_id", machineDetail.DeviceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", machineDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Machine assign detail with ID {id} not found");
                    }
                }

                return Ok("Machine assign detail updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MachineAssignDetails/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteMachineAssignDetail(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the record exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Machine_Details", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@Machine_Detail_id", id);
                    
                    connection.Open();
                    
                    MachineAssignDetailsModel machineDetail = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            machineDetail = new MachineAssignDetailsModel
                            {
                                MachineDetailId = id,
                                MachineId = Convert.ToInt32(reader["Machine_id"]),
                                PrinterId = reader["Printer_id"] != DBNull.Value ? Convert.ToInt32(reader["Printer_id"]) : (int?)null,
                                DeviceId = reader["Device_id"] != DBNull.Value ? Convert.ToInt32(reader["Device_id"]) : (int?)null,
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0
                            };
                        }
                    }
                    
                    if (machineDetail == null)
                    {
                        return NotFound($"Machine assign detail with ID {id} not found");
                    }

                    // Now delete the record
                    SqlCommand cmd = new SqlCommand("P_M_Machine_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Machine_Detail_id", id);
                    cmd.Parameters.AddWithValue("@Machine_id", machineDetail.MachineId);
                    cmd.Parameters.AddWithValue("@Printer_id", machineDetail.PrinterId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Device_id", machineDetail.DeviceId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", machineDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Machine assign detail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 