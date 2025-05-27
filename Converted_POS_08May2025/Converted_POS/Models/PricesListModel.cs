using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class PricesListModel
    {
        public int ProductPriceId { get; set; }
        
        [Required(ErrorMessage = "Price name is required")]
        [StringLength(100)]
        public string PriceName { get; set; }
        
        public int CompanyId { get; set; }
        
        public int? LoginId { get; set; }
        
        public string IpAddress { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifyDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsDefault { get; set; }
        
        // Additional properties for price details
        public int? PriceId { get; set; }
        
        public decimal? Price { get; set; }
        
        public int? LocationId { get; set; }
        
        public int? SizeId { get; set; }
        
        public decimal? ActualPrice { get; set; }
        
        public decimal? Tax { get; set; }
        
        public int? ProductId { get; set; }
        
        [StringLength(1)]
        public string TranType { get; set; }
    }
} 