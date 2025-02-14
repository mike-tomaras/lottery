namespace lottery.domain.Users;

public class WalletEntity
{
    //TEST NOTE: Entities in DDD have Ids but we will skip the Id uniqueness checks for simplicity
    public int Id { get; private set; }
    public decimal Balance { get; private set; }
    //TEST NOTE: We will assume only USD for simplicity, if the currency was configurable
    //we would have a Currency property, and Currency class would be a Value Type

    public WalletEntity(int id, decimal balance)
    {
        if (balance < 0) throw new ArgumentOutOfRangeException(nameof(balance), "Balance cannot be negative");

        Id = id;
        Balance = balance;
    }
}