
using static Raylib_cs.Raylib;

public enum PlayerMode
{
    Man,
    Mouse,
}

public class GameStage : IStage
{
    private RenderBundle _renderBundle = new RenderBundle();

    public void Init()
    {
        HideCursor();

        RootGameState.Instance.Init();

        var townScene = RootGameState.Instance.SceneCache[AreaKind.Town];
        SceneManager.Instance.Push(townScene);

        _renderBundle.RenderTexture = LoadRenderTexture(GetScreenWidth() / 4, GetScreenHeight() / 4);
    }

    public void Update(float deltaTime)
    {
        var renderBundle = _renderBundle;
        SceneManager.Instance.Update(deltaTime);
        SceneManager.Instance.Render(deltaTime, ref renderBundle);
    }

    public void Deinit() { }
}