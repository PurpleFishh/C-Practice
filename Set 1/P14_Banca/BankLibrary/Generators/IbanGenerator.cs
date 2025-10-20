namespace P14_Banca.BankLibrary;

public class IbanGenerator
{
    private static long _lastAccount = 0;

    public static string GenerateIban(string countryCode, string bankCode)
    {
        _lastAccount += 1;
        var accountNumber = $"{_lastAccount}".PadLeft(14, '0');
        var bban = bankCode + accountNumber;
        // Hardcoded for now
        var checkDigits = "00";
        return $"{countryCode}{checkDigits}{bban}";
    }
}