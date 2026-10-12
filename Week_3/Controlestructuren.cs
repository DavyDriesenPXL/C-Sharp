using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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
            int olodWeb = 6;
            int olodData = 4;
            int olodC = 7;
            int olodComm = 3;
            int olodWp = 6;
            int olodItOrg = 4;

            Console.WriteLine("=============== PXL Digital ===============  ");

            Console.Write("Naam student: ");
            string? name = Console.ReadLine();
            Console.WriteLine("===========================================");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Score Web Essentials: ");
            int.TryParse(Console.ReadLine(), out int scoreWeb);
            if(scoreWeb < 0 || scoreWeb > 20)            
            {
                scoreWeb = 0;
                olodWeb = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.Write("Score Data Essentials: ");
            int.TryParse(Console.ReadLine(), out int scoreData);
            if (scoreData < 0 || scoreData> 20)
            {
                scoreData = 0;
                olodData = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.Write("Score C# Essentials: ");
            int.TryParse(Console.ReadLine(), out int scoreC);
            if (scoreC < 0 || scoreC > 20)
            {
                scoreC = 0;
                olodC = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.Write("Score Communication Skills: ");
            int.TryParse(Console.ReadLine(), out int scoreComm);
            if (scoreComm < 0 || scoreComm > 20)
            {
                scoreComm = 0;
                olodComm = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.Write("Score Werkplekleren 1: ");
            int.TryParse(Console.ReadLine(), out int scoreWp);
            if (scoreWp < 0 || scoreWp > 20)
            {
                scoreWp = 0;
                olodWp = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.Write("Score IT-Organisation: ");
            int.TryParse(Console.ReadLine(), out int scoreItOrg);
            if (scoreItOrg < 0 || scoreItOrg > 20)
            {
                scoreItOrg = 0;
                olodItOrg = 0;
                Console.WriteLine("Ongeldige invoer.");
            }
            Console.ResetColor();

            int studyPoints = olodWeb + olodData + olodC + olodComm + olodWp + olodItOrg;

            double average = (double)(scoreWeb * olodWeb + scoreData * olodData + scoreC * olodC + scoreComm * olodComm + scoreWp * olodWp + scoreItOrg * olodItOrg) / studyPoints;

            double tekortPoints = 0;

            if (scoreWeb < 10)
            {
                tekortPoints = (10 - scoreWeb) * olodWeb;
            }
            if (scoreData < 10)
            {
                tekortPoints += (10 - scoreData) * olodData;
            }
            if (scoreC < 10)
            {
                tekortPoints += (10 - scoreC) * olodC;
            }
            if (scoreComm < 10)
            {
                tekortPoints += (10 - scoreComm) * olodComm;
            }
            if (scoreWp < 10)
            {
                tekortPoints += (10 - scoreWp) * olodWp;
            }
            if (scoreItOrg < 10)
            {
                tekortPoints += (10 - scoreItOrg) * olodItOrg;
            }

            double averageProcent = average * 5;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{name} behaalde een gewogen gemiddelde van {average:F2} met {tekortPoints:F0} tekortpunten!");
            Console.ResetColor();

            if (averageProcent >= 85 && tekortPoints == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Geslaagd met de grootste onderscheiding!");
            }
            else if (averageProcent >= 77 && tekortPoints == 0)

                Console.WriteLine("Geslaagd met grote onderscheiding.");
            else if (averageProcent >= 68 && tekortPoints == 0)
                Console.WriteLine("Geslaagd met onderscheiding.");
            else if (averageProcent >= 50 && tekortPoints == 0)
            {
                Console.WriteLine("Geslaagd met voldoening.");
                Console.ResetColor();
            }

            if (tekortPoints > 0 && tekortPoints <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Gedelibereerd.");
            }
            else if (tekortPoints > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Niet geslaagd.");
                Console.ResetColor();
            }
        }
    }
}
