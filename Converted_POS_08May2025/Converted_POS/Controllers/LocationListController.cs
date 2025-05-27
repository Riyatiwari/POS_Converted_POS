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
    public class LocationListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LocationListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/LocationList/GetLocations/{companyId}
        [HttpGet("GetLocations/{companyId}")]
        public ActionResult<IEnumerable<StoreModel>> GetLocations(int companyId)
        {
            try
            {
                var locations = new List<StoreModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Location", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var location = new StoreModel
                        {
                            StoreId = Convert.ToInt32(reader["location_id"]),
                            CompanyId = companyId,
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            IsActive = reader["Active"].ToString() == "Yes",
                            City = reader["cname"].ToString(),
                            VenueName = reader["venue_name"].ToString(),
                            TillAutoLogoff = reader["till_auto_log_off"].ToString() == "Yes",
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null,
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString()
                        };
                        locations.Add(location);
                    }
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LocationList/GetLocationById/{locationId}
        [HttpGet("GetLocationById/{locationId}")]
        public ActionResult<StoreModel> GetLocationById(int locationId)
        {
            try
            {
                StoreModel location = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Location_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        location = new StoreModel
                        {
                            StoreId = locationId,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            IsActive = reader["Active"].ToString() == "Yes",
                            City = reader["cname"].ToString(),
                            VenueName = reader["venue_name"].ToString(),
                            TillAutoLogoff = reader["till_auto_log_off"].ToString() == "Yes",
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            VenueId = reader["venue_id"] != DBNull.Value ? Convert.ToInt32(reader["venue_id"]) : (int?)null,
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            HeaderReceipt = reader["header_reciept"].ToString(),
                            BackgroundColor = reader["BG_Color"].ToString(),
                            FontColor = reader["Font_Color"].ToString(),
                            BodyColor = reader["Body_Color"].ToString(),
                            Theme = reader["theme"].ToString()
                        };
                    }
                }

                if (location == null)
                {
                    return NotFound($"Location with ID {locationId} not found");
                }

                return Ok(location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LocationList/GetLocationsByVenue/{venueId}
        [HttpGet("GetLocationsByVenue/{venueId}")]
        public ActionResult<IEnumerable<StoreModel>> GetLocationsByVenue(int venueId)
        {
            try
            {
                var locations = new List<StoreModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Locations_By_Venue", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@venue_id", venueId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var location = new StoreModel
                        {
                            StoreId = Convert.ToInt32(reader["location_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            IsActive = reader["Active"].ToString() == "Yes",
                            VenueId = venueId,
                            VenueName = reader["venue_name"].ToString(),
                            TillAutoLogoff = reader["till_auto_log_off"].ToString() == "Yes"
                        };
                        locations.Add(location);
                    }
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LocationList/GetActiveLocations/{companyId}
        [HttpGet("GetActiveLocations/{companyId}")]
        public ActionResult<IEnumerable<StoreModel>> GetActiveLocations(int companyId)
        {
            try
            {
                var locations = new List<StoreModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Active_Locations", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var location = new StoreModel
                        {
                            StoreId = Convert.ToInt32(reader["location_id"]),
                            CompanyId = companyId,
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            IsActive = true
                        };
                        locations.Add(location);
                    }
                }

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 