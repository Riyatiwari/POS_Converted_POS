using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        
        public int TransactionId { get; set; }
        
        public int ProductId { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal DiscountAmount { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public virtual Transaction Transaction { get; set; }
        
        public virtual Product Product { get; set; }
    }
} 