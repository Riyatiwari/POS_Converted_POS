using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class BuyingSizeModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Buying size name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Size is required")]
        public decimal Size { get; set; }
        
        [Required(ErrorMessage = "Unit is required")]
        [StringLength(20)]
        public string Unit { get; set; }
        
        public decimal Cost { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string ProductName { get; set; }
    }
} 