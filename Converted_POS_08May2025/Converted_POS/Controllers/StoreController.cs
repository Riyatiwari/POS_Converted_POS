using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Converted_POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StoreController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/Store/GetStores
        [HttpGet("GetStores")]
        public ActionResult<IEnumerable<StoreModel>> GetStores()
        {
            try
            {
                var stores = new List<StoreModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Stores", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var store = new StoreModel
                        {
                            StoreId = Convert.ToInt32(reader["location_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null
                        };
                        stores.Add(store);
                    }
                }

                return Ok(stores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Store/GetStoreById/{id}
        [HttpGet("GetStoreById/{id}")]
        public ActionResult<StoreModel> GetStoreById(int id)
        {
            try
            {
                StoreModel store = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Store_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        store = new StoreModel
                        {
                            StoreId = Convert.ToInt32(reader["location_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null
                        };
                    }
                }

                if (store == null)
                {
                    return NotFound($"Store with ID {id} not found");
                }

                return Ok(store);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Store/AddStore
        [HttpPost("AddStore")]
        public ActionResult AddStore([FromBody] StoreModel store)
        {
            try
            {
                if (store == null)
                {
                    return BadRequest("Store data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Store", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cmp_id", store.CompanyId);
                    cmd.Parameters.AddWithValue("@code", store.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", store.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", store.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", store.IsActive);
                    cmd.Parameters.AddWithValue("@Email", store.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", store.Contact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", store.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Store added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Store/UpdateStore/{id}
        [HttpPut("UpdateStore/{id}")]
        public ActionResult UpdateStore(int id, [FromBody] StoreModel store)
        {
            try
            {
                if (store == null)
                {
                    return BadRequest("Store data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Store", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@location_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", store.CompanyId);
                    cmd.Parameters.AddWithValue("@code", store.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@name", store.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", store.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", store.IsActive);
                    cmd.Parameters.AddWithValue("@Email", store.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", store.Contact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@venue_id", store.VenueId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Store with ID {id} not found");
                    }
                }

                return Ok("Store updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Store/DeleteStore/{id}
        [HttpDelete("DeleteStore/{id}")]
        public ActionResult DeleteStore(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Store", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@location_id", id);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Store with ID {id} not found");
                    }
                }

                return Ok("Store deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 