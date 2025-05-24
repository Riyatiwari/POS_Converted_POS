using System.Collections.Generic;

namespace Converted_POS
{
    public class TransactionDetailViewModel
    {
        public string TableName { get; set; }
        public string PaymentType { get; set; }
        public string RefNo { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal TotalChange { get; set; }
        public decimal TipAmount { get; set; }
        public string TableStatus { get; set; }

        // Add a property for TransactionDetails (a list of transactions)
        public List<TransactionDetail> TransactionDetails { get; set; }
    }
    
}