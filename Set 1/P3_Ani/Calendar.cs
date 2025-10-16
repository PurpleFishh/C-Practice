namespace P3_Ani;

public static class Calendar
{
    public static List<DateTime> GetWorkingDays(int year, bool isLeap) => Enumerable.Range(0, isLeap ? 366 : 365)
        .Select(day => new DateTime(year, 1, 1).AddDays(day))
        .Where(date => date.DayOfWeek != DayOfWeek.Saturday &&
                       date.DayOfWeek != DayOfWeek.Sunday &&
                       !Holidays.HolidaysList.Any(h => h.Month == date.Month && h.Day == date.Day)
            // !Holidays.HolidaysList.Contains(date)
        ).ToList();
}