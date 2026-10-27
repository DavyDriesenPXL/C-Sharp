using System;
using System.Collections.Generic;
using System.Text;

namespace Week_5
{
    internal class Slotmachine
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random random = new();
            decimal credits;
            decimal stake;
            bool isValid;
            bool hasMoney;
            string input;
            int round = 1;

            Console.WriteLine("Met hoeveel credits wil je spelen?");
            do
            {
                Console.Write("Credits: ");
                isValid = decimal.TryParse(Console.ReadLine(), out credits);
                hasMoney = credits > 0;

                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Je invoer was niet geldig. Geef een getal in.");
                    Console.ResetColor();
                }
                else if(!hasMoney)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Je kunt niet spelen zonder credits.");
                    Console.ResetColor();
                }

            } while (!isValid || !hasMoney);

            do
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nRound {round}, place your bets!");
                    Console.ResetColor();
                    Console.Write("Inzet: ");
                    input = Console.ReadLine();
                    isValid = decimal.TryParse(input, out stake);
                    if (CanPlaceBet(credits, stake) && isValid)
                    {
                        round++;
                    }

                } while (!isValid && !input.Equals("Q", StringComparison.OrdinalIgnoreCase));

                if (!input.Equals("Q", StringComparison.OrdinalIgnoreCase) && credits > 0)
                {
                    credits = PlayRound(credits, stake, random);
                }
                if (credits == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nGeen credits meer! Druk op ENTER om af te sluiten...");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            } while (!input.Equals("Q", StringComparison.OrdinalIgnoreCase) && credits > 0);

        }

        static bool CanPlaceBet(decimal credits, decimal stake)
        {
            if (credits >= stake && stake > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static char SpinReel(Random random)
        {
            int roll = random.Next(0, 100);

            if (roll >= 0 && roll <= 49)
            {
                return 'A';
            }
            if (roll >= 50 && roll <= 84)
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
                if (reel1 == 'C')
                {
                    return 50;
                }
            }
            else if (reel1 == reel2 || reel2 == reel3)
            {
                return 2;
            }
            return 0;
        }

        static decimal PlayRound(decimal credits, decimal stake, Random rng)
        {
            if (!CanPlaceBet(credits, stake))
            {
                if (stake <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Je moet minstens 1 credit inzetten");
                    Console.ResetColor();
                }
                if (credits < stake)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Onvoldoende credits");
                    Console.ResetColor();
                }
            }
            else
            {
                credits -= stake;
                char roll1 = SpinReel(rng);
                char roll2 = SpinReel(rng);
                char roll3 = SpinReel(rng);

                decimal multiplier = PayoutMultiplier(roll1, roll2, roll3);

                decimal payout = stake * multiplier;
                credits += payout;

                Console.WriteLine($"{roll1} | {roll2} | {roll3}");
                Console.WriteLine($"Payout\t: \t€ {payout,5:F2}");
                Console.WriteLine($"Credits\t: \t€ {credits,5:F2}");
            }
            return credits;
        }
    }
}
