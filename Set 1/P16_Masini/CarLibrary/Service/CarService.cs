using P16_Masini.CarLibrary.Domain;
using P16_Masini.CarLibrary.Repository;

namespace P16_Masini.CarLibrary.Service;

public class CarService : ICarService
{
    private readonly IRepository<Car> _repository = new InMemoryCarRepository();

    public Car AddCar(string model,
        int manufactureYear,
        string motoryType,
        decimal consume,
        decimal co2)
    {
        var car = new Car(model, manufactureYear, motoryType, consume, co2);
        _repository.Add(car);
        return car;
    }

    public bool RemoveCar(string model) => _repository.DeleteByModel(model);

    public IEnumerable<Car> GetCars() => _repository.GetAll();
    public Car? GetCar(string model) => _repository.GetByModel(model);

    public bool IsCarEligibleForUnder18(Car car) => car.Co2 < 120;

    public bool IsCarEligibleForUnder18(string model)
    {
        var car = _repository.GetByModel(model);
        if (car == null)
            return false;

        return IsCarEligibleForUnder18(car);
    }
}