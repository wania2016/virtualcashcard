using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Services
{
    public interface ICardValidator
    {
        TransactionResult Validate(string pin);
    }
}