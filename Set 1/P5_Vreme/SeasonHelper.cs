namespace P5_Vreme;

public static class SeasonHelper
{
    public static Season SeasonFromMonth(int month)
    {
        return month switch
        {
            12 or 1 or 2 => Season.Iarna,
            3 or 4 or 5 => Season.Primavara,
            6 or 7 or 8 => Season.Vara,
            9 or 10 or 11 => Season.Toamna,
            _ => Season.Primavara
        };
    }
}