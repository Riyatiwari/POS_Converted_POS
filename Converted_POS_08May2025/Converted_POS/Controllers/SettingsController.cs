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
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/Settings/GetSettings/{locationId}
        [HttpGet("GetSettings/{locationId}")]
        public ActionResult<SettingsModel> GetSettings(int locationId)
        {
            try
            {
                SettingsModel settings = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Location_Settings", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        settings = new SettingsModel
                        {
                            LocationId = locationId,
                            TillAutoLogoff = reader["till_auto_log_off"] != DBNull.Value && Convert.ToBoolean(reader["till_auto_log_off"]),
                            Cashback = reader["cashback"] != DBNull.Value && Convert.ToBoolean(reader["cashback"]),
                            IsDelivery = reader["is_delivery"] != DBNull.Value && Convert.ToBoolean(reader["is_delivery"]),
                            MinAmount = reader["min_amount"] != DBNull.Value ? Convert.ToDecimal(reader["min_amount"]) : 0,
                            ServiceChargeClickCollect = reader["srv_charge_clickcollect"] != DBNull.Value && Convert.ToBoolean(reader["srv_charge_clickcollect"]),
                            ServiceChargeKiosk = reader["srv_charge_kiosk"] != DBNull.Value && Convert.ToBoolean(reader["srv_charge_kiosk"]),
                            ServiceChargeOrder = reader["srv_charge_order"] != DBNull.Value && Convert.ToBoolean(reader["srv_charge_order"]),
                            ServiceChargeDelivery = reader["srv_charge_delivery"] != DBNull.Value && Convert.ToBoolean(reader["srv_charge_delivery"]),
                            IsSkipPay = reader["is_skipPay"] != DBNull.Value && Convert.ToBoolean(reader["is_skipPay"]),
                            IsEmail = reader["is_email"] != DBNull.Value && Convert.ToBoolean(reader["is_email"]),
                            WebsalesCheckSchedule = reader["websales_check_schedule"] != DBNull.Value && Convert.ToBoolean(reader["websales_check_schedule"]),
                            ScheduleRequired = reader["schedule_required"] != DBNull.Value && Convert.ToBoolean(reader["schedule_required"]),
                            StartDate = reader["start_date"].ToString(),
                            EndDate = reader["end_date"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            HeaderReceipt = reader["header_reciept"].ToString(),
                            BackgroundColor = reader["BG_Color"].ToString(),
                            FontColor = reader["Font_Color"].ToString(),
                            BodyColor = reader["Body_Color"].ToString(),
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            OrderTable = reader["Order_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_table"]),
                            Theme = reader["theme"].ToString(),
                            TipAs = reader["tipAs"].ToString(),
                            DeliveryCharges = reader["delivery_charges"] != DBNull.Value ? Convert.ToDouble(reader["delivery_charges"]) : 0,
                            ServiceCharges = reader["service_charges"] != DBNull.Value ? Convert.ToDouble(reader["service_charges"]) : 0,
                            MessageDelivery = reader["message_delivery"].ToString(),
                            MessageOrderTable = reader["message_order_table"].ToString(),
                            ScheduleClickAndCollect = reader["schedule_cnc"] != DBNull.Value && Convert.ToBoolean(reader["schedule_cnc"]),
                            FutureBookingDays = reader["future_booking_days"] != DBNull.Value ? Convert.ToInt32(reader["future_booking_days"]) : 0,
                            HourlySlot = reader["HourlySlot"] != DBNull.Value && Convert.ToBoolean(reader["HourlySlot"]),
                            IsEmailAPK = reader["Is_Email_APK"] != DBNull.Value && Convert.ToBoolean(reader["Is_Email_APK"]),
                            TableAsBox = reader["Table_As_Box"].ToString(),
                            PosLite = reader["posLite"] != DBNull.Value && Convert.ToBoolean(reader["posLite"]),
                            SunmiSecondScreen = reader["sunmiSecondScreen"] != DBNull.Value && Convert.ToBoolean(reader["sunmiSecondScreen"]),
                            SunmiVideoPath = reader["sunmi_video_path"].ToString()
                        };
                    }
                }

                if (settings == null)
                {
                    return NotFound($"Settings for location ID {locationId} not found");
                }

                return Ok(settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Settings/UpdateSettings/{locationId}
        [HttpPut("UpdateSettings/{locationId}")]
        public ActionResult UpdateSettings(int locationId, [FromBody] SettingsModel settings)
        {
            try
            {
                if (settings == null)
                {
                    return BadRequest("Settings data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Location_Settings", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@location_id", locationId);
                    cmd.Parameters.AddWithValue("@till_auto_log_off", settings.TillAutoLogoff);
                    cmd.Parameters.AddWithValue("@cashback", settings.Cashback);
                    cmd.Parameters.AddWithValue("@is_delivery", settings.IsDelivery);
                    cmd.Parameters.AddWithValue("@min_amount", settings.MinAmount);
                    cmd.Parameters.AddWithValue("@srv_charge_clickcollect", settings.ServiceChargeClickCollect);
                    cmd.Parameters.AddWithValue("@srv_charge_kiosk", settings.ServiceChargeKiosk);
                    cmd.Parameters.AddWithValue("@srv_charge_order", settings.ServiceChargeOrder);
                    cmd.Parameters.AddWithValue("@srv_charge_delivery", settings.ServiceChargeDelivery);
                    cmd.Parameters.AddWithValue("@is_skipPay", settings.IsSkipPay);
                    cmd.Parameters.AddWithValue("@is_email", settings.IsEmail);
                    cmd.Parameters.AddWithValue("@websales_check_schedule", settings.WebsalesCheckSchedule);
                    cmd.Parameters.AddWithValue("@schedule_required", settings.ScheduleRequired);
                    cmd.Parameters.AddWithValue("@start_date", settings.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@end_date", settings.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", settings.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", settings.Contact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@header_reciept", settings.HeaderReceipt ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BG_Color", settings.BackgroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Font_Color", settings.FontColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Body_Color", settings.BodyColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@click_and_collect", settings.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@Order_table", settings.OrderTable);
                    cmd.Parameters.AddWithValue("@theme", settings.Theme ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipAs", settings.TipAs ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@delivery_charges", settings.DeliveryCharges);
                    cmd.Parameters.AddWithValue("@service_charges", settings.ServiceCharges);
                    cmd.Parameters.AddWithValue("@message_delivery", settings.MessageDelivery ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@message_order_table", settings.MessageOrderTable ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@schedule_cnc", settings.ScheduleClickAndCollect);
                    cmd.Parameters.AddWithValue("@future_booking_days", settings.FutureBookingDays);
                    cmd.Parameters.AddWithValue("@HourlySlot", settings.HourlySlot);
                    cmd.Parameters.AddWithValue("@Is_Email_APK", settings.IsEmailAPK);
                    cmd.Parameters.AddWithValue("@Table_As_Box", settings.TableAsBox ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@posLite", settings.PosLite);
                    cmd.Parameters.AddWithValue("@sunmiSecondScreen", settings.SunmiSecondScreen);
                    cmd.Parameters.AddWithValue("@sunmi_video_path", settings.SunmiVideoPath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Settings for location ID {locationId} not found");
                    }
                }

                return Ok("Settings updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Settings/UpdateLogoImage/{locationId}
        [HttpPost("UpdateLogoImage/{locationId}")]
        public async Task<ActionResult> UpdateLogoImage(int locationId, IFormFile logoImage)
        {
            try
            {
                if (logoImage == null || logoImage.Length == 0)
                {
                    return BadRequest("No logo image file was uploaded.");
                }

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await logoImage.CopyToAsync(memoryStream);
                    byte[] logoImageBytes = memoryStream.ToArray();

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("Update_Location_Logo", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@location_id", locationId);
                        cmd.Parameters.AddWithValue("@L_Image", logoImageBytes);
                        cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                        cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"Location with ID {locationId} not found");
                        }
                    }
                }

                return Ok("Logo image updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Settings/GetLogoImage/{locationId}
        [HttpGet("GetLogoImage/{locationId}")]
        public ActionResult GetLogoImage(int locationId)
        {
            try
            {
                byte[] logoImage = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT L_Image FROM M_Location WHERE location_id = @location_id", connection);
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        logoImage = (byte[])result;
                    }
                }

                if (logoImage == null || logoImage.Length == 0)
                {
                    return NotFound($"Logo image for location ID {locationId} not found");
                }

                return File(logoImage, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 