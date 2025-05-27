using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Store code is required")]
        [StringLength(50)]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Store name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Address { get; set; }
        
        public bool IsActive { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Contact { get; set; }
        
        public int? VenueId { get; set; }
        
        public string VenueName { get; set; }
        
        public string City { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? StateId { get; set; }
        
        public int? CityId { get; set; }
        
        public bool TillAutoLogoff { get; set; }
        
        public bool IsCashback { get; set; }
        
        public bool IsDelivery { get; set; }
        
        public decimal? MinAmount { get; set; }
        
        public string HeaderReceipt { get; set; }
        
        public byte[] LogoImage { get; set; }
        
        public string BackgroundColor { get; set; }
        
        public string FontColor { get; set; }
        
        public string BodyColor { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        public string Theme { get; set; }
        
        public double? DeliveryCharges { get; set; }
        
        public double? ServiceCharges { get; set; }
        
        public string MessageDelivery { get; set; }
        
        public string MessageOrderTable { get; set; }
        
        public bool ScheduleClickAndCollect { get; set; }
        
        public int? FutureBookingDays { get; set; }
        
        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }
        
        public string RedirectUri { get; set; }
    }
} 