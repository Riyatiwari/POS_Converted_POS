using System;
using System.Threading.Tasks;
using Converted_POS.ViewModels;
using Converted_POS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Converted_POS.Controllers
{
    [Authorize]
    public class PSummaryReportController : Controller
    {
        private readonly IPSummaryService _pSummaryService;

        public PSummaryReportController(IPSummaryService pSummaryService)
        {
            _pSummaryService = pSummaryService;
        }

        public async Task<IActionResult> Index()
        {
            var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            var viewModel = new PSummaryReportViewModel
            {
                CompanyId = companyId,
                CanView = await _pSummaryService.CanViewReportAsync(userId, companyId),
                ReportDataTable = new DataTable()
            };

            if (!viewModel.CanView)
            {
                return View(viewModel);
            }

            // Load dropdown data
            viewModel.Locations = await _pSummaryService.GetLocationsAsync(companyId);
            viewModel.Categories = await _pSummaryService.GetProductGroupsAsync(companyId);
            viewModel.Products = await _pSummaryService.GetProductsAsync(companyId);
            viewModel.Tills = await _pSummaryService.GetTillsAsync(companyId);
            viewModel.Customers = await _pSummaryService.GetCustomersAsync(companyId);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PSummaryReportViewModel model)
        {
            var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            model.CompanyId = companyId;
            model.CanView = await _pSummaryService.CanViewReportAsync(userId, companyId);

            if (!model.CanView)
            {
                return View(model);
            }

            // Reload dropdown data
            model.Locations = await _pSummaryService.GetLocationsAsync(companyId);
            model.Categories = await _pSummaryService.GetProductGroupsAsync(companyId);
            model.Products = await _pSummaryService.GetProductsAsync(companyId, model.CategoryId.HasValue ? model.CategoryId.Value.ToString() : "0");
            model.Tills = await _pSummaryService.GetTillsAsync(companyId);
            model.Customers = await _pSummaryService.GetCustomersAsync(companyId);

            // Set date range based on duration
            if (model.Duration != "Custom")
            {
                model.FromDate = _pSummaryService.GetStartDateForDuration(model.Duration);
                model.ToDate = _pSummaryService.GetEndDateForDuration(model.Duration);
            }

            // Get report data
            model.ReportData = await _pSummaryService.GetReportDataAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string categoryId)
        {
            var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
            var products = await _pSummaryService.GetProductsAsync(companyId, categoryId);
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel(PSummaryReportViewModel model)
        {
            var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            if (!await _pSummaryService.CanViewReportAsync(userId, companyId))
            {
                return Forbid();
            }

            model.CompanyId = companyId;
            var reportData = await _pSummaryService.GetReportDataAsync(model);
            var excelBytes = await _pSummaryService.ExportToExcelAsync(reportData);

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"ProductSummary_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            );
        }

        [HttpPost]
        public IActionResult ToggleLocation()
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult ToggleSize()
        {
            return Json(new { success = true });
        }
    }
} 