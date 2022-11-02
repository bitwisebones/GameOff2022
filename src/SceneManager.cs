
public enum Scene
{
    Loading,
    MainMenu,
    Game,
}

public class SceneManager
{
    private static SceneManager? _instance;
    public static SceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneManager();
            }
            return _instance;
        }
    }

    private SceneManager() { }

    public IScene CurrentScene { get; private set; } = new LoadingScene();

    public void GoTo(Scene scene)
    {
        switch (scene)
        {
            case Scene.Loading:
                CurrentScene = new LoadingScene();
                break;
            case Scene.MainMenu:
                CurrentScene = new MainMenuScene();
                break;
            case Scene.Game:
                break;
        }
    }
}