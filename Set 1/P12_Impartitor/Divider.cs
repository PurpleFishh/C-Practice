using System.Globalization;

namespace P12_Impartitor;

public class Divider
{
    private bool _running = true;

    public Divider()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Schimb Valutar is running(- to stop it)");
        Console.WriteLine(
            $"Current culture: {CultureInfo.CurrentCulture.DisplayName} (decimal separator: '{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}')");
        while (_running)
        {
            if (!TakeInput("number1", out var xInput))
                continue;

            if (xInput == "-")
            {
                _running = false;
                continue;
            }

            if (!TryParseDecimal(xInput, out var x))
            {
                Console.WriteLine("Invalid number1. Use your locale's decimal separator");
                continue;
            }

            if (!TakeInput("number2 (divisor)", out var yInput)) continue;
            if (yInput == "-")
            {
                _running = false;
                break;
            }

            if (!TryParseDecimal(yInput, out var y))
            {
                Console.WriteLine("Invalid number2. Use your locale's decimal separator");
                continue;
            }

            if (y == 0)
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
                continue;
            }

            TakeInput("number of decimals (press Enter for 2)", out var decInput, allowEmpty: true);
            if (decInput == "-")
            {
                _running = false;
                break;
            }

            var decimals = 2;
            if (decInput != string.Empty)
                if (!int.TryParse(decInput, out decimals) || decimals < 0 || decimals > 28)
                {
                    Console.WriteLine("Invalid decimals. Using 2");
                    decimals = 2;
                }

            var rounding = AskRoundingPolicy();
            if (!_running) break;

            var result = x / y;

            decimal rounded = Math.Round(result, decimals, rounding);

            Console.WriteLine();
            Console.WriteLine($"Raw result: {result}");
            Console.WriteLine($"Rounded result: {rounded} (decimals={decimals}, mode={ModeName(rounding)})");
            Console.WriteLine();
        }
    }

    private static string ModeName(MidpointRounding mode) =>
        mode switch
        {
            MidpointRounding.ToEven => "Banker's (Half To Even)",
            MidpointRounding.AwayFromZero => "Half Up (Away From Zero)",
            MidpointRounding.ToZero => "Toward Zero",
            MidpointRounding.ToNegativeInfinity => "Floor",
            MidpointRounding.ToPositiveInfinity => "Ceiling",
            _ => mode.ToString()
        };

    private MidpointRounding AskRoundingPolicy()
    {
        while (true)
        {
            Console.WriteLine("Choose rounding mode:");
            Console.WriteLine("  1) Banker's (Half To Even)");
            Console.WriteLine("  2) Half Up (Away From Zero)");
            Console.WriteLine("  3) Floor (toward -inf)");
            Console.WriteLine("  4) Ceiling (toward +inf)");
            Console.WriteLine("  5) Toward Zero (truncate)");

            TakeInput(" 1-5 (default 2)", out var choice, allowEmpty: true);
            if (choice == "-")
            {
                _running = false;
                return MidpointRounding.ToEven;
            }
            
            switch (choice.Trim())
            {
                case "1": return MidpointRounding.ToEven;
                case "2":
                case "": return MidpointRounding.AwayFromZero;
                case "3": return MidpointRounding.ToNegativeInfinity;
                case "4": return MidpointRounding.ToPositiveInfinity;
                case "5": return MidpointRounding.ToZero;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number 1-5.");
                    break;
            }
        }
    }


    private bool TakeInput(string param, out string input, bool allowEmpty = false)
    {
        Console.Write($"Enter {param}: ");
        var userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput))
        {
            input = string.Empty;
            return allowEmpty;
        }

        input = userInput;
        return true;
    }

    private static bool TryParseDecimal(string s, out decimal value)
        => decimal.TryParse(
            s,
            NumberStyles.Number | NumberStyles.AllowThousands,
            CultureInfo.CurrentCulture,
            out value
        );
}