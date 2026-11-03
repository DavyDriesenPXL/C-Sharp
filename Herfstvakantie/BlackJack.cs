namespace Week_Herfstvakantie
{
    internal class BlackJack
    {

        public static void Run()
        {
            char input;

            do
            {
                ShowText("B L A C K J A C K\n\nWelcome to the table!\n-------------------------------", ConsoleColor.DarkBlue);
                ShowText("Are you feeling lucky?", ConsoleColor.DarkMagenta);
                ShowText(" 1. Hell yeah! Lets play!\n 2. Nah, maybe later.", ConsoleColor.Yellow);
                input = Console.ReadKey(true).KeyChar;

                if (input != '1' && input != '2')
                {
                    ShowText("Invalid choice, please try again.", ConsoleColor.Red);
                    ShowText("Press ENTER to continue ...", ConsoleColor.DarkBlue);
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (input == '1')
                {
                    ShowText("Your turn, press ENTER to draw a card ...", ConsoleColor.Cyan);
                    Console.ReadLine();
                    PlayNextGame();
                }

                else if (input == '2')
                {
                    ShowText("See you next time!\nThank you for playing!\nB L A C K J A C K", ConsoleColor.DarkBlue);
                }
            } while (input != '2');
        }

        static void PlayNextGame()
        {
            int playerScore = DealPlayerCards();

            if (playerScore > 21)
            {
                EndGame("dealer");
            }

            else
            {
                ShowText("Dealers turn, press ENTER to start ...", ConsoleColor.Cyan, false);
                Console.ReadLine();
                int dealerScore = DealComputerCards();

                if (playerScore > dealerScore || dealerScore > 21)
                {
                    EndGame("player");

                }
                else if (playerScore == dealerScore)
                {
                    EndGame();
                }
                else
                {
                    EndGame("dealer");
                }
            }
        }

        static int DealPlayerCards()
        {
            int total = 0;
            char input;

            int card = GetRandomCard();
            total += card;
            ShowText($"You drew a {card}. Your total is now {total}", ConsoleColor.Green);

            do
            {
                ShowText("Do you want to hit or stand? (H/S)", ConsoleColor.Magenta);
                input = char.ToUpper(Console.ReadKey(true).KeyChar);

                if (input == 'H')
                {
                    card = GetRandomCard();
                    total += card;

                    if (total > 21)
                    {
                        ShowText($"You drew a {card}. You busted!", ConsoleColor.DarkYellow);
                        break;
                    }
                    else
                    {
                        ShowText($"You drew a {card}. Your total is now {total}", ConsoleColor.Green);
                    }

                }
                else if (input == 'S')
                {
                    ShowText($"You chose to stand. Your total remains {total}", ConsoleColor.DarkGreen);
                }

                else
                {
                    ShowText("Invalid input, please enter H or S", ConsoleColor.Red);
                }

            } while (input != 'S' && total < 21);

            return total;
        }

        static int DealComputerCards()
        {
            int total = 0;
            int card;

            do
            {
                card = GetRandomCard();
                total += card;

                ShowText($"Dealer drew a {card}. Dealers total is now {total}", ConsoleColor.Green);
            } while (total < 16);

            return total;
        }

        static int GetRandomCard()
        {
            Random random = new();
            int card = random.Next(2, 12);

            return card;
        }

        static void ShowText(string text, ConsoleColor color, bool newline = true)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            if (newline)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void EndGame(string winner = "")
        {
            ShowText("This game has come to an end: ", ConsoleColor.Cyan, false);
            if (winner.Equals("dealer"))
            {
                ShowText("Dealer wins.", ConsoleColor.Yellow);
            }
            else if (winner.Equals("player"))
            {
                ShowText("You win!", ConsoleColor.Green);
            }
            else
            {
                ShowText("Nobody wins.", ConsoleColor.DarkGray);
            }

            ShowText("Press ENTER to continue...", ConsoleColor.DarkBlue);
            Console.ReadLine();
            Console.Clear();
        }
    }
}
