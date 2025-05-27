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
    public class VenueListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public VenueListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/VenueList/GetVenues/{companyId}
        [HttpGet("GetVenues/{companyId}")]
        public ActionResult<IEnumerable<VenueModel>> GetVenues(int companyId)
        {
            try
            {
                var venues = new List<VenueModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var venue = new VenueModel
                        {
                            VenueId = Convert.ToInt32(reader["venue_id"]),
                            CompanyId = companyId,
                            VenueName = reader["venue_name"].ToString(),
                            Description = reader["description"].ToString(),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null,
                            MacId = reader["mac_id"].ToString(),
                            PrintReceipt = reader["print_receipt"] != DBNull.Value && Convert.ToBoolean(reader["print_receipt"]),
                            PrintDuplicateReceipt = reader["print_duplicate_receipt"] != DBNull.Value && Convert.ToBoolean(reader["print_duplicate_receipt"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            DateTime = reader["date_time"]?.ToString(),
                            GroupBy = reader["group_by"] != DBNull.Value && Convert.ToBoolean(reader["group_by"]),
                            ConsileDate = reader["consile_date"] != DBNull.Value && Convert.ToBoolean(reader["consile_date"]),
                            GroupByWith = reader["group_by_with"] != DBNull.Value ? Convert.ToInt32(reader["group_by_with"]) : (int?)null
                        };
                        venues.Add(venue);
                    }
                }

                return Ok(venues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VenueList/GetVenueById/{id}/{companyId}
        [HttpGet("GetVenueById/{id}/{companyId}")]
        public ActionResult<VenueModel> GetVenueById(int id, int companyId)
        {
            try
            {
                VenueModel venue = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@venue_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        venue = new VenueModel
                        {
                            VenueId = id,
                            CompanyId = companyId,
                            VenueName = reader["venue_name"].ToString(),
                            Description = reader["description"].ToString(),
                            SortingNo = reader["sorting_no"] != DBNull.Value ? Convert.ToInt32(reader["sorting_no"]) : (int?)null,
                            MacId = reader["mac_id"].ToString(),
                            PrintReceipt = reader["print_receipt"] != DBNull.Value && Convert.ToBoolean(reader["print_receipt"]),
                            PrintDuplicateReceipt = reader["print_duplicate_receipt"] != DBNull.Value && Convert.ToBoolean(reader["print_duplicate_receipt"]),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            DateTime = reader["date_time"]?.ToString(),
                            GroupBy = reader["group_by"] != DBNull.Value && Convert.ToBoolean(reader["group_by"]),
                            ConsileDate = reader["consile_date"] != DBNull.Value && Convert.ToBoolean(reader["consile_date"]),
                            GroupByWith = reader["group_by_with"] != DBNull.Value ? Convert.ToInt32(reader["group_by_with"]) : (int?)null
                        };
                    }
                }

                if (venue == null)
                {
                    return NotFound($"Venue with ID {id} not found");
                }

                return Ok(venue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VenueList/GetActiveVenues/{companyId}
        [HttpGet("GetActiveVenues/{companyId}")]
        public ActionResult<IEnumerable<VenueModel>> GetActiveVenues(int companyId)
        {
            try
            {
                var venues = new List<VenueModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp_active", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var venue = new VenueModel
                        {
                            VenueId = Convert.ToInt32(reader["venue_id"]),
                            CompanyId = companyId,
                            VenueName = reader["venue_name"].ToString(),
                            IsActive = true
                        };
                        venues.Add(venue);
                    }
                }

                return Ok(venues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VenueList/AddVenue
        [HttpPost("AddVenue")]
        public ActionResult AddVenue([FromBody] VenueModel venue)
        {
            try
            {
                if (venue == null)
                {
                    return BadRequest("Venue data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@venue_id", 0); // New venue
                    cmd.Parameters.AddWithValue("@cmp_id", venue.CompanyId);
                    cmd.Parameters.AddWithValue("@venue_name", venue.VenueName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@description", venue.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", venue.SortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mac_id", venue.MacId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@print_receipt", venue.PrintReceipt);
                    cmd.Parameters.AddWithValue("@print_duplicate_receipt", venue.PrintDuplicateReceipt);
                    cmd.Parameters.AddWithValue("@machine_id", venue.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", venue.IsActive);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@date_time", venue.DateTime ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@group_by", venue.GroupBy);
                    cmd.Parameters.AddWithValue("@consile_date", venue.ConsileDate);
                    cmd.Parameters.AddWithValue("@group_by_with", venue.GroupByWith ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Venue added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/VenueList/UpdateVenue/{id}
        [HttpPut("UpdateVenue/{id}")]
        public ActionResult UpdateVenue(int id, [FromBody] VenueModel venue)
        {
            try
            {
                if (venue == null)
                {
                    return BadRequest("Venue data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@venue_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", venue.CompanyId);
                    cmd.Parameters.AddWithValue("@venue_name", venue.VenueName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@description", venue.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@sorting_no", venue.SortingNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mac_id", venue.MacId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@print_receipt", venue.PrintReceipt);
                    cmd.Parameters.AddWithValue("@print_duplicate_receipt", venue.PrintDuplicateReceipt);
                    cmd.Parameters.AddWithValue("@machine_id", venue.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", venue.IsActive);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@date_time", venue.DateTime ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@group_by", venue.GroupBy);
                    cmd.Parameters.AddWithValue("@consile_date", venue.ConsileDate);
                    cmd.Parameters.AddWithValue("@group_by_with", venue.GroupByWith ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Venue with ID {id} not found");
                    }
                }

                return Ok("Venue updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/VenueList/DeleteVenue/{id}
        [HttpDelete("DeleteVenue/{id}")]
        public ActionResult DeleteVenue(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@venue_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", 0);
                    cmd.Parameters.AddWithValue("@venue_name", "");
                    cmd.Parameters.AddWithValue("@description", "");
                    cmd.Parameters.AddWithValue("@sorting_no", 0);
                    cmd.Parameters.AddWithValue("@mac_id", "");
                    cmd.Parameters.AddWithValue("@print_receipt", 0);
                    cmd.Parameters.AddWithValue("@print_duplicate_receipt", 0);
                    cmd.Parameters.AddWithValue("@machine_id", 0);
                    cmd.Parameters.AddWithValue("@is_active", 0);
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@date_time", "");
                    cmd.Parameters.AddWithValue("@group_by", 0);
                    cmd.Parameters.AddWithValue("@consile_date", 0);
                    cmd.Parameters.AddWithValue("@group_by_with", 0);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Venue with ID {id} not found");
                    }
                }

                return Ok("Venue deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 