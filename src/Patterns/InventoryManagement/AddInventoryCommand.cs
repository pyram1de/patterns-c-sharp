namespace Patterns.InventoryManagement;

public class AddInventoryCommand: InventoryCommand, IParameterisedCommand
{
    public AddInventoryCommand(IUserInterface userInterface) : base(false, userInterface)
    {
        
    }
    public string InventoryName { get; private set; }

    public bool GetParameters()
    {
        if(string.IsNullOrWhiteSpace(InventoryName))
        {
            InventoryName = GetParameter("name");
        }
        return !string.IsNullOrWhiteSpace(InventoryName);
    }

    public string GetParameter(string parameterName)
    {
        var console = new ConsoleUserInterface();
        return console.ReadValue("Please enter the inventory " + parameterName + ": ");
    }

    internal override bool InternalCommand()
    {
        return true;
    }
}