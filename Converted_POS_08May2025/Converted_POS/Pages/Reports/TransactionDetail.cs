using System;

namespace Converted_POS
{
    public class TransactionDetail
    {
        public string Name { get; set; }
        public string Till { get; set; }
        public string Operator { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public DateTime Date { get; set; }
    }
}