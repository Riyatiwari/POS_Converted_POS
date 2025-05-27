using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class RoleFormModel
    {
        public int FormId { get; set; }
        
        public string FormName { get; set; }
        
        public bool IsView { get; set; }
        
        public bool IsAdd { get; set; }
        
        public bool IsEdit { get; set; }
        
        public bool IsDelete { get; set; }
        
        public int? RoleId { get; set; }
    }
} 