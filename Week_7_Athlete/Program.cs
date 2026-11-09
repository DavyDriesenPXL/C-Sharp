namespace Week_7_Athlete
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Athlete athlete = new();
            string fullName;
            

            do
            {
                Console.Write("Naam van atleet: ");
                fullName = Console.ReadLine();

                int time = int.MaxValue;
                if (!fullName.Equals("STOP"))
                {
                    do
                    {
                        Console.Write("Tijd van atleet (in seconden): ");
                    } while(!int.TryParse(Console.ReadLine(), out time));
                }

                if (time < athlete.Time)
                {
                    athlete.Name = fullName;
                    athlete.Time = time;
                }

            } while (!fullName.Equals("STOP"));

            if (!string.IsNullOrEmpty(athlete.Name))
            {
                Console.WriteLine(athlete.ToString());
            }
        }

    }
}
