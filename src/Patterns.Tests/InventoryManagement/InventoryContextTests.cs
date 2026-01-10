using Patterns.InventoryManagement;
using Xunit;
using System.Threading.Tasks;

namespace Patterns.Tests.InventoryManagement;

public class InventoryContextTests
{
    private readonly IDictionary<string, Book> _seedDictionary;
    private readonly IDictionary<string, Book> _books;
    
    [Fact]
    public void InventoryContext_ShouldManageBookQuantitiesCorrectly()
    {
        // Arrange
        var inventoryContext = InventoryContext.GetContext;
        const int numberOfBooks = 30;
        
        // Add 30 books with initial quantity 0
        var tasks = new List<Task>();
        for (int i = 1; i <= numberOfBooks; i++)
        {
            // inventoryContext.AddBook($"Book{i}");
            tasks.Add(AddBook($"Book{i}"));
        }
        Task.WaitAll(tasks.ToArray());
        tasks.Clear();

        // Act - Add quantities 1 to 10 for all books
        foreach (var book in inventoryContext.GetBooks())
        {
            for (int quantity = 1; quantity <= 10; quantity++)
            {
                // inventoryContext.UpdateQuantity(book.Name, quantity);
                tasks.Add(UpdateQuantity(book.Name, quantity));
            }
        }
        Task.WaitAll(tasks.ToArray());
        tasks.Clear();

        // Subtract quantities 1 to 10 for all books
        foreach (var book in inventoryContext.GetBooks())
        {
            for (int quantity = 1; quantity <= 10; quantity++)
            {
                // inventoryContext.UpdateQuantity(book.Name, -quantity);
                tasks.Add(UpdateQuantity(book.Name, -quantity));
            }
        }
        Task.WaitAll(tasks.ToArray());
        // Assert - All books should have quantity 0
        var books = inventoryContext.GetBooks();
        Assert.Equal(numberOfBooks, books.Length);
        foreach (var book in books)
        {
            Assert.Equal(0, book.Quantity);
        }
    }

    public Task AddBook(string book)
    {
        return Task.Run(() =>
        {
            var context = InventoryContext.GetContext;
            Assert.True(context.AddBook(book));
        });
    }

    public Task UpdateQuantity(string book, int quantity)
    {
        return Task.Run(() =>
        {
            var context = InventoryContext.GetContext;
            Assert.True(context.UpdateQuantity(book, quantity));
        });
    }
}
