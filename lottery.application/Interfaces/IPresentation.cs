using lottery.domain.Users;

namespace lottery.application.Interfaces;

public interface IPresentation
{
    public void ShowGameDetails(UserEntity player, decimal ticketPrice);
    public int GetUserTicketInput(int name, int minTickets, int maxTickets);
}
