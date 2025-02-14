using lottery.domain.Users;

namespace lottery.domain.Game;

public class GameEntity
{
    public Guid Id { get; private set; }
    public decimal PrizePool { get; private set; }
    public List<TicketPurchase> Tickets { get; private set; }
    public decimal TicketPrice { get; private set; }

    public GameEntity(decimal ticketPrice)
    {
        Id = Guid.NewGuid();
        PrizePool = 0;
        Tickets = new List<TicketPurchase>();
        TicketPrice = ticketPrice;        
    }

    public void BuyTickets(UserEntity player, int noOfTickets)
    {
        throw new NotImplementedException();
    }
}
