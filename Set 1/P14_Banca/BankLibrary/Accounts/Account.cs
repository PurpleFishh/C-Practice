using System.Collections.Immutable;

namespace P14_Banca.BankLibrary.Accounts;

public class Account
{
    public string AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    private readonly string _pin;
    private bool _isAuth;
    private ImmutableDictionary<AccountActions, Func<decimal, decimal>> _operations;

    public Account(string bankCode, string owner, string pin, decimal initialBalance = 0)
    {
        AccountNumber = IbanGenerator.GenerateIban("RO", bankCode);
        Owner = owner;
        Balance = initialBalance;
        _pin = pin;

        _operations = new Dictionary<AccountActions, Func<decimal, decimal>>
        {
            { AccountActions.Deposit, Deposit },
            { AccountActions.Withdraw, Withdraw },
            { AccountActions.Balance, _ => ShowBalance() },
            { AccountActions.Exit, _ => Exit() }
        }.ToImmutableDictionary();
    }

    public decimal Operate(AccountActions action, decimal amount = 0)
    {
        if (!_operations.ContainsKey(action))
            throw new InvalidOperationException("Invalid account action.");
        if (!_isAuth)
            throw new UnauthorizedAccessException(
                "Account not authenticated. Please authenticate before performing operations");
        return _operations[action](amount);
    }

    public bool Authenticate(string pin)
    {
        if (pin != _pin) return false;
        _isAuth = true;
        return true;
    }

    private decimal Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));

        Balance += amount;
        return Balance;
    }

    private decimal Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds for this withdrawal");

        Balance -= amount;
        return Balance;
    }

    private decimal ShowBalance() => Balance;

    private decimal Exit()
    {
        _isAuth = false;
        return 0;
    }
}