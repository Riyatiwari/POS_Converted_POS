using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class VoucherListModel
    {
        public int VoucherId { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Voucher name is required")]
        [StringLength(100, ErrorMessage = "Voucher name cannot exceed 100 characters")]
        public string VoucherName { get; set; }
        
        [Required(ErrorMessage = "Voucher type is required")]
        [StringLength(50, ErrorMessage = "Voucher type cannot exceed 50 characters")]
        public string VoucherType { get; set; }
        
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        
        public bool IsEndless { get; set; }
        
        [StringLength(50)]
        public string VoucherDuration { get; set; }
        
        public string IpAddress { get; set; }
        
        public int LoginId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
    }
} 