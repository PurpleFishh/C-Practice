using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9_Schimb_Valutar;

public class CurrencyMaster
{
    private bool _running = true;
    private readonly Dictionary<DateTime, Dictionary<Currency, decimal>> _data;

    public CurrencyMaster()
    {
        _data = CurrencyData.GetData();
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Schimb Valutar is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("money", out var input))
                continue;

            if (input == "-")
            {
                _running = false;
                continue;
            }

            if (!decimal.TryParse(input, out var money))
            {
                Console.WriteLine("Enter valid money!");
                continue;
            }

            Console.WriteLine($"Currency table for {money}RON");

            var today = DateTime.Today;
            var tblSpace = $"{DateFormat(today)}".Length + 3;
            // var tblSpace = $"{_data[DateTime.Today][CurrencyData.Currency.Eur] * money}".Length + 3;
            var tbl = new Table(tblSpace);

            tbl.PrintCell("Date/Curreny");
            // Enumerable.Range(0, 30).Select(i => DateTime.Today.AddDays(-i)).ToList().ForEach(type => tbl.PrintCell(type));
            Enum.GetNames(typeof(Currency)).ToList().ForEach(type => tbl.PrintCell(type));
            tbl.EndLine();


            for (var i = 0; i < 30; i++)
            {
                var date = today.AddDays(-i);
                tbl.PrintCell(DateFormat(date));
                _data[date].Values.ToList().ForEach(val => tbl.PrintCell($"{money / val:0.00}"));
                tbl.EndLine();
            }
        }
    }

    private string DateFormat(DateTime date) => date.ToShortDateString();

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