using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_Vreme;

public class Weather
{
    private bool _running = true;

    public Weather()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Weather is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("temp", out var input))
                continue;

            if (input == "-")
            {
                _running = false;
                continue;
            }

            if (!double.TryParse(input, out var temp))
            {
                Console.WriteLine("Enter valid temperature!");
                continue;
            }

            Console.WriteLine($"Cities {string.Join(',', WeatherData.Data.Keys)}");
            if (!TakeInput("city", out var city))
                continue;
            if (!WeatherData.Data.ContainsKey(city))
            {
                Console.WriteLine($"{city} is not a valid city!");
                continue;
            }

            var currentSeason = SeasonHelper.SeasonFromMonth(DateTime.UtcNow.Month);
            var seasonMean = WeatherData.Data[city][currentSeason];
            Console.WriteLine($"Your temperature is {DescribeTemperature(temp, seasonMean)}");
        }
    }

    static string DescribeTemperature(double temp, double mean)
    {
        var diff = temp - mean;
        return diff switch
        {
            > 7 => "very hot",
            > 3 => "hot",
            < -3 => "cold",
            _ => "normal"
        };
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