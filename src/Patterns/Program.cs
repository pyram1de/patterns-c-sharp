using Microsoft.Extensions.DependencyInjection;
using Patterns.Account;
using Patterns.InventoryManagement;

namespace Patterns;

class Program
{
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IUserInterface, ConsoleUserInterface>();
        services.AddTransient<ICatalogService, CatalogService>();
        var context = new InventoryContext();
        services.AddSingleton<IInventoryContext, InventoryContext>(c => context);
        services.AddSingleton<IInventoryContextWrite, InventoryContext>(c => context);
        services.AddSingleton<IInventoryContextRead, InventoryContext>(c => context);
        services.AddTransient<InventoryCommand, HelpCommand>();
        services.AddTransient<InventoryCommand, AddInventoryCommand>();
        services.AddTransient<InventoryCommand, GetInventoryCommand>();
        services.AddTransient<InventoryCommand, UpdateQuantityCommand>();
        services.AddTransient<InventoryCommand, QuitCommand>();
        services.AddTransient<InventoryCommand, UnknownCommand>();
        services.AddTransient<IInventoryCommandFactory, InventoryCommandFactory>();
    }
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Design Patterns ===\n");
        IServiceCollection services = new ServiceCollection();
        ConfigureServices(services);       
        IServiceProvider serviceProvider = services.BuildServiceProvider(); 

        var service = serviceProvider.GetService<ICatalogService>();
        service.Run();
    }
}