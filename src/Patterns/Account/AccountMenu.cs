namespace Patterns.Account;

public class AccountMenu
{
    private readonly AccountRepository _repository;

    public AccountMenu()
    {
        _repository = new AccountRepository();
        _repository.SeedSampleData();
    }

    public void Run()
    {
        Console.WriteLine("\n=== Bank Account Manager ===\n");

        while (true)
        {
            ShowMenu();
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ListAccounts();
                    break;
                case "2":
                    ViewAccount();
                    break;
                case "3":
                    DepositMoney();
                    break;
                case "4":
                    WithdrawMoney();
                    break;
                case "5":
                    ApplyInterestToSavings();
                    break;
                case "6":
                    ProcessCheckingFee();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.\n");
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("1. List all accounts");
        Console.WriteLine("2. View account details");
        Console.WriteLine("3. Deposit money");
        Console.WriteLine("4. Withdraw money");
        Console.WriteLine("5. Apply interest (Savings)");
        Console.WriteLine("6. Process monthly fee (Checking)");
        Console.WriteLine("0. Back to main menu");
        Console.Write("\nSelect option: ");
    }

    private void ListAccounts()
    {
        Console.WriteLine("\n--- All Accounts ---");
        var accounts = _repository.GetAll();

        foreach (var account in accounts)
        {
            var type = account switch
            {
                SavingsAccount => "Savings",
                CheckingAccount => "Checking",
                _ => "Base"
            };
            Console.WriteLine($"[{account.Id}] {account.GetOwner()} - {type} - Balance: {account.GetBalance():C}");
        }
        Console.WriteLine();
    }

    private void ViewAccount()
    {
        Console.Write("Enter account ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                Console.WriteLine($"\n--- Account Details ---");
                Console.WriteLine($"Owner: {account.GetOwner()}");
                Console.WriteLine($"Balance: {account.GetBalance():C}");

                if (account is SavingsAccount savings)
                {
                    Console.WriteLine($"Type: Savings");
                    Console.WriteLine($"Interest Rate: {savings.GetInterestRate():P}");
                }
                else if (account is CheckingAccount checking)
                {
                    Console.WriteLine($"Type: Checking");
                    Console.WriteLine($"Monthly Fee: {checking.GetMonthlyFee():C}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        Console.WriteLine();
    }

    private void DepositMoney()
    {
        Console.Write("Enter account ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                Console.Write("Enter deposit amount: ");
                if (decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    account.Deposit(amount);
                    _repository.Save(account);
                    Console.WriteLine($"Deposited {amount:C}. New balance: {account.GetBalance():C}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        Console.WriteLine();
    }

    private void WithdrawMoney()
    {
        Console.Write("Enter account ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                Console.Write("Enter withdrawal amount: ");
                if (decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    if (account.Withdraw(amount))
                    {
                        _repository.Save(account);
                        Console.WriteLine($"Withdrew {amount:C}. New balance: {account.GetBalance():C}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        Console.WriteLine();
    }

    private void ApplyInterestToSavings()
    {
        Console.Write("Enter savings account ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var account = _repository.GetById(id);
            if (account is SavingsAccount savings)
            {
                var before = savings.GetBalance();
                savings.ApplyInterest();
                _repository.Save(savings);
                Console.WriteLine($"Interest applied. Balance: {before:C} -> {savings.GetBalance():C}");
            }
            else
            {
                Console.WriteLine("Account not found or not a savings account.");
            }
        }
        Console.WriteLine();
    }

    private void ProcessCheckingFee()
    {
        Console.Write("Enter checking account ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var account = _repository.GetById(id);
            if (account is CheckingAccount checking)
            {
                if (checking.HandleCheck())
                {
                    _repository.Save(checking);
                    Console.WriteLine($"Monthly fee of {checking.GetMonthlyFee():C} deducted. New balance: {checking.GetBalance():C}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds to process fee.");
                }
            }
            else
            {
                Console.WriteLine("Account not found or not a checking account.");
            }
        }
        Console.WriteLine();
    }
}