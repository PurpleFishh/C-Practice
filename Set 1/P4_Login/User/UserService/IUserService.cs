namespace P4_Login.User.UserService;

public interface IUserService
{
    bool UserExists(string username);

    User RegisterIfMissing(string username, string password);

    Result<User> Authenticate(
        string username,
        string password,
        bool allowAutoRegister = false
    );

    User? GetUser(string username);
}