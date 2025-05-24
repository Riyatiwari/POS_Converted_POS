using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Converted_POS.Models;
using Converted_POS.Data;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Extensions.Logging;
using Converted_POS.ViewModels;

namespace Converted_POS.Services
{
    public class TransactionReportService : ITransactionReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionReportService> _logger;

        public TransactionReportService(ApplicationDbContext context, ILogger<TransactionReportService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<TransactionReport> Data, int Total, int Filtered)> GetTransactionsAsync(
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int locationId,
            string tillIds,
            string paymentType,
            int start,
            int length,
            string searchValue,
            string sortColumn,
            string sortDirection)
        {
            var query = _context.Transactions
                .Include(t => t.Product)
                .Include(t => t.Till)
                .Include(t => t.User)
                .AsQueryable();

            // Apply date filter
            query = query.Where(t => t.CreatedDate >= fromDate && t.CreatedDate <= toDate);

            // Apply location filter
            if (locationId > 0)
            {
                query = query.Where(t => t.LocationId == locationId);
            }

            // Apply till filter
            if (!string.IsNullOrEmpty(tillIds) && tillIds != "0")
            {
                var selectedTills = tillIds.Split(',').Select(int.Parse).ToList();
                query = query.Where(t => selectedTills.Contains(t.TillId));
            }

            // Apply payment type filter
            if (!string.IsNullOrEmpty(paymentType) && paymentType != "ALL")
            {
                query = query.Where(t => t.PaymentType == paymentType);
            }

            // Apply search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(t =>
                    t.Product.Name.Contains(searchValue) ||
                    t.Till.Name.Contains(searchValue) ||
                    t.User.UserName.Contains(searchValue)
                );
            }

            // Get total count
            int totalCount = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn)
                {
                    case "ProductName":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.Product.Name) : query.OrderByDescending(t => t.Product.Name);
                        break;
                    case "Till":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.Till.Name) : query.OrderByDescending(t => t.Till.Name);
                        break;
                    case "UserName":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.User.UserName) : query.OrderByDescending(t => t.User.UserName);
                        break;
                    case "Quantity":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.Quantity) : query.OrderByDescending(t => t.Quantity);
                        break;
                    case "TotalAmount":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.TotalAmount) : query.OrderByDescending(t => t.TotalAmount);
                        break;
                    case "TaxAmount":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.TaxAmount) : query.OrderByDescending(t => t.TaxAmount);
                        break;
                    case "CreatedDate":
                        query = sortDirection == "asc" ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
                        break;
                    default:
                        query = query.OrderByDescending(t => t.CreatedDate);
                        break;
                }
            }
            else
            {
                // Default sorting
                query = query.OrderByDescending(t => t.CreatedDate);
            }

            // Apply pagination
            query = query.Skip(start).Take(length);

            // Execute query
            var transactions = await query.ToListAsync();

            // Map to TransactionReport
            var reportData = transactions.Select(t => new TransactionReport
            {
                Id = t.Id,
                ProductName = t.Product.Name,
                Till = t.Till.Name,
                UserName = t.User.UserName,
                Quantity = t.Quantity,
                TotalAmount = t.TotalAmount,
                TaxAmount = t.TaxAmount,
                CreatedDate = t.CreatedDate,
                PaymentType = t.PaymentType
            }).ToList();

            return (reportData, totalCount, transactions.Count);
        }

        public async Task<List<TransactionReport>> GetTransactionReportAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            string paymentType,
            string groupBy)
        {
            try
            {
                // Sample data for demonstration
                var result = new List<TransactionReport>
                {
                    new TransactionReport
                    {
                        Id = 1,
                        ProductName = "Coca Cola",
                        Till = "Till 1",
                        UserName = "John Smith",
                        Quantity = 10,
                        TotalAmount = 25.00m,
                        TaxAmount = 5.00m,
                        CreatedDate = DateTime.Today,
                        PaymentType = "CASH"
                    },
                    new TransactionReport
                    {
                        Id = 2,
                        ProductName = "Diet Coke",
                        Till = "Till 2",
                        UserName = "Jane Doe",
                        Quantity = 5,
                        TotalAmount = 12.50m,
                        TaxAmount = 2.50m,
                        CreatedDate = DateTime.Today,
                        PaymentType = "CARD"
                    }
                };
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting transaction report data");
                return new List<TransactionReport>();
            }
        }

        public async Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            List<int> tillIds,
            string paymentType,
            string groupBy)
        {
            try
            {
                var transactions = await GetTransactionReportAsync(companyId, fromDate, toDate, duration, locationId, tillIds, paymentType, groupBy);
                
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Transaction Report");
                
                // Add headers
                worksheet.Cell("A1").Value = "Product";
                worksheet.Cell("B1").Value = "Till";
                worksheet.Cell("C1").Value = "Operator";
                worksheet.Cell("D1").Value = "Quantity";
                worksheet.Cell("E1").Value = "Total Amount";
                worksheet.Cell("F1").Value = "Tax Amount";
                worksheet.Cell("G1").Value = "Date";
                worksheet.Cell("H1").Value = "Payment Type";
                
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;
                
                // Add data
                int row = 2;
                foreach (var transaction in transactions)
                {
                    worksheet.Cell($"A{row}").Value = transaction.ProductName;
                    worksheet.Cell($"B{row}").Value = transaction.Till;
                    worksheet.Cell($"C{row}").Value = transaction.UserName;
                    worksheet.Cell($"D{row}").Value = transaction.Quantity;
                    worksheet.Cell($"E{row}").Value = transaction.TotalAmount;
                    worksheet.Cell($"F{row}").Value = transaction.TaxAmount;
                    worksheet.Cell($"G{row}").Value = transaction.CreatedDate;
                    worksheet.Cell($"H{row}").Value = transaction.PaymentType;
                    row++;
                }
                
                // Format numbers
                worksheet.Column(4).Style.NumberFormat.Format = "#,##0.00";
                worksheet.Column(5).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Column(6).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Column(7).Style.NumberFormat.Format = "dd/mm/yyyy hh:mm";

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting transaction report to Excel");
                throw;
            }
        }

        public async Task<TransactionDetails> GetTransactionDetailsAsync(int transactionId)
        {
            try
            {
                // Sample data for demonstration
                var detail = new TransactionDetails
                {
                    Id = transactionId,
                    TransactionNumber = $"TX-{transactionId:0000}",
                    ProductName = "Multiple Products",
                    Till = "Till 1",
                    UserName = "John Smith",
                    Location = "Main Store",
                    Quantity = 15,
                    UnitPrice = 2.50m,
                    TotalAmount = 37.50m,
                    TaxAmount = 7.50m,
                    DiscountAmount = 0.00m,
                    PaymentType = "CASH",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    Items = new List<TransactionItemDetails>
                    {
                        new TransactionItemDetails
                        {
                            ProductName = "Coca Cola",
                            Quantity = 10,
                            UnitPrice = 2.50m,
                            TotalAmount = 25.00m,
                            TaxAmount = 5.00m,
                            DiscountAmount = 0.00m
                        },
                        new TransactionItemDetails
                        {
                            ProductName = "Diet Coke",
                            Quantity = 5,
                            UnitPrice = 2.50m,
                            TotalAmount = 12.50m,
                            TaxAmount = 2.50m,
                            DiscountAmount = 0.00m
                        }
                    }
                };
                
                return detail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting transaction details: {Message}", ex.Message);
                return null;
            }
        }
    }
} 