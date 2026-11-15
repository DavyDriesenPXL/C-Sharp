namespace Week_7_Personeel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wil je een personeelslid aanmaken of een lege template?");
            Console.WriteLine("1. Personeelslid aanmaken\n2. Lege template");
            int.TryParse(Console.ReadLine(), out int input);

            switch(input)
            {
                case 1:
                    Employee employee = NewEmployee();
                    Console.WriteLine(employee.ShowSummary());
                    break;
                case 2:
                    Employee emptyEmployee = new();
                    Console.WriteLine(emptyEmployee.ShowSummary());
                    break;
            }

        }

        static Employee NewEmployee()
        {
            Console.Write("Naam: ");
            string lastName = Console.ReadLine();
            Console.Write("Voornaam: ");
            string firstName = Console.ReadLine();
            Console.Write("Sex (M/V/X): ");
            char.TryParse(Console.ReadLine(), out char sex);
            Console.Write("Score: ");
            int.TryParse(Console.ReadLine(), out int score);
            Console.Write("Indiensttreding (jaar): ");
            int.TryParse(Console.ReadLine(), out int startYear);

            return new Employee(lastName, firstName, sex, score, startYear);

        }
    }
}
