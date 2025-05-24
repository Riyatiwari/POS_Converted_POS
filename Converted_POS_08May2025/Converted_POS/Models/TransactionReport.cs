using System;

namespace Converted_POS.Models
{
    public class TransactionReport
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public string Till { get; set; }
        
        public string UserName { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string PaymentType { get; set; }
    }
} 