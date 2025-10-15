namespace P8_Secv
{
    public class PerfectSquareSequence : ISequenceGenerator
    {
        public List<long> GetSequence(int limit)
        {
            if (limit <= 0)
                return new List<long>();

            var seq = Enumerable.Range(1, limit)
                .Select(i => (long)i * i)
                .ToList();

            return seq;
        }
    }
}