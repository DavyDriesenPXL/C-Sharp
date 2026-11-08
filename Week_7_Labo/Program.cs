using Week_7_Labo.Models;

namespace Week_7_Labo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Employee employee = CreateEmployee();
            
            ShowDetails(employee);

            int percentage; 
            do
            {
                Console.Write("Opslag percentage: ");
            } while(!int.TryParse(Console.ReadLine(), out percentage));
            

            employee.IncreaseSalary(percentage);

            ShowDetails(employee);

        }

        public static Employee CreateEmployee()
        {
            Employee employee = new();

            Console.Write("Voornaam: ");
            employee.FirstName = Console.ReadLine();

            Console.Write("Achternaam: ");
            employee.LastName = Console.ReadLine();

            DateTime birthDate;
            do
            {
                Console.Write("Geboortedatum: ");
            } while (!DateTime.TryParse(Console.ReadLine(), out birthDate));
            employee.BirthDate = birthDate;

            decimal salary;
            do
            {
                Console.Write("Salaris: ");
            } while (!decimal.TryParse(Console.ReadLine(), out salary));
            employee.Salary = salary;

            return employee;
           
        }
        public static void ShowDetails(Employee employee)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Werknemer: {employee.FirstName} {employee.LastName}");
            Console.WriteLine($"Geboortedatum: {employee.BirthDate.ToLongDateString()} ({employee.Age})");
            Console.WriteLine($"Salaris: {employee.Salary:c}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Samenvatting: {employee}");
            Console.WriteLine("-----------------------------");
        }

    }
}
