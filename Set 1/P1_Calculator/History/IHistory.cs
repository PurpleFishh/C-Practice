namespace P1_Calculator.History;

public interface IHistory<T>
{
    void Add(T item);
    IEnumerable<T> GetHistory();
    void Clear();
    int Count { get; }
}