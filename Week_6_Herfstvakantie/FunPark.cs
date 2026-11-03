using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Week_Herfstvakantie
{
    internal class FunPark
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Random random = new();
            int totalTicketsSold = 0;
            DateTime visitDate;
            int menuChoice;
            bool isCorrect;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welkom bij FunPark");
                    Console.WriteLine(DateTime.Now);
                    Console.WriteLine($"Totaal aantal tickets verkocht: {totalTicketsSold}");
                    Console.WriteLine("-------------------------------------");

                    Console.WriteLine(" 1) Nieuwe bestelling");
                    Console.WriteLine(" 0) Afsluiten");
                    Console.Write("Maak uw keuze: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out menuChoice) && (menuChoice == 1 || menuChoice == 0);

                    if (!isCorrect)
                    {
                        ShowText("Ongeldige keuze", ConsoleColor.Red);
                        Console.WriteLine("Druk op enter om door te gaan ...");
                        Console.ReadLine();
                    }
                } while (!isCorrect);

                if (menuChoice == 1)
                {
                    int tickets;

                    Console.Clear();
                    Console.WriteLine("Nieuwe bestelling\n--------------------------");
                    do
                    {
                        Console.Write("Bezoekdatum: ");
                        isCorrect = DateTime.TryParse(Console.ReadLine(), out visitDate) && visitDate.Date >= DateTime.Today && visitDate.Date <= DateTime.Today.AddDays(30);

                        if (!isCorrect)
                        {
                            ShowText($"Ongeldige ingave. Geef een datum tussen {DateTime.Today.ToShortDateString()} en {DateTime.Today.AddDays(30).ToShortDateString()}.", ConsoleColor.Red);
                        }
                    } while (!isCorrect);

                    do
                    {
                        Console.Write("Aantal tickets: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out tickets) && (tickets >= 1 && tickets <= 10);

                        if (!isCorrect)
                        {
                            ShowText("Ongeldige ingave. Geef een getal tussen 1 en 10.", ConsoleColor.Red);
                        }

                    } while (!isCorrect);
                    PlaceOrder(random, visitDate, tickets);
                    totalTicketsSold += tickets;
                    Console.WriteLine("Druk op enter om door te gaan ...");
                    Console.ReadLine();
                }

                if (menuChoice == 0)
                {
                    Console.WriteLine("Tot ziens");
                }

            } while (menuChoice != 0);
        }

        static void ShowText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static decimal CalculatePrice(int age, DateTime date)
        {
            decimal price = 0.00m;

            if (age <= 2)
            {
                price += 0.00m;
            }
            else if (age <= 11)
            {
                price += 15.00m;
            }
            else if (age <= 64)
            {
                price += 25.00m;
            }
            else if (age >= 65)
            {
                price += 18.00m;
            }

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                price *= 1.2m;
            }

            return price;
        }

        static string GenerateOrderCode(Random random)
        {
            int orderCode = random.Next(1000, 10000);
            return "FP-" + orderCode;
        }

        static void PlaceOrder(Random random, DateTime visitDate, int ticketCount)
        {
            bool isCorrect;
            int age = 0;
            decimal totalPrice = 0;
            StringBuilder sb = new();

            for (int i = 1; i <= ticketCount; i++)
            {
                do
                {
                    Console.Write($"Leeftijd van bezoeker {i}: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out age) && age >= 0 && age < 120;

                    if (!isCorrect)
                    {
                        ShowText("Ongeldige ingave. Geef een getal tussen 0 en 120", ConsoleColor.Red);
                    }

                } while (!isCorrect);

                totalPrice += CalculatePrice(age, visitDate);
            }

            sb.AppendLine("\n==== FUNPARK ====");
            sb.AppendLine($"Bestelcode: {GenerateOrderCode(random)}");
            sb.AppendLine($"Bezoekdatum: {visitDate.ToLongDateString()}");
            sb.AppendLine($"Aantal tickets: {ticketCount}");
            sb.AppendLine($"Totaal: € {totalPrice:F2}");

            string result = sb.ToString();

            Console.WriteLine(result);

        }
    }

}

