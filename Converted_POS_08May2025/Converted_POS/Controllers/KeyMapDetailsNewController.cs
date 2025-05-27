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
    public class KeyMapDetailsNewController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public KeyMapDetailsNewController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/KeyMapDetailsNew/GetByKeyMapId/{keyMapId}
        [HttpGet("GetByKeyMapId/{keyMapId}")]
        public ActionResult<IEnumerable<KeyMapDetailsNewModel>> GetKeyMapDetailsByKeyMapId(int keyMapId)
        {
            try
            {
                var keyMapDetails = new List<KeyMapDetailsNewModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Key_Map_Details", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@key_map_id", keyMapId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var detail = new KeyMapDetailsNewModel
                        {
                            Id = Convert.ToInt32(reader["key_map_detail_id"]),
                            KeyMapId = Convert.ToInt32(reader["key_map_id"]),
                            ProductGroupId = reader["Product_Group_id"] != DBNull.Value ? Convert.ToInt32(reader["Product_Group_id"]) : (int?)null,
                            ProductId = reader["Product_id"] != DBNull.Value ? Convert.ToInt32(reader["Product_id"]) : (int?)null,
                            SizeId = reader["Size_id"] != DBNull.Value ? Convert.ToInt32(reader["Size_id"]) : (int?)null,
                            BackgroundColor = reader["BG_Color"] != DBNull.Value ? reader["BG_Color"].ToString() : null,
                            ForegroundColor = reader["FG_Color"] != DBNull.Value ? reader["FG_Color"].ToString() : null,
                            FontColor = reader["Font_Color"] != DBNull.Value ? reader["Font_Color"].ToString() : null,
                            Matrix = reader["matrix"] != DBNull.Value ? reader["matrix"].ToString() : null,
                            DisplayName = reader["Display_Name"] != DBNull.Value ? reader["Display_Name"].ToString() : null,
                            MainCategoryId = reader["maincategory_id"] != DBNull.Value ? Convert.ToInt32(reader["maincategory_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            // Navigation properties 
                            KeyMapName = reader["key_map_name"] != DBNull.Value ? reader["key_map_name"].ToString() : null,
                            ProductGroupName = reader["product_group_name"] != DBNull.Value ? reader["product_group_name"].ToString() : null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null,
                            SizeName = reader["size_name"] != DBNull.Value ? reader["size_name"].ToString() : null,
                            MainCategoryName = reader["maincategory_name"] != DBNull.Value ? reader["maincategory_name"].ToString() : null
                        };
                        keyMapDetails.Add(detail);
                    }
                }

                return Ok(keyMapDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KeyMapDetailsNew/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<KeyMapDetailsNewModel> GetKeyMapDetailById(int id)
        {
            try
            {
                KeyMapDetailsNewModel keyMapDetail = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Key_Map_Detail_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@key_map_detail_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        keyMapDetail = new KeyMapDetailsNewModel
                        {
                            Id = Convert.ToInt32(reader["key_map_detail_id"]),
                            KeyMapId = Convert.ToInt32(reader["key_map_id"]),
                            ProductGroupId = reader["Product_Group_id"] != DBNull.Value ? Convert.ToInt32(reader["Product_Group_id"]) : (int?)null,
                            ProductId = reader["Product_id"] != DBNull.Value ? Convert.ToInt32(reader["Product_id"]) : (int?)null,
                            SizeId = reader["Size_id"] != DBNull.Value ? Convert.ToInt32(reader["Size_id"]) : (int?)null,
                            BackgroundColor = reader["BG_Color"] != DBNull.Value ? reader["BG_Color"].ToString() : null,
                            ForegroundColor = reader["FG_Color"] != DBNull.Value ? reader["FG_Color"].ToString() : null,
                            FontColor = reader["Font_Color"] != DBNull.Value ? reader["Font_Color"].ToString() : null,
                            Matrix = reader["matrix"] != DBNull.Value ? reader["matrix"].ToString() : null,
                            DisplayName = reader["Display_Name"] != DBNull.Value ? reader["Display_Name"].ToString() : null,
                            MainCategoryId = reader["maincategory_id"] != DBNull.Value ? Convert.ToInt32(reader["maincategory_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            // Navigation properties 
                            KeyMapName = reader["key_map_name"] != DBNull.Value ? reader["key_map_name"].ToString() : null,
                            ProductGroupName = reader["product_group_name"] != DBNull.Value ? reader["product_group_name"].ToString() : null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null,
                            SizeName = reader["size_name"] != DBNull.Value ? reader["size_name"].ToString() : null,
                            MainCategoryName = reader["maincategory_name"] != DBNull.Value ? reader["maincategory_name"].ToString() : null
                        };
                    }
                }

                if (keyMapDetail == null)
                {
                    return NotFound($"Key map detail with ID {id} not found");
                }

                return Ok(keyMapDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/KeyMapDetailsNew/Add
        [HttpPost("Add")]
        public ActionResult AddKeyMapDetail([FromBody] KeyMapDetailsNewModel keyMapDetail)
        {
            try
            {
                if (keyMapDetail == null)
                {
                    return BadRequest("Key map detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_KeyMapDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@key_map_detail_id", 0);
                    cmd.Parameters.AddWithValue("@key_map_id", keyMapDetail.KeyMapId);
                    cmd.Parameters.AddWithValue("@Product_Group_id", keyMapDetail.ProductGroupId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Product_id", keyMapDetail.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", keyMapDetail.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", keyMapDetail.IsActive);
                    cmd.Parameters.AddWithValue("@BG_Color", keyMapDetail.BackgroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FG_Color", keyMapDetail.ForegroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@matrix", keyMapDetail.Matrix ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@cmp_id", keyMapDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@maincategory_id", keyMapDetail.MainCategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Key map detail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/KeyMapDetailsNew/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateKeyMapDetail(int id, [FromBody] KeyMapDetailsNewModel keyMapDetail)
        {
            try
            {
                if (keyMapDetail == null)
                {
                    return BadRequest("Key map detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_KeyMapDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@key_map_detail_id", id);
                    cmd.Parameters.AddWithValue("@key_map_id", keyMapDetail.KeyMapId);
                    cmd.Parameters.AddWithValue("@Product_Group_id", keyMapDetail.ProductGroupId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Product_id", keyMapDetail.ProductId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", keyMapDetail.SizeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", keyMapDetail.IsActive);
                    cmd.Parameters.AddWithValue("@BG_Color", keyMapDetail.BackgroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FG_Color", keyMapDetail.ForegroundColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@matrix", keyMapDetail.Matrix ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@cmp_id", keyMapDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@maincategory_id", keyMapDetail.MainCategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Key map detail with ID {id} not found");
                    }
                }

                return Ok("Key map detail updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KeyMapDetailsNew/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteKeyMapDetail(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the record exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Key_Map_Detail_By_Id", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@key_map_detail_id", id);
                    
                    connection.Open();
                    
                    bool recordExists = false;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        recordExists = reader.HasRows;
                    }
                    
                    if (!recordExists)
                    {
                        return NotFound($"Key map detail with ID {id} not found");
                    }
                    
                    // Now delete the record
                    SqlCommand cmd = new SqlCommand("P_M_KeyMapDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@key_map_detail_id", id);
                    cmd.Parameters.AddWithValue("@key_map_id", 0);
                    cmd.Parameters.AddWithValue("@Product_Group_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Product_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@BG_Color", DBNull.Value);
                    cmd.Parameters.AddWithValue("@FG_Color", DBNull.Value);
                    cmd.Parameters.AddWithValue("@matrix", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@maincategory_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Key map detail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KeyMapDetailsNew/DeleteByKeyMapId/{keyMapId}
        [HttpDelete("DeleteByKeyMapId/{keyMapId}")]
        public ActionResult DeleteKeyMapDetailsByKeyMapId(int keyMapId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Key_Map_Details_Edit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@key_map_id", keyMapId);
                    cmd.Parameters.AddWithValue("@cmp_id", HttpContext.Session.GetInt32("CompanyID") ?? 0);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Key map details deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 