using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class TableListModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Table name is required")]
        [StringLength(100, ErrorMessage = "Table name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        public string LocationName { get; set; }
        
        [Required(ErrorMessage = "Minimum cover is required")]
        [Range(1, 100, ErrorMessage = "Minimum cover must be between 1 and 100")]
        public int MinCover { get; set; }
        
        [Required(ErrorMessage = "Maximum cover is required")]
        [Range(1, 100, ErrorMessage = "Maximum cover must be between 1 and 100")]
        public int MaxCover { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsOpen { get; set; }
        
        public int SortingNo { get; set; }
        
        public string IpAddress { get; set; }
        
        public int LoginId { get; set; }
        
        public int? MachineId { get; set; }
        
        public int? ShapeId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 