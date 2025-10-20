using P1_Calculator.Calculator.Services;

namespace P1_Calculator.Calculator;

public class CalculatorController
{
    private readonly ICalculatorService _calculatorService = new CalculatorService();

    public void EvaluateExpression(string expression)
    {
        var result = _calculatorService.Evaluate(expression);
        Console.WriteLine($"Result: {result}");
    }

    public void ShowHistory()
    {
        Console.WriteLine("Calculator History:");
        Console.WriteLine(_calculatorService.FormatedHistory());
    }

    public void Stop(Action stopAction)
    {
        stopAction();
        Console.WriteLine("Calculator Stopped!");
    }
}