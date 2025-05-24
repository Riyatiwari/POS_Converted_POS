using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Barcode { get; set; }
        
        public decimal Price { get; set; }
        
        public int? CategoryId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
} 