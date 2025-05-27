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
    public class CompanyListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _controllerConnectionString;

        public CompanyListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
            _controllerConnectionString = _configuration.GetConnectionString("POSControllerConnectionString");
        }

        // GET: api/CompanyList/GetCompanies
        [HttpGet("GetCompanies")]
        public ActionResult<IEnumerable<CompanyModel>> GetCompanies()
        {
            try
            {
                var companies = new List<CompanyModel>();

                using (var connection = new SqlConnection(_controllerConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Companies", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var company = new CompanyModel
                        {
                            CompanyId = Convert.ToInt32(reader["Company_id"]),
                            Name = reader["Name"].ToString(),
                            Domain = reader["Domain"].ToString(),
                            Code = reader["Code"].ToString(),
                            VatNo = reader["Vat_No"].ToString(),
                            StartingDate = reader["Starting_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Starting_Date"]) : DateTime.MinValue,
                            IsActive = reader["Active"] != DBNull.Value && Convert.ToBoolean(reader["Active"]),
                            Country = reader["Country"].ToString(),
                            State = reader["State"].ToString(),
                            City = reader["City"].ToString(),
                            ReceiptHeader = reader["Receipt_Header"].ToString()
                        };
                        companies.Add(company);
                    }
                }

                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CompanyList/GetCompanyById/{id}
        [HttpGet("GetCompanyById/{id}")]
        public ActionResult<CompanyModel> GetCompanyById(int id)
        {
            try
            {
                CompanyModel company = null;

                using (var connection = new SqlConnection(_controllerConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Company_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Company_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        company = new CompanyModel
                        {
                            CompanyId = Convert.ToInt32(reader["Company_id"]),
                            Name = reader["Name"].ToString(),
                            Domain = reader["Domain"].ToString(),
                            Code = reader["Code"].ToString(),
                            VatNo = reader["Vat_No"].ToString(),
                            StartingDate = reader["Starting_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Starting_Date"]) : DateTime.MinValue,
                            IsActive = reader["Active"] != DBNull.Value && Convert.ToBoolean(reader["Active"]),
                            Country = reader["Country"].ToString(),
                            State = reader["State"].ToString(),
                            City = reader["City"].ToString(),
                            ReceiptHeader = reader["Receipt_Header"].ToString()
                        };
                    }
                }

                if (company == null)
                {
                    return NotFound($"Company with ID {id} not found");
                }

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/CompanyList/AddCompany
        [HttpPost("AddCompany")]
        public ActionResult AddCompany([FromBody] CompanyModel company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Company data is null");
                }

                // Check if domain exists
                if (IsDomainExists(company.Domain))
                {
                    return BadRequest("Domain name already exists. Please use another domain.");
                }

                using (var connection = new SqlConnection(_controllerConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Company", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Company_id", 0); // New company
                    cmd.Parameters.AddWithValue("@Name", company.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Domain", company.Domain ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code", company.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vat_No", company.VatNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Receipt_Header", company.ReceiptHeader ?? (object)DBNull.Value);
                    
                    var startingDate = company.StartingDate;
                    if (startingDate == DateTime.MinValue)
                    {
                        startingDate = DateTime.Now;
                    }
                    cmd.Parameters.Add("@Starting_Date", SqlDbType.DateTime).Value = startingDate;
                    
                    cmd.Parameters.AddWithValue("@Country", company.Country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", company.State ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", company.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IP_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Company added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/CompanyList/UpdateCompany/{id}
        [HttpPut("UpdateCompany/{id}")]
        public ActionResult UpdateCompany(int id, [FromBody] CompanyModel company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Company data is null");
                }

                // Check if domain exists for other companies
                if (IsDomainExistsForOtherCompany(company.Domain, id))
                {
                    return BadRequest("Domain name already exists for another company. Please use another domain.");
                }

                using (var connection = new SqlConnection(_controllerConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Company", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Company_id", id);
                    cmd.Parameters.AddWithValue("@Name", company.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Domain", company.Domain ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code", company.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vat_No", company.VatNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Receipt_Header", company.ReceiptHeader ?? (object)DBNull.Value);
                    
                    var startingDate = company.StartingDate;
                    if (startingDate == DateTime.MinValue)
                    {
                        startingDate = DateTime.Now;
                    }
                    cmd.Parameters.Add("@Starting_Date", SqlDbType.DateTime).Value = startingDate;
                    
                    cmd.Parameters.AddWithValue("@Country", company.Country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", company.State ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", company.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IP_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Company with ID {id} not found");
                    }
                }

                return Ok("Company updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/CompanyList/DeleteCompany/{id}
        [HttpDelete("DeleteCompany/{id}")]
        public ActionResult DeleteCompany(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_controllerConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Company", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Company_id", id);
                    cmd.Parameters.AddWithValue("@IP_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Company with ID {id} not found");
                    }
                }

                return Ok("Company deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool IsDomainExists(string domain)
        {
            using (var connection = new SqlConnection(_controllerConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM M_Company WHERE Domain = @Domain", connection);
                cmd.Parameters.AddWithValue("@Domain", domain);

                connection.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }

        private bool IsDomainExistsForOtherCompany(string domain, int companyId)
        {
            using (var connection = new SqlConnection(_controllerConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM M_Company WHERE Domain = @Domain AND Company_id <> @Company_id", connection);
                cmd.Parameters.AddWithValue("@Domain", domain);
                cmd.Parameters.AddWithValue("@Company_id", companyId);

                connection.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
    }
} 