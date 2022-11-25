
using static Raylib_cs.Raylib;

public enum PlayerMode
{
    Man,
    Mouse,
}

public class GameStage : IStage
{
    public void Init()
    {
        HideCursor();

        RootGameState.Instance.Init();

        var startScene = RootGameState.Instance.SceneCache[AreaKind.Inn];
        SceneManager.Instance.Push(startScene);

        PlayMusicStream(ResourceManager.Instance.Music["3_mainTheme"]);
    }

    public void Update(float deltaTime)
    {
        UpdateMusicStream(ResourceManager.Instance.Music["3_mainTheme"]);
        SceneManager.Instance.Update(deltaTime);
        BeginDrawing();
        {
            SceneManager.Instance.Render(deltaTime);
        }
        EndDrawing();
    }

    public void Deinit()
    {
        StopMusicStream(ResourceManager.Instance.Music["3_mainTheme"]);
    }
}