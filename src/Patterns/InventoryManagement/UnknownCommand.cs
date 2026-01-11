namespace Patterns.InventoryManagement;

internal class UnknownCommand: NonTerminatingCommand
{
    private IUserInterface _userInterface;
    public UnknownCommand(IUserInterface userInterface): base(userInterface)
    {
        _userInterface = userInterface;
    }
    internal override bool InternalCommand()
    {
        _userInterface.WriteWarning("what?");
        return true;
    }
}