using Converted_POS.Services;
using Converted_POS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Converted_POS.Extensions;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Converted_POS.Controllers
{
    [Authorize]
    public class PSummaryController : BaseController
    {
        private readonly IProductSummaryService _productSummaryService;

        public PSummaryController(
            IBindingService bindingService,
            IRoleService roleService,
            IProductSummaryService productSummaryService,
            ILogger<PSummaryController> logger)
            : base(bindingService, roleService, logger)
        {
            _productSummaryService = productSummaryService;
        }

        public async Task<IActionResult> Index(
            string duration = "Today",
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? venueId = null,
            int? locationId = null,
            int? productGroupId = null,
            int? productId = null,
            int? customerId = null,
            bool includeReturns = false,
            string reportType = "ALL")
        {
            var model = new PSummaryReportViewModel
            {
                Duration = duration,
                CanView = await HasViewPermission("ProductSummary"),
                VenueId = venueId?.ToString(),
                LocationId = locationId?.ToString(),
                ProductGroupId = productGroupId?.ToString(),
                ProductId = productId?.ToString(),
                CustomerId = customerId?.ToString(),
                IncludeReturns = includeReturns,
                ReportType = reportType
            };

            if (!model.CanView)
            {
                return View(model);
            }

            if (fromDate.HasValue)
            {
                model.FromDate = fromDate.Value;
            }
            else
            {
                model.FromDate = DateTime.Today;
            }

            if (toDate.HasValue)
            {
                model.ToDate = toDate.Value;
            }
            else
            {
                model.ToDate = DateTime.Today;
            }

            await BindDropdowns(model);

            if (model.Duration != "Custom")
            {
                SetDateRange(model.Duration, out DateTime from, out DateTime to);
                model.FromDate = from;
                model.ToDate = to;
            }

            try
            {
                var companyId = GetCompanyId();
                
                // Convert string IDs to int or null
                int? locationIdValue = string.IsNullOrEmpty(model.LocationId) ? null : int.Parse(model.LocationId);
                int? productGroupIdValue = string.IsNullOrEmpty(model.ProductGroupId) ? null : int.Parse(model.ProductGroupId);
                int? productIdValue = string.IsNullOrEmpty(model.ProductId) ? null : int.Parse(model.ProductId);
                int? customerIdValue = string.IsNullOrEmpty(model.CustomerId) ? null : int.Parse(model.CustomerId);
                
                // Convert TillId(s) to List<int>
                List<int> tillIds = new List<int>();
                if (!string.IsNullOrEmpty(model.TillId))
                {
                    tillIds = model.TillId.Split(',')
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => int.Parse(x.Trim()))
                        .ToList();
                }
                
                model.ReportData = await _productSummaryService.GetProductSummaryAsync(
                    companyId,
                    model.FromDate,
                    model.ToDate,
                    model.Duration,
                    locationIdValue,
                    tillIds,
                    productGroupIdValue,
                    productIdValue,
                    customerIdValue,
                    model.IncludeReturns,
                    model.ReportType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product summary report");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLocation(bool showLocation)
        {
            try
            {
                return Json(new { success = true, showLocation });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling location visibility");
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSize(bool showSize)
        {
            try
            {
                return Json(new { success = true, showSize });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling size visibility");
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetLocations(int venueId)
        {
            try
            {
                var companyId = GetCompanyId();
                var locations = await _bindingService.GetLocationsByVenueAsync(companyId, venueId);
                return Json(new { success = true, locations });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting locations for venue {VenueId}", venueId);
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts(int productGroupId)
        {
            try
            {
                var companyId = GetCompanyId();
                var products = await _bindingService.GetProductsByGroupAsync(companyId, productGroupId);
                return Json(new { success = true, products });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for product group {ProductGroupId}", productGroupId);
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(
            string duration = "Today",
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? venueId = null,
            int? locationId = null,
            int? productGroupId = null,
            int? productId = null,
            int? customerId = null,
            bool includeReturns = false,
            string reportType = "ALL",
            bool showLocation = false,
            bool showSize = false)
        {
            try
            {
                if (!await HasViewPermission("ProductSummary"))
                {
                    return Unauthorized();
                }

                var companyId = GetCompanyId();
                
                if (duration != "Custom")
                {
                    SetDateRange(duration, out DateTime from, out DateTime to);
                    fromDate = from;
                    toDate = to;
                }

                var excelBytes = await _productSummaryService.ExportToExcelAsync(
                    companyId,
                    fromDate ?? DateTime.Today,
                    toDate ?? DateTime.Today,
                    duration,
                    locationId,
                    new System.Collections.Generic.List<int>(),
                    productGroupId,
                    productId,
                    customerId,
                    includeReturns,
                    reportType,
                    showLocation,
                    showSize);

                return File(
                    excelBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"ProductSummary_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting product summary to Excel");
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task BindDropdowns(PSummaryReportViewModel model)
        {
            var companyId = GetCompanyId();
            
            model.Venues = await _bindingService.GetVenuesAsync(companyId);
            
            int venueIdValue = 0;
            if (!string.IsNullOrEmpty(model.VenueId) && int.TryParse(model.VenueId, out venueIdValue))
            {
                model.Locations = await _bindingService.GetLocationsByVenueAsync(companyId, venueIdValue);
            }
            
            model.ProductGroups = await _bindingService.GetProductGroupsAsync(companyId);
            
            int productGroupIdValue = 0;
            if (!string.IsNullOrEmpty(model.ProductGroupId) && int.TryParse(model.ProductGroupId, out productGroupIdValue))
            {
                model.Products = await _bindingService.GetProductsByGroupAsync(companyId, productGroupIdValue);
            }
            
            model.Customers = await _bindingService.GetCustomersAsync(companyId);
        }

        private void SetDateRange(string duration, out DateTime fromDate, out DateTime toDate)
        {
            toDate = DateTime.Now.Date;
            fromDate = toDate;

            switch (duration)
            {
                case "Today":
                    break;
                case "Yesterday":
                    fromDate = toDate.AddDays(-1);
                    toDate = fromDate;
                    break;
                case "This Week":
                    int diff = (7 + (toDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                    fromDate = toDate.AddDays(-diff);
                    break;
                case "This Month":
                    fromDate = new DateTime(toDate.Year, toDate.Month, 1);
                    break;
                case "This Year":
                    fromDate = new DateTime(toDate.Year, 1, 1);
                    break;
                case "Last Week":
                    int lastWeekDiff = (7 + (toDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                    toDate = toDate.AddDays(-lastWeekDiff - 1);
                    fromDate = toDate.AddDays(-6);
                    break;
                case "Last Month":
                    if (toDate.Month == 1)
                    {
                        fromDate = new DateTime(toDate.Year - 1, 12, 1);
                        toDate = new DateTime(toDate.Year - 1, 12, DateTime.DaysInMonth(toDate.Year - 1, 12));
                    }
                    else
                    {
                        fromDate = new DateTime(toDate.Year, toDate.Month - 1, 1);
                        toDate = new DateTime(toDate.Year, toDate.Month - 1, DateTime.DaysInMonth(toDate.Year, toDate.Month - 1));
                    }
                    break;
                case "Last Year":
                    fromDate = new DateTime(toDate.Year - 1, 1, 1);
                    toDate = new DateTime(toDate.Year - 1, 12, 31);
                    break;
                default:
                    break;
            }
        }
    }
} 