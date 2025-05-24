using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Converted_POS.ViewModels
{
    public class TillSummaryReportViewModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; } = DateTime.Today;

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; } = DateTime.Today;

        [Display(Name = "Duration")]
        public string Duration { get; set; } = "Today";

        [Display(Name = "Venue")]
        public string VenueId { get; set; } = "0";

        [Display(Name = "Location")]
        public string LocationId { get; set; } = "0";

        [Display(Name = "Till")]
        public string TillId { get; set; } = "0";
        
        [Display(Name = "Machine")]
        public string MachineId { get; set; } = "0";

        public int CompanyId { get; set; }
        public string DisplayReport { get; set; } = "0";
        public bool CanView { get; set; } = true;
        public bool ShowCustomDateRange { get; set; }

        // Properties for report data
        public decimal TotalAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal OtherAmount { get; set; }
        
        // Dropdown data
        public IEnumerable<SelectListItem> Venues { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Locations { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Tills { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Machines { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Durations { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DisplayReportTypes { get; set; } = new List<SelectListItem>();

        // Report data
        public DataTable ReportData { get; set; }
        public List<TillSummaryRow> ReportRows { get; set; } = new List<TillSummaryRow>();
        public List<TillSummaryDetail> Details { get; set; } = new List<TillSummaryDetail>();

        // Durations for dropdown
        public List<SelectListItem> DurationOptions => new List<SelectListItem>
        {
            new SelectListItem { Text = "Today", Value = "Today" },
            new SelectListItem { Text = "Yesterday", Value = "Yesterday" },
            new SelectListItem { Text = "This Week", Value = "This Week" },
            new SelectListItem { Text = "This Month", Value = "This Month" },
            new SelectListItem { Text = "This Year", Value = "This Year" },
            new SelectListItem { Text = "Last Week", Value = "Last Week" },
            new SelectListItem { Text = "Last Month", Value = "Last Month" },
            new SelectListItem { Text = "Last Year", Value = "Last Year" },
            new SelectListItem { Text = "Custom", Value = "Custom" }
        };
    }

    public class TillSummaryRow
    {
        public string Venue { get; set; }
        
        public string Location { get; set; }
        
        public string Till { get; set; }
        
        public decimal SaleCount { get; set; }
        
        public decimal ReturnCount { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal NetAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal Cash { get; set; }
        
        public decimal Card { get; set; }
        
        public decimal Other { get; set; }
    }

    public class TillSummaryFilterModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LocationId { get; set; }
        public int MachineId { get; set; }
        public int VenueId { get; set; }
        public string Duration { get; set; }
        public string DisplayReport { get; set; }
    }

    public class TillSummaryDetail
    {
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string LocationName { get; set; }
        public string MachineName { get; set; }
        public string VenueName { get; set; }
    }
} 