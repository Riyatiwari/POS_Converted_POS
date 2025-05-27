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
    public class IngredientsListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public IngredientsListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/IngredientsList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<IngredientModel>> GetAllIngredients()
        {
            try
            {
                var ingredients = new List<IngredientModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_All_Ingredients", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var ingredient = new IngredientModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Quantity = reader["quantity"] != DBNull.Value ? Convert.ToDecimal(reader["quantity"]) : 0,
                            Unit = reader["unit"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                        ingredients.Add(ingredient);
                    }
                }

                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/IngredientsList/GetByProductId/{productId}
        [HttpGet("GetByProductId/{productId}")]
        public ActionResult<IEnumerable<IngredientModel>> GetIngredientsByProductId(int productId)
        {
            try
            {
                var ingredients = new List<IngredientModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Ingredients_By_Product_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", productId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var ingredient = new IngredientModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Quantity = reader["quantity"] != DBNull.Value ? Convert.ToDecimal(reader["quantity"]) : 0,
                            Unit = reader["unit"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                        ingredients.Add(ingredient);
                    }
                }

                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/IngredientsList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<IngredientModel> GetIngredientById(int id)
        {
            try
            {
                IngredientModel ingredient = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_Ingredient_By_Id", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ingredient = new IngredientModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            LocationId = Convert.ToInt32(reader["location_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Name = reader["name"].ToString(),
                            Quantity = reader["quantity"] != DBNull.Value ? Convert.ToDecimal(reader["quantity"]) : 0,
                            Unit = reader["unit"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            CompanyName = reader["company_name"] != DBNull.Value ? reader["company_name"].ToString() : null,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            ProductName = reader["product_name"] != DBNull.Value ? reader["product_name"].ToString() : null
                        };
                    }
                }

                if (ingredient == null)
                {
                    return NotFound($"Ingredient with ID {id} not found");
                }

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/IngredientsList/Add
        [HttpPost("Add")]
        public ActionResult AddIngredient([FromBody] IngredientModel ingredient)
        {
            try
            {
                if (ingredient == null)
                {
                    return BadRequest("Ingredient data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Ingredient", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@company_id", ingredient.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", ingredient.LocationId);
                    cmd.Parameters.AddWithValue("@product_id", ingredient.ProductId);
                    cmd.Parameters.AddWithValue("@name", ingredient.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                    cmd.Parameters.AddWithValue("@unit", ingredient.Unit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", ingredient.IsActive);
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Ingredient added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/IngredientsList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateIngredient(int id, [FromBody] IngredientModel ingredient)
        {
            try
            {
                if (ingredient == null)
                {
                    return BadRequest("Ingredient data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update_Ingredient", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@company_id", ingredient.CompanyId);
                    cmd.Parameters.AddWithValue("@location_id", ingredient.LocationId);
                    cmd.Parameters.AddWithValue("@product_id", ingredient.ProductId);
                    cmd.Parameters.AddWithValue("@name", ingredient.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                    cmd.Parameters.AddWithValue("@unit", ingredient.Unit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@is_active", ingredient.IsActive);
                    cmd.Parameters.AddWithValue("@modified_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Ingredient with ID {id} not found");
                    }
                }

                return Ok("Ingredient updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/IngredientsList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteIngredient(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete_Ingredient", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@deleted_by", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Ingredient with ID {id} not found");
                    }
                }

                return Ok("Ingredient deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 