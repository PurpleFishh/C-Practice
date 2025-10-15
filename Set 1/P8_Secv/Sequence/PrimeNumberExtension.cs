namespace P8_Secv.Sequence;

public static class PrimeNumberExtension
{
    public static bool IsPrime(this long number)
    {
        if (number < 2) return false;
        if (number == 2 || number == 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;

        for (long i = 5; i * i <= number; i += 6)
            if (number % i == 0 || number % (i + 2) == 0)
                return false;

        return true;
    }

    public static bool IsPrime(this int number) => ((long)number).IsPrime();
}