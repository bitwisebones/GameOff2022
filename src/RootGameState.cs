
using System.Numerics;
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

    public bool IsLookingForEggs = false;
    public bool KnowsAboutTheSewer = false;
    public bool IsLookingForHorseshoes = false;
    public bool IsLookingForClippers = false;
    public bool IsLookingForFlowers = false;
    public bool IsLookingForPick = false;

    public Dictionary<AreaKind, IScene> SceneCache = new Dictionary<AreaKind, IScene>();
    public Dictionary<ItemKind, Item> ItemCache = new Dictionary<ItemKind, Item>();

    public void Init()
    {

        SceneCache = new Dictionary<AreaKind, IScene>
        {
            {AreaKind.Town, SceneFactory.Build(TownSceneData.GetData())},
            {AreaKind.Church, SceneFactory.Build(ChurchSceneData.GetData())},
            {AreaKind.Inn, SceneFactory.Build(InnSceneData.GetData())},
            {AreaKind.Woods, SceneFactory.Build(WoodsSceneData.GetData())},
            {AreaKind.Farm, SceneFactory.Build(FarmSceneData.GetData())},
            {AreaKind.ManorGrounds, SceneFactory.Build(ManorGroundsSceneData.GetData())},
            {AreaKind.ManorGardens, SceneFactory.Build(ManorGardensSceneData.GetData())},
            {AreaKind.ManorHouse, SceneFactory.Build(ManorHouseSceneData.GetData())},
            {AreaKind.Tailor, SceneFactory.Build(TailorSceneData.GetData())},
            {AreaKind.TailorGarden, SceneFactory.Build(TailorGardenSceneData.GetData())},
            {AreaKind.Blacksmith, SceneFactory.Build(BlacksmithSceneData.GetData())},
            {AreaKind.Sewer, SceneFactory.Build(SewerSceneData.GetData())},
            {AreaKind.Cellar, SceneFactory.Build(CellarSceneData.GetData())},
        };

        var startingScene = SceneCache[AreaKind.Inn];
        PlayerDirection = Direction.South;
        PlayerGridPos = new Vector3(1, 0, 1);
        PlayerMode = PlayerMode.Man;
        CurrentArea = AreaKind.Inn;
        Inventory = new List<ItemKind>();

        ItemCache = new Dictionary<ItemKind, Item>();

        foreach (var scene in SceneCache.Values)
        {
            foreach (var e in scene.Entities)
            {
                switch (e)
                {
                    case Item i:
                        if (i.ItemKind == ItemKind.None) continue;
                        ItemCache.Add(i.ItemKind, i);
                        break;
                }
            }
        }

        ItemCache.Add(ItemKind.Ale, new Item
        {
            ItemKind = ItemKind.Ale,
            Texture = ResourceManager.Instance.Textures["ale"],
            HoverText = "A tankard of ale"
        });

        ItemCache.Add(ItemKind.LoveLetter, new Item
        {
            ItemKind = ItemKind.LoveLetter,
            Texture = ResourceManager.Instance.Textures["loveletter"],
            HoverText = "A love letter"
        });

        ItemCache.Add(ItemKind.Horseshoe, new Item
        {
            ItemKind = ItemKind.Horseshoe,
            Texture = ResourceManager.Instance.Textures["horseshoe"],
            HoverText = "A horseshoe"
        });

        ItemCache.Add(ItemKind.Clippers, new Item
        {
            ItemKind = ItemKind.Clippers,
            Texture = ResourceManager.Instance.Textures["clippers"],
            HoverText = "Hedge clippers"
        });

        ItemCache.Add(ItemKind.Flowers, new Item
        {
            ItemKind = ItemKind.Flowers,
            Texture = ResourceManager.Instance.Textures["bush_d"],
            HoverText = "Beautiful flowers"
        });

        ItemCache.Add(ItemKind.Pickaxe, new Item
        {
            ItemKind = ItemKind.Pickaxe,
            Texture = ResourceManager.Instance.Textures["pickaxe"],
            HoverText = "A pickaxe"
        });
    }
}