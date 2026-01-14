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
        var context = new InventoryContext();
        services.AddSingleton<IInventoryContext, InventoryContext>(c => context);
        services.AddSingleton<IInventoryContextWrite, InventoryContext>(c => context);
        services.AddSingleton<IInventoryContextRead, InventoryContext>(c => context);
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