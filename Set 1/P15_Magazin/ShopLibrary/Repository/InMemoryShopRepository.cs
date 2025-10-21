using System.Collections.Concurrent;
using P15_Magazin.ShopLibrary.Domain;

namespace P15_Magazin.ShopLibrary.Repository;

public class InMemoryShopRepository : IRepository<Product>
{
    private readonly ConcurrentDictionary<string, Product> _store = new(StringComparer.OrdinalIgnoreCase);

    public void Add(Product product)
    {
        Validate(product);
        if (!_store.TryAdd(product.Name, Clone(product)))
            throw new InvalidOperationException($"Product with name '{product.Name}' already exists.");
    }

    public void Save(IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);
        _store.Clear();
        foreach (var p in products)
        {
            Validate(p);
            _store[p.Name] = Clone(p);
        }
    }

    public IEnumerable<Product> GetAll()
        => _store.Values
            .OrderBy(p => p.Name)
            .Select(Clone)
            .ToArray();

    public Product? GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        return _store.TryGetValue(name, out var p) ? Clone(p) : null;
    }

    public Product Upsert(Product product)
    {
        Validate(product);
        return _store.AddOrUpdate(product.Name, _ => Clone(product), (_, __) => Clone(product));
    }

    public bool DeleteByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        return _store.TryRemove(name, out _);
    }

    private static void Validate(Product p)
    {
        ArgumentNullException.ThrowIfNull(p);
        if (string.IsNullOrWhiteSpace(p.Name)) throw new ArgumentException("Product.Name is required");
        if (p.Price < 0) throw new ArgumentOutOfRangeException(nameof(p.Price), "Price cannot be negative");
        if (p.Stock < 0) throw new ArgumentOutOfRangeException(nameof(p.Stock), "Stock cannot be negative");
    }

    private static Product Clone(Product p) => new(p.Name, p.Price, p.Stock);
}