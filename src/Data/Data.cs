
using System.Numerics;

public static class Scenes
{
    public static Dictionary<(AreaKind, AreaKind), (Vector3, Direction)> Transitions = new Dictionary<(AreaKind, AreaKind), (Vector3, Direction)>()
    {
        {(AreaKind.Town, AreaKind.Church), (new Vector3(3, 0, 7), Direction.North)},
        {(AreaKind.Town, AreaKind.Inn), (new Vector3(3, 0, 2), Direction.South)},
        {(AreaKind.Town, AreaKind.Woods), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.Town, AreaKind.Farm), (new Vector3(0, 0, 12), Direction.East)},
        {(AreaKind.Town, AreaKind.ManorGrounds), (new Vector3(14, 0, 7), Direction.West)},
        {(AreaKind.Town, AreaKind.Tailor), (new Vector3(1, 0, 0), Direction.South)},
        {(AreaKind.Town, AreaKind.Blacksmith), (new Vector3(2, 0, 4), Direction.North)},

        {(AreaKind.Church, AreaKind.Town), (new Vector3(7, 0, 5), Direction.South)},
        {(AreaKind.Inn, AreaKind.Town), (new Vector3(4, 0, 12), Direction.North)},
        {(AreaKind.Tailor, AreaKind.Town), (new Vector3(8, 0, 11), Direction.North)},
        {(AreaKind.Blacksmith, AreaKind.Town), (new Vector3(12, 0, 8), Direction.South)},

        {(AreaKind.Woods, AreaKind.Town), (new Vector3(1, 0, 1), Direction.South)},
        {(AreaKind.Woods, AreaKind.ManorGardens), (new Vector3(1, 0, 0), Direction.South)},
        {(AreaKind.Woods, AreaKind.Farm), (new Vector3(2, 0, 0), Direction.South)},
        {(AreaKind.Woods, AreaKind.Sewer), (new Vector3(15, 0, 0), Direction.West)},

        {(AreaKind.Sewer, AreaKind.ManorHouse), (new Vector3(8, 0, 1), Direction.South)},
        {(AreaKind.Sewer, AreaKind.Woods), (new Vector3(10, 0, 3), Direction.East)},

        {(AreaKind.Farm, AreaKind.Woods), (new Vector3(15, 0, 15), Direction.North)},
        {(AreaKind.Farm, AreaKind.Town), (new Vector3(13, 0, 8), Direction.West)},

        {(AreaKind.ManorGrounds, AreaKind.Town), (new Vector3(1, 0, 9), Direction.East)},
        {(AreaKind.ManorGrounds, AreaKind.ManorHouse), (new Vector3(5, 0, 7), Direction.West)},
        {(AreaKind.ManorGardens, AreaKind.Woods), (new Vector3(0, 0, 15), Direction.North)},
        {(AreaKind.ManorHouse, AreaKind.ManorGrounds), (new Vector3(5, 0, 7), Direction.East)},
        {(AreaKind.ManorHouse, AreaKind.Sewer), (new Vector3(0, 0, 15), Direction.East)},

        {(AreaKind.Tailor, AreaKind.TailorGarden), (new Vector3(0, 0, 4), Direction.South)},
        {(AreaKind.TailorGarden, AreaKind.Tailor), (new Vector3(0, 0, 3), Direction.North)},
    };

    public static (IScene, Vector3, Direction) GetScene(AreaKind newArea, AreaKind currentArea)
    {
        var scene = RootGameState.Instance.SceneCache[newArea];
        var (pos, dir) = Transitions[(currentArea, newArea)];
        return (scene, pos, dir);
    }

    public static AreaKind GetAreaFromDoor(DoorKind door)
    {
        return door switch
        {
            DoorKind.Church => AreaKind.Church,
            DoorKind.Inn => AreaKind.Inn,
            DoorKind.Town => AreaKind.Town,
            DoorKind.Blacksmith => AreaKind.Blacksmith,
            DoorKind.Farm => AreaKind.Farm,
            DoorKind.ManorGrounds => AreaKind.ManorGrounds,
            DoorKind.Tailor => AreaKind.Tailor,
            DoorKind.TailorGarden => AreaKind.TailorGarden,
            DoorKind.Woods => AreaKind.Woods,
            DoorKind.Sewer => AreaKind.Sewer,
            DoorKind.ManorHouse => AreaKind.ManorHouse,
            DoorKind.ManorGardens => AreaKind.ManorGardens,
            DoorKind.Farmhouse => AreaKind.None,
            _ => AreaKind.None,
        };
    }
}