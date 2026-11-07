using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;

namespace Week_6_Herfstvakantie
{
    internal class DeepDive2
    {
        public static void Run()
        {
            Random random = new();
            StringBuilder logbook = new();
            int maxOxygen = 120;
            int currentOxygen = 120;
            int completedDives = 0;
            int menuChoice;

            do
            {
                Console.Clear();
                Console.WriteLine("=== DEEPDIVE: NAUTILUX-X ===");
                Console.WriteLine($"Zuurstoftank : {BuildGauge(currentOxygen, maxOxygen, 20)} ({currentOxygen}/{maxOxygen})");
                Console.WriteLine($"Expeditiedoel: {BuildGauge(completedDives, 5)} ({completedDives}/5 duiken)");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(" 1) Nieuwe duik uitvoeren\n 2) Oppervlaktestop (zuurstof bijvullen)\n 3) Expeditielogboek bekijken\n 0) Missie beëindigen");
                menuChoice = ReadIntInRange("Maak uw keuze (0-3): ", 0, 3);

                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Nieuwe duikmissie\n----------------------------------------");
                        int oxygenLeft = ExecuteDive(random, currentOxygen, logbook);

                        if (currentOxygen > oxygenLeft)
                        {
                            currentOxygen = oxygenLeft;
                            completedDives++;
                        }
                        Console.WriteLine("Druk enter om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Oppervlaktestop\n----------------------------------------");
                        currentOxygen = RefillOxygen(currentOxygen, maxOxygen);
                        Console.WriteLine("Druk enter om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Expeditielogboek\n----------------------------------------");
                        if (logbook.Length == 0)
                        {
                            Console.WriteLine("Nog geen duiken geregistreerd.");
                        }
                        else
                        {
                            string result = logbook.ToString();
                            Console.WriteLine(result);
                        }
                        Console.WriteLine("Druk enter om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 0:
                        Console.WriteLine("Nautilus-X keert terug naar de haven. Tot ziens!");
                        break;
                }
            } while (menuChoice != 0);
        }

        static int ReadIntInRange(string prompt, int min, int max)
        {
            bool isCorrect;
            int output;
            do
            {
                Console.Write(prompt);
                isCorrect = int.TryParse(Console.ReadLine(), out output) && output >= min && output <= max;

                if (!isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ongeldige ingave. Geef een getal tussen {min} en {max}.");
                    Console.ResetColor();
                }
            } while (!isCorrect);

            return output;
        }

        static string BuildGauge(int current, int max, int width = 10)
        {
            string gauge = "";

            if (current > max)
            {
                current = max;
            }

            double percentage = Math.Round((double)current / max * 100, 0, MidpointRounding.AwayFromZero);
            double filledCount = Math.Round((double)current / max * width, 0, MidpointRounding.AwayFromZero);

            for (int i = 0; i < width; i++)
            {

                if (i < filledCount)
                {
                    gauge += "#";
                }
                else
                {
                    gauge += ".";
                }
            }

            return $"[{gauge}] {(int)percentage}%";
        }

        static int CalculateOxygenUse(int depth, int durationHours)
        {
            int oxygenUse = 0;

            if (depth <= 200)
            {
                oxygenUse = 5;
            }
            else if (depth <= 600)
            {
                oxygenUse = 10;
            }
            else if (depth <= 1000)
            {
                oxygenUse = 18;
            }

            return oxygenUse * durationHours;
        }

        static int CalculateOxygenUse(int depth, int durationHours, bool useBoosters, int waterTemp)
        {
            double oxygenUse = CalculateOxygenUse(depth, durationHours);

            if (useBoosters)
            {
                oxygenUse *= 1.25;
            }
            if (waterTemp < 4)
            {
                oxygenUse += 5;
            }

            oxygenUse = Math.Ceiling(oxygenUse);

            return (int)oxygenUse;
        }

        static int RunSonarScan(Random random, int depth, int sampleCount)
        {
            int totalPoints = 0;

            Console.WriteLine("--- Sonarresultaten ---");
            for (int i = 1; i <= sampleCount; i++)
            {
                int frequency = random.Next(10, 100);

                Console.Write($"Scan {i} ({frequency} Hz): ");

                if (frequency % 7 == 0 && depth > 500)
                {
                    Console.Write("Hydrothermale bron (+25 pt)");
                    totalPoints += 25;
                }
                else if (frequency % 2 == 0)
                {
                    Console.Write("Koraal- of mineraalader (+10 pt)");
                    totalPoints += 10;
                }
                else
                {
                    Console.Write("Lege zeebodem (+0 pt)");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Behaalde onderzoekspunten tijdens deze duik: {totalPoints} pt");

            return totalPoints;
        }

        static int RefillOxygen(int currentOxygen, int maxOxygen)
        {
            if (currentOxygen >= maxOxygen * 0.75)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Oppervlaktestop geweigerd: zuurstofpeil is nog minstens 75%.");
                Console.ResetColor();
                return currentOxygen;
            }
            else
            {
                currentOxygen += 40;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Oppervlaktestop voltooid! Zuurstofpeil hersteld naar {Math.Min(currentOxygen, maxOxygen)}/{maxOxygen}.");
                Console.ResetColor();

                return Math.Min(currentOxygen, maxOxygen);
            }
        }

        static int ExecuteDive(Random random, int currentOxygen, StringBuilder logbook)
        {
            bool isCorrect;
            bool useBoosters = false;
            int maxOxygen = 120;
            
            int depth = ReadIntInRange("Doeldiepte in meter (10-1000): ", 10, 1000);
            int durationHours = ReadIntInRange("Duikduur in uren (1-6): ", 1, 6);
            int waterTemp = ReadIntInRange("Watertgemperatuur in C (-2 tot 25): ", -2, 25);
            int sampleCount = ReadIntInRange("Aantal sonarscans (1-5): ", 1, 5);

            do
            {
                Console.Write("Turbomotoren inschakelen (J/N): ");
                string output = Console.ReadLine();
                isCorrect = output.Equals("J",StringComparison.OrdinalIgnoreCase) || output.Equals("N",StringComparison.OrdinalIgnoreCase);

                if (output.Equals("J",StringComparison.OrdinalIgnoreCase))
                {
                    useBoosters = true;
                }

                if (!isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ongeldige ingave. Antwoord met J of N.");
                    Console.ResetColor();
                }
            } while (!isCorrect);

            int oxygenNeeded = CalculateOxygenUse(depth, durationHours, useBoosters, waterTemp);

            if(oxygenNeeded > currentOxygen)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ALARM: Onvoldoende zuurstof! Benodigd: {oxygenNeeded},beschikbaar: {currentOxygen}. Duik afgebroken.");
                Console.ResetColor();
                return currentOxygen;
            }
            else
            {
                currentOxygen -= oxygenNeeded;
                Console.WriteLine($"\nZuurstofverbruik voor deze duik: {oxygenNeeded} eenheden.");

                int divePoints = RunSonarScan(random, depth, sampleCount);
                Console.WriteLine($"Duik succesvol afgerond! Resterende zuurstof: {currentOxygen}/{maxOxygen}.");
                logbook.AppendLine($"[{DateTime.Now.ToShortTimeString()}] Diepte: {depth}m | Duur: {durationHours}u | Zuurstof: -{oxygenNeeded} | Score: +{divePoints} pt");

                return currentOxygen;
            }

        }

    }
}
