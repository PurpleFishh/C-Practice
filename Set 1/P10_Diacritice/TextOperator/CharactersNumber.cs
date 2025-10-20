namespace P10_Diacritice.TextOperator;

public class CharactersNumber: ITextOperator
{
    public int Operate(string text) => text.Length;
}