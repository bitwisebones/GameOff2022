
using System.Numerics;

public static class Const
{
    public const string InnDoor = "inn_door";
    public const string ChurchDoor = "church_door";
    public const string TownDoor = "town_door";

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
            },
            new EntityData{
                Name = Const.InnDoor,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(4, 0, 12),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Enter The Inn"
            },
            new EntityData{
                Name = Const.ChurchDoor,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(7, 0, 5),
                IsInteractable = true,
                Side = Direction.North,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Enter The Church"
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
                Name = Const.TownDoor,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(3, 0, 7),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Exit The Church"
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
                Name = Const.TownDoor,
                RenderType = RenderType.Quad,
                Texture = "door_a",
                GridPos = new Vector3(2, 0, 3),
                IsInteractable = true,
                Side = Direction.South,
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Exit The Inn"
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

    public static IScene GetScene(Area newArea, Area currentArea)
    {
        var scene = newArea switch
        {
            Area.Town => SceneFactory.Build(Scenes.Town),
            Area.Church => SceneFactory.Build(Scenes.Church),
            Area.Inn => SceneFactory.Build(Scenes.Inn),
            _ => throw new NotImplementedException(),
        };

        var (pos, dir) = Transitions[(currentArea, newArea)];
        scene.SceneData.PlayerSpawnDirection = dir;
        scene.SceneData.PlayerSpawnGridPos = pos;
        return scene;
    }
}