using System.Numerics;

public static class TailorGardenSceneData
{
    public static SceneData GetData()
    {
        var data = new SceneData
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
                new ItemData{
                    GridPos = new Vector3(1, 0, 6),
                    HoverText = "You need something to cut them with.",
                    ItemKind = ItemKind.None,
                    LocalPos = new Vector3(0, -0.6f, 0),
                    Scale = Vector3.One,
                    Side = Direction.South,
                    TextureName = "bush_d",
                },
            }
        };

        data.Entities.AddRange(ForestGen.GenerateGarden());
        return data;
    }
}