// This file is the "advanced techniques" corner of the project - deliberately
// kept out of the students' way. It draws the surface scene using braille
// Unicode characters (U+2800 and up) packed 2x4 dots per character,
// generated on the fly from World's biome layout instead of any stored
// map, so the same world always draws the same picture. At the bottom of the
// file, PlayIntro draws the opening cutscene (the exploding sun and the
// title) with the same braille-dot technique.
internal static class WorldArt
{
    // Used only if the real console window size can't be read (e.g.
    // redirected output).
    private const int DefaultSceneWidth = 50;
    private const int MinSceneWidth = 30;

    // The scene never grows to fill the whole window: ShowStats prints the
    // title above it and the biome/resource lines below it, then
    // ShowActionMenu prints its options right after that on the same
    // screen (no clear in between) - without reserving room for all of
    // that, a tall scene pushes its own top rows off screen the moment
    // that text is printed. 20 is a floor regardless of window size, per
    // the "don't let it get cramped either" side of the same tradeoff.
    private const int MinSceneHeight = 20;
    private const int ReservedRows = 15;

    // How many subrows below the baseline the sea's flat surface sits.
    private const int WaterLevelOffset = 3;

    // How many screen columns one world/biome step covers. Without this,
    // a step (and so a biome) would be exactly one character wide on
    // screen - too thin to look like anything. At 10, a 50-wide biome
    // spans 500 screen columns, plenty of room to blend smoothly into its
    // neighbors instead of cutting off sharply.
    private const int VisualScale = 10;

    private static int GetSceneWidth()
    {
        try
        {
            int width = Console.WindowWidth - 2;

            if (width < MinSceneWidth)
            {
                return DefaultSceneWidth;
            }

            return width;
        }
        catch
        {
            return DefaultSceneWidth;
        }
    }

    private static int GetSceneHeight()
    {
        try
        {
            int height = Console.WindowHeight - ReservedRows;

            if (height < MinSceneHeight)
            {
                return MinSceneHeight;
            }

            return height;
        }
        catch
        {
            return MinSceneHeight;
        }
    }

    private static int Hash(int seed, int x, int y)
    {
        unchecked
        {
            int h = seed * 374761393 + x * 668265263 + y * 2147483647;
            h = (h ^ (h >> 13)) * 1274126177;
            return h ^ (h >> 16);
        }
    }

    // Same as Hash, but guaranteed non-negative (avoids Math.Abs overflowing
    // on int.MinValue) so it's safe to use directly with '%'.
    private static int PositiveHash(int seed, int x, int y)
    {
        return Hash(seed, x, y) & 0x7FFFFFFF;
    }

    private static char BraillePixel(bool d1, bool d2, bool d3, bool d4, bool d5, bool d6, bool d7, bool d8)
    {
        int mask = 0;
        if (d1) mask |= 1 << 0;
        if (d2) mask |= 1 << 1;
        if (d3) mask |= 1 << 2;
        if (d4) mask |= 1 << 3;
        if (d5) mask |= 1 << 4;
        if (d6) mask |= 1 << 5;
        if (d7) mask |= 1 << 6;
        if (d8) mask |= 1 << 7;
        return (char)(0x2800 + mask);
    }

    // Picked once per run, so the scenery stays the same for the whole
    // game but looks different every time the game is started. This is a
    // purely visual choice, so it lives here instead of in World.
    private static readonly int VisualSeed = new Random().Next();

    // The location exactly at the middle of the biome that `location` falls
    // in - e.g. camp's own biome is centered on 0, its neighbors on -20 and
    // 20. Used to blend terrain smoothly between a biome and its neighbors:
    // each biome's shape is strongest right at its own center and fades
    // out toward the edges, rather than cutting off sharply.
    private static int ChunkCenterAt(int location)
    {
        double chunk = Math.Floor((location + World.CharsPerBiome / 2.0) / World.CharsPerBiome);
        return (int)chunk * World.CharsPerBiome;
    }

    // Raises a wave value (already in roughly [-1, 1]) to a fractional
    // power while keeping its sign, so instead of a smooth sine curve it
    // reads as a sharp, pointed spike - used for the jagged biomes.
    private static double Sharpen(double wave, double sharpness)
    {
        double sign = wave < 0 ? -1.0 : 1.0;
        return sign * Math.Pow(Math.Abs(wave), sharpness);
    }

    // The single sinoid that defines the terrain's main flow, everywhere -
    // the same rolling-hills shape the original surface used before biomes
    // existed. This never switches off or gets replaced; every biome just
    // adds its own character on top of it, which is what keeps the terrain
    // reading as one continuous landscape instead of a patchwork of
    // unrelated shapes stitched together.
    private static double MainFlow(int seed, double x)
    {
        double a = Math.Sin((x + seed) * 0.15) * 3;
        double b = Math.Sin((x + seed) * 0.05) * 6;
        return a + b;
    }

