using System.Collections;
using System.Collections.Immutable;
using P10_Diacritice.TextOperator;

namespace P10_Diacritice;

public class TextAnnalize
{
    private bool _running = true;
    private readonly ImmutableList<TextOperations> _operations;

    public TextAnnalize()
    {
        _operations = ImmutableList.Create(Enum.GetValues<TextOperations>());

        Main();
    }

    private void Main()
    {
        Console.WriteLine("Text info is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("text", out var input))
                continue;

            if (input == "-")
            {
                _running = false;
                continue;
            }

            var results = Operations.ApplyOperations(_operations, input);
            results.ToList().ForEach(res => PrintOperationResult(res.Operation, res.Result));
        }
    }

    private void PrintOperationResult(TextOperations op, int result)
    {
        switch (op)
        {
            case TextOperations.VowelsNumber:
                Console.WriteLine($"The text contains {result} vowels.");
                break;
            case TextOperations.ConsonantsNumber:
                Console.WriteLine($"The text contains {result} consonants.");
                break;
            case TextOperations.WordsNumber:
                Console.WriteLine($"The text contains {result} words.");
                break;
            case TextOperations.CharactersNumber:
                Console.WriteLine($"The text contains {result} characters.");
                break;
            default:
                throw new InvalidOperationException("Unsupported operation");
        }
    }

    private bool TakeInput(string param, out string input)
    {
        Console.WriteLine($"Enter a {param}: ");
        var userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine($"Please enter a valid {param}!");
            input = string.Empty;
            return false;
        }

        input = userInput;
        return true;
    }
}