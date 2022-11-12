
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
                    },
                    new TerrainData{
                        Name = "churchbuilding",
                        GridPos = new Vector3(3.5f, -0.5f, -3.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "church",
                        TextureName = "church",
                    },
                    new TerrainData{
                        Name = "tailorbuilding",
                        GridPos = new Vector3(6.5f, -0.5f, 11.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "tailor",
                        TextureName = "tailor",
                    },
                    new TerrainData{
                        Name = "blacksmithbuilding",
                        GridPos = new Vector3(9.5f, -0.5f, 2.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "blacksmith",
                        TextureName = "blacksmith",
                    },
                    new TerrainData{
                        Name = "innbuilding",
                        GridPos = new Vector3(0.5f, -0.5f, 10.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "inn",
                        TextureName = "inn",
                    },
                    new DoorData{
                        Name = "inn_door",
                        GridPos = new Vector3(4, 0, 12),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_d",
                        Side = Direction.South,
                        HoverText = "Enter the Inn",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Inn,
                    },
                    new DoorData{
                        Name = "church_door",
                        GridPos = new Vector3(7, 0, 5),
                        LocalPos = new Vector3(0, 0.25f, 0.05f),
                        TextureName = "churchdoor",
                        Side = Direction.North,
                        HoverText = "Enter the Church",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Church,
                    },
                    new DoorData{
                        Name = "blacksmith_door",
                        GridPos = new Vector3(12, 0, 8),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_c",
                        Side = Direction.North,
                        HoverText = "Enter the Blacksmith's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Blacksmith,
                    },
                    new DoorData{
                        Name = "tailor_door",
                        GridPos = new Vector3(8, 0, 11),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_b",
                        Side = Direction.South,
                        HoverText = "Enter the Tailor's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Tailor,
                    },
                    new DoorData{
                        Name = "woods_door",
                        GridPos = new Vector3(1, 0, 1),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_e",
                        Side = Direction.North,
                        HoverText = "To the Woods",
                        Scale = new Vector3(1.3f, 1, 1),
                        DoorKind = DoorKind.Woods,
                    },
                    new DoorData{
                        Name = "farm_door",
                        GridPos = new Vector3(13, 0, 8),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_f",
                        Side = Direction.East,
                        HoverText = "To the Farm",
                        Scale = new Vector3(1, 1, 1.4f),
                        DoorKind = DoorKind.Farm,
                    },
                    new DoorData{
                        Name = "manor_door",
                        GridPos = new Vector3(1, 0, 9),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_f",
                        Side = Direction.West,
                        HoverText = "To the Manor",
                        Scale = new Vector3(1, 1, 1.4f),
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
                    },
                    new PersonData{
                        Name = "gravedigger",
                        GridPos = new Vector3(2, 0, 1),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Rudy Duncan",
                        PersonKind = PersonKind.Gravedigger,
                    },
                    new BillboardData{
                        Name = "tree",
                        GridPos = new Vector3(2, 0, 2),
                        LocalPos = new Vector3(0, 3f, 0),
                        TextureName = "tree_a",
                        Scale = new Vector3(8, 8, 1),
                    },
                    new BillboardData{
                        Name = "tree",
                        GridPos = new Vector3(8, 0, 6),
                        LocalPos = new Vector3(0, 3f, 0),
                        TextureName = "tree_a",
                        Scale = new Vector3(8, 8, 1),
                    },
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
                        TextureName = "inninterior",
                        ModelName = "inninterior",
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(3, 0, 2),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_d",
                        Side = Direction.North,
                        HoverText = "Exit The Inn",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new PersonData{
                        Name = "inn_keeper",
                        GridPos = new Vector3(3, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Meredith Farnsby",
                        PersonKind = PersonKind.InnKeeper,
                    },
                    new PersonData{
                        Name = "inn_keepers_husband",
                        GridPos = new Vector3(0, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Liam Farnsby",
                        PersonKind = PersonKind.InnKeepersHusband,
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
                        ModelName = "churchinterior",
                        TextureName = "churchinterior",
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(3, 0, 7),
                        LocalPos = new Vector3(0, 0.25f, 0),
                        TextureName = "churchdoor",
                        Side = Direction.South,
                        HoverText = "Exit The Church",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new PersonData{
                        Name = "priest",
                        GridPos = new Vector3(0, 0, 0),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Father Brooks",
                        PersonKind = PersonKind.Priest,
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
                    },
                    new DoorData{
                        Name = "sewer_door",
                        GridPos = new Vector3(10, 0, 3),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.West,
                        HoverText = "Enter the Sewers",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Sewer,
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
                    },
                    new PersonData{
                        Name = "farmer",
                        GridPos = new Vector3(8, 0, 10),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Samuel Matthews",
                        PersonKind = PersonKind.Farmer,
                    },
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
                    new DoorData{
                        Name = "manorhouse_door",
                        GridPos = new Vector3(13, 0, 7),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.West,
                        HoverText = "Enter the Manor",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.ManorHouse,
                    }
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
                        TextureName = "tailorinterior",
                        ModelName = "tailorinterior",
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(1, 0, 0),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_b",
                        Side = Direction.North,
                        HoverText = "Exit the Tailor's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new DoorData{
                        Name = "garden_door",
                        GridPos = new Vector3(0, 0, 3),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_b",
                        Side = Direction.South,
                        HoverText = "To the Garden",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.TailorGarden,
                    },
                    new PersonData{
                        Name = "tailor",
                        GridPos = new Vector3(1, 0, 2),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Charles Huges",
                        PersonKind = PersonKind.Tailor,
                    }
                }
            }
        },
        {
            AreaKind.TailorGarden,
            new SceneData {
                Name = "tailorgarden",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData> {
                    new TerrainData{
                        Name = "tailorbuilding",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        ModelName = "tailor",
                        TextureName = "tailor",
                    },
                    new DoorData{
                        Name = "tailor_door",
                        GridPos = new Vector3(0, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_b",
                        Side = Direction.North,
                        HoverText = "Enter the Tailor's Shop",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Tailor,
                    },
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
                        TextureName = "blacksmithinterior",
                        ModelName = "blacksmithinterior",
                    },
                    new DoorData{
                        Name = "town_door",
                        GridPos = new Vector3(2, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_c",
                        Side = Direction.South,
                        HoverText = "To the Town",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Town,
                    },
                    new PersonData{
                        Name = "blacksmith",
                        GridPos = new Vector3(2, 0, 2),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "person",
                        Scale = new Vector3(1.5f, 1.5f, 0),
                        HoverText = "Talk to Edmund Fitch",
                        PersonKind = PersonKind.Blacksmith,
                    },
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
        },
        {
            AreaKind.ManorHouse,
            new SceneData{
                Name = "manorhouse",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData{
                        Name = "manorhouse_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "manorhouse",
                    },
                    new DoorData{
                        Name = "manorgrounds_door",
                        GridPos = new Vector3(12, 0, 7),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "Exit the Manor",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.ManorGrounds,
                    },
                    new DoorData{
                        Name = "sewer_door",
                        GridPos = new Vector3(6, 0, 4),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "Exit the Manor",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Sewer,
                    }
                }
            }
        },
        {
            AreaKind.Sewer,
            new SceneData{
                Name = "sewers",
                SceneType = SceneType.Roaming,
                Entities = new List<EntityData>{
                    new TerrainData{
                        Name = "sewer_terrain",
                        GridPos = new Vector3(-0.5f, -0.5f, -0.5f),
                        LocalPos = Vector3.Zero,
                        TextureName = "town",
                        ModelName = "sewers",
                    },
                    new DoorData{
                        Name = "woods_door",
                        GridPos = new Vector3(15, 0, 0),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.East,
                        HoverText = "Exit the Sewers",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.Woods,
                    },
                    new DoorData{
                        Name = "manorhouse_door",
                        GridPos = new Vector3(0, 0, 15),
                        LocalPos = new Vector3(0, -0.25f, 0),
                        TextureName = "door_a",
                        Side = Direction.West,
                        HoverText = "Exit the Sewers",
                        Scale = new Vector3(1, 1, 1),
                        DoorKind = DoorKind.ManorHouse,
                    }
                }
            }
        }
    };

    public static Dictionary<(AreaKind, AreaKind), (Vector3, Direction)> Transitions = new Dictionary<(AreaKind, AreaKind), (Vector3, Direction)>()
    {
        {(AreaKind.Town, AreaKind.Church), (new Vector3(3, 0, 7), Direction.North)},
        {(AreaKind.Town, AreaKind.Inn), (new Vector3(3, 0, 2), Direction.South)},
        {(AreaKind.Town, AreaKind.Woods), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.Town, AreaKind.Farm), (new Vector3(0, 0, 12), Direction.East)},
        {(AreaKind.Town, AreaKind.ManorGrounds), (new Vector3(15, 0, 7), Direction.West)},
        {(AreaKind.Town, AreaKind.Tailor), (new Vector3(1, 0, 0), Direction.South)},
        {(AreaKind.Town, AreaKind.Blacksmith), (new Vector3(2, 0, 4), Direction.North)},

        {(AreaKind.Church, AreaKind.Town), (new Vector3(7, 0, 5), Direction.South)},
        {(AreaKind.Inn, AreaKind.Town), (new Vector3(4, 0, 12), Direction.North)},
        {(AreaKind.Tailor, AreaKind.Town), (new Vector3(8, 0, 11), Direction.North)},
        {(AreaKind.Blacksmith, AreaKind.Town), (new Vector3(12, 0, 8), Direction.South)},

        {(AreaKind.Woods, AreaKind.Town), (new Vector3(1, 0, 1), Direction.South)},
        {(AreaKind.Woods, AreaKind.ManorGrounds), (new Vector3(1, 0, 1), Direction.South)},
        {(AreaKind.Woods, AreaKind.Farm), (new Vector3(2, 0, 0), Direction.South)},
        {(AreaKind.Woods, AreaKind.Sewer), (new Vector3(15, 0, 0), Direction.West)},

        {(AreaKind.Sewer, AreaKind.ManorHouse), (new Vector3(6, 0, 4), Direction.West)},
        {(AreaKind.Sewer, AreaKind.Woods), (new Vector3(10, 0, 3), Direction.East)},

        {(AreaKind.Farm, AreaKind.Woods), (new Vector3(15, 0, 15), Direction.North)},
        {(AreaKind.Farm, AreaKind.Town), (new Vector3(13, 0, 8), Direction.West)},
        {(AreaKind.Farm, AreaKind.FarmHouse), (new Vector3(8, 0, 15), Direction.North)},
        {(AreaKind.FarmHouse, AreaKind.Farm), (new Vector3(9, 0, 11), Direction.West)},

        {(AreaKind.ManorGrounds, AreaKind.Town), (new Vector3(1, 0, 9), Direction.East)},
        {(AreaKind.ManorGrounds, AreaKind.Woods), (new Vector3(0, 0, 15), Direction.North)},
        {(AreaKind.ManorGrounds, AreaKind.ManorHouse), (new Vector3(12, 0, 7), Direction.West)},
        {(AreaKind.ManorHouse, AreaKind.ManorGrounds), (new Vector3(13, 0, 7), Direction.East)},
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
            DoorKind.Farmhouse => AreaKind.FarmHouse,
            DoorKind.Sewer => AreaKind.Sewer,
            DoorKind.ManorHouse => AreaKind.ManorHouse,
            _ => throw new NotImplementedException($"{door} in GetAreaFromDoor"),
        };
    }
}