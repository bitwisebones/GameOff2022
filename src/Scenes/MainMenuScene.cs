
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class MainMenuScene : IScene
{
    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(10, 2, 10),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    private int _renderWidth = GetScreenWidth() / 4;
    private int _renderHeight = GetScreenHeight() / 4;
    private int _screenWidth = GetScreenWidth();
    private int _screenHeight = GetScreenHeight();

    private RenderTexture2D _renderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);

    private Model model;

    public void Init()
    {
        SetCameraMode(_camera, CameraMode.CAMERA_ORBITAL);
        model = ResourceManager.Instance.Models["test"];
        var texture = ResourceManager.Instance.Textures["wall"];
        Raylib.SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);
    }

    public void Update(float deltaTime)
    {
        Raylib.UpdateCamera(ref _camera);
        Raylib.BeginDrawing();
        {
            Raylib.BeginTextureMode(_renderTexture);
            {
                Raylib.ClearBackground(Color.WHITE);
                Raylib.DrawText("MAN OR MOUSE?", 12, 12, 20, Color.BLACK);

                Raylib.BeginMode3D(_camera);
                {
                    Raylib.DrawModel(model, new Vector3(0, 0, 0), 1, Color.WHITE);
                }
                Raylib.EndMode3D();
            }
            Raylib.EndTextureMode();

            DrawTexturePro(_renderTexture.texture, new Rectangle(0, 0, -_renderWidth, _renderHeight), new Rectangle(_screenWidth, _screenHeight, _screenWidth, _screenHeight), new Vector2(0, 0), 180, Color.WHITE);
            DrawFPS(10, 10);
        }
        Raylib.EndDrawing();
    }

    public void Deinit() { }
}