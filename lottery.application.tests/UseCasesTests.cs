using lottery.domain.Game;
using lottery.domain.Users;
using NUnit.Framework;

namespace lottery.application.tests
{
    [TestFixture]
    public class UseCasesTests
    {
        [Test]
        public void WhenInitializingAGame_ShouldReturnGameEntityWithCorrectTicketPrice()
        {
            // Arrange
            var useCases = new UseCases();
            decimal ticketPrice = 5m;

            // Act
            var game = useCases.InitializeGame(ticketPrice);

            // Assert
            Assert.That(game, Is.Not.Null);
            Assert.That(game.TicketPrice, Is.EqualTo(ticketPrice));
        }

        [Test]
        public void WhenInitializingAPlayer_ShouldReturnUserEntityWithCorrectNameAndWalletBalance()
        {
            // Arrange
            var useCases = new UseCases();
            int name = 1;
            decimal initialWalletBalance = 100m;

            // Act
            var user = useCases.InitializePlayer(name, initialWalletBalance);

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo(name));
            Assert.That(user.Wallet, Is.Not.Null);
            Assert.That(user.Wallet.Balance, Is.EqualTo(initialWalletBalance));
        }

        [Test]
        public void WhenBuyTickets_ShouldAddTicketsToGame()
        {
            // Arrange
            var useCases = new UseCases();
            var game = new GameEntity(5m);
            var user = new UserEntity(1, new WalletEntity(1, 100m));
            int noOfTickets = 2;

            // Act
            useCases.BuyTickets(game, user, noOfTickets);

            // Assert
            //Only testing a simple happy path to assert the call to the entity method
            //The edge cases are tested in the domain tests
            Assert.That(game.Tickets.Count, Is.EqualTo(noOfTickets));
        }
    }
}

