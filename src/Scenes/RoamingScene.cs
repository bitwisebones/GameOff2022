
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

    private double _nextCheese = GetTime() + 60;
    private bool _playCheeseSound = true;

    private Entity? _hovered;
    private bool _isInventoryOpen;
    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);
    private Random _rnd = new Random(3456345);

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
            // check to see if we're in a safe space to transform'
            if (NavigationGrid!.CanTransform(gameState.PlayerGridPos))
            {
                gameState.PlayerMode = gameState.PlayerMode == PlayerMode.Mouse ? PlayerMode.Man : PlayerMode.Mouse;
                PlaySound(ResourceManager.Instance.Sounds["transform"]);
            }
            else
            {
                PlaySound(ResourceManager.Instance.Sounds["wrong"]);
            }
        }
        if (IsKeyPressed(KeyboardKey.KEY_TAB))
        {
            _isInventoryOpen = !_isInventoryOpen;
        }
        if (IsKeyPressed(KeyboardKey.KEY_GRAVE))
        {
            _isDebug = !_isDebug;
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
                var margin = 40;
                DrawRectangle(margin, 0, ScreenInfo.RenderWidth - margin * 2, 64 * 4, Color.BLACK);
                var i = 0;
                foreach (var item in RootGameState.Instance.Inventory)
                {
                    var isHovering = IsHoveringInventoryItem(i * (64 * 4) + margin);

                    // draw background highlight
                    if (isHovering)
                    {
                        DrawRectangle(i * (64 * 4) + margin, 0, 64 * 4, 64 * 4, Color.GRAY);
                    }

                    // then draw item sprite
                    var itemData = RootGameState.Instance.ItemCache[item];
                    DrawTextureEx(itemData.Texture, new Vector2(i * (64 * 4) + margin, 0), 0.0f, 4f, Color.WHITE);

                    // finally, draw text overlay
                    if (isHovering)
                    {
                        var data = RootGameState.Instance.ItemCache[item];
                        var hoverTextPos = new Vector2(ScreenInfo.MouseX + 80, ScreenInfo.MouseY + 50);
                        DrawTextEx(ResourceManager.Instance.Fonts["alagard"], data.GetHoverText(), hoverTextPos + new Vector2(3, 3), 36, 1, Color.BLACK);
                        DrawTextEx(ResourceManager.Instance.Fonts["alagard"], data.GetHoverText(), hoverTextPos, 36, 1, Color.WHITE);
                    }
                    i += 1;
                }
            }

            if (_isActive)
            {
                var cursorTexture = _hovered == null ? "cursor" : "cursor_hover";
                DrawTextureEx(ResourceManager.Instance.Textures[cursorTexture], new Vector2(ScreenInfo.MouseX, ScreenInfo.MouseY), 0.0f, 2, Color.WHITE);
            }

            if (_hovered != null && _hovered.GetHoverText() != null && !string.IsNullOrEmpty(_hovered.GetHoverText()))
            {
                if (_isActive)
                {
                    var hoverTextPos = new Vector2(ScreenInfo.MouseX + 80, ScreenInfo.MouseY + 50);
                    DrawTextEx(ResourceManager.Instance.Fonts["alagard"], _hovered.GetHoverText(), hoverTextPos + new Vector2(3, 3), 36, 1, Color.BLACK);
                    DrawTextEx(ResourceManager.Instance.Fonts["alagard"], _hovered.GetHoverText(), hoverTextPos, 36, 1, Color.WHITE);
                }
            }
            if (_isDebug)
            {
                DrawFPS(10, 10);
            }


            if (GetTime() > _nextCheese)
            {
                if (_playCheeseSound)
                {
                    PlaySound(ResourceManager.Instance.Sounds["cheese"]);
                    _playCheeseSound = false;
                }

                DrawTexture(ResourceManager.Instance.Textures["cheese"], 235, 0, Color.WHITE);
                if (GetTime() > _nextCheese + 0.1)
                {
                    _nextCheese = GetTime() + (int)Math.Floor((_rnd.NextSingle() * 60) + 180);
                    _playCheeseSound = true;
                }
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
                        if (RootGameState.Instance.PlayerMode == PlayerMode.Man)
                        {
                            _hovered = p;
                        }
                        break;
                    case Door d:
                        if (RootGameState.Instance.PlayerMode == PlayerMode.Man)
                        {
                            _hovered = d;
                            var model = d.Model;
                            var texture = d.HoverTexture;
                            SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);
                        }
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
                    if (RootGameState.Instance.PlayerMode == PlayerMode.Man)
                    {
                        var newArea = Scenes.GetAreaFromDoor(d.DoorKind);
                        if (newArea != AreaKind.None)
                        {
                            SceneManager.Instance.TransitionTo(newArea);
                        }
                    }
                    break;
                case Person p:
                    if (RootGameState.Instance.PlayerMode == PlayerMode.Man)
                    {
                        gameState.CurrentConversationTarget = p.PersonKind;
                        var dialogueScene = new DialogueScene();
                        SceneManager.Instance.Push(dialogueScene);
                    }
                    break;
                case Item i:
                    if (i.ItemKind == ItemKind.None)
                    {
                        return;
                    }

                    // TODO - move this garbage elsewhere
                    if (i.ItemKind == ItemKind.RubblePile)
                    {
                        if (RootGameState.Instance.Inventory.Contains(ItemKind.Pickaxe))
                        {
                            var sewer = (RoamingScene)RootGameState.Instance.SceneCache[AreaKind.Sewer];
                            var pile = (Item)sewer.Entities.Where(e => e is Item).First(e => ((Item)e).ItemKind == ItemKind.RubblePile);
                            sewer.Entities.Remove(pile);
                            sewer.NavigationGrid!.AddTile((14, 3));
                            PlaySound(ResourceManager.Instance.Sounds["explosion"]);
                            SceneManager.Instance.Push(new TextScene(new List<string>{
                                "You smash the pile of rubble!",
                                "    [click to continue]",
                            }, 650));

                            return;
                        }

                        RootGameState.Instance.IsLookingForPick = true;
                        SceneManager.Instance.Push(new TextScene(new List<string>{
                            "       There's a pile of rubble blocking the path.",
                            "It looks like you might be able to remove it with a tool.",
                            "                        [click to continue]",
                        }, 400));
                        return;
                    }

                    if (i.ItemKind == ItemKind.Note)
                    {
                        SceneManager.Instance.Push(new TextScene(new List<string>{
                            "      DO NOT ENTER",
                            "AREA UNSAFE FOR TOWNSFOLK",
                            "     -Father Brooks",
                            "    [click to continue]",
                        }, 650));
                        return;
                    }

                    if (i.ItemKind == ItemKind.SewerKey)
                    {
                        var woods = RootGameState.Instance.SceneCache[AreaKind.Woods];
                        var door = (Door)woods.Entities.Where(e => e is Door).First(e => ((Door)e).DoorKind == DoorKind.None);
                        door.HoverText = "Enter the sewer.";
                        door.DoorKind = DoorKind.Sewer;
                    }

                    if (i.ItemKind == ItemKind.Flowers)
                    {
                        PlaySound(ResourceManager.Instance.Sounds["cut"]);
                    }

                    if (!gameState.Inventory.Contains(i.ItemKind))
                    {
                        gameState.Inventory.Add(i.ItemKind);
                        PlaySound(ResourceManager.Instance.Sounds["pickup"]);
                    }
                    Entities.Remove(_hovered);
                    _hovered = null;
                    break;
            }
        }
    }

    private bool IsHoveringInventoryItem(int x)
    {
        var mouseX = ScreenInfo.MouseX;
        var mouseY = ScreenInfo.MouseY;
        return mouseX > x && mouseX < x + (64 * 4) && mouseY > 0 && mouseY < (64 * 4);
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