    // What a biome adds to (or, for the low/sunken ones, subtracts from)
    // the main flow within its own territory: a small handful of sine
    // terms, sharpened for the jagged biomes. Camp adds nothing - it's the
    // calm, unmodified backbone shape.
    private static double BiomeModifier(int seed, char biome, double x)
    {
        switch (biome)
        {
            case 'c': // Mountain - short-period, high-amplitude peaks. The
                      // old frequencies (0.08/0.2) had periods of ~78 and
                      // ~31 units - inside a biome only CharsPerBiome (20)
                      // wide, that's at most a quarter of one wave, which
                      // reads as a single gentle rise rather than a range
                      // of summits. Much higher frequencies fit several
                      // peaks inside the territory instead, and a moderate
                      // Sharpen (well short of Crystal Peaks' needle-thin
                      // one) keeps each peak near its own top for longer.
                return Sharpen(Math.Sin((x + seed) * 0.35), 0.5) * 28 + Sharpen(Math.Sin((x + seed) * 0.9), 0.45) * 14;
            case 'e': // Crystal Peaks - tall, sharply pointed hills
                return Sharpen(Math.Sin((x + seed) * 0.3), 0.16) * 22 + Sharpen(Math.Sin((x + seed) * 0.7), 0.14) * 8;
            case 'i': // Purple Crystal Ravines - sharp gorges pulling the flow down
                return -Sharpen(Math.Sin((x + seed) * 0.13), 0.25) * 12;
            case 'd': // Sea - pulls the flow down below sea level and calms it
                return -18 + Math.Sin((x + seed) * 0.03);
            case 'h': // Radioactive Fields - erratic sharp blips
                return Sharpen(Math.Sin((x + seed) * 0.5), 0.15) * 3;
            case 'b': // Forest - a bit more rolling texture
                return Math.Sin((x + seed) * 0.09) * 3;
            case 'g': // Crimson Forest - similar to forest, a touch taller
                return Math.Sin((x + seed) * 0.09) * 4 + 1;
            case 'f': // Desert - subtracts, flattening the dunes down low
                return -4 - Math.Sin((x + seed) * 0.2);
            default: // Camp - no modifier at all
                return 0.0;
        }
    }

    // How much of a biome's own territory (as a fraction of CharsPerBiome)
    // stays at full strength before tapering off, so its modifier is
    // actually visible across most of its space and only fades right near
    // the edge, where it meets its neighbor.
    private const double EnvelopePlateau = 0.4;

    private static double Envelope(double distance)
    {
        double plateauWidth = World.CharsPerBiome * EnvelopePlateau;

        if (distance <= plateauWidth)
        {
            return 1.0;
        }

        if (distance >= World.CharsPerBiome)
        {
            return 0.0;
        }

        double t = (distance - plateauWidth) / (World.CharsPerBiome - plateauWidth);
        return (Math.Cos(t * Math.PI) + 1.0) / 2.0;
    }

    // The main flow is always present. Any nearby biome (current chunk
    // plus its two neighbors) layers its own modifier on top, scaled by
    // how close this point is to that biome's own center - so a biome's
    // character grows in as you approach it and fades back into the plain
    // flow as you leave, rather than the terrain switching wholesale from
    // one shape to a completely different one at a boundary.
    private static double TerrainHeightAt(World world, int visualSeed, double dotLocationX, int locationInt)
    {
        double height = MainFlow(visualSeed, dotLocationX);
        int centerLocation = ChunkCenterAt(locationInt);

        for (int offset = -1; offset <= 1; offset++)
        {
            int candidateCenter = centerLocation + offset * World.CharsPerBiome;
            double distance = Math.Abs(dotLocationX - candidateCenter);
            double envelope = Envelope(distance);

            if (envelope > 0.0)
            {
                char candidateBiome = world.BiomeCodeAt(candidateCenter);
                height += envelope * BiomeModifier(visualSeed, candidateBiome, dotLocationX);
            }
        }

        return height;
    }

    // Dry land is clamped to never sink at or past the flat water line -
    // without that, a biome that dips low (which never floods, see
    // IsFlooded below) could render lower on screen than the sea sits
    // elsewhere in view, making the water look like it's floating above
    // lower ground. Sea columns are exempt: sinking past the line there is
    // what makes them flood in the first place.
    //
    // Ravines are a special case: they never flood either (IsFlooded only
    // ever checks for 'd'), and unlike every other dry biome their whole
    // point is to dip well below the usual ground line, so they're exempt
    // from the sea-level clamp too. But left fully unclamped, a deep
    // enough dip could push the ground row past the bottom of the visible
    // screen entirely, leaving the whole column blank instead of purple -
    // so ravines get their own floor instead: never deeper than the
    // scene's actual last subrow (recovered from groundBaseSubRow, since
    // it's always exactly two thirds of the way down the scene), which
    // still lets a dip look dramatically deep without ever losing its
    // fill before it reaches the bottom of the screen.
    private static int GroundRowAt(World world, int visualSeed, double dotLocationX, int locationInt, int groundBaseSubRow, int waterTopSubRow)
    {
        int row = groundBaseSubRow - (int)Math.Round(TerrainHeightAt(world, visualSeed, dotLocationX, locationInt));
        char biome = world.BiomeCodeAt(locationInt);

        if (biome == 'd')
        {
            return row;
        }

        if (biome == 'i')
        {
            int lastVisibleSubRow = groundBaseSubRow * 3 / 2 - 1;
            return Math.Min(row, lastVisibleSubRow);
        }

        return Math.Min(row, waterTopSubRow - 1);
    }

    // Only Sea columns are even eligible to flood, so an ordinary mountain
    // or forest valley never turns into a stray pond no matter how deep the
    // terrain noise happens to dip there. Within a Sea biome, a column
    // counts as flooded once its own ground has sunk to or past the flat
    // water line.
    private static bool IsFlooded(World world, int visualSeed, int locationInt, int groundBaseSubRow, int waterTopSubRow)
    {
        if (world.BiomeCodeAt(locationInt) != 'd')
        {
            return false;
        }

        return GroundRowAt(world, visualSeed, locationInt, locationInt, groundBaseSubRow, waterTopSubRow) >= waterTopSubRow;
    }

    private static int CountDots(bool d1, bool d2, bool d3, bool d4, bool d5, bool d6, bool d7, bool d8)
    {
        int dotsOn = 0;
        if (d1) dotsOn++;
        if (d2) dotsOn++;
        if (d3) dotsOn++;
        if (d4) dotsOn++;
        if (d5) dotsOn++;
        if (d6) dotsOn++;
        if (d7) dotsOn++;
        if (d8) dotsOn++;
        return dotsOn;
    }

