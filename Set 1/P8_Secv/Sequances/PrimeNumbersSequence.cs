using System.Collections;
using System.Numerics;

namespace P8_Secv
{
    public class PrimeNumbersSequence : ISequenceGenerator
    {
        public List<long> GetSequence(int limit)
        {
            if (limit <= 0)
                return new List<long>();
            var seq = InfiniteSequence.Infinite().Where(x => x.IsPrime()).Take(limit).ToList();
            return seq;
        }
    }
}