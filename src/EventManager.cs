
public class EventManager
{
    private static EventManager? _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }
            return _instance;
        }
    }

    private EventManager() { }
}

public class GameEvent<T>
{
    public delegate void Handler(T args);
    public event Handler? Event;
    public void RaiseEvent(T args)
    {
        Event?.Invoke(args);
    }
}

public class GameEvent
{
    public delegate void Handler();
    public event Handler? Event;
    public void RaiseEvent()
    {
        Event?.Invoke();
    }
}