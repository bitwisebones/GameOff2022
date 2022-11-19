
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Raymath;
using static Raylib_cs.ShaderLocationIndex;

// Player can walk around and click on things
public class RoamingScene : IScene
{
    public NavigationGrid? NavigationGrid { get; set; }
    public List<Entity> Entities { get; set; } = new List<Entity>();
    public Vector4 FogColor { get; set; } = new Vector4(1, 1, 1, 1);
    public float FogDensity { get; set; } = 0f;

    private bool _isActive => SceneManager.Instance.Peek() == this;
    private bool _isDebug = false;

    private Entity? _hovered;
    private bool _isInventoryOpen;
    private RenderTexture2D _renderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);

    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(0, 2, 0),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    public void Init()
    {
        var fogDensityLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogDensity");
        var fogColorLoc = GetShaderLocation(ResourceManager.Instance.Shader, "fogColor");

        SetShaderValue(ResourceManager.Instance.Shader, fogColorLoc, FogColor, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
        SetShaderValue(ResourceManager.Instance.Shader, fogDensityLoc, FogDensity, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);
    }

    public unsafe void Update(float deltaTime)
    {
        var gameState = RootGameState.Instance;

        if (IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            gameState.PlayerMode = gameState.PlayerMode == PlayerMode.Mouse ? PlayerMode.Man : PlayerMode.Mouse;
        }
        if (IsKeyPressed(KeyboardKey.KEY_TAB))
        {
            _isInventoryOpen = !_isInventoryOpen;
        }
        if (IsKeyPressed(KeyboardKey.KEY_GRAVE))
        {
            _isDebug = !_isDebug;
        }
        if (IsKeyPressed(KeyboardKey.KEY_F5))
        {
            ResourceManager.Instance.ReloadLoadAll();
        }

        CheckForHover();
        CheckClicks(gameState);
        UpdatePlayer(gameState);
        UpdateCamera(gameState.PlayerGridPos, gameState.PlayerDirection, gameState.PlayerMode);
        SetShaderValue(ResourceManager.Instance.Shader, ResourceManager.Instance.Shader.locs[(int)SHADER_LOC_VECTOR_VIEW], _camera.position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
    }

    public unsafe void Render(float deltaTime)
    {

        BeginTextureMode(_renderTexture);
        {
            ClearBackground(Color.BLACK);

            BeginMode3D(_camera);
            {
                if (_isDebug)
                {
                    NavigationGrid!.DebugDraw();
                    DebugDrawCardinalDirections();
                }

                foreach (var entity in Entities)
                {
                    switch (entity)
                    {
                        case Terrain tn:
                            DrawModel(tn.Model, tn.Position, 1, Color.WHITE);
                            break;
                        case Door d:
                            DrawModelEx(d.Model, d.Position, new Vector3(1, 0, 0), 180, Vector3.One, Color.WHITE);
                            if (_isDebug)
                            {
                                DrawBoundingBox(d.GetBoundingBox(), Color.BLUE);
                            }
                            break;
                        case Item i:
                            DrawModelEx(i.Model, i.Position, new Vector3(1, 0, 0), 180, Vector3.One, Color.WHITE);
                            if (_isDebug)
                            {
                                DrawBoundingBox(i.GetBoundingBox(), Color.BLUE);
                            }
                            break;
                    }
                }

                BeginShaderMode(ResourceManager.Instance.Shader);
                {
                    foreach (var entity in Entities)
                    {
                        switch (entity)
                        {
                            case Person p:
                                var t = _hovered == p ? p.HoverTexture : p.Texture;
                                var m = MatrixIdentity();
                                SetShaderValueMatrix(ResourceManager.Instance.Shader, ResourceManager.Instance.Shader.locs[(int)SHADER_LOC_MATRIX_MODEL], m);
                                DrawBillboardRec(
                                    _camera,
                                    t,
                                    new Rectangle { x = 0, y = 0, width = p.Texture.width, height = p.Texture.height },
                                    p.Position,
                                    new Vector2(p.Scale.X, p.Scale.Y),
                                    Color.WHITE
                                );
                                if (_isDebug)
                                {
                                    DrawBoundingBox(p.GetBoundingBox(), Color.BLUE);
                                }
                                break;
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
                                if (_isDebug)
                                {
                                    DrawBoundingBox(b.GetBoundingBox(), Color.BLUE);
                                }
                                break;
                        }
                    }
                }
                EndShaderMode();
            }
            EndMode3D();

            if (_isInventoryOpen)
            {
                var margin = 10;
                DrawRectangle(margin, 0, GetScreenWidth() / ScreenInfo.Crunch - margin * 2, 64, Color.BLACK);
                var i = 0;
                foreach (var item in RootGameState.Instance.Inventory)
                {
                    if (IsHoveringInventoryItem(i * 64 + margin))
                    {
                        DrawRectangle(i * 64 + margin, 0, 64, 64, Color.GRAY);
                    }

                    var itemData = RootGameState.Instance.ItemCache[item];
                    DrawTexture(itemData.Texture, i * 64 + margin, 0, Color.WHITE);
                    i += 1;
                }
            }

            if (_isActive)
            {
                var cursorTexture = _hovered == null ? "cursor" : "cursor_hover";
                DrawTextureEx(ResourceManager.Instance.Textures[cursorTexture], new Vector2(GetMouseX() / ScreenInfo.Crunch, GetMouseY() / ScreenInfo.Crunch), 0.0f, 0.5f, Color.WHITE);
            }
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

        if (_hovered != null && _hovered.GetHoverText() != null && !string.IsNullOrEmpty(_hovered.GetHoverText()))
        {
            if (_isActive)
            {
                var hoverTextPos = new Vector2(GetMouseX() + 80, GetMouseY() + 50);
                DrawTextEx(ResourceManager.Instance.Fonts["alagard"], _hovered.GetHoverText(), hoverTextPos + new Vector2(3, 3), 36, 1, Color.BLACK);
                DrawTextEx(ResourceManager.Instance.Fonts["alagard"], _hovered.GetHoverText(), hoverTextPos, 36, 1, Color.WHITE);
            }
        }
        if (_isDebug)
        {
            DrawFPS(10, 10);
        }
    }

    private bool CanMoveForward(RootGameState gameState)
    {
        var dirVector = gameState.PlayerDirection switch
        {
            Direction.North => new Vector3(0, 0, -1),
            Direction.South => new Vector3(0, 0, 1),
            Direction.East => new Vector3(1, 0, 0),
            _ => new Vector3(-1, 0, 0),
        };

        var potentialPos = gameState.PlayerGridPos + dirVector;
        return NavigationGrid!.CanNavigateToGridPos(potentialPos, gameState.PlayerMode);
    }

    private bool CanMoveBackward(RootGameState gameState)
    {
        var dirVector = gameState.PlayerDirection switch
        {
            Direction.North => new Vector3(0, 0, 1),
            Direction.South => new Vector3(0, 0, -1),
            Direction.East => new Vector3(-1, 0, 0),
            _ => new Vector3(1, 0, 0),
        };

        var potentialPos = gameState.PlayerGridPos + dirVector;
        return NavigationGrid!.CanNavigateToGridPos(potentialPos, gameState.PlayerMode);
    }

    private void UpdatePlayer(RootGameState gameState)
    {
        var moved = false;
        if (IsKeyPressed(KeyboardKey.KEY_W))
        {
            if (!CanMoveForward(gameState) && !_isDebug)
            {
                return;
            }
            var pos = gameState.PlayerGridPos;
            switch (gameState.PlayerDirection)
            {
                case Direction.North:
                    pos.Z -= 1;
                    break;
                case Direction.South:
                    pos.Z += 1;
                    break;
                case Direction.East:
                    pos.X += 1;
                    break;
                case Direction.West:
                    pos.X -= 1;
                    break;
            }
            moved = true;
            gameState.PlayerGridPos = pos;
        }

        if (IsKeyPressed(KeyboardKey.KEY_S))
        {
            if (!CanMoveBackward(gameState) && !_isDebug)
            {
                return;
            }

            var pos = gameState.PlayerGridPos;
            switch (gameState.PlayerDirection)
            {
                case Direction.North:
                    pos.Z += 1;
                    break;
                case Direction.South:
                    pos.Z -= 1;
                    break;
                case Direction.East:
                    pos.X -= 1;
                    break;
                case Direction.West:
                    pos.X += 1;
                    break;
            }
            moved = true;
            gameState.PlayerGridPos = pos;
        }

        if (IsKeyPressed(KeyboardKey.KEY_D))
        {
            moved = true;
            gameState.PlayerDirection += 1;
            if (gameState.PlayerDirection > Direction.West)
            {
                gameState.PlayerDirection = Direction.North;
            }
        }

        if (IsKeyPressed(KeyboardKey.KEY_A))
        {
            moved = true;
            gameState.PlayerDirection -= 1;
            if (gameState.PlayerDirection < Direction.North)
            {
                gameState.PlayerDirection = Direction.West;
            }
        }

        if (moved && _isInventoryOpen)
        {
            _isInventoryOpen = false;
        }
    }

    private void UpdateCamera(Vector3 playerGridPos, Direction direction, PlayerMode mode)
    {
        var playerWorldPos = Grid.ToWorld(playerGridPos);
        var deltaX = _camera.position.X - playerWorldPos.X;
        var deltaZ = _camera.position.Z - playerWorldPos.Z;

        var cameraHeight = mode == PlayerMode.Man ? 1f : 0.15f;

        _camera.position.X -= deltaX;
        _camera.position.Z -= deltaZ;
        _camera.position.Y = cameraHeight;

        var target = direction switch
        {
            Direction.North => new Vector3(playerWorldPos.X, cameraHeight, playerWorldPos.Z - 1),
            Direction.South => new Vector3(playerWorldPos.X, cameraHeight, playerWorldPos.Z + 1),
            Direction.East => new Vector3(playerWorldPos.X + 1, cameraHeight, playerWorldPos.Z),
            _ => new Vector3(playerWorldPos.X - 1, cameraHeight, playerWorldPos.Z),
        };

        _camera.target = target;
    }

    private bool IsInteractable(Entity e) => e switch
    {
        Terrain => false,
        _ => true,
    };

    private unsafe void CheckForHover()
    {
        var ray = GetMouseRay(GetMousePosition(), _camera);
        var es = Entities.Where(e => IsInteractable(e));
        var oldHovered = _hovered;
        _hovered = null;
        foreach (var entity in es)
        {
            var collisionA = GetRayCollisionBox(ray, entity.GetBoundingBox());
            if (collisionA.hit && collisionA.distance < 3.5f)
            {
                switch (entity)
                {
                    case Person p:
                        _hovered = p;
                        break;
                    case Door d:
                        _hovered = d;
                        var model = d.Model;
                        var texture = d.HoverTexture;
                        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);
                        break;
                    case Item i:
                        _hovered = i;
                        var itemTexture = i.HoverTexture;
                        var itemModel = i.Model;
                        SetMaterialTexture(ref itemModel, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref itemTexture);
                        break;
                }
            }
        }

        if (_hovered == null && oldHovered != null || (_hovered != null && oldHovered != null && _hovered != oldHovered))
        {
            switch (oldHovered)
            {
                case Door d:
                    var doorModel = d.Model;
                    var doorTexture = d.Texture;
                    SetMaterialTexture(ref doorModel, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref doorTexture);
                    break;
                case Item i:
                    var itemModel = i.Model;
                    var itemTexture = i.Texture;
                    SetMaterialTexture(ref itemModel, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref itemTexture);
                    break;
            }
        }

    }

    private void CheckClicks(RootGameState gameState)
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && _hovered != null)
        {
            _isInventoryOpen = false;
            switch (_hovered)
            {
                case Door d:
                    var newArea = Scenes.GetAreaFromDoor(d.DoorKind);
                    if (newArea != AreaKind.None)
                    {
                        SceneManager.Instance.TransitionTo(newArea);
                    }
                    break;
                case Person p:
                    gameState.CurrentConversationTarget = p.PersonKind;
                    var dialogueScene = new DialogueScene();
                    SceneManager.Instance.Push(dialogueScene);
                    break;
                case Item i:
                    if (!gameState.Inventory.Contains(i.ItemKind))
                    {
                        gameState.Inventory.Add(i.ItemKind);
                    }
                    Entities.Remove(_hovered);
                    _hovered = null;
                    break;
            }
        }
    }

    private bool IsHoveringInventoryItem(int x)
    {
        var mouseX = GetMouseX() / ScreenInfo.Crunch;
        var mouseY = GetMouseY() / ScreenInfo.Crunch;
        return mouseX > x && mouseX < x + 64 && mouseY > 0 && mouseY < 64;
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