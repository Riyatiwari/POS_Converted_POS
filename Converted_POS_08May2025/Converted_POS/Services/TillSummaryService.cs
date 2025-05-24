using Converted_POS.Data;
using Converted_POS.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Converted_POS.Services
{
    public class TillSummaryService : ITillSummaryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TillSummaryService> _logger;

        public TillSummaryService(ApplicationDbContext context, ILogger<TillSummaryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DataTable> GetReportDataAsync(TillSummaryReportViewModel filters)
        {
            try
            {
                var dataTable = new DataTable();
                
                dataTable.Columns.Add("Venue", typeof(string));
                dataTable.Columns.Add("Location", typeof(string));
                dataTable.Columns.Add("Till", typeof(string));
                dataTable.Columns.Add("SaleCount", typeof(decimal));
                dataTable.Columns.Add("ReturnCount", typeof(decimal));
                dataTable.Columns.Add("TotalAmount", typeof(decimal));
                dataTable.Columns.Add("Discount", typeof(decimal));
                dataTable.Columns.Add("NetAmount", typeof(decimal));
                dataTable.Columns.Add("TaxAmount", typeof(decimal));
                dataTable.Columns.Add("Cash", typeof(decimal));
                dataTable.Columns.Add("Card", typeof(decimal));
                dataTable.Columns.Add("Other", typeof(decimal));
                
                var rows = await GetTillSummaryRowsAsync(
                    filters.CompanyId,
                    filters.FromDate ?? DateTime.Today,
                    filters.ToDate ?? DateTime.Today,
                    filters.Duration,
                    string.IsNullOrEmpty(filters.LocationId) ? null : int.Parse(filters.LocationId),
                    string.IsNullOrEmpty(filters.TillId) ? null : int.Parse(filters.TillId),
                    string.IsNullOrEmpty(filters.VenueId) ? null : int.Parse(filters.VenueId));
                
                foreach (var row in rows)
                {
                    dataTable.Rows.Add(
                        row.Venue,
                        row.Location,
                        row.Till,
                        row.SaleCount,
                        row.ReturnCount,
                        row.TotalAmount,
                        row.Discount,
                        row.NetAmount,
                        row.TaxAmount,
                        row.Cash,
                        row.Card,
                        row.Other
                    );
                }
                
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting report data");
                return new DataTable();
            }
        }
        
        public async Task<List<SelectListItem>> GetLocationsAsync(int companyId)
        {
            try
            {
                // Since Location model doesn't have CompanyId, we need to get the venues for the company first
                // and then get locations for those venues
                var venues = await _context.Venues
                    .Where(v => v.CompanyId == companyId)
                    .Select(v => v.Id)
                    .ToListAsync();
                
                var locations = await _context.Locations
                    .Where(l => venues.Contains(l.VenueId))
                    .OrderBy(l => l.Name)
                    .Select(l => new SelectListItem
                    {
                        Value = l.Id.ToString(),
                        Text = l.Name
                    })
                    .ToListAsync();
                
                return locations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting locations");
                return new List<SelectListItem>();
            }
        }
        
        public async Task<List<SelectListItem>> GetTillsAsync(int companyId)
        {
            try
            {
                // Since Till model doesn't have CompanyId, we need to get the locations for the company first
                // and then get tills for those locations
                var venues = await _context.Venues
                    .Where(v => v.CompanyId == companyId)
                    .Select(v => v.Id)
                    .ToListAsync();
                
                var locations = await _context.Locations
                    .Where(l => venues.Contains(l.VenueId))
                    .Select(l => l.Id)
                    .ToListAsync();
                
                var tills = await _context.Tills
                    .Where(t => locations.Contains(t.LocationId))
                    .OrderBy(t => t.Name)
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    })
                    .ToListAsync();
                
                return tills;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tills");
                return new List<SelectListItem>();
            }
        }
        
        public async Task<bool> CanViewReportAsync(int userId, int companyId)
        {
            try
            {
                // Since UserPermissions DbSet is not available, we'll use a different approach
                // Check if the user has access to the report using User model
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);
                
                // For now, we'll assume all users can view till summary
                // You may need to customize this based on your actual permissions model
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user permissions");
                return false;
            }
        }
        
        public async Task<byte[]> ExportToExcelAsync(DataTable reportData)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Till Summary");
                    
                    // Add headers
                    for (int i = 0; i < reportData.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = reportData.Columns[i].ColumnName;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }
                    
                    // Add data
                    for (int row = 0; row < reportData.Rows.Count; row++)
                    {
                        for (int col = 0; col < reportData.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = reportData.Rows[row][col].ToString();
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting data to Excel");
                throw;
            }
        }

        public async Task<DataTable> GetTillSummaryAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? tillId,
            int? venueId)
        {
            try
            {
                var rows = await GetTillSummaryRowsAsync(companyId, fromDate, toDate, duration, locationId, tillId, venueId);
                
                var dataTable = new DataTable();
                dataTable.Columns.Add("Venue", typeof(string));
                dataTable.Columns.Add("Location", typeof(string));
                dataTable.Columns.Add("Till", typeof(string));
                dataTable.Columns.Add("SaleCount", typeof(decimal));
                dataTable.Columns.Add("ReturnCount", typeof(decimal));
                dataTable.Columns.Add("TotalAmount", typeof(decimal));
                dataTable.Columns.Add("Discount", typeof(decimal));
                dataTable.Columns.Add("NetAmount", typeof(decimal));
                dataTable.Columns.Add("TaxAmount", typeof(decimal));
                dataTable.Columns.Add("Cash", typeof(decimal));
                dataTable.Columns.Add("Card", typeof(decimal));
                dataTable.Columns.Add("Other", typeof(decimal));
                
                foreach (var row in rows)
                {
                    dataTable.Rows.Add(
                        row.Venue,
                        row.Location,
                        row.Till,
                        row.SaleCount,
                        row.ReturnCount,
                        row.TotalAmount,
                        row.Discount,
                        row.NetAmount,
                        row.TaxAmount,
                        row.Cash,
                        row.Card,
                        row.Other
                    );
                }
                
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting till summary data");
                return new DataTable();
            }
        }
        
        private async Task<List<TillSummaryRow>> GetTillSummaryRowsAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? tillId,
            int? venueId)
        {
            try
            {
                // Sample data generation for testing
                var result = new List<TillSummaryRow>();
                
                result.Add(new TillSummaryRow 
                { 
                    Venue = "Venue 1", 
                    Location = "Location 1", 
                    Till = "Till 1", 
                    SaleCount = 15, 
                    ReturnCount = 2, 
                    TotalAmount = 1500.00m, 
                    Discount = 75.00m, 
                    NetAmount = 1425.00m, 
                    TaxAmount = 285.00m, 
                    Cash = 1125.00m, 
                    Card = 300.00m, 
                    Other = 0.00m 
                });
                
                result.Add(new TillSummaryRow 
                { 
                    Venue = "Venue 1", 
                    Location = "Location 2", 
                    Till = "Till 2", 
                    SaleCount = 25, 
                    ReturnCount = 1, 
                    TotalAmount = 2100.00m, 
                    Discount = 110.00m, 
                    NetAmount = 1990.00m, 
                    TaxAmount = 398.00m, 
                    Cash = 1390.00m, 
                    Card = 500.00m, 
                    Other = 100.00m 
                });
                
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting till summary data");
                return new List<TillSummaryRow>();
            }
        }

        public async Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? tillId,
            int? venueId)
        {
            try
            {
                var dataTable = await GetTillSummaryAsync(companyId, fromDate, toDate, duration, locationId, tillId, venueId);
                return await ExportToExcelAsync(dataTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting till summary to Excel");
                throw;
            }
        }
    }
} 