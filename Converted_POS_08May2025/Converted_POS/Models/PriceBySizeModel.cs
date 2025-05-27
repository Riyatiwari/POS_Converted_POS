using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class PriceBySizeModel
    {
        public int SizeId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Size name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string Size { get; set; }
        
        public int? ProductId { get; set; }
        
        public int? UnitId { get; set; }
        
        // Price-related properties
        public int? PriceId { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        
        public int? LocationId { get; set; }
        
        public decimal ActualPrice { get; set; }
        
        public decimal Tax { get; set; }
        
        public decimal? TaxId { get; set; }
        
        public decimal? Tax2 { get; set; }
        
        public decimal? TaxId2 { get; set; }
        
        public int? ProductPriceId { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool Deliver { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        public int? SortingNo { get; set; }
        
        // Additional properties for price levels
        public int? FromLevelId { get; set; }
        
        public int? ToLevelId { get; set; }
        
        public int? PriceType { get; set; }
        
        public decimal? PriceValue { get; set; }
        
        // Cost related properties
        public int? CostId { get; set; }
        
        public decimal? Cost { get; set; }
        
        [StringLength(1)]
        public string Action { get; set; }
    }
} 