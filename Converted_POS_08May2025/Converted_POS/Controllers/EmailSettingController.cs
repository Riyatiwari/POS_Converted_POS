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
    public class EmailSettingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmailSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/EmailSetting/GetEmailSettings/{companyId}
        [HttpGet("GetEmailSettings/{companyId}")]
        public ActionResult<IEnumerable<EmailSettingModel>> GetEmailSettings(int companyId)
        {
            try
            {
                var emailSettings = new List<EmailSettingModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Email_Settings", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var emailSetting = new EmailSettingModel
                        {
                            TranId = Convert.ToInt32(reader["Tran_id"]),
                            CompanyId = Convert.ToInt32(reader["Company_id"]),
                            MailServer = reader["MailServer"].ToString(),
                            MailServerUsername = reader["MailServer_UserName"].ToString(),
                            MailServerPassword = reader["MailServer_Password"].ToString(),
                            Port = reader["Port"].ToString(),
                            FromEmail = reader["From_Email"].ToString(),
                            SSL = reader["ssl"] != DBNull.Value && Convert.ToBoolean(reader["ssl"]),
                            Alias = reader["Alias"].ToString(),
                            IsMES = reader["is_MES"] != DBNull.Value && Convert.ToBoolean(reader["is_MES"]),
                            MESURI = reader["MES_URI"].ToString(),
                            ReplyTo = reader["Reply_to"].ToString(),
                            SType = reader["S_Type"] != DBNull.Value ? Convert.ToByte(reader["S_Type"]) : (byte?)null,
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : (int?)null
                        };
                        emailSettings.Add(emailSetting);
                    }
                }

                return Ok(emailSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/EmailSetting/GetEmailSettingById/{id}
        [HttpGet("GetEmailSettingById/{id}")]
        public ActionResult<EmailSettingModel> GetEmailSettingById(int id)
        {
            try
            {
                EmailSettingModel emailSetting = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Email_Setting_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tran_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        emailSetting = new EmailSettingModel
                        {
                            TranId = id,
                            CompanyId = Convert.ToInt32(reader["Company_id"]),
                            MailServer = reader["MailServer"].ToString(),
                            MailServerUsername = reader["MailServer_UserName"].ToString(),
                            MailServerPassword = reader["MailServer_Password"].ToString(),
                            Port = reader["Port"].ToString(),
                            FromEmail = reader["From_Email"].ToString(),
                            SSL = reader["ssl"] != DBNull.Value && Convert.ToBoolean(reader["ssl"]),
                            Alias = reader["Alias"].ToString(),
                            IsMES = reader["is_MES"] != DBNull.Value && Convert.ToBoolean(reader["is_MES"]),
                            MESURI = reader["MES_URI"].ToString(),
                            ReplyTo = reader["Reply_to"].ToString(),
                            SType = reader["S_Type"] != DBNull.Value ? Convert.ToByte(reader["S_Type"]) : (byte?)null,
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : (int?)null
                        };
                    }
                }

                if (emailSetting == null)
                {
                    return NotFound($"Email Setting with ID {id} not found");
                }

                return Ok(emailSetting);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/EmailSetting/GetEmailSettingByLocation/{locationId}
        [HttpGet("GetEmailSettingByLocation/{locationId}")]
        public ActionResult<EmailSettingModel> GetEmailSettingByLocation(int locationId)
        {
            try
            {
                EmailSettingModel emailSetting = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Email_Setting_By_Location", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        emailSetting = new EmailSettingModel
                        {
                            TranId = Convert.ToInt32(reader["Tran_id"]),
                            CompanyId = Convert.ToInt32(reader["Company_id"]),
                            MailServer = reader["MailServer"].ToString(),
                            MailServerUsername = reader["MailServer_UserName"].ToString(),
                            MailServerPassword = reader["MailServer_Password"].ToString(),
                            Port = reader["Port"].ToString(),
                            FromEmail = reader["From_Email"].ToString(),
                            SSL = reader["ssl"] != DBNull.Value && Convert.ToBoolean(reader["ssl"]),
                            Alias = reader["Alias"].ToString(),
                            IsMES = reader["is_MES"] != DBNull.Value && Convert.ToBoolean(reader["is_MES"]),
                            MESURI = reader["MES_URI"].ToString(),
                            ReplyTo = reader["Reply_to"].ToString(),
                            SType = reader["S_Type"] != DBNull.Value ? Convert.ToByte(reader["S_Type"]) : (byte?)null,
                            LocationId = locationId
                        };
                    }
                }

                if (emailSetting == null)
                {
                    return NotFound($"Email Setting for location ID {locationId} not found");
                }

                return Ok(emailSetting);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/EmailSetting/AddEmailSetting
        [HttpPost("AddEmailSetting")]
        public ActionResult AddEmailSetting([FromBody] EmailSettingModel emailSetting)
        {
            try
            {
                if (emailSetting == null)
                {
                    return BadRequest("Email Setting data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Email_Settings", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_id", 0); // New email setting
                    cmd.Parameters.AddWithValue("@Company_id", emailSetting.CompanyId);
                    cmd.Parameters.AddWithValue("@MailServer", emailSetting.MailServer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MailServer_UserName", emailSetting.MailServerUsername ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MailServer_Password", emailSetting.MailServerPassword ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Port", emailSetting.Port ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@From_Email", emailSetting.FromEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ssl", emailSetting.SSL);
                    cmd.Parameters.AddWithValue("@Alias", emailSetting.Alias ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_MES", emailSetting.IsMES);
                    cmd.Parameters.AddWithValue("@MES_URI", emailSetting.MESURI ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Reply_to", emailSetting.ReplyTo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@S_Type", emailSetting.SType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@location_id", emailSetting.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Email Setting added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/EmailSetting/UpdateEmailSetting/{id}
        [HttpPut("UpdateEmailSetting/{id}")]
        public ActionResult UpdateEmailSetting(int id, [FromBody] EmailSettingModel emailSetting)
        {
            try
            {
                if (emailSetting == null)
                {
                    return BadRequest("Email Setting data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_Email_Settings", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_id", id);
                    cmd.Parameters.AddWithValue("@Company_id", emailSetting.CompanyId);
                    cmd.Parameters.AddWithValue("@MailServer", emailSetting.MailServer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MailServer_UserName", emailSetting.MailServerUsername ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MailServer_Password", emailSetting.MailServerPassword ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Port", emailSetting.Port ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@From_Email", emailSetting.FromEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ssl", emailSetting.SSL);
                    cmd.Parameters.AddWithValue("@Alias", emailSetting.Alias ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_MES", emailSetting.IsMES);
                    cmd.Parameters.AddWithValue("@MES_URI", emailSetting.MESURI ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Reply_to", emailSetting.ReplyTo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@S_Type", emailSetting.SType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@location_id", emailSetting.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Email Setting with ID {id} not found");
                    }
                }

                return Ok("Email Setting updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/EmailSetting/DeleteEmailSetting/{id}
        [HttpDelete("DeleteEmailSetting/{id}")]
        public ActionResult DeleteEmailSetting(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Email_Setting", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Tran_id", id);
                    cmd.Parameters.AddWithValue("@Ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Email Setting with ID {id} not found");
                    }
                }

                return Ok("Email Setting deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 