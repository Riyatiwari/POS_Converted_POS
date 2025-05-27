using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class BarcodeModel
    {
        public int BarcodeSizeId { get; set; }
        
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Barcode is required")]
        [StringLength(100)]
        public string Barcode { get; set; }
        
        public int? SizeId { get; set; }
        
        public int? ProductId { get; set; }
        
        public bool IsActive { get; set; }
    }
} 