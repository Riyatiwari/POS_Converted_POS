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
    public class CourseListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CourseListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/CourseList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<CourseListModel>> GetAllCourses()
        {
            try
            {
                var courses = new List<CourseListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var course = new CourseListModel
                        {
                            CourseId = Convert.ToInt32(reader["course_id"]),
                            Name = reader["name"].ToString(),
                            Value = Convert.ToInt32(reader["value"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CourseCategoryId = reader["course_category_id"] != DBNull.Value ? Convert.ToInt32(reader["course_category_id"]) : 0,
                            CourseCategoryName = reader["cname"] != DBNull.Value ? reader["cname"].ToString() : null,
                            IsCheckSlot = reader["is_checkSlot"] != DBNull.Value ? Convert.ToBoolean(reader["is_checkSlot"]) : false,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        courses.Add(course);
                    }
                }

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CourseList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<CourseListModel>> GetCoursesByCompanyId(int companyId)
        {
            try
            {
                var courses = new List<CourseListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var course = new CourseListModel
                        {
                            CourseId = Convert.ToInt32(reader["course_id"]),
                            Name = reader["name"].ToString(),
                            Value = Convert.ToInt32(reader["value"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CourseCategoryId = reader["course_category_id"] != DBNull.Value ? Convert.ToInt32(reader["course_category_id"]) : 0,
                            CourseCategoryName = reader["cname"] != DBNull.Value ? reader["cname"].ToString() : null,
                            IsCheckSlot = reader["is_checkSlot"] != DBNull.Value ? Convert.ToBoolean(reader["is_checkSlot"]) : false,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        courses.Add(course);
                    }
                }

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CourseList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<CourseListModel> GetCourseById(int id)
        {
            try
            {
                CourseListModel course = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Course_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        course = new CourseListModel
                        {
                            CourseId = Convert.ToInt32(reader["course_id"]),
                            Name = reader["name"].ToString(),
                            Value = Convert.ToInt32(reader["value"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CourseCategoryId = reader["course_category_id"] != DBNull.Value ? Convert.ToInt32(reader["course_category_id"]) : 0,
                            CourseCategoryName = reader["cname"] != DBNull.Value ? reader["cname"].ToString() : null,
                            IsCheckSlot = reader["is_checkSlot"] != DBNull.Value ? Convert.ToBoolean(reader["is_checkSlot"]) : false,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found");
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/CourseList/Add
        [HttpPost("Add")]
        public ActionResult AddCourse([FromBody] CourseListModel course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Course data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Course_id", 0);
                    cmd.Parameters.AddWithValue("@Name", course.Name);
                    cmd.Parameters.AddWithValue("@Value", course.Value);
                    cmd.Parameters.AddWithValue("@Cmp_id", course.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                    cmd.Parameters.AddWithValue("@Course_Category_id", course.CourseCategoryId);
                    cmd.Parameters.AddWithValue("@is_checkslot", course.IsCheckSlot);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Course added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/CourseList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateCourse(int id, [FromBody] CourseListModel course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Course data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Course_id", id);
                    cmd.Parameters.AddWithValue("@Name", course.Name);
                    cmd.Parameters.AddWithValue("@Value", course.Value);
                    cmd.Parameters.AddWithValue("@Cmp_id", course.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                    cmd.Parameters.AddWithValue("@Course_Category_id", course.CourseCategoryId);
                    cmd.Parameters.AddWithValue("@is_checkslot", course.IsCheckSlot);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Course with ID {id} not found");
                    }
                }

                return Ok("Course updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/CourseList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Get the course details first to pass to the delete procedure
                    CourseListModel course = null;
                    SqlCommand getCmd = new SqlCommand("Get_M_Course", connection);
                    getCmd.CommandType = CommandType.StoredProcedure;
                    getCmd.Parameters.AddWithValue("@Course_id", id);
                    
                    connection.Open();
                    SqlDataReader reader = getCmd.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        course = new CourseListModel
                        {
                            CourseId = Convert.ToInt32(reader["course_id"]),
                            Name = reader["name"].ToString(),
                            Value = Convert.ToInt32(reader["value"]),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            CourseCategoryId = reader["course_category_id"] != DBNull.Value ? Convert.ToInt32(reader["course_category_id"]) : 0
                        };
                    }
                    
                    reader.Close();
                    
                    if (course == null)
                    {
                        return NotFound($"Course with ID {id} not found");
                    }

                    SqlCommand cmd = new SqlCommand("P_M_Course", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Course_id", id);
                    cmd.Parameters.AddWithValue("@Name", course.Name);
                    cmd.Parameters.AddWithValue("@Value", course.Value);
                    cmd.Parameters.AddWithValue("@Cmp_id", course.CompanyId);
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    cmd.Parameters.AddWithValue("@Course_Category_id", course.CourseCategoryId);
                    cmd.Parameters.AddWithValue("@is_checkslot", false);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Course with ID {id} not found");
                    }
                }

                return Ok("Course deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 