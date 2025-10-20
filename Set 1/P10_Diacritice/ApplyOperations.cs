using System.Collections.Immutable;
using P10_Diacritice.TextOperator;

namespace P10_Diacritice;

public static class Operations
{
    public static IEnumerable<(TextOperations Operation, int Result)> ApplyOperations(
        ImmutableList<TextOperations> operations, string input) => ApplyOperations(operations.ToList(), input);

    public static IEnumerable<(TextOperations Operation, int Result)> ApplyOperations(
        List<TextOperations> operations, string input)
    {
        return operations
            .Select(op => (Operation: op, Operator: TextOperationFactory.Operator(op)))
            .Select(res => (res.Operation, Result: res.Operator.Operate(input)));
    }
}