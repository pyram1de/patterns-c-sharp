using Microsoft.Extensions.DependencyInjection;
using Patterns.Account;
using Patterns.InventoryManagement;

namespace Patterns;

class Program
{

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IUserInterface, ConsoleUserInterface>();
        services.AddTransient<IInventoryCommandFactory, InventoryCommandFactory>();
        services.AddTransient<ICatalogService, CatalogService>();
    }
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Design Patterns ===\n");
        IServiceCollection services = new ServiceCollection();
        ConfigureServices(services);       
        IServiceProvider serviceProvider = services.BuildServiceProvider(); 

        var service = serviceProvider.GetService<ICatalogService>();
        service.Run();

        var terminateSession = false;
        
        IUserInterface userInterface = new ConsoleUserInterface();
        IInventoryCommandFactory factory = new InventoryCommandFactory(userInterface);

        while (!terminateSession)
        {
            var error = false;
            var command = factory.GetCommand(userInterface.ReadValue("enter value:"));
            (error, terminateSession) = command.RunCommand();
        }

        while (true)
        {
            Console.WriteLine("1. Inheritance Example (Bank Accounts)");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect pattern: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    new AccountMenu().Run();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.\n");
                    break;
            }
        }
    }
}