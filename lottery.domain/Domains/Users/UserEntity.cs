namespace lottery.domain.Domains.Users;

public class UserEntity
{
    //TEST NOTE: Name is the same as Id an integer for simplicity, in a real-world scenario it would be a string
    //we will skip Name/Id uniqueness checks across players, the consumer will be responsible for that
    public int Name { get; private set; }
    public WalletEntity Wallet { get; private set; }

    public UserEntity(int name, WalletEntity wallet)
    {
        if (wallet == null) throw new ArgumentNullException(nameof(wallet));

        Name = name;
        Wallet = wallet;
    }

    public static class Factory
    {
        public static UserEntity GetDefaultUser(int name, decimal balance)
        {
            return new UserEntity(name, new WalletEntity(name, balance));
        }
    }
}
