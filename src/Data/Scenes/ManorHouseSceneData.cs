using System.Numerics;

public static class ManorHouseSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "manorinterior",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>{
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "manorinterior",
                ModelName = "manorinterior",
            },
            new DoorData{
                GridPos = new Vector3(5, 0, 8f),
                LocalPos = new Vector3(0, 0.25f, 0),
                TextureName = "churchdoor",
                Side = Direction.East,
                HoverText = "Exit the Manor",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.ManorGrounds,
            },
            new PersonData {
                GridPos = new Vector3(2, 0, 7),
                LocalPos = new Vector3(0, -0.25f, 0),
                HoverText = "Talk to the butler",
                PersonKind = PersonKind.Butler,
                Scale = new Vector3(1.5f, 1.5f, 1.5f),
                TextureName = "person_butler",
            },
            new DoorData{
                GridPos = new Vector3(3, 0, 6),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_d",
                Side = Direction.North,
                HoverText = "It's locked.",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.None,
            },
            new DoorData{
                GridPos = new Vector3(3, 0, 10),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_d",
                Side = Direction.South,
                HoverText = "It's locked.",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.None,
            },
        }
    };
}