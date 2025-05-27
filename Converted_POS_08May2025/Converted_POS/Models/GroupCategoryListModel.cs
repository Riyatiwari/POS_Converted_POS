using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class GroupCategoryListModel
    {
        public int MainCategoryId { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentId { get; set; }
        
        public string DepartmentName { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string IpAddress { get; set; }
        
        public int? LoginId { get; set; }
        
        public int? MachineId { get; set; }
        
        public int? VenueId { get; set; }
        
        public int SortingNo { get; set; }
        
        public bool IsAvailableOnline { get; set; }
        
        public string LocationId { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool Deliver { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        [StringLength(500)]
        public string WebSalesDescription { get; set; }
        
        public byte[] Image { get; set; }
        
        public string ImageFileName { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 