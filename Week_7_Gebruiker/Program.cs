using Week_7_Gebruiker.Models;
using Week_7_Gebruiker.Services;

namespace Week_7_Gebruiker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserManager user = new();
            Login login = new();

            Console.WriteLine("----- Register -----\n-------------------");
            Console.Write("username: ");
            string username = Console.ReadLine();
            Console.Write("password: ");
            string password = Console.ReadLine();
            user.Register(username, user.HashPassword(password));

            Console.Clear();
            Console.WriteLine("----- Login -----\n-------------------");
            Console.Write("username: ");
            username = Console.ReadLine();
            Console.Write("password: ");
            password = Console.ReadLine();

            login.Username = username;
            login.Password = password;

            user.TryLogin(login);

            if (user.TryLogin(login))
            {
                Console.WriteLine("Login succesvol!");
            }
            else
            {
                Console.WriteLine("Login failed");
            }

        }
    }
}
