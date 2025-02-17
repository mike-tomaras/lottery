namespace lottery.application.Interfaces;

public interface INotifications
{
    //This is an example of an integration API that the app would use to send updates/notifications about the game
    //A different csproj would implement this interface. We could have one proj per persistence technology (email, push, SMS etc)
    //Because of the way the dependencies are set up, this interface can only reference classes in the application domain layers
    //forcing us to not leak integration implementation details into the app and domain layers
}
