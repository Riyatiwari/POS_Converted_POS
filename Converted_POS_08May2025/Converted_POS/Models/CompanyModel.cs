using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(255)]
        public string Address { get; set; }
        
        public DateTime? StartingDate { get; set; }
        
        [StringLength(100)]
        public string Domain { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? StateId { get; set; }
        
        public int? CityId { get; set; }
        
        public int? LiteVersion { get; set; }
        
        public byte[] Logo { get; set; }
        
        [StringLength(50)]
        public string Code { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        
        [StringLength(20)]
        public string Postal { get; set; }
        
        [StringLength(100)]
        public string Website { get; set; }
        
        [StringLength(50)]
        public string Contact { get; set; }
        
        public int? RegistrationNo { get; set; }
        
        public int? GSTVAT { get; set; }
        
        public int? CSTVAT { get; set; }
        
        public int? ServiceTaxNo { get; set; }
        
        public int? PanNo { get; set; }
        
        [StringLength(100)]
        public string BranchName { get; set; }
        
        [StringLength(50)]
        public string Synchronization { get; set; }
        
        [StringLength(100)]
        public string VenueName { get; set; }
        
        [StringLength(50)]
        public string VatNo { get; set; }
        
        [StringLength(500)]
        public string ReceiptHeader { get; set; }
        
        [StringLength(500)]
        public string ReceiptFooter { get; set; }
        
        public int? LogOffTime { get; set; }
        
        public int? ParSaleParOperator { get; set; }
        
        public int? CurrencyId { get; set; }
        
        [StringLength(100)]
        public string JudoId { get; set; }
        
        [StringLength(100)]
        public string JudoToken { get; set; }
        
        [StringLength(100)]
        public string JudoSecret { get; set; }
        
        [StringLength(20)]
        public string WeekStartDay { get; set; }
        
        public int? ShowChips { get; set; }
        
        public int? BusinessDoneBy { get; set; }
        
        public int? DisplayDeclaration { get; set; }
        
        public int? CheckDuration { get; set; }
        
        public int? IsAddTax2 { get; set; }
        
        public int? IsExclusiveTax { get; set; }
        
        public int? IsPaymentGateway { get; set; }
    }
} 