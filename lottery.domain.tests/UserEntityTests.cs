using lottery.domain.Domains.Users;

namespace lottery.domain.tests;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void WhenCreatingANewUser_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        int name = 1;
        var wallet = new WalletEntity(name, 100m);

        // Act
        var user = new UserEntity(name, wallet);

        // Assert
        Assert.That(user.Name, Is.EqualTo(name));
        Assert.That(user.Wallet, Is.EqualTo(wallet));
    }

    [Test]
    public void WhenCreatingANewUser_GivenWalletIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        int name = 1;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UserEntity(name, new WalletEntity(1, 1)));
    }

    [Test]
    public void WhenGettingDefaultUser_ShouldReturnUserWithDefaultWalletWith10Dollars()
    {
        // Arrange
        int name = 1;
        decimal balance = 10m;

        // Act
        var user = UserEntity.Factory.GetDefaultUser(name, balance);

        // Assert
        Assert.That(user.Name, Is.EqualTo(name));
        Assert.IsNotNull(user.Wallet);
        Assert.That(user.Wallet.Id, Is.EqualTo(name));
        Assert.That(user.Wallet.Balance, Is.EqualTo(balance));
    }
}
