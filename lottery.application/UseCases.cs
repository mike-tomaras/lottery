using lottery.domain.Game;
using lottery.domain.Users;

namespace lottery.application;

public class UseCases
{
    public GameEntity InitializeGame(decimal ticketPrice)
    {
        return new GameEntity(ticketPrice);
    }

    public UserEntity InitializePlayer(int name, decimal initialWalletBalance)
    {
        return UserEntity.Factory.GetDefaultUser(name, initialWalletBalance);
    }

    public void BuyTickets(GameEntity game, UserEntity user, int noOfTickets)
    {
        game.BuyTickets(user, noOfTickets);
    }
}
