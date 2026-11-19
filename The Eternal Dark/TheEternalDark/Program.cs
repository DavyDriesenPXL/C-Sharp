// This is the whole game loop. It only ever talks to World, Player, Biome,
// and Display - it never prints anything itself and never touches how the
// scenes are actually drawn. That part lives in Display.cs / WorldArt.cs.
using TheEternalDark;

public class Program
{
    public static void Main(string[] args)
    {
        Display.Initialize();
        Display.ShowIntroLore();

        string name = Display.ShowWelcomeAndAskName();
        World world = new World(name);

        bool running = true;

        while (running)
        {
            Display.ShowStats(world);

            int choice = Display.ShowActionMenu();
            bool dayPassed = false;

            if (choice == 1)
            {
                Biome biome = new Biome(world.BiomeCodeAt(world.Player.Location));
                string result = biome.Gather(world.Player);
                Display.ShowMessage(result);
                dayPassed = true;
            }
            else if (choice == 2)
            {
                string result = world.Player.MoveLeft(world);
                Display.ShowMessage(result);
                dayPassed = true;
            }
            else if (choice == 3)
            {
                string result = world.Player.MoveRight(world);
                Display.ShowMessage(result);
                dayPassed = true;
            }
            else if (choice == 4)
            {
                string result = world.Player.BuildSun();
                Display.ShowMessage(result);
                dayPassed = true;
            }
            else if (choice == 5)
            {
                int daysForTrip = world.Player.StepsToReturnToCamp();
                string result = world.Player.ReturnToCamp();
                Display.ShowMessage(result);

                for (int day = 0; day < daysForTrip; day++)
                {
                    world.ProcessDay();
                }
            }
            else if (choice == 0)
            {
                running = false;
            }
            else
            {
                Display.ShowMessage("That is not a valid choice.");
            }

            if (running && dayPassed)
            {
                world.ProcessDay();
            }

            if (running && world.Player.SunBuilt)
            {
                Display.ShowWin(world);
                running = false;
            }
        }

        Display.ShowGoodbye();
    }
}
