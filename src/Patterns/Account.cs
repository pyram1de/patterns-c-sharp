namespace Patterns;

public class Account
{
    public int Id { get; set; }
    private string _owner;
    private decimal _balance;

    public Account(string owner, decimal balance)
    {
        _owner = owner;
        _balance = balance;
    }

    public string GetOwner() => _owner;

    public decimal GetBalance() => _balance;

    protected void SetBalance(decimal balance) => _balance = balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            _balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && _balance >= amount)
        {
            _balance -= amount;
            return true;
        }
        return false;
    }
}