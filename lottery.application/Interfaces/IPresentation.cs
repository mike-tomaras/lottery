using lottery.domain.Game;
using lottery.domain.Users;

namespace lottery.application.Interfaces;

public interface IPresentation
{
    public void ShowInitialGameDetails(UserEntity player, decimal ticketPrice);
    public void ShowPreDrawGameDetails(int noOfPlayers, int noOfTickets, decimal pot);
    public int GetUserTicketInput(int name, int minTickets, int maxTickets);
    void ShowGameResults(GameEntity game, DrawResult results);
}
