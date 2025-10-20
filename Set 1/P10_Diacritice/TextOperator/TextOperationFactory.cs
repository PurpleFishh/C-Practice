using System.Diagnostics;

namespace P10_Diacritice.TextOperator;

public static class TextOperationFactory
{
    public static ITextOperator Operator(TextOperations operation)
    {
        return operation switch
        {
            TextOperations.VowelsNumber => new VowelsNumber(),
            TextOperations.ConsonantsNumber => new ConsonantNumber(),
            TextOperations.WordsNumber => new WordsNumber(),
            TextOperations.CharactersNumber => new CharactersNumber(),
            _ => throw new InvalidOperationException("Unsupported operation")
        };
    }
}