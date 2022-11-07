
using System.Numerics;

public static class Scenes
{
    public static SceneData Town = new SceneData
    {
        Name = "town",
        PlayerSpawnDirection = Direction.South,
        PlayerSpawnGridPos = new Vector3(1, 0, 1),
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new TerrainData{
                Name = "town_terrain",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                ModelName = "town",
                TextureName = "town",
                AreaKind = AreaKind.Town,
            },
            new PersonData{
                Name = "blacksmith",
                GridPos = new Vector3(6, 0, 9),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "person",
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to Person",
                PersonKind = PersonKind.Blacksmith,
            },
            new DoorData{
                Name = "inn_door",
                GridPos = new Vector3(4, 0, 12),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.South,
                HoverText = "Enter The Inn",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Inn,
            },
            new DoorData{
                Name = "church_door",
                GridPos = new Vector3(7, 0, 5),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.North,
                HoverText = "Enter The Church",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Church,
            },
            new ItemData
            {
                Name = "blacksmith_key",
                GridPos = new Vector3(9, 0, 11),
                LocalPos = new Vector3(0, -0.75f, 0),
                TextureName = "key",
                Side = Direction.South,
                HoverText = "A small brass skeleton key",
                Scale = new Vector3(0.5f, 0.5f, 0.5f),
                ItemKind = ItemKind.BlacksmithKey,
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
            new TerrainData{
                Name = "church_terrain",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "church",
                AreaKind = AreaKind.Church,
            },
            new DoorData{
                Name = "town_door",
                GridPos = new Vector3(3, 0, 7),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.South,
                HoverText = "Exit The Church",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Town,
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
            new TerrainData{
                Name = "inn_terrain",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "inn",
                AreaKind = AreaKind.Inn,
            },
            new DoorData{
                Name = "town_door",
                GridPos = new Vector3(2, 0, 3),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.South,
                HoverText = "Exit The Inn",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Town,
            }
        }
    };

    public static Dictionary<(AreaKind, AreaKind), (Vector3, Direction)> Transitions = new Dictionary<(AreaKind, AreaKind), (Vector3, Direction)>()
    {
        {(AreaKind.Town, AreaKind.Church), (new Vector3(3, 0, 7), Direction.North)},
        {(AreaKind.Church, AreaKind.Town), (new Vector3(7, 0, 5), Direction.South)},
        {(AreaKind.Town, AreaKind.Inn), (new Vector3(2, 0, 3), Direction.North)},
        {(AreaKind.Inn, AreaKind.Town), (new Vector3(4, 0, 12), Direction.North)},
    };

    public static (IScene, Vector3, Direction) GetScene(AreaKind newArea, AreaKind currentArea, RootGameState gameState)
    {
        var scene = gameState.Scenes[newArea];
        var (pos, dir) = Transitions[(currentArea, newArea)];
        return (scene, pos, dir);
    }

    public static AreaKind GetAreaFromDoor(DoorKind door)
    {
        return door switch
        {
            DoorKind.Church => AreaKind.Church,
            DoorKind.Inn => AreaKind.Inn,
            DoorKind.Town => AreaKind.Town,
            _ => throw new NotImplementedException($"{door} in GetAreaFromDoor"),
        };
    }
}