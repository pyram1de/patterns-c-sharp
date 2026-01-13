namespace Patterns.InventoryManagement;

public class Book
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
}

public interface IInventoryContext
{
    Book[] GetBooks();
    bool AddBook(string name);
    bool UpdateQuantity(string name, int quantity);
}