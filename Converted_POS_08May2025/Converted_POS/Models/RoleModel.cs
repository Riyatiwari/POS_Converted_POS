using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(100)]
        public string RoleName { get; set; }
        
        public byte? Type { get; set; }
        
        [StringLength(500)]
        public string RoleDetail { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsView { get; set; }
        
        public bool IsAdd { get; set; }
        
        public bool IsEdit { get; set; }
        
        public bool IsDelete { get; set; }
        
        [StringLength(100)]
        public string Data { get; set; }
        
        [StringLength(100)]
        public string FormName { get; set; }
    }
} 