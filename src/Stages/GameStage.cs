
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

        var townScene = RootGameState.Instance.SceneCache[AreaKind.Town];
        SceneManager.Instance.Push(townScene);
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