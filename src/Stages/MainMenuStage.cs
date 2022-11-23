
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;
using static Raylib_cs.ShaderLocationIndex;

public class MainMenuStage : IStage
{
    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(1, 2, 19),
        target = new Vector3(19, 2, 19),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    private List<Entity> Entities { get; set; } = new List<Entity>();
    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);

    private static float _speed = 0.005f;
    private List<CameraMovement> _moves = new List<CameraMovement>
    {
        new CameraMovement{
            StartPosition = new Vector3(1, 5, 19),
            EndPosition = new Vector3(3, 5, 19),
            Target = new Vector3(3, 5, 19),
            Delta = new Vector3(_speed, 0, 0),
        },
        new CameraMovement{
            StartPosition = new Vector3(15, 0, 16),
            EndPosition = new Vector3(15, 0, 14),
            Target = new Vector3(15, 4, 10),
            Delta = new Vector3(0, 0, -_speed),
        },
        new CameraMovement{
            StartPosition = new Vector3(14, 1, 20),
            EndPosition = new Vector3(12, 1, 20),
            Target = new Vector3(14, 1, 22),
            Delta = new Vector3(-_speed, 0, 0),
            IsPan = true,
        }
    };

    private int _currentMoveIndex = 0;

    private Texture2D _cursor;

    public void Init()
    {
        _cursor = ResourceManager.Instance.Textures["cursor"];

        SetCameraMode(_camera, CameraMode.CAMERA_CUSTOM);
        HideCursor();

        var s = SceneFactory.Build(TownSceneData.GetData());
        Entities = s.Entities;

        var initialMovement = _moves[_currentMoveIndex];
        _camera.position = initialMovement.StartPosition;
        _camera.target = initialMovement.Target;

        var fogDensityLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogDensity");
        var fogColorLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogColor");

        var fogColor = new Vector4(0.3f, 0.3f, 0.3f, 1);
        var fogDensity = 0.02f;

        SetShaderValue(ResourceManager.Instance.Shader, fogColorLoc, fogColor, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
        SetShaderValue(ResourceManager.Instance.Shader, fogDensityLoc, fogDensity, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);

        PlayMusicStream(ResourceManager.Instance.Music["1_iCutMyself"]);
    }

    public void Update(float deltaTime)
    {
        UpdateMusicStream(ResourceManager.Instance.Music["1_iCutMyself"]);
        var font = ResourceManager.Instance.Fonts["alagard"];
        UpdateCamera(deltaTime);
        BeginDrawing();
        {
            Render(deltaTime);
        }
        EndDrawing();

        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            StageManager.Instance.GoTo(Stage.Game);
        }
    }

    public void Deinit()
    {
        StopMusicStream(ResourceManager.Instance.Music["1_iCutMyself"]);
    }

    private void UpdateCamera(float deltaTime)
    {
        var movement = _moves[_currentMoveIndex];
        _camera.position += movement.Delta;

        if (movement.IsPan)
        {
            _camera.target += movement.Delta;
        }

        if (movement.Delta.X < 0 && _camera.position.X < movement.EndPosition.X)
        {
            NextMovement();
        }

        if (movement.Delta.X > 0 && _camera.position.X > movement.EndPosition.X)
        {
            NextMovement();
        }

        if (movement.Delta.Z < 0 && _camera.position.Z < movement.EndPosition.Z)
        {
            NextMovement();
        }

        if (movement.Delta.Z > 0 && _camera.position.Z > movement.EndPosition.Z)
        {
            NextMovement();
        }
    }

    private void NextMovement()
    {
        _currentMoveIndex += 1;
        if (_currentMoveIndex >= _moves.Count)
        {
            _currentMoveIndex = 0;
        }

        _camera.position = _moves[_currentMoveIndex].StartPosition;
        _camera.target = _moves[_currentMoveIndex].Target;
    }

    private unsafe void Render(float deltaTime)
    {
        var font = ResourceManager.Instance.Fonts["alagard"];

        BeginTextureMode(_renderTexture);
        {
            ClearBackground(Color.BLACK);

            BeginMode3D(_camera);
            {
                foreach (var entity in Entities)
                {
                    switch (entity)
                    {
                        case Terrain tn:
                            DrawModel(tn.Model, tn.Position, 1, Color.WHITE);
                            break;
                        case Door d:
                            DrawModelEx(d.Model, d.Position, new Vector3(1, 0, 0), 180, Vector3.One, Color.WHITE);
                            break;
                        case Item i:
                            DrawModelEx(i.Model, i.Position, new Vector3(1, 0, 0), 180, Vector3.One, Color.WHITE);
                            break;
                    }
                }

                BeginShaderMode(ResourceManager.Instance.Shader);
                {
                    foreach (var entity in Entities)
                    {
                        switch (entity)
                        {
                            case Billboard b:
                                var q = MatrixIdentity();
                                SetShaderValueMatrix(ResourceManager.Instance.Shader, ResourceManager.Instance.Shader.locs[(int)SHADER_LOC_MATRIX_MODEL], q);
                                DrawBillboardRec(
                                    _camera,
                                    b.Texture,
                                    new Rectangle { x = 0, y = 0, width = b.Texture.width, height = b.Texture.height },
                                    b.Position,
                                    new Vector2(b.Scale.X, b.Scale.Y),
                                    Color.WHITE
                                );
                                break;
                        }
                    }
                }
                EndShaderMode();
            }
            EndMode3D();

            DrawTextEx(font, "MOUSE OR MAN?", new Vector2(33, 33), 90, 1, Color.BLACK);
            DrawTextEx(font, "MOUSE OR MAN?", new Vector2(30, 30), 90, 1, Color.RED);

            DrawTextEx(font, "CLICK TO START", new Vector2(33, 923), 60, 1, Color.BLACK);
            DrawTextEx(font, "CLICK TO START", new Vector2(30, 920), 60, 1, Color.WHITE);

            DrawTextureEx(_cursor, new Vector2(ScreenInfo.MouseX, ScreenInfo.MouseY), 0.0f, 2f, Color.WHITE);
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

public class CameraMovement
{
    public Vector3 StartPosition { get; set; }
    public Vector3 EndPosition { get; set; }
    public Vector3 Target { get; set; }
    public Vector3 Delta { get; set; }
    public bool IsPan { get; set; } = false;
}