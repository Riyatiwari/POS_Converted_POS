using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class ProductCondimentModel
    {
        public int TransactionId { get; set; }
        
        public int CondimentId { get; set; }
        
        public int ProductId { get; set; }
        
        public int CompanyId { get; set; }
        
        [StringLength(100)]
        public string Condiment { get; set; }
        
        [StringLength(100)]
        public string ProductName { get; set; }
        
        public decimal Price { get; set; }
        
        public int Choice { get; set; }
        
        public int MinSelect { get; set; }
        
        public int MaxSelect { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool IsAddSubstract { get; set; }
        
        public int? Quantity { get; set; }
        
        public int? UnitId { get; set; }
        
        public bool? IsBySize { get; set; }
        
        public int? SizeId { get; set; }
        
        public bool? UseProductCondiment { get; set; }
        
        [StringLength(1)]
        public string TranType { get; set; }
    }
} 