// Like WorldArt.cs, this file is part of the "advanced techniques" corner
// of the project - deliberately kept out of the students' way. It talks
// directly to Windows (DllImport, structs, IntPtr) and only exists so the
// braille glyphs WorldArt draws show up properly.
//
// Classic cmd.exe/conhost windows often default to a font (Consolas, or an
// old raster font) that's missing braille and some symbol glyphs, which is
// why they can show boxes/question marks where Windows Terminal (Cascadia
// Mono/Code) wouldn't. This nudges the console's own font to Cascadia Mono
// at startup - a legacy Win32 API that only does anything on that classic
// console host, and is wrapped in a try/catch since it's purely cosmetic:
// if it fails (no real console, non-Windows, API unavailable), the game
// just keeps whatever font was already set.
using System.Runtime.InteropServices;

internal static class ConsoleFont
{
    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CONSOLE_FONT_INFO_EX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public uint FontFamily;
        public uint FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

    private const int StdOutputHandle = -11;

    public static void TryUseBrailleCapableFont()
    {
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        try
        {
            IntPtr handle = GetStdHandle(StdOutputHandle);
            CONSOLE_FONT_INFO_EX fontInfo = new CONSOLE_FONT_INFO_EX();
            fontInfo.cbSize = (uint)Marshal.SizeOf(fontInfo);

            if (!GetCurrentConsoleFontEx(handle, false, ref fontInfo))
            {
                return;
            }

            fontInfo.FaceName = "Cascadia Mono";

            if (fontInfo.dwFontSize.Y < 14)
            {
                fontInfo.dwFontSize.Y = 16;
            }

            SetCurrentConsoleFontEx(handle, false, ref fontInfo);
        }
        catch
        {
            // Cosmetic only - if this fails for any reason, just keep
            // whatever font the console already had.
        }
    }
}
