
using System.Numerics;

public static class Scenes
{
    public static Dictionary<AreaKind, SceneData> SceneDataSource = new Dictionary<AreaKind, SceneData>()
    {
        {
            AreaKind.Town,
            new SceneData
            {
                Name = "town",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData{
                        Name = "town_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "town",
                        TextureName = "town",
                        AreaKind = AreaKind.Town,
                    },
                    new PersonData{
                        Name = "blacksmith",
                        GridPos = new Vector3(6, 0, 9),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Person",
                        PersonKind = PersonKind.Blacksmith,
                    },
                    new DoorData{
                        Name = "inn_door",
                        GridPos = new Vector3(4, 0, 12),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "Enter the Inn",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Inn,
                    },
                    new DoorData{
                        Name = "church_door",
                        GridPos = new Vector3(7, 0, 5),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.North,
                        HoverText = "Enter the Church",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Church,
                    },
                    new DoorData{
                        Name = "blacksmith_door",
                        GridPos = new Vector3(11, 0, 8),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.North,
                        HoverText = "Enter the Blacksmith's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Blacksmith,
                    },
                    new DoorData{
                        Name = "tailor_door",
                        GridPos = new Vector3(8, 0, 11),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "Enter the Tailor's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Tailor,
                    },
                    new DoorData{
                        Name = "woods_door",
                        GridPos = new Vector3(1, 0, 1),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.North,
                        HoverText = "To the Woods",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Woods,
                    },
                    new DoorData{
                        Name = "farm_door",
                        GridPos = new Vector3(13, 0, 8),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "To the Farm",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Farm,
                    },
                    new DoorData{
                        Name = "manor_door",
                        GridPos = new Vector3(1, 0, 9),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.West,
                        HoverText = "To the Manor",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.ManorGrounds,
                    },
                    new ItemData
                    {
                        Name = "blacksmith_key",
                        GridPos = new Vector3(9, 0, 11),
                        LocalPos = new Vector3(0, -0.75f, 0),
                        TextureName = "key",
                        Side = Direction.South,
                        HoverText = "A small brass skeleton key",
                        Scale = new Vector3(0.5f, 0.5f, 0.5f),
                        ItemKind = ItemKind.BlacksmithKey,
                    }
                }
            }
        },
        {
            AreaKind.Inn,
            new SceneData
            {
                Name = "inn",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>
                {
                    new TerrainData{
                        Name = "inn_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "inn",
                        AreaKind = AreaKind.Inn,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(2, 0, 3),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "Exit The Inn",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    }
                }
            }
        },
        {
            AreaKind.Church,
            new SceneData
            {
                Name = "church",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>()
                {
                    new TerrainData{
                        Name = "church_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "church",
                        AreaKind = AreaKind.Church,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(3, 0, 7),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "Exit The Church",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    }
                }
            }
        },
        {
            AreaKind.Woods,
            new SceneData{
                Name = "woods",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>(){
                    new TerrainData{
                        Name = "woods_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "woods",
                        AreaKind = AreaKind.Woods,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(8, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new DoorData{
                        Name = "manor_door",
                        GridPos = new Vector3(0, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "To the Manor",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.ManorGrounds,
                    },
                    new DoorData{
                        Name = "farm_door",
                        GridPos = new Vector3(15, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "To the Farm",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Farm,
                    }
                }
            }
        },
        {
            AreaKind.Farm,
            new SceneData{
                Name = "farm",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData{
                        Name = "farm_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "farm",
                        AreaKind = AreaKind.Farm,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(0, 0, 12),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.West,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new DoorData{
                        Name = "woods_door",
                        GridPos = new Vector3(2, 0, 0),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.North,
                        HoverText = "To the Woods",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Woods,
                    },
                    new DoorData{
                        Name = "farmhouse_door",
                        GridPos = new Vector3(9, 0, 11),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "Enter the Farmhouse",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Farmhouse,
                    }
                }
            }
        },
        {
            AreaKind.ManorGrounds,
            new SceneData{
                Name = "manorgrounds",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData {
                        Name = "manor_grounds_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "manorgrounds",
                        AreaKind = AreaKind.ManorGrounds,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(15, 0, 7),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new DoorData{
                        Name = "woods_door",
                        GridPos = new Vector3(1, 0, 1),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.North,
                        HoverText = "To the Woods",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Woods,
                    },
                }
            }
        },
        {
            AreaKind.Tailor,
            new SceneData{
                Name = "tailor",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData> {
                    new TerrainData{
                        Name = "tailor_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "tailor",
                        AreaKind = AreaKind.Tailor,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(8, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    }
                }
            }
        },
        {
            AreaKind.Blacksmith,
            new SceneData{
                Name = "blacksmith",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData> {
                    new TerrainData{
                        Name = "blacksmith_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "blacksmith",
                        AreaKind = AreaKind.Blacksmith,
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(8, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    }
                }
            }
        },
        {
            AreaKind.FarmHouse,
            new SceneData{
                Name = "farmhouse",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData{
                        Name = "farmhouse_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "farmhouse",
                        AreaKind = AreaKind.FarmHouse,
                    },
                    new DoorData{
                        Name = "farm_door",
                        GridPos = new Vector3(8, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.South,
                        HoverText = "Exit the Farmhouse",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Farm,
                    }
                }
            }
        }
    };

    public static Dictionary<(AreaKind, AreaKind), (Vector3, Direction)> Transitions = new Dictionary<(AreaKind, AreaKind), (Vector3, Direction)>()
    {
        {(AreaKind.Town, AreaKind.Church), (new Vector3(3, 0, 7), Direction.North)},
        {(AreaKind.Town, AreaKind.Inn), (new Vector3(2, 0, 3), Direction.North)},
        {(AreaKind.Town, AreaKind.Woods), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.Town, AreaKind.Farm), (new Vector3(0, 0, 12), Direction.East)},
        {(AreaKind.Town, AreaKind.ManorGrounds), (new Vector3(15, 0, 7), Direction.West)},
        {(AreaKind.Town, AreaKind.Tailor), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.Town, AreaKind.Blacksmith), (new Vector3(8, 0, 15), Direction.North)},

        {(AreaKind.Church, AreaKind.Town), (new Vector3(7, 0, 5), Direction.South)},
        {(AreaKind.Inn, AreaKind.Town), (new Vector3(4, 0, 12), Direction.North)},
        {(AreaKind.Tailor, AreaKind.Town), (new Vector3(8, 0, 11), Direction.North)},
        {(AreaKind.Blacksmith, AreaKind.Town), (new Vector3(11, 0, 8), Direction.South)},

        {(AreaKind.Woods, AreaKind.Town), (new Vector3(1, 0, 1), Direction.South)},
        {(AreaKind.Woods, AreaKind.ManorGrounds), (new Vector3(1, 0, 1), Direction.South)},
        {(AreaKind.Woods, AreaKind.Farm), (new Vector3(2, 0, 0), Direction.South)},

        {(AreaKind.Farm, AreaKind.Woods), (new Vector3(15, 0, 15), Direction.North)},
        {(AreaKind.Farm, AreaKind.Town), (new Vector3(13, 0, 8), Direction.West)},
        {(AreaKind.Farm, AreaKind.FarmHouse), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.FarmHouse, AreaKind.Farm), (new Vector3(9, 0, 11), Direction.West)},

        {(AreaKind.ManorGrounds, AreaKind.Town), (new Vector3(1, 0, 9), Direction.East)},
        {(AreaKind.ManorGrounds, AreaKind.Woods), (new Vector3(0, 0, 15), Direction.North)},
        {(AreaKind.ManorGrounds, AreaKind.ManorHouse), (new Vector3(0, 0, 0), Direction.West)}, // @TODO

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
            DoorKind.Woods => AreaKind.Woods,
            DoorKind.Farmhouse => AreaKind.FarmHouse,
            _ => throw new NotImplementedException($"{door} in GetAreaFromDoor"),
        };
    }
}