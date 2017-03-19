using Castle.Windsor;
using CreditCuisse.VirtualCard.Services;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace CreditSuisse.VirtualCard.Tests
{
    [TestFixture]
    public class SessionManagerTests
    {
        [SetUp]
        public void Setup()
        {
            SessionManager.Clear();
        }

        [Test]
        public void AddShouldAddANewCard()
        {
            //act
            SessionManager.Start("100");
            
            //assert
            SessionManager.Exists("100").ShouldBeEquivalentTo(true);

        }

        [Test]
        public void ExistsShouldFindAnExistingCard()
        {
            //arrange
            SessionManager.Start("100");

            //act
            var actual = SessionManager.Exists("100");

            //assert
            actual.ShouldBeEquivalentTo(true);
        }

        [Test]
        public void ClearShouldClearEverythingInTheSessionManager()
        {
            //arrange
            SessionManager.Start("100");
            SessionManager.Start("300");

            //act
            SessionManager.Clear();

            //assert
            var actual = SessionManager.Exists("100");
            actual.ShouldBeEquivalentTo(false);
        }
    }
}