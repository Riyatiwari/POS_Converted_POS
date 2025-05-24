using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.Services;
using Converted_POS.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Converted_POS.Extensions;

namespace Converted_POS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductReportService _productReportService;

        public ProductReportController(IConfiguration configuration, IProductReportService productReportService)
        {
            _configuration = configuration;
            _productReportService = productReportService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetProductSummary(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] int? productId,
            [FromQuery] int? categoryId,
            [FromQuery] string duration,
            [FromQuery] string type,
            [FromQuery] bool includeReturns,
            [FromQuery] int? locationId,
            [FromQuery] string tillIds,
            [FromQuery] int? customerId)
        {
            try
            {
                var companyId = User.GetCompanyId();
                var result = await _productReportService.GetProductSummaryAsync(
                    companyId,
                    fromDate ?? DateTime.Now,
                    toDate ?? DateTime.Now,
                    productId ?? 0,
                    categoryId ?? 0,
                    duration ?? "Today",
                    type ?? "ALL",
                    includeReturns,
                    locationId ?? 0,
                    tillIds,
                    customerId ?? 0);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetProductDetails(
            [FromQuery] int productId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            try
            {
                var companyId = User.GetCompanyId();
                var result = await _productReportService.GetProductDetailsAsync(
                    companyId,
                    productId,
                    fromDate ?? DateTime.Now,
                    toDate ?? DateTime.Now);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportProductReport(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] int? productId,
            [FromQuery] int? categoryId,
            [FromQuery] string duration,
            [FromQuery] string type)
        {
            try
            {
                var companyId = User.GetCompanyId();
                var result = await _productReportService.ExportProductReportAsync(
                    companyId,
                    fromDate ?? DateTime.Now,
                    toDate ?? DateTime.Now,
                    productId ?? 0,
                    categoryId ?? 0,
                    duration ?? "Today",
                    type ?? "ALL");

                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductReport.xlsx");
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }
    }
} 