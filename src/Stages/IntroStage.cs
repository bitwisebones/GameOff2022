using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;
using static Raylib_cs.ShaderLocationIndex;

public class IntroStage : IStage
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

    private Texture2D _cursor;

    private static List<string> _text = new List<string>
    {
        "The full moon beckons...",
        "The pale yellow disk reminds you that you must feed.",
        "FEED ON CHEESE.",
        "As a weremouse, you must feed to survive.",
        "And you've arrived here in Cheeseshire to do just that.",
        "Find the cheese. Feed. Survive.",
    };

    private static List<int> _delay = new List<int>
    {
        4,
        5,
        3,
        3,
        5,
        4,
    };

    private int _currentTextIndex = 0;
    private double _nextTextTime = GetTime() + _delay[0];
    private float _transitionElapsed = 0;

    public void Init()
    {
        _cursor = ResourceManager.Instance.Textures["cursor"];

        SetCameraMode(_camera, CameraMode.CAMERA_CUSTOM);
        HideCursor();

        var s = SceneFactory.Build(TownSceneData.GetData());
        Entities = s.Entities;

        var moonModel = ResourceManager.Instance.Models["moon"];
        var moonTexture = ResourceManager.Instance.Textures["moon"];
        SetMaterialTexture(ref moonModel, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref moonTexture);
        var shader = ResourceManager.Instance.Shader;
        SetMaterialShader(ref moonModel, 0, ref shader);

        Entities.Add(new Terrain
        {
            Model = moonModel,
            Texture = moonTexture,
            Position = new Vector3(30, 25, 10),
        });

        _camera.position = new Vector3(10, 0.1f, 18);
        _camera.target = new Vector3(19, 12, 8);

        var fogDensityLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogDensity");
        var fogColorLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogColor");

        var fogColor = new Vector4(0.3f, 0.3f, 0.3f, 1);
        var fogDensity = 0.01f;

        SetShaderValue(ResourceManager.Instance.Shader, fogColorLoc, fogColor, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
        SetShaderValue(ResourceManager.Instance.Shader, fogDensityLoc, fogDensity, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);

        PlayMusicStream(ResourceManager.Instance.Music["4_mittelalter"]);
    }

    public void Update(float deltaTime)
    {
        UpdateMusicStream(ResourceManager.Instance.Music["4_mittelalter"]);
        if (_currentTextIndex > 0 && IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            StageManager.Instance.GoTo(Stage.Game);
        }

        _camera.position.X += 0.001f;
        BeginDrawing();
        {
            Render(deltaTime);
        }
        EndDrawing();
    }

    private unsafe void Render(float deltaTime)
    {
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

            var text = GetText(deltaTime);

            DrawTextEx(
                ResourceManager.Instance.Fonts["alagard"],
                text,
                new Vector2(52, 52),
                40,
                1,
                Color.BLACK
            );

            DrawTextEx(
                ResourceManager.Instance.Fonts["alagard"],
                text,
                new Vector2(50, 50),
                40,
                1,
                Color.WHITE
            );

            if (_currentTextIndex > 0)
            {
                DrawTextEx(ResourceManager.Instance.Fonts["alagard"], "CLICK TO SKIP", new Vector2(1650, 1000), 30, 1, Color.WHITE);
            }

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

    private string GetText(float deltaTime)
    {
        _transitionElapsed += deltaTime;

        if (GetTime() > _nextTextTime)
        {
            _currentTextIndex += 1;
            _transitionElapsed = 0;
            if (_currentTextIndex >= _text.Count)
            {
                StageManager.Instance.GoTo(Stage.Game);
                return "";
            }
            _nextTextTime = GetTime() + _delay[_currentTextIndex];
        }

        var text = _text[_currentTextIndex];
        var percent = _transitionElapsed / 2f;
        var strLen = (int)Math.Floor((float)text.Length * percent);
        strLen = strLen > text.Length ? text.Length : strLen;

        return text.Substring(0, strLen);
    }

    public void Deinit()
    {
        StopMusicStream(ResourceManager.Instance.Music["4_mittelalter"]);
    }
}