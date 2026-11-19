// Every Console.Write/ReadLine in the whole project lives in this file (and
// in WorldArt.cs, which this file calls into for the actual scene drawing).
// Program.cs and the domain classes never touch Console directly - when the
// visual layer gets upgraded later, only this file (and WorldArt.cs) change.
using TheEternalDark;

public static class Display
{
    // ConsoleFont.cs swaps in a font that can show WorldArt's braille
    // glyphs - you don't need to look inside it.
    public static void Initialize()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ConsoleFont.TryUseBrailleCapableFont();
    }

    // Console.Clear() throws if there's no real console buffer behind
    // stdout (piped/redirected output, some CI or IDE terminals). Fall back
    // to printing some blank lines so the game still runs there.
    private static void ClearScreen()
    {
        try
        {
            Console.Clear();
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine(new string('\n', 20));
        }
    }

    public static void ShowIntroLore()
    {
        WorldArt.PlayIntro();
        ClearScreen();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== THE ETERNAL DARK ===");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("The dwarf wakes on cold ground and opens his eyes to... nothing.");
        Console.WriteLine("No dawn. No glow on the horizon. The sky is simply empty.");
        Console.WriteLine("The sun is gone, and the night has gone on long enough to feel eternal.");
        Console.WriteLine();
        Console.WriteLine("There is only one thing left to do: gather what the land still offers,");
        Console.WriteLine("and build a new sun with his own two hands.");
        Console.WriteLine();
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine();
    }

    public static string ShowWelcomeAndAskName()
    {
        ClearScreen();
        Console.Write("What is your dwarf's name? ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Grum";
        }

        return name;
    }

    private static string BiomeName(char biomeCode)
    {
        switch (biomeCode)
        {
            case 'b':
                return "Forest";
            case 'c':
                return "Mountain";
            case 'd':
                return "Sea";
            case 'e':
                return "Crystal Peaks";
            case 'f':
                return "Desert";
            case 'g':
                return "Crimson Forest";
            case 'h':
                return "Radioactive Fields";
            case 'i':
                return "Purple Crystal Ravines";
            default:
                return "Camp";
        }
    }

    private static string DistanceFromCampText(int location)
    {
        if (location == 0)
        {
            return "At camp";
        }

        if (location < 0)
        {
            return -location + " west of camp";
        }

        return location + " east of camp";
    }

    public static void ShowStats(World world)
    {
        ClearScreen();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== THE ETERNAL DARK === Day " + world.DaysWithoutSun + " without a sun ===");
        Console.ResetColor();

        WorldArt.RenderSurface(world);

        char biomeCode = world.BiomeCodeAt(world.Player.Location);
        Player player = world.Player;

        Console.WriteLine();
        Console.Write(player.Name + " | Standing in: ");
        Console.ForegroundColor = WorldArt.ColorForBiome(biomeCode);
        Console.Write(BiomeName(biomeCode));
        Console.ResetColor();
        Console.WriteLine(" | " + DistanceFromCampText(player.Location));
        Console.WriteLine("Wood: " + player.Wood + "  Stone: " + player.Stone + "  Iron: " + player.Iron
            + "  Crystal: " + player.Crystal);

        Console.WriteLine();
    }

    // Returns the chosen number, or 0 for Quit (typed as "X" rather than
    // a number, since it isn't just another action).
    public static int ShowActionMenu()
    {
        Console.WriteLine("What do you do?");
        Console.WriteLine("1. Gather");
        Console.WriteLine("2. Move west");
        Console.WriteLine("3. Move east");
        Console.WriteLine("4. Build the Sun");
        Console.WriteLine("5. Return to camp");
        Console.WriteLine("X. Quit");
        Console.Write("> ");
        string input = Console.ReadLine();

        if (input != null && input.Trim().ToUpper() == "X")
        {
            return 0;
        }

        return ParseChoice(input);
    }

    // Shared by every menu that reads a plain numeric choice: valid number
    // in, that number out; anything else (blank, letters, garbage) becomes
    // -1 so callers can treat it as "not a valid option".
    private static int ParseChoice(string input)
    {
        int choice;
        bool success = int.TryParse(input, out choice);

        if (success)
        {
            return choice;
        }

        return -1;
    }

    public static void ShowMessage(string message)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    // A handful of frames with the sun climbing higher over a fixed
    // horizon, each held briefly - the payoff for the whole game before
    // settling on the final win text.
    private static void AnimateSunrise()
    {
        string horizon = new string('_', 44);

        string[] frames =
        {
            "\n\n\n\n\n" + horizon,
            "\n\n\n\n            .-.\n" + horizon,
            "\n\n         \\ | /\n        --(o)--\n" + horizon,
            "\n     \\   |   /\n      \\  |  /\n    ---( O )---\n      /  |  \\\n     /   |   \\\n" + horizon,
        };

        foreach (string frame in frames)
        {
            ClearScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(frame);
            Console.ResetColor();
            System.Threading.Thread.Sleep(450);
        }
    }

    public static void ShowWin(World world)
    {
        AnimateSunrise();
        ClearScreen();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== A NEW SUN RISES ===");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("The last piece slides into place, and light floods the land for the");
        Console.WriteLine("first time in " + world.DaysWithoutSun + " days. " + world.Player.Name + " shields his eyes, then laughs -");
        Console.WriteLine("a real, disbelieving laugh. The eternal dark is over.");
    }

    public static void ShowGoodbye()
    {
        Console.WriteLine();
        Console.WriteLine("Thanks for playing The Eternal Dark!");
    }
}
