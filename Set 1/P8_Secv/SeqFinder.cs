using System.Globalization;

namespace P8_Secv;

public class SeqFinder
{
    private bool _running = true;

    public SeqFinder()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Seq finder is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("number", out var input))
                continue;

            if (input == "-")
            {
                _running = false;
                continue;
            }

            if (!int.TryParse(input, out var number))
            {
                Console.WriteLine("Enter valid number!");
                continue;
            }

            if (!TakeInput(
                    "sequence(fib - fibonacci, g - geometric, n - prime numbers, fac - factorial, t- triunghiular, pp - patrate perfecte)",
                    out var seqTypeInput))
                continue;
            if (!GetSeqType(seqTypeInput, out var seqType))
            {
                Console.WriteLine("Enter valid sequence type!");
                continue;
            }

            var seqGenerator = SequenceFactory.GetSeqGenerator(seqType);
            var seq = seqGenerator.GetSequence(number);
            Console.WriteLine("Sequence found: " + string.Join(", ", seq));
        }
    }

    private bool GetSeqType(string sequence, out SeqType seqType)
    {
        seqType = SeqType.PerfectSq;
        switch (sequence)
        {
            case "fib": seqType = SeqType.Fibonacci; break;
            case "g": seqType = SeqType.Geometric; break;
            case "n": seqType = SeqType.PrimeNums; break;
            case "fac": seqType = SeqType.Factorial; break;
            case "t": seqType = SeqType.Tri; break;
            case "pp": seqType = SeqType.PerfectSq; break;
            default: return false;
        }

        return true;
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