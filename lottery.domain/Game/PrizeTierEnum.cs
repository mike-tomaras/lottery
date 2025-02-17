namespace lottery.domain.Game;

public enum PrizeTierEnum
{
    None = 0,
    First = 1,
    Second = 2,
    Third = 3
}

public class  PrizeTier
{
    public PrizeTierEnum Tier { get; private set; }
    public decimal WinningAmount { get; private set; }
    public List<Ticket> WinningTickets { get; private set; }

    public PrizeTier(PrizeTierEnum tier, decimal winningAmount, List<Ticket> winningTickets)
    {
        if (winningTickets == null) throw new ArgumentNullException(nameof(winningTickets));
        if (winningTickets.Count == 0) throw new ArgumentException("The Prize must have at least one ticket", nameof(winningTickets));
        if (winningAmount <= 0) throw new ArgumentException("The Prize amount must be positive", nameof(winningAmount));

        Tier = tier;
        WinningAmount = winningAmount;
        WinningTickets = winningTickets;
    }
}