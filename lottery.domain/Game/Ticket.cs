
namespace lottery.domain.Game;

public class Ticket
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public Guid GameId { get; private set; }
    private PrizeTierEnum _prize;
    public PrizeTierEnum Prize 
    { 
        get => _prize;
        set {
            if (_prize == PrizeTierEnum.None)
            {
                _prize = value;
            }
        } 
    }

    public Ticket(int userId, Guid gameId)
    {
        if (gameId == Guid.Empty) throw new ArgumentException("GameId cannot be an empty GUID.", nameof(gameId));

        Id = Guid.NewGuid();
        UserId = userId;
        GameId = gameId;
        _prize = PrizeTierEnum.None;
    }
};
