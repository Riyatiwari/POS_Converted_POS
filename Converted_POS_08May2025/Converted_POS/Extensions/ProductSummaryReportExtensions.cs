using Converted_POS.Models;
using System;

namespace Converted_POS.Extensions
{
    public static class ProductSummaryReportExtensions
    {
        public static decimal Quantity(this ProductSummaryReport report)
        {
            // Map to appropriate property based on model structure
            return report.SalesQuantity;
        }

        public static decimal Amount(this ProductSummaryReport report)
        {
            // Map to appropriate property based on model structure
            return report.TotalAmount;
        }
    }
} 