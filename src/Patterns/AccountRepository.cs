using Microsoft.Data.Sqlite;

namespace Patterns;

public class AccountRepository
{
    private readonly string _connectionString;

    public AccountRepository(string dbPath = "accounts.db")
    {
        _connectionString = $"Data Source={dbPath}";
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Accounts (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Owner TEXT NOT NULL,
                Balance REAL NOT NULL,
                AccountType TEXT NOT NULL,
                InterestRate REAL,
                MonthlyFee REAL
            )";
        command.ExecuteNonQuery();
    }

    public void Save(Account account)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        if (account.Id == 0)
        {
            command.CommandText = @"
                INSERT INTO Accounts (Owner, Balance, AccountType, InterestRate, MonthlyFee)
                VALUES ($owner, $balance, $type, $interestRate, $monthlyFee);
                SELECT last_insert_rowid();";
        }
        else
        {
            command.CommandText = @"
                UPDATE Accounts
                SET Balance = $balance, InterestRate = $interestRate, MonthlyFee = $monthlyFee
                WHERE Id = $id";
            command.Parameters.AddWithValue("$id", account.Id);
        }

        command.Parameters.AddWithValue("$owner", account.GetOwner());
        command.Parameters.AddWithValue("$balance", account.GetBalance());

        string accountType = account switch
        {
            SavingsAccount => "Savings",
            CheckingAccount => "Checking",
            _ => "Base"
        };
        command.Parameters.AddWithValue("$type", accountType);

        if (account is SavingsAccount savings)
            command.Parameters.AddWithValue("$interestRate", savings.GetInterestRate());
        else
            command.Parameters.AddWithValue("$interestRate", DBNull.Value);

        if (account is CheckingAccount checking)
            command.Parameters.AddWithValue("$monthlyFee", checking.GetMonthlyFee());
        else
            command.Parameters.AddWithValue("$monthlyFee", DBNull.Value);

        if (account.Id == 0)
        {
            var result = command.ExecuteScalar();
            account.Id = Convert.ToInt32(result);
        }
        else
        {
            command.ExecuteNonQuery();
        }
    }

    public List<Account> GetAll()
    {
        var accounts = new List<Account>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Accounts";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var owner = reader.GetString(1);
            var balance = reader.GetDecimal(2);
            var accountType = reader.GetString(3);

            Account account = accountType switch
            {
                "Savings" => new SavingsAccount(owner, balance, reader.IsDBNull(4) ? 0 : reader.GetDecimal(4)),
                "Checking" => new CheckingAccount(owner, balance, reader.IsDBNull(5) ? 0 : reader.GetDecimal(5)),
                _ => new Account(owner, balance)
            };

            account.Id = id;
            accounts.Add(account);
        }

        return accounts;
    }

    public Account? GetById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Accounts WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            var owner = reader.GetString(1);
            var balance = reader.GetDecimal(2);
            var accountType = reader.GetString(3);

            Account account = accountType switch
            {
                "Savings" => new SavingsAccount(owner, balance, reader.IsDBNull(4) ? 0 : reader.GetDecimal(4)),
                "Checking" => new CheckingAccount(owner, balance, reader.IsDBNull(5) ? 0 : reader.GetDecimal(5)),
                _ => new Account(owner, balance)
            };

            account.Id = id;
            return account;
        }

        return null;
    }

    public void SeedSampleData()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = "SELECT COUNT(*) FROM Accounts";
        var count = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (count > 0) return;

        var sampleAccounts = new List<Account>
        {
            new SavingsAccount("Alice Johnson", 5000.00m, 0.03m),
            new SavingsAccount("Bob Smith", 12500.50m, 0.025m),
            new CheckingAccount("Charlie Brown", 2500.00m, 15.00m),
            new CheckingAccount("Diana Ross", 8750.25m, 12.50m),
            new SavingsAccount("Eve Williams", 25000.00m, 0.035m)
        };

        foreach (var account in sampleAccounts)
        {
            Save(account);
        }
    }
}