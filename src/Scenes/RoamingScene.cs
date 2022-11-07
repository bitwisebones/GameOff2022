
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

// Player can walk around and click on things
public class RoamingScene : IScene
{
    public SceneData SceneData { get; set; }
    public NavigationGrid? NavigationGrid { get; set; }
    public List<Entity> Entities { get; set; } = new List<Entity>();

    private bool _isActive => SceneManager.Instance.Peek() == this;

    private Entity? _hovered;
    private bool _isInventoryOpen;

    private Camera3D _camera = new Camera3D()
    {
        position = new Vector3(0, 2, 0),
        target = new Vector3(0, 2, 0),
        up = new Vector3(0, 1, 0),
        fovy = 60,
        projection = CameraProjection.CAMERA_PERSPECTIVE,
    };

    public RootGameState Update(float deltaTime, RootGameState gameState)
    {
        if (IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            gameState.PlayerMode = gameState.PlayerMode == PlayerMode.Mouse ? PlayerMode.Man : PlayerMode.Mouse;
        }
        if (IsKeyPressed(KeyboardKey.KEY_TAB))
        {
            _isInventoryOpen = !_isInventoryOpen;
        }

        CheckForHover();
        CheckClicks(gameState);
        UpdatePlayer(gameState);
        UpdateCamera(gameState.PlayerGridPos, gameState.PlayerDirection, gameState.PlayerMode);

        return gameState;
    }

    public void Render(float deltaTime, ref RenderBundle renderBundle, RootGameState gameState)
    {
        BeginDrawing();
        {
            BeginTextureMode(renderBundle.RenderTexture);
            {
                ClearBackground(Color.BLACK);

                BeginMode3D(_camera);
                {
                    // NavigationGrid!.DebugDraw();
                    // DebugDrawCardinalDirections();
                    foreach (var entity in Entities)
                    {
                        switch (entity.EntityType)
                        {
                            case RenderType.Model:
                                DrawModel(entity.Model, entity.Position, 1, Color.WHITE);
                                // DrawBoundingBox(entity.BoundingBox, Color.BLUE);
                                break;
                            case RenderType.Billboard:
                                var t = _hovered == entity ? entity.HoverTexture : entity.Texture;
                                DrawBillboardRec(
                                    _camera,
                                    t,
                                    new Rectangle { x = 0, y = 0, width = entity.Texture.width, height = entity.Texture.height },
                                    entity.Position,
                                    new Vector2(entity.Scale.X, entity.Scale.Y),
                                    Color.WHITE
                                );
                                // DrawBoundingBox(entity.BoundingBox, Color.BLUE);
                                break;
                            case RenderType.Quad:
                                DrawModelEx(entity.Model, entity.Position, new Vector3(1, 0, 0), 180, Vector3.One, Color.WHITE);
                                // DrawBoundingBox(entity.BoundingBox, Color.BLUE);
                                break;
                        }
                    }
                }
                EndMode3D();

                if (_isInventoryOpen)
                {
                    var margin = 10;
                    DrawRectangle(margin, 0, GetScreenWidth() / ScreenInfo.Crunch - margin * 2, 64, Color.BLACK);
                    var i = 0;
                    foreach (var item in gameState.Inventory)
                    {
                        if (IsHoveringInventoryItem(i * 64 + margin))
                        {
                            DrawRectangle(i * 64 + margin, 0, 64, 64, Color.GRAY);
                        }

                        var texture = ResourceManager.Instance.Textures[item];
                        DrawTexture(texture, i * 64 + margin, 0, Color.WHITE);
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
                renderBundle.RenderTexture.texture,
                new Rectangle(0, 0, -ScreenInfo.RenderWidth, ScreenInfo.RenderHeight),
                new Rectangle(ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight, ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight),
                new Vector2(0, 0),
                180,
                Color.WHITE
            );

            if (_hovered != null && _hovered.HoverText != null && !string.IsNullOrEmpty(_hovered.HoverText))
            {
                if (_isActive)
                {
                    DrawText(_hovered.HoverText, GetMouseX() + 80, GetMouseY() + 50, 36, Color.WHITE);
                }
            }
            DrawFPS(10, 10);
        }
        EndDrawing();
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
            if (!CanMoveForward(gameState))
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
            if (!CanMoveBackward(gameState))
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

    private unsafe void CheckForHover()
    {
        var ray = GetMouseRay(GetMousePosition(), _camera);
        var es = Entities.Where(e => e.IsInteractable);
        var oldHovered = _hovered;
        _hovered = null;
        foreach (var entity in es)
        {
            var collisionA = GetRayCollisionBox(ray, entity.BoundingBox);
            if (collisionA.hit && collisionA.distance < 3.5f)
            {
                // DrawCube(collisionA.point, 0.1f, 0.1f, 0.1f, Color.MAGENTA);
                switch (entity.EntityType)
                {
                    case RenderType.Model:
                    case RenderType.Quad:
                        _hovered = entity;
                        var model = entity.Model;
                        var texture = entity.HoverTexture;
                        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);
                        break;
                    case RenderType.Billboard:
                        _hovered = entity;
                        break;
                }
            }
        }

        if (_hovered == null && oldHovered != null || (_hovered != null && oldHovered != null && _hovered != oldHovered))
        {
            if (oldHovered.EntityType != RenderType.Billboard)
            {
                var model = oldHovered.Model;
                var texture = oldHovered.Texture;
                SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);
            }
        }

    }

    private void CheckClicks(RootGameState gameState)
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && _hovered != null)
        {
            _isInventoryOpen = false;
            switch (_hovered.InteractionType)
            {
                case InteractionType.Door:
                    var newArea = _hovered.Name switch
                    {
                        Doors.Church => Area.Church,
                        Doors.Inn => Area.Inn,
                        Doors.Town => Area.Town,
                        _ => throw new NotImplementedException($"{_hovered.Name} not implemented in RoamingScene->CheckClicks"),
                    };
                    var newScene = Scenes.GetScene(newArea, gameState.CurrentArea, gameState);
                    SceneManager.Instance.Replace(newScene);
                    gameState.PlayerDirection = newScene.SceneData.PlayerSpawnDirection;
                    gameState.PlayerGridPos = newScene.SceneData.PlayerSpawnGridPos;
                    gameState.CurrentArea = newArea;
                    break;
                case InteractionType.Person:
                    var dialogScene = new DialogScene();
                    SceneManager.Instance.Push(dialogScene);
                    break;
                case InteractionType.Flavor:
                    break;
                case InteractionType.Item:
                    gameState.Inventory.Add(_hovered.Name!);
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