using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Converted_POS.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        
        public string TransactionNumber { get; set; }
        
        public int ProductId { get; set; }
        
        public int TillId { get; set; }
        
        public int UserId { get; set; }
        
        public int LocationId { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal DiscountAmount { get; set; }
        
        public string PaymentType { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public virtual Product Product { get; set; }
        
        public virtual Till Till { get; set; }
        
        public virtual User User { get; set; }
        
        public virtual Location Location { get; set; }
        
        public virtual ICollection<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();
    }
} 