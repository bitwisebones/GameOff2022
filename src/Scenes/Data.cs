
using System.Numerics;

public static class Doors
{
    public const string Inn = "inn_door";
    public const string Church = "church_door";
    public const string Town = "town_door";
}

public static class Items
{
    public const string Key = "key";
}

public static class Scenes
{
    public static SceneData Town = new SceneData
    {
        Name = "town",
        PlayerSpawnDirection = Direction.South,
        PlayerSpawnGridPos = new Vector3(1, 0, 1),
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new EntityData{
                Name = "town_terrain",
                RenderType = RenderType.Model,
                Texture = "town",
                Model = "town",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
            },
            new EntityData{
                Name = "person",
                RenderType = RenderType.Billboard,
                Texture = "person",
                GridPos = new Vector3(6, 0, 9),
                LocalPos = new Vector3(0, -0.25f, 0),
                IsInteractable = true,
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to Person",
                InteractionType = InteractionType.Person,
            },
            new EntityData{
                Name = Doors.Inn,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(4, 0, 12),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Enter The Inn",
                Scale = new Vector3(1, 1, 1),
                InteractionType = InteractionType.Door,
            },
            new EntityData{
                Name = Doors.Church,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(7, 0, 5),
                IsInteractable = true,
                Side = Direction.North,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Enter The Church",
                Scale = new Vector3(1, 1, 1),
                InteractionType = InteractionType.Door,
            },
            new EntityData
            {
                Name = "key",
                RenderType = RenderType.Quad,
                Texture = "key",
                GridPos = new Vector3(9, 0, 11),
                Side = Direction.South,
                IsInteractable = true,
                HoverText = "A small brass skeleton key",
                LocalPos = new Vector3(0, -0.75f, 0),
                Scale = new Vector3(0.5f, 0.5f, 0.5f),
                InteractionType = InteractionType.Item,
            }
        }
    };

    public static SceneData Church = new SceneData
    {
        Name = "church",
        SceneType = SceneType.Roaming,
        PlayerSpawnDirection = Direction.North,
        PlayerSpawnGridPos = new Vector3(3, 0, 7),
        Entities = new List<EntityData>()
        {
            new EntityData{
                Name = "church_terrain",
                RenderType = RenderType.Model,
                Texture = "town",
                Model = "church",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
            },
            new EntityData{
                Name = Doors.Town,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(3, 0, 7),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Exit The Church",
                Scale = new Vector3(1, 1, 1),
                InteractionType = InteractionType.Door,
            }
        }
    };

    public static SceneData Inn = new SceneData
    {
        Name = "inn",
        SceneType = SceneType.Roaming,
        PlayerSpawnDirection = Direction.South,
        PlayerSpawnGridPos = new Vector3(0, 0, 0),
        Entities = new List<EntityData>
        {
            new EntityData{
                Name = "inn_terrain",
                RenderType = RenderType.Model,
                Texture = "town",
                Model = "inn",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
            },
            new EntityData{
                Name = Doors.Town,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(2, 0, 3),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Exit The Inn",
                Scale = new Vector3(1, 1, 1),
                InteractionType = InteractionType.Door,
            }
        }
    };

    public static Dictionary<(Area, Area), (Vector3, Direction)> Transitions = new Dictionary<(Area, Area), (Vector3, Direction)>()
    {
        {(Area.Town, Area.Church), (new Vector3(3, 0, 7), Direction.North)},
        {(Area.Church, Area.Town), (new Vector3(7, 0, 5), Direction.South)},
        {(Area.Town, Area.Inn), (new Vector3(2, 0, 3), Direction.North)},
        {(Area.Inn, Area.Town), (new Vector3(4, 0, 12), Direction.North)},
    };

    public static IScene GetScene(Area newArea, Area currentArea, RootGameState gameState)
    {
        var scene = gameState.Scenes[newArea];
        var (pos, dir) = Transitions[(currentArea, newArea)];
        scene.SceneData.PlayerSpawnDirection = dir;
        scene.SceneData.PlayerSpawnGridPos = pos;
        return scene;
    }
}