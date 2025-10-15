using P5_Vreme;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_Login
{
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
                if (!TakeInput("temp", out string input))
                    continue;

                if (input == "-")
                {
                    _running = false;
                    continue;
                }

                if (!Double.TryParse(input, out double temp))
                {
                    Console.WriteLine("Enter valid temperature!");
                    continue;
                }

                Console.WriteLine($"Cities {String.Join(',', WeatherData.Data.Keys)}");
                if (!TakeInput("city", out string city))
                    continue;
                if (!WeatherData.Data.ContainsKey(city))
                {
                    Console.WriteLine($"{city} is not a valid city!");
                    continue;
                }

                var currentSeasson = SeassonHelper.SeasonFromMonth(DateTime.UtcNow.Month);
                var seassonMean = WeatherData.Data[city][currentSeasson];
                Console.WriteLine($"Your temperature is {DescribeTemperature(temp, seassonMean)}");
            }
        }

        static string DescribeTemperature(double temp, double mean)
        {
            double diff = temp - mean;
            if (diff > 7) return "very hot";
            if (diff > 3) return "hot";
            if (diff < -3) return "cold";
            return "normal";
        }

        private bool TakeInput(string param, out string input)
        {
            Console.WriteLine($"Enter a {param}: ");
            var userInput = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine($"Please enter a valid {param}!");
                input = string.Empty;
                return false;
            }
            input = userInput;
            return true;
        }
    }
}
