using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Services
{
    public interface IAccountService
    {
        TransactionResult Withdraw(string pin, double amount);
        TransactionResult Deposit(string pin, int amount);
        TransactionResponse CheckBalance(string pin);
    }
}
