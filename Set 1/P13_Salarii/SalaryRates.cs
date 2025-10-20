namespace P13_Salarii;

public abstract class SalaryRates
{
    public virtual decimal CasRate => 0.25m;
    public virtual decimal CassRate => 0.10m;
    public virtual decimal IncomeTaxRate => 0.10m;
    public virtual int AvgWeeksPerMonth => 4;
}