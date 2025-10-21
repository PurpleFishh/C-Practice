using P16_Masini.CarLibrary.Domain;

namespace P16_Masini.CarLibrary.Service;

public interface ICarService
{
    public Car AddCar(string model,
        int manufactureYear,
        string motoryType,
        decimal consume,
        decimal co2);

    public bool RemoveCar(string name);
    public IEnumerable<Car> GetCars();
    public bool IsCarEligibleForUnder18(string model);
    public bool IsCarEligibleForUnder18(Car car);
    public Car? GetCar(string name);
}