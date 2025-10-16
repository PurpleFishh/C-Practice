namespace P4_Login;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool success, T? value, string? error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Ok(T value) => new(true, value, null);

    public static Result<T> Fail(string error, T? value = default)
        => new(false, value, error);
}