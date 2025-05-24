using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }
        
        public int VenueId { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual Venue Venue { get; set; }
        
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        
        public virtual ICollection<Till> Tills { get; set; } = new List<Till>();
    }
} 