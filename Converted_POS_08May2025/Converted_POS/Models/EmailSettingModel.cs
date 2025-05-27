using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class EmailSettingModel
    {
        public int TranId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Mail server is required")]
        [StringLength(100)]
        public string MailServer { get; set; }
        
        [StringLength(100)]
        public string MailServerUsername { get; set; }
        
        [StringLength(100)]
        public string MailServerPassword { get; set; }
        
        [StringLength(20)]
        public string Port { get; set; }
        
        [Required(ErrorMessage = "From email is required")]
        [EmailAddress]
        [StringLength(100)]
        public string FromEmail { get; set; }
        
        public bool SSL { get; set; }
        
        [StringLength(100)]
        public string Alias { get; set; }
        
        public bool IsMES { get; set; }
        
        [StringLength(200)]
        public string MESURI { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string ReplyTo { get; set; }
        
        public byte? SType { get; set; }
        
        public int? LocationId { get; set; }
    }
} 