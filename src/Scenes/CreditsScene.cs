
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class CreditsScene : IScene
{
    public List<Entity> Entities { get; set; } = new List<Entity>();
    public Vector4 FogColor { get; set; }
    public float FogDensity { get; set; }

    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);

    public void Init()
    {
    }

    public void Render(float deltaTime)
    {
        var font = ResourceManager.Instance.Fonts["alagard"];
        BeginTextureMode(_renderTexture);
        {
            ClearBackground(new Color(0, 0, 0, 255));
            DrawTextEx(font, "The End", new Vector2(650, 400), 160, 1, Color.WHITE);
            DrawTextureEx(ResourceManager.Instance.Textures["cursor"], new Vector2(ScreenInfo.MouseX, ScreenInfo.MouseY), 0.0f, 2f, Color.WHITE);
        }
        EndTextureMode();

        DrawTexturePro(
            _renderTexture.texture,
            new Rectangle(0, 0, -ScreenInfo.RenderWidth, ScreenInfo.RenderHeight),
            new Rectangle(ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight, ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight),
            new Vector2(0, 0),
            180,
            Color.WHITE
        );
    }

    public void Update(float deltaTime)
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            CloseWindow();
        }
    }
}