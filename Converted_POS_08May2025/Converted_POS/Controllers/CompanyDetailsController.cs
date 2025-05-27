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
    public class CompanyDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CompanyDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/CompanyDetails/GetCompany/{id}
        [HttpGet("GetCompany/{id}")]
        public ActionResult<CompanyModel> GetCompany(int id)
        {
            try
            {
                CompanyModel company = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Company", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Company_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        company = new CompanyModel
                        {
                            CompanyId = id,
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            StartingDate = reader["Starting_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Starting_Date"]) : (DateTime?)null,
                            Domain = reader["Domain"].ToString(),
                            CountryId = reader["Country"] != DBNull.Value ? Convert.ToInt32(reader["Country"]) : (int?)null,
                            StateId = reader["State"] != DBNull.Value ? Convert.ToInt32(reader["State"]) : (int?)null,
                            CityId = reader["City"] != DBNull.Value ? Convert.ToInt32(reader["City"]) : (int?)null,
                            LiteVersion = reader["ProductType"] != DBNull.Value ? Convert.ToInt32(reader["ProductType"]) : (int?)null,
                            Logo = reader["Logo"] != DBNull.Value ? (byte[])reader["Logo"] : null,
                            Code = reader["Code"].ToString(),
                            Email = reader["Email"].ToString(),
                            Description = reader["Description"].ToString(),
                            Postal = reader["Postal"].ToString(),
                            Website = reader["Website"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            RegistrationNo = reader["Registration_no"] != DBNull.Value ? Convert.ToInt32(reader["Registration_no"]) : (int?)null,
                            GSTVAT = reader["GST_VAT"] != DBNull.Value ? Convert.ToInt32(reader["GST_VAT"]) : (int?)null,
                            CSTVAT = reader["CST_VAT"] != DBNull.Value ? Convert.ToInt32(reader["CST_VAT"]) : (int?)null,
                            ServiceTaxNo = reader["Service_tax_no"] != DBNull.Value ? Convert.ToInt32(reader["Service_tax_no"]) : (int?)null,
                            PanNo = reader["Pan_no"] != DBNull.Value ? Convert.ToInt32(reader["Pan_no"]) : (int?)null,
                            BranchName = reader["Branch_Name"].ToString(),
                            Synchronization = reader["Synchronization"].ToString(),
                            VenueName = reader["Venue_Name"].ToString(),
                            VatNo = reader["Vat_No"].ToString(),
                            ReceiptHeader = reader["Receipt_Header"].ToString(),
                            ReceiptFooter = reader["Receipt_Footer"].ToString(),
                            LogOffTime = reader["log_off_time"] != DBNull.Value ? Convert.ToInt32(reader["log_off_time"]) : (int?)null,
                            ParSaleParOperator = reader["par_sale_par_operator"] != DBNull.Value ? Convert.ToInt32(reader["par_sale_par_operator"]) : (int?)null,
                            CurrencyId = reader["Currency_Id"] != DBNull.Value ? Convert.ToInt32(reader["Currency_Id"]) : (int?)null,
                            JudoId = reader["judo_id"].ToString(),
                            JudoToken = reader["judo_token"].ToString(),
                            JudoSecret = reader["judo_secret"].ToString(),
                            WeekStartDay = reader["week_start_day"].ToString(),
                            ShowChips = reader["show_chips"] != DBNull.Value ? Convert.ToInt32(reader["show_chips"]) : (int?)null,
                            BusinessDoneBy = reader["BusinessDoneBy"] != DBNull.Value ? Convert.ToInt32(reader["BusinessDoneBy"]) : (int?)null,
                            DisplayDeclaration = reader["Display_declaration"] != DBNull.Value ? Convert.ToInt32(reader["Display_declaration"]) : (int?)null,
                            CheckDuration = reader["chk_duration"] != DBNull.Value ? Convert.ToInt32(reader["chk_duration"]) : (int?)null,
                            IsAddTax2 = reader["IsAddTax2"] != DBNull.Value ? Convert.ToInt32(reader["IsAddTax2"]) : (int?)null,
                            IsExclusiveTax = reader["IsExclusiveTax"] != DBNull.Value ? Convert.ToInt32(reader["IsExclusiveTax"]) : (int?)null,
                            IsPaymentGateway = reader["IsPaymentGtway"] != DBNull.Value ? Convert.ToInt32(reader["IsPaymentGtway"]) : (int?)null
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

        // PUT: api/CompanyDetails/UpdateCompany/{id}
        [HttpPut("UpdateCompany/{id}")]
        public ActionResult UpdateCompany(int id, [FromBody] CompanyModel company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Company data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_M_Company", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Company_id", id);
                    cmd.Parameters.AddWithValue("@Name", company.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", company.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Starting_Date", company.StartingDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Domain", company.Domain ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", company.CountryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", company.StateId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", company.CityId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProductType", company.LiteVersion ?? (object)DBNull.Value);
                    if (company.Logo != null)
                    {
                        cmd.Parameters.AddWithValue("@Logo", company.Logo);
                    }
                    cmd.Parameters.AddWithValue("@Code", company.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", company.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", company.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Postal", company.Postal ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Website", company.Website ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", company.Contact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Registration_no", company.RegistrationNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@GST_VAT", company.GSTVAT ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CST_VAT", company.CSTVAT ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Service_tax_no", company.ServiceTaxNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pan_no", company.PanNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Branch_Name", company.BranchName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Synchronization", company.Synchronization ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Venue_Name", company.VenueName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vat_No", company.VatNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Receipt_Header", company.ReceiptHeader ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Receipt_Footer", company.ReceiptFooter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@log_off_time", company.LogOffTime ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@par_sale_par_operator", company.ParSaleParOperator ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Currency_Id", company.CurrencyId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@judo_id", company.JudoId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@judo_token", company.JudoToken ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@judo_secret", company.JudoSecret ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@week_start_day", company.WeekStartDay ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@show_chips", company.ShowChips ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BusinessDoneBy", company.BusinessDoneBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Display_declaration", company.DisplayDeclaration ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@chk_duration", company.CheckDuration ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsAddTax2", company.IsAddTax2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsExclusiveTax", company.IsExclusiveTax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsPaymentGtway", company.IsPaymentGateway ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IP_Address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@Login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

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
    }
} 