namespace Patterns.InventoryManagement;

internal abstract class NonTerminatingCommand: InventoryCommand
{
    protected NonTerminatingCommand(IUserInterface userInterface) : base(false, userInterface)
    {
        
    }
}