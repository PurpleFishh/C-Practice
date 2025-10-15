namespace P8_Secv.Sequence.Services;

public class GeometricSequence : ISequenceGenerator
{
    public int Ratio { get; set; } = 2;

    public GeometricSequence()
    {
    }

    public GeometricSequence(int ratio)
    {
        Ratio = ratio;
    }

    public List<long> GetSequence(int limit)
    {
        if (limit <= 0)
            return new List<long>();

        var seq = Enumerable.Range(1, limit - 1)
            .ToList()
            .Aggregate(new List<long> { 1 }, (seq, _) =>
            {
                seq.Add(seq.Last() * Ratio);
                return seq;
            });

        return seq;
    }
}