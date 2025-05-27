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
    public class PrinterListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PrinterListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/PrinterList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PrinterModel>> GetAllPrinters()
        {
            try
            {
                var printers = new List<PrinterModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Printers", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var printer = new PrinterModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
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
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                        printers.Add(printer);
                    }
                }

                return Ok(printers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PrinterList/GetByLocationId/{locationId}
        [HttpGet("GetByLocationId/{locationId}")]
        public ActionResult<IEnumerable<PrinterModel>> GetPrintersByLocationId(int locationId)
        {
            try
            {
                var printers = new List<PrinterModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Printers_By_Location_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var printer = new PrinterModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
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
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                        printers.Add(printer);
                    }
                }

                return Ok(printers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/PrinterList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<PrinterModel> GetPrinterById(int id)
        {
            try
            {
                PrinterModel printer = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Printer_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        printer = new PrinterModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            PrinterPath = reader["printer_path"] != DBNull.Value ? reader["printer_path"].ToString() : null,
                            PrinterType = reader["printer_type"] != DBNull.Value ? reader["printer_type"].ToString() : null,
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
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null,
                            Port = reader["port"] != DBNull.Value ? reader["port"].ToString() : null
                        };
                    }
                }

                if (printer == null)
                {
                    return NotFound($"Printer with ID {id} not found");
                }

                return Ok(printer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PrinterList/Add
        [HttpPost("Add")]
        public ActionResult AddPrinter([FromBody] PrinterModel printer)
        {
            try
            {
                if (printer == null)
                {
                    return BadRequest("Printer data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Printer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@company_id", printer.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", printer.LocationId);
                    cmd.Parameters.AddWithValue("@name", printer.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_path", printer.PrinterPath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_type", printer.PrinterType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_receipt", printer.IsReceipt);
                    cmd.Parameters.AddWithValue("@is_kitchen", printer.IsKitchen);
                    cmd.Parameters.AddWithValue("@is_bar", printer.IsBar);
                    cmd.Parameters.AddWithValue("@is_label", printer.IsLabel);
                    cmd.Parameters.AddWithValue("@is_default", printer.IsDefault);
                    cmd.Parameters.AddWithValue("@is_active", printer.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", printer.IPAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@port", printer.Port ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@user_ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Printer added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PrinterList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdatePrinter(int id, [FromBody] PrinterModel printer)
        {
            try
            {
                if (printer == null)
                {
                    return BadRequest("Printer data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Printer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@company_id", printer.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", printer.LocationId);
                    cmd.Parameters.AddWithValue("@name", printer.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_path", printer.PrinterPath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@printer_type", printer.PrinterType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_receipt", printer.IsReceipt);
                    cmd.Parameters.AddWithValue("@is_kitchen", printer.IsKitchen);
                    cmd.Parameters.AddWithValue("@is_bar", printer.IsBar);
                    cmd.Parameters.AddWithValue("@is_label", printer.IsLabel);
                    cmd.Parameters.AddWithValue("@is_default", printer.IsDefault);
                    cmd.Parameters.AddWithValue("@is_active", printer.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", printer.IPAddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@port", printer.Port ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@user_ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Printer with ID {id} not found");
                    }
                }

                return Ok("Printer updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PrinterList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeletePrinter(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Printer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@user_ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Printer with ID {id} not found");
                    }
                }

                return Ok("Printer deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 