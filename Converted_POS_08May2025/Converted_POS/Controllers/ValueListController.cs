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
    public class ValueListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _controllerConnectionString;

        public ValueListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
            _controllerConnectionString = _configuration.GetConnectionString("POSControllerConnectionString");
        }

        // GET: api/ValueList/GetCountries
        [HttpGet("GetCountries")]
        public ActionResult<IEnumerable<CountryModel>> GetCountries()
        {
            try
            {
                var countries = new List<CountryModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Countries", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var country = new CountryModel
                        {
                            CountryId = Convert.ToInt32(reader["Country_id"]),
                            CountryName = reader["CountryName"].ToString(),
                            CountryCode = reader["CountryCode"].ToString()
                        };
                        countries.Add(country);
                    }
                }

                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ValueList/GetStates/{countryId}
        [HttpGet("GetStates/{countryId}")]
        public ActionResult<IEnumerable<StateModel>> GetStates(int countryId)
        {
            try
            {
                var states = new List<StateModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_States", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@country_id", countryId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var state = new StateModel
                        {
                            StateId = Convert.ToInt32(reader["State_Id"]),
                            StateName = reader["StateName"].ToString(),
                            CountryId = countryId
                        };
                        states.Add(state);
                    }
                }

                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ValueList/GetCities/{stateId}
        [HttpGet("GetCities/{stateId}")]
        public ActionResult<IEnumerable<CityModel>> GetCities(int stateId)
        {
            try
            {
                var cities = new List<CityModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_City", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_id", stateId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var city = new CityModel
                        {
                            CityId = Convert.ToInt32(reader["City_id"]),
                            CityName = reader["CityName"].ToString(),
                            StateId = stateId
                        };
                        cities.Add(city);
                    }
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ValueList/GetVenues/{companyId}
        [HttpGet("GetVenues/{companyId}")]
        public ActionResult<IEnumerable<VenueModel>> GetVenues(int companyId)
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
                            VenueName = reader["venue_name"].ToString(),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes"
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

        // GET: api/ValueList/GetTaxes/{companyId}
        [HttpGet("GetTaxes/{companyId}")]
        public ActionResult<IEnumerable<TaxModel>> GetTaxes(int companyId)
        {
            try
            {
                var taxes = new List<TaxModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Tax_List", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tax = new TaxModel
                        {
                            TaxId = Convert.ToInt32(reader["tax_id"]),
                            Name = reader["name"].ToString(),
                            TaxValue = Convert.ToDecimal(reader["tax_value"]),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes"
                        };
                        taxes.Add(tax);
                    }
                }

                return Ok(taxes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ValueList/GetDepartments/{companyId}
        [HttpGet("GetDepartments/{companyId}")]
        public ActionResult<IEnumerable<DepartmentModel>> GetDepartments(int companyId)
        {
            try
            {
                var departments = new List<DepartmentModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Department_List", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var department = new DepartmentModel
                        {
                            DepartmentId = Convert.ToInt32(reader["dept_id"]),
                            Name = reader["dept_name"].ToString(),
                            Code = reader["Code"].ToString(),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes"
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

        // GET: api/ValueList/GetCourses/{companyId}
        [HttpGet("GetCourses/{companyId}")]
        public ActionResult<IEnumerable<CourseModel>> GetCourses(int companyId)
        {
            try
            {
                var courses = new List<CourseModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Course_List", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var course = new CourseModel
                        {
                            CourseId = Convert.ToInt32(reader["course_id"]),
                            Name = reader["name"].ToString(),
                            DisplayOrder = Convert.ToInt32(reader["display_order"]),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes"
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

        // GET: api/ValueList/GetStaff/{companyId}
        [HttpGet("GetStaff/{companyId}")]
        public ActionResult<IEnumerable<StaffModel>> GetStaff(int companyId)
        {
            try
            {
                var staffList = new List<StaffModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Staff_List", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var staff = new StaffModel
                        {
                            StaffId = Convert.ToInt32(reader["staff_id"]),
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Mobile = reader["mobile"].ToString(),
                            UserName = reader["username"].ToString(),
                            CompanyId = companyId,
                            IsActive = reader["Active"].ToString() == "Yes"
                        };
                        staffList.Add(staff);
                    }
                }

                return Ok(staffList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 