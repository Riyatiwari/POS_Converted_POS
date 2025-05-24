using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsProduct
    {
        public List<SelectListItem> DTdepartment { get; set; }
        public int? ProductId { get; set; }
        public int CompanyId { get; set; }
        public int RowId { get; set; }
        public int CategoryId { get; set; }
        public int KeyMapId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float BPrice { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte IsActive { get; set; }
        public string IpAddress { get; set; }
        public int LoginId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int List { get; set; }
        public string PrinterId { get; set; }
        public decimal ActualPrice { get; set; }
        public int TaxId { get; set; }
        public int BTaxId { get; set; }
        public int ChoiceId { get; set; }
        public string Allergy { get; set; }
        public int Allergyid { get; set; }
        public float maxselect { get; set; }
        public float minselect { get; set; }
        public string Tax { get; set; }
        public int MachineId { get; set; }
        public string OtherId { get; set; }
        public string OtherSize { get; set; }
        public float Size { get; set; }
        public decimal SortingNo { get; set; }
        public decimal Sorting_No { get; set; }
        public string TranType { get; set; }
        public byte IsIngredient { get; set; }
        public byte IsCondiment { get; set; }
        public string Condiment { get; set; }
        public int Condiment_id { get; set; }
      
        public int Base { get; set; }
        public int UnitId { get; set; }
        public int BUnitId { get; set; }
        public int SingleRecord { get; set; }
        public byte SizeZero { get; set; }
        public string GroupName { get; set; }
        public string UnitName { get; set; }
        public string BUnitName { get; set; }
        public string ProId { get; set; }
        public int LocationId { get; set; }
        public int BLocationId { get; set; }
        public int AllergyId { get; set; }
        public byte[] ProductImage { get; set; }
        public byte IsStock { get; set; }
        public int ProductPriceId { get; set; }
        public int CatId { get; set; }
        public int Active { get; set; }
        public string DelStatus { get; set; }
        public int PriceId { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ClickAndCollect { get; set; }
        public int Deliver { get; set; }
        public int OrderAtTable { get; set; }
        public int IsWebAvailable { get; set; }
        public byte CloakRoom { get; set; }
        public int IsDanceVoucher { get; set; }
        public byte IsPriceOnScaleWeight { get; set; }
        public byte IsHouse { get; set; }
        public byte IsPkgProduct { get; set; }
        public string MainCategoryName { get; set; }
        public int IsUnflagPrinter { get; set; }
        public string DepartmentName { get; set; }
        public string PrinterName1 { get; set; }
        public string PrinterName2 { get; set; }
        public string PrinterName3 { get; set; }
        public string Size1Name { get; set; }
        public decimal Size1Price { get; set; }
        public decimal Size1 { get; set; }
        public string Size1Unit { get; set; }
        public string Size2Name { get; set; }
        public decimal Size2Price { get; set; }
        public decimal Size2 { get; set; }
        public string Size2Unit { get; set; }
        public string Size3Name { get; set; }
        public decimal Size3Price { get; set; }
        public decimal Size3 { get; set; }
        public string Size3Unit { get; set; }
        public string Size4Name { get; set; }
        public decimal Size4Price { get; set; }
        public decimal Size4 { get; set; }
        public string Size4Unit { get; set; }
        public byte IsAdditionalTax { get; set; }
        public byte ForKiosk { get; set; }
        public byte IsOutOfStock { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public string Barcode3 { get; set; }
        public string Barcode4 { get; set; }
        public string baseSize { get; set; }
        public string Ingredientname { get; set; }
        public string sbaseSize { get; set; }
        public string BbaseSize { get; set; }
        public int BarcodeID { get; set; }
        public bool sClickAndCollect { get; set; }  // Change to bool
        public bool sDeliver { get; set; }          // Change to bool
        public bool sOrderAtTable { get; set; }     // Change to bool
        public bool sIsActive { get; set; }
        public clsProduct ObjClsProduct { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public List<SelectListItem> Taxes { get; set; }
        public List<SelectListItem> Size_Zero { get; set; }
        public List<SelectListItem> Ingredient { get; set; }
        public List<SelectListItem> Condiments { get; set; }
        public List<SelectListItem> Allergys { get; set; }
        public string TaxName { get; set; }
        public string BTaxName { get; set; }
        // public clsCategory ObjClsCategory { get; set; }
        public int? cmp_id { get; set; }
        public string[] PrinterIds { get; set; }
        public string SName { get; set; }
        public string tName { get; set; }
        public List<clsProduct> productsList { get; set; }
        public List<clsProduct> AllergyList { get; set; }
        public string AllergyName { get; internal set; }
        List<clsProduct> productData = new List<clsProduct>();
        //public List<clsProduct> productData { get; set; }
    }
}
