namespace Patterns.Account;

public class SavingsAccount : Account
{
    private decimal _interestRate;

    public SavingsAccount(string owner, decimal balance, decimal interestRate)
        : base(owner, balance)
    {
        _interestRate = interestRate;
    }

    public decimal GetInterestRate() => _interestRate;

    public void ApplyInterest()
    {
        var interest = GetBalance() * _interestRate;
        Deposit(interest);
    }
}