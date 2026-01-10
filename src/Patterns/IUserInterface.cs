namespace Patterns;

public interface IUserInterface: IWriteUserInterface, IReadUserInterface 
{}

public interface IWriteUserInterface
{
    public void WriteMessage(string message);
    public void WriteWarning(string message);
}

public interface IReadUserInterface
{
    public string ReadValue(string message);
}
