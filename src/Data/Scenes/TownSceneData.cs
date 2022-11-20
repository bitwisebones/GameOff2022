
using System.Numerics;

public static class TownSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "town",
        SceneType = SceneType.Roaming,
        FogColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f),
        FogDensity = 0.035f,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                ModelName = "town",
                TextureName = "town",
            },
            new TerrainData{
                GridPos = new Vector3(3.5f, -0.5f, -3.5f),
                LocalPos = Vector3.Zero,
                ModelName = "church",
                TextureName = "church",
            },
            new TerrainData{
                GridPos = new Vector3(6.5f, -0.5f, 11.5f),
                LocalPos = Vector3.Zero,
                ModelName = "tailor",
                TextureName = "tailor",
            },
            new TerrainData{
                GridPos = new Vector3(9.5f, -0.5f, 2.5f),
                LocalPos = Vector3.Zero,
                ModelName = "blacksmith",
                TextureName = "blacksmith",
            },
            new TerrainData{
                GridPos = new Vector3(0.5f, -0.5f, 10.5f),
                LocalPos = Vector3.Zero,
                ModelName = "inn",
                TextureName = "inn",
            },
            new DoorData{
                GridPos = new Vector3(4, 0, 12),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_d",
                Side = Direction.South,
                HoverText = "Enter the Inn",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Inn,
            },
            new DoorData{
                GridPos = new Vector3(7, 0, 5),
                LocalPos = new Vector3(0, 0.25f, 0.05f),
                TextureName = "churchdoor",
                Side = Direction.North,
                HoverText = "Enter the Church",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Church,
            },
            new DoorData{
                GridPos = new Vector3(12, 0, 8),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_c",
                Side = Direction.North,
                HoverText = "Enter the Blacksmith's Shop",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Blacksmith,
            },
            new DoorData{
                GridPos = new Vector3(8, 0, 11),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_b",
                Side = Direction.South,
                HoverText = "Enter the Tailor's Shop",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Tailor,
            },
            new DoorData{
                GridPos = new Vector3(1, 0, 1),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_e",
                Side = Direction.North,
                HoverText = "To the Woods",
                Scale = new Vector3(1.3f, 1, 1),
                DoorKind = DoorKind.Woods,
            },
            new DoorData{
                GridPos = new Vector3(13, 0, 8),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_f",
                Side = Direction.East,
                HoverText = "To the Farm",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.Farm,
            },
            new DoorData{
                GridPos = new Vector3(1, 0, 9),
                LocalPos = new Vector3(0, -0.5f, 0),
                TextureName = "door_f",
                Side = Direction.West,
                HoverText = "To the Manor",
                Scale = new Vector3(1, 1, 1.4f),
                DoorKind = DoorKind.ManorGrounds,
            },
            new ItemData
            {
                GridPos = new Vector3(9, 0, 11),
                LocalPos = new Vector3(0, -0.75f, 0),
                TextureName = "key",
                Side = Direction.South,
                HoverText = "A small brass skeleton key",
                Scale = new Vector3(0.5f, 0.5f, 0.5f),
                ItemKind = ItemKind.BlacksmithKey,
            },
            new PersonData{
                GridPos = new Vector3(2, 0, 1),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "person_gravedigger",
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to Rudy Duncan",
                PersonKind = PersonKind.Gravedigger,
            },
            new BillboardData{
                GridPos = new Vector3(2, 0, 2),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_c",
                Scale = new Vector3(15, 15, 1),
            },
            new BillboardData{
                GridPos = new Vector3(8, 0, 6),
                LocalPos = new Vector3(0, 5.5f, 0),
                TextureName = "tree_b",
                Scale = new Vector3(15, 15, 1),
            },
            new TerrainData{
                GridPos = new Vector3(8, 0, 8),
                LocalPos = Vector3.Zero,
                ModelName = "skybox",
                TextureName = "sky",
            }
        }
    };
}