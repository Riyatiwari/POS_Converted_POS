using Converted_POS.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Converted_POS.Services
{
    public interface IProductSummaryService
    {
        Task<List<PSummaryRow>> GetProductSummaryAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            int? productGroupId,
            int? productId,
            int? customerId,
            bool includeReturns,
            string reportType);

        Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            int? productGroupId,
            int? productId,
            int? customerId,
            bool includeReturns,
            string reportType,
            bool showLocation,
            bool showSize);
    }
} 