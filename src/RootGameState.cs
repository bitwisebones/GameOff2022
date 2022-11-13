
using System.Numerics;

[Serializable]
public class RootGameState
{
    private static RootGameState? _gameState;
    public static RootGameState Instance
    {
        get
        {
            if (_gameState == null)
            {
                _gameState = new RootGameState();
            }
            return _gameState;
        }
    }

    public PlayerMode PlayerMode { get; set; }
    public Vector3 PlayerGridPos { get; set; }
    public Direction PlayerDirection { get; set; }
    public AreaKind CurrentArea { get; set; }
    public List<ItemKind> Inventory { get; set; } = new List<ItemKind>();
    public PersonKind CurrentConversationTarget { get; set; } = PersonKind.Nobody;

    public Dictionary<AreaKind, IScene> SceneCache = new Dictionary<AreaKind, IScene>();
    public Dictionary<ItemKind, ItemData> ItemCache = new Dictionary<ItemKind, ItemData>();

    public void Init()
    {
        var startingScene = Scenes.SceneDataSource[AreaKind.Town];
        PlayerDirection = Direction.South;
        PlayerGridPos = new Vector3(1, 0, 1);
        PlayerMode = PlayerMode.Man;
        CurrentArea = AreaKind.Town;
        Inventory = new List<ItemKind>();

        SceneCache = new Dictionary<AreaKind, IScene>
        {
            {AreaKind.Town, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Town])},
            {AreaKind.Church, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Church])},
            {AreaKind.Inn, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Inn])},
            {AreaKind.Woods, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Woods])},
            {AreaKind.Farm, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Farm])},
            {AreaKind.ManorGrounds, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.ManorGrounds])},
            {AreaKind.ManorGardens, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.ManorGardens])},
            {AreaKind.Tailor, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Tailor])},
            {AreaKind.TailorGarden, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.TailorGarden])},
            {AreaKind.Blacksmith, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Blacksmith])},
            {AreaKind.FarmHouse, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.FarmHouse])},
            {AreaKind.ManorHouse, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.ManorHouse])},
            {AreaKind.Sewer, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Sewer])},
        };

        ItemCache = new Dictionary<ItemKind, ItemData>();

        foreach (var sceneData in Scenes.SceneDataSource.Values)
        {
            foreach (var e in sceneData.Entities)
            {
                switch (e)
                {
                    case ItemData i:
                        ItemCache.Add(i.ItemKind, i);
                        break;
                }
            }
        }
    }
}