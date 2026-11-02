using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Week_Herfstvakantie
{
    internal class DeepDive
    {
        public static void Run()
        {
            Console.WriteLine(BuildGauge(1,3,20));
        }

        static int ReadIntInRange(string prompt, int min, int max)
        {
            min = 10;
            max = 1000;
            int output;
            bool isCorrect;

            do
            {
                Console.Write("Doeldiepte in meter (10-1000: ");
                isCorrect = int.TryParse(Console.ReadLine(), out output) && output >= min && output <= max;

                if (!isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ongeldige ingave. Geef een getal tussen 10 en 10000.");
                    Console.ResetColor();
                }

            } while (!isCorrect);

            return output;
        }

        static string BuildGauge(int current, int max, int width = 10)
        {
            string gauge = "";
            double percentage;
            double filledCount;

            if (current > max)
            {
                current = max;
            }

            percentage = Math.Round((double)current / max * 100, 0, MidpointRounding.AwayFromZero);
            filledCount = Math.Round((double)current / max * width, 0, MidpointRounding.AwayFromZero);

            for (int i = 0; i < width; i++)
            {
                if (i < (int)filledCount)
                {
                    gauge += "#";
                }
                else
                {
                    gauge += ".";
                }
            }

            return "[" + gauge + "]" + " " + (int)percentage + "%";
        }

        static int CalculateOxygenUse(int depth, int durationHours)
        {
            return depth;
        }

        static int CalculateOxygenUse(int depth, int durationHours, bool useBoosters, int waterTemp)
        {
            return depth;
        }

        static int RunSonarScan(Random random, int depth, int sampleCount)
        {
            return sampleCount;
        }

        static int RefillOxygen(int currentOxygen, int maxOxygen)
        {
            return currentOxygen;
        }

        static int ExecuteDive(Random random, int currentOxygen, StringBuilder logbook)
        {
            return currentOxygen;
        }


    }
}
