using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
} 