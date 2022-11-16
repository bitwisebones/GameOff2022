using System.Numerics;

public static class ManorHouseSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "manorhouse",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "manorhouse",
            },
            new DoorData{
                GridPos = new Vector3(12, 0, 7),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.East,
                HoverText = "Exit the Manor",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.ManorGrounds,
            },
            new DoorData{
                GridPos = new Vector3(6, 0, 4),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.East,
                HoverText = "Exit the Manor",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Sewer,
            }
        }
    };
}