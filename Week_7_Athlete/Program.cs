namespace Week_7_Athlete
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Athlete athlete = new();
            string name;
            
            do
            {
                Console.Write("Naam van atleet: ");
                name = Console.ReadLine();

                int time = int.MaxValue;
                if (!name.Equals("STOP"))
                {
                    do
                    {
                        Console.Write("Tijd van atleet (in seconden): ");
                    } while(!int.TryParse(Console.ReadLine(), out time));
                }

                if (time < athlete.Time)
                {
                    athlete.Name = name;
                    athlete.Time = time;
                }

            } while (!name.Equals("STOP"));

            if (!string.IsNullOrEmpty(athlete.Name))
            {
                Console.WriteLine(athlete.ToString());
            }
        }

    }
}
