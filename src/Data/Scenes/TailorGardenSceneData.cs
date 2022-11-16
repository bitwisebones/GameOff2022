using System.Numerics;

public static class TailorGardenSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "tailorgarden",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData> {
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                ModelName = "tailor",
                TextureName = "tailor",
            },
            new DoorData{
                GridPos = new Vector3(0, 0, 4),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_b",
                Side = Direction.North,
                HoverText = "Enter the Tailor's Shop",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Tailor,
            },
        }
    };
}