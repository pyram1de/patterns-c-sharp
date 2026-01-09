namespace Patterns.InventoryManagement;

public abstract class InventoryCommand
{
    private readonly bool _isTerminatingCommand;
    protected IUserInterface Interface { get; }
    internal InventoryCommand(bool commandIsTerminating, IUserInterface userInterface)
    {
        _isTerminatingCommand = commandIsTerminating;
        Interface = userInterface;
    }
    public (bool, bool) RunCommand()
    {
        if (this is IParameterisedCommand parameterisedCommand)
        {
            var allParametersCompleted = false;
            while (allParametersCompleted == false)
            {
                allParametersCompleted = parameterisedCommand.GetParameters();
            }
        }
        return (InternalCommand(), _isTerminatingCommand);
    }

    internal abstract bool InternalCommand();
}