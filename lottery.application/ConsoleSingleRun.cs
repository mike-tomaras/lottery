
using lottery.application.Interfaces;

namespace lottery.application;

public class ConsoleSingleRun
{
    private readonly IPresentation presentation;

    public ConsoleSingleRun(IPresentation presentation)
    {
        this.presentation = presentation;
    }

    //TEST NOTE: this file exists to allow a single console app run
    //it is not part of the presentation logic, it just happens to be in the 
    //same project. All logic goes through the UseCases class which is presentation layer agnostic    
    public void Run()
    {
        //Config
        //TEST NOTE: I would add this to a config file or a database to change it easily without recompiling the code
        decimal ticketPrice = 3m;
        decimal initialWalletBalance = 10m;
        int maxTickets = 10;
        int minTickets = 1;

        var app = new UseCases();

        //TEST NOTE: we will only do one run of the lottery game
        //if we were to run multiple times, we would need to return and reuse the players and wallets state
        //and make sure we handle the case where a user runs out of money
        //This case is already handled in the domain logic,
        //but we would need to make sure we have enough players with enough money
        //to keep the game going
        var game = app.InitializeGame(ticketPrice);
        var players = Enumerable.Range(1, new Random().Next(10, 15))
                    .Select(name => app.InitializePlayer(name, initialWalletBalance))
                    .ToList();

        presentation.ShowInitialGameDetails(players[0], ticketPrice);

        //first player is the actual user
        var noOfTickets = presentation.GetUserTicketInput(players[0].Name, minTickets, maxTickets);
        app.BuyTickets(game, players[0], noOfTickets);

        //rest of the players are bots
        for (int i = 1; i < players.Count; i++)
        {
            app.BuyTickets(game, players[i], new Random().Next(minTickets, maxTickets));
        }

        presentation.ShowPreDrawGameDetails(players.Count, game.Tickets.Count, game.Pot);

        var results = app.DrawWinners(game);

        presentation.ShowGameResults(game, results);
    }
}
