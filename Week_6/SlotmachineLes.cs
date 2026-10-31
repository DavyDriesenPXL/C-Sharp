using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Week_5
{
    internal class SlotmachineLes
    {
        public static void Run()
        {
            short round = 0;
            decimal credits;
            decimal stake;
            
            Console.WriteLine("Met hoeveel credits wil je spelen?");

            do
            {
                Console.Write("Credits: ");

            } while (!decimal.TryParse(Console.ReadLine(), out credits));

            while (credits > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Round {++round}, place your bets");
                Console.ResetColor();
                bool isCorrect = false;

                do
                {
                    Console.Write("Inzet: ");
                    string input = Console.ReadLine();
                    
                    isCorrect = decimal.TryParse(input, out stake);
                    if (!input.ToUpper().Equals("Q"))
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
                        return;
                    }
                } while (!isCorrect);

                credits = PlayRound(credits, stake, new Random());
                Console.WriteLine($"Credits\t:\t {credits}");
            }

        }
        static bool CanPlaceBet(decimal credits, decimal stake)
        {
            return stake <= credits;
        }

        static char SpinReel(Random rnd)
        {
            int random = rnd.Next(0, 100);
            if (random < 50)
                return 'A';
            if (random < 85)
                return 'B';
            return 'C';
        }

        static int PayOutMultiplier(char reel1, char reel2, char reel3)
        {
            if (reel1 == reel2 && reel2 == reel3)
            {
                return reel1 == 'A' ? 10 : (reel1 == 'B' ? 20 : 50);
            }
            if (reel1 == reel2 || reel2 == reel3)
            {
                return 2;
            }
            return 0;
        }

        static decimal PlayRound(decimal credits, decimal stake, Random rng)
        {
            char reel1;
            char reel2;
            char reel3;

            if (!CanPlaceBet(credits, stake))
            {
                Console.WriteLine("Onvoldoende credits");
                return credits;
            }

            credits -= stake;

            reel1 = SpinReel(rng);
            reel2 = SpinReel(rng);
            reel3 = SpinReel(rng);
            Console.WriteLine("+---+---+---+");
            Console.WriteLine($"| {reel1} | {reel2} | {reel3} |");
            Console.WriteLine("+---+---+---+");

            decimal payout = stake * PayOutMultiplier(reel1, reel2, reel3);
            Console.WriteLine($"Payout\t:\t {payout}");
            return credits + payout;
        }
    }
}
