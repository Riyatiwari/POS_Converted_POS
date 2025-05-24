using System;
using System.Threading.Tasks;
using Converted_POS.Models;
using Converted_POS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Converted_POS.ViewModels;
// Using fully qualified names to resolve ambiguity
using MSSessionExtensions = Microsoft.AspNetCore.Http.SessionExtensions;

namespace Converted_POS.Controllers
{
    [Authorize]
    public class TransactionReportController : Controller
    {
        private readonly ITransactionReportService _transactionReportService;

        public TransactionReportController(ITransactionReportService transactionReportService)
        {
            _transactionReportService = transactionReportService;
        }

        public async Task<IActionResult> Index()
        {
            var companyId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
            var userId = MSSessionExtensions.GetInt32(HttpContext.Session, "user_id") ?? 0;

            if (companyId == 0)
            {
                return RedirectToAction("Index", "SignIn");
            }

            var viewModel = new ViewModels.TransactionReportViewModel();
            viewModel.CanView = await _transactionReportService.CanViewReportAsync(userId, companyId);

            if (!viewModel.CanView)
            {
                return View(viewModel);
            }

            viewModel.Transactions = await _transactionReportService.GetTransactionDetailsAsync(companyId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            var companyId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
            var userId = MSSessionExtensions.GetInt32(HttpContext.Session, "user_id") ?? 0;

            if (companyId == 0)
            {
                return RedirectToAction("Index", "SignIn");
            }

            var canView = await _transactionReportService.CanViewReportAsync(userId, companyId);
            if (!canView)
            {
                return Unauthorized();
            }

            var transactions = await _transactionReportService.GetTransactionDetailsAsync(companyId);
            var excelBytes = await _transactionReportService.ExportToExcelAsync(transactions);

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"TransactionReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            );
        }
    }
} 