namespace P7_Ghici;

public class NumberGenerator : INumberGenerator
{
    public int GenerateNumber(int? leftRange, int? rightRange)
    {
        var random = new Random();
        if (!leftRange.HasValue && !rightRange.HasValue)
            return random.Next();
        if (!leftRange.HasValue && rightRange.HasValue)
            return random.Next(rightRange.Value);
        if (leftRange.HasValue && !rightRange.HasValue)
            return random.Next() + leftRange.Value;
        return random.Next(leftRange!.Value, rightRange!.Value);
    }
}