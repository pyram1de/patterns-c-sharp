namespace Patterns.InventoryManagement;

public class GetInventoryCommand: NonTerminatingCommand
{
    private readonly IInventoryContextRead _context;
    protected override string[] CommandStrings => new[] {"get", "g" };

    public GetInventoryCommand(IUserInterface userInterface, IInventoryContextRead context) : base(userInterface)
    {
        _context = context;
    }

    internal override bool InternalCommand()
    {
        foreach (var book in _context.GetBooks())
        {
            Interface.WriteMessage($"{book.Name, -30}\tQuantity:{book.Quantity}");
        }

        return true;
    }
}