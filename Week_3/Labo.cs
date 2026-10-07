using System;
using System.Collections.Generic;
using System.Text;

namespace Week_3
{
    internal class Labo
    {
        public static void Pretpark()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welkom in PretLand");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Naam: ");
            string? name = Console.ReadLine();
            int age;
            int height;

            do
            {
                Console.Write("Leeftijd: ");
                string? inputAge = Console.ReadLine();

                if (int.TryParse(inputAge, out age))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Geef een geldige leeftijd in.");
                    Console.ResetColor();
                }
            } while (true);
            do
            {

                Console.Write("Lengte in cm: ");
                string? inputHeight = Console.ReadLine();

                if (int.TryParse(inputHeight, out height))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Geef een geldige lengte in.");
                    Console.ResetColor();
                }

            } while (true);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welke attractie wil je bezoeken?\n\t1. Meteor Rush\n\t2. Jungle Splash\n\t3. Haunted Lab\n\t4. Crazy House");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Maak je keuze: ");

            string? attraction = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            string attractionName;
            int minAge;
            int minHeight;
            int basePrice;

            switch (attraction)
            {
                case "1":
                    attractionName = "Meteor Rush";
                    minAge = 12;
                    minHeight = 143;
                    basePrice = 18;
                    break;
                case "2":
                    attractionName = "Jungle Splash";
                    minAge = 8;
                    minHeight = 120;
                    basePrice = 14;
                    break;
                case "3":
                    attractionName = "Haunted Lab";
                    minAge = 16;
                    minHeight = 150;
                    basePrice = 11;
                    break;
                case "4":
                    attractionName = "Crazy House";
                    minAge = 13;
                    minHeight = 140;
                    basePrice = 10;
                    break;

                default:
                    attractionName = "";
                    minAge = 0;
                    minHeight = 0;
                    basePrice = 0;
                    Console.WriteLine("Die attractie hebben we niet.");
                    break;
            }
            Console.WriteLine($"Minimum leeftijd: {minAge} jaar\nMinimum lengte: {minHeight} cm\nStandaard prijs: {basePrice} euro");

            bool hasAccess = age >= minAge && height >= minHeight ? true : false;

            if (hasAccess)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Welk ticket wil je aankopen?\n\t1. Standaard ticket\n\t2. Fastlane ticket");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Maak je keuze: ");

                string? choice = Console.ReadLine();

                if (choice == "1" && age < 12)
                {
                    basePrice -= 2;
                }
                else if (choice == "2")
                {
                    basePrice += 5;
                }

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Name: {name}\nAttraction: {attractionName}\nTicket Price: {basePrice}\nHave fun!!!");
            }
            else
            {
                if (age < minAge)
                    Console.WriteLine("\nJe bent helaas te jong voor deze attractie.");
                if (minHeight % height <= 5)
                    Console.WriteLine("Je bent minder dan 5 cm te klein voor deze attractie. Nog heel even groeien!");
                else if (height < minHeight)
                    Console.WriteLine("Je bent helaas te klein voor deze attractie.");
            }

            Console.ResetColor();
        }
    }
}
