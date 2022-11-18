
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

    private AreaKind? _nextArea = null;

    private Stack<IScene> _sceneStack { get; } = new Stack<IScene>();

    // Only top scene gets updated
    public void Update(float deltaTime)
    {
        var gameState = RootGameState.Instance;

        if (_nextArea != null)
        {
            var (newScene, gridPos, dir) = Scenes.GetScene(_nextArea.Value, gameState.CurrentArea);
            SceneManager.Instance.Replace(newScene);
            gameState.PlayerDirection = dir;
            gameState.PlayerGridPos = gridPos;
            gameState.CurrentArea = _nextArea.Value;
            _nextArea = null;
        }

        var scene = _sceneStack.Peek();
        scene.Update(deltaTime);
    }

    // All scenes in the stack are rendered
    public void Render(float deltaTime)
    {
        foreach (var scene in _sceneStack.Reverse())
        {
            scene.Render(deltaTime);
        }
    }

    public void Push(IScene scene)
    {
        scene.Init();
        _sceneStack.Push(scene);
    }

    public void Pop()
    {
        _ = _sceneStack.Pop();
    }

    public void Replace(IScene scene)
    {
        Pop();
        scene.Init();
        Push(scene);
    }

    public IScene Peek()
    {
        return _sceneStack.Peek();
    }

    public void TransitionTo(AreaKind area)
    {
        _nextArea = area;
    }
}