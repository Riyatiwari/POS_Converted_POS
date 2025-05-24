using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages.Reports
{
    public class SalesReportModel : PageModel
    {
        SalesReportController obj = new SalesReportController();
        [BindProperty]
        public clsSalesReport salesreport { get; set; }
        public IEnumerable<SelectListItem> DTLocation { get; set; }
        public IEnumerable<SelectListItem> DTVenue { get; set; }
        public IEnumerable<SalesReportItem> DTBindTable { get; set; }
        public IEnumerable<SelectListItem> DTTable { get; set; }
        public string ConnStr { get; set; }
        public string cmp_id { get; set; }
        public int venue_id { get; set; }
        public string table_name { get; set; }
        public string sales_id { get; set; }
        public string TranUuid { get; set; }
        public string SaleType { get; set; }
        public string PayType { get; set; }

        //public string Date { get; set; }
        //public string Till { get; set; }
        //public string UserName { get; set; }
        //public string TableName { get; set; }
        //public decimal TotalAmount { get; set; }
        //public decimal TotalDiscount { get; set; }
        //public decimal NetAmount { get; set; }
        //public string Payment { get; set; }
        //public string SaleType { get; set; }
        //public string Srno { get; set; }
        //public string TableUuid { get; set; }
        //public string TranUuid { get; set; }
        //public int SalesId { get; set; }
        DateTime? txtForDate = DateTime.Now;
        DateTime? txtToDate = DateTime.Now;
        public IActionResult OnGet(int? id, string connStr, string locationId, string venueId, string selectedDuration, DateTime? fromDate, DateTime? toDate, string tranUuid, string saleType)
        {
            if (HttpContext.Session.GetString("cmp_id") == null)
            {
                return RedirectToPage("/SignIn");
            }

            ConnStr = HttpContext.Session.GetString("conString");
            cmp_id = HttpContext.Session.GetString("cmp_id");

            DateTime? txtForDate = null;
            DateTime? txtToDate = null;

            if (selectedDuration == "Custom")
            {
                txtForDate = fromDate;  // Get the custom 'From Date' value
                txtToDate = toDate;     // Get the custom 'To Date' value
            }
            else
            {
                // Set default values based on the selected duration
                switch (selectedDuration)
                {
                    case "Today":
                        txtForDate = DateTime.Now.Date;
                        txtToDate = DateTime.Now.Date;
                        break;
                    case "Yesterday":
                        txtForDate = DateTime.Now.AddDays(-1).Date;
                        txtToDate = DateTime.Now.AddDays(-1).Date;
                        break;
                    case "This Week":
                        var currentWeekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                        txtForDate = currentWeekStart.Date;
                        txtToDate = currentWeekStart.AddDays(6).Date;
                        break;
                    case "This Month":
                        var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        txtForDate = currentMonthStart.Date;
                        txtToDate = currentMonthStart.AddMonths(1).AddDays(-1).Date;
                        break;
                    case "This Year":
                        var currentYearStart = new DateTime(DateTime.Now.Year, 1, 1);
                        txtForDate = currentYearStart.Date;
                        txtToDate = currentYearStart.AddYears(1).AddDays(-1).Date;
                        break;
                    default:
                        txtForDate = DateTime.Now.Date;
                        txtToDate = DateTime.Now.Date;
                        break;
                }
            }

            DTVenue = obj.GetVenue(Convert.ToInt32(cmp_id), ConnStr);
            DTLocation = obj.GetLocation(Convert.ToInt32(cmp_id), ConnStr);
            DTBindTable = obj.BindTable(Convert.ToInt32(cmp_id), ConnStr, fromDate, toDate, locationId, venueId, selectedDuration);
            //DTTable = (IEnumerable<SelectListItem>)obj.ViewDetail(id, ConnStr, tranUuid, saleType);
            var selectListItems = obj.ViewDetail(id, ConnStr, tranUuid, saleType);

            // Extract SelectListItems from JsonResult (if needed)
            if (selectListItems is JsonResult jsonResult)
            {
                var data = jsonResult.Value as List<SelectListItem>;
                DTTable = data ?? new List<SelectListItem>(); // Ensure DTTable is properly assigned
            }

            ViewData["SelectListItems"] = DTBindTable;
            
            return Page();
        }
    }
}
