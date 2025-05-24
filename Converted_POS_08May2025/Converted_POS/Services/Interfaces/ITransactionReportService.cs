using System.Collections.Generic;
using System.Threading.Tasks;
using Converted_POS.Models;

namespace Converted_POS.Services.Interfaces
{
    public interface ITransactionReportService
    {
        Task<List<TransactionDetailRow>> GetTransactionDetailsAsync(int companyId);
        Task<bool> CanViewReportAsync(int userId, int companyId);
        Task<byte[]> ExportToExcelAsync(List<TransactionDetailRow> transactions);
    }
} 