namespace Converted_POS.Pages.Reports
{
    public class TableTransactionModel
    {
        public object TableName { get; set; }
        public object PaymentType { get; set; }
        public object RefNo { get; set; }
        public decimal PaymentAmount { get; set; }
        public object TableStatus { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal Change { get; set; }
        public decimal TipAmount { get; set; }
        public object DataTable { get; set; }
    }
}