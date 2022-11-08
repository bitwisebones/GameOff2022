
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

    // Only top scene gets updated
    public void Update(float deltaTime)
    {
        var scene = _sceneStack.Peek();
        scene.Update(deltaTime);
    }

    // All scenes in the stack are rendered
    public void Render(float deltaTime, ref RenderBundle renderBundle)
    {
        foreach (var scene in _sceneStack)
        {
            scene.Render(deltaTime, ref renderBundle);
        }
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

    public IScene Peek()
    {
        return _sceneStack.Peek();
    }
}