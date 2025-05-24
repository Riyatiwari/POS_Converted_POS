using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Converted_POS.ViewModels
{
    // Add extension method to fix 'Rows' error
    public static class PSummaryRowExtensions
    {
        public static List<PSummaryRow> Rows(this List<PSummaryRow> list)
        {
            return list;
        }
    }

    public class PSummaryReportViewModel
    {
        public int CompanyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Duration { get; set; }
        public string LocationId { get; set; }
        public string ProductId { get; set; }
        public string ProductGroupId { get; set; }
        public string TillId { get; set; }
        public string VenueId { get; set; }
        public string CustomerId { get; set; }
        public bool IncludeReturn { get; set; }
        public string TransactionType { get; set; }
        public bool CanView { get; set; }
        
        public List<int> TillIds { get; set; } = new List<int>();
        public bool IncludeReturns { get; set; }
        public string ReportType { get; set; }
        public int? CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ReportTypeOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DurationOptions { get; set; } = new List<SelectListItem>();
        public List<string> SelectedTills { get; set; } = new List<string>();
        public string SizeLocationValue { get; set; }
        public bool ShowSize { get; set; }
        public bool ShowLocation { get; set; }
        public List<SelectListItem> ReportTypes { get; set; } = new List<SelectListItem>();
        
        public List<SelectListItem> Venues { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Locations { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ProductGroups { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Tills { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Durations { get; set; } = new List<SelectListItem>();
        
        public List<PSummaryRow> ReportData { get; set; } = new List<PSummaryRow>();
        
        // Add DataTable property to fix the 'Rows' method group error in views
        public DataTable ReportDataTable { get; set; } = new DataTable();
    }

    public class PSummaryRow
    {
        public string ProductGroup { get; set; }
        
        public string ProductName { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal SaleQuantity { get; set; }
        
        public decimal SaleQuantityOther { get; set; }
        
        public decimal ReturnQuantity { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal NetAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal VolumeSold { get; set; }
        
        public decimal QuantitySold { get; set; }
        
        public string Size { get; set; }
        
        public string Location { get; set; }
        
        public int ProductId { get; set; }
    }
} 