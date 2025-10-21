using P16_Masini.CarLibrary.Domain;
using P16_Masini.CarLibrary.Dtos;

namespace P16_Masini.CarLibrary.Mappers;

public static class CarDtoMapper
{
    public static CarDto ToDto(Car car, bool eligibleUnder18) => new CarDto
    {
        Model = car.Model,
        ManufactureYear = car.ManufactureYear,
        MotoryType = car.MotoryType,
        Consume = car.Consume,
        Co2 = car.Co2,
        EligibleUnder18 = eligibleUnder18
    };
}