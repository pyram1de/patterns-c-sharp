namespace Patterns.InventoryManagement;

internal class HelpCommand: NonTerminatingCommand
{
    public HelpCommand(IUserInterface userInterface) : base(userInterface)
    {
        
    }
    internal override bool InternalCommand()
    {
        Interface.WriteMessage("Available commands:");
        Interface.WriteMessage(" - help: Show this help message");
        Console.WriteLine(" - add <item>: Add an item to the inventory");
        Console.WriteLine(" - remove <item>: Remove an item from the inventory");
        Console.WriteLine(" - list: List all items in the inventory");
        Console.WriteLine(" - quit: Exit the inventory management system");
        return true;
    }
}