using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Channels;

namespace Week_3
{
    internal class Controlestructuren
    {

        public static void GrootsteGetal()
        {
            Console.Write("Geef een geheel getal in: ");
            int.TryParse(Console.ReadLine(), out int number1);
            Console.Write("Geef nog een geheel getal in: ");
            int.TryParse(Console.ReadLine(), out int number2);

            Console.WriteLine(number1 > number2 ? "Het grootste getal is " + number1 : "Het grootste getal is " + number2);
            //Console.WriteLine($"Het grootste getal is {(number1 > number2 ? number1 : number2)}");
        }

        public static void Rekenmachine()
        {
            Console.Write("Geef het eerste getal: ");
            int.TryParse(Console.ReadLine(), out int number1);
            Console.Write("Geef het tweede getal: ");
            int.TryParse(Console.ReadLine(), out int number2);

            Console.WriteLine("Geef de gewenste bewerking in (*, / of + of -)");
            string? bewerking = Console.ReadLine();

            int result = 0;
            bool isCorrect = true;
            switch (bewerking)
            {
                case "/":
                    result = number1 / number2;
                    break;
                case "*":
                    result = number1 * number2;
                    break;
                case "+":
                    result = number1 + number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                default:
                    isCorrect = false;
                    break;
            }

            if (isCorrect)
                Console.WriteLine($"Resultaat: {result}");
            else
                Console.WriteLine("De bewerking was niet geldig");

        }

        public static void Meerderjarig()
        {
            Console.Write("Geef je geboortejaar in: ");

            string? input = Console.ReadLine();
            int geboortejaar;

            if (int.TryParse(input, out geboortejaar) && geboortejaar >= 1900 && geboortejaar <= 2026)
            {
                int leeftijd = 2026 - geboortejaar;

                Console.WriteLine($"Je bent {leeftijd} jaar oud, dus {(leeftijd >= 18 ? "meerderjarig." : "minderjarig.")}");
            }
        }

        public static void Verzekering()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Schrijf een toepassing die het te betalen bedrag berekend voor een hospitalisatieverzekering.\n");
            Console.ResetColor();

            Console.Write("Geef je leeftijd in: ");
            int.TryParse(Console.ReadLine(), out int age);

            Console.Write("In welk gewest woon je (Vlaanderen, Brussel of Wallonië): ");
            string? region = Console.ReadLine();

            Console.Write("Rook je? Y/N: ");
            string? smoker = Console.ReadLine();
            int fee = 0;

            if (age >= 18 && age < 67)
            {
                fee += 150;
            }
            else if (age >= 67)
            {
                fee += 300;
            }

            if (region == "Brussel" && age >= 18)
            {
                fee += 200;
            }

            if (smoker == "Y")
            {
                fee *= 2;
            }

            Console.WriteLine($"De totaalprijs van uw hospitalisatieverzekering is: {fee} euro.");
        }
        public static void Leveringskosten()
        {
            Console.Write("Geef de prijs in van het product: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);
            Console.Write("Geef het aantal producten in: ");
            int.TryParse(Console.ReadLine(), out int amount);
            Console.WriteLine("BTW-percentage (6, 12 of 21): ");
            int.TryParse(Console.ReadLine(), out int btw);

            decimal nettoPrice = price * amount;
            decimal tax = 0;
            decimal discountedNettoPrice = nettoPrice;
            decimal totalPrice = 0;

            if (amount >= 10)
            {
                discountedNettoPrice *= 0.95m;
            }

            if (btw == 21)
            {
                tax = discountedNettoPrice * 0.21m;
            }
            else if (btw == 12)
            {
                tax = discountedNettoPrice * 0.12m;
            }
            else if (btw == 6)
            {
                tax = discountedNettoPrice * 0.06m;
            }
            totalPrice = discountedNettoPrice + tax;
            if (totalPrice < 50)
                totalPrice += 15;
            else if (totalPrice >= 70)
                totalPrice += 10;
            else
                totalPrice += 12;

            Console.WriteLine($"De totaalprijs is: {totalPrice:F2} euro inclusief BTW.\nIn totaal betaalde u {tax:F2} euro aan BTW.");

        }

        public static void Weddeberekening()
        {
            Console.Write("Geef je naam in: ");
            string? naam = Console.ReadLine();

            decimal hourlyWage = 0;
            int workedHours = 0;

            do
            {
                Console.Write("Geef je uurloon in: ");
            } while (!decimal.TryParse(Console.ReadLine(), out hourlyWage));

            do
            {
                Console.Write("Geef het aantal gewerkte uren in: ");
            } while (!int.TryParse(Console.ReadLine(), out workedHours));

            decimal brutoWage = hourlyWage * workedHours;
            decimal taxedWage = brutoWage;
            decimal tax = 0;

            if (taxedWage > 50000)
            {
                taxedWage -= 50000;
                tax += taxedWage * 0.50m;
                taxedWage = 50000;
            }
            if (taxedWage > 25000)
            {
                taxedWage -= 25000;
                tax += taxedWage * 0.40m;
                taxedWage = 25000;
            }
            if (taxedWage > 15000)
            {
                taxedWage -= 15000;
                tax += taxedWage * 0.30m;
                taxedWage = 15000;
            }
            if (taxedWage > 10000)
            {
                taxedWage -= 10000;
                tax += taxedWage * 0.20m;
            }

            Console.WriteLine($"\nLoonfiche van: {naam}\n\n" +
                $"Aantal gewerkte uren:\t{workedHours}\n" +
                $"Uurloon:\t\t{hourlyWage}\n" +
                $"Bruto Jaarwedde:\t{brutoWage}\n" +
                $"Belasting:\t\t{tax:F2}\n" +
                $"Netto Jaarwedde:\t{(brutoWage - tax):F2}");
        }

        public static void KmToMiles()
        {
            Console.WriteLine("In welke eenheid wilt u de afstand ingeven?");
            Console.WriteLine("\t1. Kilometer\n\t2. Mijl");

            Console.Write("Uw keuze: ");
            string? input = Console.ReadLine();

            const double Ratio = 1.60934;
            double outcome = 0;

            if (input != null)
            {
                input.ToLower();
            }
            switch (input)
            {
                case "1":
                    Console.Write("Afstand in km: ");

                    if (double.TryParse(Console.ReadLine(), out outcome))
                    {
                        Console.Write($"Afstand in mijl: {(outcome / Ratio)}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dit is geen geldige invoer.");
                        Console.ResetColor();
                    }
                    break;
                case "2":
                    Console.Write("Afstand in mijl: ");
                    if (double.TryParse(Console.ReadLine(), out outcome))
                    {
                        Console.Write($"Afstand in km: {(outcome * Ratio)}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dit is geen geldige invoer.");
                        Console.ResetColor();
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Deze eenheid wordt niet ondersteund");
                    Console.ResetColor();
                    break;
            }
        }

        public static void Diploma()
        {

        }
    }
}
