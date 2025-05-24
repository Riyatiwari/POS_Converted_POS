using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Converted_POS.Models;

namespace Converted_POS.Services
{
    public interface IProductReportService
    {
        Task<IEnumerable<ProductSummaryReport>> GetProductSummaryAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            int productId,
            int categoryId,
            string duration,
            string type,
            bool includeReturns,
            int locationId,
            string tillIds,
            int customerId);

        Task<IEnumerable<ProductDetailReport>> GetProductDetailsAsync(
            int companyId,
            int productId,
            DateTime fromDate,
            DateTime toDate);

        Task<byte[]> ExportProductReportAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            int productId,
            int categoryId,
            string duration,
            string type);
    }
} 