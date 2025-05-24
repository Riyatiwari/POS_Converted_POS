using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Address { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public int CompanyId { get; set; }
        
        // Navigation properties
        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
    }
} 