namespace Patterns.InventoryManagement;

internal class InventoryContext: IInventoryContext
{
    public InventoryContext()
    {
        _books = new Dictionary<string, Book>();
    }

    private readonly IDictionary<string, Book> _books;

    public Book[] GetBooks()
    {
        return _books.Values.ToArray();
    }

    public bool AddBook(string name)
    {
        _books.Add(name, new Book { Name = name });
        return true;
    }

    public bool UpdateQuantity(string name, int quantity)
    {
        _books[name].Quantity += quantity;
        return true;
    }
}