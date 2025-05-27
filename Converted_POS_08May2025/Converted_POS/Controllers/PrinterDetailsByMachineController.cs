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
    public class PrinterDetailsByMachineController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PrinterDetailsByMachineController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/PrinterDetailsByMachine/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PrinterDetailsByMachineModel>> GetAllPrinterDetailsByMachine()
        {
            try
            {
                var printerDetails = new List<PrinterDetailsByMachineModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Printer_Details_By_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var printerDetail = new PrinterDetailsByMachineModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            PrinterId = Convert.ToInt32(reader["printer_id"]),
                            IsReceipt = Convert.ToBoolean(reader["is_receipt"]),
                            IsKitchen = Convert.ToBoolean(reader["is_kitchen"]),
                            IsBar = Convert.ToBoolean(reader["is_bar"]),
                            IsLabel = Convert.ToBoolean(reader["is_label"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                        printerDetails.Add(printerDetail);
                    }
                }

                return Ok(printerDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PrinterDetailsByMachine/GetByMachineId/{machineId}
        [HttpGet("GetByMachineId/{machineId}")]
        public ActionResult<IEnumerable<PrinterDetailsByMachineModel>> GetPrinterDetailsByMachineId(int machineId)
        {
            try
            {
                var printerDetails = new List<PrinterDetailsByMachineModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Printer_Details_By_Machine_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@machine_id", machineId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var printerDetail = new PrinterDetailsByMachineModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            PrinterId = Convert.ToInt32(reader["printer_id"]),
                            IsReceipt = Convert.ToBoolean(reader["is_receipt"]),
                            IsKitchen = Convert.ToBoolean(reader["is_kitchen"]),
                            IsBar = Convert.ToBoolean(reader["is_bar"]),
                            IsLabel = Convert.ToBoolean(reader["is_label"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                        printerDetails.Add(printerDetail);
                    }
                }

                return Ok(printerDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PrinterDetailsByMachine/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<PrinterDetailsByMachineModel> GetPrinterDetailsByMachineById(int id)
        {
            try
            {
                PrinterDetailsByMachineModel printerDetail = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Printer_Detail_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        printerDetail = new PrinterDetailsByMachineModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            PrinterId = Convert.ToInt32(reader["printer_id"]),
                            IsReceipt = Convert.ToBoolean(reader["is_receipt"]),
                            IsKitchen = Convert.ToBoolean(reader["is_kitchen"]),
                            IsBar = Convert.ToBoolean(reader["is_bar"]),
                            IsLabel = Convert.ToBoolean(reader["is_label"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            MachineName = reader["machine_name"] != DBNull.Value ? reader["machine_name"].ToString() : null,
                            PrinterName = reader["printer_name"] != DBNull.Value ? reader["printer_name"].ToString() : null,
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                    }
                }

                if (printerDetail == null)
                {
                    return NotFound($"Printer detail with ID {id} not found");
                }

                return Ok(printerDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PrinterDetailsByMachine/Add
        [HttpPost("Add")]
        public ActionResult AddPrinterDetailsByMachine([FromBody] PrinterDetailsByMachineModel printerDetail)
        {
            try
            {
                if (printerDetail == null)
                {
                    return BadRequest("Printer detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Printer_Detail_By_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@machine_id", printerDetail.MachineId);
                    cmd.Parameters.AddWithValue("@printer_id", printerDetail.PrinterId);
                    cmd.Parameters.AddWithValue("@is_receipt", printerDetail.IsReceipt);
                    cmd.Parameters.AddWithValue("@is_kitchen", printerDetail.IsKitchen);
                    cmd.Parameters.AddWithValue("@is_bar", printerDetail.IsBar);
                    cmd.Parameters.AddWithValue("@is_label", printerDetail.IsLabel);
                    cmd.Parameters.AddWithValue("@is_default", printerDetail.IsDefault);
                    cmd.Parameters.AddWithValue("@is_active", printerDetail.IsActive);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Printer detail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PrinterDetailsByMachine/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdatePrinterDetailsByMachine(int id, [FromBody] PrinterDetailsByMachineModel printerDetail)
        {
            try
            {
                if (printerDetail == null)
                {
                    return BadRequest("Printer detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Printer_Detail_By_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@machine_id", printerDetail.MachineId);
                    cmd.Parameters.AddWithValue("@printer_id", printerDetail.PrinterId);
                    cmd.Parameters.AddWithValue("@is_receipt", printerDetail.IsReceipt);
                    cmd.Parameters.AddWithValue("@is_kitchen", printerDetail.IsKitchen);
                    cmd.Parameters.AddWithValue("@is_bar", printerDetail.IsBar);
                    cmd.Parameters.AddWithValue("@is_label", printerDetail.IsLabel);
                    cmd.Parameters.AddWithValue("@is_default", printerDetail.IsDefault);
                    cmd.Parameters.AddWithValue("@is_active", printerDetail.IsActive);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Printer detail with ID {id} not found");
                    }
                }

                return Ok("Printer detail updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PrinterDetailsByMachine/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeletePrinterDetailsByMachine(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Printer_Detail_By_Machine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Printer detail with ID {id} not found");
                    }
                }

                return Ok("Printer detail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 