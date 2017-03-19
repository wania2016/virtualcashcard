namespace CreditCuisse.VirtualCard.Types
{
    public enum TransactionResult
    {
        None,
        Success,
        InvalidPin,
        NotEnoughBalance,
        AlreadyInProgress
    }
}