using System.Collections.Immutable;
using P14_Banca.BankLibrary.Accounts;

namespace P14_Banca.BankLibrary;

public class Bank
{
    public string BankCode { get; } = Generators.BankCodeGenerator.Generate();
    private const string CountryCode = "RO";
    private readonly Dictionary<string, Account> _accounts = [];


    public bool CreateAccount(string owner, string pin)
    {
        if(AccountExists(owner))
            return false;
        var account = new Account(BankCode, owner, pin);
        _accounts.Add(owner, account);
        return true;
    }

    public bool Authenticate(string owner, string pin, out Account? account)
    {
        if (!_accounts.TryGetValue(owner, out account))
            return false;
        return account.Authenticate(pin);
    }
    
    private bool AccountExists(string owner)
    {
        return _accounts.ContainsKey(owner);
    }
}