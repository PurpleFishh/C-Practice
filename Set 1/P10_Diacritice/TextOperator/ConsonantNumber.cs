namespace P10_Diacritice.TextOperator;

public class ConsonantNumber : ITextOperator
{
    private readonly char[] _vowels = ['a', 'e', 'i', 'o', 'u', 'ă', 'â', 'î', 'ș', 'ț'];

    public int Operate(string text) => text.ToLower().ToArray().Count(c => !_vowels.Contains(c));
}