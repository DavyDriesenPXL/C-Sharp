using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text;

namespace Week_5
{
    internal class BlackJack
    {

        public static void BlackJack_()
        {
            ShowTitle();
            ShowMenu();

            char menuChoice;

            do
            {
                menuChoice = Console.ReadKey(true).KeyChar;

                switch (menuChoice)
                {
                    case '1':
                        ShowColoredText("Your turn, press ENTER to draw a card...", ConsoleColor.Cyan, true);
                        Console.ReadLine();
                        PlayNextGame();
                        break;
                    case '2':
                        ShowColoredText("See you next time!\nThanks for playing!\nB L A C K J A C K", ConsoleColor.Blue, true);
                        break;
                    default:
                        ShowColoredText("Invalid choice, please try again.", ConsoleColor.Red, true);
                        ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue, true);
                        Console.ReadLine();
                        break;
                }
            } while (menuChoice != '2');
        }

        static void ShowTitle()
        {
            ShowColoredText("B L A C K J A C K\n\nWelcome to the table!\n---------------------------", ConsoleColor.Blue, true);
        }

        static void ShowMenu()
        {
            ShowColoredText("Are you feeling lucky?", ConsoleColor.Magenta, true);
            ShowColoredText("  1. Hell yeah! Lets play!\n  2. Nah, Maybe later.", ConsoleColor.DarkYellow, true);
        }

        static int GetRandomCard()
        {
            Random random = new();
            int cardValue = random.Next(2, 12);
            return cardValue;
        }

        static int DealComputerCards()
        {
            int totalCardValue = 0;

            ShowColoredText("Dealers turn, press ENTER to start...", ConsoleColor.Cyan, true);

            do
            {
                int cardValue = GetRandomCard();
                totalCardValue += cardValue;
                ShowColoredText($"Dealer drew a {cardValue}. Dealers total is now {totalCardValue}", ConsoleColor.Green, true);

            } while (totalCardValue < 16);

            return totalCardValue;
        }

        static int DealPlayerCards()
        {
            int totalCardValue = 0;
            char hitOrStand;
            int firstCard = GetRandomCard();
            int nextCard;
            totalCardValue += firstCard;

            ShowColoredText($"You drew a {firstCard}. Your total is now {totalCardValue}", ConsoleColor.Green, true);

            do
            {
                ShowColoredText("Do you want to hit or stand? (H/S)", ConsoleColor.Magenta);
                hitOrStand = char.ToUpper(Console.ReadKey(true).KeyChar);
                Console.WriteLine();

                if (hitOrStand == 'H')
                {
                    nextCard = GetRandomCard();
                    totalCardValue += nextCard;
                    ShowColoredText($"You drew a {nextCard}. Your total is now {totalCardValue}", ConsoleColor.Green, true);

                    if ( totalCardValue > 21)
                    {
                        ShowColoredText($"You drew a {nextCard}. You busted!", ConsoleColor.Yellow, true);
                    }
                }
                else if (hitOrStand == 'S')
                {
                    ShowColoredText($"You chose to stand. Your total remains {totalCardValue}", ConsoleColor.DarkGreen, true);
                }
                else if (hitOrStand != 'H' && hitOrStand != 'S')
                {
                    ShowColoredText("Invalid input, please enter H or S.", ConsoleColor.Red, true);
                }
            } while (hitOrStand != 'S' && totalCardValue < 21);

            return totalCardValue;

        }

        static void PlayNextGame()
        {
            int valuePlayer = DealPlayerCards();

            if (valuePlayer > 21)
            {
                ShowColoredText("This game has come to an end: ", ConsoleColor.Cyan);
                ShowColoredText("Dealer wins!", ConsoleColor.Yellow, true);
                ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue);
                Console.ReadLine();
                Console.Clear();
                ShowTitle();
                ShowMenu();


            }
            else if (valuePlayer == 21)
            {
                ShowColoredText("B L A C K J A C K: ", ConsoleColor.Cyan);
                ShowColoredText("You win!", ConsoleColor.DarkGreen, true);
                ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue);
                Console.ReadLine();
                Console.Clear();
                ShowTitle();
                ShowMenu();

            }
            else
            {
                int valueComputer = DealComputerCards();

                if (valuePlayer > valueComputer || valueComputer > 21)
                {
                    ShowColoredText("This game has come to an end: ", ConsoleColor.Cyan);
                    ShowColoredText("You win!", ConsoleColor.DarkGreen, true);
                    ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue);
                    Console.ReadLine();
                    Console.Clear();
                    ShowTitle();
                    ShowMenu();

                }
                else if (valuePlayer == valueComputer)
                {
                    ShowColoredText("It's a tie. You are both losers!", ConsoleColor.DarkMagenta, true);
                    ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue);
                    Console.ReadLine();
                    Console.Clear();
                    ShowTitle();
                    ShowMenu();
                }
                else
                {
                    ShowColoredText("This game has come to an end: ", ConsoleColor.Cyan);
                    ShowColoredText("Dealer wins!", ConsoleColor.Yellow, true);
                    ShowColoredText("Press ENTER to continue...", ConsoleColor.Blue);
                    Console.ReadLine();
                    Console.Clear();
                    ShowTitle();
                    ShowMenu();
                }
            }
        }

        static void ShowColoredText(string text, ConsoleColor color, bool appendLine = false)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            if (appendLine)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
