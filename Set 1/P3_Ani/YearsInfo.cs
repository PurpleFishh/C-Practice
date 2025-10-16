using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P3_Ani.History.Calendar;

namespace P3_Ani;

public class YearsInfo
{
    private bool _running = true;
    private readonly ICalendarHistory _history = new CalendarHistory();

    public YearsInfo()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Year info is running(- to stop it, H for history)");
        while (_running)
        {
            if (!TakeInput("year", out var input))
            {
                Console.WriteLine("Please enter a year!");
                continue;
            }

            switch (input)
            {
                case "-":
                    _running = false;
                    continue;
                case "H":
                    DisplayHistory();
                    continue;
            }

            if (!int.TryParse(input, out var year))
            {
                Console.WriteLine("Please enter a valid year!");
                continue;
            }

            var yearInfo = new Year(year);
            Console.WriteLine(yearInfo);
            _history.Add(yearInfo);
        }
    }

    private void DisplayHistory()
    {
        Console.WriteLine("History: ");
        if (_history.Count == 0)
        {
            Console.WriteLine("History empty!");
            return;
        }

        Console.WriteLine(_history);
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