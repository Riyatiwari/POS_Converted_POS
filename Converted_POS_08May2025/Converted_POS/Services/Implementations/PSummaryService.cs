using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Converted_POS.ViewModels;
using Converted_POS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Dapper;
using ClosedXML.Excel;
using Converted_POS.Extensions;

namespace Converted_POS.Services.Implementations
{
    public class PSummaryService : IPSummaryService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public PSummaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<PSummaryRow>> GetReportDataAsync(PSummaryReportViewModel filters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@date1", filters.FromDate);
                parameters.Add("@date2", filters.ToDate);
                parameters.Add("@cmp_id", filters.CompanyId);
                parameters.Add("@product_id", string.IsNullOrEmpty(filters.ProductId) ? "0" : filters.ProductId);
                parameters.Add("@category_id", filters.CategoryId.HasValue ? filters.CategoryId.Value : 0);
                parameters.Add("@Duration", string.IsNullOrEmpty(filters.Duration) ? "Today" : filters.Duration);
                parameters.Add("@type", filters.ReportType);
                parameters.Add("@Product_Return", filters.IncludeReturns ? 1 : 0);
                parameters.Add("@Location_Id", string.IsNullOrEmpty(filters.LocationId) ? "0" : filters.LocationId);
                parameters.Add("@Till_Id", GetSelectedTillsValue(filters.SelectedTills));
                parameters.Add("@cust_id", string.IsNullOrEmpty(filters.CustomerId) ? "0" : filters.CustomerId);

                string storedProcedure = filters.SizeLocationValue switch
                {
                    "0" => "P_R_Product_New",
                    "1" => "P_R_Product_Location",
                    "2" => "P_R_Product_Size",
                    _ => "P_R_Product_New"
                };

                // First, get data from database as DataTable
                var reader = await connection.ExecuteReaderAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                var dataTable = new DataTable();
                dataTable.Load(reader);

                // Then convert to List<PSummaryRow>
                var result = new List<PSummaryRow>();
                foreach (DataRow row in dataTable.Rows)
                {
                    result.Add(new PSummaryRow
                    {
                        ProductGroup = row["ProductGroup"]?.ToString() ?? "",
                        ProductName = row["ProductName"]?.ToString() ?? "",
                        Price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0,
                        SaleQuantity = row["SaleQuantity"] != DBNull.Value ? Convert.ToDecimal(row["SaleQuantity"]) : 0,
                        SaleQuantityOther = row["SaleQuantityOther"] != DBNull.Value ? Convert.ToDecimal(row["SaleQuantityOther"]) : 0, 
                        ReturnQuantity = row["ReturnQuantity"] != DBNull.Value ? Convert.ToDecimal(row["ReturnQuantity"]) : 0,
                        TotalAmount = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0,
                        Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0,
                        NetAmount = row["NetAmount"] != DBNull.Value ? Convert.ToDecimal(row["NetAmount"]) : 0,
                        TaxAmount = row["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["TaxAmount"]) : 0,
                        VolumeSold = row["VolumeSold"] != DBNull.Value ? Convert.ToDecimal(row["VolumeSold"]) : 0,
                        QuantitySold = row["QuantitySold"] != DBNull.Value ? Convert.ToDecimal(row["QuantitySold"]) : 0,
                        Size = row["Size"]?.ToString() ?? "",
                        Location = row["Location"]?.ToString() ?? "",
                        ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0
                    });
                }

                return result;
            }
        }

        public async Task<List<SelectListItem>> GetLocationsAsync(int companyId)
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

