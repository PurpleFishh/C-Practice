using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P1_Calculator
{
    public class Calculator
    {
        private readonly IEvaluator _evaluator = new SystemEvaluator();
        private readonly Stack<(string expression, double result)> _history = new();
        private bool _powerOn = true;

        public Calculator()
        {
            Input();
        }

        public void Input()
        {
            Console.WriteLine("Calculator ON! (Options: X to stop the calculator, H for history)");
            while (_powerOn)
            {
                Console.Write("Enter the expression: ");
                string expression = Console.ReadLine() ?? string.Empty;

                switch (expression)
                {
                    case "":
                        Console.WriteLine("Please enter an valid expression!");
                        continue;
                    case "X":
                        Stop();
                        continue;
                    case "H":
                        ShowHistory();
                        continue;
                    default:
                        Evaluate(expression);
                        break;
                }
            }
        }

        private void Evaluate(string expression)
        {
            try
            {
                var result = _evaluator.Evaluate(expression);
                Console.WriteLine($"{expression} = {result}");
                _history.Push((expression, result));
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("ERROR: Dividing by zero is not permitted!");
            }
            catch (SyntaxErrorException)
            {
                Console.WriteLine("ERROR: Invalid expression!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private void Stop()
        {
            Console.WriteLine("Calculator Stopped!");
            _powerOn = false;
        }

        private void ShowHistory()
        {
            Console.WriteLine("Calculator History:");
            if (_history.Count == 0)
            {
                Console.WriteLine("History empty!");
                return;
            }
            Console.WriteLine(string.Join('\n', _history.Select(h => $"{h.expression} = {h.result}")));
        }
    }
}
