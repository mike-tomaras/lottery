# Lottery

This is a slighlty overengineered solution as I am trying to demonstrate long term thinking about code maintainability and easy extensibility/refactoring. 

There are notes specific to this test starting with "TEST NOTE" throughout the solution.

The problem statement implies that a player can win more than one prizes, its winning __tickets__ that are excluded from further prize tiers. I am going with that assumption.

## Code architecture notes
I am using Clean/Onion code architecture as described [here](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html). 
This is the same as Hexagonal architecture but with a different name. The idea is to separate the essential complexity of the domain from the accidental complexity of the presentation, integration and persistence.
I added [another diagram](/docs/onion.png), more specific to this repo.

I only use one project for all domains but separate them as folders so they should be easy to refactor in new projects if they grew too big. 
Domain classes are responsible for maintaining correct state using private setters and asserting correst state in all ctors and methods. The consumers can't create invalid state.
If Domain Entities become to complex to manage in a Domain, we can start using Aggregates to simplify the consumers.

If there was persistence, the Application layer would be responsible for mapping the Domain objects to the persistence objects and passing 
them along between layers of the onion.

I use business logic exceptions to communicate errors to the presentation layer but exception based control flow
is not my preference. I would prefer to use a Result type or similar.

## Testing notes
There is full unit test coverage on the essential complexity, i.e. all the code in the Domain project and the use cases in the Application project. 

The Application project tests do not need to mock the Domain and I do not test functionality inside the Domain, only logic in the Application. 
The Application layer does need to mock the presentation layer (console atm) and anything else that would exist in the outer circle of the Onion. We can add wider 
scope integration tests (scope in and out of this specific repo) to make sure the Onion layers communicate correctly and 
the outputs of the presentation/integration/persistence are what we expect.


# Further work
- Making the lottery do multiple runs maintaining the player and wallet state between runs. (the code to make sure a player can only buy as many tickets as they can afford is there).
- When drawing for a very large numbers of tickets and potential winners, the current implementation may not scale well for performance as we perform a full scan of the ticket list for each ticket draw. We could trade more memory or add accitental complexity with persistence and the latency involved to get less computation latency.
- Making the prize tiers more configurable by adding a PrizeTier class. The Game constructor would take in a list of PrizeTiers and the DrawWinners method would be able to calculate the winners based on any configuration of no of winners and % of pot.
- Mmore use cases for players to draw and top up their wallets as well as do other account admin.
- Adding a new presentation layer by adding a new project, referencing the Application proj and implementing the IPresentation interface. 
- Adding integrations and/or persistence by adding new projects and implementing the relevant interfaces in the Application project. 
- Dockerizing (not needed for a console presentation layer).
- Deploy yaml and config transformation files per deployment environment.
- If it was deployable, I would add infra-as-code to the same repo.
- If there was persistence, I would add the migrations-as-code or schema-as-code in the same repo.