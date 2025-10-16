namespace P10_Diacritice;

public class TextAnnalize
{
    private bool _running = true;
    private readonly char[] _romanianDiacritics = { 'ă', 'â', 'î', 'ș', 'ț' };

    public TextAnnalize()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Gaseste diacritica is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("text", out var input))
                continue;

            if (input == "-")
            {
                _running = false;
                continue;
            }

            var diacriticCount = input
                .ToLower()
                .Count(c => _romanianDiacritics.Contains(c));
            
            Console.WriteLine($"The text contains {diacriticCount} diacritics.");
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