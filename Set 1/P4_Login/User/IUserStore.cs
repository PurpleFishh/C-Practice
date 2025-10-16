namespace P4_Login.User;

public interface IUserStore
{
    bool Exists(string username);
    User? Get(string username);
    bool Add(User user);
}