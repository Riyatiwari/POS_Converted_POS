using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

namespace Converted_POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/UserList/GetUsers/{companyId}
        [HttpGet("GetUsers/{companyId}")]
        public ActionResult<IEnumerable<UserModel>> GetUsers(int companyId)
        {
            try
            {
                var users = new List<UserModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Staff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            StaffId = Convert.ToInt32(reader["staff_id"]),
                            CompanyId = companyId,
                            StaffCode = reader["staff_code"].ToString(),
                            Name = reader["name"].ToString(),
                            JoiningDate = reader["joining_date"] != DBNull.Value ? Convert.ToDateTime(reader["joining_date"]) : (DateTime?)null,
                            BranchId = reader["branch_id"] != DBNull.Value ? Convert.ToInt32(reader["branch_id"]) : (int?)null,
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : (int?)null,
                            DesignationId = reader["designation_id"] != DBNull.Value ? Convert.ToInt32(reader["designation_id"]) : (int?)null,
                            Address = reader["address"].ToString(),
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            NationalId = reader["national_id"].ToString(),
                            ContactNo = reader["contact_no"].ToString(),
                            Email = reader["email"].ToString(),
                            LastWorkingDate = reader["last_working_date"] != DBNull.Value ? Convert.ToDateTime(reader["last_working_date"]) : (DateTime?)null,
                            RoleId = reader["role_id"] != DBNull.Value ? Convert.ToInt32(reader["role_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            OtherId = reader["other_id"].ToString(),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            TillCode = reader["till_code"].ToString(),
                            TillActive = reader["till_active"] != DBNull.Value && Convert.ToBoolean(reader["till_active"]),
                            Photo = reader["photo"].ToString(),
                            AuthenticationCode = reader["Authentication_Code"].ToString(),
                            FunctionTypeId = reader["Function_type_id"].ToString(),
                            IsTrainee = reader["is_trainee"] != DBNull.Value && Convert.ToBoolean(reader["is_trainee"]),
                            FromDate = reader["from_date"] != DBNull.Value ? Convert.ToDateTime(reader["from_date"]) : (DateTime?)null,
                            ToDate = reader["to_date"] != DBNull.Value ? Convert.ToDateTime(reader["to_date"]) : (DateTime?)null,
                            VenueId = reader["venue_id"].ToString(),
                            UserUUID = reader["UserUUID"].ToString()
                        };
                        users.Add(user);
                    }
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/UserList/GetUserById/{id}
        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            try
            {
                UserModel user = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Staff_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@staff_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            StaffId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            StaffCode = reader["staff_code"].ToString(),
                            Name = reader["name"].ToString(),
                            JoiningDate = reader["joining_date"] != DBNull.Value ? Convert.ToDateTime(reader["joining_date"]) : (DateTime?)null,
                            BranchId = reader["branch_id"] != DBNull.Value ? Convert.ToInt32(reader["branch_id"]) : (int?)null,
                            DepartmentId = reader["department_id"] != DBNull.Value ? Convert.ToInt32(reader["department_id"]) : (int?)null,
                            DesignationId = reader["designation_id"] != DBNull.Value ? Convert.ToInt32(reader["designation_id"]) : (int?)null,
                            Address = reader["address"].ToString(),
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            NationalId = reader["national_id"].ToString(),
                            ContactNo = reader["contact_no"].ToString(),
                            Email = reader["email"].ToString(),
                            LastWorkingDate = reader["last_working_date"] != DBNull.Value ? Convert.ToDateTime(reader["last_working_date"]) : (DateTime?)null,
                            RoleId = reader["role_id"] != DBNull.Value ? Convert.ToInt32(reader["role_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            OtherId = reader["other_id"].ToString(),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            TillCode = reader["till_code"].ToString(),
                            TillActive = reader["till_active"] != DBNull.Value && Convert.ToBoolean(reader["till_active"]),
                            Photo = reader["photo"].ToString(),
                            AuthenticationCode = reader["Authentication_Code"].ToString(),
                            FunctionTypeId = reader["Function_type_id"].ToString(),
                            IsTrainee = reader["is_trainee"] != DBNull.Value && Convert.ToBoolean(reader["is_trainee"]),
                            FromDate = reader["from_date"] != DBNull.Value ? Convert.ToDateTime(reader["from_date"]) : (DateTime?)null,
                            ToDate = reader["to_date"] != DBNull.Value ? Convert.ToDateTime(reader["to_date"]) : (DateTime?)null,
                            VenueId = reader["venue_id"].ToString(),
                            UserUUID = reader["UserUUID"].ToString()
                        };
                    }
                }

                if (user == null)
                {
                    return NotFound($"User with ID {id} not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/UserList/GetUserByCode/{code}
        [HttpGet("GetUserByCode/{code}")]
        public ActionResult<UserModel> GetUserByCode(string code)
        {
            try
            {
                UserModel user = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Staff_By_Code", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@staff_code", code);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            StaffId = Convert.ToInt32(reader["staff_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            StaffCode = code,
                            Name = reader["name"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            RoleId = reader["role_id"] != DBNull.Value ? Convert.ToInt32(reader["role_id"]) : (int?)null
                        };
                    }
                }

                if (user == null)
                {
                    return NotFound($"User with code {code} not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/UserList/AddUser
        [HttpPost("AddUser")]
        public ActionResult AddUser([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@staff_id", 0); // New user
                    cmd.Parameters.AddWithValue("@cmp_id", user.CompanyId);
                    cmd.Parameters.AddWithValue("@staff_code", user.StaffCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", user.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@joining_date", user.JoiningDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@branch_id", user.BranchId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@department_id", user.DepartmentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@designation_id", user.DesignationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", user.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@country_id", user.CountryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@state_id", user.StateId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city_id", user.CityId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@national_id", user.NationalId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@contact_no", user.ContactNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_working_date", user.LastWorkingDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@role_id", user.RoleId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", user.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@other_id", user.OtherId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@machine_id", user.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_code", user.TillCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_active", user.TillActive);
                    cmd.Parameters.AddWithValue("@photo", user.Photo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Authentication_Code", user.AuthenticationCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Function_type_id", user.FunctionTypeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_trainee", user.IsTrainee);
                    cmd.Parameters.AddWithValue("@from_date", user.FromDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@to_date", user.ToDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", user.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserUUID", user.UserUUID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("User added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/UserList/UpdateUser/{id}
        [HttpPut("UpdateUser/{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@staff_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", user.CompanyId);
                    cmd.Parameters.AddWithValue("@staff_code", user.StaffCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", user.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@joining_date", user.JoiningDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@branch_id", user.BranchId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@department_id", user.DepartmentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@designation_id", user.DesignationId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", user.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@country_id", user.CountryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@state_id", user.StateId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city_id", user.CityId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@national_id", user.NationalId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@contact_no", user.ContactNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_working_date", user.LastWorkingDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@role_id", user.RoleId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", user.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@other_id", user.OtherId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@machine_id", user.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_code", user.TillCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@till_active", user.TillActive);
                    cmd.Parameters.AddWithValue("@photo", user.Photo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Authentication_Code", user.AuthenticationCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Function_type_id", user.FunctionTypeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_trainee", user.IsTrainee);
                    cmd.Parameters.AddWithValue("@from_date", user.FromDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@to_date", user.ToDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", user.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserUUID", user.UserUUID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"User with ID {id} not found");
                    }
                }

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/UserList/DeleteUser/{id}
        [HttpDelete("DeleteUser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@staff_id", id);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"User with ID {id} not found");
                    }
                }

                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 