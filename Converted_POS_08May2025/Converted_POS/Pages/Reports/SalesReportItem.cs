using System;

namespace Converted_POS.Pages.Reports
{
    public class SalesReportItem
    {
        public DateTime Date { get; set; }
        public string Till { get; set; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public decimal TotalAmount { get; set; }
        public string FormattedTotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string FormattedTotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public string FormattedNetAmount { get; set; }
        public string Payment { get; set; }
        public string SaleType { get; set; }
        public string PayType { get; set; }
        public string Srno { get; set; }
        public string TableUuid { get; set; }
        public string TranUuid { get; set; }
        public int SalesId { get; set; }
    }
}