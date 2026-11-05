using System;
using System.Collections.Generic;
using System.Text;

namespace Week_6_Herfstvakantie
{
    internal class FastParcel
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random random = new();
            int totalParcelsShipped = 0;
            int numberOfParcels;
            DateTime deliveryDate;
            bool isCorrect;
            int menuChoice;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welkom bij FastParcel");
                    Console.WriteLine(DateTime.Now);
                    Console.WriteLine($"Totaal aantal pakketten verzonden: {totalParcelsShipped}");
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine(" 1) Nieuwe zending\n 0) Afsluiten");
                    Console.Write("Maak uw keuze: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out menuChoice) && (menuChoice == 1 || menuChoice == 0);

                    if (!isCorrect)
                    {
                        ShowText("Ongeldige keuze", ConsoleColor.Red);
                        Console.WriteLine("Druk op enter om door te gaan...");
                        Console.ReadLine();
                    }
                } while (!isCorrect);

                if (menuChoice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Nieuwe zending\n--------------------------------");
                    do
                    {
                        Console.Write("Leverdatum: ");
                        isCorrect = DateTime.TryParse(Console.ReadLine(), out deliveryDate) && deliveryDate >= DateTime.Today && deliveryDate <= DateTime.Today.AddDays(14);
                        if (!isCorrect)
                        {
                            ShowText($"Ongeldige ingave. Geef een datum tussen {DateTime.Today.ToShortDateString()} en {DateTime.Today.AddDays(14).ToShortDateString()}", ConsoleColor.Red);
                        }
                    } while (!isCorrect);
                    do
                    {
                        Console.Write("Aantal pakketten: ");
                        isCorrect = int.TryParse(Console.ReadLine(), out numberOfParcels) && numberOfParcels >= 1 && numberOfParcels <= 5;
                        if (!isCorrect)
                        {
                            ShowText("Ongeldige ingave. Geef een getal tussen 1 en 5.", ConsoleColor.Red);
                        }
                    } while (!isCorrect);

                    RegisterShipment(random, deliveryDate, numberOfParcels);
                    totalParcelsShipped += numberOfParcels;
                    Console.WriteLine("Druk op enter om door te gaan ...");
                    Console.ReadLine();
                }
                else if(menuChoice == 0)
                {
                    Console.WriteLine("Tot ziens!");
                }
                
            } while (menuChoice != 0);
        }

        static void ShowText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static decimal CalculateShippingCost(int weight, DateTime deliverydate)
        {
            decimal price = 0;

            if (weight <= 2)
            {
                price = 4.50m;
            }
            else if (weight <= 10)
            {
                price = 8.00m;
            }
            else if (weight <= 20)
            {
                price = 14.50m;
            }
            else if (weight <= 30)
            {
                price = 22.00m;
            }

            if (deliverydate.DayOfWeek == DayOfWeek.Saturday || deliverydate.DayOfWeek == DayOfWeek.Sunday)
            {
                price *= 1.25m;
            }

            return price;
        }

        static string GenerateTrackingCode(Random rnd)
        {
            int trackingCode = rnd.Next(10000, 100000);
            return "BE-" + trackingCode;
        }

        static void RegisterShipment(Random random, DateTime deliveryDate, int parcelCount)
        {
            StringBuilder sb = new();

            bool isCorrect;
            decimal totalCost = 0;
            int weight;

            for(int i = 1; i <= parcelCount; i++)
            {
                do
                {
                    Console.Write($"Gewicht van pakket {i} (in kg): ");
                    isCorrect = int.TryParse(Console.ReadLine(), out weight) && weight >= 1 && weight <= 30;

                    if(!isCorrect)
                    {
                        ShowText("Ongeldige ingave. Geef een getal tussen 1 en 30.", ConsoleColor.Red);
                    }
                } while (!isCorrect);

                totalCost += CalculateShippingCost(weight, deliveryDate);
            }

            sb.AppendLine("\n==== FASTPARCEL ====");
            sb.AppendLine($"Trackingcode: {GenerateTrackingCode(random)}");
            sb.AppendLine($"Leverdatum: {deliveryDate.ToLongDateString()}");
            sb.AppendLine($"Aantal pakketten: {parcelCount}");
            sb.AppendLine($"Totaal: {totalCost:C}");
                       
            string result = sb.ToString();
            Console.WriteLine(result);
        }
    }
}
