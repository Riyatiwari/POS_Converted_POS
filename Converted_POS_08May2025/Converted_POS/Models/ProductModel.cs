using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        
        public int CompanyId { get; set; }
        
        public int? RowId { get; set; }
        
        public int? CategoryId { get; set; }
        
        public int? KeyMapId { get; set; }
        
        [Required(ErrorMessage = "Product code is required")]
        [StringLength(50)]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        [StringLength(50)]
        public string Barcode { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public int? DepartmentId { get; set; }
        
        public int? CourseId { get; set; }
        
        public int? List { get; set; }
        
        [StringLength(100)]
        public string PrinterId { get; set; }
        
        public decimal? ActualPrice { get; set; }
        
        public int? TaxId { get; set; }
        
        [StringLength(50)]
        public string Tax { get; set; }
        
        public int? MachineId { get; set; }
        
        [StringLength(100)]
        public string OtherId { get; set; }
        
        [StringLength(100)]
        public string OtherSize { get; set; }
        
        public decimal? SortingNo { get; set; }
        
        public bool IsIngredient { get; set; }
        
        public bool IsCondiment { get; set; }
        
        public int? Base { get; set; }
        
        public int? UnitId { get; set; }
        
        public int? SingleRecord { get; set; }
        
        public bool SizeZero { get; set; }
        
        [StringLength(100)]
        public string GroupName { get; set; }
        
        [StringLength(100)]
        public string UnitName { get; set; }
        
        [StringLength(100)]
        public string ProId { get; set; }
        
        public int? LocationId { get; set; }
        
        public int? AllergyId { get; set; }
        
        public byte[] Image { get; set; }
        
        public bool IsStock { get; set; }
        
        public int? ProductPriceId { get; set; }
        
        public int? CatId { get; set; }
        
        public int? Active { get; set; }
        
        [StringLength(50)]
        public string DelStatus { get; set; }
        
        public int? PriceId { get; set; }
        
        public int? SizeId { get; set; }
        
        [StringLength(100)]
        public string SizeName { get; set; }
        
        public decimal? Size { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool Deliver { get; set; }
        
        public bool OrderAtTable { get; set; }
        
        public bool IsWebAvailable { get; set; }
        
        public bool CloakRoom { get; set; }
        
        public bool IsDanceVoucher { get; set; }
        
        public bool IsPriceOnScaleWeight { get; set; }
        
        public bool IsHouse { get; set; }
        
        public bool IsPkgProduct { get; set; }
        
        [StringLength(100)]
        public string MainCategoryName { get; set; }
        
        public bool IsUnflagPrinter { get; set; }
        
        [StringLength(100)]
        public string DepartmentName { get; set; }
        
        [StringLength(100)]
        public string PrinterName1 { get; set; }
        
        [StringLength(100)]
        public string PrinterName2 { get; set; }
        
        [StringLength(100)]
        public string PrinterName3 { get; set; }
        
        [StringLength(100)]
        public string Size1Name { get; set; }
        
        public decimal? Size1Price { get; set; }
        
        public decimal? Size1 { get; set; }
        
        [StringLength(50)]
        public string Size1Unit { get; set; }
        
        [StringLength(100)]
        public string Size2Name { get; set; }
        
        public decimal? Size2Price { get; set; }
        
        public decimal? Size2 { get; set; }
        
        [StringLength(50)]
        public string Size2Unit { get; set; }
        
        [StringLength(100)]
        public string Size3Name { get; set; }
        
        public decimal? Size3Price { get; set; }
        
        public decimal? Size3 { get; set; }
        
        [StringLength(50)]
        public string Size3Unit { get; set; }
        
        [StringLength(100)]
        public string Size4Name { get; set; }
        
        public decimal? Size4Price { get; set; }
        
        public decimal? Size4 { get; set; }
        
        [StringLength(50)]
        public string Size4Unit { get; set; }
        
        public bool IsAdditionalTax { get; set; }
        
        public bool ForKiosk { get; set; }
        
        public bool IsOutOfStock { get; set; }
        
        [StringLength(50)]
        public string Barcode1 { get; set; }
        
        [StringLength(50)]
        public string Barcode2 { get; set; }
        
        [StringLength(50)]
        public string Barcode3 { get; set; }
        
        [StringLength(50)]
        public string Barcode4 { get; set; }
    }
}