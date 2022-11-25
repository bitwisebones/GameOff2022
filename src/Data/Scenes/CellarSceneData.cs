
using System.Numerics;

public static class CellarSceneData
{
    public static SceneData GetData()
    {
        return new SceneData
        {
            Name = "cellar",
            SceneType = SceneType.Roaming,
            FogColor = new Vector4(0f, 0f, 0f, 1.0f),
            FogDensity = 0.15f,
            Entities = new List<EntityData> {
                new TerrainData{
                    ModelName = "cellar",
                    TextureName = "town",
                    GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                    LocalPos = Vector3.Zero,
                },
                new DoorData {
                    GridPos = new Vector3(0, 0, 2),
                    LocalPos = new Vector3(0, -0.25f, 0),
                    TextureName = "door_c",
                    DoorKind = DoorKind.Sewer,
                    HoverText = "Exit the Manor cellar",
                    Scale = Vector3.One,
                    Side = Direction.West,
                },
                new DoorData {
                    GridPos = new Vector3(6, 0, 2),
                    LocalPos = new Vector3(0, -0.25f, 0),
                    TextureName = "door_c",
                    DoorKind = DoorKind.None,
                    HoverText = "It's locked.",
                    Scale = Vector3.One,
                    Side = Direction.East,
                },
                new PersonData{
                    GridPos = new Vector3(3, 0, 2),
                    LocalPos = new Vector3(0, -0.25f, 0),
                    HoverText = "Talk to Magnus",
                    PersonKind = PersonKind.Lord,
                    Scale = new Vector3(1.5f, 1.5f, 0),
                    TextureName = "person_lord",
                },
            }
        };
    }
}