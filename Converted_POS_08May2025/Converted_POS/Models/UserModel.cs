using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class UserModel
    {
        public int StaffId { get; set; }
        
        public int CompanyId { get; set; }
        
        [StringLength(50)]
        public string TillCode { get; set; }
        
        public bool TillActive { get; set; }
        
        [StringLength(255)]
        public string Photo { get; set; }
        
        [StringLength(50)]
        public string StaffCode { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        public DateTime? JoiningDate { get; set; }
        
        public int? BranchId { get; set; }
        
        public int? DepartmentId { get; set; }
        
        public int? DesignationId { get; set; }
        
        [StringLength(500)]
        public string Address { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? StateId { get; set; }
        
        public int? CityId { get; set; }
        
        [StringLength(50)]
        public string NationalId { get; set; }
        
        [StringLength(20)]
        public string ContactNo { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        public DateTime? LastWorkingDate { get; set; }
        
        public int? RoleId { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(100)]
        public string OtherId { get; set; }
        
        public int? MachineId { get; set; }
        
        [StringLength(100)]
        public string AuthenticationCode { get; set; }
        
        [StringLength(100)]
        public string FunctionTypeId { get; set; }
        
        public bool IsTrainee { get; set; }
        
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }
        
        [StringLength(100)]
        public string VenueId { get; set; }
        
        [StringLength(100)]
        public string UserUUID { get; set; }
        
        [StringLength(100)]
        public string Password { get; set; }
    }
} 