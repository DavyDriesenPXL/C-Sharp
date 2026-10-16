using System;
using System.Collections.Generic;
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

            } while(!isCorrect);

            for(int i = 1; i <= 10; i++)
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
    }
}
