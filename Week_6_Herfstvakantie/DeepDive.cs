using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Week_Herfstvakantie
{
    internal class DeepDive
    {
        public static void Run()
        {
            Random random = new();
            StringBuilder logbook = new();

            int maxOxygen = 120;
            int currentOxygen = 120;
            int completedDives = 0;
            int menuChoice;
            bool isCorrect;

            do
            {
                Console.Clear();
                Console.WriteLine("=== DEEPDIVE: NAUTILUS-X ===");
                Console.WriteLine($"Zuurstoftank: {BuildGauge(currentOxygen, maxOxygen, 20)} ({currentOxygen}/{maxOxygen})");
                Console.WriteLine($"Expeditiedoel: {BuildGauge(completedDives, 5)} ({completedDives}/5 duiken)");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine(" 1) Nieuwe duik uitvoeren\n 2) Oppervlaktestop (zuurstof bijvullen)\n 3) Expeditielogboek bekijken\n 0) Missie beëindigen");
                menuChoice = ReadIntInRange("Maak uw keuze (0-3): ", 0, 3);

                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Nieuwe duikmissie\n----------------------");
                        int oxygenAfterDive = ExecuteDive(random, currentOxygen, logbook);
                        if (currentOxygen > oxygenAfterDive)
                        {
                            currentOxygen = oxygenAfterDive;
                            completedDives++;
                        }
                        Console.WriteLine("\nDruk ENTER om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Oppervlaktestop\n----------------------");
                        currentOxygen = RefillOxygen(currentOxygen, maxOxygen);
                        Console.WriteLine("\nDruk ENTER om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Expeditielogboek\n----------------------");
                        if (logbook.Length == 0)
                        {
                            Console.WriteLine("Nog geen duiken geregistreerd.");
                        }
                        else
                        {
                            string logbookResult = logbook.ToString();
                            Console.WriteLine(logbookResult);
                        }
                        Console.WriteLine("\nDruk ENTER om door te gaan...");
                        Console.ReadLine();
                        break;
                    case 0:
                        Console.WriteLine("\nNautilus-X keert terug naar de haven. Tot ziens!");
                        break;
                }

            } while (menuChoice != 0);

        }

        static int ReadIntInRange(string prompt, int min, int max)
        {
            int output;
            bool isCorrect;

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
            int baseUse;

            if (depth >= 10 && depth <= 200)
            {
                baseUse = 5;
            }
            else if (depth >= 201 && depth <= 600)
            {
                baseUse = 10;
            }
            else
            {
                baseUse = 18;
            }

            baseUse *= durationHours;

            return baseUse;
        }

        static int CalculateOxygenUse(int depth, int durationHours, bool useBoosters, int waterTemp)
        {
            int baseUse = CalculateOxygenUse(depth, durationHours);
            double totalUse = baseUse;

            if (useBoosters)
            {
                totalUse *= 1.25;
            }
            if (waterTemp < 4)
            {
                totalUse += 5;
            }
            totalUse = Math.Ceiling(totalUse);

            return (int)totalUse;
        }

        static int RunSonarScan(Random random, int depth, int sampleCount)
        {
            int points = 0;

            Console.WriteLine("--- Sonarresultaten ---");

            for (int i = 1; i <= sampleCount; i++)
            {
                int frequency = random.Next(10, 100);
                Console.Write($"Scan {i} ({frequency} Hz): ");

                if (frequency % 7 == 0 && depth > 500)
                {
                    Console.Write("Hydrothermale bron (+25 pt)");
                    points += 25;
                }
                else if (frequency % 2 == 0)
                {
                    Console.Write("Koraal- of mineraalader (+10 pt)");
                    points += 10;
                }
                else
                {
                    Console.Write("Lege zeebodem (+0 pt)");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Behaalde onderzoekspunten tijdens deze duik: {points} pt");

            return points;
        }

        static int RefillOxygen(int currentOxygen, int maxOxygen)
        {

            if (((double)currentOxygen / maxOxygen * 100) >= 75)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Oppervlaktestop geweigerd: zuurstofpeil is nog minstens 75%.");
                Console.ResetColor();

                return currentOxygen;
            }
            else
            {
                int newOxygen = Math.Min(currentOxygen + 40, maxOxygen);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Oppervlaktestop voltooid! Zuurstofpeil hersteld naar {newOxygen}/{maxOxygen}.");
                Console.ResetColor();

                return newOxygen;
            }
        }

        static int ExecuteDive(Random random, int currentOxygen, StringBuilder logbook)
        {
            int depth = ReadIntInRange("Doeldiepte in meter (10-1000): ", 10, 1000);
            int durationHours = ReadIntInRange("Duikduur in uren (1-6): ", 1, 6);
            int waterTemp = ReadIntInRange("Watertemperatuur in °C (-2 tot 25): ", -2, 25);
            int sampleCount = ReadIntInRange("Aantal sonarscans (1-5): ", 1, 5);

            string enableTurbo;
            bool isCorrect;
            bool useBoosters = false;
            do
            {
                Console.Write("Turbomotoren inschakelen? (J/N): ");
                enableTurbo = Console.ReadLine();
                isCorrect = enableTurbo.ToUpper().Equals("J") || enableTurbo.ToUpper().Equals("N");

                if (!isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ongeldige ingave. Antwoord met J of N.");
                    Console.ResetColor();
                }
                if (enableTurbo.ToUpper().Equals("J"))
                {
                    useBoosters = true;
                }

            } while (!isCorrect);

            int oxygenNeeded = CalculateOxygenUse(depth, durationHours, useBoosters, waterTemp);
            int maxOxygen = 120;

            if (oxygenNeeded > currentOxygen)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nALARM: Onvoldoende zuurstof! Benodigd: {oxygenNeeded}, beschikbaar: {currentOxygen}. Duik afgebroken.");
                Console.ResetColor();
                return currentOxygen;
            }
            else
            {
                Console.WriteLine($"\nZuurstofverbruik voor deze duik: {oxygenNeeded} eenheden.");

                currentOxygen -= oxygenNeeded;

                int points = RunSonarScan(random, depth, sampleCount);
                Console.WriteLine($"Duik succesvol afgerond! Resterende zuurstof: {currentOxygen}/{maxOxygen}.");
                logbook.AppendLine($"[{DateTime.Now.ToShortTimeString()}] Diepte: {depth}m | Duur: {durationHours}u | Zuurstof: -{oxygenNeeded} | Score: +{points} pt");

                return currentOxygen;
            }
        }
    }
}
