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
    public class GroupCategoryListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public GroupCategoryListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/GroupCategoryList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<GroupCategoryListModel>> GetAllGroupCategories()
        {
            try
            {
                var categories = new List<GroupCategoryListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var category = new GroupCategoryListModel
                        {
                            MainCategoryId = Convert.ToInt32(reader["maincategory_id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : 0,
                            DepartmentName = reader["department_name"] != DBNull.Value ? reader["department_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null,
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : 0,
                            IsAvailableOnline = reader["is_available_online"] != DBNull.Value && Convert.ToBoolean(reader["is_available_online"]),
                            LocationId = reader["location_id"] != DBNull.Value ? reader["location_id"].ToString() : null,
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            Deliver = reader["deliver"] != DBNull.Value && Convert.ToBoolean(reader["deliver"]),
                            OrderAtTable = reader["Order_at_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_at_table"]),
                            WebSalesDescription = reader["web_sales_description"] != DBNull.Value ? reader["web_sales_description"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        categories.Add(category);
                    }
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/GroupCategoryList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<GroupCategoryListModel>> GetGroupCategoriesByCompanyId(int companyId)
        {
            try
            {
                var categories = new List<GroupCategoryListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var category = new GroupCategoryListModel
                        {
                            MainCategoryId = Convert.ToInt32(reader["maincategory_id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : 0,
                            DepartmentName = reader["department_name"] != DBNull.Value ? reader["department_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null,
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : 0,
                            IsAvailableOnline = reader["is_available_online"] != DBNull.Value && Convert.ToBoolean(reader["is_available_online"]),
                            LocationId = reader["location_id"] != DBNull.Value ? reader["location_id"].ToString() : null,
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            Deliver = reader["deliver"] != DBNull.Value && Convert.ToBoolean(reader["deliver"]),
                            OrderAtTable = reader["Order_at_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_at_table"]),
                            WebSalesDescription = reader["web_sales_description"] != DBNull.Value ? reader["web_sales_description"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                        categories.Add(category);
                    }
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/GroupCategoryList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<GroupCategoryListModel> GetGroupCategoryById(int id)
        {
            try
            {
                GroupCategoryListModel category = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maincategory_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        category = new GroupCategoryListModel
                        {
                            MainCategoryId = Convert.ToInt32(reader["maincategory_id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : 0,
                            DepartmentName = reader["department_name"] != DBNull.Value ? reader["department_name"].ToString() : null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null,
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : 0,
                            IsAvailableOnline = reader["is_available_online"] != DBNull.Value && Convert.ToBoolean(reader["is_available_online"]),
                            LocationId = reader["location_id"] != DBNull.Value ? reader["location_id"].ToString() : null,
                            ClickAndCollect = reader["click_and_collect"] != DBNull.Value && Convert.ToBoolean(reader["click_and_collect"]),
                            Deliver = reader["deliver"] != DBNull.Value && Convert.ToBoolean(reader["deliver"]),
                            OrderAtTable = reader["Order_at_table"] != DBNull.Value && Convert.ToBoolean(reader["Order_at_table"]),
                            WebSalesDescription = reader["web_sales_description"] != DBNull.Value ? reader["web_sales_description"].ToString() : null,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null
                        };
                    }
                }

                if (category == null)
                {
                    return NotFound($"Group category with ID {id} not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/GroupCategoryList/Add
        [HttpPost("Add")]
        public ActionResult AddGroupCategory([FromBody] GroupCategoryListModel category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Group category data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maincategory_id", 0);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.Parameters.AddWithValue("@description", category.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", category.CompanyId);
                    cmd.Parameters.AddWithValue("@department_id", category.DepartmentId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@machine_id", category.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", category.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", category.SortingNo);
                    cmd.Parameters.AddWithValue("@is_available_online", category.IsAvailableOnline);
                    cmd.Parameters.AddWithValue("@location_id", category.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@click_and_collect", category.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@deliver", category.Deliver);
                    cmd.Parameters.AddWithValue("@Order_at_table", category.OrderAtTable);
                    cmd.Parameters.AddWithValue("@web_sales_description", category.WebSalesDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", category.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Group category added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/GroupCategoryList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateGroupCategory(int id, [FromBody] GroupCategoryListModel category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Group category data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maincategory_id", id);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.Parameters.AddWithValue("@description", category.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", category.CompanyId);
                    cmd.Parameters.AddWithValue("@department_id", category.DepartmentId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@machine_id", category.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", category.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", category.SortingNo);
                    cmd.Parameters.AddWithValue("@is_available_online", category.IsAvailableOnline);
                    cmd.Parameters.AddWithValue("@location_id", category.LocationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@click_and_collect", category.ClickAndCollect);
                    cmd.Parameters.AddWithValue("@deliver", category.Deliver);
                    cmd.Parameters.AddWithValue("@Order_at_table", category.OrderAtTable);
                    cmd.Parameters.AddWithValue("@web_sales_description", category.WebSalesDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", category.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Group category with ID {id} not found");
                    }
                }

                return Ok("Group category updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/GroupCategoryList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteGroupCategory(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the category exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_MainCategory", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@maincategory_id", id);
                    
                    connection.Open();
                    
                    GroupCategoryListModel category = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new GroupCategoryListModel
                            {
                                MainCategoryId = id,
                                Name = reader["name"].ToString(),
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                                DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : 0
                            };
                        }
                    }
                    
                    if (category == null)
                    {
                        return NotFound($"Group category with ID {id} not found");
                    }

                    // Now delete the category
                    SqlCommand cmd = new SqlCommand("P_M_MainCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maincategory_id", id);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.Parameters.AddWithValue("@description", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cmp_id", category.CompanyId);
                    cmd.Parameters.AddWithValue("@department_id", category.DepartmentId);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@machine_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", 0);
                    cmd.Parameters.AddWithValue("@is_available_online", false);
                    cmd.Parameters.AddWithValue("@location_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@click_and_collect", false);
                    cmd.Parameters.AddWithValue("@deliver", false);
                    cmd.Parameters.AddWithValue("@Order_at_table", false);
                    cmd.Parameters.AddWithValue("@web_sales_description", DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Group category deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 