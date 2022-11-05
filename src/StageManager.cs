
public enum Scene
{
    Loading,
    MainMenu,
    Game,
}

public class StageManager
{
    private static StageManager? _instance;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StageManager();
            }
            return _instance;
        }
    }

    private StageManager() { }

    public IStage CurrentScene { get; private set; } = new LoadingStage();

    public void GoTo(Scene scene)
    {
        CurrentScene.Deinit();

        switch (scene)
        {
            case Scene.Loading:
                CurrentScene = new LoadingStage();
                break;
            case Scene.MainMenu:
                CurrentScene = new MainMenuStage();
                break;
            case Scene.Game:
                CurrentScene = new GameStage();
                break;
        }

        CurrentScene.Init();
    }
}