using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class SizeModel
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
        
        public bool IsActive { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool Deliver { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        public int? SortingNo { get; set; }
        
        // Price-related properties
        public int? PriceId { get; set; }
        
        public decimal? Price { get; set; }
        
        public int? LocationId { get; set; }
        
        public decimal? ActualPrice { get; set; }
        
        public decimal? Tax { get; set; }
        
        public decimal? TaxId { get; set; }
        
        public decimal? Tax2 { get; set; }
        
        public decimal? TaxId2 { get; set; }
        
        public int? ProductPriceId { get; set; }
        
        public string Action { get; set; }
    }
} 