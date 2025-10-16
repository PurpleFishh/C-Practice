namespace P4_Login.User;

public class InMemoryUserStore : IUserStore
{
    private readonly Dictionary<string, User> _users = new();

    public bool Exists(string username) => _users.ContainsKey(username);

    public User? Get(string username)
        => _users.GetValueOrDefault(username);

    public bool Add(User user)
        => _users.TryAdd(user.Username, user);
}