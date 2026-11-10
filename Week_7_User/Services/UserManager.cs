using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Week_7_User.Models;

namespace Week_7_User.Services
{
    internal class UserManager
    {
        Login login = new();

        public void Register(string username, string password)
        {
            login.Username = username;
            login.Password = HashPassword(password);
        }

        public bool TryLogin(Login credentials)
        {
            return credentials.Username.Equals(login.Username) && HashPassword(credentials.Password).Equals((login.Password));
        }

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
