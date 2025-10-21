namespace P16_Masini.CarLibrary.Domain;

public class Car(string model, int manufactureYear, string motoryType, decimal consume, decimal co2)
{
    public string Model { get; private set; } = model;
    public int ManufactureYear { get; set; } = manufactureYear;
    public string MotoryType { get; set; } = motoryType;
    public decimal Consume { get; set; } = consume;
    public decimal Co2 { get; set; } = co2;

    public override string ToString()
    {
        return $"Model: {Model}, Year: {ManufactureYear}, Motor Type: {MotoryType}, " +
               $"Consume: {Consume} L/100km, CO₂ Emissions: {Co2} g/km";
    }
}