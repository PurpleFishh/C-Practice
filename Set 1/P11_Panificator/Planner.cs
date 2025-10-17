using System.Globalization;

namespace P11_Panificator;

public class Planner
{
    private bool _running = true;
    private const string DateFormat = "dd/MM/yyyy HH:mm";

    public Planner()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Planner is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("person name", out var personName))
                continue;

            if (!TakeInput("location", out var location))
                continue;

            if (!TakeInput($"date time({DateFormat})", out var dateInput))
                continue;

            if (!DateTimeOffset.TryParseExact(dateInput, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out var date))
            {
                Console.WriteLine("Date format is invalid!");
                continue;
            }

            if (DateTimeOffset.Now > date)
            {
                Console.WriteLine("Date must be in the future!");
                continue;
            }

            Console.WriteLine($"Planning for {personName} in {location}...");

            var meet = new Meeting(personName, location, date);
            meet.WaitConfirmation();
            Console.WriteLine(meet);
        }
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