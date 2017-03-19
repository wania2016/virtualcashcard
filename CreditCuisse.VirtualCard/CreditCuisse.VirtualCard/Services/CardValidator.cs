using CreditCuisse.VirtualCard.Repository;
using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Services
{
    public class CardValidator : ICardValidator
    {
        private readonly IAccountRepository accountRepository;

        public CardValidator(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public TransactionResult Validate(string pin)
        {
            if (!ValidatePrivate(pin)) return  TransactionResult.InvalidPin;

            var account = accountRepository.GetById(pin);

            return account == null ? TransactionResult.InvalidPin : TransactionResult.Success;
        }

        private static bool ValidatePrivate(string pin)
        {
            //Empty Pin
            if (string.IsNullOrEmpty(pin))
            {
                return false;
            }

            pin = pin.Trim();

            //Less than 4 characters
            if (pin.Length < 4)
            {
                return false;
            }

            //All should be numeric
            int cardNumber;

            if (!int.TryParse(pin, out cardNumber))
            {
                return false;
            }
            return true;
        }
    }
}