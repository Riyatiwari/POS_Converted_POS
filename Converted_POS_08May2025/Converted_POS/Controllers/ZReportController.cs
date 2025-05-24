using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Converted_POS.Models;
using Converted_POS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Converted_POS.Helpers;

namespace Converted_POS.Controllers
{
    [Authorize]
    public class ZReportController : Controller
    {
        private readonly IZReportService _zReportService;

        public ZReportController(IZReportService zReportService)
        {
            _zReportService = zReportService;
        }

        public async Task<IActionResult> Index(
            string duration = "Today",
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? venueId = null,
            int? locationId = null,
            int? machineId = null,
            string salesType = "All",
            string shiftName = null,
            int? operatorId = null)
        {
            if (!fromDate.HasValue)
                fromDate = DateTime.Today;

            if (!toDate.HasValue)
                toDate = DateTime.Today;

            var viewModel = new ZReportViewModel
            {
                FromDate = fromDate.Value,
                ToDate = toDate.Value,
                Duration = duration,
                VenueId = venueId,
                LocationId = locationId,
                MachineId = machineId,
                SalesType = salesType,
                ShiftName = shiftName,
                OperatorId = operatorId
            };

            // Get the company ID from the user's session
            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");

            // Load dropdown data
            viewModel.Venues = await _zReportService.GetVenuesAsync(companyId);
            
            if (venueId.HasValue)
                viewModel.Locations = await _zReportService.GetLocationsByVenueAsync(companyId, venueId.Value);
            
            if (locationId.HasValue)
                viewModel.Machines = await _zReportService.GetMachinesByLocationAsync(companyId, locationId.Value);
            
            viewModel.Operators = await _zReportService.GetOperatorsAsync(companyId);
            viewModel.Shifts = await _zReportService.GetShiftsByDurationAsync(companyId, duration, fromDate.Value, toDate.Value);

            // Get report data
            viewModel.ReportData = await _zReportService.GetZReportDataAsync(
                companyId,
                fromDate.Value,
                toDate.Value,
                duration,
                locationId,
                machineId,
                venueId,
                salesType,
                shiftName,
                operatorId);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetLocations(int venueId)
        {
            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");
            var locations = await _zReportService.GetLocationsByVenueAsync(companyId, venueId);
            return Json(locations);
        }

        [HttpPost]
        public async Task<IActionResult> GetMachines(int locationId)
        {
            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");
            var machines = await _zReportService.GetMachinesByLocationAsync(companyId, locationId);
            return Json(machines);
        }

        [HttpPost]
        public async Task<IActionResult> GetShifts(string duration, DateTime fromDate, DateTime toDate)
        {
            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");
            var shifts = await _zReportService.GetShiftsByDurationAsync(companyId, duration, fromDate, toDate);
            return Json(shifts);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(
            string duration = "Today",
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? venueId = null,
            int? locationId = null,
            int? machineId = null,
            string salesType = "All",
            string shiftName = null,
            int? operatorId = null)
        {
            if (!fromDate.HasValue)
                fromDate = DateTime.Today;

            if (!toDate.HasValue)
                toDate = DateTime.Today;

            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");

            var excelBytes = await _zReportService.ExportToExcelAsync(
                companyId,
                fromDate.Value,
                toDate.Value,
                duration,
                locationId,
                machineId,
                venueId,
                salesType,
                shiftName,
                operatorId);

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"ZReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(
            string email,
            string duration = "Today",
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? venueId = null,
            int? locationId = null,
            int? machineId = null,
            string salesType = "All",
            string shiftName = null,
            int? operatorId = null)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { success = false, message = "Email address is required." });

            if (!fromDate.HasValue)
                fromDate = DateTime.Today;

            if (!toDate.HasValue)
                toDate = DateTime.Today;

            var companyId = int.Parse(HttpContext.Session.GetSessionString("cmp_id") ?? "0");

            var success = await _zReportService.SendZReportByEmailAsync(
                email,
                companyId,
                fromDate.Value,
                toDate.Value,
                duration,
                locationId,
                machineId,
                venueId,
                salesType,
                shiftName,
                operatorId);

            return Json(new { success, message = success ? "Email sent successfully!" : "Failed to send email." });
        }
    }
} 