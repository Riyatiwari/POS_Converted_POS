using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Services.Interfaces
{
    public interface IPSummaryService
    {
        // Get report data based on filters
        Task<List<PSummaryRow>> GetReportDataAsync(PSummaryReportViewModel filters);

        // Get dropdown data
        Task<List<SelectListItem>> GetLocationsAsync(int companyId);
        Task<List<SelectListItem>> GetProductGroupsAsync(int companyId);
        Task<List<SelectListItem>> GetProductsAsync(int companyId, string categoryId = "0");
        Task<List<SelectListItem>> GetTillsAsync(int companyId);
        Task<List<SelectListItem>> GetCustomersAsync(int companyId);

        // Check user permissions
        Task<bool> CanViewReportAsync(int userId, int companyId);

        // Export report data
        Task<byte[]> ExportToExcelAsync(List<PSummaryRow> reportData);

        // Helper methods
        string GetSelectedTillsValue(List<string> selectedTills);
        DateTime GetStartDateForDuration(string duration, DateTime? customDate = null);
        DateTime GetEndDateForDuration(string duration, DateTime? customDate = null);
    }
} 