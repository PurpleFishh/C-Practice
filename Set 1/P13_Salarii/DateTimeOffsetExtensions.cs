using System.Runtime.Serialization;

namespace P13_Salarii;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset Max(this (DateTimeOffset a, DateTimeOffset b) pair)
        => pair.a >= pair.b ? pair.a : pair.b;

    public static DateTimeOffset Min(this (DateTimeOffset a, DateTimeOffset b) pair)
        => pair.a <= pair.b ? pair.a : pair.b;
}