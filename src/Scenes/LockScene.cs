
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class LockScene : IScene
{
    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);
    public List<Entity> Entities { get; set; } = new List<Entity>();
    public Vector4 FogColor { get; set; } = new Vector4(1, 1, 1, 1);
    public float FogDensity { get; set; } = 0f;

    private int[] offsets = new int[5] { 0, 2, 2, 1, 4 };
    private int[] identity = new int[5] { 0b10000, 0b01000, 0b00100, 0b00010, 0b00001 };
    private int[] masks = new int[5] { 0b10000, 0b01100, 0b00101, 0b01010, 0b10001 };
    private int _hoveredIndex = -1;

    private int _xOffset = 640;
    private int _yOffset = 180;

    public void Init() { }

    public void Update(float deltaTime)
    {
        var solved = offsets.All(o => o == 1);
        if (solved)
        {
            SceneManager.Instance.Pop();
            RootGameState.Instance.CurrentConversationTarget = PersonKind.Lord;
            SceneManager.Instance.Push(new DialogueScene());
            return;
        }
        _hoveredIndex = -1;
        for (var i = 0; i < 5; i++)
        {
            var minX = _xOffset + 180 + (60 * i);
            var maxX = minX + 30;
            var minY = (_yOffset + 320) + offsets[i] * 10;
            var maxY = minY + 120;

            if (ScreenInfo.MouseX > minX && ScreenInfo.MouseX < maxX && ScreenInfo.MouseY > minY && ScreenInfo.MouseY < maxY)
            {
                _hoveredIndex = i;
            }
        }

        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            if (_hoveredIndex != -1)
            {
                var mask = masks[_hoveredIndex];
                for (var i = 0; i < 5; i++)
                {
                    if ((mask & identity[i]) > 0)
                    {
                        offsets[i] += 1;
                        if (offsets[i] > 4)
                        {
                            offsets[i] = 0;
                        }
                    }
                }
            }
        }
    }

    public void Render(float deltaTime)
    {
        BeginTextureMode(_renderTexture);
        {
            ClearBackground(new Color(0, 0, 0, 0));

            DrawRectangle(0, 0, ScreenInfo.RenderWidth, ScreenInfo.RenderHeight, new Color(0, 0, 0, 55));

            DrawTextureEx(ResourceManager.Instance.Textures["lock_bg"], new Vector2(_xOffset, _yOffset), 0, 10f, Color.WHITE);

            for (var i = 0; i < 5; i++)
            {
                var tex = _hoveredIndex == i ? "lock_pin_hover" : "lock_pin";
                var pos = new Vector2(_xOffset + 180 + (60 * i), (_yOffset + 320) + offsets[i] * 10);
                DrawTextureEx(ResourceManager.Instance.Textures[tex], pos, 0, 10, Color.WHITE);
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