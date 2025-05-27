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
    public class KeyMapListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public KeyMapListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/KeyMapList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<KeyMapModel>> GetAllKeyMaps()
        {
            try
            {
                var keyMaps = new List<KeyMapModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Key_Maps", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var keyMap = new KeyMapModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            KeyType = reader["key_type"] != DBNull.Value ? reader["key_type"].ToString() : null,
                            TabOrder = reader["tab_order"] != DBNull.Value ? Convert.ToInt32(reader["tab_order"]) : (int?)null,
                            ParentId = reader["parent_id"] != DBNull.Value ? Convert.ToInt32(reader["parent_id"]) : (int?)null,
                            ParentName = reader["parent_name"] != DBNull.Value ? reader["parent_name"].ToString() : null,
                            DisplayText = reader["display_text"] != DBNull.Value ? reader["display_text"].ToString() : null,
                            BackgroundColor = reader["background_color"] != DBNull.Value ? reader["background_color"].ToString() : null,
                            FontColor = reader["font_color"] != DBNull.Value ? reader["font_color"].ToString() : null,
                            IconPath = reader["icon_path"] != DBNull.Value ? reader["icon_path"].ToString() : null,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                        keyMaps.Add(keyMap);
                    }
                }

                return Ok(keyMaps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KeyMapList/GetByLocationId/{locationId}
        [HttpGet("GetByLocationId/{locationId}")]
        public ActionResult<IEnumerable<KeyMapModel>> GetKeyMapsByLocationId(int locationId)
        {
            try
            {
                var keyMaps = new List<KeyMapModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Key_Maps_By_Location_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var keyMap = new KeyMapModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            KeyType = reader["key_type"] != DBNull.Value ? reader["key_type"].ToString() : null,
                            TabOrder = reader["tab_order"] != DBNull.Value ? Convert.ToInt32(reader["tab_order"]) : (int?)null,
                            ParentId = reader["parent_id"] != DBNull.Value ? Convert.ToInt32(reader["parent_id"]) : (int?)null,
                            ParentName = reader["parent_name"] != DBNull.Value ? reader["parent_name"].ToString() : null,
                            DisplayText = reader["display_text"] != DBNull.Value ? reader["display_text"].ToString() : null,
                            BackgroundColor = reader["background_color"] != DBNull.Value ? reader["background_color"].ToString() : null,
                            FontColor = reader["font_color"] != DBNull.Value ? reader["font_color"].ToString() : null,
                            IconPath = reader["icon_path"] != DBNull.Value ? reader["icon_path"].ToString() : null,
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                        keyMaps.Add(keyMap);
                    }
                }

                return Ok(keyMaps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KeyMapList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<KeyMapModel> GetKeyMapById(int id)
        {
            try
            {
                KeyMapModel keyMap = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Key_Map_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        keyMap = new KeyMapModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            KeyType = reader["key_type"] != DBNull.Value ? reader["key_type"].ToString() : null,
                            TabOrder = reader["tab_order"] != DBNull.Value ? Convert.ToInt32(reader["tab_order"]) : (int?)null,
                            ParentId = reader["parent_id"] != DBNull.Value ? Convert.ToInt32(reader["parent_id"]) : (int?)null,
                            ParentName = reader["parent_name"] != DBNull.Value ? reader["parent_name"].ToString() : null,
                            DisplayText = reader["display_text"] != DBNull.Value ? reader["display_text"].ToString() : null,
                            BackgroundColor = reader["background_color"] != DBNull.Value ? reader["background_color"].ToString() : null,
                            FontColor = reader["font_color"] != DBNull.Value ? reader["font_color"].ToString() : null,
                            IconPath = reader["icon_path"] != DBNull.Value ? reader["icon_path"].ToString() : null,
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

                if (keyMap == null)
                {
                    return NotFound($"Key map with ID {id} not found");
                }

                return Ok(keyMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/KeyMapList/Add
        [HttpPost("Add")]
        public ActionResult AddKeyMap([FromBody] KeyMapModel keyMap)
        {
            try
            {
                if (keyMap == null)
                {
                    return BadRequest("Key map data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Key_Map", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@company_id", keyMap.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", keyMap.LocationId);
                    cmd.Parameters.AddWithValue("@name", keyMap.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@key_type", keyMap.KeyType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tab_order", keyMap.TabOrder ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@parent_id", keyMap.ParentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@display_text", keyMap.DisplayText ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@background_color", keyMap.BackgroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@font_color", keyMap.FontColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@icon_path", keyMap.IconPath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", keyMap.IsActive);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Key map added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/KeyMapList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateKeyMap(int id, [FromBody] KeyMapModel keyMap)
        {
            try
            {
                if (keyMap == null)
                {
                    return BadRequest("Key map data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Key_Map", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@company_id", keyMap.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", keyMap.LocationId);
                    cmd.Parameters.AddWithValue("@name", keyMap.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@key_type", keyMap.KeyType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tab_order", keyMap.TabOrder ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@parent_id", keyMap.ParentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@display_text", keyMap.DisplayText ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@background_color", keyMap.BackgroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@font_color", keyMap.FontColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@icon_path", keyMap.IconPath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", keyMap.IsActive);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Key map with ID {id} not found");
                    }
                }

                return Ok("Key map updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KeyMapList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteKeyMap(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Key_Map", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Key map with ID {id} not found");
                    }
                }

                return Ok("Key map deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 