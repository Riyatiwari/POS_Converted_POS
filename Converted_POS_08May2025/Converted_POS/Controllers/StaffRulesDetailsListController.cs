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
    public class StaffRulesDetailsListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StaffRulesDetailsListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/StaffRulesDetailsList/GetStaffRulesDetails/{staffRuleMasterId}
        [HttpGet("GetStaffRulesDetails/{staffRuleMasterId}")]
        public ActionResult<IEnumerable<StaffRulesDetailModel>> GetStaffRulesDetails(int staffRuleMasterId)
        {
            try
            {
                var staffRulesDetails = new List<StaffRulesDetailModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Function_by_staff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@m_staff_id", staffRuleMasterId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var staffRuleDetail = new StaffRulesDetailModel
                        {
                            MStaffId = staffRuleMasterId,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            FunctionTypeId = Convert.ToInt32(reader["Function_type_id"]),
                            FunctionName = reader["f_name"].ToString()
                        };
                        staffRulesDetails.Add(staffRuleDetail);
                    }
                }

                return Ok(staffRulesDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/StaffRulesDetailsList/AddStaffRulesDetail
        [HttpPost("AddStaffRulesDetail")]
        public ActionResult AddStaffRulesDetail([FromBody] StaffRulesDetailModel staffRuleDetail)
        {
            try
            {
                if (staffRuleDetail == null)
                {
                    return BadRequest("Staff Rule Detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff_rules_detail", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@m_staff_id", staffRuleDetail.MStaffId);
                    cmd.Parameters.AddWithValue("@cmp_id", staffRuleDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@Function_type_id", staffRuleDetail.FunctionTypeId);
                    cmd.Parameters.AddWithValue("@f_name", staffRuleDetail.FunctionName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Staff Rule Detail added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/StaffRulesDetailsList/DeleteStaffRulesDetail
        [HttpDelete("DeleteStaffRulesDetail")]
        public ActionResult DeleteStaffRulesDetail([FromBody] StaffRulesDetailModel staffRuleDetail)
        {
            try
            {
                if (staffRuleDetail == null)
                {
                    return BadRequest("Staff Rule Detail data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Staff_rules_detail", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@m_staff_id", staffRuleDetail.MStaffId);
                    cmd.Parameters.AddWithValue("@cmp_id", staffRuleDetail.CompanyId);
                    cmd.Parameters.AddWithValue("@Function_type_id", staffRuleDetail.FunctionTypeId);
                    cmd.Parameters.AddWithValue("@f_name", staffRuleDetail.FunctionName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound("Staff Rule Detail not found");
                    }
                }

                return Ok("Staff Rule Detail deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/StaffRulesDetailsList/GetFunctions/{companyId}
        [HttpGet("GetFunctions/{companyId}")]
        public ActionResult<IEnumerable<object>> GetFunctions(int companyId)
        {
            try
            {
                var functions = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Function_By_Cmp", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var function = new
                        {
                            FunctionTypeId = Convert.ToInt32(reader["function_id"]),
                            Name = reader["name"].ToString()
                        };
                        functions.Add(function);
                    }
                }

                return Ok(functions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 