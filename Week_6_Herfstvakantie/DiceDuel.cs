using System;
using System.Collections.Generic;
using System.Text;

namespace Week_Herfstvakantie
{
    internal class DiceDuel
    {
        public static void Run()
        {
            int round = 0;
            int playerWins = 0;
            int computerWins = 0;

            PrintStart();
            do
            {
                round++;

                PrintRound(round, playerWins, computerWins);

                int playerScore = RollThreeDice("PLAYER");
                int computerScore = RollThreeDice("COMPUTER");

                if (playerScore > computerScore)
                {
                    playerWins++;
                    Console.WriteLine("\nPlayer wins this round!");
                }
                else if (computerScore > playerScore)
                {
                    computerWins++;
                    Console.WriteLine("\nComputer wins this round!");
                }
                else
                {
                    Console.WriteLine("\nIt's a tie! No points awarded.");
                }

                Console.WriteLine("\nPress ENTER for next round...");
                Console.ReadLine();

            } while (playerWins < 3 && computerWins < 3);

            Console.Clear();
            Console.WriteLine("=== GAME OVER ===");
            if (playerWins > computerWins)
            {
                ShowText("\nYOU WON THE DICE DUEL", ConsoleColor.Green);
            }
            else
            {
                ShowText("\nTHE COMPUTER WON THE DICE DUEL", ConsoleColor.Red);
            }

            Console.WriteLine($"\n\nFinal score - Player: {playerWins}, Computer: {computerWins}");
            Console.WriteLine("\nPress ENTER to quit");
            Console.ReadLine();
        }

        static void PrintStart()
        {
            Console.WriteLine("=== DICE DUEL ===\nBest out of five wins!\n\nPress ENTER to start...");
            Console.ReadLine();
        }

        static void PrintRound(int round, int playerWins, int computerWins)
        {
            Console.Clear();

            Console.WriteLine($"--- ROUND {round} ---");
            if (playerWins > computerWins)
            {
                ShowText($"Player wins: {playerWins}", ConsoleColor.Green);
                Console.Write(" | ");
                ShowText($"Computer wins: {computerWins}\n", ConsoleColor.Red);
            }
            else if (computerWins > playerWins)
            {
                ShowText($"Player wins: {playerWins}", ConsoleColor.Red);
                Console.Write(" | ");
                ShowText($"Computer wins: {computerWins}\n", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine($"Player wins: {playerWins} | Computer wins: {computerWins}");
            }
        }

        static void ShowText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        static int RollDice()
        {
            Random random = new();
            int diceRoll = random.Next(1, 7);
            return diceRoll;
        }

        static int RollThreeDice(string name)
        {
            int roll1 = RollDice();
            int roll2 = RollDice();
            int roll3 = RollDice();

            int total = roll1 + roll2 + roll3;
            Console.WriteLine($"\n{name} rolled:\n Die 1: {roll1}\n Die 2: {roll2}\n Die 3: {roll3}\n TOTAL: {total}");

            return total;
        }
    }
}
