// The whole game world. Write the rest of this class yourself, following
// the WORLD tickets in README.md.
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TheEternalDark;

public class World
{
    // ===== GIVEN CODE: do not change =====
    // Every location maps onto one BiomeSeed character, CharsPerBiome
    // steps wide, with camp ('a') at location 0. The shift is added before
    // dividing so the math works the same for locations west of camp
    // (negative) as it does east. Anything beyond the string is endless sea.
    public char BiomeCodeAt(int location)
    {
        int campIndex = BiomeSeed.IndexOf('a');
        int shifted = location + CharsPerBiome / 2 + CharsPerBiome * campIndex;
        int index = shifted / CharsPerBiome;

        if (index < 0 || index >= BiomeSeed.Length)
        {
            return 'd';
        }

        return BiomeSeed[index];
    }
    // ===== END OF GIVEN CODE =====

    public string BiomeSeed { get; set; }
    public Player Player { get; set; }
    public int DaysWithoutSun { get; set; }

    public const int CharsPerBiome = 20;

    private Random _rnd;

    public World(string playerName)
    {
        _rnd = new();
        BiomeSeed = GenerateBiomeSeed();
        Player = new(playerName);
        DaysWithoutSun = 0;
    }

    private string GenerateBiomeSeed()
    {
        string pool = "bcef";
        string west = "";
        string east = "";

        for (int i = 0; i < 2; i++)
        {
            int index = _rnd.Next(pool.Length);
            west += pool[index];
            pool = pool.Remove(index,1);
        }

        for (int i = 0; i < 2; i++)
        {
            int index = _rnd.Next(pool.Length);
            east += pool[index];
            pool = pool.Remove(index,1);
        }

        return $"d{west}a{east}d";
    }

    public void ProcessDay()
    {
        DaysWithoutSun++;
    }
}
