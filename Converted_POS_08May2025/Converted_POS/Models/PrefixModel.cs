using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class PrefixModel
    {
        public int PrefixId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Prefix is required")]
        [StringLength(50)]
        public string Prefix { get; set; }
        
        public bool IsActive { get; set; }
    }
} 