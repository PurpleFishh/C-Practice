using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Ani
{
    public class YearsInfo
    {

        private bool _running = true;
        private Stack<Year> history = new();

        public YearsInfo()
        {
            Main();
        }

        private void Main()
        {
            Console.WriteLine("Year info is running(- to stop it, H for history)");
            while (_running)
            {
                Console.WriteLine("Enter a year: ");
                var input = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a year!");
                    continue;
                }

                if (input == "-")
                {
                    _running = false;
                    continue;
                }

                if (input == "H")
                {
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
                history.Push(yearInfo);
            }
        }

        private void DisplayHistory()
        {
            Console.WriteLine("History: ");
            if (history.Count == 0)
            {
                Console.WriteLine("History empty!");
                return;
            }

            Console.WriteLine(String.Join('\n', history.Select(year => year.ToString())));
        }
    }
}
