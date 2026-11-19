using System;
using System.Collections.Generic;
using System.Text;

namespace TheEternalDark
{
    public class Player
    {
        public string Name { get; set; }
        public int Wood { get; set; }
        public int Iron { get; set; }
        public int Crystal { get; set; }
        public int Stone { get; set; }
        public int Location { get; set; }
        public bool SunBuilt { get; set; }

        private const int MoveStep = 5;
        public const int SunCostWood = 40;
        public const int SunCostStone = 40;
        public const int SunCostIron = 25;
        public const int SunCostCrystal = 20;

        public Player(string name)
        {
            Name = name;
            Wood = 0;
            Crystal = 0;
            Iron = 0;
            Stone = 0;
            Location = 0;
            SunBuilt = false;
        }

        public string MoveLeft(World world)
        {
            int targetLocation = Location - MoveStep;

            if (world.BiomeCodeAt(targetLocation) == 'd')
            {
                return "De zee blokkeert de weg.";
            }
            else
            {
                Location = targetLocation;
                return "Je trekt verder naar het westen ...";
            }
        }

        public string MoveRight(World world)
        {
            int targetLocation = Location + MoveStep;

            if (world.BiomeCodeAt(targetLocation) == 'd')
            {
                return "De zee blokkeert de weg.";
            }
            else
            {
                Location = targetLocation;
                return "Je trekt verder naar het oosten ...";
            }
        }

        public int StepsToReturnToCamp()
        {
            return Math.Abs(Location) / MoveStep;
        }

        public string ReturnToCamp()
        {
            if (Location == 0)
            {
                return "Je bent al in het kamp.";
            }

            int steps = StepsToReturnToCamp();
            Location = 0;
            return $"Je reis naar het kamp duurde {steps} dagen.";
        }

        public string BuildSun()
        {
            if (SunBuilt)
            {
                return "De zon schijnt al!";
            }
            else if (Location != 0)
            {
                return "Keer eerst terug naar het kamp.";
            }
            else if (Wood >= SunCostWood && Iron >= SunCostIron && Crystal >= SunCostCrystal && Stone >= SunCostStone)
            {
                Wood -= SunCostWood;
                Iron -= SunCostIron;
                Crystal -= SunCostCrystal;
                Stone -= SunCostStone;
                SunBuilt = true;

                return "Je hebt de zon heropgebouwd!";
            }
            else
            {
                int woodNeeded = SunCostWood - Wood;
                int ironNeeded = SunCostIron - Iron;
                int crystalNeeded = SunCostCrystal - Crystal;
                int stoneNeeded = SunCostStone - Stone;

                return $"Je hebt de volgende grondstoffen nog nodig:\nWood: {Math.Max(woodNeeded, 0)}\nIron: {Math.Max(ironNeeded, 0)}\nCrystal: {Math.Max(crystalNeeded, 0)}\nStone: {Math.Max(stoneNeeded, 0)}";
            }
        }
    }
}
