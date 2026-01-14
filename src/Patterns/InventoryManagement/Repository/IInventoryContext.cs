namespace Patterns.InventoryManagement;

public class Book
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
}

public interface IInventoryContextRead
{
    Book[] GetBooks();
}

public interface IInventoryContextWrite
{
    bool AddBook(string name);
    bool UpdateQuantity(string name, int quantity);
}

public interface IInventoryContext : IInventoryContextRead, IInventoryContextWrite
{
}