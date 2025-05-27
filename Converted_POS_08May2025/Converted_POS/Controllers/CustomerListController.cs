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
    public class CustomerListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CustomerListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/CustomerList/GetCustomers/{companyId}
        [HttpGet("GetCustomers/{companyId}")]
        public ActionResult<IEnumerable<CustomerModel>> GetCustomers(int companyId)
        {
            try
            {
                var customers = new List<CustomerModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var customer = new CustomerModel
                        {
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            CompanyId = companyId,
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = reader["email"].ToString(),
                            ContactNo = reader["contact_no"].ToString(),
                            AlternetContact = reader["Alternet_contact"].ToString(),
                            Address = reader["address"].ToString(),
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            PostalCode = reader["postal_code"].ToString(),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            ModifyDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            OtherId = reader["other_id"].ToString(),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            ProfileId = reader["profile_id"] != DBNull.Value ? Convert.ToInt32(reader["profile_id"]) : (int?)null,
                            AccountId = reader["AccountID"] != DBNull.Value ? Convert.ToInt32(reader["AccountID"]) : (int?)null,
                            IsCredit = reader["Is_credit"] != DBNull.Value && Convert.ToBoolean(reader["Is_credit"]),
                            PriceLevel = reader["price_level"] != DBNull.Value ? Convert.ToInt32(reader["price_level"]) : (int?)null
                        };
                        customers.Add(customer);
                    }
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CustomerList/GetCustomerById/{id}
        [HttpGet("GetCustomerById/{id}")]
        public ActionResult<CustomerModel> GetCustomerById(int id)
        {
            try
            {
                CustomerModel customer = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new CustomerModel
                        {
                            CustomerId = id,
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = reader["email"].ToString(),
                            ContactNo = reader["contact_no"].ToString(),
                            AlternetContact = reader["Alternet_contact"].ToString(),
                            Address = reader["address"].ToString(),
                            CountryId = reader["country_id"] != DBNull.Value ? Convert.ToInt32(reader["country_id"]) : (int?)null,
                            StateId = reader["state_id"] != DBNull.Value ? Convert.ToInt32(reader["state_id"]) : (int?)null,
                            CityId = reader["city_id"] != DBNull.Value ? Convert.ToInt32(reader["city_id"]) : (int?)null,
                            PostalCode = reader["postal_code"].ToString(),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            ModifyDate = reader["modify_date"] != DBNull.Value ? Convert.ToDateTime(reader["modify_date"]) : (DateTime?)null,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]),
                            OtherId = reader["other_id"].ToString(),
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null,
                            ProfileId = reader["profile_id"] != DBNull.Value ? Convert.ToInt32(reader["profile_id"]) : (int?)null,
                            AccountId = reader["AccountID"] != DBNull.Value ? Convert.ToInt32(reader["AccountID"]) : (int?)null,
                            IsCredit = reader["Is_credit"] != DBNull.Value && Convert.ToBoolean(reader["Is_credit"]),
                            CardNumber = reader["CardNumber"]?.ToString(),
                            ExpDate = reader["ExpDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpDate"]) : (DateTime?)null,
                            CustomerProfile = reader["CustomerProfile"] != DBNull.Value ? (byte[])reader["CustomerProfile"] : null,
                            PriceLevel = reader["price_level"] != DBNull.Value ? Convert.ToInt32(reader["price_level"]) : (int?)null
                        };
                    }
                }

                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CustomerList/GetCustomerByEmail/{email}
        [HttpGet("GetCustomerByEmail/{email}")]
        public ActionResult<CustomerModel> GetCustomerByEmail(string email)
        {
            try
            {
                CustomerModel customer = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Customer_email", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new CustomerModel
                        {
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = email,
                            ContactNo = reader["contact_no"].ToString(),
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                    }
                }

                if (customer == null)
                {
                    return NotFound($"Customer with email {email} not found");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/CustomerList/GetCustomerByContact/{contact}
        [HttpGet("GetCustomerByContact/{contact}")]
        public ActionResult<CustomerModel> GetCustomerByContact(string contact)
        {
            try
            {
                CustomerModel customer = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@contact_no", contact);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new CustomerModel
                        {
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            CompanyId = Convert.ToInt32(reader["cmp_id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = reader["email"].ToString(),
                            ContactNo = contact,
                            IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                        };
                    }
                }

                if (customer == null)
                {
                    return NotFound($"Customer with contact {contact} not found");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/CustomerList/AddCustomer
        [HttpPost("AddCustomer")]
        public ActionResult AddCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customer_id", 0); // New customer
                    cmd.Parameters.AddWithValue("@cmp_id", customer.CompanyId);
                    cmd.Parameters.AddWithValue("@first_name", customer.FirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_name", customer.LastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", customer.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@contact_no", customer.ContactNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Alternet_contact", customer.AlternetContact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", customer.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@country_id", customer.CountryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@state_id", customer.StateId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city_id", customer.CityId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@postal_code", customer.PostalCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", customer.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@other_id", customer.OtherId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@machine_id", customer.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@profile_id", customer.ProfileId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AccountID", customer.AccountId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                    cmd.Parameters.AddWithValue("@Price_level_id", customer.PriceLevel ?? (object)DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Customer added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/CustomerList/UpdateCustomer/{id}
        [HttpPut("UpdateCustomer/{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customer_id", id);
                    cmd.Parameters.AddWithValue("@cmp_id", customer.CompanyId);
                    cmd.Parameters.AddWithValue("@first_name", customer.FirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_name", customer.LastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", customer.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@contact_no", customer.ContactNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Alternet_contact", customer.AlternetContact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", customer.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@country_id", customer.CountryId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@state_id", customer.StateId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city_id", customer.CityId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@postal_code", customer.PostalCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", customer.IsActive);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@other_id", customer.OtherId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@machine_id", customer.MachineId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@profile_id", customer.ProfileId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AccountID", customer.AccountId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                    cmd.Parameters.AddWithValue("@Price_level_id", customer.PriceLevel ?? (object)DBNull.Value);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Customer with ID {id} not found");
                    }
                }

                return Ok("Customer updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/CustomerList/DeleteCustomer/{id}
        [HttpDelete("DeleteCustomer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Customer", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customer_id", id);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Customer with ID {id} not found");
                    }
                }

                return Ok("Customer deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 