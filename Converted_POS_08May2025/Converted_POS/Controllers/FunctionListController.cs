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
    public class FunctionListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public FunctionListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/FunctionList/GetFunctions/{companyId}
        [HttpGet("GetFunctions/{companyId}")]
        public ActionResult<IEnumerable<FunctionModel>> GetFunctions(int companyId)
        {
            try
            {
                var functions = new List<FunctionModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Function", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var function = new FunctionModel
                        {
                            FunctionId = Convert.ToInt32(reader["function_id"]),
                            CompanyId = companyId,
                            Name = reader["name"].ToString(),
                            Code = reader["code"].ToString(),
                            CaptionType = reader["caption_type"].ToString(),
                            ShortingNo = reader["shorting_no"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            FormName = reader["form_name"].ToString(),
                            BackColor = reader["back_color"].ToString(),
                            ForColor = reader["for_color"].ToString(),
                            BigButton = reader["big_button"] != DBNull.Value && Convert.ToBoolean(reader["big_button"]),
                            IsGroupByDept = reader["isgroupbydept"] != DBNull.Value && Convert.ToBoolean(reader["isgroupbydept"]),
                            IsGroupByCourse = reader["isgroupbycourse"] != DBNull.Value && Convert.ToBoolean(reader["isgroupbycourse"]),
                            DeptId = reader["dept_id"].ToString(),
                            CourseId = reader["course_id"].ToString(),
                            PanelId = reader["Panel_id"] != DBNull.Value ? Convert.ToInt32(reader["Panel_id"]) : (int?)null,
                            ParentId = reader["Parent_id"] != DBNull.Value ? Convert.ToInt32(reader["Parent_id"]) : (int?)null,
                            MainFunctionId = reader["mainfunction_id"] != DBNull.Value ? Convert.ToInt32(reader["mainfunction_id"]) : (int?)null
                        };
                        functions.Add(function);
                    }
                }

                return Ok(functions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/FunctionList/GetFunctionById/{id}
        [HttpGet("GetFunctionById/{id}")]
        public ActionResult<FunctionModel> GetFunctionById(int id)
        {
            try
            {
                FunctionModel function = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Function_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@function_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        function = new FunctionModel
                        {
                            FunctionId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Name = reader["name"].ToString(),
                            Code = reader["code"].ToString(),
                            CaptionType = reader["caption_type"].ToString(),
                            ShortingNo = reader["shorting_no"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            FormName = reader["form_name"].ToString(),
                            BackColor = reader["back_color"].ToString(),
                            ForColor = reader["for_color"].ToString(),
                            BigButton = reader["big_button"] != DBNull.Value && Convert.ToBoolean(reader["big_button"]),
                            IsGroupByDept = reader["isgroupbydept"] != DBNull.Value && Convert.ToBoolean(reader["isgroupbydept"]),
                            IsGroupByCourse = reader["isgroupbycourse"] != DBNull.Value && Convert.ToBoolean(reader["isgroupbycourse"]),
                            DeptId = reader["dept_id"].ToString(),
                            CourseId = reader["course_id"].ToString(),
                            PanelId = reader["Panel_id"] != DBNull.Value ? Convert.ToInt32(reader["Panel_id"]) : (int?)null,
                            ParentId = reader["Parent_id"] != DBNull.Value ? Convert.ToInt32(reader["Parent_id"]) : (int?)null,
                            MainFunctionId = reader["mainfunction_id"] != DBNull.Value ? Convert.ToInt32(reader["mainfunction_id"]) : (int?)null
                        };
                    }
                }

                if (function == null)
                {
                    return NotFound($"Function with ID {id} not found");
                }

                return Ok(function);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/FunctionList/GetFunctionsByCmp/{companyId}
        [HttpGet("GetFunctionsByCmp/{companyId}")]
        public ActionResult<IEnumerable<FunctionModel>> GetFunctionsByCmp(int companyId)
        {
            try
            {
                var functions = new List<FunctionModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Function_By_Cmp_ForTill", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var function = new FunctionModel
                        {
                            FunctionId = Convert.ToInt32(reader["function_id"]),
                            CompanyId = companyId,
                            Name = reader["name"].ToString(),
                            Code = reader["code"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                        functions.Add(function);
                    }
                }

                return Ok(functions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/FunctionList/AddFunction
        [HttpPost("AddFunction")]
        public ActionResult AddFunction([FromBody] FunctionModel function)
        {
            try
            {
                if (function == null)
                {
                    return BadRequest("Function data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Function", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@function_id", 0); // New function
                    cmd.Parameters.AddWithValue("@cmp_id", function.CompanyId);
                    cmd.Parameters.AddWithValue("@name", function.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@code", function.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@caption_type", function.CaptionType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@shorting_no", function.ShortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", function.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@form_name", function.FormName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@back_color", function.BackColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@for_color", function.ForColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@big_button", function.BigButton);
                    cmd.Parameters.AddWithValue("@isgroupbydept", function.IsGroupByDept);
                    cmd.Parameters.AddWithValue("@isgroupbycourse", function.IsGroupByCourse);
                    cmd.Parameters.AddWithValue("@dept_id", function.DeptId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@course_id", function.CourseId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Panel_id", function.PanelId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Parent_id", function.ParentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mainfunction_id", function.MainFunctionId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Function added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/FunctionList/UpdateFunction/{id}
        [HttpPut("UpdateFunction/{id}")]
        public ActionResult UpdateFunction(int id, [FromBody] FunctionModel function)
        {
            try
            {
                if (function == null)
                {
                    return BadRequest("Function data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Function", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@function_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", function.CompanyId);
                    cmd.Parameters.AddWithValue("@name", function.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@code", function.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@caption_type", function.CaptionType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@shorting_no", function.ShortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", function.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@form_name", function.FormName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@back_color", function.BackColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@for_color", function.ForColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@big_button", function.BigButton);
                    cmd.Parameters.AddWithValue("@isgroupbydept", function.IsGroupByDept);
                    cmd.Parameters.AddWithValue("@isgroupbycourse", function.IsGroupByCourse);
                    cmd.Parameters.AddWithValue("@dept_id", function.DeptId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@course_id", function.CourseId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Panel_id", function.PanelId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Parent_id", function.ParentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mainfunction_id", function.MainFunctionId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Function with ID {id} not found");
                    }
                }

                return Ok("Function updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/FunctionList/DeleteFunction/{id}
        [HttpDelete("DeleteFunction/{id}")]
        public ActionResult DeleteFunction(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Function", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@function_id", id);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Function with ID {id} not found");
                    }
                }

                return Ok("Function deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 