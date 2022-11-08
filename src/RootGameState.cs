
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

        PlayerDirection = startingScene.PlayerSpawnDirection;
        PlayerGridPos = startingScene.PlayerSpawnGridPos;
        PlayerMode = PlayerMode.Man;
        CurrentArea = AreaKind.Town;
        Inventory = new List<ItemKind>();

        SceneCache = new Dictionary<AreaKind, IScene>
        {
            {AreaKind.Town, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Town])},
            {AreaKind.Church, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Church])},
            {AreaKind.Inn, SceneFactory.Build(Scenes.SceneDataSource[AreaKind.Inn])},
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