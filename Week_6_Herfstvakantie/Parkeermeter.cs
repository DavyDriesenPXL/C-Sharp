using System;
using System.Text;

namespace Week_6_Herfstvakantie
{
    internal class Parkeermeter
    {
        public static void Run()
        {
            DateTime startSession = DateTime.MinValue;
            DateTime endSession = DateTime.MinValue;
            int menuChoice;
            
            do
            {
                ShowTitle();
                menuChoice = ShowMenu();

                switch (menuChoice)
                {
                    case 1:
                        if (startSession == DateTime.MinValue)
                        {
                            startSession = StartSession();
                        }
                        else
                        {
                            ShowError("Beëindig eerst de lopende sessie.");
                        }
                        break;
                    case 2:
                        if (startSession == DateTime.MinValue)
                        {
                            ShowError("Er is momenteel geen lopende sessie.");
                        }
                        else
                        {
                            endSession = StopSession();
                        }
                        break;
                    case 3:
                        if (startSession != DateTime.MinValue && endSession != DateTime.MinValue)
                        {
                            PrintTicket(startSession, endSession);
                            Console.ReadLine();
                        }
                        else
                        {
                            ShowError("Er is nog geen starttijd en/of eindtijd ingegeven.");
                        }
                        break;
                    default:
                        if (menuChoice != 4)
                        {
                            ShowError("Kies een geldige optie aub.");
                        }
                        break;
                }

            } while (menuChoice != 4);
        }

        static void ShowTitle()
        {
            Console.Clear();
            Console.WriteLine("+-------------+");
            Console.WriteLine("| P(arking)XL |");
            Console.WriteLine("+-------------+");
        }

        static int ShowMenu()
        {
            Console.WriteLine(" 1 - Start\n 2 - Stop\n 3 - Ticket\n 4 - Sluit");
            Console.Write("Uw keuze: ");
            int.TryParse(Console.ReadLine(), out int menuChoice);

            return menuChoice;
        }

        static void ShowError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
            Console.ReadLine();
        }

        static DateTime StartSession()
        {
            DateTime sessionStart;
            bool isCorrect;

            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Starttijd: ");
                isCorrect = DateTime.TryParse(Console.ReadLine(), out sessionStart);
            } while (!isCorrect);

            Console.ResetColor();
            Console.WriteLine("Sessie gestart! Druk op enter om verder te gaan...");
            Console.ReadLine();

            return sessionStart;
        }

        static DateTime StopSession()
        {
            DateTime sessionStop;
            bool isCorrect;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Eindtijd: ");
                isCorrect = DateTime.TryParse(Console.ReadLine(), out sessionStop);
            } while (!isCorrect);

            Console.ResetColor();
            Console.WriteLine("Sessie gestopt! Druk op enter om verder te gaan...");
            Console.ReadLine();

            return sessionStop;
        }

        static decimal CalculatePrice(double totalSessionInMinutes)
        {
            decimal price = (decimal)(Math.Ceiling((totalSessionInMinutes - 15) / 30)) * 0.60m;

            if(price > 8)
            {
                price = 8;
            }

            return price;
        }

        static void PrintTicket(DateTime sessionStart, DateTime sessionEnd)
        {
            StringBuilder sb = new();

            TimeSpan minutes = sessionEnd - sessionStart;

            decimal price = CalculatePrice(minutes.TotalMinutes);
            Console.WriteLine();
            ShowTitle();
            sb.AppendLine($"\nStarttijd:\n{sessionStart.ToShortDateString(),15}\n{sessionStart.ToShortTimeString(),15}");
            sb.AppendLine($"Eindtijd:\n{sessionEnd.ToShortDateString(),15}\n{sessionEnd.ToShortTimeString(),15}");
            sb.AppendLine($"Duur:\n{minutes.TotalMinutes,7} minuten");
            sb.AppendLine($"Prijs:\n{price,15:F2}");
            sb.AppendLine("----------------");
            sb.AppendLine("\n\nDruk op enter om verder te gaan ...");

            string result = sb.ToString();
            Console.WriteLine(result);
        }

    }
}
