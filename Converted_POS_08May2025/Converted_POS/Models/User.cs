using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public int? RoleId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
} 