namespace P1_Calculator.Calculator.Services;

public interface ICalculatorService
{
    public double Evaluate(string expression);

    public IEnumerable<(string expression, double result)> GetHistory();

    public string FormatedHistory();
}