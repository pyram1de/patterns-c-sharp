using System.Runtime.InteropServices.Marshalling;

namespace Patterns.InventoryManagement;

public class UnknownCommand: NonTerminatingCommand
{
    private IUserInterface _userInterface;
    protected override string[] CommandStrings => new[] { "" };

    public override bool IsCommandFor(string input)
    {
        return true;
    }
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