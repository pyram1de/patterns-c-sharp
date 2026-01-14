namespace Patterns.InventoryManagement;

internal class UpdateQuantityCommand: NonTerminatingCommand, IParameterisedCommand
{
    private IInventoryContextWrite _context;
    private int _quantity;
    internal int Quantity { get => _quantity; private set => _quantity = value; }
    internal string InventoryName { get; private set; }
    
    public UpdateQuantityCommand(IUserInterface userInterface, IInventoryContextWrite context) : base(userInterface)
    {
        _context = context;
    }

    internal override bool InternalCommand()
    {
        _context.UpdateQuantity(InventoryName, Quantity);
        return true;
    }

    public bool GetParameters()
    {
        if(string.IsNullOrWhiteSpace(InventoryName))
        {
            InventoryName = GetParameter("name");
        }

        if (Quantity == 0)
        {
            int.TryParse(GetParameter("quantity"), out _quantity);
        }

        return !string.IsNullOrWhiteSpace(InventoryName)
               && Quantity != 0;
    }
    
    public string GetParameter(string parameterName)
    {
        return Interface.ReadValue("Please enter the inventory " + parameterName + ": ");
    }
}