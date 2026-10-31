using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Week_5
{
    internal class HogerOfLager
    {
        public static void Run()
        {
            Random random = new();
            int streak = 0;
            int longestStreak = 0;
            int round = 0;
            decimal stake;
            bool isCorrect;

            decimal credits = ReadCredits();

            while (credits > 0)
            {
               
                do
                {
                    Console.Write("\nInzet: ");
                    string input = Console.ReadLine();

                    isCorrect = decimal.TryParse(input, out stake);
                    if (!string.Equals(input, "Q", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!isCorrect)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ongeldige ingave.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        ShowColoredText($"You cash out {credits} credits.", ConsoleColor.Green, true);
                        ShowColoredText($"Your longest streak was {longestStreak}.", ConsoleColor.Yellow, true);
                        return;
                    }
                } while (!isCorrect);

                if (!CanPlaceBet(credits, stake))
                {
                    ShowColoredText("Your stake can't be higher than your credits.", ConsoleColor.Red, true);
                }
                else
                {
                    int result = PlayRound(random);

                    if (result == 1)
                    {
                        streak++;
                        if(streak > longestStreak)
                        {
                            longestStreak = streak;
                        }
                        credits -= stake;
                        decimal payout = stake * PayoutMultiplier(result, streak);
                        credits += payout;
                        ShowColoredText("\nYou win!", ConsoleColor.Green, true);
                    }
                    else if (result == 0)
                    {
                        streak--; //bij een draw verlies je 1 streak, tenzij hij al 0 was
                        if (streak < 0)
                            streak = 0;
                        ShowColoredText("It's a draw!", ConsoleColor.DarkGray, true);
                    }
                    else
                    {
                        streak = 0;
                        credits -= stake;
                        ShowColoredText("\nYou lose!", ConsoleColor.Red, true);
                    }
                   
                    ShowStatus(++round, credits, streak);

                   
                    if (credits == 0)
                    {
                        ShowColoredText("You have no credits left", ConsoleColor.Red, true);
                        ShowColoredText($"Your longest streak was {longestStreak}.", ConsoleColor.Yellow, true);
                    }
                }
            }
        }

        static decimal ReadCredits()
        {
            decimal credits;
            bool isCorrect;
            bool isValid;


            Console.WriteLine("How much money do you have?");

            do
            {
                Console.Write("Credits: ");
                isCorrect = decimal.TryParse(Console.ReadLine(), out credits);
                isValid = credits > 0;
                if (!isCorrect)
                {
                    ShowColoredText("Enter a valid number.", ConsoleColor.Red, true);
                }
                else if (!isValid)
                {
                    ShowColoredText("You can't play without credits.", ConsoleColor.Red, true);
                }

            } while (!isCorrect || !isValid);

            return credits;

        }

        static bool CanPlaceBet(decimal credits, decimal stake)
        {
            return credits >= stake && stake > 0;
        }

        static int DrawCard(Random rng)
        {
            int card = rng.Next(1, 14);
            return card;
        }

        static char ReadGuess()
        {
            char guess;

            do
            {
                Console.Write("Higher or Lower? (H/L): ");
                guess = char.ToUpper(Console.ReadKey(true).KeyChar);

                if (guess != 'H' && guess != 'L')
                {
                    ShowColoredText("\nEnter H for higher or L for lower", ConsoleColor.Red, true);
                }

            } while (guess != 'H' && guess != 'L');

            return guess;
        }

        static int DetermineResult(int firstCard, int secondCard, char guess)
        {
            if (firstCard > secondCard && guess == 'L')
                return 1;
            if (firstCard < secondCard && guess == 'L')
                return -1;
            if (firstCard < secondCard && guess == 'H')
                return 1;
            if (firstCard > secondCard && guess == 'H')
                return -1;
            else
                return 0;
        }

        static int PayoutMultiplier(int result, int streak)
        {
            if (streak >= 3)
                return 3;
            else
                return 2;
        }

        static int PlayRound(Random rng)
        {
            int firstCard = DrawCard(rng);

            ShowColoredText($"The first card is: {firstCard}", ConsoleColor.Magenta, true);
            char guess = ReadGuess();
            int secondCard = DrawCard(rng);
            ShowColoredText($"\nThe second card is: {secondCard}", ConsoleColor.Magenta, true);
            int result = DetermineResult(firstCard, secondCard, guess);

            return result;
        }

        static void ShowStatus(int round, decimal credits, int streak)
        {
            Console.WriteLine($"Round: {round}");
            Console.WriteLine($"Credits: {credits}");
            Console.WriteLine($"Streak: {streak}");
        }

        static void ShowColoredText(string text, ConsoleColor color, bool newline = false)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            if (newline)
            {
                Console.WriteLine();
            }
            Console.ResetColor();

        }
    }
}
