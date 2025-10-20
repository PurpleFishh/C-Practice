namespace P14_Banca.BankLibrary.Generators;

public static class BankCodeGenerator
{
    private static readonly NumericStringGenerator Generator = new();

    public static string Generate() => Generator.Generate(6);
}