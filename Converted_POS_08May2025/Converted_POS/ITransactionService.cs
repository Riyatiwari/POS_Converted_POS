namespace Converted_POS
{
    
        public interface ITransactionService
        {
            //IEnumerable<TransactionDetail> GetTransactionDetails(TransactionDetailViewModel model);
            object GetTransactionDetails(int? salesId, string tranUuid, string saleType);
        }
    
}