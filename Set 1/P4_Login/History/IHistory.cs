namespace P4_Login.History;

public interface IHistory<T>
{
    void Add(T item);
    IEnumerable<T> GetHistory();
    void Clear();
    int Count { get; }
}