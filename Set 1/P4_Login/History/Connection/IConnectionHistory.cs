namespace P4_Login.History.Connection;

public interface IConnectionHistory : IHistory<(DateTime date, bool success)>
{
}