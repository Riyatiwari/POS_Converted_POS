using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class StaffRulesDetailModel
    {
        public int MStaffId { get; set; }
        
        public int CompanyId { get; set; }
        
        public int FunctionTypeId { get; set; }
        
        [StringLength(100)]
        public string FunctionName { get; set; }
    }
} 