using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class PrinterDetailsByMachineModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Machine ID is required")]
        public int MachineId { get; set; }
        
        [Required(ErrorMessage = "Printer ID is required")]
        public int PrinterId { get; set; }
        
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
        
        public string MachineName { get; set; }
        
        public string PrinterName { get; set; }
        
        public string PrinterPath { get; set; }
        
        public string PrinterType { get; set; }
        
        [StringLength(100)]
        public string IPAddress { get; set; }
        
        [StringLength(10)]
        public string Port { get; set; }
    }
} 