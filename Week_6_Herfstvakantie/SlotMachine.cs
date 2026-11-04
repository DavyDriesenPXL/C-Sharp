using System;
using System.Collections.Generic;
using System.Text;

namespace Week_6_Herfstvakantie
{
    internal class SlotMachine
    {

        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Random rng = new();
            decimal credits;
            decimal stake;
            bool isCorrect;
            int round = 0;
            string input;
            
            Console.WriteLine("Met hoeveel credits wil je spelen?");
            do
            {
                Console.Write("Credits: ");
                isCorrect = decimal.TryParse(Console.ReadLine(), out credits) && credits > 0;

            } while (!isCorrect);

            do
            {
                round++;
                ShowColoredText($"\nRound {round}, place your bets!", ConsoleColor.Magenta);
                do
                {
                    Console.Write("Inzet: ");
                    input = Console.ReadLine();
                    isCorrect = decimal.TryParse(input, out stake) && stake > 0;
                } while (!isCorrect && input != "Q");

                if (input != "Q")
                {
                    credits = PlayRound(credits, stake, rng);
                }
                if (credits == 0)
                {
                    ShowColoredText("Geen credits meer!", ConsoleColor.Red);
                }
            } while (input != "Q" && credits > 0);

        }

        static bool CanPlaceBet(decimal credits, decimal stake)
        {
            return credits >= stake && credits > 0;
        }

        static char SpinReel(Random random)
        {
            int roll = random.Next(0, 100);

            if (roll <= 49)
            {
                return 'A';
            }
            else if (roll <= 84)
            {
                return 'B';
            }
            else
            {
                return 'C';
            }
        }

        static int PayoutMultiplier(char reel1, char reel2, char reel3)
        {
            if (reel1 == reel2 && reel2 == reel3)
            {
                if (reel1 == 'A')
                {
                    return 10;
                }
                if (reel1 == 'B')
                {
                    return 20;
                }
                else
                {
                    return 50;
                }
            }

            if (reel1 == reel2 || reel2 == reel3)
            {
                return 2;
            }

            return 0;
        }

        static decimal PlayRound(decimal credits, decimal stake, Random rng)
        {
            if (!CanPlaceBet(credits, stake))
            {
                ShowColoredText("Onvoldoende credits", ConsoleColor.Red);
                return credits;
            }
            else
            {
                credits -= stake;

                char roll1 = SpinReel(rng);
                char roll2 = SpinReel(rng);
                char roll3 = SpinReel(rng);

                Console.WriteLine("+---+---+---+");
                Console.WriteLine($"| {roll1} | {roll2} | {roll3} |");
                Console.WriteLine("+---+---+---+");

                decimal payout = stake * PayoutMultiplier(roll1, roll2, roll3);
                credits += payout;

                Console.WriteLine($"Payout\t:\t € {payout,8:f2}");
                Console.WriteLine($"Credits\t:\t € {credits,8:f2}");

                return credits;
            }
        }

        static void ShowColoredText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
