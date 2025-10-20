using System.Text;

namespace P14_Banca.BankLibrary.Generators;

public class NumericStringGenerator : IGenerator
{
    public virtual string Generate(int length)
    {
        var random = new Random();

        return Enumerable
            .Range(0, length)
            .ToList()
            .Select(_ => random.Next())
            .Aggregate("", (x, y) => x + y);
    }
}