    // Decoration placement is keyed off the screen column, not the (much
    // coarser) location, so trees/sparkles/glints stay scattered at normal
    // screen resolution even though a single biome now spans many columns.
    private static bool IsTree(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 777) % 60 == 0;
    }

    // A proper forest, not just a sprinkling - roughly every other column
    // gets a tree.
    private static bool IsDenseTree(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 777) % 2 == 0;
    }

    private static bool IsSparkle(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 888) % 50 == 0;
    }

    private static bool IsGlint(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 999) % 70 == 0;
    }

    private static bool IsRock(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 333) % 45 == 0;
    }

    private static bool IsCactus(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 111) % 10 == 0;
    }

    // The eternal-dark sky: mostly empty, with rare bright stars and more
    // common dim ones, so it reads as a real night sky rather than the
    // occasional stray dot.
    private static void RenderSkyCell(int visualSeed, char[,] glyphs, ConsoleColor[,] colors, int screenCol, int cx, int cy)
    {
        int roll = PositiveHash(visualSeed, screenCol, cy) % 180;

        if (roll == 0)
        {
            glyphs[cy, cx] = '*';
            colors[cy, cx] = ConsoleColor.White;
        }
        else if (roll <= 7)
        {
            glyphs[cy, cx] = '.';
            colors[cy, cx] = ConsoleColor.Gray;
        }
        else
        {
            glyphs[cy, cx] = ' ';
        }
    }

    // The character row a decoration or the player should stand on: one
    // row above wherever the ground silhouette's top surface lands.
    private static int StandingRowAt(World world, int visualSeed, double dotLocationX, int locationInt, int groundBaseSubRow, int waterTopSubRow)
    {
        return GroundRowAt(world, visualSeed, dotLocationX, locationInt, groundBaseSubRow, waterTopSubRow) / 4 - 1;
    }

    // Small ASCII sprites for clutter, each a set of rows anchored at its
    // bottom row (the trunk/base touches the ground). A space is a "see
    // through" cell - it leaves whatever's already in the buffer alone
    // instead of overwriting it with blank, so sprites don't need to be
    // rectangular. These are only reachable through StampSprite, which is
    // why multi-cell decorations need a buffer in the first place: a
    // canopy row sits above (so it's drawn to the console earlier than)
    // the ground row that decides whether there's a tree there at all in
    // a single streaming pass - the buffer lets later passes freely
    // overwrite cells an earlier pass already decided.
    private static readonly string[] TreeSmall = { " ♣ ", " | " };
    private static readonly string[] TreeBushy = { "♣♣♣", " | " };
    private static readonly string[] TreeTall = { " ♣ ", " ♣ ", " | " };
    private static readonly string[] RockSingle = { "○" };
    private static readonly string[] RockCluster = { "○○" };
    // A little variety instead of one shape everywhere - box-drawing
    // junction characters (extended ASCII 180/185/195/204) reused as
    // single- and double-line branch arms off the stem - and a spread of
    // heights from a single stub up to a full three-row cactus.
    private static readonly string[] CactusStub = { "|" };
    private static readonly string[] CactusY = { "Y" };
    private static readonly string[] CactusShortLeft = { " ┤ ", " | " };
    private static readonly string[] CactusShortRight = { " ├ ", " | " };
    private static readonly string[] CactusTallLeft = { " | ", " ┤ ", " | " };
    private static readonly string[] CactusTallRight = { " | ", " ├ ", " | " };
    private static readonly string[] CactusTallDoubleLeft = { " | ", " ╣ ", " | " };
    private static readonly string[] CactusTallDoubleRight = { " | ", " ╠ ", " | " };
    private static readonly string[] Tent = { " ▲ ", "/___\\" };
    private static readonly string[] CampfireFlame = { "^" };

    // Rises up and away from the flame below it, drifting side to side
    // rather than climbing in a straight column, so it reads as smoke
    // instead of a rigid stack of dots. Thins out toward the top (','
    // then '.') as it disperses into the sky.
    private static readonly string[] CampfireSmoke =
    {
        "    .",
        "   , ",
        "    ~",
        "  ~  ",
        "   ~ ",
        "    ~",
        "  ~  ",
        " ~   ",
    };

    private static string[] PickTree(int seed, int screenCol)
    {
        int roll = PositiveHash(seed, screenCol, 666) % 3;

        if (roll == 0)
        {
            return TreeSmall;
        }

        if (roll == 1)
        {
            return TreeBushy;
        }

        return TreeTall;
    }

    private static string[] PickRock(int seed, int screenCol)
    {
        return PositiveHash(seed, screenCol, 222) % 2 == 0 ? RockSingle : RockCluster;
    }

    // One variant per roll, in the same order the roll used to check them
    // in. Spans all three heights (1, 2, and 3 characters tall) so a
    // cactus field doesn't all read as the same size.
    private static readonly string[][] CactusVariants =
    {
        CactusStub, CactusY, CactusShortLeft, CactusShortRight,
        CactusTallLeft, CactusTallRight, CactusTallDoubleLeft, CactusTallDoubleRight
    };

    private static string[] PickCactus(int seed, int screenCol)
    {
        int roll = PositiveHash(seed, screenCol, 133) % CactusVariants.Length;
        return CactusVariants[roll];
    }

    // Stamps a sprite into the buffer, bottom row at anchorRow, horizontally
    // centered on anchorCol. Cells that land outside the buffer are simply
    // skipped rather than wrapping or crashing.
    private static void StampSprite(char[,] glyphs, ConsoleColor[,] colors, int sceneWidth, int sceneHeight, string[] sprite, int anchorCol, int anchorRow, ConsoleColor color)
    {
        int height = sprite.Length;

        for (int rowIndex = 0; rowIndex < height; rowIndex++)
        {
            string rowText = sprite[rowIndex];
            int bufferRow = anchorRow - (height - 1) + rowIndex;

            if (bufferRow < 0 || bufferRow >= sceneHeight)
            {
                continue;
            }

            int startCol = anchorCol - rowText.Length / 2;

            for (int colIndex = 0; colIndex < rowText.Length; colIndex++)
            {
                char glyph = rowText[colIndex];

                if (glyph == ' ')
                {
                    continue;
                }

                int bufferCol = startCol + colIndex;

                if (bufferCol < 0 || bufferCol >= sceneWidth)
                {
                    continue;
                }

                glyphs[bufferRow, bufferCol] = glyph;
                colors[bufferRow, bufferCol] = color;
            }
        }
    }

    // Used by Display to color the "Standing in" HUD text the same as the
    // terrain it's naming.
    internal static ConsoleColor ColorForBiome(char biome)
    {
        return biome switch
        {
            'c' => ConsoleColor.Gray,
            'e' => ConsoleColor.Magenta,
            'i' => ConsoleColor.DarkRed,
            'h' => ConsoleColor.Green,
            'g' => ConsoleColor.Red,
            'f' => ConsoleColor.Yellow,
            'd' => ConsoleColor.Cyan,
            _ => ConsoleColor.Green, // Camp, Forest
        };
    }

    private static void RenderGroundCell(World world, int visualSeed, char[,] glyphs, ConsoleColor[,] colors, int screenCol, int cx, int cy, int locationInt, double dotLocationLeft, double dotLocationRight, int groundBaseSubRow, int waterTopSubRow)
    {
        int groundRowLeft = GroundRowAt(world, visualSeed, dotLocationLeft, locationInt, groundBaseSubRow, waterTopSubRow);
        int groundRowRight = GroundRowAt(world, visualSeed, dotLocationRight, locationInt, groundBaseSubRow, waterTopSubRow);

        bool d1 = cy * 4 + 0 >= groundRowLeft;
        bool d2 = cy * 4 + 1 >= groundRowLeft;
        bool d3 = cy * 4 + 2 >= groundRowLeft;
        bool d4 = cy * 4 + 0 >= groundRowRight;
        bool d5 = cy * 4 + 1 >= groundRowRight;
        bool d6 = cy * 4 + 2 >= groundRowRight;
        bool d7 = cy * 4 + 3 >= groundRowLeft;
        bool d8 = cy * 4 + 3 >= groundRowRight;

        int dotsOn = CountDots(d1, d2, d3, d4, d5, d6, d7, d8);

        if (dotsOn == 0)
        {
            RenderSkyCell(visualSeed, glyphs, colors, screenCol, cx, cy);
            return;
        }

        char biome = world.BiomeCodeAt(locationInt);

        char glyph = BraillePixel(d1, d2, d3, d4, d5, d6, d7, d8);
        bool pulseBright = world.DaysWithoutSun % 2 == 0;

        ConsoleColor color = biome switch
        {
            'c' => dotsOn == 8 ? ConsoleColor.DarkGray : ConsoleColor.Gray,
            'e' => dotsOn == 8 ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta,
            'i' => dotsOn == 8 ? ConsoleColor.DarkMagenta : ConsoleColor.DarkRed,
            'h' => pulseBright
                ? (dotsOn == 8 ? ConsoleColor.Green : ConsoleColor.White)
                : (dotsOn == 8 ? ConsoleColor.DarkGreen : ConsoleColor.Green),
            'g' => dotsOn == 8 ? ConsoleColor.DarkRed : ConsoleColor.Red,
            'f' => dotsOn == 8 ? ConsoleColor.DarkYellow : ConsoleColor.Yellow,
            _ => dotsOn == 8 ? ConsoleColor.DarkYellow : ConsoleColor.Green, // Camp, Forest
        };

        glyphs[cy, cx] = glyph;
        colors[cy, cx] = color;
    }

    // How many subrows of light-blue "shallow" water sit below the flat
    // surface line before it darkens into deep blue.
    private const int ShallowWaterSubRows = 8;

    // The water's top edge is the same flat subrow for every flooded
    // column, so it reads as a straight horizontal line rather than a wave.
    // A light-blue shallow band sits just below that line, darkening into
    // deep blue further down.
    private static void RenderWaterCell(int visualSeed, char[,] glyphs, ConsoleColor[,] colors, int screenCol, int cx, int cy, int waterTopSubRow)
    {
        bool top = cy * 4 + 0 >= waterTopSubRow;
        bool upper = cy * 4 + 1 >= waterTopSubRow;
        bool lower = cy * 4 + 2 >= waterTopSubRow;
        bool bottom = cy * 4 + 3 >= waterTopSubRow;

        int dotsOn = CountDots(top, upper, lower, top, upper, lower, bottom, bottom);

        if (dotsOn == 0)
        {
            RenderSkyCell(visualSeed, glyphs, colors, screenCol, cx, cy);
            return;
        }

        bool isShallow = dotsOn < 8 || cy * 4 < waterTopSubRow + ShallowWaterSubRows;

        glyphs[cy, cx] = BraillePixel(top, upper, lower, top, upper, lower, bottom, bottom);
        colors[cy, cx] = isShallow ? ConsoleColor.Cyan : ConsoleColor.Blue;
    }

    public static void RenderSurface(World world)
    {
        int sceneWidth = GetSceneWidth();
        int sceneHeight = GetSceneHeight();
        int visualSeed = VisualSeed;

        int totalSubRows = sceneHeight * 4;
        int groundBaseSubRow = totalSubRows * 2 / 3;
        int waterTopSubRow = groundBaseSubRow + WaterLevelOffset;

        int surfaceX = world.Player.Location;
        int playerScreenCol = surfaceX * VisualScale;
        int viewStartScreenCol = playerScreenCol - sceneWidth / 2;

        char[,] glyphs = new char[sceneHeight, sceneWidth];
        ConsoleColor[,] colors = new ConsoleColor[sceneHeight, sceneWidth];

        // Pass 1: terrain and water, filling every cell in the buffer.
        // Column-outer so flood status - which only depends on the column,
        // not the row - is settled once per column instead of once per cell.
        for (int cx = 0; cx < sceneWidth; cx++)
        {
            int screenCol = viewStartScreenCol + cx;
            int locationInt = (int)Math.Round((double)screenCol / VisualScale);

            double dotLocationLeft = (screenCol * 2 + 0) / (2.0 * VisualScale);
            double dotLocationRight = (screenCol * 2 + 1) / (2.0 * VisualScale);
            bool flooded = IsFlooded(world, visualSeed, locationInt, groundBaseSubRow, waterTopSubRow);

            for (int cy = 0; cy < sceneHeight; cy++)
            {
                if (flooded)
                {
                    RenderWaterCell(visualSeed, glyphs, colors, screenCol, cx, cy, waterTopSubRow);
                }
                else
                {
                    RenderGroundCell(world, visualSeed, glyphs, colors, screenCol, cx, cy, locationInt, dotLocationLeft, dotLocationRight, groundBaseSubRow, waterTopSubRow);
                }
            }
        }

        // Pass 2: clutter sprites. These can be wider or taller than one
        // cell, so they're stamped on top of the finished terrain rather
        // than decided together with it.
        for (int cx = 0; cx < sceneWidth; cx++)
        {
            int screenCol = viewStartScreenCol + cx;
            int locationInt = (int)Math.Round((double)screenCol / VisualScale);
            double dotLocationLeft = (screenCol * 2 + 0) / (2.0 * VisualScale);

            if (IsFlooded(world, visualSeed, locationInt, groundBaseSubRow, waterTopSubRow))
            {
                continue;
            }

            int standingRow = StandingRowAt(world, visualSeed, dotLocationLeft, locationInt, groundBaseSubRow, waterTopSubRow);
            char biome = world.BiomeCodeAt(locationInt);

            if (biome == 'a' && IsTree(visualSeed, screenCol))
            {
                ConsoleColor color = PositiveHash(visualSeed, screenCol, 555) % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Green;
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, TreeSmall, cx, standingRow, color);
            }
            else if (biome == 'b' && IsDenseTree(visualSeed, screenCol))
            {
                ConsoleColor color = PositiveHash(visualSeed, screenCol, 555) % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Green;
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, PickTree(visualSeed, screenCol), cx, standingRow, color);
            }
            else if (biome == 'g' && IsDenseTree(visualSeed, screenCol))
            {
                ConsoleColor color = PositiveHash(visualSeed, screenCol, 555) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Red;
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, PickTree(visualSeed, screenCol), cx, standingRow, color);
            }
            else if ((biome == 'e' || biome == 'i') && IsSparkle(visualSeed, screenCol) && standingRow >= 0 && standingRow < sceneHeight)
            {
                glyphs[standingRow, cx] = '✦';
                colors[standingRow, cx] = PositiveHash(visualSeed, screenCol, 444) % 2 == 0 ? ConsoleColor.Magenta : ConsoleColor.Cyan;
            }
            else if (biome == 'c' && IsGlint(visualSeed, screenCol) && standingRow >= 0 && standingRow < sceneHeight)
            {
                glyphs[standingRow, cx] = '*';
                colors[standingRow, cx] = ConsoleColor.Yellow;
            }
            else if (biome == 'c' && IsRock(visualSeed, screenCol))
            {
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, PickRock(visualSeed, screenCol), cx, standingRow, ConsoleColor.Gray);
            }
            else if (biome == 'h' && IsGlint(visualSeed, screenCol) && standingRow >= 0 && standingRow < sceneHeight)
            {
                glyphs[standingRow, cx] = '☢';
                colors[standingRow, cx] = world.DaysWithoutSun % 2 == 0 ? ConsoleColor.White : ConsoleColor.Green;
            }
            else if (biome == 'f' && IsCactus(visualSeed, screenCol))
            {
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, PickCactus(visualSeed, screenCol), cx, standingRow, ConsoleColor.DarkGreen);
            }
            else if (biome == 'f' && IsRock(visualSeed, screenCol))
            {
                StampSprite(glyphs, colors, sceneWidth, sceneHeight, PickRock(visualSeed, screenCol), cx, standingRow, ConsoleColor.DarkYellow);
            }
        }

        // Pass 3: the camp tent, its campfire, and the player always
        // render on top of everything else.
        int tentCol = 0 - viewStartScreenCol;

        if (tentCol >= 0 && tentCol < sceneWidth)
        {
            double tentDotLocation = (0 * 2 + 0) / (2.0 * VisualScale);
            int tentRow = StandingRowAt(world, visualSeed, tentDotLocation, 0, groundBaseSubRow, waterTopSubRow);
            StampSprite(glyphs, colors, sceneWidth, sceneHeight, Tent, tentCol, tentRow, ConsoleColor.Yellow);

            int fireScreenCol = tentCol + 5;
            int fireLocationInt = (int)Math.Round((double)(fireScreenCol + viewStartScreenCol) / VisualScale);
            double fireDotLocation = ((fireScreenCol + viewStartScreenCol) * 2 + 0) / (2.0 * VisualScale);
            int fireRow = StandingRowAt(world, visualSeed, fireDotLocation, fireLocationInt, groundBaseSubRow, waterTopSubRow);
            ConsoleColor flameColor = world.DaysWithoutSun % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.Red;

            StampSprite(glyphs, colors, sceneWidth, sceneHeight, CampfireFlame, fireScreenCol, fireRow, flameColor);
            StampSprite(glyphs, colors, sceneWidth, sceneHeight, CampfireSmoke, fireScreenCol, fireRow - 1, ConsoleColor.Gray);
        }

        int playerCol = playerScreenCol - viewStartScreenCol;

        if (playerCol >= 0 && playerCol < sceneWidth)
        {
            int playerLocationInt = (int)Math.Round((double)playerScreenCol / VisualScale);

            if (!IsFlooded(world, visualSeed, playerLocationInt, groundBaseSubRow, waterTopSubRow))
            {
                double playerDotLocation = (playerScreenCol * 2 + 0) / (2.0 * VisualScale);
                int playerRow = StandingRowAt(world, visualSeed, playerDotLocation, playerLocationInt, groundBaseSubRow, waterTopSubRow);

                if (playerRow >= 0 && playerRow < sceneHeight)
                {
                    glyphs[playerRow, playerCol] = '@';
                    colors[playerRow, playerCol] = ConsoleColor.Cyan;
                }
            }
        }

        // Pass 4: print the finished buffer. Reassigning ForegroundColor
        // is the real per-frame cost here (and the main source of visible
        // flicker on slower console hosts) - only change it when the color
        // actually changes from the previous cell, since long runs of the
        // same color are the common case.
        ConsoleColor currentColor = colors[0, 0];
        Console.ForegroundColor = currentColor;

        for (int cy = 0; cy < sceneHeight; cy++)
        {
            for (int cx = 0; cx < sceneWidth; cx++)
            {
                if (colors[cy, cx] != currentColor)
                {
                    currentColor = colors[cy, cx];
                    Console.ForegroundColor = currentColor;
                }

                Console.Write(glyphs[cy, cx]);
            }

            Console.WriteLine();
        }

        Console.ResetColor();
    }

    // =================================================================
    // Intro cutscene: the sun boils, collapses and explodes into braille
    // debris, the stars come out, and then the title fades in. Called
    // once from Display.ShowIntroLore. Any key skips the whole intro.
    //
    // Every frame is painted into a buffer of characters plus a finer
    // grid of braille dots (2 x 4 per character, the same trick the
    // landscape uses) and then written over the previous frame from the
    // top-left corner, so nothing flickers.
    // =================================================================

    private static int introWidth;
    private static int introHeight;
    private static char[,] introGlyphs;
    private static ConsoleColor[,] introColors;
    private static bool[,] introDots;
    private static ConsoleColor[,] introDotColors;
    private static (int x, int y)[] introStars;
    private static bool introAnsi;

    private class IntroSkipped : Exception
    {
    }

    public static void PlayIntro()
    {
        introWidth = 72;
        introHeight = 22;

        try
        {
            introWidth = Math.Clamp(Console.WindowWidth - 1, 40, 140);
            introHeight = Math.Clamp(Console.WindowHeight - 2, 16, 45);
        }
        catch
        {
            // No real console window: keep the default size.
        }

        introGlyphs = new char[introHeight, introWidth];
        introColors = new ConsoleColor[introHeight, introWidth];
        introDots = new bool[introHeight * 4, introWidth * 2];
        introDotColors = new ConsoleColor[introHeight, introWidth];
        introStars = MakeIntroStars();
        introAnsi = TryEnableAnsi();

        ClearIntroScreen();
        SetCursorVisible(false);

        try
        {
            PlaySunExplosion();
            PlayTitleReveal();
        }
        catch (IntroSkipped)
        {
        }
        finally
        {
            Console.ResetColor();
            SetCursorVisible(true);
        }
    }

    private static void PlaySunExplosion()
    {
        int centerX = introWidth;
        int centerY = introHeight * 2;
        int radius = Math.Min(introWidth * 2, introHeight * 4) / 5;

        // 1. A pulsing sun with turning rays, its surface boiling more and
        //    more until it flickers white and red.
        for (int frame = 0; frame < 24; frame++)
        {
            ClearIntroCanvas();
            bool unstable = frame >= 12;
            int size = radius + frame % 2;
            ConsoleColor color = ConsoleColor.Yellow;

            if (unstable)
            {
                color = frame % 3 == 0 ? ConsoleColor.White : ConsoleColor.Red;
            }

            int rim = unstable ? 3 : 1;

            for (int dy = -size - rim; dy <= size + rim; dy++)
            {
                for (int dx = -size - rim; dx <= size + rim; dx++)
                {
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance <= size - 2)
                    {
                        IntroDot(centerX + dx, centerY + dy, color);
                    }
                    else if (distance <= size + rim && PositiveHash(frame, dx, dy) % 3 != 0)
                    {
                        IntroDot(centerX + dx, centerY + dy, color);
                    }
                }
            }

            int rayCount = 12;

            for (int ray = 0; ray < rayCount; ray++)
            {
                double angle = frame * 0.06 + ray * Math.PI * 2 / rayCount;
                int length = (ray + frame) % 2 == 0 ? 7 : 4;

                if (unstable)
                {
                    length += PositiveHash(frame, ray, 1) % 6;
                }

                for (int k = size + 4; k <= size + 4 + length; k++)
                {
                    IntroDot(centerX + (int)Math.Round(Math.Cos(angle) * k), centerY + (int)Math.Round(Math.Sin(angle) * k), color);
                }
            }

            PresentIntro();
            IntroWait(unstable ? 90 : 140);
        }

        // 2. It collapses into a single white-hot point.
        for (int step = 6; step >= 0; step--)
        {
            ClearIntroCanvas();
            int size = radius * step / 6;

            for (int dy = -size; dy <= size; dy++)
            {
                for (int dx = -size; dx <= size; dx++)
                {
                    if (dx * dx + dy * dy <= size * size)
                    {
                        IntroDot(centerX + dx, centerY + dy, ConsoleColor.White);
                    }
                }
            }

            PresentIntro();
            IntroWait(90);
        }

        // 3. Flash: the whole screen lights up, then dims.
        IntroFlash(1, ConsoleColor.White);
        IntroFlash(2, ConsoleColor.Yellow);
        IntroFlash(5, ConsoleColor.DarkYellow);

        // 4. Debris and a shockwave fly outward and cool down.
        Random random = new Random(3);
        int count = 240;
        double[] angles = new double[count];
        double[] speeds = new double[count];

        for (int i = 0; i < count; i++)
        {
            angles[i] = random.NextDouble() * Math.PI * 2;
            speeds[i] = 0.8 + random.NextDouble() * 2.6;
        }

        ConsoleColor[] cooling =
        {
            ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.DarkYellow,
            ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.DarkGray, ConsoleColor.DarkGray,
        };

        for (int t = 1; t <= 24; t++)
        {
            ClearIntroCanvas();
            ConsoleColor color = cooling[Math.Min(cooling.Length - 1, t / 3)];

            if (t < 10)
            {
                int ring = t * 5;

                for (int a = 0; a < 360; a += 2)
                {
                    if (PositiveHash(t, a, 7) % 3 == 0)
                    {
                        continue;
                    }

                    double angle = a * Math.PI / 180;
                    IntroDot(centerX + (int)Math.Round(Math.Cos(angle) * ring), centerY + (int)Math.Round(Math.Sin(angle) * ring), ConsoleColor.DarkYellow);
                }
            }

            for (int i = 0; i < count; i++)
            {
                int x = centerX + (int)Math.Round(Math.Cos(angles[i]) * speeds[i] * t);
                int y = centerY + (int)Math.Round(Math.Sin(angles[i]) * speeds[i] * t);
                IntroDot(x, y, color);

                if (t < 7)
                {
                    IntroDot(x + 1, y, color);
                    IntroDot(x, y + 1, color);
                    IntroDot(x + 1, y + 1, color);
                }
            }

            PresentIntro();
            IntroWait(60);
        }

        // 5. Darkness, and the stars come out one by one.
        for (int visible = 0; visible <= introStars.Length; visible += 3)
        {
            ClearIntroCanvas();
            DrawIntroStars(visible, visible);
            PresentIntro();
            IntroWait(50);
        }

        TypeIntroText(introHeight - 4, "The sun is gone.", ConsoleColor.Gray);
        IntroWait(1500);
    }

    private static void PlayTitleReveal()
    {
        string title = "T H E    E T E R N A L    D A R K";
        int titleX = (introWidth - title.Length) / 2;
        int titleY = introHeight / 2 - 2;
        int frames = title.Length * 2 + 8;

        for (int frame = 0; frame < frames; frame++)
        {
            ClearIntroCanvas();
            DrawIntroStars(introStars.Length, frame);

            for (int i = 0; i < title.Length; i++)
            {
                int age = frame - i * 2;

                if (age < 0 || title[i] == ' ')
                {
                    continue;
                }

                ConsoleColor color = ConsoleColor.White;

                if (age < 2)
                {
                    color = ConsoleColor.DarkGray;
                }
                else if (age < 4)
                {
                    color = ConsoleColor.Gray;
                }

                IntroPut(titleX + i, titleY, title[i], color);
            }

            PresentIntro();
            IntroWait(60);
        }

        TypeIntroText(titleY + 3, "a dwarf, a dead sun, and a very long night", ConsoleColor.DarkGray);
        IntroWait(2000);
    }

    private static void IntroFlash(int oneIn, ConsoleColor color)
    {
        ClearIntroCanvas();

        for (int y = 0; y < introHeight * 4; y++)
        {
            for (int x = 0; x < introWidth * 2; x++)
            {
                if (PositiveHash(oneIn, x, y) % oneIn == 0)
                {
                    IntroDot(x, y, color);
                }
            }
        }

        PresentIntro();
        IntroWait(70);
    }

    private static (int x, int y)[] MakeIntroStars()
    {
        Random random = new Random(11);
        int count = introWidth * introHeight / 22;
        (int x, int y)[] result = new (int x, int y)[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = (random.Next(0, introWidth), random.Next(0, introHeight));
        }

        return result;
    }

    private static void DrawIntroStars(int visible, int frame)
    {
        for (int i = 0; i < visible && i < introStars.Length; i++)
        {
            bool bright = (i * 7 + frame) % 23 == 0;
            ConsoleColor color = bright ? ConsoleColor.White : (i % 3 == 0 ? ConsoleColor.Gray : ConsoleColor.DarkGray);
            IntroPut(introStars[i].x, introStars[i].y, bright ? '*' : '.', color);
        }
    }

    // Types centered text letter by letter on top of the current frame.
    private static void TypeIntroText(int y, string text, ConsoleColor color)
    {
        int x = (introWidth - text.Length) / 2;

        for (int i = 0; i < text.Length; i++)
        {
            IntroPut(x + i, y, text[i], color);
            PresentIntro();
            IntroWait(45);
        }
    }

    private static void IntroPut(int x, int y, char glyph, ConsoleColor color)
    {
        if (x < 0 || x >= introWidth || y < 0 || y >= introHeight)
        {
            return;
        }

        introGlyphs[y, x] = glyph;
        introColors[y, x] = color;
    }

    // Lights one braille dot; its character cell takes the dot's color.
    private static void IntroDot(int dotX, int dotY, ConsoleColor color)
    {
        if (dotX < 0 || dotX >= introWidth * 2 || dotY < 0 || dotY >= introHeight * 4)
        {
            return;
        }

        introDots[dotY, dotX] = true;
        introDotColors[dotY / 4, dotX / 2] = color;
    }

    private static void ClearIntroCanvas()
    {
        for (int y = 0; y < introHeight; y++)
        {
            for (int x = 0; x < introWidth; x++)
            {
                introGlyphs[y, x] = ' ';
                introColors[y, x] = ConsoleColor.Gray;
            }
        }

        Array.Clear(introDots);
    }

    // The character to show in one cell: braille if any of its dots are
    // lit, otherwise whatever plain character was put there.
    private static char IntroCell(int x, int y, out ConsoleColor color)
    {
        int dx = x * 2;
        int dy = y * 4;
        char braille = BraillePixel(
            introDots[dy, dx], introDots[dy + 1, dx], introDots[dy + 2, dx],
            introDots[dy, dx + 1], introDots[dy + 1, dx + 1], introDots[dy + 2, dx + 1],
            introDots[dy + 3, dx], introDots[dy + 3, dx + 1]);

        if (braille != (char)0x2800)
        {
            color = introDotColors[y, x];
            return braille;
        }

        color = introColors[y, x];
        return introGlyphs[y, x];
    }

    // With ANSI support the frame goes out as one string (fast);
    // otherwise it falls back to switching Console.ForegroundColor per
    // color run, which is slow on the classic console window.
    private static void PresentIntro()
    {
        if (introAnsi)
        {
            System.Text.StringBuilder frame = new System.Text.StringBuilder(introWidth * introHeight * 2);
            frame.Append("\u001b[H");

            for (int y = 0; y < introHeight; y++)
            {
                ConsoleColor? current = null;

                for (int x = 0; x < introWidth; x++)
                {
                    char glyph = IntroCell(x, y, out ConsoleColor color);

                    if (glyph != ' ' && color != current)
                    {
                        frame.Append("\u001b[").Append(AnsiCode(color)).Append('m');
                        current = color;
                    }

                    frame.Append(glyph);
                }

                frame.Append("\u001b[0m\n");
            }

            Console.Out.Write(frame.ToString());
            Console.Out.Flush();
            return;
        }

        try
        {
            Console.SetCursorPosition(0, 0);
        }
        catch
        {
            // Redirected output: frames just scroll.
        }

        for (int y = 0; y < introHeight; y++)
        {
            System.Text.StringBuilder run = new System.Text.StringBuilder();
            ConsoleColor runColor = Console.ForegroundColor;

            for (int x = 0; x < introWidth; x++)
            {
                char glyph = IntroCell(x, y, out ConsoleColor color);

                if (glyph != ' ' && color != runColor)
                {
                    Console.ForegroundColor = runColor;
                    Console.Write(run.ToString());
                    run.Clear();
                    runColor = color;
                }

                run.Append(glyph);
            }

            Console.ForegroundColor = runColor;
            Console.WriteLine(run.ToString());
        }
    }

    private static int AnsiCode(ConsoleColor color)
    {
        switch (color)
        {
            case ConsoleColor.Black: return 30;
            case ConsoleColor.DarkRed: return 31;
            case ConsoleColor.DarkGreen: return 32;
            case ConsoleColor.DarkYellow: return 33;
            case ConsoleColor.DarkBlue: return 34;
            case ConsoleColor.DarkMagenta: return 35;
            case ConsoleColor.DarkCyan: return 36;
            case ConsoleColor.Gray: return 37;
            case ConsoleColor.DarkGray: return 90;
            case ConsoleColor.Red: return 91;
            case ConsoleColor.Green: return 92;
            case ConsoleColor.Yellow: return 93;
            case ConsoleColor.Blue: return 94;
            case ConsoleColor.Magenta: return 95;
            case ConsoleColor.Cyan: return 96;
            default: return 97;
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(int handle);

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr handle, out uint mode);

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr handle, uint mode);

    // Asks the Windows console to understand ANSI color codes. Windows
    // Terminal and other platforms already do.
    private static bool TryEnableAnsi()
    {
        if (Console.IsOutputRedirected)
        {
            return false;
        }

        if (!OperatingSystem.IsWindows())
        {
            return true;
        }

        try
        {
            const int StdOutputHandle = -11;
            const uint EnableVirtualTerminalProcessing = 0x0004;
            IntPtr handle = GetStdHandle(StdOutputHandle);

            if (!GetConsoleMode(handle, out uint mode))
            {
                return false;
            }

            return SetConsoleMode(handle, mode | EnableVirtualTerminalProcessing);
        }
        catch
        {
            return false;
        }
    }

    // Sleeps in short slices so a key press can skip the intro. Timed
    // with a stopwatch, because Thread.Sleep on Windows often oversleeps.
    private static void IntroWait(int milliseconds)
    {
        System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();

        while (timer.ElapsedMilliseconds < milliseconds)
        {
            if (IntroKeyPressed())
            {
                throw new IntroSkipped();
            }

            long remaining = milliseconds - timer.ElapsedMilliseconds;
            Thread.Sleep((int)Math.Clamp(remaining, 1, 15));
        }
    }

    private static bool IntroKeyPressed()
    {
        try
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                return true;
            }
        }
        catch (InvalidOperationException)
        {
            // Input is redirected: there are no key presses to read.
        }

        return false;
    }

    private static void SetCursorVisible(bool visible)
    {
        try
        {
            Console.CursorVisible = visible;
        }
        catch
        {
        }
    }

    private static void ClearIntroScreen()
    {
        try
        {
            Console.Clear();
        }
        catch (System.IO.IOException)
        {
        }
    }
}
