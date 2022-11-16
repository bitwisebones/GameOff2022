using System.Numerics;

public static class ManorGroundsSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "manorgrounds",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new TerrainData {
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "manorgrounds",
            },
            new TerrainData {
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "manorbuilding",
                ModelName = "manorbuilding",
            },
            new TerrainData{
                GridPos = Vector3.Zero,
                LocalPos = Vector3.Zero,
                ModelName = "skybox",
                TextureName = "sky",
            },
            new DoorData{
                GridPos = new Vector3(15, 0, 7),
                LocalPos = new Vector3(0, -0.6f, 0),
                TextureName = "door_e",
                Side = Direction.East,
                HoverText = "To the Town",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.Town,
            },
            new DoorData{
                GridPos = new Vector3(15, 0, 8),
                LocalPos = new Vector3(0, -0.6f, 0),
                TextureName = "door_e",
                Side = Direction.East,
                HoverText = "To the Town",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.Town,
            },
            new DoorData{
                GridPos = new Vector3(1, 0, 1),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_a",
                Side = Direction.North,
                HoverText = "To the Woods",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Woods,
            },
            new DoorData{
                GridPos = new Vector3(4, 0, 7.5f),
                LocalPos = new Vector3(0, 0.25f, 0),
                TextureName = "churchdoor",
                Side = Direction.West,
                HoverText = "Enter the Manor",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.ManorHouse,
            },
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = new Vector3(0, 0.2f, 0),
                ModelName = "fountain",
                TextureName = "town",
            },
            new BillboardData{
                GridPos = new Vector3(13, 0, 2),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_b",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(13, 0, 13),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_b",
                Scale = new Vector3(15, 15, 1),
            },
        }
    };
}