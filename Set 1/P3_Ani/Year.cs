using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Ani;

public class Year
{
    public int CalendarYear { get; }
    public bool IsLeap { get; private set; } = false;
    public int WorkingDays { get; private set; }
    public int HolidaysCount { get; } = Holidays.HolidaysList.Count;

    public Year(int calendarYear)
    {
        this.CalendarYear = calendarYear;
        SetYearDetails();
    }

    private void SetYearDetails()
    {
        IsLeap = (CalendarYear % 400 == 0) || (CalendarYear % 4 == 0 && CalendarYear % 100 != 0);
        WorkingDays = Calendar.GetWorkingDays(CalendarYear, IsLeap).Count;
    }

    public int GetDaysInYear => IsLeap ? 366 : 365;

    public override string ToString()
    {
        return
            $"Year {CalendarYear} details: \n\tLeap year: {IsLeap} \n\tYear days: {GetDaysInYear} \n\tWorking days: {WorkingDays} \n\tHolydays: {Holidays.HolidaysList.Count}";
    }
}