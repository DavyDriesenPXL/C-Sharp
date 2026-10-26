using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Week_5
{
    internal class Labo
    {
        public static void EscapeRoom()
        {
            ShowTitle();
            ShowInfo();
            StartGame();

            static void ShowTitle()
            {
                Console.Clear();
                Console.WriteLine(@"
 ___                         ___
| __|___ __ __ _ _ __  ___  | _ \___  ___ _ __
| _|(_-</ _/ _` | '_ \/ -_) |   / _ \/ _ \ '  \
|___/__/\__\__,_| .__/\___| |_|_\___/\___/_|_|_|
                |_|
================================================
");
            }

            static void ShowInfo()
            {
                Console.WriteLine("Solve the puzzles before time runs out!");
                Console.WriteLine("Press enter to start your adventure...");
                Console.ReadLine();
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

            static void ShowStatus(int timeLeft, bool keypadSolved, bool riddleSolved)
            {
                ShowColoredText($"Time left: {timeLeft} minutes", ConsoleColor.Blue, true);
                ShowColoredText($"Keypad: {(keypadSolved ? "solved" : "unsolved")}", ConsoleColor.Blue, true);
                ShowColoredText($"Riddle: {(riddleSolved ? "solved" : "unsolved")}", ConsoleColor.Blue, true);
                Console.WriteLine();
            }

            static void ShowMenu()
            {
                ShowColoredText("Choose an action:", ConsoleColor.White, true);
                ShowColoredText("  1) Try keypad", ConsoleColor.White, true);
                ShowColoredText("  2) Solve riddle", ConsoleColor.White, true);
                ShowColoredText("  3) Open final door", ConsoleColor.White, true);
                ShowColoredText("  9) Give up", ConsoleColor.White, true);
                ShowColoredText("Your choice: ", ConsoleColor.White, false);
            }

            static int ReadAction()
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    return choice;
                }
                return -1;

            }

            static int ApplyPenalty(int timeLeft, int penalty)
            {
                int newTime = timeLeft - penalty;
                if (newTime < 0)
                {
                    newTime = 0;
                }
                ShowColoredText($"Time penalty: -{penalty} minute(s).", ConsoleColor.DarkYellow, true);
                return newTime;
            }

            static bool TryKeyPad()
            {
                ShowColoredText("Enter 3-digit keypad code: ", ConsoleColor.Magenta);
                string input = Console.ReadLine();

                if (input == "314")
                {
                    ShowColoredText("Keypad unlocked!", ConsoleColor.Green, true);
                    return true;
                }
                else if (!int.TryParse(input, out int numericInput))
                {
                    ShowColoredText("Invalid code (not a number).", ConsoleColor.DarkRed, true);
                    return false;
                }
                else
                {
                    ShowColoredText("Wrong code.", ConsoleColor.Red, true);

                    return false;
                }
            }

            static void StartGame()
            {
                int timeLeft = 15;
                bool keypadSolved = false;
                bool riddleSolved = false;
                bool escaped = false;
                int action;
                int penalty = 2;

                do
                {
                    ShowTitle();
                    ShowStatus(timeLeft, keypadSolved, riddleSolved);
                    ShowMenu();
                    action = ReadAction();
                    switch(action)
                    {
                        case 1:
                            keypadSolved = TryKeyPad();

                            if(keypadSolved == false)
                            {
                                timeLeft = ApplyPenalty(timeLeft, penalty);
                            }
                            break;
                    }
                    Console.ReadLine();
                } while (action != 9);
            }
        }
    }
}
