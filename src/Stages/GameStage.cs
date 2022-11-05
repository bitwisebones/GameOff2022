
using System.Linq;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public enum PlayerMode
{
    Man,
    Mouse,
}

public class GameStage : IStage
{
    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(0, 2, 0),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    private RenderTexture2D _renderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);

    private Vector3 _playerGridPos = new Vector3(1, 0, 1);
    private Direction _playerDir = Direction.East;
    private PlayerMode _playerMode = PlayerMode.Man;
    private float _cameraHeight => _playerMode == PlayerMode.Man ? 1f : 0.15f;

    private Model _model;

    private NavigationGrid _navGrid = new NavigationGrid();

    public unsafe void Init()
    {
        _model = ResourceManager.Instance.Models["town"];
        var texture = ResourceManager.Instance.Textures["town"];
        SetMaterialTexture(ref _model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        _navGrid.Build("town");

        HideCursor();
    }

    public void Update(float deltaTime)
    {
        if (IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            _playerMode = _playerMode == PlayerMode.Mouse ? PlayerMode.Man : PlayerMode.Mouse;
        }

        UpdatePlayer();
        UpdateCamera();

        BeginDrawing();
        {
            BeginTextureMode(_renderTexture);
            {
                ClearBackground(Color.BLACK);

                BeginMode3D(_camera);
                {
                    // _navGrid.DebugDraw();
                    // DebugDrawCardinalDirections();
                    DrawModel(_model, new Vector3(0, 0, 0), 1, Color.WHITE);
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

    private unsafe bool CanMoveForward()
    {
        var dirVector = _playerDir switch
        {
            Direction.North => new Vector3(0, 0, -1),
            Direction.South => new Vector3(0, 0, 1),
            Direction.East => new Vector3(1, 0, 0),
            _ => new Vector3(-1, 0, 0),
        };

        var potentialPos = _playerGridPos + dirVector;
        return _navGrid.CanNavigateToGridPos(potentialPos, _playerMode);
    }

    private unsafe bool CanMoveBackward()
    {
        var dirVector = _playerDir switch
        {
            Direction.North => new Vector3(0, 0, 1),
            Direction.South => new Vector3(0, 0, -1),
            Direction.East => new Vector3(-1, 0, 0),
            _ => new Vector3(1, 0, 0),
        };

        var potentialPos = _playerGridPos + dirVector;
        return _navGrid.CanNavigateToGridPos(potentialPos, _playerMode);
    }

    private void UpdatePlayer()
    {
        if (IsKeyPressed(KeyboardKey.KEY_W))
        {
            if (!CanMoveForward())
            {
                return;
            }
            switch (_playerDir)
            {
                case Direction.North:
                    _playerGridPos.Z -= 1;
                    break;
                case Direction.South:
                    _playerGridPos.Z += 1;
                    break;
                case Direction.East:
                    _playerGridPos.X += 1;
                    break;
                case Direction.West:
                    _playerGridPos.X -= 1;
                    break;
            }
        }

        if (IsKeyPressed(KeyboardKey.KEY_S))
        {
            if (!CanMoveBackward())
            {
                return;
            }
            switch (_playerDir)
            {
                case Direction.North:
                    _playerGridPos.Z += 1;
                    break;
                case Direction.South:
                    _playerGridPos.Z -= 1;
                    break;
                case Direction.East:
                    _playerGridPos.X -= 1;
                    break;
                case Direction.West:
                    _playerGridPos.X += 1;
                    break;
            }
        }

        if (IsKeyPressed(KeyboardKey.KEY_D))
        {
            _playerDir += 1;
            if (_playerDir > Direction.West)
            {
                _playerDir = Direction.North;
            }
        }

        if (IsKeyPressed(KeyboardKey.KEY_A))
        {
            _playerDir -= 1;
            if (_playerDir < Direction.North)
            {
                _playerDir = Direction.West;
            }
        }
    }

    private void UpdateCamera()
    {
        var playerWorldPos = Grid.ToWorld(_playerGridPos);
        var deltaX = _camera.position.X - playerWorldPos.X;
        var deltaZ = _camera.position.Z - playerWorldPos.Z;

        _camera.position.X -= deltaX;
        _camera.position.Z -= deltaZ;
        _camera.position.Y = _cameraHeight;

        var target = _playerDir switch
        {
            Direction.North => new Vector3(playerWorldPos.X, _cameraHeight, playerWorldPos.Z - 1),
            Direction.South => new Vector3(playerWorldPos.X, _cameraHeight, playerWorldPos.Z + 1),
            Direction.East => new Vector3(playerWorldPos.X + 1, _cameraHeight, playerWorldPos.Z),
            _ => new Vector3(playerWorldPos.X - 1, _cameraHeight, playerWorldPos.Z),
        };

        _camera.target = target;
    }

    private void DebugDrawCardinalDirections()
    {
        var north = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, -1));
        var east = new Ray(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
        var south = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, 1));
        var west = new Ray(new Vector3(0, 0, 0), new Vector3(-1, 0, 0));
        DrawRay(north, Color.RED);
        DrawRay(east, Color.GREEN);
        DrawRay(south, Color.BLUE);
        DrawRay(west, Color.MAGENTA);
    }
}