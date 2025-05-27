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
    public class DepartmentListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DepartmentListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/DepartmentList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DepartmentListModel>> GetAllDepartments()
        {
            try
            {
                var departments = new List<DepartmentListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var department = new DepartmentListModel
                        {
                            DepartmentId = Convert.ToInt32(reader["Department_id"]),
                            Name = reader["Name"].ToString(),
                            Value = reader["value"] != DBNull.Value ? Convert.ToInt32(reader["value"]) : 0,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentCategoryId = reader["department_category_id"] != DBNull.Value ? Convert.ToInt32(reader["department_category_id"]) : (int?)null,
                            DepartmentCategoryName = reader["department_category_name"] != DBNull.Value ? reader["department_category_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        departments.Add(department);
                    }
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DepartmentList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<DepartmentListModel>> GetDepartmentsByCompanyId(int companyId)
        {
            try
            {
                var departments = new List<DepartmentListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var department = new DepartmentListModel
                        {
                            DepartmentId = Convert.ToInt32(reader["Department_id"]),
                            Name = reader["Name"].ToString(),
                            Value = reader["value"] != DBNull.Value ? Convert.ToInt32(reader["value"]) : 0,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentCategoryId = reader["department_category_id"] != DBNull.Value ? Convert.ToInt32(reader["department_category_id"]) : (int?)null,
                            DepartmentCategoryName = reader["department_category_name"] != DBNull.Value ? reader["department_category_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        departments.Add(department);
                    }
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DepartmentList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<DepartmentListModel> GetDepartmentById(int id)
        {
            try
            {
                DepartmentListModel department = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Department_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        department = new DepartmentListModel
                        {
                            DepartmentId = Convert.ToInt32(reader["Department_id"]),
                            Name = reader["Name"].ToString(),
                            Value = reader["value"] != DBNull.Value ? Convert.ToInt32(reader["value"]) : 0,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentCategoryId = reader["department_category_id"] != DBNull.Value ? Convert.ToInt32(reader["department_category_id"]) : (int?)null,
                            DepartmentCategoryName = reader["department_category_name"] != DBNull.Value ? reader["department_category_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found");
                }

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/DepartmentList/Add
        [HttpPost("Add")]
        public ActionResult AddDepartment([FromBody] DepartmentListModel department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Department data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Department_id", 0);
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.Parameters.AddWithValue("@Value", department.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", department.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Department_category_id", department.DepartmentCategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Department added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/DepartmentList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateDepartment(int id, [FromBody] DepartmentListModel department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Department data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Department_id", id);
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.Parameters.AddWithValue("@Value", department.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", department.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Department_category_id", department.DepartmentCategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Department with ID {id} not found");
                    }
                }

                return Ok("Department updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/DepartmentList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Check if the department exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Department", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@Department_id", id);
                    
                    connection.Open();
                    
                    DepartmentListModel department = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            department = new DepartmentListModel
                            {
                                DepartmentId = id,
                                Name = reader["Name"].ToString(),
                                Value = reader["value"] != DBNull.Value ? Convert.ToInt32(reader["value"]) : 0,
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                                DepartmentCategoryId = reader["department_category_id"] != DBNull.Value ? Convert.ToInt32(reader["department_category_id"]) : (int?)null
                            };
                        }
                    }
                    
                    if (department == null)
                    {
                        return NotFound($"Department with ID {id} not found");
                    }

                    // Delete the department
                    SqlCommand cmd = new SqlCommand("P_M_Department", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Department_id", id);
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.Parameters.AddWithValue("@Value", department.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", department.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Department_category_id", department.DepartmentCategoryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Department deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 