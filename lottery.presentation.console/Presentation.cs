﻿using lottery.application.Interfaces;
using lottery.domain.Users;

namespace lottery.presentation.console;

public class Presentation : IPresentation
{
    public void ShowInitialGameDetails(UserEntity player, decimal ticketPrice)
    {
        Console.WriteLine($"Welcome to the Lottery, Player {player.Name}!\n" +
                   $"* Your digital balance: ${player.Wallet.Balance}\n" +
                   $"* Ticket Price: ${ticketPrice} each\n");
        Console.WriteLine();
    }

    public void ShowPreDrawGameDetails(int noOfPlayers)
    {
        Console.WriteLine($"{noOfPlayers - 1} other CPU players also have purchased tickets.");
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

    
}
