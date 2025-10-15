using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Ani
{
    public class Year
    {
        public int year { get; }
        public bool isLeap { get; private set; } = false;
        public int workingDays { get; private set; }
        public int holidays { get; } = Holydays.HolydaysList.Count;

        public Year(int year)
        {
            this.year = year;
            SetYearDetails();
        }

        private void SetYearDetails()
        {
            isLeap = (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0);
            workingDays = Holydays.GetWorkingDays(year, isLeap).Count;
        }

        public int GetDaysInYear => isLeap ? 366 : 365;


        public override string ToString()
        {
            return $"Year {year} details: \n\tLeap year: {isLeap} \n\tYear days: {GetDaysInYear} \n\tWorking days: {workingDays} \n\tHolydays: {Holydays.HolydaysList.Count}";
        }

    }
}
