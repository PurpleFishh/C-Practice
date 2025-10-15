using System.Collections;

namespace P8_Secv;

public static class InfiniteSequence
{
    public static IEnumerable<long> Infinite(long start = 0)
    {
        while (true)
        {
            yield return start++;
        }
    }
}