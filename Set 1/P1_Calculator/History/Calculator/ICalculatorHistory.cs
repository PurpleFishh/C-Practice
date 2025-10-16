namespace P1_Calculator.History.Calculator;

public interface ICalculatorHistory : IHistory<(string expression, double result)>
{
}