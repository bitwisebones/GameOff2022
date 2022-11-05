
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class MainMenuStage : IStage
{
    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(10, 2, 10),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);

    private Model _model;
    private Texture2D _cursor;

    public void Init()
    {
        _model = ResourceManager.Instance.Models["test"];
        var texture = ResourceManager.Instance.Textures["wall"];
        Raylib.SetMaterialTexture(ref _model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        _cursor = ResourceManager.Instance.Textures["cursor"];

        SetCameraMode(_camera, CameraMode.CAMERA_ORBITAL);
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
                DrawText("MAN OR MOUSE?", 12, 12, 20, Color.BLACK);
                DrawText("Click to Start", 12, 50, 20, Color.BLACK);

                BeginMode3D(_camera);
                {
                    DrawModel(_model, new Vector3(0, 0, 0), 1, Color.WHITE);
                }
                EndMode3D();

                DrawTextureEx(_cursor, new Vector2(GetMouseX() / ScreenInfo.Crunch, GetMouseY() / ScreenInfo.Crunch), 0.0f, 0.5f, Color.WHITE);
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