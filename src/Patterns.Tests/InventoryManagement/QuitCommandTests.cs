using Moq;
using Patterns.InventoryManagement;
using Xunit;

namespace Patterns.Tests.InventoryManagement;

public class QuitCommandTests
{
    [Fact]
    public void QuitCommand_ShouldBeATerminatingCommand()
    {
        // Arrange
        var mockUserInterface = new Mock<IUserInterface>();
        var quitCommand = new QuitCommand(mockUserInterface.Object);

        // Act
        var (result, isTerminating) = quitCommand.RunCommand();

        // Assert
        Assert.True(isTerminating, "QuitCommand should be a terminating command");
    }

    [Fact]
    public void QuitCommand_ShouldReturnTrueWhenExecuted()
    {
        // Arrange
        var mockUserInterface = new Mock<IUserInterface>();
        var quitCommand = new QuitCommand(mockUserInterface.Object);

        // Act
        var (result, isTerminating) = quitCommand.RunCommand();

        // Assert
        Assert.True(result, "QuitCommand should return true when executed");
    }

    [Fact]
    public void QuitCommand_ShouldWriteGoodbyeMessage()
    {
        // Arrange
        var mockUserInterface = new Mock<IUserInterface>();
        var quitCommand = new QuitCommand(mockUserInterface.Object);

        // Act
        quitCommand.RunCommand();

        // Assert
        mockUserInterface.Verify(
            ui => ui.WriteMessage("Exiting the inventory management system. Goodbye!"),
            Times.Once,
            "QuitCommand should write goodbye message to the user interface");
    }

    [Fact]
    public void QuitCommand_ShouldAcceptIUserInterfaceParameter()
    {
        // Arrange
        var mockUserInterface = new Mock<IUserInterface>();

        // Act
        var quitCommand = new QuitCommand(mockUserInterface.Object);

        // Assert
        Assert.NotNull(quitCommand);
    }
}

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
            Assert.Equal(0, book.Quantity, $"Book {book.Name} should have quantity 0");
        }
    }
}
