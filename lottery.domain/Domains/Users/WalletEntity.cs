namespace lottery.domain.Domains.Users;

public class WalletEntity
{
    public int Id { get; private set; }
    public decimal Balance { get; private set; }
    //TEST NOTE: We will assume only USD for simplicity, if the currency was configurable
    //we would have a Currency property, and Currency class would be a Value Type

    public WalletEntity(int id, decimal balance)
    {
        //TEST NOTE: Entities in DDD have unique Ids but we will skip the Id uniqueness checks for simplicity
        if (balance < 0) throw new ArgumentOutOfRangeException(nameof(balance), "Balance cannot be negative");

        Id = id;
        Balance = balance;
    }

    //TEST NOTE: No need to validate the balance going negative atm,
    //the method is internal and only used by the GameEntity whose 
    //unit tests maintain the invariant that the player has balance >= 0.
    //This might need to change in the future as the consumers of this method grow
    internal void Debit(decimal amount)
    {
        Balance -= amount;
    }
}