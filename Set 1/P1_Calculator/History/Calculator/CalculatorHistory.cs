namespace P1_Calculator.History.Calculator;

public class CalculatorHistory: ICalculatorHistory
{
    private readonly Stack<(string expression, double result)> _history = new();
    
    public void Add((string expression, double result) item)
    {
        _history.Push(item);
    }

    public IEnumerable<(string expression, double result)> GetHistory()
    {
        return _history.AsEnumerable();
    }

    public void Clear()
    {
        _history.Clear();
    }

    public int Count => _history.Count;

    public override string ToString()
    {
        return string.Join('\n', _history.Select(h => $"{h.expression} = {h.result}"));
    }
}