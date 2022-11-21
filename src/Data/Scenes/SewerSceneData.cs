using System.Numerics;

public static class SewerSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "sewers",
        SceneType = SceneType.Roaming,
        FogColor = new Vector4(0f, 0f, 0f, 1.0f),
        FogDensity = 0.25f,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "sewers",
            },
            new DoorData{
                GridPos = new Vector3(15, 0, 0),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_c",
                Side = Direction.East,
                HoverText = "Exit the Sewers",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Woods,
            },
            new DoorData{
                GridPos = new Vector3(0, 0, 15),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_c",
                Side = Direction.West,
                HoverText = "Exit the Sewers",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.ManorHouse,
            },
            new ItemData{
                GridPos = new Vector3(14, 0, 3),
                HoverText = "???",
                ItemKind = ItemKind.RubblePile,
                Scale = Vector3.One * 2,
                LocalPos = new Vector3(0, 0, -0.4f),
                Side = Direction.South,
                TextureName = "rubble",
            }
        }
    };
}