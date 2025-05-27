using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }
        
        [StringLength(10)]
        public string CountryCode { get; set; }
    }

    public class StateModel
    {
        public int StateId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string StateName { get; set; }
        
        public int CountryId { get; set; }
    }

    public class CityModel
    {
        public int CityId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CityName { get; set; }
        
        public int StateId { get; set; }
    }

    public class VenueModel
    {
        public int VenueId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class TaxModel
    {
        public int TaxId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public decimal TaxValue { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(20)]
        public string Code { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class CourseModel
    {
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
    }

    public class StaffModel
    {
        public int StaffId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Mobile { get; set; }
        
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
    }
} 