using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using P1_Calculator.Calculator;
using P1_Calculator.History.Calculator;

namespace P1_Calculator;

public class CalculatorManager
{
    private bool _powerOn = true;
    private readonly CalculatorController _controller = new();

    public CalculatorManager()
    {
        Input();
    }

    private void Input()
    {
        Console.WriteLine("Calculator ON! (Options: X to stop the calculator, H for history)");
        while (_powerOn)
        {
            Console.Write("Enter the expression: ");
            var expression = Console.ReadLine() ?? string.Empty;

            var operation = GetOperation(expression);
            try
            {
                switch (operation)
                {
                    case CalculatorOperations.Stop:
                        _controller.Stop(StopProgram);
                        break;
                    case CalculatorOperations.History:
                        _controller.ShowHistory();
                        break;
                    case CalculatorOperations.Evaluate:
                        _controller.EvaluateExpression(expression);
                        break;
                    default:
                        throw new SyntaxErrorException();
                }
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
    }

    private CalculatorOperations GetOperation(string input) =>
        input.ToUpper() switch
        {
            "X" => CalculatorOperations.Stop,
            "H" => CalculatorOperations.History,
            _ => CalculatorOperations.Evaluate
        };


    private void StopProgram()
    {
        _powerOn = false;
    }
}