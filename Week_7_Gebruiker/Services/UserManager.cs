using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Week_7_Gebruiker.Models;

namespace Week_7_Gebruiker.Services
{
    internal class UserManager
    {
        Login login = new();

        public void Register(string username, string password)
        {
            login.Username = username;
            login.Password = password;
        }

        public bool TryLogin(Login credentials)
        {
            return login.Username.Equals(credentials.Username) && HashPassword(credentials.Password).Equals(login.Password);
        }

        public string HashPassword(string password)
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
