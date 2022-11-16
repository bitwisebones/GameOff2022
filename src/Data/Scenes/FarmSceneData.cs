using System.Numerics;

public static class FarmSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "farm",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "town",
                ModelName = "farm",
            },
            new TerrainData{
                GridPos = Vector3.Zero,
                LocalPos = Vector3.Zero,
                ModelName = "skybox",
                TextureName = "sky",
            },
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "farmbuildings",
                ModelName = "farmbuildings",
            },
            new DoorData{
                GridPos = new Vector3(0, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_f",
                Side = Direction.West,
                HoverText = "To the Town",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.Town,
            },
            new DoorData{
                GridPos = new Vector3(2, 0, 0),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_f",
                Side = Direction.North,
                HoverText = "To the Woods",
                Scale = new Vector3(1.4f, 1, 1),
                DoorKind = DoorKind.Woods,
            },
            new DoorData{
                GridPos = new Vector3(9, 0, 11),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_c",
                Side = Direction.East,
                HoverText = "It's locked.",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Farmhouse,
            },
            new DoorData{
                GridPos = new Vector3(2, 0, 2),
                LocalPos = new Vector3(0, 0.25f, 1),
                TextureName = "churchdoor",
                Side = Direction.East,
                HoverText = "It's locked.",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Farmhouse,
            },
            new PersonData{
                GridPos = new Vector3(9, 0, 10),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "person",
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to Samuel Matthews",
                PersonKind = PersonKind.Farmer,
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(5, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(5, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 10),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(5, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 12),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(3, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(5, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(7, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 13),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "bush_c",
                Scale = new Vector3(1, 1, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 6),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 6),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 6),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(10, 0, 6),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(4, 0, 8),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(6, 0, 8),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 8),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(10, 0, 8),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
        }
    };
}