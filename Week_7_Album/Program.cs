using System.Text;

namespace Week_7_Album
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Album album = new();
            StringBuilder sb = new();
            bool isCorrect;

            ShowText("*** Favoriete album ***", ConsoleColor.Cyan);
            album.Name = ReadLine("Wat is de naam van je favoriete album?");

            do
            {
                isCorrect = DateTime.TryParse(ReadLine("Op welke datum werd het uitgebracht?"), out DateTime releaseDate);
                if (!isCorrect)
                {
                    ShowText("Geef een geldige datum in.", ConsoleColor.Red);
                }
                else
                {
                    album.ReleaseDate = releaseDate;
                }
            } while (!isCorrect);

            do
            {
                isCorrect = int.TryParse(ReadLine("Hoeveel nummers staan er op het album?"), out int songAmount);
                if (!isCorrect)
                {
                    ShowText("Geef een geldig getal in.", ConsoleColor.Red);
                }
                else
                {
                    album.Songs = songAmount;
                }
            } while (!isCorrect);

            do
            {
                isCorrect = int.TryParse(ReadLine("Hoe lang duurt het album in totaal? (minuten)"), out int minutes);
                if (!isCorrect)
                {
                    ShowText("Geef een geldig getal in.", ConsoleColor.Red);
                }
                else
                {
                    album.Duration = TimeSpan.FromMinutes(minutes);
                }
            } while (!isCorrect);

            sb.AppendLine($"{"Naam",-25}: {album.Name}");
            sb.AppendLine($"{"Uitgebracht",-25}: {album.ReleaseDate.ToLongDateString()}");
            sb.AppendLine($"{"Aantal nummers",-25}: {album.Songs}");
            sb.AppendLine($"{"Totale duur",-25}: {album.Duration.Hours} uur {album.Duration.Minutes} minuten");
            Console.WriteLine("-------------------------------------------------\n");

            ShowText(sb.ToString(), ConsoleColor.Green);
        }

        static void ShowText(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static string ReadLine(string question, ConsoleColor color = ConsoleColor.Magenta)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(question);
            Console.ForegroundColor = color;
            string text = Console.ReadLine();
            Console.ResetColor();

            return text;
        }

    }
}