        public async Task<List<SelectListItem>> GetProductGroupsAsync(int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT category_id AS Value, 
                           category_name AS Text 
                    FROM category_master 
                    WHERE company_id = @CompanyId 
                    ORDER BY category_name";

                var groups = await connection.QueryAsync<SelectListItem>(query, new { CompanyId = companyId });
                return groups.ToList();
            }
        }

        public async Task<List<SelectListItem>> GetProductsAsync(int companyId, string categoryId = "0")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = categoryId == "0" 
                    ? @"SELECT product_id AS Value, 
                             product_name AS Text 
                      FROM product_master 
                      WHERE company_id = @CompanyId 
                      ORDER BY product_name"
                    : @"SELECT product_id AS Value, 
                             product_name AS Text 
                      FROM product_master 
                      WHERE company_id = @CompanyId 
                        AND category_id = @CategoryId 
                      ORDER BY product_name";

                var products = await connection.QueryAsync<SelectListItem>(
                    query, 
                    new { CompanyId = companyId, CategoryId = categoryId });
                return products.ToList();
            }
        }

        public async Task<List<SelectListItem>> GetTillsAsync(int companyId)
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

        public async Task<List<SelectListItem>> GetCustomersAsync(int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT customer_id AS Value, 
                           customer_name AS Text 
                    FROM customer_master 
                    WHERE company_id = @CompanyId 
                    ORDER BY customer_name";

                var customers = await connection.QueryAsync<SelectListItem>(query, new { CompanyId = companyId });
                return customers.ToList();
            }
        }

        public async Task<bool> CanViewReportAsync(int userId, int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    SELECT 1 
                    FROM user_permissions 
                    WHERE user_id = @UserId 
                      AND company_id = @CompanyId 
                      AND can_view_product_summary = 1";

                var hasPermission = await connection.QueryFirstOrDefaultAsync<int?>(
                    query, 
                    new { UserId = userId, CompanyId = companyId });
                return hasPermission != null;
            }
        }

        public async Task<byte[]> ExportToExcelAsync(DataTable reportData)
        {
            // Convert from our old DataTable format to the new list format
            List<PSummaryRow> data = new List<PSummaryRow>();
            foreach (DataRow row in reportData.Rows)
            {
                data.Add(new PSummaryRow
                {
                    ProductGroup = row["ProductGroup"]?.ToString() ?? "",
                    ProductName = row["ProductName"]?.ToString() ?? "",
                    Price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0,
                    SaleQuantity = row["SaleQuantity"] != DBNull.Value ? Convert.ToDecimal(row["SaleQuantity"]) : 0,
                    SaleQuantityOther = row["SaleQuantityOther"] != DBNull.Value ? Convert.ToDecimal(row["SaleQuantityOther"]) : 0,
                    ReturnQuantity = row["ReturnQuantity"] != DBNull.Value ? Convert.ToDecimal(row["ReturnQuantity"]) : 0,
                    TotalAmount = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0,
                    Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0,
                    NetAmount = row["NetAmount"] != DBNull.Value ? Convert.ToDecimal(row["NetAmount"]) : 0,
                    TaxAmount = row["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["TaxAmount"]) : 0,
                    VolumeSold = row["VolumeSold"] != DBNull.Value ? Convert.ToDecimal(row["VolumeSold"]) : 0,
                    QuantitySold = row["QuantitySold"] != DBNull.Value ? Convert.ToDecimal(row["QuantitySold"]) : 0,
                    Size = row["Size"]?.ToString() ?? "",
                    Location = row["Location"]?.ToString() ?? "",
                    ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0
                });
            }
            
            return await ExportListToExcelAsync(data);
        }

        // Implement interface method
        public async Task<byte[]> ExportToExcelAsync(List<PSummaryRow> data)
        {
            return await ExportListToExcelAsync(data);
        }

        // New method to export List<PSummaryRow> to Excel
        private async Task<byte[]> ExportListToExcelAsync(List<PSummaryRow> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Product Summary");

                // Add headers
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
                worksheet.Cell(1, col++).Value = "Size";
                worksheet.Cell(1, col++).Value = "Location";
                
                // Style header
                var headerRange = worksheet.Range(1, 1, 1, col - 1);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

                // Add data
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
                    worksheet.Cell(row, col++).Value = item.Size;
                    worksheet.Cell(row, col++).Value = item.Location;
                    row++;
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();
                
                // Add totals row
                worksheet.Row(row).Style.Font.Bold = true;
                worksheet.Cell(row, 1).Value = "Total";
                
                // Calculate totals for numeric columns
                worksheet.Cell(row, 3).FormulaA1 = $"SUM(C2:C{row-1})"; // Price
                worksheet.Cell(row, 4).FormulaA1 = $"SUM(D2:D{row-1})"; // Sales Qty
                worksheet.Cell(row, 5).FormulaA1 = $"SUM(E2:E{row-1})"; // Sales Qty Other
                worksheet.Cell(row, 6).FormulaA1 = $"SUM(F2:F{row-1})"; // Return Qty
                worksheet.Cell(row, 7).FormulaA1 = $"SUM(G2:G{row-1})"; // Total Amount
                worksheet.Cell(row, 8).FormulaA1 = $"SUM(H2:H{row-1})"; // Discount
                worksheet.Cell(row, 9).FormulaA1 = $"SUM(I2:I{row-1})"; // Net Amount
                worksheet.Cell(row, 10).FormulaA1 = $"SUM(J2:J{row-1})"; // Tax Amount
                worksheet.Cell(row, 11).FormulaA1 = $"SUM(K2:K{row-1})"; // Volume Sold
                worksheet.Cell(row, 12).FormulaA1 = $"SUM(L2:L{row-1})"; // Qty Sold
                
                // Format numeric columns
                var numericColumns = new[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                foreach (var colNum in numericColumns)
                {
                    worksheet.Range(2, colNum, row, colNum).Style.NumberFormat.Format = "#,##0.00";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public string GetSelectedTillsValue(List<string> selectedTills)
        {
            if (selectedTills == null || !selectedTills.Any())
                return "0";

            return string.Join(",", selectedTills).Replace("#", ",");
        }

        public DateTime GetStartDateForDuration(string duration, DateTime? customDate = null)
        {
            return duration switch
            {
                "Today" => DateTime.Today,
                "Yesterday" => DateTime.Today.AddDays(-1),
                "This Week" => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek),
                "This Month" => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                "This Year" => new DateTime(DateTime.Today.Year, 1, 1),
                "Last Week" => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7),
                "Last Month" => new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
                "Last Year" => new DateTime(DateTime.Today.Year - 1, 1, 1),
                "Custom" => customDate ?? DateTime.Today,
                _ => DateTime.Today
            };
        }

        public DateTime GetEndDateForDuration(string duration, DateTime? customDate = null)
        {
            return duration switch
            {
                "Today" => DateTime.Today,
                "Yesterday" => DateTime.Today.AddDays(-1),
                "This Week" => DateTime.Today.AddDays(6 - (int)DateTime.Today.DayOfWeek),
                "This Month" => new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)),
                "This Year" => new DateTime(DateTime.Today.Year, 12, 31),
                "Last Week" => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1),
                "Last Month" => new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month)),
                "Last Year" => new DateTime(DateTime.Today.Year - 1, 12, 31),
                "Custom" => customDate ?? DateTime.Today,
                _ => DateTime.Today
            };
        }
    }
} 