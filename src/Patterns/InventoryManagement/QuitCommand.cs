namespace Patterns.InventoryManagement;

public class QuitCommand: InventoryCommand
{
    public QuitCommand(IUserInterface userInterface) : base(true, userInterface)
    {
        
    }
    internal override bool InternalCommand()
    {
        Interface.WriteMessage("Exiting the inventory management system. Goodbye!");
        return true;
    }
}