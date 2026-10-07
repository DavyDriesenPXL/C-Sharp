using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Week_1
{
    internal class Introductie
    {
        static public void Kleur()
        {
            Console.WriteLine("Wat is je naam?");
            string? naam = Console.ReadLine();
            Console.WriteLine("Wat is je favoriete kleur?");
            string? kleur = Console.ReadLine();
            Console.WriteLine($"Leuk om je te leren kennen, {naam}! {kleur} is een mooie kleur.");
        }

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
