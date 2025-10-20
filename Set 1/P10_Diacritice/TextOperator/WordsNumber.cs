namespace P10_Diacritice.TextOperator;

public class WordsNumber : ITextOperator
{
    public int Operate(string text) => text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
}