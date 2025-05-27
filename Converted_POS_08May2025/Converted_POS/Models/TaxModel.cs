using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class TaxModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        [Required(ErrorMessage = "Tax name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Tax rate is required")]
        public decimal Rate { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsIncluded { get; set; }
        
        public bool IsDefault { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string CompanyName { get; set; }
        
        public string LocationName { get; set; }
    }
} 