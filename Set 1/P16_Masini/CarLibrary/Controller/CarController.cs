using System.Collections.Immutable;
using P16_Masini.CarLibrary.Service;
using P16_Masini.CarLibrary.Domain;
using P16_Masini.CarLibrary.Dtos;
using P16_Masini.CarLibrary.Mappers;

namespace P16_Masini.CarLibrary.Controller;

public class CarController
{
    private readonly ICarService _carService = new CarService();

    public void AddCar(string model,
        int manufactureYear,
        string motoryType,
        decimal consume,
        decimal co2)
    {
        _carService.AddCar(model, manufactureYear, motoryType, consume, co2);
    }

    public bool RemoveCar(string model) => _carService.RemoveCar(model);

    public IEnumerable<CarDto> GetCars()
    {
        var cars = _carService.GetCars();
        return cars.Select(car => CarDtoMapper.ToDto(car, _carService.IsCarEligibleForUnder18(car)));
    }

    public CarDto? GetCar(string model)
    {
        var car = _carService.GetCar(model);
        if (car == null)
            return null;
        var isEligibleForUnder18 = _carService.IsCarEligibleForUnder18(car);
        return CarDtoMapper.ToDto(car, isEligibleForUnder18);
    }

    public bool IsCarEligibleForUnder18(string model) => _carService.IsCarEligibleForUnder18(model);
}