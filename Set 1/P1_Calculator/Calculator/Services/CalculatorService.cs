using System.Data;
using P1_Calculator.History.Calculator;

namespace P1_Calculator.Calculator.Services;

public class CalculatorService : ICalculatorService
{
    private readonly IEvaluator _evaluator = new SystemEvaluator();
    private readonly ICalculatorHistory _history = new CalculatorHistory();

    public double Evaluate(string expression)
    {
        try
        {
            var result = _evaluator.Evaluate(expression);
            _history.Add((expression, result));
            return result;
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

        return 0;
    }

    public IEnumerable<(string expression, double result)> GetHistory() => _history.GetHistory();

    public string FormatedHistory()
    {
        if (_history.Count == 0)
            return "History empty!";

        return _history.ToString()!;
    }
}