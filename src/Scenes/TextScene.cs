using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class TextScene : IScene
{
    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);
    public List<Entity> Entities { get; set; } = new List<Entity>();
    public Vector4 FogColor { get; set; } = new Vector4(1, 1, 1, 1);
    public float FogDensity { get; set; } = 0f;

    private int _rowHeight = 80;
    private int _padding = 25;
    private int _xpadding = 5;

    private List<string> _text;

    public TextScene(List<string> text, int padding = 5)
    {
        _text = text;
        _xpadding = padding;
    }

    public void Init() { }

    public void Update(float deltaTime)
    {
        // CheckForClicks
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            SceneManager.Instance.Pop();
        }
    }

    public void Render(float deltaTime)
    {
        var font = ResourceManager.Instance.Fonts["alagard"];
        var fontSize = 55;

        BeginTextureMode(_renderTexture);
        {
            ClearBackground(new Color(0, 0, 0, 0));
            var totalHeight = _text.Count * _rowHeight;
            var yOffset = (int)Math.Floor(ScreenInfo.RenderHeight / 2f) - (int)Math.Floor(totalHeight / 2f) + _padding;
            DrawRectangle(0, yOffset, ScreenInfo.RenderWidth, totalHeight, Color.BLACK);

            for (var i = 0; i < _text.Count; i++)
            {
                DrawTextEx(font, _text[i], new Vector2(_xpadding, yOffset + (i * _rowHeight)), fontSize, 1, Color.WHITE);
            }

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
}