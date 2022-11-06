
using System.Numerics;

public static class Scenes
{
    public static SceneData Town = new SceneData
    {
        Name = "town",
        PlayerSpawnDirection = Direction.South,
        PlayerSpawnGridPos = new Vector3(1, 0, 1),
        SceneType = SceneType.Roaming,
        Interactables = new List<InteractableData>{
            new InteractableData{
                Name = "test",
                Texture = "wall",
                GridPos = new Vector3(1, 0, 1),
                Side = Direction.South,
                LocalPos = new Vector3(0, 0, 0),
                IsBillboard = false,
            },
            // new InteractableData{
            //     Name = "other",
            //     Texture = "wall",
            //     GridPos = new Vector3(5, 0, 5),
            //     Side = Direction.South,
            //     LocalPos = new Vector3(0, 0, 0),
            //     IsBillboard = true,
            // }
        }
    };
}