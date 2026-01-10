using Patterns.InventoryManagement;
using Xunit;

namespace Patterns.Tests.InventoryManagement;

public class InventoryContextTests
{
    [Fact]
    public void InventoryContext_ShouldManageBookQuantitiesCorrectly()
    {
        // Arrange
        var inventoryContext = new InventoryContext();
        const int numberOfBooks = 30;

        // Add 30 books with initial quantity 0
        for (int i = 1; i <= numberOfBooks; i++)
        {
            inventoryContext.AddBook($"Book{i}");
        }

        // Act - Add quantities 1 to 10 for all books
        foreach (var book in inventoryContext.GetBooks())
        {
            for (int quantity = 1; quantity <= 10; quantity++)
            {
                inventoryContext.UpdateQuantity(book.Name, quantity);
            }
        }

        // Subtract quantities 1 to 10 for all books
        foreach (var book in inventoryContext.GetBooks())
        {
            for (int quantity = 1; quantity <= 10; quantity++)
            {
                inventoryContext.UpdateQuantity(book.Name, -quantity);
            }
        }

        // Assert - All books should have quantity 0
        var books = inventoryContext.GetBooks();
        Assert.Equal(numberOfBooks, books.Length);
        foreach (var book in books)
        {
            Assert.Equal(0, book.Quantity);
        }
    }
}
