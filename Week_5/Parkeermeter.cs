using System;
using System.Collections.Generic;
using System.Text;

namespace Week_5
{
    internal class Parkeermeter
    {
        public static void Parkeermeter_()
        {
            int menuChoice;
            DateTime sessionStart = DateTime.MinValue;
            DateTime sessionEnd = DateTime.MinValue;
            bool liveSession = false;
            bool endedSession = false;

            do
            {
                ShowTitle();
                menuChoice = ShowMenu();

                switch (menuChoice)
                {
                    case 1:
                        if (!liveSession)
                        {
                            sessionStart = StartSession();
                            liveSession = true;
                            endedSession = false;
                            Console.WriteLine("Sessie gestart! Druk op enter om verder te gaan...");
                            Console.ReadLine();
                        }
                        else
                        {
                            ShowError("Beëindig eerst de lopende sessie");
                        }
                        break;
                    case 2:
                        if (liveSession)
                        {
                            sessionEnd = StopSession();
                            liveSession = false;
                            endedSession = true;
                            Console.WriteLine("Sessie gestopt! Druk op enter om verder te gaan...");
                            Console.ReadLine();
                        }
                        else
                        {
                            ShowError("Er is momenteel geen lopende sessie.");
                        }
                        break;
                    case 3:
                        if (endedSession)
                        {
                            PrintTicket(sessionStart, sessionEnd);
                        }
                        else 
                        {
                            ShowError("Geen sessie gestart om af te printen.");
                        }
                        break;
                    case 4:
                        break;
                    default:
                        ShowError("Kies een geldige optie aub.");
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
            int input;

            Console.WriteLine("1 - Start\n2 - Stop\n3 - Ticket\n4 - Sluit");
            Console.Write("Uw keuze: ");
            int.TryParse(Console.ReadLine(), out input);

            return input;
        }

        static void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            Console.ReadLine();
        }

        static DateTime StartSession()
        {
            bool isCorrect;
            DateTime sessionStart;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Startijd: ");
                Console.ResetColor();
                isCorrect = DateTime.TryParse(Console.ReadLine(), out sessionStart);
            } while (!isCorrect);


            return sessionStart;

        }

        static DateTime StopSession()
        {
            bool isCorrect;
            DateTime sessionEnd;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Eindtijd: ");
                Console.ResetColor();
                isCorrect = DateTime.TryParse(Console.ReadLine(), out sessionEnd);
            } while (!isCorrect);

            return sessionEnd;
        }

        static decimal CalculatePrice(double minutes)
        {
            decimal price = 0;

            if (minutes > 15)
            {
                minutes = Math.Ceiling(minutes / 30);
                price = (decimal)minutes * 0.60m;
            }

            return price;
        }


        static void PrintTicket(DateTime start, DateTime end)
        {

            TimeSpan duration = end - start;
            decimal price = CalculatePrice(duration.TotalMinutes);

            Console.WriteLine($"Start:  {start}");
            Console.WriteLine($"Einde:  {end}");
            Console.WriteLine($"Duur:   {duration.TotalMinutes} minuten");
            Console.WriteLine($"Prijs:  {price:C}");
            Console.ReadLine();
        }
    }
}
