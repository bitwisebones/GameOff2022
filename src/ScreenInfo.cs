using static Raylib_cs.Raylib;

public static class ScreenInfo
{
    public const int Crunch = 4;
    public static int RenderWidth => GetScreenWidth() / Crunch;
    public static int RenderHeight => GetScreenHeight() / Crunch;
    public static int ScreenWidth => GetScreenWidth();
    public static int ScreenHeight => GetScreenHeight();
}