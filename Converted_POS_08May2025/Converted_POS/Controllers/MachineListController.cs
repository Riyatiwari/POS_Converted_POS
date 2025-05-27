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
    public class MachineListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MachineListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/MachineList/GetMachines/{companyId}
        [HttpGet("GetMachines/{companyId}")]
        public ActionResult<IEnumerable<MachineModel>> GetMachines(int companyId)
        {
            try
            {
                var machines = new List<MachineModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var machine = new MachineModel
                        {
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            CompanyId = companyId,
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            MacAddress = reader["mac_address"].ToString(),
                            Model = reader["model"].ToString(),
                            SerialNo = reader["serial_no"].ToString(),
                            IpAddress = reader["ip_address"].ToString(),
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : (int?)null,
                            IsAssign = reader["is_assign"] != DBNull.Value && Convert.ToBoolean(reader["is_assign"]),
                            IsMiniPos = reader["is_minipos"] != DBNull.Value && Convert.ToBoolean(reader["is_minipos"]),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            ReceiptHeader = reader["Receipt_Header"].ToString(),
                            IsMasterSelf = reader["is_master_self"] != DBNull.Value && Convert.ToBoolean(reader["is_master_self"]),
                            ReceiptFooter = reader["Receipt_Footer"].ToString(),
                            KeymapId = reader["keymap_id"] != DBNull.Value ? Convert.ToInt32(reader["keymap_id"]) : (int?)null,
                            FunctionId = reader["function_id"] != DBNull.Value ? Convert.ToInt32(reader["function_id"]) : (int?)null,
                            TillId = reader["till_id"] != DBNull.Value ? Convert.ToInt32(reader["till_id"]) : (int?)null,
                            TillIpAddress = reader["tillip_address"].ToString(),
                            MasterTill = reader["master_till"] != DBNull.Value ? Convert.ToInt32(reader["master_till"]) : (int?)null,
                            TillServer = reader["till_server"] != DBNull.Value && Convert.ToBoolean(reader["till_server"]),
                            TableSharing = reader["table_sharing"] != DBNull.Value && Convert.ToBoolean(reader["table_sharing"]),
                            PrinterSharing = reader["printer_sharing"] != DBNull.Value && Convert.ToBoolean(reader["printer_sharing"]),
                            SyncTime = reader["sync_time"].ToString(),
                            IsKiosk = reader["IsKiosk"] != DBNull.Value && Convert.ToBoolean(reader["IsKiosk"]),
                            POSLite = reader["posLite"] != DBNull.Value && Convert.ToBoolean(reader["posLite"])
                        };
                        machines.Add(machine);
                    }
                }

                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MachineList/GetMachineById/{id}
        [HttpGet("GetMachineById/{id}")]
        public ActionResult<MachineModel> GetMachineById(int id)
        {
            try
            {
                MachineModel machine = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@machine_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        machine = new MachineModel
                        {
                            MachineId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            MacAddress = reader["mac_address"].ToString(),
                            Model = reader["model"].ToString(),
                            SerialNo = reader["serial_no"].ToString(),
                            IpAddress = reader["ip_address"].ToString(),
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : (int?)null,
                            IsAssign = reader["is_assign"] != DBNull.Value && Convert.ToBoolean(reader["is_assign"]),
                            IsMiniPos = reader["is_minipos"] != DBNull.Value && Convert.ToBoolean(reader["is_minipos"]),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            ReceiptHeader = reader["Receipt_Header"].ToString(),
                            IsMasterSelf = reader["is_master_self"] != DBNull.Value && Convert.ToBoolean(reader["is_master_self"]),
                            ReceiptFooter = reader["Receipt_Footer"].ToString(),
                            KeymapId = reader["keymap_id"] != DBNull.Value ? Convert.ToInt32(reader["keymap_id"]) : (int?)null,
                            FunctionId = reader["function_id"] != DBNull.Value ? Convert.ToInt32(reader["function_id"]) : (int?)null,
                            TillId = reader["till_id"] != DBNull.Value ? Convert.ToInt32(reader["till_id"]) : (int?)null,
                            TillIpAddress = reader["tillip_address"].ToString(),
                            MasterTill = reader["master_till"] != DBNull.Value ? Convert.ToInt32(reader["master_till"]) : (int?)null,
                            TillServer = reader["till_server"] != DBNull.Value && Convert.ToBoolean(reader["till_server"]),
                            TableSharing = reader["table_sharing"] != DBNull.Value && Convert.ToBoolean(reader["table_sharing"]),
                            PrinterSharing = reader["printer_sharing"] != DBNull.Value && Convert.ToBoolean(reader["printer_sharing"]),
                            SyncTime = reader["sync_time"].ToString(),
                            IsKiosk = reader["IsKiosk"] != DBNull.Value && Convert.ToBoolean(reader["IsKiosk"]),
                            POSLite = reader["posLite"] != DBNull.Value && Convert.ToBoolean(reader["posLite"])
                        };
                    }
                }

                if (machine == null)
                {
                    return NotFound($"Machine with ID {id} not found");
                }

                return Ok(machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MachineList/GetMachinesByLocation/{locationId}
        [HttpGet("GetMachinesByLocation/{locationId}")]
        public ActionResult<IEnumerable<MachineModel>> GetMachinesByLocation(int locationId)
        {
            try
            {
                var machines = new List<MachineModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Machine_By_Location", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var machine = new MachineModel
                        {
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            LocationId = locationId,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        machines.Add(machine);
                    }
                }

                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MachineList/AddMachine
        [HttpPost("AddMachine")]
        public ActionResult AddMachine([FromBody] MachineModel machine)
        {
            try
            {
                if (machine == null)
                {
                    return BadRequest("Machine data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@machine_id", 0); // New machine
                    cmd.Parameters.AddWithValue("@cmp_id", machine.CompanyId);
                    cmd.Parameters.AddWithValue("@code", machine.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", machine.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mac_address", machine.MacAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@model", machine.Model ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@serial_no", machine.SerialNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@location_id", machine.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_assign", machine.IsAssign);
                    cmd.Parameters.AddWithValue("@is_minipos", machine.IsMiniPos);
                    cmd.Parameters.AddWithValue("@is_active", machine.IsActive);
                    cmd.Parameters.AddWithValue("@Receipt_Header", machine.ReceiptHeader ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_master_self", machine.IsMasterSelf);
                    cmd.Parameters.AddWithValue("@Receipt_Footer", machine.ReceiptFooter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@keymap_id", machine.KeymapId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@function_id", machine.FunctionId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_id", machine.TillId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tillip_address", machine.TillIpAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@master_till", machine.MasterTill ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_server", machine.TillServer);
                    cmd.Parameters.AddWithValue("@table_sharing", machine.TableSharing);
                    cmd.Parameters.AddWithValue("@printer_sharing", machine.PrinterSharing);
                    cmd.Parameters.AddWithValue("@sync_time", machine.SyncTime ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsKiosk", machine.IsKiosk);
                    cmd.Parameters.AddWithValue("@posLite", machine.POSLite);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Machine added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MachineList/UpdateMachine/{id}
        [HttpPut("UpdateMachine/{id}")]
        public ActionResult UpdateMachine(int id, [FromBody] MachineModel machine)
        {
            try
            {
                if (machine == null)
                {
                    return BadRequest("Machine data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@machine_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", machine.CompanyId);
                    cmd.Parameters.AddWithValue("@code", machine.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", machine.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mac_address", machine.MacAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@model", machine.Model ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@serial_no", machine.SerialNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@location_id", machine.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_assign", machine.IsAssign);
                    cmd.Parameters.AddWithValue("@is_minipos", machine.IsMiniPos);
                    cmd.Parameters.AddWithValue("@is_active", machine.IsActive);
                    cmd.Parameters.AddWithValue("@Receipt_Header", machine.ReceiptHeader ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_master_self", machine.IsMasterSelf);
                    cmd.Parameters.AddWithValue("@Receipt_Footer", machine.ReceiptFooter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@keymap_id", machine.KeymapId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@function_id", machine.FunctionId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_id", machine.TillId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tillip_address", machine.TillIpAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@master_till", machine.MasterTill ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_server", machine.TillServer);
                    cmd.Parameters.AddWithValue("@table_sharing", machine.TableSharing);
                    cmd.Parameters.AddWithValue("@printer_sharing", machine.PrinterSharing);
                    cmd.Parameters.AddWithValue("@sync_time", machine.SyncTime ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsKiosk", machine.IsKiosk);
                    cmd.Parameters.AddWithValue("@posLite", machine.POSLite);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Machine with ID {id} not found");
                    }
                }

                return Ok("Machine updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MachineList/DeleteMachine/{id}
        [HttpDelete("DeleteMachine/{id}")]
        public ActionResult DeleteMachine(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@machine_id", id);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Machine with ID {id} not found");
                    }
                }

                return Ok("Machine deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 