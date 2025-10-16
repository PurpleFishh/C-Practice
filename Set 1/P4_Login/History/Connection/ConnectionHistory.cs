namespace P4_Login.History.Connection;

public class ConnectionHistory : IConnectionHistory
{
    private readonly Stack<(DateTime date, bool success)> _history = new();

    public void Add((DateTime date, bool success) item)
    {
        _history.Push(item);
    }

    public IEnumerable<(DateTime date, bool success)> GetHistory()
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
        return string.Join('\n', _history.Select(login => $"{login.date}: {login.success}"));
    }
}