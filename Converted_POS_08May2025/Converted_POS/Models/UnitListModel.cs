using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class UnitListModel
    {
        public int UnitId { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Unit name is required")]
        [StringLength(50, ErrorMessage = "Unit name cannot exceed 50 characters")]
        public string UnitName { get; set; }
        
        public bool IsActive { get; set; }
        
        public string IpAddress { get; set; }
        
        public int? LoginId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 