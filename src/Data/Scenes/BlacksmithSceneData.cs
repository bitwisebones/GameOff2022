using System.Numerics;

public static class BlacksmithSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "blacksmith",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData> {
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "blacksmithinterior",
                ModelName = "blacksmithinterior",
            },
            new DoorData{
                GridPos = new Vector3(2, 0, 4),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_c",
                Side = Direction.South,
                HoverText = "To the Town",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Town,
            },
            new PersonData{
                GridPos = new Vector3(2, 0, 2),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "person",
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to Edmund Fitch",
                PersonKind = PersonKind.Blacksmith,
            },
        }
    };
}