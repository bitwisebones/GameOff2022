
using System.Numerics;

public static class WoodsSceneData
{
    public static SceneData GetData()
    {
        var data = new SceneData
        {
            Name = "woods",
            SceneType = SceneType.Roaming,
            Entities = new List<EntityData>(){
                new TerrainData{
                    GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                    LocalPos = Vector3.Zero,
                    TextureName = "town",
                    ModelName = "woods",
                },
                new TerrainData{
                    GridPos = Vector3.Zero,
                    LocalPos = Vector3.Zero,
                    ModelName = "skybox",
                    TextureName = "sky",
                },
                new DoorData{
                    GridPos = new Vector3(8, 0, 15),
                    LocalPos = new Vector3(0, -0.5f, 0),
                    TextureName = "door_f",
                    Side = Direction.South,
                    HoverText = "To the Town",
                    Scale = new Vector3(1.4f, 1, 1),
                    DoorKind = DoorKind.Town,
                },
                new DoorData{
                    GridPos = new Vector3(0, 0, 15),
                    LocalPos = new Vector3(0, -0.5f, 0),
                    TextureName = "door_f",
                    Side = Direction.South,
                    HoverText = "To the Manor",
                    Scale = new Vector3(1.4f, 1, 1),
                    DoorKind = DoorKind.ManorGardens,
                },
                new DoorData{
                    GridPos = new Vector3(15, 0, 15),
                    LocalPos = new Vector3(0, -0.5f, 0),
                    TextureName = "door_f",
                    Side = Direction.South,
                    HoverText = "To the Farm",
                    Scale = new Vector3(1.4f, 1, 1),
                    DoorKind = DoorKind.Farm,
                },
                new DoorData{
                    GridPos = new Vector3(10, 0, 3),
                    LocalPos = new Vector3(0, -0.25f, 0),
                    TextureName = "door_a",
                    Side = Direction.West,
                    HoverText = "Enter the Sewers",
                    Scale = new Vector3(1, 1, 1),
                    DoorKind = DoorKind.Sewer,
                },
            }
        };

        data.Entities.AddRange(ForestGen.Generate());

        return data;
    }
}