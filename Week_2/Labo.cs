using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Week_2
{
    internal class Labo
    {
        public static void Minecraft()

        {
            int pickStone = 131;
            int pickIron = 250;
            int pickDiamond = 1561;
            int pickNetherite = 2031;

            Console.Write("Hoeveel blokken wil je mijnen met een stone pickaxe?: ");
            int.TryParse(Console.ReadLine(), out int input);

            Console.WriteLine($"\nStone: {pickStone - input} durability over");
            Console.WriteLine($"Iron: {pickIron - input} durability over");
            Console.WriteLine($"Diamond: {pickDiamond - input} durability over");
            Console.WriteLine($"Netherite: {pickNetherite - input} durability over");

            Console.Write("\nMaak je eigen tool - geef een naam: ");
            string? tool = Console.ReadLine();
            Console.Write("Geef de maximale durability (geheel getal): ");
            int.TryParse(Console.ReadLine(), out int durability);
            Console.WriteLine($"\nJe tool '{tool}' heeft nog {durability - input} over na {input} blokken");
        }
    }
}
