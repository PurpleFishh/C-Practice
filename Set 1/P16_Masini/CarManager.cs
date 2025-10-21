using System.Collections.Immutable;
using P16_Masini.CarLibrary.Controller;

namespace P16_Masini;

public class CarManager
{
    private bool _running = true;
    private readonly CarController _controller = new();

    public CarManager()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Car Manager is running(- to stop it)");
        while (_running)
        {
            Console.WriteLine(
                "Operations: a - add new car, s - show cars, r - remove car, v - view car");
            if (!TakeInput("operation", out var operation))
                continue;
            var carAction = ParseOperation(operation);
            if (!carAction.HasValue)
            {
                Console.WriteLine("Invalid operation");
                continue;
            }

            bool isSuccess;
            string? msg;
            switch (carAction)
            {
                case CarActions.AddCar:
                    (isSuccess, msg) = AddCar();
                    if (!isSuccess)
                        Console.WriteLine($"Operation failed: {msg}");
                    else
                        Console.WriteLine("Car added");
                    break;
                case CarActions.RemoveCar:
                    (isSuccess, msg) = RemoveCar();
                    if (!isSuccess)
                        Console.WriteLine($"Operation failed: {msg}");
                    else
                        Console.WriteLine("Car removed");
                    break;
                case CarActions.ShowCars:
                    (_, msg) = ShowCars();
                    Console.WriteLine("Cars:\n" + msg);
                    break;
                case CarActions.ViewCar:
                    (_, msg) = ViewCar();
                    Console.WriteLine("Car info: " + msg);
                    break;
                default:
                    Console.WriteLine("Invalid car action");
                    break;
            }
        }
    }

    private CarActions? ParseOperation(string operation)
    {
        return operation.ToLower() switch
        {
            "a" => CarActions.AddCar,
            "s" => CarActions.ShowCars,
            "r" => CarActions.RemoveCar,
            "v" => CarActions.ViewCar,
            _ => null
        };
    }

    private (bool isSuccess, string? msg) AddCar()
    {
        if (!TakeInput("car model", out var model))
            return (false, "Invalid car model");
        if (!TakeInput("manufacture year", out var yearInput))
            return (false, "Invalid manufacture year");
        if (!int.TryParse(yearInput, out var manufactureYear))
            return (false, "Invalid manufacture year format");
        if (!TakeInput("motor type", out var motoryType))
            return (false, "Invalid motor type");
        if (!TakeInput("consume", out var consumeInput))
            return (false, "Invalid consume value");
        if (!decimal.TryParse(consumeInput, out var consume))
            return (false, "Invalid consume format");
        if (!TakeInput("CO2 emission", out var co2Input))
            return (false, "Invalid CO2 emission value");
        if (!decimal.TryParse(co2Input, out var co2))
            return (false, "Invalid CO2 emission format");

        _controller.AddCar(model, manufactureYear, motoryType, consume, co2);
        return (true, null);
    }

    private (bool isSuccess, string? msg) RemoveCar()
    {
        if (!TakeInput("car model", out var model))
            return (false, "Invalid car model");
        if (!_controller.RemoveCar(model))
            return (false, "Car not found");
        return (true, null);
    }

    private (bool isSuccess, string? msg) ShowCars()
    {
        var cars = _controller.GetCars().ToList();
        if (cars.Count == 0)
            return (true, "No cars found");
        var str = string.Join("\n", cars);
        return (true, str);
    }

    private (bool isSuccess, string? msg) ViewCar()
    {
        if (!TakeInput("car model", out var model))
            return (false, "Invalid car model");
        var car = _controller.GetCar(model);
        if (car == null)
            return (false, "Car not found");

        return (true, car.ToString());
    }

    private bool TakeInput(string variable, out string input)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input))
            return false;
        return true;
    }

    private bool CheckProgramEnd(string input)
    {
        if (input != "-") return false;
        _running = false;
        return true;
    }
}