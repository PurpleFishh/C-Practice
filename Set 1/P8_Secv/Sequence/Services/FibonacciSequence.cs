namespace P8_Secv.Sequence.Services;

public class FibonacciSequence : ISequenceGenerator
{
    public List<long> GetSequence(int limit)
    {
        if (limit <= 0)
            return new List<long>();

        var seq = Enumerable.Range(2, limit)
            .ToList()
            .Aggregate(new List<long> { 0, 1 }, (seq, _) =>
                {
                    seq.Add(seq.TakeLast(2).Sum());
                    return seq;
                }
            );

        return seq;
    }
}