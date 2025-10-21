namespace P16_Masini.CarLibrary.Dtos;

public class CarDto
{
    public string Model { get; set; }
    public int ManufactureYear { get; set; }
    public string MotoryType { get; set; }
    public decimal Consume { get; set; }
    public decimal Co2 { get; set; }
    public bool EligibleUnder18 { get; set; }

    public override string ToString()
    {
        return $"Model: {Model}, Year: {ManufactureYear}, Motor Type: {MotoryType}, " +
               $"Consume: {Consume} L/100km, CO₂ Emissions: {Co2} g/km, Eligible for under 18: {EligibleUnder18}";
    }
}