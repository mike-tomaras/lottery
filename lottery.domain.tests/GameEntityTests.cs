using lottery.domain.Game;
using lottery.domain.Users;

namespace lottery.domain.tests
{
    [TestFixture]
    public class GameEntityTests_WhenCreatingANewGame
    {
        [Test]
        public void ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            decimal ticketPrice = 5m;

            // Act
            var game = new GameEntity(ticketPrice);

            // Assert
            Assert.That(game.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(game.PrizePool, Is.EqualTo(0));
            Assert.That(game.Tickets, Is.Empty);
            Assert.That(game.TicketPrice, Is.EqualTo(ticketPrice));
        }
    }

    [TestFixture]
    public class GameEntityTests_WhenBuyingTickets_GivenPlayerHasEnoughBalance
    {
        private UserEntity player;

        [SetUp]
        public void Setup()
        {
            // Arrange
            decimal ticketPrice = 5m;
            var game = new GameEntity(ticketPrice);
            player = UserEntity.Factory.GetDefaultUser(1, 100);
            var noOfTickets = 5;
            // Act
            game.BuyTickets(player, noOfTickets);
        }

        [Test]
        public void ShouldAddToPrizePool()
        {
            //TODO: Implement this test`
        }
        [Test]
        public void ShouldAddToTicketPurchases()
        {
            //TODO: Implement this test`
        }
        [Test]
        public void ShouldUpdatePlayerWalletBalance()
        {
            //TODO: Implement this test`
        }
    }

    [TestFixture]
    public class GameEntityTests_WhenBuyingTickets_GivenPlayerDoesNotHaveEnoughBalance
    {
        [Test]
        public void ShouldBuyOnlyTicketsSheCanAfford()
        {
            // Arrange
            decimal ticketPrice = 1m;
            var game = new GameEntity(ticketPrice);
            var player = UserEntity.Factory.GetDefaultUser(1, 5);
            var noOfTickets = 10;
            // Act
            game.BuyTickets(player, noOfTickets);

            //TODO: Implement this test
        }
    }
}

