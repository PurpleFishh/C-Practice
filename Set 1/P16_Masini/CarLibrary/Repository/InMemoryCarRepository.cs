using System.Collections.Concurrent;
using P16_Masini.CarLibrary.Domain;

namespace P16_Masini.CarLibrary.Repository;

public class InMemoryCarRepository : IRepository<Car>
{
    private readonly ConcurrentDictionary<string, Car> _store = new(StringComparer.OrdinalIgnoreCase);

    public void Add(Car car)
    {
        Validate(car);
        if (!_store.TryAdd(car.Model, Clone(car)))
            throw new InvalidOperationException($"Product with name '{car.Model}' already exists.");
    }

    public void Save(IEnumerable<Car> cars)
    {
        ArgumentNullException.ThrowIfNull(cars);
        _store.Clear();
        foreach (var p in cars)
        {
            Validate(p);
            _store[p.Model] = Clone(p);
        }
    }

    public IEnumerable<Car> GetAll()
        => _store.Values
            .OrderBy(p => p.Model)
            .Select(Clone)
            .ToArray();

    public Car? GetByModel(string model)
    {
        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model is required", nameof(model));
        return _store.TryGetValue(model, out var p) ? Clone(p) : null;
    }

    public Car Upsert(Car car)
    {
        Validate(car);
        return _store.AddOrUpdate(car.Model, _ => Clone(car), (_, _) => Clone(car));
    }

    public bool DeleteByModel(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Model is required", nameof(name));
        return _store.TryRemove(name, out _);
    }

    private static void Validate(Car p)
    {
        ArgumentNullException.ThrowIfNull(p);
        if (string.IsNullOrWhiteSpace(p.Model)) throw new ArgumentException("Car.Model is required");
        if (p.Co2 < 0) throw new ArgumentOutOfRangeException(nameof(p.Co2), "Co2 cannot be negative");
        if (p.Consume < 0) throw new ArgumentOutOfRangeException(nameof(p.Consume), "Consume cannot be negative");
        if (p.ManufactureYear is < 1000 or > 9999)
            throw new ArgumentOutOfRangeException(nameof(p.Consume), "ManufactureYear must be a valid year");
    }

    private static Car Clone(Car p) => new(p.Model, p.ManufactureYear, p.MotoryType, p.Consume, p.Co2);
}