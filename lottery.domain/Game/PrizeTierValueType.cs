namespace lottery.domain.Game;

public enum PrizeTierEnum
{
    None = 0,
    First = 1,
    Second = 2,
    Third = 3
}

public record class PrizeTierValueType
{
    public PrizeTierEnum Tier { get; }
    public decimal WinningAmount { get; }
    public List<TicketEntity> WinningTickets { get; }

    public PrizeTierValueType(PrizeTierEnum tier, decimal winningAmount, List<TicketEntity> winningTickets)
    {
        if (winningTickets == null) throw new ArgumentNullException(nameof(winningTickets));
        if (winningTickets.Count == 0) throw new ArgumentException("The Prize must have at least one ticket", nameof(winningTickets));
        if (winningAmount <= 0) throw new ArgumentException("The Prize amount must be positive", nameof(winningAmount));

        Tier = tier;
        WinningAmount = winningAmount;
        WinningTickets = winningTickets;
    }
}