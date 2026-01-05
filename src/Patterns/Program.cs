namespace Patterns;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Design Patterns ===\n");

        while (true)
        {
            Console.WriteLine("1. Inheritance Example (Bank Accounts)");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect pattern: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    new AccountMenu().Run();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.\n");
                    break;
            }
        }
    }
}