using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P4_Login
{
    public class User
    {
        public string Username { get; private set; }
        private readonly string password;
        private Stack<(DateTime date, bool success)> _connectionAttempts = new();
        public ImmutableList<(DateTime date, bool success)> GetConnectionHistory => _connectionAttempts.ToImmutableList();

        public User(string username, string password)
        {
            this.Username = username;
            this.password = password;
        }

        public bool Auth(string inputPassword)
        {
            var isAuth = inputPassword == password;
            _connectionAttempts.Push((DateTime.UtcNow, isAuth));
            return isAuth;
        }

    }
}
