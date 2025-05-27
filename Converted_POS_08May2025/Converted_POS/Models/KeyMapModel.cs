using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class KeyMapModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        [Required(ErrorMessage = "Key name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(50)]
        public string KeyType { get; set; }
        
        public int? TabOrder { get; set; }
        
        public int? ParentId { get; set; }
        
        [StringLength(100)]
        public string ParentName { get; set; }
        
        public string DisplayText { get; set; }
        
        public string BackgroundColor { get; set; }
        
        public string FontColor { get; set; }
        
        public string IconPath { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string CompanyName { get; set; }
        
        public string LocationName { get; set; }
    }
} 