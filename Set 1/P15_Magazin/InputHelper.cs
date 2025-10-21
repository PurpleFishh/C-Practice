namespace P15_Magazin;

public static class InputHelper
{
    public static bool TakeInput(string param, out string input)
        => TakeInput(param, out input, Console.In, Console.Out);

    public static bool TakeInput(string param, out string input, TextReader inputReader, TextWriter outputWriter)
    {
        outputWriter.WriteLine($"Enter a {param}: ");
        var userInput = inputReader.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput))
        {
            outputWriter.WriteLine($"Please enter a valid {param}!");
            input = string.Empty;
            return false;
        }

        input = userInput;
        return true;
    }
}