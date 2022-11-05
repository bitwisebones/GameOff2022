
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

    public Stack<Scene> SceneStack { get; } = new Stack<Scene>();

    public void Update(float deltaTime)
    {
        var scene = SceneStack.Peek();
        scene.Update(deltaTime);
    }
}