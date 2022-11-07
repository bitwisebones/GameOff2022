
using static Raylib_cs.Raylib;

public enum PlayerMode
{
    Man,
    Mouse,
}

public class GameStage : IStage
{
    private RootGameState? _gameState;
    private RenderBundle _renderBundle = new RenderBundle();

    public void Init()
    {
        HideCursor();

        _gameState = new RootGameState
        {
            PlayerDirection = Scenes.Town.PlayerSpawnDirection,
            PlayerGridPos = Scenes.Town.PlayerSpawnGridPos,
            PlayerMode = PlayerMode.Man,
            CurrentArea = Area.Town,
            Inventory = new List<string>(),
            Scenes = new Dictionary<Area, IScene>
            {
                {Area.Town, SceneFactory.Build(Scenes.Town)},
                {Area.Church, SceneFactory.Build(Scenes.Church)},
                {Area.Inn, SceneFactory.Build(Scenes.Inn)},
            },
        };

        var townScene = _gameState.Scenes[Area.Town];
        SceneManager.Instance.Push(townScene);

        _renderBundle.RenderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);
    }

    public void Update(float deltaTime)
    {
        var renderBundle = _renderBundle;
        var gameState = _gameState!;
        var newGameState = SceneManager.Instance.Update(deltaTime, gameState);
        SceneManager.Instance.Render(deltaTime, ref renderBundle, gameState);
        _gameState = newGameState;
    }

    public void Deinit() { }
}