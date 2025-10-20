using P14_Banca.BankLibrary;
using P14_Banca.BankLibrary.Accounts;

namespace P14_Banca;

public static class AccountManager
{
    private enum Operations
    {
        Balance,
        Deposit,
        Withdraw,
        Exit
    }

    private static bool _running = true;

    public static void AccountControl(Bank bank, Account account)
    {
        _running = true;

        while (_running)
        {
            Console.WriteLine("Operations: b - check balance, d - deposit, w - withdraw, e - exit account");
            if (!TakeInput("operation", out var opText))
                continue;

            var operation = ParseOperation(opText);
            if (!operation.HasValue)
            {
                Console.WriteLine("Invalid operation");
                continue;
            }

            try
            {
                InterpretOperation(operation.Value, bank, account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
        }
    }

    private static void InterpretOperation(Operations operation, Bank bank, Account account)
    {
        var resultBalance = Operate(operation, bank, account);

        switch (operation)
        {
            case Operations.Balance:
                Console.WriteLine($" Balance: {resultBalance:C}");
                break;
            case Operations.Deposit:
                Console.WriteLine(
                    $" Deposit successful. New balance: {resultBalance:C}");
                break;
            case Operations.Withdraw:
                Console.WriteLine(
                    $" Withdrawal successful. New balance: {resultBalance:C}");
                break;
            case Operations.Exit:
                Console.WriteLine("Exiting account...");
                break;
            default:
                Console.WriteLine("Unknown operation.");
                break;
        }
    }

    private static Operations? ParseOperation(string operation)
    {
        return operation.ToLower() switch
        {
            "b" => Operations.Balance,
            "d" => Operations.Deposit,
            "w" => Operations.Withdraw,
            "e" => Operations.Exit,
            _ => null
        };
    }

    private static decimal Operate(Operations operation, Bank bank, Account account)
    {
        return operation switch
        {
            Operations.Balance => account.GetBalance(),
            Operations.Deposit => account.Deposit(),
            Operations.Withdraw => account.Withdraw(),
            Operations.Exit => account.Exit(),
            _ => throw new InvalidOperationException("Unsupported operation.")
        };
    }

    private static decimal GetBalance(this Account account) => account.Operate(AccountActions.Balance);


    private static decimal Deposit(this Account account)
    {
        if (!TakeInput("amount to deposit", out var depText) || !decimal.TryParse(depText, out var depAmt))
            throw new ArgumentException("Invalid amount");
        return account.Operate(AccountActions.Deposit, depAmt);
    }

    private static decimal Withdraw(this Account account)
    {
        if (!TakeInput("amount to withdraw", out var withText) || !decimal.TryParse(withText, out var withAmt))
            throw new ArgumentException("Invalid amount");
        return account.Operate(AccountActions.Withdraw, withAmt);
    }

    private static decimal Exit(this Account account)
    {
        _running = false;
        return account.Operate(AccountActions.Exit);
    }

    private static bool TakeInput(string variable, out string input)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input))
            return false;
        return true;
    }

    private static bool CheckProgramEnd(string input)
    {
        if (input != "-") return false;
        _running = false;
        return true;
    }
}