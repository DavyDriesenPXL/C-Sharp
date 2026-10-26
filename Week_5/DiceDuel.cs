using System;
using System.Collections.Generic;
using System.Text;

namespace Week_5
{
    internal class DiceDuel
    {
        public static void DiceDuel_()
        {
            int round = 1;
            int playerWins = 0;
            int computerWins = 0;
            
            PrintStart();

            do
            {
                PrintRound(round, playerWins, computerWins);

                int playerScore = RollThreeDice("PLAYER");
                int computerScore = RollThreeDice("COMPUTER");
                round++;

                if (playerScore > computerScore)
                {
                    Console.WriteLine("\nPLAYER wins this round!");
                    playerWins++;
                }
                else if (computerScore > playerScore)
                {
                    Console.WriteLine("\nCOMPUTER wins this round!");
                    computerWins++;
                }
                else
                {
                    Console.WriteLine("\nIt's a tie! No points rewarded.");
                }

                Console.WriteLine("\nPress ENTER for the next round ...");
                Console.ReadLine();
            } while (playerWins < 3 && computerWins < 3);
                       
                Console.Clear();
                Console.WriteLine("=== GAME OVER ===");

            if (playerWins == 3)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU WON THE DICE DUEL!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTHE COMPUTER WON THE DICE DUEL!");
                Console.ResetColor();
            }
            Console.WriteLine($"\nFinal score - Player: {playerWins}, Computer: {computerWins}");
            Console.WriteLine("\nPress ENTER to quit.");
            Console.ReadLine();
        }

        static void PrintStart()
        {
            Console.WriteLine("=== DICE DUEL ===\nBest out of five wins!\n\nPress ENTER to start ...\n");
            Console.ReadLine();
        }

        static void PrintRound(int round, int playerWins, int computerWins)
        {
            Console.Clear();
            Console.WriteLine($"--- ROUND {round} ---");

            if (playerWins > computerWins)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Player wins: {playerWins} ");
                Console.ResetColor();
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Computer wins: {computerWins}\n");
                Console.ResetColor();
            }
            else if (playerWins < computerWins)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Player wins: {playerWins} ");
                Console.ResetColor();
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Computer wins: {computerWins}\n");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Player wins: {playerWins} | Computer wins: {computerWins}");
            }
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
            
            Console.WriteLine($"\n{name} rolled:\n  Die 1: {roll1}\n  Die 2: {roll2}\n  Die 3: {roll3}\n  TOTAL: {total}");
                        
            return total;
        }


    }
}
