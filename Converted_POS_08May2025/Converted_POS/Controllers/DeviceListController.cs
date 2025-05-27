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
    public class DeviceListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DeviceListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/DeviceList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DeviceListModel>> GetAllDevices()
        {
            try
            {
                var devices = new List<DeviceListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Devices", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var device = new DeviceListModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            SerialNumber = reader["serial_number"] != DBNull.Value ? reader["serial_number"].ToString() : null,
                            Model = reader["model"] != DBNull.Value ? reader["model"].ToString() : null,
                            DeviceType = reader["device_type"] != DBNull.Value ? reader["device_type"].ToString() : null,
                            Status = reader["status"] != DBNull.Value ? reader["status"].ToString() : null,
                            PaymentProviderId = reader["payment_provider_id"] != DBNull.Value ? Convert.ToInt32(reader["payment_provider_id"]) : (int?)null,
                            PaymentProviderName = reader["payment_provider_name"] != DBNull.Value ? reader["payment_provider_name"].ToString() : null,
                            ApiKey = reader["api_key"] != DBNull.Value ? reader["api_key"].ToString() : null,
                            ApiSecret = reader["api_secret"] != DBNull.Value ? reader["api_secret"].ToString() : null,
                            MerchantId = reader["merchant_id"] != DBNull.Value ? reader["merchant_id"].ToString() : null,
                            TerminalId = reader["terminal_id"] != DBNull.Value ? reader["terminal_id"].ToString() : null,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                        devices.Add(device);
                    }
                }

                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DeviceList/GetByLocationId/{locationId}
        [HttpGet("GetByLocationId/{locationId}")]
        public ActionResult<IEnumerable<DeviceListModel>> GetDevicesByLocationId(int locationId)
        {
            try
            {
                var devices = new List<DeviceListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Devices_By_Location_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var device = new DeviceListModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            SerialNumber = reader["serial_number"] != DBNull.Value ? reader["serial_number"].ToString() : null,
                            Model = reader["model"] != DBNull.Value ? reader["model"].ToString() : null,
                            DeviceType = reader["device_type"] != DBNull.Value ? reader["device_type"].ToString() : null,
                            Status = reader["status"] != DBNull.Value ? reader["status"].ToString() : null,
                            PaymentProviderId = reader["payment_provider_id"] != DBNull.Value ? Convert.ToInt32(reader["payment_provider_id"]) : (int?)null,
                            PaymentProviderName = reader["payment_provider_name"] != DBNull.Value ? reader["payment_provider_name"].ToString() : null,
                            ApiKey = reader["api_key"] != DBNull.Value ? reader["api_key"].ToString() : null,
                            ApiSecret = reader["api_secret"] != DBNull.Value ? reader["api_secret"].ToString() : null,
                            MerchantId = reader["merchant_id"] != DBNull.Value ? reader["merchant_id"].ToString() : null,
                            TerminalId = reader["terminal_id"] != DBNull.Value ? reader["terminal_id"].ToString() : null,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                        devices.Add(device);
                    }
                }

                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DeviceList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<DeviceListModel> GetDeviceById(int id)
        {
            try
            {
                DeviceListModel device = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Device_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        device = new DeviceListModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            SerialNumber = reader["serial_number"] != DBNull.Value ? reader["serial_number"].ToString() : null,
                            Model = reader["model"] != DBNull.Value ? reader["model"].ToString() : null,
                            DeviceType = reader["device_type"] != DBNull.Value ? reader["device_type"].ToString() : null,
                            Status = reader["status"] != DBNull.Value ? reader["status"].ToString() : null,
                            PaymentProviderId = reader["payment_provider_id"] != DBNull.Value ? Convert.ToInt32(reader["payment_provider_id"]) : (int?)null,
                            PaymentProviderName = reader["payment_provider_name"] != DBNull.Value ? reader["payment_provider_name"].ToString() : null,
                            ApiKey = reader["api_key"] != DBNull.Value ? reader["api_key"].ToString() : null,
                            ApiSecret = reader["api_secret"] != DBNull.Value ? reader["api_secret"].ToString() : null,
                            MerchantId = reader["merchant_id"] != DBNull.Value ? reader["merchant_id"].ToString() : null,
                            TerminalId = reader["terminal_id"] != DBNull.Value ? reader["terminal_id"].ToString() : null,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                    }
                }

                if (device == null)
                {
                    return NotFound($"Device with ID {id} not found");
                }

                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/DeviceList/Add
        [HttpPost("Add")]
        public ActionResult AddDevice([FromBody] DeviceListModel device)
        {
            try
            {
                if (device == null)
                {
                    return BadRequest("Device data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Device", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@company_id", device.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", device.LocationId);
                    cmd.Parameters.AddWithValue("@name", device.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@serial_number", device.SerialNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@model", device.Model ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@device_type", device.DeviceType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", device.Status ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@payment_provider_id", device.PaymentProviderId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@api_key", device.ApiKey ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@api_secret", device.ApiSecret ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@merchant_id", device.MerchantId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@terminal_id", device.TerminalId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", device.IsActive);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Device added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/DeviceList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateDevice(int id, [FromBody] DeviceListModel device)
        {
            try
            {
                if (device == null)
                {
                    return BadRequest("Device data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Device", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@company_id", device.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", device.LocationId);
                    cmd.Parameters.AddWithValue("@name", device.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@serial_number", device.SerialNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@model", device.Model ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@device_type", device.DeviceType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", device.Status ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@payment_provider_id", device.PaymentProviderId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@api_key", device.ApiKey ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@api_secret", device.ApiSecret ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@merchant_id", device.MerchantId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@terminal_id", device.TerminalId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", device.IsActive);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Device with ID {id} not found");
                    }
                }

                return Ok("Device updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/DeviceList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteDevice(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Device", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Device with ID {id} not found");
                    }
                }

                return Ok("Device deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 