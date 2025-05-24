using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Till
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string TillNumber { get; set; }
        
        public int LocationId { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual Location Location { get; set; }
        
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
} 