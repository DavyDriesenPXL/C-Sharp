using Week_7_User.Models;
using Week_7_User.Services;

namespace Week_7_User
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserManager user = new();
            Login credentials = new();
            
            Console.WriteLine("----- Register ------");
            Console.WriteLine("------------------------");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            user.Register(username, password);

            Console.WriteLine("Registration succesfull!");
            Console.WriteLine("Press enter to continue ...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("----- Login ------");
            Console.WriteLine("------------------------");
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();

            credentials.Username = username;
            credentials.Password = password;

            if (user.TryLogin(credentials))
            {
                Console.WriteLine("Login succesfull!");
            }
            else
            {
                Console.WriteLine("Login failed.");
            }

        }
    }
}
