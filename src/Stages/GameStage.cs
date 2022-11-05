
using System.Numerics;
using Raylib_cs;
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

        var x = new SceneData
        {
            Name = "town",
            PlayerSpawnDirection = Direction.South,
            PlayerSpawnGridPos = new Vector3(1, 0, 1),
            SceneType = SceneType.Roaming,
        };

        var townScene = SceneFactory.Build(x);
        SceneManager.Instance.SceneStack.Push(townScene);

        _gameState = new RootGameState
        {
            PlayerDirection = x.PlayerSpawnDirection,
            PlayerGridPos = x.PlayerSpawnGridPos,
            PlayerMode = PlayerMode.Man,
        };

        _renderBundle.RenderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);
    }

    public void Update(float deltaTime)
    {
        var renderBundle = _renderBundle;
        var gameState = _gameState!;
        var newGameState = SceneManager.Instance.Update(deltaTime, gameState);
        SceneManager.Instance.Render(deltaTime, ref renderBundle);
        _gameState = newGameState;
    }

    public void Deinit() { }
}