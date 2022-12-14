
using System.Numerics;

public static class InnSceneData
{
    public static SceneData GetData() => new SceneData
    {
        Name = "inn",
        SceneType = SceneType.Roaming,
        Entities = new List<EntityData>
                {
                    new TerrainData{
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "inninterior",
                        ModelName = "inninterior",
                    },
                    new DoorData{
                        GridPos = new Vector3(3, 0, 2),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_d",
                        Side = Direction.North,
                        HoverText = "Exit The Inn",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new PersonData{
                        GridPos = new Vector3(3, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person_innkeeper",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to the innkeeper",
                        PersonKind = PersonKind.InnKeeper,
                    },
                    new PersonData{
                        GridPos = new Vector3(0, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person_innkeepershusband",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to the innkeeper's husband",
                        PersonKind = PersonKind.InnKeepersHusband,
                    }
                }
    };
}