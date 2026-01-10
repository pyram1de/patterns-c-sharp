namespace Patterns.InventoryManagement;

internal class GetInventoryCommand: NonTerminatingCommand
{
    private readonly IInventoryContext _context;

    internal GetInventoryCommand(IUserInterface userInterface, IInventoryContext context) : base(userInterface)
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