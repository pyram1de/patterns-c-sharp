namespace Patterns.InventoryManagement;

public interface IInventoryCommandFactory
{
    public InventoryCommand GetCommand(string input);
}

public class InventoryCommandFactory: IInventoryCommandFactory
{
    private readonly IEnumerable<InventoryCommand> _commands;
    public InventoryCommandFactory(IEnumerable<InventoryCommand> commands)
    {
        _commands = commands;
    }
    
    public InventoryCommand GetCommand(string input)
    {
        return _commands.First(c => c.IsCommandFor(input));
    }
}