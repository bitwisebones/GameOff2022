
using Raylib_cs;

public class MainMenuScene : IScene
{
    public void Update(float deltaTime)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        Raylib.DrawText("MAIN MENU", 12, 12, 20, Color.BLACK);

        Raylib.EndDrawing();
    }
}