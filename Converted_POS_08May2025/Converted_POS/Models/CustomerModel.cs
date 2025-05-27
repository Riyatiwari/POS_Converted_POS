using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [StringLength(100)]
        public string LastName { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string ContactNo { get; set; }
        
        [StringLength(20)]
        public string AlternetContact { get; set; }
        
        [StringLength(500)]
        public string Address { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? StateId { get; set; }
        
        public int? CityId { get; set; }
        
        [StringLength(20)]
        public string PostalCode { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifyDate { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(100)]
        public string OtherId { get; set; }
        
        public int? MachineId { get; set; }
        
        public int? ProfileId { get; set; }
        
        public int? AccountId { get; set; }
        
        public bool IsCredit { get; set; }
        
        [StringLength(50)]
        public string CardNumber { get; set; }
        
        public DateTime? ExpDate { get; set; }
        
        public byte[] CustomerProfile { get; set; }
        
        public int? PriceLevel { get; set; }
    }
} 