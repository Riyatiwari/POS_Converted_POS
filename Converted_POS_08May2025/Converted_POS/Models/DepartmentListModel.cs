using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class DepartmentListModel
    {
        public int DepartmentId { get; set; }
        
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Value is required")]
        public int Value { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        public int? DepartmentCategoryId { get; set; }
        
        public string DepartmentCategoryName { get; set; }
        
        public bool IsActive { get; set; }
        
        public string IpAddress { get; set; }
        
        public int? LoginId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 