using lottery.domain.Domains.Users;

namespace lottery.application.tests;

[TestFixture]
public class WalletEntityTests
{
    [Test]
    public void WhenCreatingANewWallet_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        int id = 1;
        decimal balance = 100m;

        // Act
        var wallet = new WalletEntity(id, balance);

        // Assert
        Assert.That(wallet.Id, Is.EqualTo(id));
        Assert.That(wallet.Balance, Is.EqualTo(balance));
    }

    [Test]
    public void WhenCreatingANewWallet_GivenBalanceIsNegative_ShouldThrowArgumentOutOfRangeException_()
    {
        // Arrange
        int id = 1;
        decimal negativeBalance = -50m;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new WalletEntity(id, negativeBalance));
    }
}
