using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class ProductGroupModel
    {
        public int CategoryId { get; set; }
        
        public int CompanyId { get; set; }
        
        public int? KeyMapId { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifyDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsWebAvailable { get; set; }
        
        public int? MachineId { get; set; }
        
        public int? SortingNo { get; set; }
        
        public int? MainCategoryId { get; set; }
        
        [StringLength(500)]
        public string StrLocationId { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool Deliver { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        [StringLength(500)]
        public string DescriptionSales { get; set; }
        
        public byte[] Image { get; set; }
    }
} 