using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.IO;
using Converted_POS.Models;
using Converted_POS.Extensions;

namespace Converted_POS.Services
{
    public class ProductReportService : IProductReportService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public ProductReportService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ProductSummaryReport>> GetProductSummaryAsync(
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
            int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cmp_id", companyId);
                parameters.Add("@date1", fromDate);
                parameters.Add("@date2", toDate);
                parameters.Add("@product_id", productId);
                parameters.Add("@category_id", categoryId);
                parameters.Add("@Duration", duration);
                parameters.Add("@type", type);
                parameters.Add("@Product_Return", includeReturns ? 1 : 0);
                parameters.Add("@Location_Id", locationId);
                parameters.Add("@Till_Id", tillIds);
                parameters.Add("@cust_id", customerId);

                var spName = locationId > 0 ? "P_R_Product_Location" : "P_R_Product_New";
                
                var result = await connection.QueryAsync<ProductSummaryReport>(
                    spName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<ProductDetailReport>> GetProductDetailsAsync(
            int companyId,
            int productId,
            DateTime fromDate,
            DateTime toDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cmp_id", companyId);
                parameters.Add("@product_id", productId);
                parameters.Add("@date1", fromDate);
                parameters.Add("@date2", toDate);

                var result = await connection.QueryAsync<ProductDetailReport>(
                    "P_R_Product_Details",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<byte[]> ExportProductReportAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            int productId,
            int categoryId,
            string duration,
            string type)
        {
            var data = await GetProductSummaryAsync(
                companyId,
                fromDate,
                toDate,
                productId,
                categoryId,
                duration,
                type,
                false,
                0,
                null,
                0);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Product Report");

                // Add headers
                worksheet.Cells[1, 1].Value = "Product Name";
                worksheet.Cells[1, 2].Value = "Category";
                worksheet.Cells[1, 3].Value = "Quantity";
                worksheet.Cells[1, 4].Value = "Amount";
                worksheet.Cells[1, 5].Value = "Date Range";

                // Style headers
                var headerRange = worksheet.Cells[1, 1, 1, 5];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                // Add data
                var row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.ProductName;
                    worksheet.Cells[row, 2].Value = item.CategoryName;
                    worksheet.Cells[row, 3].Value = item.Quantity;
                    worksheet.Cells[row, 4].Value = item.Amount;
                    worksheet.Cells[row, 5].Value = $"{fromDate:d} - {toDate:d}";
                    row++;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                return await package.GetAsByteArrayAsync();
            }
        }
    }
} 