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
            Console.WriteLine(BuildGauge(60, 120));
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

                if(!isCorrect)
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
            double percentage = Math.Round((double)current / max * 100, 0, MidpointRounding.AwayFromZero);
            double gaugeVisual = Math.Round((double)current / max * width, 0, MidpointRounding.AwayFromZero);

           
            return gauge + percentage + "%";
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
