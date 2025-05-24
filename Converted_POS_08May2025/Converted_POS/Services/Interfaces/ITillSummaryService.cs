using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Services.Interfaces
{
    public interface ITillSummaryService
    {
        Task<DataTable> GetReportDataAsync(TillSummaryReportViewModel filters);
        Task<List<SelectListItem>> GetLocationsAsync(int companyId);
        Task<List<SelectListItem>> GetTillsAsync(int companyId);
        Task<bool> CanViewReportAsync(int userId, int companyId);
        Task<byte[]> ExportToExcelAsync(DataTable reportData);
        
        // Additional methods needed by the implementation
        Task<DataTable> GetTillSummaryAsync(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId);
        Task<byte[]> ExportToExcelAsync(int companyId, DateTime fromDate, DateTime toDate, string duration, int? locationId, int? tillId, int? venueId);
    }
} 