using System;
using System.Collections.Generic;

namespace Converted_POS.Models
{
    public class TransactionDetails
    {
        public int Id { get; set; }
        
        public string TransactionNumber { get; set; }
        
        public string ProductName { get; set; }
        
        public string Till { get; set; }
        
        public string UserName { get; set; }
        
        public string Location { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal DiscountAmount { get; set; }
        
        public string PaymentType { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public List<TransactionItemDetails> Items { get; set; } = new List<TransactionItemDetails>();
    }
    
    public class TransactionItemDetails
    {
        public string ProductName { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal DiscountAmount { get; set; }
    }
} 