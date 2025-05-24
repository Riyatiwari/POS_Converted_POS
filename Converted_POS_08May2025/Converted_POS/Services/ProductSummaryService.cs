using Converted_POS.Data;
using Converted_POS.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using Converted_POS.Extensions;
using System.Data;

namespace Converted_POS.Services
{
    public class ProductSummaryService : IProductSummaryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductSummaryService> _logger;

        public ProductSummaryService(ApplicationDbContext context, ILogger<ProductSummaryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<PSummaryRow>> GetProductSummaryAsync(
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
            string reportType)
        {
            try
            {
                // Sample data for demonstration
                var result = new List<PSummaryRow>();
                
                result.Add(new PSummaryRow
                {
                    ProductGroup = "Beverages",
                    ProductName = "Coca Cola",
                    Price = 2.50m,
                    SaleQuantity = 125,
                    SaleQuantityOther = 10,
                    ReturnQuantity = 5,
                    TotalAmount = 312.50m,
                    Discount = 12.50m,
                    NetAmount = 300.00m,
                    TaxAmount = 60.00m,
                    VolumeSold = 135.0m,
                    QuantitySold = 130.0m,
                    Size = "500ml",
                    Location = "Bar",
                    ProductId = 1
                });
                
                result.Add(new PSummaryRow
                {
                    ProductGroup = "Beverages",
                    ProductName = "Diet Coke",
                    Price = 2.50m,
                    SaleQuantity = 75,
                    SaleQuantityOther = 5,
                    ReturnQuantity = 2,
                    TotalAmount = 187.50m,
                    Discount = 7.50m,
                    NetAmount = 180.00m,
                    TaxAmount = 36.00m,
                    VolumeSold = 80.0m,
                    QuantitySold = 78.0m,
                    Size = "500ml",
                    Location = "Bar",
                    ProductId = 2
                });
                
                result.Add(new PSummaryRow
                {
                    ProductGroup = "Food",
                    ProductName = "Burger",
                    Price = 8.95m,
                    SaleQuantity = 45,
                    SaleQuantityOther = 0,
                    ReturnQuantity = 1,
                    TotalAmount = 402.75m,
                    Discount = 17.75m,
                    NetAmount = 385.00m,
                    TaxAmount = 77.00m,
                    VolumeSold = 45.0m,
                    QuantitySold = 44.0m,
                    Size = "Regular",
                    Location = "Kitchen",
                    ProductId = 3
                });
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product summary data");
                throw;
            }
        }

        public async Task<byte[]> ExportToExcelAsync(
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
            bool showSize)
        {
            try
            {
                var data = await GetProductSummaryAsync(
                    companyId,
                    fromDate,
                    toDate,
                    duration,
                    locationId,
                    tillIds,
                    productGroupId,
                    productId,
                    customerId,
                    includeReturns,
                    reportType);
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Product Summary");
                    
                    // Add header row
                    int col = 1;
                    worksheet.Cell(1, col++).Value = "Product Group";
                    worksheet.Cell(1, col++).Value = "Product";
                    worksheet.Cell(1, col++).Value = "Price";
                    worksheet.Cell(1, col++).Value = "Sales Qty";
                    worksheet.Cell(1, col++).Value = "Sales Qty Other";
                    worksheet.Cell(1, col++).Value = "Return Qty";
                    worksheet.Cell(1, col++).Value = "Total Amount";
                    worksheet.Cell(1, col++).Value = "Discount";
                    worksheet.Cell(1, col++).Value = "Net Amount";
                    worksheet.Cell(1, col++).Value = "Tax Amount";
                    worksheet.Cell(1, col++).Value = "Volume Sold";
                    worksheet.Cell(1, col++).Value = "Qty Sold";
                    
                    if (showSize)
                    {
                        worksheet.Cell(1, col++).Value = "Size";
                    }
                    
                    if (showLocation)
                    {
                        worksheet.Cell(1, col++).Value = "Location";
                    }
                    
                    // Add data rows
                    int row = 2;
                    foreach (var item in data)
                    {
                        col = 1;
                        worksheet.Cell(row, col++).Value = item.ProductGroup;
                        worksheet.Cell(row, col++).Value = item.ProductName;
                        worksheet.Cell(row, col++).Value = item.Price;
                        worksheet.Cell(row, col++).Value = item.SaleQuantity;
                        worksheet.Cell(row, col++).Value = item.SaleQuantityOther;
                        worksheet.Cell(row, col++).Value = item.ReturnQuantity;
                        worksheet.Cell(row, col++).Value = item.TotalAmount;
                        worksheet.Cell(row, col++).Value = item.Discount;
                        worksheet.Cell(row, col++).Value = item.NetAmount;
                        worksheet.Cell(row, col++).Value = item.TaxAmount;
                        worksheet.Cell(row, col++).Value = item.VolumeSold;
                        worksheet.Cell(row, col++).Value = item.QuantitySold;
                        
                        if (showSize)
                        {
                            worksheet.Cell(row, col++).Value = item.Size;
                        }
                        
                        if (showLocation)
                        {
                            worksheet.Cell(row, col++).Value = item.Location;
                        }
                        
                        row++;
                    }
                    
                    // Format header
                    var headerRange = worksheet.Range(1, 1, 1, col - 1);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    
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
                _logger.LogError(ex, "Error exporting product summary to Excel");
                throw;
            }
        }
    }
} 