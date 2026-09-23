using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Week_1
{
    internal class Oefening1
    {
        static public void Kleur()
        {
            Console.WriteLine("Wat is je naam?");
            string? naam = Console.ReadLine();
            Console.WriteLine("Wat is je favoriete kleur?");
            string? kleur = Console.ReadLine();
            Console.WriteLine($"Leuk om je te leren kennen, {naam}! {kleur} is een mooie kleur.");
        }
    }
}
