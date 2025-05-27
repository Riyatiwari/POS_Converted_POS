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
    public class RoleListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public RoleListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/RoleList/GetRoles/{companyId}
        [HttpGet("GetRoles/{companyId}")]
        public ActionResult<IEnumerable<RoleModel>> GetRoles(int companyId)
        {
            try
            {
                var roles = new List<RoleModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Role", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cmp_Id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var role = new RoleModel
                        {
                            RoleId = Convert.ToInt32(reader["Role_id"]),
                            CompanyId = companyId,
                            RoleName = reader["Role_name"].ToString(),
                            Type = reader["Role_Type"] != DBNull.Value ? Convert.ToByte(reader["Role_Type"]) : (byte?)null,
                            RoleDetail = reader["Role_Detail"].ToString(),
                            IsActive = reader["is_Active"] != DBNull.Value && Convert.ToBoolean(reader["is_Active"])
                        };
                        roles.Add(role);
                    }
                }

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/RoleList/GetRoleById/{id}
        [HttpGet("GetRoleById/{id}/{companyId}")]
        public ActionResult<RoleModel> GetRoleById(int id, int companyId)
        {
            try
            {
                RoleModel role = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Role", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cmp_Id", companyId);
                    cmd.Parameters.AddWithValue("@Role_Id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        role = new RoleModel
                        {
                            RoleId = id,
                            CompanyId = companyId,
                            RoleName = reader["Role_name"].ToString(),
                            Type = reader["Role_Type"] != DBNull.Value ? Convert.ToByte(reader["Role_Type"]) : (byte?)null,
                            RoleDetail = reader["Role_Detail"].ToString(),
                            IsActive = reader["is_Active"] != DBNull.Value && Convert.ToBoolean(reader["is_Active"])
                        };
                    }
                }

                if (role == null)
                {
                    return NotFound($"Role with ID {id} not found");
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/RoleList/GetRoleForms/{roleId}
        [HttpGet("GetRoleForms/{roleId}")]
        public ActionResult<IEnumerable<RoleFormModel>> GetRoleForms(int roleId)
        {
            try
            {
                var roleForms = new List<RoleFormModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Role_Form", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Role_id", roleId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var roleForm = new RoleFormModel
                        {
                            FormId = Convert.ToInt32(reader["Form_id"]),
                            FormName = reader["Form_name"].ToString(),
                            IsView = reader["is_View"] != DBNull.Value && Convert.ToBoolean(reader["is_View"]),
                            IsAdd = reader["is_add"] != DBNull.Value && Convert.ToBoolean(reader["is_add"]),
                            IsEdit = reader["is_Edit"] != DBNull.Value && Convert.ToBoolean(reader["is_Edit"]),
                            IsDelete = reader["is_Delete"] != DBNull.Value && Convert.ToBoolean(reader["is_Delete"]),
                            RoleId = roleId
                        };
                        roleForms.Add(roleForm);
                    }
                }

                return Ok(roleForms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/RoleList/GetAllForms
        [HttpGet("GetAllForms")]
        public ActionResult<IEnumerable<RoleFormModel>> GetAllForms()
        {
            try
            {
                var forms = new List<RoleFormModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Form", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var form = new RoleFormModel
                        {
                            FormId = Convert.ToInt32(reader["Form_id"]),
                            FormName = reader["Form_name"].ToString()
                        };
                        forms.Add(form);
                    }
                }

                return Ok(forms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/RoleList/AddRole
        [HttpPost("AddRole")]
        public ActionResult AddRole([FromBody] RoleModel role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest("Role data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Role", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Role_id", 0); // New role
                    cmd.Parameters.AddWithValue("@Cmp_id", role.CompanyId);
                    cmd.Parameters.AddWithValue("@Role_name", role.RoleName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role_Type", role.Type ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role_Detail", role.RoleDetail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_Active", role.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Role added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/RoleList/UpdateRole/{id}
        [HttpPut("UpdateRole/{id}")]
        public ActionResult UpdateRole(int id, [FromBody] RoleModel role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest("Role data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Role", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Role_id", id);
                    cmd.Parameters.AddWithValue("@Cmp_id", role.CompanyId);
                    cmd.Parameters.AddWithValue("@Role_name", role.RoleName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role_Type", role.Type ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role_Detail", role.RoleDetail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_Active", role.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Role with ID {id} not found");
                    }
                }

                return Ok("Role updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/RoleList/DeleteRole/{id}
        [HttpDelete("DeleteRole/{id}")]
        public ActionResult DeleteRole(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Role", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Role_id", id);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Role with ID {id} not found");
                    }
                }

                return Ok("Role deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 