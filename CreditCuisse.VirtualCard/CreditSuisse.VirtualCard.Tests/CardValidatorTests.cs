using Castle.Windsor;
using CreditCuisse.VirtualCard.Repository;
using CreditCuisse.VirtualCard.Services;
using CreditCuisse.VirtualCard.Types;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace CreditSuisse.VirtualCard.Tests
{
    [TestFixture]
    public class CardValidatorTests
    {
        private ICardValidator validator;
        private IWindsorContainer container;
        private IAccountRepository repository;

        [SetUp]
        public void Setup()
        {
            this.container = new WindsorContainer();
            repository = Substitute.For<IAccountRepository>();
            validator = new CardValidator(repository);
        }

        [TestCase("", TransactionResult.InvalidPin)]
        [TestCase("1", TransactionResult.InvalidPin)]
        [TestCase("1w", TransactionResult.InvalidPin)]
        [TestCase("123w", TransactionResult.InvalidPin)]
        [TestCase("1234", TransactionResult.Success)]
        public void ValidateShouldValidateCardPinCorrectly(string pin, TransactionResult expected)
        {
            //arrange
            this.repository.GetById(pin).Returns(new Account());
            //act
            var success = validator.Validate(pin);

            //assert
            success.ShouldBeEquivalentTo(expected);
        }


        [Test]
        public void ValidateShouldReturnInvalidCardWhenAccountIsNotFound()
        {
            //arrange
            var pin = "1234";
            this.repository.GetById(pin).Returns(default(Account));
            //act
            var success = validator.Validate(pin);

            //assert
            success.ShouldBeEquivalentTo(TransactionResult.InvalidPin);
        }
    }
}