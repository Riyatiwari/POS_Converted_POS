using System;

namespace Converted_POS.Models
{
    public class ProductSummaryReport
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductGroup { get; set; }
        public decimal Price { get; set; }
        public decimal SalesQuantity { get; set; }
        public decimal SalesQuantityOther { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalVolumeSold { get; set; }
        public decimal TotalQuantitySold { get; set; }
        public string Size { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        
        // Properties to fix the missing definition errors
        public decimal Quantity => SalesQuantity;
        public decimal Amount => TotalAmount;
    }

    public class ProductDetailReport
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string LocationName { get; set; }
        public string TillName { get; set; }
        public string StaffName { get; set; }
        public string Size { get; set; }
    }
} 