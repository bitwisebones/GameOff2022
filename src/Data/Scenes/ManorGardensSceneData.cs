using System.Numerics;

public static class ManorGardensSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "manorgardens",
        SceneType = SceneType.Roaming,
        FogColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f),
        FogDensity = 0.035f,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                ModelName = "manorgardens",
                TextureName = "town",
            },
            new TerrainData{
                GridPos = new Vector3(15.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                ModelName = "manorbuilding",
                TextureName = "manorbuilding",
            },
            new TerrainData{
                GridPos = new Vector3(8, 0, 8),
                LocalPos = Vector3.Zero,
                ModelName = "skybox",
                TextureName = "sky",
            },
            new DoorData{
                GridPos = new Vector3(1, 0, 0),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_e",
                Side = Direction.North,
                HoverText = "To the Woods",
                Scale = new Vector3(1.3f, 1, 1),
                DoorKind = DoorKind.Woods,
            },
            new DoorData{
                GridPos = new Vector3(15, 0, 1),
                LocalPos = new Vector3(0, -0.6f, 0),
                TextureName = "door_e",
                Side = Direction.East,
                HoverText = "To the Manor",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.ManorGrounds,
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 2),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 2),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(11, 0, 2),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 13),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 13),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(11, 0, 13),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new ItemData
            {
                GridPos = new Vector3(5, 0, 5),
                LocalPos = new Vector3(0, -0.75f, 0),
                TextureName = "key",
                Side = Direction.North,
                HoverText = "A small brass skeleton key",
                Scale = new Vector3(0.5f, 0.5f, 0.5f),
                ItemKind = ItemKind.BlacksmithKey,
            },
            new BillboardData{
                GridPos = new Vector3(10, 0, 9),
                LocalPos = new Vector3(-0.3f, -0.6f, 0),
                Scale = Vector3.One,
                TextureName = "bush_d"
            },
            new BillboardData{
                GridPos = new Vector3(10, 0, 9),
                LocalPos = new Vector3(0.3f, -0.6f, 0),
                Scale = Vector3.One,
                TextureName = "bush_d"
            }
        }
    };
}