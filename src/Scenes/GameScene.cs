
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class GameScene : IScene
{
    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(10, 2, 10),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    private RenderTexture2D _renderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);

    public void Init()
    {
        HideCursor();
    }

    public void Update(float deltaTime)
    {
        UpdateCamera(ref _camera);
        BeginDrawing();
        {
            BeginTextureMode(_renderTexture);
            {
                ClearBackground(Color.WHITE);

                BeginMode3D(_camera);
                {
                }
                EndMode3D();

                DrawTextureEx(ResourceManager.Instance.Textures["cursor"], new Vector2(GetMouseX() / ScreenInfo.Crunch, GetMouseY() / ScreenInfo.Crunch), 0.0f, 0.5f, Color.WHITE);

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
            DrawFPS(10, 10);
        }
        EndDrawing();
    }

    public void Deinit() { }
}