using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4_Login.User;
using P4_Login.User.UserService;

namespace P4_Login;

public class Auth
{
    private bool _running = true;
    private readonly IUserService _userService;

    public Auth()
    {
        _userService = new UserService(new InMemoryUserStore());
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Auth is running(- to stop it)");
        while (_running)
        {
            if (!TakeInput("username", out string username))
                continue;

            if (username == "-")
            {
                _running = false;
                continue;
            }

            if (!_userService.UserExists(username))
                Console.WriteLine("No user found, please register!");

            UserLogin(username);
        }
    }

    private void UserLogin(string username)
    {
        var attempts = 3;
        while (attempts > 0)
        {
            if (!TakeInput("password", out var password))
                continue;

            var userResult = _userService.Authenticate(username, password, true);

            if (!userResult.Success)
            {
                attempts--;
                Console.WriteLine(attempts == 0
                    ? "Out of attempts..."
                    : $"{userResult.Error} (attempts left {attempts})!");
                continue;
            }

            var user = userResult.Value!;
            Console.WriteLine("Auth with success!");
            var lastLogin = user.ConnectionAttempts.GetHistory().Last();
            Console.WriteLine($"Last login: {lastLogin.date}, successful: {lastLogin.success}");
            user.UserControl();
            break;
        }
    }

    private bool TakeInput(string param, out string input)
    {
        Console.WriteLine($"Enter a {param}: ");
        var userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine($"Please enter a valid {param}!");
            input = string.Empty;
            return false;
        }

        input = userInput;
        return true;
    }
}