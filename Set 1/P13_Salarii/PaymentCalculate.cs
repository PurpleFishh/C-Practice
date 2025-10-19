using System.Globalization;

namespace P13_Salarii;

public static class PaymentCalculate
{
    public static List<RateSegment> ParsePaymentSchedule(string raw, out bool ok)
    {
        ok = true;
        if (decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var single))
            return [new RateSegment { Start = DateTimeOffset.MinValue, Rate = single }];

        var schedule = new List<RateSegment>();
        try
        {
            var parts = raw.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var part in parts)
                if (ParseRateDate(part, out var rate))
                    schedule.Add(rate!);
                else
                {
                    ok = false;
                    break;
                }

            if (!ok || schedule.Count == 0)
            {
                ok = false;
                return [];
            }

            schedule = schedule
                .OrderBy(s => s.Start)
                .GroupBy(s => s.Start)
                .Select(g => new RateSegment { Start = g.Key, Rate = g.Last().Rate })
                .ToList();

            return schedule;
        }
        catch
        {
            ok = false;
            return [];
        }
    }

    private static bool ParseRateDate(string str, out RateSegment? rateSegment)
    {
        rateSegment = null;
        var kv = str.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (kv.Length != 2)
            return false;

        var dateToken = kv[0];
        var rateToken = kv[1];

        if (!decimal.TryParse(rateToken, NumberStyles.Number, CultureInfo.InvariantCulture, out var rate) ||
            rate <= 0)
            return false;


        DateTimeOffset start;
        if (int.TryParse(dateToken, out var year) && year is >= 1900 and <= 3000)
            start = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero).ToLocalTime();
        else
            return false;

        rateSegment = new RateSegment { Start = start, Rate = rate };
        return true;
    }

    public static decimal GetRateForDate(List<RateSegment> schedule, DateTimeOffset pointInTime)
    {
        var interval = schedule.FindLast(rate => rate.Start <= pointInTime) ?? schedule[0];
        return interval.Rate;
    }

    public static (decimal totalGross, decimal totalNet) ComputeTotals(
        decimal hoursPerWeek,
        DateTimeOffset employmentStart,
        DateTimeOffset now,
        List<RateSegment> schedule,
        SalaryRates rates)
    {
        var segments = new List<RateSegment>(schedule);
        segments.Add(new RateSegment { Start = DateTimeOffset.MaxValue, Rate = segments[^1].Rate });

        var totalGross = 0m;

        for (var i = 0; i < segments.Count - 1; i++)
        {
            var segStart = segments[i].Start;
            var segEnd = segments[i + 1].Start;
            var rate = segments[i].Rate;

            var start = (employmentStart, segStart).Max();
            var end = (now, segEnd).Min();

            if (end <= start) continue;

            var weeks = (decimal)(end - start).TotalDays / 7m;
            var gross = hoursPerWeek * rate * weeks;
            totalGross += gross;
        }

        var totalCas = totalGross * rates.CasRate;
        var totalCass = totalGross * rates.CassRate;
        var taxable = totalGross - totalCas - totalCass;
        var totalIncomeTax = Math.Max(0m, taxable) * rates.IncomeTaxRate;
        var totalNet = totalGross - totalCas - totalCass - totalIncomeTax;

        return (totalGross, totalNet);
    }
}