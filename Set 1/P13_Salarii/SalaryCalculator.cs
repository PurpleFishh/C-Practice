using System.Globalization;

namespace P13_Salarii;

public class SalaryCalculator
{
    private bool _running = true;
    private const string DateFormat = "dd/MM/yyyy HH:mm";
    private readonly SalaryRates _rates = new SalaryRates2025();

    public SalaryCalculator()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Planner is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("hours worked", out var hoursInput))
                continue;

            if (!decimal.TryParse(hoursInput, out var hours))
            {
                Console.WriteLine("Hours worked is invalid!");
                continue;
            }

            if (!TakeInput("hour payment (single value OR 'YYYY=rate;YYYY=rate')", out var paymentInput))
                continue;

            var schedule = PaymentCalculate.ParsePaymentSchedule(paymentInput, out var parseOk);
            if (!parseOk)
            {
                Console.WriteLine("Hour payment is invalid! Use a number or a schedule like '2021=35.5;2023=42'");
                continue;
            }

            if (!TakeInput($"employment date({DateFormat})", out var dateInput))
                continue;

            if (!DateTimeOffset.TryParseExact(dateInput, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces, out var date))
            {
                Console.WriteLine("Date format is invalid!");
                continue;
            }

            if (DateTimeOffset.Now < date)
            {
                Console.WriteLine("Date can not be in the future!");
                continue;
            }

            var now = DateTimeOffset.Now;
            var currentRate = PaymentCalculate.GetRateForDate(schedule, now);

            var currentGrossMonthly = hours * currentRate * _rates.AvgWeeksPerMonth;

            var currentCas = currentGrossMonthly * _rates.CasRate;
            var currentCass = currentGrossMonthly * _rates.CassRate;
            var currentTaxableBase = currentGrossMonthly - currentCas - currentCass;
            var currentIncomeTax = Math.Max(0m, currentTaxableBase) * _rates.IncomeTaxRate;
            var currentNetMonthly = currentGrossMonthly - currentCas - currentCass - currentIncomeTax;

            var (totalGross, totalNet) =
                PaymentCalculate.ComputeTotals(hours, date, now, schedule, _rates);

            var curr = CultureInfo.CurrentCulture;

            Console.WriteLine();
            Console.WriteLine("Salariu lunar curent");
            Console.WriteLine($"Brut: {currentGrossMonthly.ToString("C", curr)}");
            Console.WriteLine($"  - CAS (25%): {currentCas.ToString("C", curr)}");
            Console.WriteLine($"  - CASS (10%): {currentCass.ToString("C", curr)}");
            Console.WriteLine($"  - Impozit (10% din baza impozabila): {currentIncomeTax.ToString("C", curr)}");
            Console.WriteLine($"Net estimat: {currentNetMonthly.ToString("C", curr)}");

            Console.WriteLine();
            Console.WriteLine("Total de la angajare pana acum");
            Console.WriteLine($"Total brut: {totalGross.ToString("C", curr)}");
            Console.WriteLine($"Total net estimat: {totalNet.ToString("C", curr)}");
            Console.WriteLine();
        }
    }

    private bool TakeInput(string variable, out string input)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input))
            return false;
        return true;
    }

    private bool CheckProgramEnd(string input)
    {
        if (input != "-") return false;
        _running = false;
        return true;
    }
}