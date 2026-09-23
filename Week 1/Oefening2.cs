using System;
using System.Collections.Generic;
using System.Text;

namespace Week_1
{
    internal class Oefening2
    {
        static public void Leeftijd()
        {
            int date = DateTime.Today.Year;
            int geboorteJaar;

            Console.WriteLine($"Het huidige jaartal is {date}");
            Console.WriteLine("Wat is je geboortejaar?");
            do
            { 
                string? input = Console.ReadLine();

                if (int.TryParse(input, out geboorteJaar))
                {
                    int age = date - geboorteJaar;
                    Console.WriteLine($"Je leeftijd is momenteel {age}");
                    break;
                }
                else Console.WriteLine("Geef je geboortejaar in cijfers.");
            }
            while (true);
        }
    }
}
