using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class CourseListModel
    {
        public int CourseId { get; set; }
        
        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Value is required")]
        public int Value { get; set; }
        
        public int CompanyId { get; set; }
        
        public string IpAddress { get; set; }
        
        public int LoginId { get; set; }
        
        public int CourseCategoryId { get; set; }
        
        public string CourseCategoryName { get; set; }
        
        public bool IsCheckSlot { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 