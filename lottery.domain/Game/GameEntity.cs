using lottery.domain.Interfaces;
using lottery.domain.Users;

namespace lottery.domain.Game;

public class GameEntity
{
    public Guid Id { get; private set; }
    public decimal Pot { get; private set; }
    public List<Ticket> Tickets { get; private set; }
    public decimal TicketPrice { get; private set; }

    public GameEntity(decimal ticketPrice)
    {
        Id = Guid.NewGuid();
        Pot = 0;
        Tickets = new List<Ticket>();
        TicketPrice = ticketPrice;        
    }

    public void BuyTickets(UserEntity player, int noOfTickets)
    {
        var actualNoOfTickets = Math.Min(noOfTickets, (int)(player.Wallet.Balance / TicketPrice));

        player.Wallet.Debit(actualNoOfTickets * TicketPrice);
        
        if (actualNoOfTickets == 0) return;

        Enumerable.Range(1, actualNoOfTickets).ToList().ForEach(_ =>
            Tickets.Add(new Ticket(player.Name, Id))
        );
        
        Pot += noOfTickets * TicketPrice;
    }

    public DrawResult DrawWinners(IRandomGenerator randomGen)
    {
        var prizes = new List<PrizeTier>
        {
            DrawFirstPrize(randomGen),
            //DrawSecondPrize(randomGen),
            //DrawThirdPrize(randomGen)
        };

        var houseProfit = Pot - prizes.Sum(p => p.WinningTickets.Count * p.WinningAmount);

        return new DrawResult(prizes, houseProfit);
    }

    private PrizeTier DrawFirstPrize(IRandomGenerator randomGen)
    {
        var ticketsInPlay = Tickets.Where(t => t.Prize == PrizeTierEnum.None).ToList();
        var winningTicketIndex = randomGen.GetRandomInt(1, ticketsInPlay.Count) - 1;
        var amountPerWinningTicket = Math.Round(Pot * 0.5m);

        var winningTicket = ticketsInPlay[winningTicketIndex];
        winningTicket.Prize = PrizeTierEnum.First;

        return new PrizeTier(PrizeTierEnum.First, amountPerWinningTicket, new List<Ticket> { winningTicket });
    }

    private PrizeTier DrawSecondPrize(IRandomGenerator randomGen)
    {
        var ticketsInPlay = Tickets.Where(t => t.Prize == PrizeTierEnum.None).ToList();
        
        var noOfWinningTickets = (int)Math.Round(Tickets.Count * 0.1);
        var amountPerWinningTicket = Pot * 0.3m / noOfWinningTickets;


        return new PrizeTier(PrizeTierEnum.Second, amountPerWinningTicket, new List<Ticket> {});
    }

    private PrizeTier DrawThirdPrize(IRandomGenerator randomGen)
    {
        var ticketsInPlay = Tickets.Where(t => t.Prize == PrizeTierEnum.None).ToList();
        
        var noOfWinningTickets = (int)Math.Round(Tickets.Count * 0.2);
        var amountPerWinningTicket = Pot * 0.1m / noOfWinningTickets;


        return new PrizeTier(PrizeTierEnum.Third, amountPerWinningTicket, new List<Ticket> { });
    }
}

public record DrawResult(List<PrizeTier> Prizes, decimal HouseProfit);