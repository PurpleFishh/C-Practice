using P8_Secv.Sequence;
using P8_Secv.Sequence.Services;

namespace P8_Secv;

public enum SeqType
{
    Fibonacci,
    Geometric,
    PrimeNums,
    Factorial,
    Tri,
    PerfectSq
}

public static class SequenceFactory
{
    public static ISequenceGenerator GetSeqGenerator(SeqType type)
    {
        return type switch
        {
            SeqType.Fibonacci => new FibonacciSequence(),
            SeqType.Factorial => new FactorialSequence(),
            SeqType.Geometric => new GeometricSequence(),
            SeqType.PerfectSq => new PerfectSquareSequence(),
            SeqType.PrimeNums => new PrimeNumbersSequence(),
            _ => new TriangularSequence()
        };
    }
}