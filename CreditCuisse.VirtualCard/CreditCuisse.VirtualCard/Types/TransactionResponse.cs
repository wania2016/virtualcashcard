namespace CreditCuisse.VirtualCard.Types
{
    public class TransactionResponse
    {
        public TransactionResponse()
        {
            this.Account = new Account();
            this.TransactionResult = TransactionResult.None;
        }
        public Account Account { get; set; }
        public TransactionResult  TransactionResult { get; set; }
    }
}
