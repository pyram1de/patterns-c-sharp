namespace Patterns.Account;

public class CheckingAccount : Account
{
    private decimal _monthlyFee;

    public CheckingAccount(string owner, decimal balance, decimal monthlyFee)
        : base(owner, balance)
    {
        _monthlyFee = monthlyFee;
    }

    public decimal GetMonthlyFee() => _monthlyFee;

    public bool HandleCheck()
    {
        return Withdraw(_monthlyFee);
    }
}