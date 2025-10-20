using System.Collections.Immutable;
using P14_Banca.BankLibrary;
using P14_Banca.BankLibrary.Accounts;

namespace P14_Banca;

public class BankManager
{
    private bool _running = true;
    private const string DateFormat = "dd/MM/yyyy HH:mm";
    private readonly Bank _bank = new Bank();
    private readonly ImmutableDictionary<BankActions, Func<(bool isSuccess, string? msg)>> _operations;

    public BankManager()
    {
        _operations = new Dictionary<BankActions, Func<(bool isSuccess, string? error)>>
        {
            { BankActions.AccessAccount, AccessAccount },
            { BankActions.CreateAccount, RegisterAccount },
        }.ToImmutableDictionary();
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Bank Manager is running(- to stop it)");
        while (_running)
        {
            Console.WriteLine("Operations: l - access back account, r - register new account");
            if (!TakeInput("operation", out var operation))
                continue;
            var bankAction = ParseOperation(operation);
            if (!bankAction.HasValue)
            {
                Console.WriteLine("Invalid operation");
                continue;
            }

            var (isSuccess, msg) = ExecuteOperation(bankAction.Value);
            if (!isSuccess)
                Console.WriteLine($"Operation failed: {msg}");
            else if (msg != null)
                Console.WriteLine(msg);
        }
    }

    private BankActions? ParseOperation(string operation)
    {
        return operation.ToLower() switch
        {
            "l" => BankActions.AccessAccount,
            "r" => BankActions.CreateAccount,
            _ => null
        };
    }

    private (bool isSuccess, string? msg) ExecuteOperation(BankActions action)
    {
        if (!_operations.ContainsKey(action))
            return (false, "Invalid bank action");

        return _operations[action]();
    }

    private (bool isSuccess, string? msg) AccessAccount()
    {
        if (!TakeInput("owner name", out var owner))
            return (false, "Invalid owner name");
        if (!TakeInput("pin (4 digits)", out var pinInput))
            return (false, "Invalid pin");
        if (!int.TryParse(pinInput, out var pin) || pinInput.Length != 4)
            return (false, "Invalid pin, must be 4 digits");

        var success = _bank.Authenticate(owner, pinInput, out var account);
        if (!success)
            return (false, "Authentication failed");

        AccountManager.AccountControl(_bank, account!);

        return (true, null);
    }

    private (bool isSuccess, string? msg) RegisterAccount()
    {
        if (!TakeInput("owner name", out var owner))
            return (false, "Invalid owner name");
        if (!TakeInput("pin (4 digits)", out var pinInput))
            return (false, "Invalid pin");
        if (!int.TryParse(pinInput, out var pin) || pinInput.Length != 4)
            return (false, "Invalid pin, must be 4 digits");

        var success = _bank.CreateAccount(owner, pinInput);
        if (!success)
            return (false, "Register failed, account already exists!");

        return (true, "Account created successfully!");
    }

    private bool TakeInput(string variable, out string input)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input))
            return false;
        return true;
    }

    private bool CheckProgramEnd(string input)
    {
        if (input != "-") return false;
        _running = false;
        return true;
    }
}