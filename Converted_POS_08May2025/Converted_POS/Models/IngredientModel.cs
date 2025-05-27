using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Ingredient name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        public decimal Quantity { get; set; }
        
        [StringLength(20)]
        public string Unit { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string CompanyName { get; set; }
        
        public string LocationName { get; set; }
        
        public string ProductName { get; set; }
    }
} 