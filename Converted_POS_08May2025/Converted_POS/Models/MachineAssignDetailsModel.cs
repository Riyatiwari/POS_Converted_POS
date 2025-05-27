using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class MachineAssignDetailsModel
    {
        public int MachineDetailId { get; set; }
        
        [Required(ErrorMessage = "Machine ID is required")]
        public int MachineId { get; set; }
        
        public int? PrinterId { get; set; }
        
        public int? DeviceId { get; set; }
        
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }
        
        public string IpAddress { get; set; }
        
        public int? LoginId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        // Navigation properties
        public string MachineName { get; set; }
        
        public string PrinterName { get; set; }
        
        public string DeviceName { get; set; }
    }
} 