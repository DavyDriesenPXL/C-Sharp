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
    }
}
