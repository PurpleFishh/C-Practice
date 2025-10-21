namespace P15_Magazin.ShopLibrary.Repository;

public interface IRepository<T>
{
    void Add(T item);
    void Save(IEnumerable<T> items);
    IEnumerable<T> GetAll();
    T? GetByName(string name);
    T Upsert(T product);
    bool DeleteByName(string name);
}