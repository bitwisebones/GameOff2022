using System.Numerics;

public static class TailorSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "tailor",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData> {
            new TerrainData{
                GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                LocalPos = Vector3.Zero,
                TextureName = "tailorinterior",
                ModelName = "tailorinterior",
            },
            new DoorData{
                GridPos = new Vector3(1, 0, 0),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_b",
                Side = Direction.North,
                HoverText = "Exit the Tailor's Shop",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.Town,
            },
            new DoorData{
                GridPos = new Vector3(0, 0, 3),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "door_b",
                Side = Direction.South,
                HoverText = "It's locked.",
                Scale = new Vector3(1, 1, 1),
                DoorKind = DoorKind.None,
            },
            new PersonData{
                GridPos = new Vector3(1, 0, 2),
                LocalPos = new Vector3(0, -0.25f, 0),
                TextureName = "person_tailor",
                Scale = new Vector3(1.5f, 1.5f, 0),
                HoverText = "Talk to the tailor",
                PersonKind = PersonKind.Tailor,
            }
        }
    };
}