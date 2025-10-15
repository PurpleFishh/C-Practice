using System.Globalization;

namespace P7_Ghici
{
    public class NumberFinder
    {
        private bool _running = true;
        private INumberGenerator _numberGenerator;

        public NumberFinder()
        {
            _numberGenerator = new NumberGenerator();
            Main();
        }

        private void Main()
        {
            Console.WriteLine("Number finder is running(- to stop it)");
            while (_running)
            {
                if (!TakeInput("number(between 0 and 100)", out var input))
                    continue;

                if (input == "-")
                {
                    _running = false;
                    continue;
                }

                if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.CurrentCulture, out var number))
                {
                    Console.WriteLine("Enter valid number!");
                    continue;
                }

                if (number < 0 || number > 100)
                {
                    Console.WriteLine("Enter number between 0 and 100!");
                    continue;
                }

                int? leftRange = null;
                var rightRange = 100;

                while (true)
                {
                    var generated = _numberGenerator.GenerateNumber(leftRange, rightRange);
                    Console.WriteLine($"Is it... {generated}?");
                    if (!TakeInput("estimation(<, >, =)", out var estimation))
                        continue;
                    if (estimation != "<" && estimation != ">" && estimation != "=")
                    {
                        Console.WriteLine("Enter valid estimation!");
                        continue;
                    }

                    switch (estimation)
                    {
                        case "=":
                            Console.WriteLine(generated != number ? "Why are you lying?? Game Over!" : "Great!");
                            return;
                        case "<":
                            rightRange = generated - 1;
                            break;
                        case ">":
                            leftRange = generated + 1;
                            break;
                    }
                }
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
}