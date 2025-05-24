using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.Services.Interfaces;
using Converted_POS.Services;
using Converted_POS.ViewModels;
// Using fully qualified names to resolve ambiguity
using MSSessionExtensions = Microsoft.AspNetCore.Http.SessionExtensions;

namespace Converted_POS.Controllers
{
    [Authorize]
    public class TillSummaryReportController : BaseController
    {
        private readonly IBindingService _bindingService;
        private readonly IRoleService _roleService;
        private readonly Services.ITillSummaryService _tillSummaryService;
        private readonly ILogger<TillSummaryReportController> _logger;

        public TillSummaryReportController(
            IBindingService bindingService,
            IRoleService roleService,
            Services.ITillSummaryService tillSummaryService,
            ILogger<TillSummaryReportController> logger) 
            : base(bindingService, roleService, logger)
        {
            _bindingService = bindingService;
            _roleService = roleService;
            _tillSummaryService = tillSummaryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                int? cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id");
                if (!cmpId.HasValue)
                {
                    return RedirectToAction("SignIn", "Account");
                }

                var viewModel = new ViewModels.TillSummaryReportViewModel
                {
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    CanView = await CheckRoleAccess()
                };

                if (viewModel.CanView)
                {
                    await BindDropdowns(viewModel);
                    viewModel.ReportData = await GetReportData();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("TillSummaryReport:Index: {Message}", ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetReport([FromBody] TillSummaryFilterModel filter)
        {
            try
            {
                int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
                var reportData = await _tillSummaryService.GetReportDataAsync(
                    new ViewModels.TillSummaryReportViewModel {
                        CompanyId = cmpId,
                        FromDate = filter.FromDate,
                        ToDate = filter.ToDate,
                        LocationId = filter.LocationId.ToString(),
                        MachineId = filter.MachineId.ToString(),
                        VenueId = filter.VenueId.ToString(),
                        Duration = filter.Duration
                    });

                return Json(new { success = true, data = reportData });
            }
            catch (Exception ex)
            {
                _logger.LogError("TillSummaryReport:GetReport: {Message}", ex.Message);
                return Json(new { success = false, message = "Error processing request" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLocationsByVenue(int venueId)
        {
            try
            {
                int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
                var locations = await _bindingService.GetLocationsAsync(cmpId, venueId);
                return Json(new { success = true, data = locations });
            }
            catch (Exception ex)
            {
                _logger.LogError("TillSummaryReport:GetLocationsByVenue: {Message}", ex.Message);
                return Json(new { success = false, message = "Error loading locations" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMachinesByLocation(int locationId)
        {
            try
            {
                int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
                var machines = await _bindingService.GetTillsAsync(cmpId, locationId);
                return Json(new { success = true, data = machines });
            }
            catch (Exception ex)
            {
                _logger.LogError("TillSummaryReport:GetMachinesByLocation: {Message}", ex.Message);
                return Json(new { success = false, message = "Error loading machines" });
            }
        }

        private async Task<bool> CheckRoleAccess()
        {
            int staffRoleId = MSSessionExtensions.GetInt32(HttpContext.Session, "staff_role_id") ?? 0;
            if (staffRoleId == 0)
                return true;

            int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
            var roleAccess = await _roleService.CheckAccessAsync(
                companyId: cmpId,
                roleId: staffRoleId,
                formName: "Till Summary"
            );

            return roleAccess.CanView;
        }

        private async Task<ViewModels.TillSummaryReportViewModel> BindDropdowns(ViewModels.TillSummaryReportViewModel viewModel)
        {
            int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
            viewModel.Venues = await _bindingService.GetVenuesAsync(cmpId);
            viewModel.Durations = GetDurationsList();
            viewModel.DisplayReportTypes = GetDisplayReportTypes();
            
            // Initialize Machines collection with default "All Machines" option
            viewModel.Machines = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "All Machines" }
            };
            
            return viewModel;
        }

        private async Task<DataTable> GetReportData()
        {
            int cmpId = MSSessionExtensions.GetInt32(HttpContext.Session, "cmp_id") ?? 0;
            return await _tillSummaryService.GetReportDataAsync(
                new ViewModels.TillSummaryReportViewModel {
                    CompanyId = cmpId,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    LocationId = "0",
                    MachineId = "0", // Using MachineId instead of TillId
                    VenueId = "0",
                    Duration = "Today"
                });
        }

        private List<SelectListItem> GetDurationsList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Today", Text = "Today" },
                new SelectListItem { Value = "Yesterday", Text = "Yesterday" },
                new SelectListItem { Value = "ThisWeek", Text = "This Week" },
                new SelectListItem { Value = "LastWeek", Text = "Last Week" },
                new SelectListItem { Value = "ThisMonth", Text = "This Month" },
                new SelectListItem { Value = "LastMonth", Text = "Last Month" },
                new SelectListItem { Value = "Custom", Text = "Custom" }
            };
        }

        private List<SelectListItem> GetDisplayReportTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Summary" },
                new SelectListItem { Value = "1", Text = "Detailed" }
            };
        }
    }
} 