namespace P8_Secv.Sequence;

public interface ISequenceGenerator
{
    List<long> GetSequence(int limit);
}