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

