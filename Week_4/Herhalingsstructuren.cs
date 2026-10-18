using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Week_4
{
    internal class Herhalingsstructuren
    {
        public static void SnelsteAtleet()
        {
            StringBuilder sb = new StringBuilder();

            string? naamAtleet;
            int seconden;
            string? snelsteAtleet = "";
            int besteTijd = int.MaxValue;

            Console.Write("Snelste atleet\n");

            do
            {
                Console.Write("\nNaam atleet: ");
                naamAtleet = Console.ReadLine();

                if (naamAtleet != "STOP")
                {
                    do
                    {
                        Console.Write("Looptijd: ");
                    }
                    while (!int.TryParse(Console.ReadLine(), out seconden));

                    if (seconden < besteTijd)
                    {
                        besteTijd = seconden;
                        snelsteAtleet = naamAtleet;
                    }
                }
            } while (naamAtleet != "STOP");

            int hours = (besteTijd / 60) / 60;
            int minutes = (besteTijd / 60) % 60;
            int seconds = besteTijd % 60;

            sb.AppendLine($"De snelste loper is {snelsteAtleet} met {besteTijd} seconden.");
            sb.AppendLine($"{hours} uren.");
            sb.AppendLine($"{minutes} minuten.");
            sb.AppendLine($"{seconds} seconden.");

            string result = sb.ToString();
            Console.WriteLine(result);
        }

        public static void Machtsverheffing()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Machtsverheffing");
            Console.ResetColor();

            int number;
            bool isCorrect;
            do
            {
                Console.Write("Geef een getal: ");
                isCorrect = int.TryParse(Console.ReadLine(), out number);

            } while (!isCorrect);

            for (int i = 1; i <= 10; i++)
            {
                double machtsverheffing = Math.Pow(number, i);

                if (i == 1)
                {
                    Console.WriteLine($"{number} tot de {i}ste macht = {machtsverheffing}");
                }
                else
                {
                    Console.WriteLine($"{number} tot de {i}de macht = {machtsverheffing}");
                }
            }
        }

        public static void Faculteit()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Faculteitsberekening");
            Console.ResetColor();

            bool isCorrect;
            int number;

            do
            {
                Console.Write("Geef een getal: ");
                isCorrect = int.TryParse(Console.ReadLine(), out number);

            } while (!isCorrect);

            int faculteit = 1;
            string berekening = "";

            for (int i = number; i > 0; i--)
            {
                faculteit *= i;

                berekening += i > 1 ? i + " * " : i.ToString();
            }
            Console.WriteLine($"{number} faculteit = {berekening} = {faculteit}");
        }

        public static void Lotto()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("LOTTO");
            Console.ResetColor();

            Random random = new Random();

            int number;
            string input;

            do
            {
                for (int i = 1; i < 7; i++)
                {
                    number = random.Next(1, 46);
                    Console.WriteLine($"\nGetal {i}: {number}");
                }
                do
                {
                    Console.Write("\nWilt u andere getallen (J/N)? ");
                    input = Console.ReadLine()!;

                } while (input != "J" && input != "N");

            } while (input != "N");
        }

        public static void Vermenigvuldigingstabel()
        {
            int number;

            do
            {
                Console.Write("Geef een getal in: ");
                int.TryParse(Console.ReadLine(), out number);

            } while (number > 10);

            Console.WriteLine();

            for (int i = 1; i < number + 1; i++)
            {
                Console.Write("\t");
                Console.Write($"{i}");
            }

            Console.WriteLine();

            for (int i = 1; i < number + 1; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i}\t");
                Console.ResetColor();

                for (int j = 1; j < number + 1; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{j * i}\t");
                    Console.ResetColor();
                }

                Console.WriteLine();

            }
        }

        public static void Sparen()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Sparen\n");
            Console.ResetColor();

            Console.Write("Zakgeld (per week): ");
            decimal.TryParse(Console.ReadLine(), out decimal zakgeld);
            Console.Write("Wekelijkse verhoging: ");
            decimal.TryParse(Console.ReadLine(), out decimal verhoging);
            Console.Write("Gewenst bedrag (doel): ");
            decimal.TryParse(Console.ReadLine(), out decimal doel);

            decimal total = zakgeld;
            int week = 0;

            Console.WriteLine($"\nWeek {week,5}\t Zakgeld € {zakgeld:F2}\t\t Totaal: € {total,5:F2}");

            while (total <= doel)
            {
                week++;
                zakgeld += verhoging;
                total += zakgeld;

                Console.WriteLine($"Week {week,5}\t Zakgeld € {zakgeld:F2}\t\t Totaal: € {total,5:F2}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nJe hebt {week} weken nodig om je doel van {doel} euro bij elkaar te sparen.");
            Console.ResetColor();
        }
    }
}
