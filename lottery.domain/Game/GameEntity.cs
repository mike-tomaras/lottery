using lottery.domain.Interfaces;
using lottery.domain.Users;

namespace lottery.domain.Game;

public class GameEntity
{
    public Guid Id { get; private set; }
    public decimal Pot => Tickets.Count * TicketPrice;
    public List<TicketEntity> Tickets { get; private set; }
    public decimal TicketPrice { get; private set; }

    public GameEntity(decimal ticketPrice)
    {
        Id = Guid.NewGuid();
        Tickets = new List<TicketEntity>();
        TicketPrice = ticketPrice;        
    }

    public void BuyTickets(UserEntity player, int noOfTickets)
    {
        var actualNoOfTickets = Math.Min(noOfTickets, (int)(player.Wallet.Balance / TicketPrice));

        player.Wallet.Debit(actualNoOfTickets * TicketPrice);
        
        if (actualNoOfTickets == 0) return;

        Enumerable.Range(1, actualNoOfTickets).ToList().ForEach(_ =>
            Tickets.Add(new TicketEntity(player.Name, Id))
        );
    }

    public DrawResult DrawWinners(IRandomGenerator randomGen)
    {
        var prizes = new List<PrizeTierValueType>
        {
            DrawFirstPrize(randomGen),
            DrawSecondPrize(randomGen),
            DrawThirdPrize(randomGen)
        };

        var houseProfit = Pot - prizes.Sum(p => p.WinningTickets.Count * p.WinningAmount);

        return new DrawResult(prizes, houseProfit);
    }

    private PrizeTierValueType DrawFirstPrize(IRandomGenerator randomGen)
    {
        var amountPerWinningTicket = Math.Round(Pot * 0.5m);

        TicketEntity winningTicket = DrawTicket(randomGen, PrizeTierEnum.First);

        return new PrizeTierValueType(PrizeTierEnum.First, amountPerWinningTicket, new List<TicketEntity> { winningTicket });
    }

    private PrizeTierValueType DrawSecondPrize(IRandomGenerator randomGen)
    {
        var noOfWinningTickets = (int)Math.Round(Tickets.Count * 0.1, digits: 0);
        var amountPerWinningTicket = Math.Round(Pot * 0.3m / noOfWinningTickets, decimals: 2);

        var winningTickets = new List<TicketEntity>();
        for (int i = 0; i < noOfWinningTickets; i++)
        {
            winningTickets.Add(DrawTicket(randomGen, PrizeTierEnum.Second));
        }

        return new PrizeTierValueType(PrizeTierEnum.Second, amountPerWinningTicket, winningTickets);
    }

    private PrizeTierValueType DrawThirdPrize(IRandomGenerator randomGen)
    {
        var noOfWinningTickets = (int)Math.Round(Tickets.Count * 0.2, digits: 0);
        var amountPerWinningTicket = Math.Round(Pot * 0.1m / noOfWinningTickets, decimals: 2);

        var winningTickets = new List<TicketEntity>();
        for (int i = 0; i < noOfWinningTickets; i++)
        {
            winningTickets.Add(DrawTicket(randomGen, PrizeTierEnum.Third));
        }

        return new PrizeTierValueType(PrizeTierEnum.Third, amountPerWinningTicket, winningTickets);
    }

    private TicketEntity DrawTicket(IRandomGenerator randomGen, PrizeTierEnum prizeTier)
    {
        var ticketsInPlay = Tickets.Where(t => t.Prize == PrizeTierEnum.None).ToList();
        var winningTicketIndex = randomGen.GetRandomInt(1, ticketsInPlay.Count) - 1;
        var winningTicket = ticketsInPlay[winningTicketIndex];
        winningTicket.Prize = prizeTier;
        return winningTicket;
    }
}

public record DrawResult(List<PrizeTierValueType> Prizes, decimal HouseProfit);