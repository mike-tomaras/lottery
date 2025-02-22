﻿using lottery.domain.Domains.Game;
using lottery.domain.Domains.Users;

namespace lottery.application;

public class UseCases
{
    //TEST NOTE: these are pass through methods so we can test the domain logic
    //directly, no need for extra tests for these
    public GameEntity InitializeGame(decimal ticketPrice)
    {
        return new GameEntity(ticketPrice);
    }

    public UserEntity InitializePlayer(int name, decimal initialWalletBalance)
    {
        return UserEntity.Factory.GetDefaultUser(name, initialWalletBalance);
    }

    public void BuyTickets(GameEntity game, UserEntity user, int noOfTickets)
    {
        game.BuyTickets(user, noOfTickets);
    }

    public DrawResult DrawWinners(GameEntity game)
    {
        return game.DrawWinners(new NativeRandomGen());
    }
}
