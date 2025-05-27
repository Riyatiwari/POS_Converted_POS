using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class FunctionModel
    {
        public int FunctionId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Function name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(50)]
        public string Code { get; set; }
        
        [StringLength(50)]
        public string CaptionType { get; set; }
        
        [StringLength(50)]
        public string ShortingNo { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(100)]
        public string FormName { get; set; }
        
        [StringLength(50)]
        public string BackColor { get; set; }
        
        [StringLength(50)]
        public string ForColor { get; set; }
        
        public int? MachineId { get; set; }
        
        public int? PaymentId { get; set; }
        
        public bool BigButton { get; set; }
        
        public int? PayType { get; set; }
        
        [StringLength(50)]
        public string PaySubType { get; set; }
        
        public bool IsGroupByDept { get; set; }
        
        public bool IsGroupByCourse { get; set; }
        
        [StringLength(100)]
        public string DeptId { get; set; }
        
        [StringLength(100)]
        public string CourseId { get; set; }
        
        public int? PanelId { get; set; }
        
        public int? ParentId { get; set; }
        
        public int? MainFunctionId { get; set; }
        
        [StringLength(100)]
        public string PlatformAdd { get; set; }
        
        [StringLength(200)]
        public string ClientToken { get; set; }
        
        [StringLength(200)]
        public string AccessToken { get; set; }
        
        [StringLength(100)]
        public string ServiceId { get; set; }
        
        public int? TaxId { get; set; }
        
        public decimal? EOHelpOutMaxAmountEach { get; set; }
        
        public decimal? TotalValue { get; set; }
        
        public decimal? SalesHandlingFee { get; set; }
        
        public decimal? PaymentHandlingFee { get; set; }
        
        public decimal? TaxAmount { get; set; }
        
        public int? ProfileId { get; set; }
        
        [StringLength(50)]
        public string DefaultDateTime { get; set; }
        
        public int? ZRVenueID { get; set; }
        
        public int? ZRLocationID { get; set; }
        
        [StringLength(50)]
        public string ZRTillID { get; set; }
        
        public int? CardPayType { get; set; }
    }
} 