using Patterns.InventoryManagement;

namespace Patterns;

public class CatalogService: ICatalogService
{
    private readonly IUserInterface _userInterface;
    private readonly IInventoryCommandFactory _commandFactory;

    public CatalogService(IUserInterface userInterface, IInventoryCommandFactory commandFactory)
    {
        _userInterface = userInterface;
        _commandFactory = commandFactory;
    }
    public void Run()
    {
        Greeting();

        var (_, shouldQuit) = _commandFactory.GetCommand("?").RunCommand();

        while (!shouldQuit)
        {
            var input = _userInterface.ReadValue(">").ToLower();
            var command = _commandFactory.GetCommand(input);

            (_, shouldQuit) = command.RunCommand();

        }
    }

    public void Greeting()
    {
        _userInterface.WriteMessage("Welcome to the Inventory Management System!");
    }
}