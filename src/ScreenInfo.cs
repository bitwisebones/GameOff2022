using static Raylib_cs.Raylib;

public static class ScreenInfo
{
    public const int Crunch = 1;
    public static int RenderWidth => 1920;
    public static int RenderHeight => 1080;
    public static int ScreenWidth => GetScreenWidth();
    public static int ScreenHeight => GetScreenHeight();
    public static int MouseX => (int)Math.Floor(((float)RenderWidth / (float)ScreenWidth) * GetMouseX());
    public static int MouseY => (int)Math.Floor(((float)RenderHeight / (float)ScreenHeight) * GetMouseY());

    public static float RatioX = (float)RenderWidth / (float)ScreenWidth;
    public static float RatioY = (float)RenderHeight / (float)ScreenHeight;
}