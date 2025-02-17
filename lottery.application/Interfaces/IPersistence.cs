namespace lottery.application.Interfaces;

public interface IPersistence
{
    //Here we'd define the API that the app would use to save and retrieve data from some sort of persistence
    //A different csproj would implement this interface. We could have one proj per persistence technology (DB, File, event stream, in memory, cloud BLOB etc)
    //Because of the way the dependencies are set up, this interface can only reference classes in the application domain layers
    //forcing us to not leak persistence implementation details into the app and domain layers
}
