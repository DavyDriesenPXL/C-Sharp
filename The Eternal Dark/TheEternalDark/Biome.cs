using System;
using System.Collections.Generic;
using System.Text;

namespace TheEternalDark
{
    public class Biome
    {
        public char Code { get; set; }
        private Random _rnd;

        public Biome(char code)
        {
            Code = code;
            _rnd = new();
        }

        private int RollAmount()
        {
            return _rnd.Next(3, 7);
        }

        public string Gather(Player player)
        {
            int resourceGained;

            switch (Code)
            {
                case 'a':
                    return "In het kamp valt niets te verzamelen";
                case 'b':
                    resourceGained = RollAmount();
                    player.Wood += resourceGained;
                    return $"Je hebt {resourceGained} wood verzameld!";
                case 'c':
                    resourceGained = RollAmount();
                    player.Stone += resourceGained;
                    int secondResourceGained = RollAmount() / 2;
                    player.Iron += secondResourceGained;
                    return $"Je hebt {resourceGained} stone en {secondResourceGained} iron verzameld!";
                case 'd':
                case 'e':
                    resourceGained = RollAmount();
                    player.Crystal += resourceGained;
                    return $"Je hebt {resourceGained} crystal verzameld!";
                case 'f':
                    resourceGained = RollAmount();
                    player.Stone += resourceGained;
                    return $"Je hebt {resourceGained} stone verzameld!";
                default:
                    return "Niks te verzamelen.";

            }
        }
    }
}
