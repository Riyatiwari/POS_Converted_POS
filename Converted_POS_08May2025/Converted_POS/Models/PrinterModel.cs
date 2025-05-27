using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class PrinterModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Location ID is required")]
        public int LocationId { get; set; }
        
        [Required(ErrorMessage = "Printer name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string PrinterPath { get; set; }
        
        [StringLength(50)]
        public string PrinterType { get; set; }
        
        public bool IsReceipt { get; set; }
        
        public bool IsKitchen { get; set; }
        
        public bool IsBar { get; set; }
        
        public bool IsLabel { get; set; }
        
        public bool IsDefault { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public string CompanyName { get; set; }
        
        public string LocationName { get; set; }
        
        [StringLength(100)]
        public string IPAddress { get; set; }
        
        [StringLength(10)]
        public string Port { get; set; }
    }
} 