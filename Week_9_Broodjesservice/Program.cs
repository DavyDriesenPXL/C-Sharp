namespace Week_9_Broodjesservice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Broodje> broodjes = [];
            string name;
            decimal price;
            string type = string.Empty;

            Console.WriteLine("--- BROODJESBESTELLING ---");
            Console.WriteLine("Geldige types: warm, koud, veggie of speciaal.");

            do
            {
                Console.WriteLine("\nGeef de naam van het broodje (of 'stop om te eindigen')");
                name = Console.ReadLine();
                if (!name.ToUpper().Equals("STOP"))
                {
                    Console.WriteLine("Geef het type:");
                    type = Console.ReadLine();

                    do
                    {
                        Console.WriteLine("Geef de prijs:");
                    } while (!decimal.TryParse(Console.ReadLine(), out price));
                    try
                    {
                        broodjes.Add(new Broodje(name, type, price));

                    }

                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } while (!name.ToUpper().Equals("STOP"));
        }
    }
}
