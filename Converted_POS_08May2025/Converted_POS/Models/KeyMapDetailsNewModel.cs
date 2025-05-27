using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class KeyMapDetailsNewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Key map ID is required")]
        public int KeyMapId { get; set; }
        
        public int CompanyId { get; set; }
        
        public int? ProductGroupId { get; set; }
        
        public int? ProductId { get; set; }
        
        public int? SizeId { get; set; }
        
        [StringLength(50)]
        public string BackgroundColor { get; set; }
        
        [StringLength(50)]
        public string ForegroundColor { get; set; }
        
        [StringLength(50)]
        public string FontColor { get; set; }
        
        [StringLength(50)]
        public string Matrix { get; set; }
        
        [StringLength(100)]
        public string DisplayName { get; set; }
        
        [StringLength(50)]
        public string ButtonStyle { get; set; }
        
        [StringLength(50)]
        public string ImageOption { get; set; }
        
        [StringLength(50)]
        public string ImageStyle { get; set; }
        
        public int? MainCategoryId { get; set; }
        
        public bool IsActive { get; set; }
        
        public string IpAddress { get; set; }
        
        public int LoginId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        // Navigation properties for related data
        public string KeyMapName { get; set; }
        
        public string ProductGroupName { get; set; }
        
        public string ProductName { get; set; }
        
        public string SizeName { get; set; }
        
        public string MainCategoryName { get; set; }
    }
} 