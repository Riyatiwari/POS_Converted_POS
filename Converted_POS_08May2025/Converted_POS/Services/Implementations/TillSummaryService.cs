using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Converted_POS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Dapper;
using ClosedXML.Excel;

namespace Converted_POS.Services.Implementations
{
    public class TillSummaryService : Services.ITillSummaryService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TillSummaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Explicit interface implementations for Services.Interfaces.ITillSummaryService
        Task<DataTable> Services.Interfaces.ITillSummaryService.GetReportDataAsync(TillSummaryReportViewModel filters)
        {
            return GetReportDataAsyncImpl(filters);
        }

        Task<List<SelectListItem>> Services.Interfaces.ITillSummaryService.GetLocationsAsync(int companyId)
        {
            return GetLocationsAsyncImpl(companyId);
        }

        Task<List<SelectListItem>> Services.Interfaces.ITillSummaryService.GetTillsAsync(int companyId)
        {
            return GetTillsAsyncImpl(companyId);
        }

        Task<bool> Services.Interfaces.ITillSummaryService.CanViewReportAsync(int userId, int companyId)
        {
            return CanViewReportAsyncImpl(userId, companyId);
        }

        Task<byte[]> Services.Interfaces.ITillSummaryService.ExportToExcelAsync(DataTable reportData)
        {
            return ExportToExcelAsyncImpl(reportData);
        }

        Task<DataTable> Services.Interfaces.ITillSummaryService.GetTillSummaryAsync(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId)
        {
            return GetTillSummaryAsyncImpl(companyId, fromDate, toDate, duration, locationId, tillId, venueId);
        }

        Task<byte[]> Services.Interfaces.ITillSummaryService.ExportToExcelAsync(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId)
        {
            return ExportToExcelAsyncImpl(companyId, fromDate, toDate, duration, locationId, tillId, venueId);
        }

        // Private implementation methods
        private async Task<DataTable> GetReportDataAsyncImpl(TillSummaryReportViewModel filters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@date1", filters.FromDate ?? DateTime.Today);
                parameters.Add("@date2", filters.ToDate ?? DateTime.Today);
                parameters.Add("@cmp_id", filters.CompanyId);
                parameters.Add("@Location_Id", string.IsNullOrEmpty(filters.LocationId) ? "0" : filters.LocationId);
                parameters.Add("@Till_Id", string.IsNullOrEmpty(filters.MachineId) ? "0" : filters.MachineId);
                parameters.Add("@Duration", string.IsNullOrEmpty(filters.Duration) ? "Today" : filters.Duration);

                var reader = await connection.ExecuteReaderAsync("Till_Summary_Report", parameters, commandType: CommandType.StoredProcedure);
                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable;
            }
        }

        private async Task<DataTable> GetTillSummaryAsyncImpl(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@date1", fromDate);
                parameters.Add("@date2", toDate);
                parameters.Add("@cmp_id", companyId);
                parameters.Add("@Location_Id", locationId?.ToString() ?? "0");
                parameters.Add("@Till_Id", tillId?.ToString() ?? "0");
                parameters.Add("@Duration", duration ?? "Today");
                
                if (venueId.HasValue && venueId > 0)
                {
                    parameters.Add("@Venue_Id", venueId.Value);
                }

                var reader = await connection.ExecuteReaderAsync("Till_Summary_Report", parameters, commandType: CommandType.StoredProcedure);
                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable;
            }
        }

        private async Task<List<SelectListItem>> GetLocationsAsyncImpl(int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT location_id AS Value, 
                           location_name AS Text 
                    FROM location_master 
                    WHERE company_id = @CompanyId 
                    ORDER BY location_name";

                var locations = await connection.QueryAsync<SelectListItem>(query, new { CompanyId = companyId });
                return locations.ToList();
            }
        }

        private async Task<List<SelectListItem>> GetTillsAsyncImpl(int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT till_id AS Value, 
                           till_name AS Text 
                    FROM till_master 
                    WHERE company_id = @CompanyId 
                    ORDER BY till_name";

                var tills = await connection.QueryAsync<SelectListItem>(query, new { CompanyId = companyId });
                return tills.ToList();
            }
        }

        private async Task<bool> CanViewReportAsyncImpl(int userId, int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT 1 
                    FROM user_permissions 
                    WHERE user_id = @UserId 
                      AND company_id = @CompanyId 
                      AND can_view_till_summary = 1";

                var hasPermission = await connection.QueryFirstOrDefaultAsync<int?>(
                    query, 
                    new { UserId = userId, CompanyId = companyId });
                return hasPermission.HasValue;
            }
        }

        private async Task<byte[]> ExportToExcelAsyncImpl(DataTable reportData)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Till Summary");

                // Add headers
                for (int i = 0; i < reportData.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = reportData.Columns[i].ColumnName;
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                // Add data
                for (int i = 0; i < reportData.Rows.Count; i++)
                {
                    for (int j = 0; j < reportData.Columns.Count; j++)
                    {
                        var value = reportData.Rows[i][j];
                        var cell = worksheet.Cell(i + 2, j + 1);

                        if (value != null)
                        {
                            if (value is decimal || value is double)
                            {
                                cell.Value = Convert.ToDouble(value);
                                cell.Style.NumberFormat.Format = "#,##0.00";
                            }
                            else if (value is DateTime)
                            {
                                cell.Value = ((DateTime)value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                cell.Value = value.ToString();
                            }
                        }
                    }
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        
        private async Task<byte[]> ExportToExcelAsyncImpl(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId)
        {
            var reportData = await GetTillSummaryAsyncImpl(companyId, fromDate, toDate, duration, locationId, tillId, venueId);
            return await ExportToExcelAsyncImpl(reportData);
        }
    }
} 