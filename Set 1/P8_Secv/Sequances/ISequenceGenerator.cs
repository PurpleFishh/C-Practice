namespace P8_Secv
{

    public interface ISequenceGenerator
    {
        List<long> GetSequence(int limit);
    }
}