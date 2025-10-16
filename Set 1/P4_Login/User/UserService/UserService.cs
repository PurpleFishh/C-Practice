namespace P4_Login.User.UserService;

public class UserService(IUserStore store) : IUserService
{
    public bool UserExists(string username) => store.Exists(username);

    public User RegisterIfMissing(string username, string password)
    {
        var existing = store.Get(username);
        if (existing is not null) return existing;

        var user = new User(username, password);
        store.Add(user);
        return user;
    }

    public Result<User> Authenticate(
        string username,
        string password,
        bool allowAutoRegister = false
    )
    {
        var user = store.Get(username);

        if (user is null)
        {
            if (!allowAutoRegister)
                return Result<User>.Fail("User not found");

            user = RegisterIfMissing(username, password);
        }

        var ok = user.Auth(password);

        return ok ? Result<User>.Ok(user) : Result<User>.Fail("Invalid password", user);
    }

    public User? GetUser(string username) => store.Get(username);
}