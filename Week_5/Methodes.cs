using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Week_5
{
    internal class Methodes
    {
        public static void Raadspelletje()
        {
            Random random = new Random();
            int rightAnswer = random.Next(1, 100);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Raadspel\n");
            Console.ResetColor();
            StartCheck(rightAnswer);
        }

        static void StartCheck(int rightAnswer)
        {
            int guess;
            int attempts = 0;
            do
            {
                attempts++;
                guess = GiveAnswer();
                CheckAnswer(rightAnswer, guess);
            } while (guess != rightAnswer);

            Console.WriteLine($"Je hebt het getal geraden in {attempts} pogingen!");

        }

        static int GiveAnswer()
        {
            bool isCorrect;
            int guess;

            do
            {
                Console.Write("Geef een getal tussen 1 en 100: ");
                isCorrect = int.TryParse(Console.ReadLine(), out guess) && guess > 1 && guess < 100;

            } while (!isCorrect);

            return guess;

        }

        static void CheckAnswer(int rightAnswer, int guess)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (rightAnswer < guess)
            {
                Console.WriteLine("Raad lager!");
            }
            else if (rightAnswer > guess)
            {
                Console.WriteLine("Raad hoger!");
            }
            Console.ResetColor();
        }
        //=================================================================================================================================================================

        public static void Rekenmachine()
        {
            double number1 = ReadNumber(1);
            double number2 = ReadNumber(2);
            char operation = ReadCalculation();

            double outcome = Calculate(number1, number2, operation);
            outcome = Math.Round(outcome, 2, MidpointRounding.AwayFromZero);

            Console.WriteLine($"\n{number1} {operation} {number2} = {outcome}");

            static double ReadNumber(byte numbersToCalculate)
            {
                double number;
                bool isCorrect;

                do
                {
                    Console.Write($"Geef getal {numbersToCalculate}: ");
                    isCorrect = double.TryParse(Console.ReadLine(), out number);

                    if (!isCorrect)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Geef een geheel getal in");
                        Console.ResetColor();
                    }
                }
                while (!isCorrect);

                return number;
            }

            static char ReadCalculation()
            {

                bool isCorrect;
                char parameter;
                do
                {
                    Console.Write("Geef bewerking: ");
                    isCorrect = char.TryParse(Console.ReadLine(), out parameter) && parameter == '+' || parameter == '-' || parameter == '/' || parameter == '*';

                    if (!isCorrect)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Geef een geldige bewerking in (+, -, / of *).");
                        Console.ResetColor();
                    }

                } while (!isCorrect);

                return parameter;
            }

            static double Calculate(double chosenNumber, double chosenNumber2, char parameter)
            {
                double result = 0;

                switch (parameter)
                {
                    case '+':
                        result = chosenNumber + chosenNumber2;
                        break;
                    case '-':
                        result = chosenNumber - chosenNumber2;
                        break;
                    case '*':
                        result = chosenNumber * chosenNumber2;
                        break;
                    case '/':
                        result = chosenNumber / chosenNumber2;
                        break;
                }
                return result;
            }

        }
    }
}

