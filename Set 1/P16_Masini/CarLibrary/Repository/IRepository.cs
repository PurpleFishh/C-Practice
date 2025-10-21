namespace P16_Masini.CarLibrary.Repository;

public interface IRepository<T>
{
    void Add(T item);
    void Save(IEnumerable<T> cars);
    IEnumerable<T> GetAll();
    T? GetByModel(string model);
    T Upsert(T product);
    bool DeleteByModel(string name);
}