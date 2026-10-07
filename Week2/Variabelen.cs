using System;
using System.Collections.Generic;
using System.Text;

namespace Week_1
{
    internal class Variabelen
    {
        public static void Rekenmachine()
        {
            Console.Write("Geef het eerste getal: ");
            int.TryParse(Console.ReadLine(), out int number1);
            Console.Write("Geef het tweede getal: ");
            int.TryParse(Console.ReadLine(), out int number2);
            int result = number1 + number2;
            Console.WriteLine($"Resultaat: {number1} + {number2} = {result}");
        }

        public static void Celcius()
        {
            Console.Write("Temperatuur in Fahrenheit: ");
            double.TryParse(Console.ReadLine(), out double fahrenheit);
            double celcius = (fahrenheit - 32) * 5 / 9;
            Console.Write($"Temperatuur in Celcius: {celcius:F2}");


        }

        public static void Kassa()
        {

            Console.Write("Geef de prijs van het product: ");
            double.TryParse(Console.ReadLine(), out double price);
            Console.Write("Geef het aantal producten in: ");
            int.TryParse(Console.ReadLine(), out int amount);
            double result = price * amount;
            Console.WriteLine($"Het totaalbedrag is {result}");
        }


        public static void Bioscoop()
        {
            double normalPrice = 9.10;
            double discountPrice = 8.10;
            double studentPrice = 6.90;
            
            Console.WriteLine($"Normale tarief bedraagt: {normalPrice} euro");
            Console.WriteLine($"Kortingstarief bedraagt: {discountPrice} euro");
            Console.WriteLine($"Studententarief bedraagt {studentPrice} euro\n");

            Console.Write("Geef aantal tickets voor normale tarief: ");
            int.TryParse(Console.ReadLine(), out int amountNormal);
            Console.Write("Geef aantal tickets voor kortingstarief: ");
            int.TryParse(Console.ReadLine(), out int amountDiscount);
            Console.Write("Geef aantal tickets voor studententarief: ");
            int.TryParse(Console.ReadLine(), out int amountStudent);

            Console.WriteLine("\nUw afrekening\n");
            Console.WriteLine($"Normaal tarief: {normalPrice} x {amountNormal} = {normalPrice * amountNormal}");
            Console.WriteLine($"Normaal tarief: {discountPrice} x {amountDiscount} = {discountPrice * amountDiscount}");
            Console.WriteLine($"Normaal tarief: {studentPrice} x {amountStudent} = {studentPrice * amountStudent}\n");

            double total = (normalPrice * amountNormal) + (discountPrice * amountDiscount) + (studentPrice * amountStudent);
            Console.WriteLine($"Totaal: {total:F2}");

        }

        public static void Average()
        {
            Console.WriteLine("Geef 4 numerieke waarden in om het gemiddelde te berekenen:");
            
            if(!int.TryParse(Console.ReadLine(), out int number1) || number1 < 1 || number1 > 10)
                {
                Console.WriteLine("Getal 1 moet tussen 1 en 10 liggen");
            }
            
            int.TryParse(Console.ReadLine(), out int number2);
            int.TryParse(Console.ReadLine(), out int number3);
            int.TryParse(Console.ReadLine(), out int number4);

            double average = (double)(number1 + number2 + number3 + number4) / 4;
            Console.WriteLine($"\nHet gemiddelde van {number1}, {number2}, {number3} and {number4} is {average:F2}");
        }

        public static void Seconds()
        {
            
            Console.Write("Geef het aantal seconden in: ");
            int.TryParse(Console.ReadLine(), out int seconds);

            int hours = (seconds / 60) / 60;
            int minutes = (seconds / 60 ) % 60;
            seconds = seconds % 60;

            Console.WriteLine($"H: {hours} M: {minutes} S: {seconds}");

        }

        public static void Frisdrank()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int insertCoin = 2;

            Console.WriteLine($"Ingeworpen bedrag: {insertCoin}");
            Console.Write("Prijs van de gekozen frisdrank: € ");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            decimal change = insertCoin - price;
            Console.WriteLine($"Wisselgeld: {change:c}");

            int remainingCents = (int)(change * 100);

            int coins1Euro = remainingCents / 100;
            remainingCents %= 100;

            int coins50Cent = remainingCents / 50;
            remainingCents %= 50;

            int coins20Cent = remainingCents / 20;
            remainingCents %= 20;

            int coins10Cent = remainingCents / 10;
            remainingCents %= 10;

            int coins5Cent = remainingCents / 5;
            remainingCents %= 5;

            int coins2Cent = remainingCents / 2;
            remainingCents %= 2;

            int coins1Cent = remainingCents;
            Console.WriteLine($"Munten van 1 euro: {coins1Euro} " );
            Console.WriteLine($"Munten van 0,50 euro: {coins50Cent}");
            Console.WriteLine($"Munten van 0,20 euro: {coins20Cent}");
            Console.WriteLine($"Munten van 0,10 euro: {coins10Cent}");
            Console.WriteLine($"Munten van 0,05 euro: {coins5Cent}");
            Console.WriteLine($"Munten van 0,02 euro: {coins2Cent}");
            Console.WriteLine($"Munten van 0,01 euro: {coins1Cent}");
        }

        public static void Afstand()
        {
            Console.Write("Afstand in cm: ");
            int.TryParse(Console.ReadLine(), out int centimeters);

            int foot = (int)(centimeters / 2.54) / 12;
            double inches = (centimeters / 2.54) % 12;
            

            Console.WriteLine($"{centimeters} cm is gelijk aan {foot} voet en {inches:F2} inch.");
        }
    
    }
}
