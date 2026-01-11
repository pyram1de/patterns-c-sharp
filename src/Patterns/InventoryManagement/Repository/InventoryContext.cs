using System.Collections.Concurrent;

namespace Patterns.InventoryManagement;

internal class InventoryContext: IInventoryContext
{
    
    protected InventoryContext()
    {
        _books = new ConcurrentDictionary<string, Book>();
    }

    private static InventoryContext _instance;
    private static readonly object _lock = new object();

    public static InventoryContext GetContext
    {
        get
        {
            if (_instance == null)
            {
                //lock (_lock)
                //{
                    if (_instance == null)
                    {
                        //lock (_lock)
                        //{
                            _instance = new InventoryContext();
                        //}
                    }
                //}
            }

            return _instance;
        }
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
        lock (_lock)
        {
            _books[name].Quantity += quantity;
            return true;
        }
    }
}