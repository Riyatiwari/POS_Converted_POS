using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class ShiftModel
    {
        public int TillShiftId { get; set; }
        
        public int? MachineId { get; set; }
        
        [Required(ErrorMessage = "Shift name is required")]
        [StringLength(100)]
        public string ShiftName { get; set; }
        
        public bool Active { get; set; }
        
        public int CompanyId { get; set; }
        
        public int? ShiftNo { get; set; }
        
        public int? VenueId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifyDate { get; set; }
    }
} 