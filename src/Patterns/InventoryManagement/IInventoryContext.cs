namespace Patterns.InventoryManagement;

internal class Book
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
}

internal interface IInventoryContext
{
    Book[] GetBooks();
    bool AddBook(string name);
    bool UpdateQuantity(string name, int quantity);
}