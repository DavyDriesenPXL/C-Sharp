using System;
using System.Collections.Generic;
using System.Security;
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

        public static void GalacticExpress()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            string name;
            string destination;
            DateTime departureDate;
            decimal basePrice = 89.95m;
            decimal bagagePrice = 1.75m;
            decimal totalCost = 0;
            int gate = random.Next(1, 12);
            int row = random.Next(1, 30);
            int seat = random.Next(1, 6);
            int controleNumber = random.Next(1000, 9999);

            do
            {
                Console.Write("Geef je naam in: ");
                name = Console.ReadLine()?.Trim()!;

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Het veld naam mag niet leeg zijn.");
                }

            } while (string.IsNullOrWhiteSpace(name));

            do
            {
                Console.Write("Geef je bestemming: ");
                destination = Console.ReadLine()?.Trim()!;

                if (destination.Length < 3)
                {
                    Console.WriteLine("De bestemming moet minstens 3 letters bevatten.");
                }
            } while (destination.Length < 3);

            do
            {
                Console.Write("Geef de vertrekdatum (yyyy-MM-dd): ");
                departureDate = DateTime.Parse(Console.ReadLine()!);

                if(departureDate < DateTime.Today)
                {
                    Console.WriteLine("De vertrekdatum kan niet in het verleden liggen.");
                }

            } while (departureDate < DateTime.Today);

            Console.Write("Geef het gewicht van je bagage in in kg: ");
            int.TryParse(Console.ReadLine(), out int bagageWeight);

            TimeSpan calculateDates = departureDate - DateTime.Today;
            int daysTillDeparture = calculateDates.Days;

            if (daysTillDeparture < 7)
            {
                basePrice += 12.50m;
            }

            totalCost = Math.Round(((bagagePrice * bagageWeight) + basePrice), 2, MidpointRounding.AwayFromZero);

            sb.AppendLine("\n==================================");
            sb.AppendLine("\tGALACTIC EXPRESS");
            sb.AppendLine("\tBOARDING PASS");
            sb.AppendLine("==================================");
            sb.AppendLine($"{"Reiziger:",-18} {name}");
            sb.AppendLine($"{"Bestemming:", -18} {destination}");
            if(destination.ToLower().Contains("station"))
            {
                sb.AppendLine($"{"",-18} Special destination to a station!");
            }
            sb.AppendLine($"{"Code:", -18} {destination.Substring(0,3).ToUpper()}");
            sb.AppendLine($"{"Vertrekdatum:",-18} {departureDate}");
            sb.AppendLine($"{"Vertrekdag:",-18} {departureDate.DayOfWeek}");
            sb.AppendLine($"{"Dagen tot vertrek:",-18} {daysTillDeparture}\n");
            sb.AppendLine($"{"Gate:",-18} {gate}");
            sb.AppendLine($"{"Stoel:",-18} Rij {row} - Stoel {seat}");
            sb.AppendLine($"{"Controlecode:",-18} {controleNumber}\n");
            sb.AppendLine($"{"Bagage:",-18} {bagageWeight:F2} kg");
            sb.AppendLine($"{"Totale prijs:",-18} {totalCost:C}");
            sb.AppendLine($"{"Boekingscode:",-18} {name.Replace(" ", "-")}\n");
            sb.AppendLine($"{"Retourdatum:",-18} {departureDate.AddDays(7)}");

            string result = sb.ToString();
            Console.WriteLine(result);

        }
    }
}
