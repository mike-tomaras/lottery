using lottery.domain.Domains.Game;

namespace lottery.domain.Game.Tests
{
    [TestFixture]
    public class WhenCreatingANewPrizeTier
    {
        [Test]
        public void ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var tier = PrizeTierEnum.First;
            decimal winningAmount = 100m;
            var winningTickets = new List<TicketEntity> { new TicketEntity(1, Guid.NewGuid()) };

            // Act
            var prizeTier = new PrizeTierValueType(tier, winningAmount, winningTickets);

            // Assert
            Assert.That(prizeTier.Tier, Is.EqualTo(tier));
            Assert.That(prizeTier.WinningAmount, Is.EqualTo(winningAmount));
            Assert.That(prizeTier.WinningTickets, Is.EqualTo(winningTickets));
        }

        [Test]
        public void GivenWinningTicketsIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var tier = PrizeTierEnum.First;
            decimal winningAmount = 100m;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new PrizeTierValueType(tier, winningAmount, new List<TicketEntity>()));
            Assert.That(ex.ParamName, Is.EqualTo("winningTickets"));
        }

        [Test]
        public void GivenWinningTicketsIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            var tier = PrizeTierEnum.First;
            decimal winningAmount = 100m;
            var emptyWinningTickets = new List<TicketEntity>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new PrizeTierValueType(tier, winningAmount, emptyWinningTickets));
            Assert.That(ex.ParamName, Is.EqualTo("winningTickets"));
        }

        [Test]
        public void GivenWinningAmountIsNotPositive_ShouldThrowArgumentException()
        {
            // Arrange
            var tier = PrizeTierEnum.First;
            decimal nonPositiveWinningAmount = 0m;
            var winningTickets = new List<TicketEntity> { new TicketEntity(1, Guid.NewGuid()) };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new PrizeTierValueType(tier, nonPositiveWinningAmount, winningTickets));
            Assert.That(ex.ParamName, Is.EqualTo("winningAmount"));
        }
    }
}


