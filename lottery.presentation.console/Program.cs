using lottery.application;

namespace lottery.presentation.console;

public class Program
{
    public static void Main(string[] args)
    {
        var console = new Presentation();
        var app = new ConsoleSingleRun(console);

        app.Run();

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
