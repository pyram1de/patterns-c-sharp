using Microsoft.Extensions.DependencyInjection;

namespace Patterns.InventoryManagement;

public interface IInventoryCommandFactory
{
    public InventoryCommand GetCommand(string input);
}

public class InventoryCommandFactory: IInventoryCommandFactory
{
    private readonly IServiceProvider _serviceProvider;
    public InventoryCommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public InventoryCommand GetCommand(string input)
    {
        return _serviceProvider.GetServices<InventoryCommand>().First(c => c.IsCommandFor(input));
    }
}