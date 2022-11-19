
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
    }

    public void Update(float deltaTime)
    {
        SceneManager.Instance.Update(deltaTime);
        BeginDrawing();
        {
            SceneManager.Instance.Render(deltaTime);
        }
        EndDrawing();
    }

    public void Deinit() { }
}