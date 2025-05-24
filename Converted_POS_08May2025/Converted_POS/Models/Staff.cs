using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public int? UserId { get; set; }
        
        public int? RoleId { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public int CompanyId { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
} 