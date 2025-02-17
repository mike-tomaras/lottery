using lottery.application.Interfaces;
using lottery.domain.Game;
using lottery.domain.Users;

namespace lottery.presentation.console;

public class Presentation : IPresentation
{
    //TEST NOTE: we could unit test the string outputs if we abstracted them into another class
    //that provided the strings only. Skipping now
    public void ShowInitialGameDetails(UserEntity player, decimal ticketPrice)
    {
        Console.WriteLine($"Welcome to the Lottery, Player {player.Name}!\n" +
                   $"* Your digital balance: ${player.Wallet.Balance}\n" +
                   $"* Ticket Price: ${ticketPrice} each\n");
        Console.WriteLine();
    }

    public void ShowPreDrawGameDetails(int noOfPlayers, int noOfTickets, decimal pot)
    {
        Console.WriteLine($"{noOfPlayers - 1} other CPU players also have purchased tickets.");
        Console.WriteLine($"Total tickets: {noOfTickets}");
        Console.WriteLine($"Pot: ${pot}");
        Console.WriteLine();
    }

    public int GetUserTicketInput(int name, int min, int max)
    {
        var tickets = 0;
        while (tickets < min || tickets > max)
        {
            Console.WriteLine($"How many tickets would you like to buy, Player {name}? Choose between {min} and {max}");
            var input = Console.ReadLine();
            if (input == null) continue;
            tickets = int.Parse(input);
        }

        return tickets;
    }

    public void ShowGameResults(GameEntity game, DrawResult results)
    {
        Console.WriteLine("DRAW!!!");
        Console.WriteLine("Ticket draw results:");
        Console.WriteLine();

        var firstPrize = results.Prizes.First(p => p.Tier == PrizeTierEnum.First);
        var firstPrizeWinner = firstPrize.WinningTickets.First().UserId;
        Console.WriteLine($"* Grand prize: Player {firstPrizeWinner} wins ${firstPrize.WinningAmount}!");

        var secondPrize = results.Prizes.First(p => p.Tier == PrizeTierEnum.Second);
        var secondPrizeWinners = secondPrize.WinningTickets.GroupBy(w => w.UserId);
        Console.WriteLine($"* Second Tier: Each ticket wins ${secondPrize.WinningAmount}!");
        foreach (var winner in secondPrizeWinners)
        {
            Console.WriteLine($"*  Player {winner.Key} wins ${secondPrize.WinningAmount * winner.Count()}!");
        }
        
        var thirdPrize = results.Prizes.First(p => p.Tier == PrizeTierEnum.Third);
        var thirdPrizeWinners = thirdPrize.WinningTickets.GroupBy(w => w.UserId);
        Console.WriteLine($"* Second Tier: Each ticket wins ${thirdPrize.WinningAmount}!");
        foreach (var winner in secondPrizeWinners)
        {
            Console.WriteLine($"*  Player {winner.Key} wins ${thirdPrize.WinningAmount * winner.Count()}!");
        }

        Console.WriteLine($"House revenue: ${results.HouseProfit}");
    }
}
