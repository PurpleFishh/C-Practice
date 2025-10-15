namespace P9_Schimb_Valutar
{
    public static class CurrencyData
    {
        public enum Currency
        {
            Eur,
            Usd,
            Gbp
        }
        public static Dictionary<DateTime, Dictionary<Currency, decimal>> GetData()
        {
            var rates = new Dictionary<DateTime, Dictionary<Currency, decimal>>();
            var today = DateTime.Today;

            for (var i = 0; i < 30; i++)
            {
                var date = today.AddDays(-i);

                rates[date] = new Dictionary<Currency, decimal>
                {
                    [Currency.Eur] = 5.00m + (i % 5) * 0.01m,
                    [Currency.Usd] = 4.75m + (i % 4) * 0.01m,
                    [Currency.Gbp] = 6.00m + (i % 6) * 0.01m
                };
            }

            return rates;
        }
    }
}