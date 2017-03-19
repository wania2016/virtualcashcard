using CreditCuisse.VirtualCard.Types;

namespace CreditCuisse.VirtualCard.Repository
{
    public interface IAccountRepository
    {
        Account GetById(string  pin);
        void Withdraw(string pin, double amount);
        void Deposit(string pin, double amount);
    }
}