using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;

namespace Week_3
{
    internal class Controlestructuren
    {

        public static void GrootsteGetal()
        {
            Console.Write("Geef een geheel getal in: ");
            int.TryParse(Console.ReadLine(), out int number1);
            Console.Write("Geef nog een geheel getal in: ");
            int.TryParse(Console.ReadLine(), out int number2);

            Console.WriteLine(number1 > number2 ? "Het grootste getal is " + number1 : "Het grootste getal is " + number2);
            //Console.WriteLine($"Het grootste getal is {(number1 > number2 ? number1 : number2)}");
        }

        public static void Rekenmachine()
        {
            Console.Write("Geef het eerste getal: ");
            int.TryParse(Console.ReadLine(), out int number1);
            Console.Write("Geef het tweede getal: ");
            int.TryParse(Console.ReadLine(), out int number2);

            Console.WriteLine("Geef de gewenste bewerking in (*, / of + of -)");
            string? bewerking = Console.ReadLine();

            int result = 0;
            bool isCorrect = true;
            switch (bewerking)
            {
                case "/":
                    result = number1 / number2;
                    break;
                case "*":
                    result = number1 * number2;
                    break;
                case "+":
                    result = number1 + number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                default:
                    isCorrect = false;
                    break;
            }

            if (isCorrect)
                Console.WriteLine($"Resultaat: {result}");
            else
                Console.WriteLine("De bewerking was niet geldig");

        }

        public static void Meerderjarig()
        {
            Console.Write("Geef je geboortejaar in: ");

            string? input = Console.ReadLine();
            int geboortejaar;

            if (int.TryParse(input, out geboortejaar) && geboortejaar >= 1900 && geboortejaar <= 2026) ;
            {
                int leeftijd = 2026 - geboortejaar;

                Console.WriteLine($"Je bent {leeftijd} jaar oud, dus {(leeftijd >= 18 ? "meerderjarig." : "minderjarig.")}");
            }
        }

        public static void Verzekering()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Schrijf een toepassing die het te betalen bedrag berekend voor een hospitalisatieverzekering.\n");
            Console.ResetColor();

            Console.Write("Geef je leeftijd in: ");
            int.TryParse(Console.ReadLine(), out int age);

            Console.Write("In welk gewest woon je (Vlaanderen, Brussel of Wallonië): ");
            string? region = Console.ReadLine();

            Console.Write("Rook je? Y/N: ");
            string? smoker = Console.ReadLine();
            int fee = 0;

            if (age >= 18 && age < 67)
            {
                fee += 150;
            }
            else if (age >= 67)
            {
                fee += 300;
            }
                        
            if(region == "Brussel" && age >= 18)
            {
                fee += 200;
            }

            if(smoker == "Y")
            {
                fee *= 2;
            }
            
            Console.WriteLine($"De totaalprijs van uw hospitalisatieverzekering is: {fee} euro.");

        }
    }
}
