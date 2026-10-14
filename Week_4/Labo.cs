using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace Week_4
{
    internal class Labo
    {
        public static void KnightsVsGoblins()
        {
            Random Random = new Random();

            int knightHealth;
            int goblinHealth = Random.Next(0, 101);
            bool isCorrect;

            do
            {
                Console.Write("Enter the health points for your knight: ");
                isCorrect = int.TryParse(Console.ReadLine(), out knightHealth) && knightHealth > 0 && knightHealth < 100;

                if (!isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid number between 0 and 100!");
                    Console.ResetColor();
                }

            } while (!isCorrect);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Knight health: {knightHealth}");
            Console.WriteLine($"Goblin health: {goblinHealth}");
            Console.ResetColor();

            Console.WriteLine("Choose your next move carefully");

            int knightAttack = 10;
            int goblinAttack = Random.Next(5, 17);
            string? action;

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1. Attack");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("2. Heal");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                action = Console.ReadLine();



                switch (action)
                {
                    case "1":

                        goblinAttack = Random.Next(5, 17);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"You attacked the goblin for {knightAttack} damage!");
                        Console.WriteLine($"You were attacked by the goblin for {goblinAttack} damage!");
                        Console.ResetColor();
                        knightHealth -= goblinAttack;
                        goblinHealth -= knightAttack;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Knight health: {knightHealth}");
                        Console.WriteLine($"Goblin health: {goblinHealth}");
                        break;
                    case "2":

                        goblinAttack = Random.Next(5, 17);

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("You healed yourself for 10 healthpoints!");
                        Console.ResetColor();
                        Console.WriteLine($"You were attacked by the goblin for {goblinAttack} damage!");
                        knightHealth += 10;
                        knightHealth -= goblinAttack;
                        Console.WriteLine($"Knight health: {knightHealth}");
                        Console.WriteLine($"Goblin health: {goblinHealth}");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("That action was not possible. You have been hit for 5 damage!");
                        knightHealth -= 5;
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Knight healt: {knightHealth}");
                        Console.ResetColor();
                        break;
                }

                if (goblinHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Victory! You have killed the goblin!");
                    Console.ResetColor();
                }
                if (knightHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Defeat! You were killed by the goblin!");
                    Console.ResetColor();
                }
            } while (goblinHealth > 0 && knightHealth > 0);
        }
    }
}
