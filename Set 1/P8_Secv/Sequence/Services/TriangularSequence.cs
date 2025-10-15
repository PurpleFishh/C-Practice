namespace P8_Secv.Sequence.Services;

public class TriangularSequence : ISequenceGenerator
{
    public List<long> GetSequence(int limit)
    {
        if (limit <= 0)
            return new List<long>();

        var seq = Enumerable.Range(1, limit)
            .ToList()
            .Aggregate(new List<long> { 1 }, (seq, index) =>
                {
                    seq.Add(seq.Last() + index);
                    return seq;
                }
            );

        return seq;
    }
}