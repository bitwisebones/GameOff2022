
using System.Numerics;

public enum Area
{
    Town,
    Inn,
    Church,
    Tailor,
    Blacksmith,
    Woods,
    Manor,
    ManorGardens,
    Farm,
    FarmHouse,
    Sewer,
}

[Serializable]
public class RootGameState
{
    public PlayerMode PlayerMode { get; set; }
    public Vector3 PlayerGridPos { get; set; }
    public Direction PlayerDirection { get; set; }
    public Area CurrentArea { get; set; }
    public List<string> Inventory { get; set; } = new List<string>();
}