using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_Login
{
    public class Auth
    {
        private bool _running = true;
        private Dictionary<string, User> users = new();

        public Auth()
        {
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

                var login = users.ContainsKey(username);
                if (!login)
                    Console.WriteLine("No user found, please register!");

                var attempts = 3;
                while (attempts > 0)
                {
                    if (!TakeInput("password", out string password))
                        continue;

                    if (!login)
                        users[username] = new User(username, password);

                    var user = users[username];
                    if (user.Auth(password))
                    {
                        Console.WriteLine("Auth with success!");
                        var lastLogin = user.GetConnectionHistory.Last();
                        Console.WriteLine($"Last login: {lastLogin.date}, succesful: {lastLogin.success}");
                        UserControl(user);
                    }
                    else
                    {
                        attempts--;
                        if (attempts == 0)
                            Console.WriteLine($"Out of attempts...");
                        else
                            Console.WriteLine($"Invalid password, please try again(attempts left {attempts})!");
                        continue;
                    }

                    attempts = 0;
                }
            }
        }

        private void UserControl(User user)
        {
            Console.WriteLine("Welcome(- to logout, H for login history)");
            while (true)
            {
                var input = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid username!");
                    continue;
                }

                if (input == "-")
                {
                    Console.WriteLine("Logged out!");
                    break;
                }

                if (input == "H")
                {
                    Console.WriteLine(String.Join('\n', user.GetConnectionHistory.Select(login => $"{login.date}: {login.success}")));
                    continue;
                }
            }
        }

        private bool TakeInput(string param, out string input)
        {
            Console.WriteLine($"Enter a {param}: ");
            var userInput = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine($"Please enter a valid {param}!");
                input = string.Empty;
                return false;
            }
            input = userInput;
            return true;
        }
    }
}
