using System.Collections.Concurrent;

namespace Patterns.InventoryManagement;

public class InventoryContext: IInventoryContext
{
    
    public InventoryContext()
    {
        _books = new ConcurrentDictionary<string, Book>();
    }
    
    private static readonly object _lock = new object();

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
        lock (_lock)
        {
            _books[name].Quantity += quantity;
            return true;
        }
    }
}