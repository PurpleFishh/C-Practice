using P3_Ani.History.Calendar;

namespace P3_Ani.History.Calendar;

public class CalendarHistory : ICalendarHistory
{
    private readonly Stack<Year> _history = new();

    public void Add(Year item)
    {
        _history.Push(item);
    }

    public IEnumerable<Year> GetHistory()
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
        return string.Join('\n', _history.Select(year => year.ToString()));
    }
}