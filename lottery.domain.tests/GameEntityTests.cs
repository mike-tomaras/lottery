using lottery.application;
using lottery.domain.Game;
using lottery.domain.Interfaces;
using lottery.domain.Users;
using Moq;

namespace lottery.domain.tests;

[TestFixture]
public class WhenCreatingANewGame
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
        Assert.That(game.Pot, Is.EqualTo(0));
        Assert.That(game.Tickets, Is.Empty);
        Assert.That(game.TicketPrice, Is.EqualTo(ticketPrice));
    }
}

[TestFixture]
public class WhenBuyingTickets_GivenPlayerHasEnoughBalance
{
    private decimal ticketPrice;
    private GameEntity game;
    private UserEntity player;
    private int noOfTickets;
    private decimal initialBalance;

    [SetUp]
    public void Setup()
    {
        // Arrange
        initialBalance = 100m;
        ticketPrice = 6m;
        game = new GameEntity(ticketPrice);
        player = UserEntity.Factory.GetDefaultUser(1, initialBalance);
        noOfTickets = 5;
        // Act
        game.BuyTickets(player, noOfTickets);
    }

    [Test]
    public void ShouldAddToPrizePool()
    {
        Assert.That(game.Pot, Is.EqualTo(ticketPrice * noOfTickets));
    }
    [Test]
    public void ShouldAddToTicketPurchases()
    {
        Assert.That(game.Tickets.All(t => t.UserId == player.Name), Is.True);
        Assert.That(game.Tickets.Count, Is.EqualTo(noOfTickets));
        Assert.That(game.Tickets.All(t => t.Prize == PrizeTierEnum.None), Is.True);
    }
    [Test]
    public void ShouldUpdatePlayerWalletBalance()
    {
        Assert.That(player.Wallet.Balance, Is.EqualTo(initialBalance - ticketPrice * noOfTickets));
    }
}

[TestFixture]
public class WhenBuyingTickets_GivenPlayerDoesNotHaveEnoughBalance
{
    [TestCase(10, 6, 2, 0)]
    [TestCase(10, 10, 3, 1)]
    public void ShouldBuyOnlyTicketsSheCanAfford(int noOfTickets, decimal balance, int expectedNoOfBoughtTickets, decimal expectedRemainingBalance)
    {
        // Arrange
        decimal ticketPrice = 3m;
        var game = new GameEntity(ticketPrice);
        var player = UserEntity.Factory.GetDefaultUser(1, balance);

        // Act
        game.BuyTickets(player, noOfTickets);

        //Assert
        Assert.That(game.Tickets.Count, Is.EqualTo(expectedNoOfBoughtTickets));
        Assert.That(player.Wallet.Balance, Is.EqualTo(expectedRemainingBalance));
    }

    [Test]
    public void ShouldBuyZeroTicketsIfThereIsNoMoneyForOne()
    {
        // Arrange
        decimal ticketPrice = 10m;
        decimal balance = 9m;
        var game = new GameEntity(ticketPrice);
        var player = UserEntity.Factory.GetDefaultUser(1, balance);

        // Act
        game.BuyTickets(player, 1);

        //Assert
        Assert.That(game.Tickets.Count, Is.Zero);
        Assert.That(player.Wallet.Balance, Is.EqualTo(balance));
    }
}

[TestFixture]
public class WhenRunningTheLottery_GivenASuccessfulRun
{
    private GameEntity game;
    private DrawResult result;

    [SetUp]
    public void Setup()
    {
        // Arrange
        var randomGen = new Mock<IRandomGenerator>();
        randomGen.SetupSequence(x => x.GetRandomInt(It.IsAny<int>(), It.IsAny<int>()))
            //1st tier goes to player 10, last ticket of 10
            .Returns(100)
            //2nd tier goes to player 8, 1st to 10th tickets, i.e. all of them
            .Returns(81).Returns(82).Returns(83).Returns(84).Returns(85)
            .Returns(86).Returns(87).Returns(88).Returns(89).Returns(90)
            //3rd tier goes to players 1 and 2, 1st to 10th tickets, i.e. all of them for both
            .Returns(1).Returns(2).Returns(3).Returns(4).Returns(5)
            .Returns(6).Returns(7).Returns(8).Returns(9).Returns(10)
            .Returns(11).Returns(12).Returns(13).Returns(14).Returns(15)
            .Returns(16).Returns(17).Returns(18).Returns(19).Returns(20);
        game = new GameEntity(5m);
        var players = Enumerable.Range(1, 10)
                .Select(name => UserEntity.Factory.GetDefaultUser(name, balance: 100))
                .ToList();

        foreach (var player in players)
            game.BuyTickets(player, 10);

        //10 players, 10 tickets each, 100 tickets total, $500 pot

        // Act
        result = game.DrawWinners(randomGen.Object);
    }

    [Test]
    public void ShouldDrawTheFirstTierWinners()
    {
        // Assert
        var firstPrize = result.Prizes.Single(p => p.Tier == PrizeTierEnum.First);

        Assert.That(game.Tickets.Count(t => t.Prize == PrizeTierEnum.First), Is.EqualTo(1));
        Assert.That(firstPrize.WinningTickets.Count, Is.EqualTo(1));
        Assert.That(firstPrize.WinningAmount, Is.EqualTo(250m));

        var winningTicket = firstPrize.WinningTickets.SingleOrDefault();
        Assert.That(winningTicket, Is.Not.Null);
        Assert.That(winningTicket.UserId, Is.EqualTo(10));//player 10
    }

    [Test]
    public void ShouldDrawTheSecondTierWinners()
    {
        // Assert
        var secondPrize = result.Prizes.Single(p => p.Tier == PrizeTierEnum.Second);

        Assert.That(game.Tickets.Count(t => t.Prize == PrizeTierEnum.Second), Is.EqualTo(10));//10% of all tickets
        Assert.That(secondPrize.WinningTickets.Count, Is.EqualTo(10));
        Assert.That(secondPrize.WinningAmount, Is.EqualTo(15m));
    }
    [Test]
    public void ShouldDrawTheThirdTierWinners()
    {
        // Assert
        var thirdPrize = result.Prizes.Single(p => p.Tier == PrizeTierEnum.Third);

        Assert.That(game.Tickets.Count(t => t.Prize == PrizeTierEnum.Third), Is.EqualTo(20));//20% of all tickets
        Assert.That(thirdPrize.WinningTickets.Count, Is.EqualTo(20));
        Assert.That(thirdPrize.WinningAmount, Is.EqualTo(5m));        
    }
    [Test]
    public void ShouldGetTheHouseProfit()
    {
        // Assert
        Assert.That(result.HouseProfit, Is.EqualTo(100m));
    }
}

