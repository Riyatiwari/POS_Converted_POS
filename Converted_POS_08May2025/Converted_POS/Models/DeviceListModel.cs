using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class DeviceListModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        [Required(ErrorMessage = "Device name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string SerialNumber { get; set; }
        
        [StringLength(100)]
        public string Model { get; set; }
        
        [StringLength(50)]
        public string DeviceType { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; }
        
        public int? PaymentProviderId { get; set; }
        
        [StringLength(100)]
        public string PaymentProviderName { get; set; }
        
        [StringLength(200)]
        public string ApiKey { get; set; }
        
        [StringLength(200)]
        public string ApiSecret { get; set; }
        
        [StringLength(200)]
        public string MerchantId { get; set; }
        
        [StringLength(200)]
        public string TerminalId { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string CompanyName { get; set; }
        
        public string LocationName { get; set; }
    }
} 