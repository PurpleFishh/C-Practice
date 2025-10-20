namespace P13_Salarii;

public class SalaryRates2025 : SalaryRates
{
    public override decimal CasRate => 0.25m;
    public override decimal CassRate => 0.10m;
    public override decimal IncomeTaxRate => 0.10m;
    public override int AvgWeeksPerMonth => 4;
}