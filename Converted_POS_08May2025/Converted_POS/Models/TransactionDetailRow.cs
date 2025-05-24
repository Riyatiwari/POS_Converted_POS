using System;

namespace Converted_POS.Models
{
    public class TransactionDetailRow
    {
        public string Name { get; set; }
        
        public string SizeName { get; set; }
        
        public string Till { get; set; }
        
        public string UserName { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string FullProductName => string.IsNullOrEmpty(SizeName) 
            ? Name 
            : $"{Name} - {SizeName}";
    }
} 