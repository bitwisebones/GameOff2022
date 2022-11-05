
public class SceneManager
{
    private static SceneManager? _sceneManager;
    public static SceneManager Instance
    {
        get
        {
            if (_sceneManager == null)
            {
                _sceneManager = new SceneManager();
            }
            return _sceneManager;
        }
    }

    private SceneManager() { }

    public Stack<IScene> SceneStack { get; } = new Stack<IScene>();

    public RootGameState Update(float deltaTime, RootGameState gameState)
    {
        var scene = SceneStack.Peek();
        return scene.Update(deltaTime, gameState);
    }

    public void Render(float deltaTime, ref RenderBundle renderBundle)
    {
        var scene = SceneStack.Peek();
        scene.Render(deltaTime, ref renderBundle);
    }
}