
using System.Numerics;

[Serializable]
public class RootGameState
{
    public PlayerMode PlayerMode { get; set; }
    public Vector3 PlayerGridPos { get; set; }
    public Direction PlayerDirection { get; set; }
    public AreaKind CurrentArea { get; set; }
    public Dictionary<AreaKind, IScene> Scenes = new Dictionary<AreaKind, IScene>();
    public List<ItemKind> Inventory { get; set; } = new List<ItemKind>();
    public PersonKind CurrentConversationTarget { get; set; } = PersonKind.Nobody;
}