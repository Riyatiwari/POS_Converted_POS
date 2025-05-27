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
    public class TableListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TableListController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("POSConnectionString");
        }

        // GET: api/TableList/GetAll
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<TableListModel>> GetAllTables()
        {
            try
            {
                var tables = new List<TableListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var table = new TableListModel
                        {
                            Id = Convert.ToInt32(reader["Table_id"]),
                            Name = reader["Table_name"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : 0,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            MinCover = reader["MinCover"] != DBNull.Value ? Convert.ToInt32(reader["MinCover"]) : 0,
                            MaxCover = reader["MaxCover"] != DBNull.Value ? Convert.ToInt32(reader["MaxCover"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            IsOpen = reader["is_open"] != DBNull.Value ? Convert.ToBoolean(reader["is_open"]) : false,
                            SortingNo = reader["SortingNo"] != DBNull.Value ? Convert.ToInt32(reader["SortingNo"]) : 0,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ShapeId = reader["shape_id"] != DBNull.Value ? Convert.ToInt32(reader["shape_id"]) : (int?)null,
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null
                        };
                        tables.Add(table);
                    }
                }

                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/TableList/GetByCompanyId/{companyId}
        [HttpGet("GetByCompanyId/{companyId}")]
        public ActionResult<IEnumerable<TableListModel>> GetTablesByCompanyId(int companyId)
        {
            try
            {
                var tables = new List<TableListModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cmp_id", companyId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var table = new TableListModel
                        {
                            Id = Convert.ToInt32(reader["Table_id"]),
                            Name = reader["Table_name"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : 0,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            MinCover = reader["MinCover"] != DBNull.Value ? Convert.ToInt32(reader["MinCover"]) : 0,
                            MaxCover = reader["MaxCover"] != DBNull.Value ? Convert.ToInt32(reader["MaxCover"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            IsOpen = reader["is_open"] != DBNull.Value ? Convert.ToBoolean(reader["is_open"]) : false,
                            SortingNo = reader["SortingNo"] != DBNull.Value ? Convert.ToInt32(reader["SortingNo"]) : 0,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ShapeId = reader["shape_id"] != DBNull.Value ? Convert.ToInt32(reader["shape_id"]) : (int?)null,
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null
                        };
                        tables.Add(table);
                    }
                }

                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/TableList/GetById/{id}
        [HttpGet("GetById/{id}")]
        public ActionResult<TableListModel> GetTableById(int id)
        {
            try
            {
                TableListModel table = null;

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Get_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@table_id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        table = new TableListModel
                        {
                            Id = Convert.ToInt32(reader["Table_id"]),
                            Name = reader["Table_name"].ToString(),
                            CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                            LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : 0,
                            LocationName = reader["location_name"] != DBNull.Value ? reader["location_name"].ToString() : null,
                            MinCover = reader["MinCover"] != DBNull.Value ? Convert.ToInt32(reader["MinCover"]) : 0,
                            MaxCover = reader["MaxCover"] != DBNull.Value ? Convert.ToInt32(reader["MaxCover"]) : 0,
                            IsActive = reader["is_active"] != DBNull.Value ? Convert.ToBoolean(reader["is_active"]) : false,
                            IsOpen = reader["is_open"] != DBNull.Value ? Convert.ToBoolean(reader["is_open"]) : false,
                            SortingNo = reader["SortingNo"] != DBNull.Value ? Convert.ToInt32(reader["SortingNo"]) : 0,
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : (DateTime?)null,
                            CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : (int?)null,
                            ModifiedDate = reader["modified_date"] != DBNull.Value ? Convert.ToDateTime(reader["modified_date"]) : (DateTime?)null,
                            ModifiedBy = reader["modified_by"] != DBNull.Value ? Convert.ToInt32(reader["modified_by"]) : (int?)null,
                            ShapeId = reader["shape_id"] != DBNull.Value ? Convert.ToInt32(reader["shape_id"]) : (int?)null,
                            MachineId = reader["machine_id"] != DBNull.Value ? Convert.ToInt32(reader["machine_id"]) : (int?)null
                        };
                    }
                }

                if (table == null)
                {
                    return NotFound($"Table with ID {id} not found");
                }

                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/TableList/Add
        [HttpPost("Add")]
        public ActionResult AddTable([FromBody] TableListModel table)
        {
            try
            {
                if (table == null)
                {
                    return BadRequest("Table data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@table_id", 0);
                    cmd.Parameters.AddWithValue("@name", table.Name);
                    cmd.Parameters.AddWithValue("@Location_id", table.LocationId);
                    cmd.Parameters.AddWithValue("@min_cover", table.MinCover);
                    cmd.Parameters.AddWithValue("@max_cover", table.MaxCover);
                    cmd.Parameters.AddWithValue("@is_open", table.IsOpen);
                    cmd.Parameters.AddWithValue("@shorting_no", table.SortingNo);
                    cmd.Parameters.AddWithValue("@cmp_id", table.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", table.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "I");
                    
                    if (table.ShapeId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@shape_id", table.ShapeId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@shape_id", DBNull.Value);
                    }
                    
                    if (table.MachineId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@machine_id", table.MachineId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@machine_id", DBNull.Value);
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Table added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/TableList/Update/{id}
        [HttpPut("Update/{id}")]
        public ActionResult UpdateTable(int id, [FromBody] TableListModel table)
        {
            try
            {
                if (table == null)
                {
                    return BadRequest("Table data is null");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("P_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@table_id", id);
                    cmd.Parameters.AddWithValue("@name", table.Name);
                    cmd.Parameters.AddWithValue("@Location_id", table.LocationId);
                    cmd.Parameters.AddWithValue("@min_cover", table.MinCover);
                    cmd.Parameters.AddWithValue("@max_cover", table.MaxCover);
                    cmd.Parameters.AddWithValue("@is_open", table.IsOpen);
                    cmd.Parameters.AddWithValue("@shorting_no", table.SortingNo);
                    cmd.Parameters.AddWithValue("@cmp_id", table.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", table.IsActive);
                    cmd.Parameters.AddWithValue("@Tran_Type", "U");
                    
                    if (table.ShapeId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@shape_id", table.ShapeId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@shape_id", DBNull.Value);
                    }
                    
                    if (table.MachineId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@machine_id", table.MachineId.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@machine_id", DBNull.Value);
                    }

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound($"Table with ID {id} not found");
                    }
                }

                return Ok("Table updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/TableList/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteTable(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if the record exists
                    SqlCommand checkCmd = new SqlCommand("Get_M_Table", connection);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@table_id", id);
                    
                    connection.Open();
                    
                    TableListModel table = null;
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            table = new TableListModel
                            {
                                Id = Convert.ToInt32(reader["Table_id"]),
                                Name = reader["Table_name"].ToString(),
                                CompanyId = reader["cmp_id"] != DBNull.Value ? Convert.ToInt32(reader["cmp_id"]) : 0,
                                LocationId = reader["location_id"] != DBNull.Value ? Convert.ToInt32(reader["location_id"]) : 0,
                                MinCover = reader["MinCover"] != DBNull.Value ? Convert.ToInt32(reader["MinCover"]) : 0,
                                MaxCover = reader["MaxCover"] != DBNull.Value ? Convert.ToInt32(reader["MaxCover"]) : 0
                            };
                        }
                    }
                    
                    if (table == null)
                    {
                        return NotFound($"Table with ID {id} not found");
                    }

                    // Now delete the record
                    SqlCommand cmd = new SqlCommand("P_M_Table", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@table_id", id);
                    cmd.Parameters.AddWithValue("@name", table.Name);
                    cmd.Parameters.AddWithValue("@Location_id", table.LocationId);
                    cmd.Parameters.AddWithValue("@min_cover", table.MinCover);
                    cmd.Parameters.AddWithValue("@max_cover", table.MaxCover);
                    cmd.Parameters.AddWithValue("@is_open", false);
                    cmd.Parameters.AddWithValue("@shorting_no", 0);
                    cmd.Parameters.AddWithValue("@cmp_id", table.CompanyId);
                    cmd.Parameters.AddWithValue("@ip_address", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1");
                    cmd.Parameters.AddWithValue("@login_id", HttpContext.Session.GetInt32("UserID") ?? 0);
                    cmd.Parameters.AddWithValue("@is_active", false);
                    cmd.Parameters.AddWithValue("@Tran_Type", "D");
                    cmd.Parameters.AddWithValue("@shape_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@machine_id", DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                return Ok("Table deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 