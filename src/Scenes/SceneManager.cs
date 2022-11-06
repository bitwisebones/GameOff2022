
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

    private Stack<IScene> _sceneStack { get; } = new Stack<IScene>();

    public RootGameState Update(float deltaTime, RootGameState gameState)
    {
        var scene = _sceneStack.Peek();
        return scene.Update(deltaTime, gameState);
    }

    public void Render(float deltaTime, ref RenderBundle renderBundle)
    {
        var scene = _sceneStack.Peek();
        scene.Render(deltaTime, ref renderBundle);
    }

    public void Push(IScene scene)
    {
        _sceneStack.Push(scene);
    }

    public void Pop()
    {
        _ = _sceneStack.Pop();
    }

    public void Replace(IScene scene)
    {
        Pop();
        Push(scene);
    }
}