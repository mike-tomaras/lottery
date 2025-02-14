
namespace lottery.domain.Game;

public record class TicketPurchase(int UserId, Guid GameId, int TicketCount, decimal ticketPrice);
