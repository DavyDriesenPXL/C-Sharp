using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Week_3
{
    internal class Functies
    {
        public static void Ondernemingsnummer()
        {
            bool isValid = false;
            string ondernemingsnummer;
            do
            {
                Console.Write("Ondernemingsnummer: ");
                ondernemingsnummer = Console.ReadLine()?.Trim()!;

                if (ondernemingsnummer.Length != 10 || !ondernemingsnummer.All(char.IsDigit))
                {
                    Console.WriteLine("Ondernemingsnummer moet uit exact 10 cijfers bestaan.");
                }
                else
                {
                    isValid = true;
                }
            } while (!isValid);

            string firstEightDigits = ondernemingsnummer.Substring(0, 8);
            string lastTwoDigits = ondernemingsnummer.Substring(8, 2);

            int rest = int.Parse(firstEightDigits) % 97;
            int controleNumber = 97 - rest;

            if (controleNumber == 0)
            {
                controleNumber += 97;
            }
            if (controleNumber == int.Parse(lastTwoDigits))
            {
                Console.WriteLine("Het ondernemingsnummer is juist.");
            }
            else
            {
                Console.WriteLine("Het ondernemingsnummer is fout.");
            }

        }

        public static void Eindsaldo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Beginsaldo: ");
            decimal.TryParse(Console.ReadLine(), out decimal initialInput);
            Console.Write("Aantal jaren: ");
            decimal.TryParse(Console.ReadLine(), out decimal timeSpan);
            Console.Write("Rente per jaar (%): ");
            decimal.TryParse(Console.ReadLine(), out decimal interest);

            double result = (double)initialInput * Math.Pow((double)(1 + interest / 100), (double)timeSpan);
            result = Math.Round(result, 2);

            Console.WriteLine($"Je eindsaldo na {timeSpan} jaar: {result:C}");
        }

        public static void Driehoek()
        {
            Console.Write("Lengte van zijde a: ");
            int.TryParse(Console.ReadLine(), out int zijdeA);
            Console.Write("Lengte van zijde b: ");
            int.TryParse(Console.ReadLine(), out int zijdeB);
            Console.Write("Lengte van zijde c: ");
            int.TryParse(Console.ReadLine(), out int zijdeC);

            if ((zijdeA + zijdeB) > zijdeC && (zijdeB + zijdeC) > zijdeA && (zijdeC + zijdeA) > zijdeB)
            {
                double halveOmtrek = (double)(zijdeA + zijdeB + zijdeC) / 2;

                double oppervlakte = Math.Sqrt(halveOmtrek * (halveOmtrek - zijdeA) * (halveOmtrek - zijdeB) * (halveOmtrek - zijdeC));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"De oppervlakte van de driehoek is: {oppervlakte:F2} m3");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("De driehoek is ongeldig.");
                Console.ResetColor();
            }
        }

        public static void Landmeter()
        {
            Console.Write("Afstand AC (in meter): ");
            int.TryParse(Console.ReadLine(), out int meterAC);

            if (meterAC > 0)
            {
                Console.Write("Hoek A (in graden): ");
                int.TryParse(Console.ReadLine(), out int gradenA);
                Console.Write("Hoek C (in graden): ");
                int.TryParse(Console.ReadLine(), out int gradenC);

                if (gradenA > 0 && gradenA < 180 && gradenC > 0 && gradenC < 180)
                {

                    double radiaalA = gradenA * Math.PI / 180;
                    double radiaalC = gradenC * Math.PI / 180;

                    double meterAB = meterAC * Math.Sin(radiaalC) / Math.Sin(radiaalA + radiaalC);

                    Console.WriteLine($"Afstand AB is {meterAB} meter.");
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: De hoeken van een driehoek moeten groter zijn dan 0° en kleiner dan 180° zijn.");
                    Console.ResetColor();
                    Console.WriteLine("Druk op ENTER om af te sluiten ...");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: De afstand AC moet groter zijn dan 0m");
                Console.ResetColor();
                Console.WriteLine("Druk op ENTER om af te sluiten ...");
            }
        }
    }
}
