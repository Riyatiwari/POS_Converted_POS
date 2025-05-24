using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Models
{
    public class ZReportViewModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.Today;

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.Today;

        [Display(Name = "Duration")]
        public string Duration { get; set; } = "Today";

        [Display(Name = "Venue")]
        public int? VenueId { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Machine/Till")]
        public int? MachineId { get; set; }

        [Display(Name = "Sales Type")]
        public string SalesType { get; set; } = "All";

        [Display(Name = "Shift Type")]
        public string ShiftName { get; set; }

        [Display(Name = "Operator")]
        public int? OperatorId { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        // Receipt header from the report
        public string ReceiptHeader { get; set; }

        // First transaction date
        public string FirstTransactionDate { get; set; }

        // Last transaction date
        public string LastTransactionDate { get; set; }

        // Number of sales
        public string NumberOfSales { get; set; }

        // Number of returns
        public string NumberOfReturns { get; set; }

        // Dropdown data
        public IEnumerable<SelectListItem> Venues { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Locations { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Machines { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Shifts { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Operators { get; set; } = new List<SelectListItem>();

        // Report data
        public DataTable ReportData { get; set; } = new DataTable();

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

        // Sales types for dropdown
        public List<SelectListItem> SalesTypeOptions => new List<SelectListItem>
        {
            new SelectListItem { Text = "All", Value = "All" },
            new SelectListItem { Text = "Sale", Value = "Sale" },
            new SelectListItem { Text = "Return", Value = "Return" }
        };
    }
} 