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
    public class TaxListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TaxListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/TaxList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<TaxModel>> GetAllTaxes()
        {
            try
            {
                var taxes = new List<TaxModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Taxes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tax = new TaxModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            Rate = Convert.ToDecimal(reader["rate"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            IsIncluded = Convert.ToBoolean(reader["is_included"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
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

        // GET: api/TaxList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<TaxModel>> GetTaxesByCompanyId(int companyId)
        {
            try
            {
                var taxes = new List<TaxModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Taxes_By_Company_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@company_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tax = new TaxModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            Rate = Convert.ToDecimal(reader["rate"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            IsIncluded = Convert.ToBoolean(reader["is_included"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
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

        // GET: api/TaxList/GetByLocationId/{locationId}
        [HttpGet("GetByLocationId/{locationId}")]
        public ActionResult<IEnumerable<TaxModel>> GetTaxesByLocationId(int locationId)
        {
            try
            {
                var taxes = new List<TaxModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Taxes_By_Location_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@location_id", locationId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tax = new TaxModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            Rate = Convert.ToDecimal(reader["rate"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            IsIncluded = Convert.ToBoolean(reader["is_included"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
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

        // GET: api/TaxList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<TaxModel> GetTaxById(int id)
        {
            try
            {
                TaxModel tax = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Tax_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        tax = new TaxModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            Name = reader["name"].ToString(),
                            Rate = Convert.ToDecimal(reader["rate"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            IsIncluded = Convert.ToBoolean(reader["is_included"]),
                            IsDefault = Convert.ToBoolean(reader["is_default"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null
                        };
                    }
                }

                if (tax == null)
                {
                    return NotFound($"Tax with ID {id} not found");
                }

                return Ok(tax);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/TaxList/Add
        [HttpPost("Add")]
        public ActionResult AddTax([FromBody] TaxModel tax)
        {
            try
            {
                if (tax == null)
                {
                    return BadRequest("Tax data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Tax", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@company_id", tax.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", tax.LocationId);
                    cmd.Parameters.AddWithValue("@name", tax.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@rate", tax.Rate);
                    cmd.Parameters.AddWithValue("@is_active", tax.IsActive);
                    cmd.Parameters.AddWithValue("@is_included", tax.IsIncluded);
                    cmd.Parameters.AddWithValue("@is_default", tax.IsDefault);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Tax added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/TaxList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateTax(int id, [FromBody] TaxModel tax)
        {
            try
            {
                if (tax == null)
                {
                    return BadRequest("Tax data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Tax", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@company_id", tax.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", tax.LocationId);
                    cmd.Parameters.AddWithValue("@name", tax.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@rate", tax.Rate);
                    cmd.Parameters.AddWithValue("@is_active", tax.IsActive);
                    cmd.Parameters.AddWithValue("@is_included", tax.IsIncluded);
                    cmd.Parameters.AddWithValue("@is_default", tax.IsDefault);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Tax with ID {id} not found");
                    }
                }

                return Ok("Tax updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/TaxList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteTax(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Tax", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Tax with ID {id} not found");
                    }
                }

                return Ok("Tax deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 