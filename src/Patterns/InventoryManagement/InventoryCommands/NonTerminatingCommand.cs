namespace Patterns.InventoryManagement;

public abstract class NonTerminatingCommand: InventoryCommand
{
    protected NonTerminatingCommand(IUserInterface userInterface) : base(false, userInterface)
    {
        
    }
}