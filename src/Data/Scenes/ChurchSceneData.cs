
using System.Numerics;

public static class ChurchSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "church",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>()
                {
                    new TerrainData{
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "churchinterior",
                        TextureName = "churchinterior",
                    },
                    new DoorData
                    {
                        GridPos = new Vector3(3, 0, 7),
                        LocalPos = new Vector3(0, 0.25f, 0),
                        TextureName = "churchdoor",
                        Side = Direction.South,
                        HoverText = "Exit The Church",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new PersonData
                    {
                        GridPos = new Vector3(0, 0, 0),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person_priest",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to the priest",
                        PersonKind = PersonKind.Priest,
                    }
                }
    };
}