
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
            new EntityData{
                Name = "town_terrain",
                EntityType = EntityType.Model,
                Texture = "town",
                Model = "town",
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
            },
            new EntityData{
                Name = "test",
                EntityType = EntityType.Quad,
                Texture = "wall",
                GridPos = new Vector3(5, 0, 2),
                Side = Direction.East,
            },
            new EntityData{
                Name = "other",
                EntityType = EntityType.Billboard,
                Texture = "wall",
                GridPos = new Vector3(6, 0, 9),
                LocalPos = new Vector3(0, 0, 0),
            }
        }
    };
}