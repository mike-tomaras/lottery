using lottery.domain.Game;
using lottery.domain.Users;

namespace lottery.application;

public class UseCases
{
    //TEST NOTE: these are pass through methods so we can test the domain logic
    //directly, no need for extra tests for these
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

    public void DrawWinners(GameEntity game)
    {
        game.DrawWinners(new NativeRandomGen());
    }
}
