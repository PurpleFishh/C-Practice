using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using P4_Login.History;
using P4_Login.History.Connection;

namespace P4_Login.User;

public class User
{
    public string Username { get; private set; }
    private readonly string _password;
    public readonly IConnectionHistory ConnectionAttempts = new ConnectionHistory();

    public User(string username, string password)
    {
        this.Username = username;
        this._password = password;
    }

    public bool Auth(string inputPassword)
    {
        var isAuth = inputPassword == _password;
        ConnectionAttempts.Add((DateTime.UtcNow, isAuth));
        return isAuth;
    }

    public void UserControl()
    {
        Console.WriteLine("Welcome(- to logout, H for login history)");
        while (true)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
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
                Console.WriteLine(ConnectionAttempts);
        }
    }
}