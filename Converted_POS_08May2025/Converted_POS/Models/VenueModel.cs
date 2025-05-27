using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class VenueModel
    {
        public int VenueId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Venue name is required")]
        [StringLength(100)]
        public string VenueName { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public int? SortingNo { get; set; }
        
        [StringLength(100)]
        public string MacId { get; set; }
        
        public bool PrintReceipt { get; set; }
        
        public bool PrintDuplicateReceipt { get; set; }
        
        public int? MachineId { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(50)]
        public string DateTime { get; set; }
        
        public bool GroupBy { get; set; }
        
        public bool ConsileDate { get; set; }
        
        public int? GroupByWith { get; set; }
    }
} 