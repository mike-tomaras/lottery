# lottery

This is a slighlty overengineered solution as I am trying to demonstrate long term thinking about code maintainability and easy extensibility/refactoring. 

## Code architecture
I am using Clean/Onion code architecture as described [here](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html). 

We could add different presentation layers in the future 

I only use one project for all domains but separate them as folders so they should be easy to refactor in new projects if they grew too big. 



## Testing
There is full unit test coverage on the essential complexity, i.e. all the code in the Domain project and the use cases in the Application project. 

The "units" in the Domain project are not necessarily one class. A test case can cover multiple components. 
This is on purpose as by testing the behavior of the Domain and not the internals allows for a lot of refactoring flexibility little no need to change tests. 

The Application project tests does not need to mock the Domain and I do not test functionality inside the Domain, only logic in the Application. 
The Application layer does need to mock the presentation layer (console atm) and anything else that would exist in the outer circle of the Onion. We can add wider 
scope integration tests (scope in and out of th is specific repo) to make sure the Onion layers communicate correctly and 
the outputs of the presentation/integration/persistence are what we expect.


# Further work
- Adding a new presentation layer by adding a new project, referencing the Application proj and implementing the IPresentation interface. 
- Adding integrations and/or persistence by adding new projects and implementing the relevant interfaces in the Application project. 
- Dockerizing (not needed for a console presentation layer).
- Deploy yaml and config transformation files per deployment environment.
- If it was deployable, I would add infra-as-code to the same repo.
- If there was persistence, I would add the migrations-as-code or schema-as-code to the same repo.