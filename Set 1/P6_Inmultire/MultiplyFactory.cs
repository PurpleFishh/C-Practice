using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6_Inmultire
{
    public class MultiplyFactory
    {
        private bool _running = true;

        public MultiplyFactory()
        {
            Main();
        }

        private void Main()
        {
            Console.WriteLine("Multiply is running(- to stop it)");
            while (_running)
            {
                if (!TakeInput("number", out var input))
                    continue;

                if (input == "-")
                {
                    _running = false;
                    continue;
                }

                if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.CurrentCulture, out var mulMax))
                {
                    Console.WriteLine("Enter valid number!");
                    continue;
                }

                Console.WriteLine($"Multiply table for {mulMax}");

                var tblSpace = $"{mulMax * mulMax}".Length + 1;
                var tbl = new Table(tblSpace);

                tbl.PrintCell("x");
                Enumerable.Range(1, mulMax).ToList().ForEach(i => tbl.PrintCell(i));
                tbl.EndLine();

                for (var i = 1; i <= mulMax; i++)
                {
                    tbl.PrintCell(i);
                    for (int j = 1, acc = i; j <= mulMax; j++, acc += i)
                        tbl.PrintCell(acc);
                    tbl.EndLine();
                }
            }
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