using lottery.domain.Game;
using NUnit.Framework;
using System;

namespace lottery.domain.tests;

[TestFixture]
public class WhenCreatingATicket
{
    [Test]
    public void ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        int userId = 1;
        Guid gameId = Guid.NewGuid();

        // Act
        var ticket = new Ticket(userId, gameId);

        // Assert
        Assert.That(ticket.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(ticket.UserId, Is.EqualTo(userId));
        Assert.That(ticket.GameId, Is.EqualTo(gameId));
        Assert.That(ticket.Prize, Is.EqualTo(PrizeTierEnum.None));
    }

    [Test]
    public void GivenGameIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        int userId = 1;
        Guid emptyGameId = Guid.Empty;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Ticket(userId, emptyGameId));
        Assert.That(ex.ParamName, Is.EqualTo("gameId"));
    }
}

[TestFixture]
public class WhenSettingThePrizeOfATicket
{
    [Test]
    public void GivenCurrentPrizeIsNone_ShouldBeSettable()
    {
        // Arrange
        var ticket = new Ticket(1, Guid.NewGuid());

        // Act
        ticket.Prize = PrizeTierEnum.First;

        // Assert
        Assert.That(ticket.Prize, Is.EqualTo(PrizeTierEnum.First));
    }

    [Test]
    public void GivenPrizeIsAlreadySet_ShouldNotBeAbleToChangeIt()
    {
        // Arrange
        var ticket = new Ticket(1, Guid.NewGuid());
        ticket.Prize = PrizeTierEnum.First;

        // Act
        ticket.Prize = PrizeTierEnum.Second;

        // Assert
        Assert.That(ticket.Prize, Is.EqualTo(PrizeTierEnum.First));
    }
}

