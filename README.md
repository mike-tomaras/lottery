# Lottery

This is a slighlty overengineered solution as I am trying to demonstrate long term thinking about code maintainability and easy extensibility/refactoring. 

There are notes specific to this test starting with "TEST NOTE" throughout the solution. The entry point is [here](/lottery.presentation.console/Program.cs) and the lottery run is [here](/lottery.application/ConsoleSingleRun.cs).

The problem statement implies that a player can win more than one prizes, its winning __tickets__ that are excluded from further prize tiers. I am going with that assumption.

## Code architecture notes
I am using Clean/Onion code architecture as described [here](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html). 
The idea is to separate the essential complexity of the domain from the accidental complexity of the presentation, integration and persistence.
I added [another diagram](/docs/onion.png), more specific to this repo.

As for the Domain project itself I am using some simple DDD principles like Entities and Value Objects. 
The Domains are separated in folders (Game, Users) but if they grow enough they could be extracted in their own projects. Whether in a folder or a project, 
the domain, its application use cases and its use in integrations/presentation/persistence should be separate and easy to extract in new repos/services if needed. 

I only use one project for all domains but separate them as folders so they should be easy to refactor in new projects if they grew too big. 
Domain classes are responsible for maintaining correct state using private setters and checks in all ctors and methods. The consumers can't create invalid state.
If Domain Entities become to complex to manage in a Domain, we can start using Aggregates to simplify the consumers.

If there was persistence, the Application layer would be responsible for mapping the Domain objects to the persistence objects and passing 
them along between layers of the onion.

I use business logic exceptions to communicate errors to the presentation layer but exception based control flow
is not my preference. I would prefer to use a Result type or similar.

## Testing notes
There is full unit test coverage on the essential complexity, i.e. all the code in the Domain project. This would also apply to the use cases 
in the Application project if they had any logic in them. 

If there were Application project tests they would do not need to mock the Domain, simplifying the tests a bit. We would not re test functionality 
inside the Domain, only logic in the Application. 
The Application layer would need to mock the presentation layer (console atm) and anything else that would exist in the outer circle of the Onion. 

We can add wider scope integration tests (covering functionality nad config in and out of this specific repo) to make sure the repo layers or code across 
repos is configured and communicates correctly, giving expected outputs. More test types, the definitions of their names, theit standards, their proportions, 
their coverage and when they run in the CI (always optimizing for fast feedback) would ideally be part of a common test strategy. 


# Further work
- Making the lottery do multiple runs maintaining the player and wallet state between runs (the code to make sure a player can only buy as many tickets as they can afford is already there though).
- When drawing for a very large numbers of tickets and potential winners, the current implementation may not scale well for CPU performance as we perform a full scan of the ticket list for each ticket draw. We could trade more latency with more RAM or external persitence.
- Making the prize tiers more configurable by extending the PrizeTier with to abstreact away the tier configuration and winners calculations. The Game constructor would take in a list of PrizeTiers and the DrawWinners method would call the PrizeTier obj to calculate the winners.
- More use cases for players to draw and top up their wallets as well as do other account admin.
- Adding a new presentation layer by adding a new project, referencing the Application proj and implementing the IPresentation interface. 
- Adding integrations and/or persistence by adding new projects and implementing the relevant interfaces in the Application project. 
- Dockerizing (not needed for a console presentation layer).
- Add deploy.yaml and config transformation files per deployment environment.
- If the app was deployable, I would add infra-as-code to the same repo.
- If there was relational DB as a persistence layer, I would add the migrations-as-code or schema-as-code in the same repo.
