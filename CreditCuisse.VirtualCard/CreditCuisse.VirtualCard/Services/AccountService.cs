using System.Text.RegularExpressions;
using System.Threading;
using Castle.Components.DictionaryAdapter;
using CreditCuisse.VirtualCard.Repository;
using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Services
{

    /// <summary>
    /// Main Account service which expeoses three functions Withdraw, Deposit and CheckBalance. 
    /// All these individual methods can grow and can extracted out as seperate dependencies like (WithdrawalService, DepositService, CheckBalanceService)
    /// And these services can be tested seperately and glued together in the AccountService which will act as Orchertrator
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly ICardValidator cardValidator;
        private readonly IAccountRepository accountRepository;

        public AccountService(ICardValidator cardValidator, IAccountRepository accountRepository )
        {
            this.cardValidator = cardValidator;
            this.accountRepository = accountRepository;
        }
        public TransactionResult Withdraw(string pin, double amount)
        {
            //This should handle concurrency issue, where two active user session will try to withdraw money at the same time
            if (SessionManager.Exists(pin))
            {
                return TransactionResult.AlreadyInProgress;
            }

            if (cardValidator.Validate(pin) != TransactionResult.Success)
            {
                return TransactionResult.InvalidPin;
            }

            SessionManager.Start(pin);

            var account = accountRepository.GetById(pin);

            if (account.Balance < amount)
            {
                return TransactionResult.NotEnoughBalance;
            }

            accountRepository.Withdraw(pin, amount);
            SessionManager.End(pin);

            return TransactionResult.Success;
        }

        public TransactionResult Deposit(string pin, int amount)
        {
            if (cardValidator.Validate(pin) != TransactionResult.Success)
            {
                return TransactionResult.InvalidPin;
            }

             accountRepository.Deposit(pin, amount);

            return TransactionResult.Success;

        }


        public TransactionResponse CheckBalance(string pin)
        {
            var response = new TransactionResponse();

            if (cardValidator.Validate(pin) != TransactionResult.Success)
            {
                response.TransactionResult = TransactionResult.InvalidPin;
                return response;
            }

            var account = accountRepository.GetById(pin);
            response.Account = account;
            return response;

        }
    }
}
