using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private IList<Account> accountList = new List<Account>
        {
            new Account
            {
                Pin = "1010",
                Balance = 0
            },
            new Account
            {
                Pin = "2323",
                Balance = 0
            },
            new Account
            {
                Pin = "1111",
                Balance = 0
            }
        };

        public Account GetById(string  pin)
        {
            return accountList.FirstOrDefault(a => a.Pin == pin);
        }

        public void Withdraw(string pin, double amount)
        {
            //Assume Service has already validated the Account existence and balance check using the Pin, So i am not gonna repeat the business logic here
            var acc = accountList.FirstOrDefault(a => a.Pin == pin);
            acc.Balance -= amount;
        }

        public void Deposit(string pin, double amount)
        {
            //Assume Service has already validated the Account existence and balance check using the Pin, So i am not gonna repeat the business logic here
            var acc = accountList.FirstOrDefault(a => a.Pin == pin);
            acc.Balance += amount;
        }
    }
}