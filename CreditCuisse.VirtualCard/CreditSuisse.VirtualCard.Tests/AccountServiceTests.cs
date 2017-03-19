using System.Threading;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CreditCuisse.VirtualCard.Services;
using CreditCuisse.VirtualCard.Types;
using FluentAssertions;
using NUnit.Framework;

namespace CreditSuisse.VirtualCard.Tests
{

    /// <summary>
    /// This is an end to end test which involves all the dependencies of AccoutService to be real.
    /// In the real world scenario in order make the test suite faster, i will create a test double/static mock of the AccountRepository (which it is anyway at the moment)
    /// </summary>
    [TestFixture]
   // [Apartment(ApartmentState.STA)]
    public class AccountServiceTests
    {

        private IAccountService Subject;
        private IWindsorContainer container;

        [SetUp]
        public void Setup()
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.Containing<AccountService>());
            Subject = container.Resolve<IAccountService>();
        }


        #region Withdraw Tests
        [TestCase("")]
        [TestCase("123")]
        [TestCase("1231")]
        public void WithdrawShouldShouldReturnInvalidPinOnInValidCardPin(string pin)
        {
            //act
            var result = Subject.Withdraw(pin, 100);

            //assert
            result.ShouldBeEquivalentTo(TransactionResult.InvalidPin);
        }

        [Test]
        public void WithdrawShouldReturnNotEnoughBalanceWhenDrawingMoreThanTheAccountBalance()
        {
            //arrange
            var pin = "2323";

            //act
            var success = Subject.Withdraw(pin, 2000);

            //assert
            success.ShouldBeEquivalentTo(TransactionResult.NotEnoughBalance);
        }


        [Test]
        public void WithdrawShouldWithdrawMoneyFromAccount()
        {
            //arrange
            var pin = "1111";
            Subject.Deposit(pin, 2000);

            //act
            Subject.Withdraw(pin, 2000);

            //assert
            var response = Subject.CheckBalance(pin);
            response.Account.Balance.ShouldBeEquivalentTo(0);
        }
        #endregion

        #region Deposit Tests
        [TestCase("")]
        [TestCase("123")]
        [TestCase("6543")]
        public void DepositShouldShouldReturnInvalidPinOnInValidCardPin(string pin)
        {
            //act
            var result = Subject.Deposit(pin, 100);

            //assert
            result.ShouldBeEquivalentTo(TransactionResult.InvalidPin);
        }

        [Test]
        public void DepositShouldTopupMoneyInTheAccount()
        {
            //arrange
            var pin = "1010";
            var response = Subject.CheckBalance(pin);
            response.Account.Balance.ShouldBeEquivalentTo(0);

            //act
            Subject.Deposit(pin, 100);

            //assert
            response = Subject.CheckBalance(pin);
            response.Account.Balance.ShouldBeEquivalentTo(100);

        }
        #endregion

        #region CheckBalance Tests

        [TestCase("")]
        [TestCase("123")]
        [TestCase("1254")]
        public void CheckBalanceShouldShouldReturnInvalidPinOnInValidCardPin(string pin)
        {
            //act
            var result = Subject.CheckBalance(pin);

            //assert
            result.TransactionResult.ShouldBeEquivalentTo(TransactionResult.InvalidPin);
        }


        [Test]
        public void CheckBalanceShouldReturnCorrectAccountBalance()
        {
            //arrange
            var pin = "1010";
            var response = Subject.CheckBalance(pin);
            response.Account.Balance.ShouldBeEquivalentTo(0);

            //act
            Subject.Deposit(pin, 100);

            //assert
            Subject.CheckBalance(pin).Account.Balance.ShouldBeEquivalentTo(100);
        } 
        #endregion

    }
}
