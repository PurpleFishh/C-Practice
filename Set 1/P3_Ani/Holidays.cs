using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Ani;

public static class Holidays
{
    // Hardcoded only for 2025
    public static readonly ImmutableHashSet<DateTime> HolidaysList = new HashSet<DateTime>
    {
        new DateTime(2025, 1, 1), // New Year’s Day
        new DateTime(2025, 1, 2), // Second Day of New Year
        new DateTime(2025, 1, 6), // Epiphany (Boboteaza)
        new DateTime(2025, 1, 7), // St. John the Baptist
        new DateTime(2025, 1, 24), // Day of the Unification of the Romanian Principalities
        new DateTime(2025, 4, 18), // Orthodox Good Friday
        new DateTime(2025, 4, 20), // Orthodox Easter Sunday
        new DateTime(2025, 4, 21), // Orthodox Easter Monday
        new DateTime(2025, 5, 1), // Labour Day / May Day
        new DateTime(2025, 6, 1), // Children’s Day
        new DateTime(2025, 6, 8), // Pentecost Sunday (Orthodox)
        new DateTime(2025, 6, 9), // Pentecost Monday (Whit Monday)
        new DateTime(2025, 8, 15), // Assumption of Mary (St Mary’s Day)
        new DateTime(2025, 11, 30), // St. Andrew’s Day
        new DateTime(2025, 12, 1), // National Day (Great Union Day)
        new DateTime(2025, 12, 25), // Christmas Day
        new DateTime(2025, 12, 26) // Second day of Christmas (Boxing Day)
    }.ToImmutableHashSet();
}