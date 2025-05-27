using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class StaffRulesMasterModel
    {
        public int MStaffId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
    }
} 