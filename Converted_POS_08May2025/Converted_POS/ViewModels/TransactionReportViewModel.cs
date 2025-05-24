using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Converted_POS.Models;

namespace Converted_POS.ViewModels
{
    public class TransactionReportViewModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.Today;

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.Today;

        [Display(Name = "Duration")]
        public string Duration { get; set; } = "Today";

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Till")]
        public List<int> TillIds { get; set; } = new List<int>();

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; } = "ALL";

        [Display(Name = "Group By")]
        public string GroupBy { get; set; } = "date"; // date, till, operator, payment

        public bool CanView { get; set; } = true;

        // Dropdown data
        public IEnumerable<SelectListItem> Locations { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Tills { get; set; } = new List<SelectListItem>();

        // Report data
        public DataTable ReportData { get; set; }

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

        // Payment types for dropdown
        public List<SelectListItem> PaymentTypeOptions => new List<SelectListItem>
        {
            new SelectListItem { Text = "All Types", Value = "ALL" },
            new SelectListItem { Text = "Cash", Value = "CASH" },
            new SelectListItem { Text = "Card", Value = "CARD" },
            new SelectListItem { Text = "Other", Value = "OTHER" }
        };

        // Group by options for radio buttons
        public List<SelectListItem> GroupByOptions => new List<SelectListItem>
        {
            new SelectListItem { Text = "Date", Value = "date", Selected = (GroupBy == "date") },
            new SelectListItem { Text = "Till", Value = "till", Selected = (GroupBy == "till") },
            new SelectListItem { Text = "Operator", Value = "operator", Selected = (GroupBy == "operator") },
            new SelectListItem { Text = "Payment Type", Value = "payment", Selected = (GroupBy == "payment") }
        };

        public List<Converted_POS.Models.TransactionDetailRow> Transactions { get; set; } = new List<Converted_POS.Models.TransactionDetailRow>();
    }

    public class TransactionDetail
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string LocationName { get; set; }
        public string TillName { get; set; }
        public string OperatorName { get; set; }
        public string TransactionNumber { get; set; }
    }
} 