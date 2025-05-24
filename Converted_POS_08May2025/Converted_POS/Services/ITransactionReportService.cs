using Converted_POS.Models;
using Converted_POS.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Converted_POS.Services
{
    public interface ITransactionReportService
    {
        Task<List<TransactionReport>> GetTransactionReportAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            string paymentType,
            string groupBy);

        Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            string paymentType,
            string groupBy);
            
        Task<TransactionDetails> GetTransactionDetailsAsync(int transactionId);
    }
} 