
public enum Stage
{
    Loading,
    MainMenu,
    Intro,
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

    public void GoTo(Stage scene)
    {
        CurrentScene.Deinit();

        switch (scene)
        {
            case Stage.Loading:
                CurrentScene = new LoadingStage();
                break;
            case Stage.MainMenu:
                CurrentScene = new MainMenuStage();
                break;
            case Stage.Intro:
                CurrentScene = new IntroStage();
                break;
            case Stage.Game:
                CurrentScene = new GameStage();
                break;
        }

        CurrentScene.Init();
    }
}